namespace StreamVibe.Application.Features.Contents.Queries.GetTopTenRankContents
{
    public record GetTopTenRankContentQuery(ContentType? Type) : IRequest<List<TopTenContentDto>>;
}
