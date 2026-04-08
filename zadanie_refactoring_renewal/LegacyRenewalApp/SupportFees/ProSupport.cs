namespace LegacyRenewalApp.Supports;

public class ProSupport : ISupportFeeStrategy
{
    public bool CheckPlanCode(string planCode)
    {
        return planCode == "PRO";
    }

    public decimal GetSupportFee()
    {
        return 400m;
    }
}