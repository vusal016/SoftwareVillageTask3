namespace StreamVibe.Infrastructure.Identity
{
    public sealed class TokenProvider(IOptions<JwtSettings> jwtOptions) : ITokenProvider
    {
        private readonly JwtSettings configuration = jwtOptions.Value;

        public DateTime RefreshTokenExpiresAt => DateTime.UtcNow.AddDays(configuration.RefreshTokenExpiresDays);    

        public int AccessTokenExpirationInSeconds => configuration.ExpirationInMinutes * 60;

        public string Create(User user)
        {
            
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.Secret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity([
                 new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                 new Claim(JwtRegisteredClaimNames.Email, user.Email),
                 new Claim(JwtRegisteredClaimNames.Name, user.UserName)
               ]),
                Expires = DateTime.UtcNow.AddMinutes(configuration.ExpirationInMinutes),
                SigningCredentials = credentials,
                Issuer = configuration.Issuer,
                Audience = configuration.Audience   
            };
            var tokenHandler = new JsonWebTokenHandler();
            string token = tokenHandler.CreateToken(tokenDescriptor);

            return token;
        }

        public string GenerateRefreshToken()
        {
           return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        }
    }
}