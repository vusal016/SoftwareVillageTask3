namespace StreamVibe.Application.Features.Genres.Queries.GetAllGenres
{
    public sealed class GetAllGenresQueryHandler(IStreamDb streamDb, IMapper mapper, IFusionCache cache) : IRequestHandler<GetAllGenresQuery, List<GenreDto>>
    {
        public async Task<List<GenreDto>> Handle(GetAllGenresQuery request, CancellationToken cancellationToken)
        {
            return await cache.GetOrSetAsync(
                 $"get_all_genres_{request.Type}",
                async token =>
                {
                    var genres = streamDb.Genres.AsNoTracking().AsQueryable();
                    if (request.Type.HasValue && !Enum.IsDefined(request.Type.Value)) throw new ArgumentException($"Invalid genre type: {request.Type.Value}");
                    if (request.Type == GenreType.Movie)
                        genres = genres.Where(g => g.Type == GenreType.Movie);
                    else if (request.Type == GenreType.Show)
                        genres = genres.Where(g => g.Type == GenreType.Show);
                    var genresQuery = await genres.ToListAsync(cancellationToken);
                    var genresDto = mapper.Map<List<GenreDto>>(genresQuery);
                    return genresDto;
                },
                    token: cancellationToken
           );
        }
    }
}
