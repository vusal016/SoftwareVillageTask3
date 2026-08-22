namespace StreamVibe.Application.Features.Contents.Queries.GetAllContentForHero
{
    public record GetAllContentForHeroQuery() : IRequest<List<HeroContentDto>>;
}
