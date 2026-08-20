namespace StreamVibe.Application.Common.Dtos
{
    public record PricingPlanDto(
        Guid Id,
        string Name,
        string Description,
        decimal Price,
        bool IsPopular
        );
}
