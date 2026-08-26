namespace StreamVibe.Application.Common.Dtos
{
    public record PersonDto(
        Guid Id,
        string Name,
        string AvatarUrl,
        string Nationality
  );
}
