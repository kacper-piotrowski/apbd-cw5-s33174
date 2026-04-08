namespace LegacyRenewalApp;

public interface IDiscountStrategy
{
    decimal CalculateDiscount(decimal discountAmount, decimal baseAmount);

    string DiscountNote();
}