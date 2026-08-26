namespace StreamVibe.Domain.Entities
{
    public sealed class Reviews : BaseEntity
    {
        private Reviews()
        {
            
        }
        public Reviews(string reviewerName, string reviewerLocation, string reviewText, decimal rating,DateTime createdAt, Guid contentId)
        {
            SetReviewerName(reviewerName);
            SetReviewerLocation(reviewerLocation);
            SetReviewText(reviewText);
            SetRating(rating);
            SetContentId(contentId);
            CreatedAt = createdAt;
        }
        public string ReviewerName { get; private set; }
        public string ReviewerLocation { get; private set; }
        public string ReviewText { get; private set; }
        public decimal Rating { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public Guid ContentId { get; private set; }
        public Content Content { get; private set; }

        private void SetReviewerName(string reviewerName)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(reviewerName);
            ReviewerName = reviewerName;
        }
        private void SetReviewerLocation(string reviewerLocation)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(reviewerLocation);
            ReviewerLocation = reviewerLocation;
        }
        private void SetReviewText(string reviewText)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(reviewText);
            ReviewText = reviewText;
        }
        private void SetRating(decimal rating)
        {
            if (rating < 0 || rating > 5)
                throw new ArgumentException("Rating must be between 0 and 5.");
            Rating = rating;
        }
        private void SetContentId(Guid contentId)
        {
            if (contentId == Guid.Empty)
                throw new ArgumentException("Content ID cannot be empty.");
            ContentId = contentId;
        }
    }
}
