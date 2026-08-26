namespace StreamVibe.Application.Common.Dtos
{
    public record ContentDetailDto(
        Guid Id,
        string Title,
        string Description,
        string PosterUrl,
        string BackGroundUrl, 
        string TrailerUrl,
        ContentType Type,
        int ReleaseYear,
        decimal ImdbRating,
        decimal StreamVibeRating,
        List<GenreContentDetailDto> Genres,
        List<string> Languages,
        List<CastContentDetailDto> Casts,
        List <PersonDto> Directors,
        List<PersonDto> Musics
    );
}
