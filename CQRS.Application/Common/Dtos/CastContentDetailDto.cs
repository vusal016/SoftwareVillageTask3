namespace StreamVibe.Application.Common.Dtos
{
    public record CastContentDetailDto(
        Guid Id,
        string Name,
        string AvatarUrl,
        string Nationality,
        string CharacterName
    );
}
