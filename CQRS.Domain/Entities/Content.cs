namespace StreamVibe.Domain.Entities
{
    public sealed class Content:BaseEntity
    {
        private Content()
        {
            
        }
        public Content(string title,string description,string posterUrl,string backGroundUrl,string trailerUrl,ContentType type,int releaseYear,decimal imdbRating,decimal streamVibeRating,bool isTrending,bool isNewRelease,bool isMustWatch,bool isFeatured,int topTenRank,DateTime createdAt)
        {
            SetTitle(title);
            SetDescription(description);
            SetPosterUrl(posterUrl);
            SetBackGroundUrl(backGroundUrl);
            SetTrailerUrl(trailerUrl);
            SetType(type);
            SetReleaseYear(releaseYear);
            SetImdbRating(imdbRating);
            SetStreamVibeRating(streamVibeRating);
            IsTrending=isTrending;
            IsNewRelease=isNewRelease;
            IsMustWatch=isMustWatch;
            IsFeatured=isFeatured;
            TopTenRank=topTenRank;
            CreatedAt=createdAt;
        }
        public string Title { get;private set; }
        public string Description { get;private set; }
        public string PosterUrl { get;private set; }
        public string BackGroundUrl { get;private set; }
        public string? TrailerUrl { get;private set; }
        public ContentType Type { get;private set; }
        public int ReleaseYear { get;private set; }
        public decimal ImdbRating { get;private set; }
        public decimal StreamVibeRating { get;private set; }
        public bool IsTrending { get; private set; }
        public bool IsNewRelease { get; private set; }
        public bool IsMustWatch { get; private set; }
        public bool IsFeatured { get; private set; }
        public int? TopTenRank { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public ICollection<ContentPeople> ContentPeople { get; private set; } = [];
        public ICollection<ContentLanguages> ContentLanguages { get; private set; } = [];
        public ICollection<ContentGenres> ContentGenres { get; private set; } = [];
        public ICollection<Seasons> Seasons { get; private set; } = [];
        public ICollection<Reviews> Reviews { get; private set; } = [];

        private void SetTitle(string title)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(title);
            Title = title;
        }
        private void SetDescription(string description)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(description);
            Description = description;
        }
        private void SetPosterUrl(string posterUrl)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(posterUrl);
            PosterUrl = posterUrl;
        }
        private void SetBackGroundUrl(string backGroundUrl)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(backGroundUrl);
            BackGroundUrl = backGroundUrl;
        }
        private void SetTrailerUrl(string trailerUrl)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(trailerUrl);
            TrailerUrl = trailerUrl;
        }
        private void SetType(ContentType type)
        {
            if (!Enum.IsDefined(type))
                throw new ArgumentException("Invalid content type");
            Type = type;
        }
        private void SetReleaseYear(int releaseYear)
        {
            if (releaseYear < 1800 || releaseYear > DateTime.Now.Year)
                throw new ArgumentException("Invalid release year");
            ReleaseYear = releaseYear;
        }
        private void SetImdbRating(decimal imdbRating)
        {
            if (imdbRating < 0 || imdbRating > 10)
                throw new ArgumentException("Invalid IMDB rating");
            ImdbRating = imdbRating;
        }
        private void SetStreamVibeRating(decimal streamVibeRating)
        {
            if (streamVibeRating < 0 || streamVibeRating > 10)
                throw new ArgumentException("Invalid StreamVibe rating");
            StreamVibeRating = streamVibeRating;
        }
    }
}
