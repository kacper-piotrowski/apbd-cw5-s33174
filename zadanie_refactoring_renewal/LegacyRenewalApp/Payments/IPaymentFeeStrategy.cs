namespace LegacyRenewalApp.Payments;

public interface IPaymentFeeStrategy
{
    bool CheckPaymentMethod(string paymentMethod);

    decimal CalculateFee(decimal amount);

    string FeeNote();
}