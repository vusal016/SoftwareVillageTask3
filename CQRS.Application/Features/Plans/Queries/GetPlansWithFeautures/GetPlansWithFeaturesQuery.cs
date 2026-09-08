namespace StreamVibe.Application.Features.Plans.Queries.GetPlansWithFeautures
{
    public record GetPlansWithFeaturesQuery(UserBillingCycle billing) : IRequest<List<PricingPlanWithFeaturesDto>>;
}
