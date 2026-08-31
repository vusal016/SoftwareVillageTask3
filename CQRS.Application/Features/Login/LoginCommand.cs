namespace StreamVibe.Application.Features.Login
{
    public record LoginCommand(string Email, string Password) : IRequest<AuthResponseDto>;
}
