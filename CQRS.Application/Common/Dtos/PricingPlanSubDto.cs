namespace StreamVibe.Application.Common.Dtos
{
    public record PricingPlanSubDto
        (
        Guid Id,
        string Name,
        string Description,
        decimal Price
    );
}
