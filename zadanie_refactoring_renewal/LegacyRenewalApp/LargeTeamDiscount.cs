namespace LegacyRenewalApp;

public class LargeTeamDiscount : IDiscountStrategy
{
    public decimal CalculateDiscount(decimal discountAmount, decimal baseAmount)
    {
        return discountAmount + baseAmount * 0.12m;
    }

    public string DiscountNote()
    {
        return "large team discount; ";
    }
}