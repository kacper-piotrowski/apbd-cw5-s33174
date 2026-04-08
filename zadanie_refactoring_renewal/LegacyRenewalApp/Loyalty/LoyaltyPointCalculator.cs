namespace LegacyRenewalApp.Loyalty;

public class LoyaltyPointCalculator
{
    private int Limit = 200;

    public int CalculatePointsToUse(int customerPoints)
    {
        if (customerPoints < 0)
        {
            return 0;
        }
        return customerPoints>Limit?Limit:customerPoints;
    }
}