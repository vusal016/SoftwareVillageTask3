namespace StreamVibe.Application.Common.Dtos
{
    public record FaqDto(
        Guid Id,
        string Question,
        string Answer,
        int OrderNumber
        );
}
