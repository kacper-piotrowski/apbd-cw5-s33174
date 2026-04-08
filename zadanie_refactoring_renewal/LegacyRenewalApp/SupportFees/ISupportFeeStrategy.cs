namespace LegacyRenewalApp.Supports;

public interface ISupportFeeStrategy
{
    bool CheckPlanCode(string planCode);

    decimal GetSupportFee();
}