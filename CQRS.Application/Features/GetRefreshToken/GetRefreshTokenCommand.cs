namespace StreamVibe.Application.Features.GetRefreshToken
{
    public record GetRefreshTokenCommand(string RefreshToken) : IRequest<RefreshTokenDto>;
}
