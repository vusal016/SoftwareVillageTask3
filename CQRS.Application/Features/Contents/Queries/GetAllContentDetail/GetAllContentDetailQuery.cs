namespace StreamVibe.Application.Features.Contents.Queries.GetAllContentDetail
{
    public record GetAllContentDetailQuery(string Id) : IRequest<ContentDetailDto>;
}
