namespace StreamVibe.Application.Features.Plans.Queries.GetPlansWithFeautures
{
    public sealed class GetPlansWithFeaturesQueryHandler(IStreamDb streamDb, IFusionCache cache) : IRequestHandler<GetPlansWithFeaturesQuery, List<PricingPlanWithFeaturesDto>>
    {
        public async Task<List<PricingPlanWithFeaturesDto>> Handle(GetPlansWithFeaturesQuery request, CancellationToken cancellationToken)
        {
            return await cache.GetOrSetAsync(
                $"plans_with_features_{request.billing}",
                async token =>
                {
                    var plansWithFeatures = streamDb.PricingPlans.AsNoTracking()
                    .Include(p => p.PlanFeatures);
                    var planFeturesDto = await plansWithFeatures.Select(p => new PricingPlanWithFeaturesDto(
                       p.Id,
                       p.Name,
                       p.Description,
                       request.billing == UserBillingCycle.Yearly ? p.PriceYearly : p.PriceMonthly,
                       p.IsPopular,
                       request.billing,
                       p.PlanFeatures.Select(f => new PlanFeatureDto(
                       f.FeatureName,
                       f.FeatureValue,
                       f.OrderNumber
                       )).ToList()
                  )).ToListAsync(token);
                    return planFeturesDto;
                },
                token: cancellationToken
          );
        }
    }
}
