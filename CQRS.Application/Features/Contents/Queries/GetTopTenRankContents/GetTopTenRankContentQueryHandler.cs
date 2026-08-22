namespace StreamVibe.Application.Features.Contents.Queries.GetTopTenRankContents
{
    public sealed class GetTopTenRankContentQueryHandler(IStreamDb streamDb, IMapper mapper, IFusionCache cache) : IRequestHandler<GetTopTenRankContentQuery, List<TopTenContentDto>>
    {
        public async Task<List<TopTenContentDto>> Handle(GetTopTenRankContentQuery request, CancellationToken cancellationToken)
        {
            return await cache.GetOrSetAsync(
                $"GetTopTenRankContentQuery_{request.Type}",
                async token => {
                    var topTenRank = streamDb.Contents.AsNoTracking().AsQueryable();
                    topTenRank= request.Type switch
                    {
                        ContentType.Movie => topTenRank.Where(x => x.Type == ContentType.Movie),
                        ContentType.Show => topTenRank.Where(x => x.Type == ContentType.Show),
                        _ => throw new ArgumentException("Invalid type parameter.")
                    };
                    var topTenRankQuery = await topTenRank
                     .Where(x => x.TopTenRank != null)
                     .OrderBy(x => x.TopTenRank)
                     .Include(x => x.ContentGenres)
                     .ThenInclude(x => x.Genre).ToListAsync(cancellationToken);
                   
                    var topTenRankDto = mapper.Map<List<TopTenContentDto>>(topTenRankQuery);
                    return topTenRankDto;
                },
                token: cancellationToken
          );
        }
    }
}
