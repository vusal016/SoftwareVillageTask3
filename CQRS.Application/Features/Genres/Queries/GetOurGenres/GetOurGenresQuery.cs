namespace StreamVibe.Application.Features.Genres.Queries.GetOurGenres
{
    public record GetOurGenresQuery(GenreType? Type) : IRequest<List<OurGenresDto>>;
}