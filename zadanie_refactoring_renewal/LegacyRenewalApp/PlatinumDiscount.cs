namespace LegacyRenewalApp;

public class PlatinumDiscount : IDiscountStrategy
{
    public decimal CalculateDiscount(decimal discountAmount, decimal baseAmount)
    {
        return discountAmount + baseAmount * 0.15m;
    }

    public string DiscountNote()
    {
        return "platinum discount; ";
    }
}