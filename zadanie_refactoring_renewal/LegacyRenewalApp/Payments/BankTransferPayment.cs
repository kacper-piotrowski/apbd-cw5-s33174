namespace LegacyRenewalApp.Payments;

public class BankTransferPayment : IPaymentFeeStrategy
{
    public bool CheckPaymentMethod(string paymentMethod)
    {
        return  paymentMethod == "BANK_TRANSFER";
    }

    public decimal CalculateFee(decimal amount)
    {
        return amount * 0.01m;
    }

    public string FeeNote()
    {
        return "bank transfer fee; ";
    }
}