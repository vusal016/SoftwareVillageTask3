namespace StreamVibe.Application.Features.Contents.Queries.GetAllContentDetail
{
    public sealed class GetAllContentDetailQueryHandler(IStreamDb streamDb, IMapper mapper, IFusionCache cache) : IRequestHandler<GetAllContentDetailQuery, ContentDetailDto>
    {
        public async Task<ContentDetailDto> Handle(GetAllContentDetailQuery request, CancellationToken cancellationToken)
        {
            return await cache.GetOrSetAsync(
                $"get_all_content_detail_{request.Id}",
                async token =>
                {
                    if (!Guid.TryParse(request.Id, out var guidId)) throw new ArgumentException("Invalid Id format");
                    var contentDetail = await streamDb.Contents
                    .AsNoTracking()
                    .Include(x => x.ContentGenres)
                    .ThenInclude(x => x.Genre)
                    .Include(x => x.ContentLanguages)
                    .Include(x => x.ContentPeople)
                    .ThenInclude(x => x.People).FirstOrDefaultAsync(x => x.Id == guidId, cancellationToken);

                    if(contentDetail is null)throw new KeyNotFoundException("Content not found");

                   var contentDetailDto = mapper.Map<ContentDetailDto>(contentDetail);
                    return contentDetailDto;
                },
                token: cancellationToken
           );
        }
    }
}
