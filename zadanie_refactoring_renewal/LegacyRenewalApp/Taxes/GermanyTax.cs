namespace LegacyRenewalApp;

public class GermanyTax : ITaxStrategy
{
    public bool CheckCountry(string country)
    {
        return country == "Germany";
    }

    public decimal GetTaxRate()
    {
        return 0.19m;
    }
}