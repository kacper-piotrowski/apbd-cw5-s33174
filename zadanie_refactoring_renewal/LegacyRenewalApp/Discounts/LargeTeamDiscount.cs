namespace LegacyRenewalApp.Discounts;

public class LargeTeamDiscount : IDiscountStrategy
{
    public bool CheckDiscount(RenewalDiscountValues discountValues)
    {
        return discountValues.SeatCount >= 50;
    }

    public decimal CalculateDiscount(decimal discountAmount, decimal baseAmount)
    {
        return discountAmount + baseAmount * 0.12m;
    }

    public string DiscountNote()
    {
        return "large team discount; ";
    }
}