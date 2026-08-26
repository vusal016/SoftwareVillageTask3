namespace StreamVibe.Infrastructure.Configurations
{
    public sealed class SeasonsConfiguration : IEntityTypeConfiguration<Seasons>
    {
        public void Configure(EntityTypeBuilder<Seasons> builder)
        {
            builder.ToTable("seasons");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.ContentId).HasColumnName("content_id").IsRequired();
            builder.Property(x => x.SeasonNumber).HasColumnName("season_number").IsRequired();
            builder.Property(x => x.EpisodeCount).HasColumnName("episode_count").IsRequired();
            builder.Property(x => x.Title).HasColumnName("title").HasMaxLength(100).IsRequired();
            builder.HasOne(x => x.Content).WithMany(x => x.Seasons).HasForeignKey(x => x.ContentId);
        }
    }
}
