namespace LegacyRenewalApp.Payments;

public class PaypalPayment : IPaymentFeeStrategy
{
    public bool CheckPaymentMethod(string paymentMethod)
    {
        return  paymentMethod == "PAYPAL";
    }

    public decimal CalculateFee(decimal amount)
    {
        return amount * 0.035m;
    }

    public string FeeNote()
    {
        return "paypal fee; ";
    }
}