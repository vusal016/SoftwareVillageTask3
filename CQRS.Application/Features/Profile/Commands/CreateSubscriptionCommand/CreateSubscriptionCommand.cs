namespace StreamVibe.Application.Features.Profile.Commands.CreateSubscriptionCommand
{
    public record CreateSubscriptionCommand(Guid UserId,Guid PlanId,UserBillingCycle BillingCycle,bool IsTrial) : IRequest<CreateSubDto>;
}
