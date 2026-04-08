namespace LegacyRenewalApp.Discounts;

public class LongTermLoyaltyDiscount : IDiscountStrategy
{
    public bool CheckDiscount(RenewalDiscountValues discountValues)
    {
        return discountValues.Customer.YearsWithCompany >= 5;
    }

    public decimal CalculateDiscount(decimal discountAmount, decimal baseAmount)
    {
        return discountAmount + baseAmount * 0.07m;
    }

    public string DiscountNote()
    {
        return "long-term loyalty discount; ";
    }
}