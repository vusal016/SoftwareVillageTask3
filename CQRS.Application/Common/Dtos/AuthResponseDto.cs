namespace StreamVibe.Application.Common.Dtos
{
    public record AuthResponseDto
    (
      UserDto User,
      string Token,
      string RefreshToken,
      int ExpiresIn
    );
}
