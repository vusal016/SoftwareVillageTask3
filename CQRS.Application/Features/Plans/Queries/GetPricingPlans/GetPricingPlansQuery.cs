namespace StreamVibe.Application.Features.Plans.Queries.GetPricingPlans
{
    public record GetPricingPlansQuery(string? billing) : IRequest<List<PricingPlanDto>>;
}
    