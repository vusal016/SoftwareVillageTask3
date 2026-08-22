namespace StreamVibe.Application.Common.Dtos
{
    public record TopTenContentDto(
        Guid Id,
        string Title,
        string PosterUrl,
        int TopTenRank,
        List<TopTenGenreDto> Genres
        );
}
