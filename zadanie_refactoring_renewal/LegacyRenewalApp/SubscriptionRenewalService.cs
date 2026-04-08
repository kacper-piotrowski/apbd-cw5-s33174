using System;
using System.Collections.Generic;
using LegacyRenewalApp.Discounts;
using LegacyRenewalApp.Legacy;
using LegacyRenewalApp.Models;
using LegacyRenewalApp.Payments;
using LegacyRenewalApp.Repositories;
using LegacyRenewalApp.Supports;
using LegacyRenewalApp.Loyalty;

namespace LegacyRenewalApp
{
    public class SubscriptionRenewalService
    {
        private readonly IEnumerable<IDiscountStrategy> _discountStrategies;
        private readonly IEnumerable<IPaymentFeeStrategy> _paymentFeeStrategies;
        private readonly IEnumerable<ITaxStrategy> _taxStrategies;
        private readonly IEnumerable<ISupportFeeStrategy> _supportFeeStrategies;
        private readonly LoyaltyPointCalculator _loyaltyPointCalculator;
        private readonly ICustomerRepository _customerRepository;
        private readonly ISubscriptionPlanRepository _subscriptionPlanRepository;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IEmailSender _emailSender;

        public SubscriptionRenewalService(IEnumerable<IDiscountStrategy> discountStrategies,
            IEnumerable<IPaymentFeeStrategy> paymentFeeStrategies,
            IEnumerable<ITaxStrategy> taxStrategies,
            IEnumerable<ISupportFeeStrategy> supportFeeStrategies,
            LoyaltyPointCalculator loyaltyPointCalculator,
            ICustomerRepository customerRepository,
            ISubscriptionPlanRepository subscriptionPlanRepository,
            IInvoiceRepository invoiceRepository,
            IEmailSender emailSender)
        {
            _discountStrategies = discountStrategies;
            _paymentFeeStrategies = paymentFeeStrategies;
            _taxStrategies = taxStrategies;
            _supportFeeStrategies = supportFeeStrategies;
            _loyaltyPointCalculator = loyaltyPointCalculator;
            _customerRepository  =  customerRepository;
            _subscriptionPlanRepository = subscriptionPlanRepository;
            _invoiceRepository = invoiceRepository;
            _emailSender = emailSender;
        }

        public SubscriptionRenewalService()
        {
            _discountStrategies = new List<IDiscountStrategy>
            {
                new BasicLoyaltyDiscount(),
                new EducationDiscount(),
                new GoldDiscount(),
                new LargeTeamDiscount(),
                new LongTermLoyaltyDiscount(),
                new MediumTeamDiscount(),
                new PlatinumDiscount(),
                new SilverDiscount(),
                new SmallTeamDiscount()
            };
            _paymentFeeStrategies = new List<IPaymentFeeStrategy>
            {
                new BankTransferPayment(),
                new CardPayment(),
                new InvoicePayment(),
                new PaypalPayment()
            };
            _taxStrategies = new List<ITaxStrategy>
            {
                new CzechRepublicTax(),
                new GermanyTax(),
                new NorwayTax(),
                new PolandTax()
            };
            _supportFeeStrategies = new List<ISupportFeeStrategy>
            {
                new EnterpriseSupport(),
                new ProSupport(),
                new StartSupport()
            };
            _loyaltyPointCalculator = new LoyaltyPointCalculator();
            _customerRepository = new CustomerRepository();
            _subscriptionPlanRepository = new SubscriptionPlanRepository();
            _invoiceRepository = new LegacyBillingController();
            _emailSender = new LegacyBillingController();
        }

