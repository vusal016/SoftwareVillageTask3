namespace StreamVibe.Domain.Entities
{
    public sealed class Seasons : BaseEntity
    {
        private Seasons()
        {

        }
        public Seasons(string title, int seasonNumber, int episodeCount, Guid contentId)
        {
            SetTitle(title);
            SetSeasonNumber(seasonNumber);
            SetEpisodeCount(episodeCount);
            SetContentId(contentId);
        }

        public string Title { get; private set; }
        public int SeasonNumber { get; private set; }
        public int EpisodeCount { get; private set; }
        public ICollection<Episodes> Episodes { get; private set; } = [];
        public Guid ContentId { get; private set; }
        public Content Content { get; private set; }

        private void SetTitle(string title)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(title);
            Title = title;
        }
        private void SetSeasonNumber(int seasonNumber)
        {
            if (seasonNumber <= 0)
                throw new ArgumentException("Season number must be greater than zero.");
            SeasonNumber = seasonNumber;
        }
        private void SetEpisodeCount(int episodeCount)
        {
            if (episodeCount < 0)
                throw new ArgumentException("Episode count cannot be negative.");
            EpisodeCount = episodeCount;
        }
        private void SetContentId(Guid contentId)
        {
            if (contentId == Guid.Empty)
                throw new ArgumentException("Content ID cannot be empty.");
            ContentId = contentId;
        }
    }
}