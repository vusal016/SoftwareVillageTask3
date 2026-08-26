namespace StreamVibe.Domain.Entities
{
    public sealed class People : BaseEntity
    {
        private People()
        {

        }
        public People(string name, string avatarUrl, string nationality)
        {
            SetName(name);
            SetAvatarUrl(avatarUrl);
            SetNationality(nationality);
        }
        public string Name { get; private set; }
        public string AvatarUrl { get; private set; }
        public string Nationality { get; private set; }
        public ICollection<ContentPeople> ContentPeople { get; private set; } = [];

        private void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name can't be empty");
            Name = name;
        }
        private void SetAvatarUrl(string avatarUrl)
        {
            if (string.IsNullOrWhiteSpace(avatarUrl))
                throw new ArgumentException("AvatarUrl can't be empty");
            AvatarUrl = avatarUrl;
        }
        private void SetNationality(string nationality)
        {
            if (string.IsNullOrWhiteSpace(nationality))
                throw new ArgumentException("Nationality can't be empty");
            Nationality = nationality;
        }
    }
}
