namespace StreamVibe.Domain.Entities
{
    public sealed class ContentLanguages: BaseEntity
    {
        private ContentLanguages()
        {
            
        }
        public ContentLanguages(Guid contentId, string language)
        {
            SetContentId(contentId);
            SetLanguage(language);
        }

        public Guid ContentId { get;private set; }
        public string Language { get;private set; }
        public Content Content { get; private set; }

        private void SetLanguage(string language)
        {
            if(string.IsNullOrWhiteSpace(language))
            {
                throw new KeyNotFoundException("Language required.");
            }
            Language = language;
        }
        private void SetContentId(Guid contentId)
        {
            if (contentId == Guid.Empty)
                throw new ArgumentException("ContentId can't be empty");
            ContentId = contentId;
        }
    }
}
