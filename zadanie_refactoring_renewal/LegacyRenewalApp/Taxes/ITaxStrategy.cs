namespace LegacyRenewalApp;

public interface ITaxStrategy
{
    bool CheckCountry(string country);

    decimal GetTaxRate();
}