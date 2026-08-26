    namespace StreamVibe.Domain.Entities
{
    public sealed class ContentPeople: BaseEntity
    {
        private ContentPeople()
        {
            
        }
        public ContentPeople(Guid contentId, Guid peopleId,string characterName, RoleType type)
        {
            SetCharacterName(characterName);
            SetContentId(contentId);
            SetPeopleId(peopleId);
            SetType(type);
        }
        public string CharacterName { get; private set; }
        public RoleType Type { get; private set; }
        public Guid ContentId { get;private set; }
        public Guid PeopleId { get;private set; }
        public Content Content { get; private set; }
        public People People { get; private set; }

        private void SetCharacterName(string characterName)
        {
            if (string.IsNullOrWhiteSpace(characterName))
                throw new ArgumentException("CharacterName can't be empty");
            CharacterName = characterName;
        }
        private void SetType(RoleType type)
        {
            if (!Enum.IsDefined(type))
                throw new ArgumentException("Invalid genre type");
            Type = type;
        }
        private void SetContentId(Guid contentId)
        {
            if (contentId == Guid.Empty)
                throw new ArgumentException("ContentId can't be empty");
            ContentId = contentId;
        }
        private void SetPeopleId(Guid peopleId)
        {
            if (peopleId == Guid.Empty)
                throw new ArgumentException("PeopleId can't be empty");
            PeopleId = peopleId;
        }
    }
}
