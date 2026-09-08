namespace StreamVibe.Application.Common.Dtos
{
    public record PricingPlanWithFeaturesDto(
        Guid Id,
        string Name,
        string Description,
        decimal Price,
        bool IsPopular,
        UserBillingCycle BillingCycle,
        List<PlanFeatureDto> Features
   );
}
