namespace StreamVibe.Domain.Entities
{
    public sealed class Episodes : BaseEntity
    {
        public Episodes()
        {
            
        }
        public Episodes(string title, string description, string thumbNailUrl, int episodeNumber, int durationMinutes, Guid seasonsId)
        {
            SetTitle(title);
            SetDescription(description);
            SetThumbNailUrl(thumbNailUrl);
            SetEpisodeNumber(episodeNumber);
            SetDurationMinutes(durationMinutes);
            SetSeasonsId(seasonsId);
        }
        public string Title { get;private set; }
        public string Description { get; private set; }
        public string ThumbnailUrl { get; private set; }
        public int EpisodeNumber { get; private set; }
        public int DurationMinutes { get; private set; }
        public Guid SeasonsId { get;private set; }
        public Seasons Seasons { get;private set; }

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
        private void SetThumbNailUrl(string thumbNailUrl)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(thumbNailUrl);
            ThumbnailUrl = thumbNailUrl;
        }
        private void SetEpisodeNumber(int episodeNumber)
        {
            if (episodeNumber <= 0)
                throw new ArgumentException("Episode number must be greater than zero.");
            EpisodeNumber = episodeNumber;
        }
        private void SetDurationMinutes(int durationMinutes)
        {
            if (durationMinutes <= 0)
                throw new ArgumentException("Duration minutes must be greater than zero.");
            DurationMinutes = durationMinutes;
        }
        private void SetSeasonsId(Guid seasonsId)
        {
            if (seasonsId == Guid.Empty)
                throw new ArgumentException("Seasons ID cannot be empty.");
            SeasonsId = seasonsId;
        }
    }
}
