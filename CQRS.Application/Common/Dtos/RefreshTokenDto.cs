namespace StreamVibe.Application.Common.Dtos
{
    public record class RefreshTokenDto(string Token, string RefreshToken, int ExpiresIn);
}
