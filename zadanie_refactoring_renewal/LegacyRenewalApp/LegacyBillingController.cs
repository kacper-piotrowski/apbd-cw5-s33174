using LegacyRenewalApp.Discounts;

namespace LegacyRenewalApp;

public class LegacyBillingController : IEmailSender, IInvoiceRepository
{
    public void Save(RenewalInvoice invoice)
    {
        LegacyBillingGateway.SaveInvoice(invoice);
    }

    public void Send(string to, string subject, string body)
    {
        LegacyBillingGateway.SendEmail(to, subject, body);
    }
}