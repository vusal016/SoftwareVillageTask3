namespace StreamVibe.Application.Common.Dtos
{
    public record ContentReviewDto
        (
            Guid Id,
            string ReviewerName,
            string ReviewerLocation,
            decimal Rating,
            string ReviewText,
            DateTime CreatedAt
        );
}
