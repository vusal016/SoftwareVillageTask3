namespace StreamVibe.Application.Features.Contents.Queries.GetAllContents
{
    public record GetAllContentsQuery(ContentType?Type, string? Filter, int Limit = 10):IRequest<List<ContentDto>>;
}