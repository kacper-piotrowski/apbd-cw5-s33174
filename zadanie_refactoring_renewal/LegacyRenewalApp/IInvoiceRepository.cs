namespace LegacyRenewalApp;

public interface IInvoiceRepository
{
    void Save(RenewalInvoice invoice);
}