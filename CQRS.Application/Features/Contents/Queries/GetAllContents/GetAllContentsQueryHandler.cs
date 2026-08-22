namespace StreamVibe.Application.Features.Contents.Queries.GetAllContents
{
    public sealed class GetAllContentsQueryHandler(IStreamDb streamDb, IMapper mapper, IFusionCache cache) : IRequestHandler<GetAllContentsQuery, List<ContentDto>>
    {
        public async Task<List<ContentDto>> Handle(GetAllContentsQuery request, CancellationToken cancellationToken)
        {
            return await cache.GetOrSetAsync(
                $"get-all-contents-{request.Type}-{request.Filter}-{request.Limit}",
                async token =>
                {
                    var contents = streamDb.Contents.AsNoTracking().AsQueryable();
                    if (string.IsNullOrWhiteSpace(request.Filter) || !request.Type.HasValue) throw new ArgumentException("Parameter is required.");
                    contents = contents.Where(c => c.Type == request.Type.Value);

                    contents = request.Filter switch
                    {
                        "trending" => contents.Where(c => c.IsTrending),
                        "new-release" => contents.Where(c => c.IsNewRelease),
                        "must-watch" => contents.Where(c => c.IsMustWatch),
                        _ => throw new ArgumentException("Invalid filter parameter.")
                    };
                    var contentQuery = await contents.Take(request.Limit).ToListAsync(cancellationToken);
                    var contentDtos = mapper.Map<List<ContentDto>>(contentQuery);
                    return contentDtos;
                },
                token: cancellationToken
          );
        }
    }
}