        public RenewalInvoice CreateRenewalInvoice(
            int customerId,
            string planCode,
            int seatCount,
            string paymentMethod,
            bool includePremiumSupport,
            bool useLoyaltyPoints)
        {
            if (customerId <= 0)
            {
                throw new ArgumentException("Customer id must be positive");
            }

            if (string.IsNullOrWhiteSpace(planCode))
            {
                throw new ArgumentException("Plan code is required");
            }

            if (seatCount <= 0)
            {
                throw new ArgumentException("Seat count must be positive");
            }

            if (string.IsNullOrWhiteSpace(paymentMethod))
            {
                throw new ArgumentException("Payment method is required");
            }

            string normalizedPlanCode = planCode.Trim().ToUpperInvariant();
            string normalizedPaymentMethod = paymentMethod.Trim().ToUpperInvariant();

            var customer = _customerRepository.GetById(customerId);
            var plan = _subscriptionPlanRepository.GetByCode(normalizedPlanCode);
            
            if (!customer.IsActive)
            {
                throw new InvalidOperationException("Inactive customers cannot renew subscriptions");
            }

            decimal baseAmount = (plan.MonthlyPricePerSeat * seatCount * 12m) + plan.SetupFee;
            decimal discountAmount = 0m;
            string notes = string.Empty;
            
            var checkDiscountValues = new RenewalDiscountValues(customer, plan, seatCount, baseAmount);

            foreach (var strategy in _discountStrategies)
            {
                if (strategy.CheckDiscount(checkDiscountValues))
                {
                    discountAmount += strategy.CalculateDiscount(discountAmount,baseAmount);
                    notes += strategy.DiscountNote();
                    break;
                }
            }

            if (useLoyaltyPoints)
            {
                int pointsToUse = _loyaltyPointCalculator.CalculatePointsToUse(customer.LoyaltyPoints);
                if (pointsToUse > 0)
                {
                    discountAmount += pointsToUse;
                    notes += $"loyalty points used: {pointsToUse}; ";
                }
            }

            decimal subtotalAfterDiscount = baseAmount - discountAmount;
            if (subtotalAfterDiscount < 300m)
            {
                subtotalAfterDiscount = 300m;
                notes += "minimum discounted subtotal applied; ";
            }

            decimal supportFee = 0m;
            if (includePremiumSupport)
            {
                foreach (var strategy in _supportFeeStrategies)
                {
                    if (strategy.CheckPlanCode(normalizedPlanCode))
                    {
                        supportFee = strategy.GetSupportFee();
                        break;
                    }
                }

                notes += "premium support included; ";
            }

            decimal paymentFee = 0m;
            bool paymentMethodFound = false;
            decimal feeAmount = subtotalAfterDiscount + supportFee;
            foreach (var strategy in _paymentFeeStrategies)
            {
                if (strategy.CheckPaymentMethod(normalizedPaymentMethod))
                {
                    paymentFee += strategy.CalculateFee(feeAmount);
                    notes += strategy.FeeNote();
                    paymentMethodFound = true;
                    break;
                }
            }

            if (!paymentMethodFound)
            {
                throw new ArgumentException("Unsupported payment method");
            }

            decimal taxRate = 0.20m;
            foreach (var strategy in _taxStrategies)
            {
                if (strategy.CheckCountry(customer.Country))
                {
                    taxRate = strategy.GetTaxRate();
                    break;
                }
            }

            decimal taxBase = subtotalAfterDiscount + supportFee + paymentFee;
            decimal taxAmount = taxBase * taxRate;
            decimal finalAmount = taxBase + taxAmount;

            if (finalAmount < 500m)
            {
                finalAmount = 500m;
                notes += "minimum invoice amount applied; ";
            }

            var invoice = new RenewalInvoice
            {
                InvoiceNumber = $"INV-{DateTime.UtcNow:yyyyMMdd}-{customerId}-{normalizedPlanCode}",
                CustomerName = customer.FullName,
                PlanCode = normalizedPlanCode,
                PaymentMethod = normalizedPaymentMethod,
                SeatCount = seatCount,
                BaseAmount = Math.Round(baseAmount, 2, MidpointRounding.AwayFromZero),
                DiscountAmount = Math.Round(discountAmount, 2, MidpointRounding.AwayFromZero),
                SupportFee = Math.Round(supportFee, 2, MidpointRounding.AwayFromZero),
                PaymentFee = Math.Round(paymentFee, 2, MidpointRounding.AwayFromZero),
                TaxAmount = Math.Round(taxAmount, 2, MidpointRounding.AwayFromZero),
                FinalAmount = Math.Round(finalAmount, 2, MidpointRounding.AwayFromZero),
                Notes = notes.Trim(),
                GeneratedAt = DateTime.UtcNow
            };

            _invoiceRepository.Save(invoice);

            if (!string.IsNullOrWhiteSpace(customer.Email))
            {
                string subject = "Subscription renewal invoice";
                string body =
                    $"Hello {customer.FullName}, your renewal for plan {normalizedPlanCode} " +
                    $"has been prepared. Final amount: {invoice.FinalAmount:F2}.";

                _emailSender.Send(customer.Email, subject, body);
            }

            return invoice;
        }
    }
}
