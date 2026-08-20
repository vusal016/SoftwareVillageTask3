namespace StreamVibe.Application.Features.Genres.Queries.GetAllGenres
{
    public record GetAllGenresQuery(GenreType? Type=null) : IRequest<List<GenreDto>>;
}
