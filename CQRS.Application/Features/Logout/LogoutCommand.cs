namespace StreamVibe.Application.Features.Logout
{
    public record LogoutCommand(string RefreshToken) : IRequest<bool>;
}
