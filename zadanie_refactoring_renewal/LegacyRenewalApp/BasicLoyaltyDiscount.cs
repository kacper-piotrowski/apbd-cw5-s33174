namespace LegacyRenewalApp;

public class BasicLoyaltyDiscount : IDiscountStrategy
{
    public decimal CalculateDiscount(decimal discountAmount, decimal baseAmount)
    {
        return discountAmount + baseAmount * 0.03m;
    }

    public string DiscountNote()
    {
        return "basic loyalty discount; ";
    }
}