namespace StreamVibe.Infrastructure.Configurations
{
    public sealed class ReviewsConfigurationsL : IEntityTypeConfiguration<Reviews>
    {
        public void Configure(EntityTypeBuilder<Reviews> builder)
        {
            builder.ToTable("reviews");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.ContentId).HasColumnName("content_id").IsRequired();
            builder.Property(x => x.ReviewerName).HasColumnName("reviewer_name").HasMaxLength(100).IsRequired();
            builder.Property(x => x.ReviewerLocation).HasColumnName("reviewer_location").HasMaxLength(100);
            builder.Property(x => x.Rating).HasColumnName("rating").HasPrecision(3, 1).IsRequired();
            builder.Property(x => x.ReviewText).HasColumnName("review_text").IsRequired();
            builder.Property(x => x.CreatedAt).HasColumnName("created_at").IsRequired();
            builder.HasOne(x => x.Content).WithMany(x => x.Reviews).HasForeignKey(x => x.ContentId);
        }
    }
}
