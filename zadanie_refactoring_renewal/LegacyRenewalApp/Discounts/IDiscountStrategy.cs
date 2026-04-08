namespace LegacyRenewalApp.Discounts;

public interface IDiscountStrategy
{
    bool CheckDiscount(RenewalDiscountValues discountValues);
    decimal CalculateDiscount(decimal discountAmount, decimal baseAmount);

    string DiscountNote();
}