namespace StreamVibe.Application.Features.Profile.Queries.GetMySubscriptions
{
    public record GetMySubscriptionsQuery(Guid UserId) : IRequest<UserSubscriptionDto>;
}
