namespace StreamVibe.Application.Common.Dtos
{
    public record ProfileSubDto
    (
         Guid Id,
         UserBillingCycle BillingCycle,
         bool IsTrial,
         UserSubStatus Status,
         DateTime StartedAt,
         DateTime ExpiresAt,
         PricingPlanProfileDto Plan
    );
}
