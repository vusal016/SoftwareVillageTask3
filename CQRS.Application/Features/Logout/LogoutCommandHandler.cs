namespace StreamVibe.Application.Features.Logout
{
    public sealed class LogoutCommandHandler(IStreamDb streamDb) : IRequestHandler<LogoutCommand, bool>
    {
        public async Task<bool> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            await streamDb.RefreshTokens.Where(rt => rt.Token == request.RefreshToken).ExecuteDeleteAsync(cancellationToken);
            return true;
        }
    }
}