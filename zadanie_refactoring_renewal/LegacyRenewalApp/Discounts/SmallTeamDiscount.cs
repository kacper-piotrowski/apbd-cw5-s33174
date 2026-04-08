namespace LegacyRenewalApp.Discounts;

public class SmallTeamDiscount : IDiscountStrategy
{
    public decimal CalculateDiscount(decimal discountAmount, decimal baseAmount)
    {
        return discountAmount + baseAmount * 0.04m;
    }

    public string DiscountNote()
    {
        return "small team discount; ";
    }
}