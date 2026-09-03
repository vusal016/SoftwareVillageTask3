namespace StreamVibe.Domain.Entities
{
    public sealed class ContactMessages : BaseEntity
    {
        private ContactMessages()
        {

        }
        public ContactMessages(string firstName, string lastName, string email, string phoneCountryCode, string phoneNumber, string message)
        {
            SetFirstName(firstName);
            SetLastName(lastName);
            SetEmail(email);
            SetPhoneCountryCode(phoneCountryCode);
            SetPhoneNumber(phoneNumber);
            SetMessage(message);
            IsRead =false;
            CreatedAt =DateTime.UtcNow;
        }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string PhoneCountryCode { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Message { get; private set; }
        public bool IsRead { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private void SetFirstName(string firstName)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(firstName, "First name required");
            FirstName = firstName;
        }

        private void SetLastName(string lastName)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(lastName, "Last name required");
            LastName = lastName;
        }
        private void SetEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email) || !Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
            {
                throw new ArgumentException("Invalid email format");
            }
            Email = email;
        }
        private void SetPhoneCountryCode(string phoneCountryCode)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(phoneCountryCode, "Phone country code required");
            PhoneCountryCode = phoneCountryCode;
        }
        private void SetPhoneNumber(string phoneNumber)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(phoneNumber, "Phone number required");
            if (!phoneNumber.All(char.IsDigit))
                throw new ArgumentException("Phone number must contain only digits");
            PhoneNumber = phoneNumber;
        }
        private void SetMessage(string message)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(message, "Message required");
            if (message.Length < 20)
                throw new ArgumentException("Message must be at least 20 characters");
            Message = message;
        }
    }
}