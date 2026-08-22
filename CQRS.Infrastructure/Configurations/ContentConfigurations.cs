namespace StreamVibe.Infrastructure.Configurations
{
    public sealed class ContentConfigurations : IEntityTypeConfiguration<Content>
    {
        public void Configure(EntityTypeBuilder<Content> builder)
        {
            builder.ToTable("content");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.Title).HasColumnName("title").IsRequired();
            builder.Property(x => x.Description).HasColumnName("description").IsRequired();
            builder.Property(x => x.PosterUrl).HasColumnName("poster_url").IsRequired();
            builder.Property(x => x.BackGroundUrl).HasColumnName("background_url").IsRequired();
            builder.Property(x => x.TrailerUrl).HasColumnName("trailer_url");
            builder.Property(x => x.Type).HasColumnName("type").IsRequired();
            builder.Property(x => x.ReleaseYear).HasColumnName("release_year").IsRequired();
            builder.Property(x => x.ImdbRating).HasColumnName("imdb_rating").HasPrecision(3, 1).IsRequired();
            builder.Property(x => x.StreamVibeRating).HasColumnName("streamvibe_rating").HasPrecision(3, 1).IsRequired();
            builder.Property(x => x.IsTrending).HasColumnName("is_trending").IsRequired();
            builder.Property(x => x.IsNewRelease).HasColumnName("is_new_release").IsRequired();
            builder.Property(x => x.IsMustWatch).HasColumnName("is_must_watch").IsRequired();
            builder.Property(x => x.IsFeatured).HasColumnName("is_featured").IsRequired();
            builder.Property(x => x.TopTenRank).HasColumnName("top_ten_rank");
            builder.Property(x => x.CreatedAt).HasColumnName("created_at").IsRequired();
        }
    }
}
