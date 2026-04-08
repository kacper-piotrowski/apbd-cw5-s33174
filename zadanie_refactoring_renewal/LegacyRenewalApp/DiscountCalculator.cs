using LegacyRenewalApp.Discounts;

namespace LegacyRenewalApp;

public class DiscountCalculator
{
    private readonly IDiscountStrategy _strategy;

    public DiscountCalculator(IDiscountStrategy strategy)
    {
        _strategy = strategy;
    }

    public decimal CalculateDiscount(decimal baseAmount, decimal discountAmount)
    {
        return _strategy.CalculateDiscount(baseAmount, discountAmount);
    }

    public string DiscountNote()
    {
        return _strategy.DiscountNote();
    }
}