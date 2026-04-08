namespace LegacyRenewalApp;

public class EducationDiscount : IDiscountStrategy
{
    public decimal CalculateDiscount(decimal discountAmount, decimal baseAmount)
    {
        return discountAmount + baseAmount * 0.20m;
    }

    public string DiscountNote()
    {
        return "education discount; ";
    }
}