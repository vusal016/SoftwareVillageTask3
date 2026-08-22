namespace StreamVibe.Domain.Entities
{
    public sealed class Genre : BaseEntity
    {
        private Genre()
        {
            
        }
        public Genre(string name, string slug, GenreType type, string coverImage_1, string coverImage_2, string coverImage_3, string coverImage_4)
        {
            SetName(name);
            SetSlug(slug);
            SetType(type);
            SetCoverImage_1(coverImage_1);
            SetCoverImage_2(coverImage_2);
            SetCoverImage_3(coverImage_3);
            SetCoverImage_4(coverImage_4);
        }
        public string Name { get;private set; }
        public string Slug { get;private set; }
        public GenreType Type { get;private set; }
        public string CoverImage_1 { get;private set; }
        public string CoverImage_2 { get;private set; }
        public string CoverImage_3 { get;private set; }
        public string CoverImage_4 { get;private set; }
        public ICollection<ContentGenres> ContentGenres { get; private set; } = [];

        private void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name can't be empty");
            Name = name;
        }
        private void SetSlug(string slug)
        {
            if (string.IsNullOrWhiteSpace(slug))
                throw new ArgumentException("Slug is required");    
            Slug = slug;
        }
        private void SetType(GenreType type)
        {
            if(!Enum.IsDefined(type))
                throw new ArgumentException("Invalid genre type");
            Type = type;
        }
        private void SetCoverImage_1(string coverImage_1)
        {
            if (string.IsNullOrWhiteSpace(coverImage_1))
                throw new ArgumentException("Cover image 1 is required");
            CoverImage_1 = coverImage_1;
        }
        private void SetCoverImage_2(string coverImage_2)
        {
            if (string.IsNullOrWhiteSpace(coverImage_2))
                throw new ArgumentException("Cover image 2 is required");
            CoverImage_2 = coverImage_2;
        }
        private void SetCoverImage_3(string coverImage_3)
        {
            if (string.IsNullOrWhiteSpace(coverImage_3))
                throw new ArgumentException("Cover image 3 is required");
            CoverImage_3 = coverImage_3;
        }
        private void SetCoverImage_4(string coverImage_4)
        {
            if (string.IsNullOrWhiteSpace(coverImage_4))
                throw new ArgumentException("Cover image 4 is required");   
            CoverImage_4 = coverImage_4;
        }
    }
}