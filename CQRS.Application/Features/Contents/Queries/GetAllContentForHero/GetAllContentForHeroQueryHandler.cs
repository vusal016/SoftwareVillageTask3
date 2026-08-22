namespace StreamVibe.Application.Features.Contents.Queries.GetAllContentForHero
{
    public sealed class GetAllContentForHeroQueryHandler(IStreamDb streamDb, IMapper mapper, IFusionCache cache) : IRequestHandler<GetAllContentForHeroQuery, List<HeroContentDto>>
    {
        public async Task<List<HeroContentDto>> Handle(GetAllContentForHeroQuery request, CancellationToken cancellationToken)
        {
            return await cache.GetOrSetAsync(
                "GetAllContentForHeroQuery",
               async token => 
               {
                   var contents = await streamDb.Contents
                   .Where(c => c.IsFeatured).ToListAsync(cancellationToken);
                   var contentDtos = mapper.Map<List<HeroContentDto>>(contents);
                   return contentDtos;
               },
               token: cancellationToken
          );
        }
    }
}
