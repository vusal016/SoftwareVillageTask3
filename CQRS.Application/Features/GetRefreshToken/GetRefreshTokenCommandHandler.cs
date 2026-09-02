namespace StreamVibe.Application.Features.GetRefreshToken
{
    public class GetRefreshTokenCommandHandler(IStreamDb streamDb, ITokenProvider tokenProvider) : IRequestHandler<GetRefreshTokenCommand, RefreshTokenDto>
    {
        public async Task<RefreshTokenDto> Handle(GetRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(request.RefreshToken);
            var refToken = await streamDb.RefreshTokens.FirstOrDefaultAsync(x => x.Token == request.RefreshToken, cancellationToken)
             ?? throw new UnauthorizedAccessException("Invalid Refresh Token");
            if (refToken.ExpiresAt < DateTime.UtcNow)
            {
                await streamDb.RefreshTokens.Where(x => x.Token ==refToken.Token).ExecuteDeleteAsync(cancellationToken);
                throw new UnauthorizedAccessException("Refresh Token Expired");
            }
            var user = await streamDb.Users.FirstOrDefaultAsync(x => x.Id == refToken.UserId, cancellationToken);
            var newToken = tokenProvider.Create(user);
            await streamDb.RefreshTokens.Where(x => x.Token == refToken.Token).ExecuteDeleteAsync(cancellationToken);
            var newRefreshToken = tokenProvider.GenerateRefreshToken();

            var refreshToken = new RefreshToken(
               token: newRefreshToken,
               expiresAt: tokenProvider.RefreshTokenExpiresAt,
               createdAt: DateTime.UtcNow,
               userId: user.Id
            );
            await streamDb.RefreshTokens.AddAsync(refreshToken, cancellationToken);
            await streamDb.SaveChangesAsync(cancellationToken);

            return new RefreshTokenDto(
                Token: newToken,
                RefreshToken: newRefreshToken,
                ExpiresIn: tokenProvider.AccessTokenExpirationInSeconds
            );
        }
    }
}