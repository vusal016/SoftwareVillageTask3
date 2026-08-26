namespace StreamVibe.Application.Features.Contents.Queries.GetContentSeasons
{
    public sealed class GetContentSeasonsQueryHandler(IStreamDb streamDb, IMapper mapper, IFusionCache cache) : IRequestHandler<GetContentSeasonsQuery, List<SeasonDto>>
    {
        public async Task<List<SeasonDto>> Handle(GetContentSeasonsQuery request, CancellationToken cancellationToken)
        {
            return await cache.GetOrSetAsync(
                $"get_content_seasons_{request.Id}",
                async token =>
                {
                    if (!Guid.TryParse(request.Id, out var guidId)) throw new ArgumentException("Invalid Id format");
                    var contentExists = await streamDb.Contents
                      .AsNoTracking()
                      .AnyAsync(x => x.Id == guidId, token);
                    if (!contentExists) throw new KeyNotFoundException("Content not found");
                    var seasons = await streamDb.Seasons
                      .AsNoTracking()
                      .Where(x => x.ContentId == guidId)
                      .OrderBy(x => x.SeasonNumber)
                      .Include(x => x.Episodes.OrderBy(e => e.EpisodeNumber))
                      .ToListAsync(cancellationToken);
                    var seasonDtos = mapper.Map<List<SeasonDto>>(seasons);
                    return seasonDtos;
                },
                token: cancellationToken
            );
        }
    }
}
