namespace StreamVibe.Domain.Entities
{
    public sealed class ContentGenres:BaseEntity
    {
        private ContentGenres()
        {
            
        }
        public ContentGenres(Guid contentId, Guid genreId)
        {
            SetContentId(contentId);
            SetGenreId(genreId);
        }

        public Guid GenreId { get;private set; }
        public Guid ContentId { get;private set; }
        public Content Content { get;private set; }
        public Genre Genre { get; private set; }

        private void SetGenreId(Guid genreId)
        {
            if (genreId == Guid.Empty)
                throw new ArgumentException("GenreId can't be empty");
            GenreId = genreId;
        }
        private void SetContentId(Guid contentId)
        {
            if (contentId == Guid.Empty)
                throw new ArgumentException("ContentId can't be empty");
            ContentId = contentId;
        }
    }
}
