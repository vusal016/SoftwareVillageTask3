namespace StreamVibe.Application.Common.Dtos
{
    public record SeasonDto
        (
           Guid Id,
           string SeasonNumber,
           int EpisodeCount,
           string Title,
           List<EpisodeDto> Episodes
        );
}
