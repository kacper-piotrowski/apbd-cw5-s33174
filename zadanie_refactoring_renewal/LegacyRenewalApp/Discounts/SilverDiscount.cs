namespace LegacyRenewalApp.Discounts;

public class SilverDiscount : IDiscountStrategy
{
    public decimal CalculateDiscount(decimal discountAmount, decimal baseAmount)
    {
        return discountAmount + baseAmount * 0.2m;
    }

    public string DiscountNote()
    {
        return "silver discount; ";
    }
}