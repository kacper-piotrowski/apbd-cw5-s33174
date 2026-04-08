namespace LegacyRenewalApp;

public class CzechRepublicTax : ITaxStrategy
{
    public bool CheckCountry(string country)
    {
        return country == "Czech Republic";
    }

    public decimal GetTaxRate()
    {
        return 0.21m;
    }
}