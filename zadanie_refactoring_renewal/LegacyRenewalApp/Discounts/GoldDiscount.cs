namespace LegacyRenewalApp.Discounts;

public class GoldDiscount : IDiscountStrategy
{
    public decimal CalculateDiscount(decimal discountAmount, decimal baseAmount)
    {
        return discountAmount +  baseAmount * 0.10m;
    }

    public string DiscountNote()
    {
        return "gold discount; ";
    }
}