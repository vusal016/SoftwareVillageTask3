namespace StreamVibe.Application.Common.Dtos
{
    public record EpisodeDto
        (
            Guid Id,
            int EpisodeNumber,
            string Title,
            string Description,
            string ThumbnailUrl,
            int DurationMinutes
        );
}
