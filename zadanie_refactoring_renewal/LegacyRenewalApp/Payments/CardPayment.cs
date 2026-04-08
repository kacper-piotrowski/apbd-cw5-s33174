namespace LegacyRenewalApp.Payments;

public class CardPayment : IPaymentFeeStrategy
{
    public bool CheckPaymentMethod(string paymentMethod)
    {
        return  paymentMethod == "CARD";
    }

    public decimal CalculateFee(decimal amount)
    {
        return amount * 0.02m;
    }

    public string FeeNote()
    {
        return "card payment fee; ";
    }
}