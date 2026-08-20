namespace StreamVibe.Infrastructure.Configurations
{
    public sealed class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.ToTable("genres");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.Name).HasColumnName("name").HasMaxLength(50).IsRequired();
            builder.Property(x => x.Slug).HasColumnName("slug").HasMaxLength(50).IsRequired();
            builder.HasIndex(x => x.Slug).IsUnique();
            builder.Property(x => x.Type).HasColumnName("type").HasColumnType("public.type_genre").IsRequired();
            builder.Property(x => x.CoverImage_1).HasColumnName("cover_image_1").HasMaxLength(500);
            builder.Property(x => x.CoverImage_2).HasColumnName("cover_image_2").HasMaxLength(500);
            builder.Property(x => x.CoverImage_3).HasColumnName("cover_image_3").HasMaxLength(500);
            builder.Property(x => x.CoverImage_4).HasColumnName("cover_image_4").HasMaxLength(500);
        }
    }
}