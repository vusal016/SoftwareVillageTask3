namespace StreamVibe.Application.Common.Dtos
{
    public record PricingPlanProfileDto
    (
        Guid Id,
        string Name,
        decimal Price
    );
}
