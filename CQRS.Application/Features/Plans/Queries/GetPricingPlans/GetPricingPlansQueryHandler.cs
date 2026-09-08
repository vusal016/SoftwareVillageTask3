namespace StreamVibe.Application.Features.Plans.Queries.GetPricingPlans
{
    public sealed class GetPricingPlansQueryHandler(IStreamDb streamDb, IFusionCache cache) : IRequestHandler<GetPricingPlansQuery, List<PricingPlanDto>>
    {
        public async Task<List<PricingPlanDto>> Handle(GetPricingPlansQuery request, CancellationToken cancellationToken)
        {
            return await cache.GetOrSetAsync(
               $"get_pricing_plans_{request.billing}",
               async token => {
                   var plans = streamDb.PricingPlans.AsNoTracking().AsQueryable();
                   var planDtos = await plans.Select(p => new PricingPlanDto(
                        p.Id,
                        p.Name,
                        p.Description,
                        request.billing == "yearly" ? p.PriceYearly : p.PriceMonthly,
                       p.IsPopular)).ToListAsync(token);
                   return planDtos;
               },
             token: cancellationToken
          );
        }
    }
}