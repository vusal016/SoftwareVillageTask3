namespace StreamVibe.Infrastructure.Configurations
{
    public sealed class ContentGenresConfigurations : IEntityTypeConfiguration<ContentGenres>
    {
        public void Configure(EntityTypeBuilder<ContentGenres> builder)
        {
            builder.ToTable("content_genres");
            builder.Ignore(x => x.Id);
            builder.HasKey(x => new { x.ContentId, x.GenreId });
            builder.Property(x => x.ContentId).HasColumnName("content_id").IsRequired();
            builder.Property(x => x.GenreId).HasColumnName("genre_id").IsRequired();
            builder.HasOne(x => x.Content).WithMany(x => x.ContentGenres).HasForeignKey(x => x.ContentId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.Genre).WithMany(x => x.ContentGenres).HasForeignKey(x => x.GenreId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
