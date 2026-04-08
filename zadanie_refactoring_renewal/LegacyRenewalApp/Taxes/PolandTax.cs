namespace LegacyRenewalApp;

public class PolandTax : ITaxStrategy
{
    public bool CheckCountry(string country)
    {
        return country == "Poland";
    }

    public decimal GetTaxRate()
    {
        return 0.23m;
    }
}