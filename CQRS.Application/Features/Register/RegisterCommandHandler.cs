namespace StreamVibe.Application.Features.Register
{
    public sealed class RegisterCommandHandler(IStreamDb streamDb, IMapper mapper, ITokenProvider tokenProvider, IPasswordHasher passwordHasher) : IRequestHandler<RegisterCommand, AuthResponseDto>
    {
        public async Task<AuthResponseDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var emailExist = await streamDb.Users.AnyAsync(u => u.Email == request.Email, cancellationToken);
            if (emailExist) throw new InvalidOperationException("Email already in use");
            var userNameExist = await streamDb.Users.AnyAsync(u => u.UserName == request.UserName, cancellationToken);
            if (userNameExist) throw new InvalidOperationException("Username already taken");

            if (request.Password.Length < 8) throw new ArgumentException("Password must be at least 8 characters long");
            var hashedPassword = passwordHasher.Hash(request.Password);

            var newUser = new User(
                userName: request.UserName,
                email: request.Email,
                passwordHash: hashedPassword,
                isActive: true,
                createdAt: DateTime.UtcNow
                );

            streamDb.Users.Add(newUser);

            var accessToken = tokenProvider.Create(newUser);
            var refreshTokenOwn = tokenProvider.GenerateRefreshToken();

            var refreshToken = new RefreshToken(
                token:refreshTokenOwn,
                expiresAt:tokenProvider.RefreshTokenExpiresAt,
                createdAt: DateTime.UtcNow,
                userId: newUser.Id
            );

            await streamDb.RefreshTokens.AddAsync(refreshToken, cancellationToken);

            await streamDb.SaveChangesAsync(cancellationToken);
           
            var userDto = mapper.Map<UserDto>(newUser);
            return new AuthResponseDto(
              userDto,
              accessToken,
              refreshToken.Token,
              tokenProvider.AccessTokenExpirationInSeconds
            );
        }
    }
}