namespace StreamVibe.Application.Features.Contents.Queries.GetAllContentReviews
{
    public sealed class GetAllContentReviewsQueryHandler(IStreamDb streamDb, IMapper mapper, IFusionCache cache) : IRequestHandler<GetAllContentReviewsQuery, List<ContentReviewDto>>
    {
        public async Task<List<ContentReviewDto>> Handle(GetAllContentReviewsQuery request, CancellationToken cancellationToken)
        {
            return await cache.GetOrSetAsync(
               $"get_all_content_reviews_{request.Id}",
               async token =>
               {
                   if (!Guid.TryParse(request.Id, out var contentId)) throw new ArgumentException("Invalid content ID format.");
                   var contentExists = await streamDb.Contents
                      .AsNoTracking()
                      .AnyAsync(x => x.Id == contentId, token);
                   if (!contentExists) throw new KeyNotFoundException("Content not found");
                   var reviews = await streamDb.Reviews
                   .AsNoTracking()
                   .Where(r => r.ContentId == contentId)
                   .OrderByDescending(r => r.CreatedAt)
                   .ToListAsync(cancellationToken);
                   if (reviews is null) throw new KeyNotFoundException("Reviews not found.");

                   var reviewDtos = mapper.Map<List<ContentReviewDto>>(reviews);
                   return reviewDtos;
               },
               token: cancellationToken
           );
        }
    }
}
