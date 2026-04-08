namespace LegacyRenewalApp;

public record RenewalDiscountValues(
    Customer Customer,
    SubscriptionPlan Plan,
    int SeatCount,
    decimal BaseAmount);