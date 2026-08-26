namespace StreamVibe.Application.Features.Contents.Queries.GetAllContentReviews
{
    public record GetAllContentReviewsQuery(string Id): IRequest<List<ContentReviewDto>>;
}
