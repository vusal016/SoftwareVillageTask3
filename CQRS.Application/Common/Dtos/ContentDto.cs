namespace StreamVibe.Application.Common.Dtos
{
    public record ContentDto(
        Guid Id,
        string Title,
        string PosterUrl,
        decimal ImdbRating,
        decimal StreamVibeRating,
        int ReleaseYear,
        ContentType Type
        );
}
