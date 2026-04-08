namespace LegacyRenewalApp.Discounts;

public class BasicLoyaltyDiscount : IDiscountStrategy
{
    public bool CheckDiscount(RenewalDiscountValues discountValues)
    {
        return discountValues.Customer.YearsWithCompany >= 2;
    }

    public decimal CalculateDiscount(decimal discountAmount, decimal baseAmount)
    {
        return discountAmount + baseAmount * 0.03m;
    }

    public string DiscountNote()
    {
        return "basic loyalty discount; ";
    }
}