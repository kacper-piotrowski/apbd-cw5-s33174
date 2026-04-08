using LegacyRenewalApp.Models;

namespace LegacyRenewalApp.Discounts;

public class MediumTeamDiscount : IDiscountStrategy
{
    public bool CheckDiscount(RenewalDiscountValues discountValues)
    {
        return discountValues.SeatCount >= 20;
    }
    public decimal CalculateDiscount(decimal discountAmount, decimal baseAmount)
    {
        return discountAmount + baseAmount * 0.08m;
    }

    public string DiscountNote()
    {
        return "medium team discount; ";
    }
}