namespace StreamVibe.Application.Features.Login
{
    public sealed class LoginCommandHandler(IStreamDb streamDb, IMapper mapper, ITokenProvider tokenProvider, IPasswordHasher passwordHasher) : IRequestHandler<LoginCommand, AuthResponseDto>
    {
        public async Task<AuthResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(request.Password);
            var user = await streamDb.Users.FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken)
            ?? throw new UnauthorizedAccessException("Invalid email or password");

            var isPasswordValid = passwordHasher.Verify(request.Password, user.PasswordHash);
            if (!isPasswordValid)
                throw new UnauthorizedAccessException("Invalid email or password");
            if (!user.IsActive)
                throw new ForbiddenException("Account is deactivated");

            await streamDb.RefreshTokens.Where(t => t.UserId == user.Id).ExecuteDeleteAsync(cancellationToken);

            var accessToken = tokenProvider.Create(user);
            var refreshTokenOwn = tokenProvider.GenerateRefreshToken();

            var refreshToken = new RefreshToken(
               token: refreshTokenOwn,
               expiresAt: tokenProvider.RefreshTokenExpiresAt,
               createdAt: DateTime.UtcNow,
               userId: user.Id
            );
            await streamDb.RefreshTokens.AddAsync(refreshToken, cancellationToken);
            await streamDb.SaveChangesAsync(cancellationToken);
            var userDto = mapper.Map<UserDto>(user);

            return new AuthResponseDto(
              userDto,
              accessToken,
              refreshToken.Token,
              tokenProvider.AccessTokenExpirationInSeconds
            );
        }
    }
}