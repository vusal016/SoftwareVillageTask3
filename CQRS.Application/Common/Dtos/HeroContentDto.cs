namespace StreamVibe.Application.Common.Dtos
{
    public record HeroContentDto(
       Guid Id,
       string Title,
       string Description,
       string BackGroundUrl,
       string TrailerUrl,
       ContentType Type
  );
}
