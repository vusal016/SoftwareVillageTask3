namespace StreamVibe.Domain.Entities
{
    public sealed class User : BaseEntity
    {
        private User()
        {

        }
        public User(string userName, string email, string passwordHash, bool isActive, DateTime createdAt)
        {
            SetUserName(userName);
            SetEmail(email);
            PasswordHash = passwordHash;
            IsActive = isActive;
            CreatedAt = createdAt;
        }

        public string UserName { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public ICollection<RefreshToken> RefreshTokens { get; private set; } = [];

        private void SetUserName(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName) || userName.Length < 3 || userName.Length > 20)
                throw new ArgumentException("User name is not valid.");
            UserName = userName;
        }
        private void SetEmail(string email)
        {
            const string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (string.IsNullOrWhiteSpace(email) || !Regex.IsMatch(email, emailPattern))
                throw new ArgumentException("Email is not valid.");
            Email = email;
        }
    }
}