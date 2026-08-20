namespace StreamVibe.Domain.Entities
{
    public sealed class Device : BaseEntity
    {
        private Device()
        {
            
        }
        public Device(string name, string description, string iconName)
        {
            SetName(name);
            SetDescription(description);
            SetIconName(iconName);
        }
        public string Name { get;private set; }
        public string Description { get;private set; }
        public string IconName { get;private set; }

        private void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name can't be empty");
            Name = name;
        }
        private void SetDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Description can't be empty");
            Description = description;
        }
        private void SetIconName(string iconName)
        {
            if (string.IsNullOrWhiteSpace(iconName))
                throw new ArgumentException("Icon name can't be empty");
            IconName = iconName;
        }
    }
}
