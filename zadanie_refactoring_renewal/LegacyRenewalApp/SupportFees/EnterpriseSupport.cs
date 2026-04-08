namespace LegacyRenewalApp.Supports;

public class EnterpriseSupport : ISupportFeeStrategy
{
    public bool CheckPlanCode(string planCode)
    {
        return planCode == "ENTERPRISE";
    }

    public decimal GetSupportFee()
    {
        return 700m;
    }
}