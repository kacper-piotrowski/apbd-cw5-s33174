namespace LegacyRenewalApp.Discounts;

public class PlatinumDiscount : IDiscountStrategy
{
    public bool CheckDiscount(RenewalDiscountValues discountValues)
    {
        return discountValues.Customer.Segment == "Platinum";
    }
    public decimal CalculateDiscount(decimal discountAmount, decimal baseAmount)
    {
        return discountAmount + baseAmount * 0.15m;
    }

    public string DiscountNote()
    {
        return "platinum discount; ";
    }
}