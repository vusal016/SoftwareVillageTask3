namespace StreamVibe.Application.Common.Interfaces
{
    public interface ITokenProvider
    {
        string Create(User user);
        string GenerateRefreshToken();
        DateTime RefreshTokenExpiresAt { get; }
        int AccessTokenExpirationInSeconds { get; }
    }
}
