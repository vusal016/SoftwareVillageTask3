namespace StreamVibe.Application.Common.Dtos
{
    public record CreateSubDto
        (
          string Message,
          Guid Id,
          Guid PlanId,
          UserBillingCycle BillingCycle,
          bool IsTrial,
          UserSubStatus Status,
          DateTime StartedAt,
          DateTime ExpiresAt
        );
}
