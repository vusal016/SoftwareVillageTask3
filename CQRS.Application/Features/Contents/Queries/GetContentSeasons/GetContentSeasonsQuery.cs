namespace StreamVibe.Application.Features.Contents.Queries.GetContentSeasons
{
    public record GetContentSeasonsQuery(string Id) : IRequest<List<SeasonDto>>;
}
