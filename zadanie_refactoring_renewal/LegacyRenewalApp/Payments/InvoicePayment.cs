namespace LegacyRenewalApp.Payments;

public class InvoicePayment : IPaymentFeeStrategy
{
    public bool CheckPaymentMethod(string paymentMethod)
    {
        return  paymentMethod == "INVOICE";
    }

    public decimal CalculateFee(decimal amount)
    {
        return amount * 0m;
    }

    public string FeeNote()
    {
        return "invoice payment; ";
    }
}