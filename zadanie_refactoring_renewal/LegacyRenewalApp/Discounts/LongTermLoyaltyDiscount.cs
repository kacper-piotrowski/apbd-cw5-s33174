namespace LegacyRenewalApp.Discounts;

public class LongTermLoyaltyDiscount : IDiscountStrategy
{
    public decimal CalculateDiscount(decimal discountAmount, decimal baseAmount)
    {
        return discountAmount + baseAmount * 0.07m;
    }

    public string DiscountNote()
    {
        return "long-term loyalty discount; ";
    }
}