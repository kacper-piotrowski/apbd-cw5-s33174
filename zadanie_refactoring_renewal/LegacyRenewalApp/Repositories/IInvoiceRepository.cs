using LegacyRenewalApp.Models;

namespace LegacyRenewalApp.Repositories;

public interface IInvoiceRepository
{
    void Save(RenewalInvoice invoice);
}