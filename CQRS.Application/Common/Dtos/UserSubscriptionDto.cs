namespace StreamVibe.Application.Common.Dtos
{
    public record UserSubscriptionDto
        (
            Guid Id,
            UserBillingCycle BillingCycle,
            bool IsTrial,
            UserSubStatus Status,
            DateTime StartedAt,
            DateTime ExpiresAt,
            PricingPlanSubDto Plan
        );
}
