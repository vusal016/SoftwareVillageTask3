namespace StreamVibe.Application.Common.Dtos
{
    public record UserDto
    (
        Guid Id,
        string UserName,
        string Email,
        DateTime CreatedAt
    );
}
