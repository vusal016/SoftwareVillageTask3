namespace StreamVibe.Application.Features.Genres.Queries.GetOurGenres
{
    public sealed class GetOurGenresQueryHandler(IStreamDb streamDb, IMapper mapper, IFusionCache cache) : IRequestHandler<GetOurGenresQuery, List<OurGenresDto>>
    {
        public async Task<List<OurGenresDto>> Handle(GetOurGenresQuery request, CancellationToken cancellationToken)
        {
            return await cache.GetOrSetAsync(
                $"GetOurGenresQueryHandler_{request.Type}",
                async token => 
                {
                    var ourGenres = streamDb.Genres.AsNoTracking().AsQueryable();
                    if (request.Type is null) throw new ArgumentException("Type is required.");
                    ourGenres = request.Type switch
                    {
                        GenreType.Movie => ourGenres.Where(x => x.Type == GenreType.Movie),
                        GenreType.Show => ourGenres.Where(x => x.Type == GenreType.Show),
                        _ => throw new ArgumentException("Invalid type parameter.")
                    };
                    var ourGenresQuery=await ourGenres.ToListAsync(cancellationToken);
                    var ourGenresDto = mapper.Map<List<OurGenresDto>>(ourGenresQuery);
                    return ourGenresDto;
                },
                token:cancellationToken
          );
        }
    }
}