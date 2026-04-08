namespace LegacyRenewalApp.Supports;

public class StartSupport : ISupportFeeStrategy
{
    public bool CheckPlanCode(string planCode)
    {
        return planCode == "START";
    }

    public decimal GetSupportFee()
    {
        return 250m;
    }
}