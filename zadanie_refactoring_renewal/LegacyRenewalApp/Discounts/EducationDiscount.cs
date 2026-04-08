using LegacyRenewalApp.Models;

namespace LegacyRenewalApp.Discounts;

public class EducationDiscount : IDiscountStrategy
{
    public bool CheckDiscount(RenewalDiscountValues discountValues)
    {
        return discountValues.Customer.Segment == "Education" && discountValues.Plan.IsEducationEligible;
    }

    public decimal CalculateDiscount(decimal discountAmount, decimal baseAmount)
    {
        return discountAmount + baseAmount * 0.20m;
    }

    public string DiscountNote()
    {
        return "education discount; ";
    }
}