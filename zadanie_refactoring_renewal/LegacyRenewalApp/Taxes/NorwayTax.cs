namespace LegacyRenewalApp;

public class NorwayTax : ITaxStrategy
{
    public bool CheckCountry(string country)
    {
        return country == "Norway";
    }

    public decimal GetTaxRate()
    {
        return 0.25m;
    }
}