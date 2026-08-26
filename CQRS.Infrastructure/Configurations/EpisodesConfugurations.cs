namespace StreamVibe.Infrastructure.Configurations
{
    public sealed class EpisodesConfigurations : IEntityTypeConfiguration<Episodes>
    {
        public void Configure(EntityTypeBuilder<Episodes> builder)
        {
            builder.ToTable("episodes");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.SeasonsId).HasColumnName("season_id").IsRequired();
            builder.Property(x => x.EpisodeNumber).HasColumnName("episode_number").IsRequired();
            builder.Property(x => x.Title).HasColumnName("title").HasMaxLength(200).IsRequired();
            builder.Property(x => x.Description).HasColumnName("description");
            builder.Property(x => x.ThumbnailUrl).HasColumnName("thumbnail_url").HasMaxLength(500);
            builder.Property(x => x.DurationMinutes).HasColumnName("duration_minutes").IsRequired();
            builder.HasOne(x => x.Seasons).WithMany(x => x.Episodes).HasForeignKey(x => x.SeasonsId);
        }
    }
}
