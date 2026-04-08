namespace LegacyRenewalApp.Discounts;

public interface IDiscountStrategy
{
    decimal CalculateDiscount(decimal discountAmount, decimal baseAmount);

    string DiscountNote();
}