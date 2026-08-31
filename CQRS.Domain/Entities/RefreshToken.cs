namespace StreamVibe.Domain.Entities
{
    public sealed class RefreshToken : BaseEntity
    {
        private RefreshToken()
        {
            
        }
        public RefreshToken(string token, DateTime expiresAt, DateTime createdAt, Guid userId)
        {
            SetToken(token);
            SetUserId(userId);
            ExpiresAt = expiresAt;
            CreatedAt = createdAt;
        }
        public string Token { get;private set; }
        public DateTime ExpiresAt { get;private set; }
        public DateTime CreatedAt { get; private set; }
        public Guid UserId { get; private set; }
        public User User { get;private set; }

        private void SetToken(string token)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace("Refresh token cannot be empty.");
            Token = token;
        }
        private void SetUserId(Guid userId)
        {
          if (userId == Guid.Empty)throw new ArgumentException("User ID cannot be empty.");
            UserId = userId;
        }
    }
}