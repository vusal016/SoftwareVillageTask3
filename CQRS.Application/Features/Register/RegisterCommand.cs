namespace StreamVibe.Application.Features.Register
{
    public record RegisterCommand(string UserName, string Email, string Password) : IRequest<AuthResponseDto>;
}