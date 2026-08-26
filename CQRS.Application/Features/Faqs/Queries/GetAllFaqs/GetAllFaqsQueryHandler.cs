namespace StreamVibe.Application.Features.Faqs.Queries.GetAllFaqs
{
    public sealed class GetAllFaqsQueryHandler(IStreamDb streamDb, IMapper mapper, IFusionCache cache) : IRequestHandler<GetAllFaqsQuery, List<FaqDto>>
    {
        public async Task<List<FaqDto>> Handle(GetAllFaqsQuery request, CancellationToken cancellationToken)
        {
            return await cache.GetOrSetAsync(
                "get_all_faqs",
                async token => {
                var faqs = await streamDb.Faqs
                    .AsNoTracking()
                    .OrderBy(f => f.OrderNumber)
                    .ToListAsync(token);
                    var faqDtos = mapper.Map<List<FaqDto>>(faqs);
                    return faqDtos;
                },
                token:cancellationToken
          );
        }
    }
}
