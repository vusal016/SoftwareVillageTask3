namespace StreamVibe.Infrastructure.Configurations
{
    public sealed class ContentLanguagesConfigurations : IEntityTypeConfiguration<ContentLanguages>
    {
        public void Configure(EntityTypeBuilder<ContentLanguages> builder)
        {
            builder.ToTable("content_languages");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.ContentId).HasColumnName("content_id").IsRequired();
            builder.Property(x => x.Language).HasColumnName("language").HasMaxLength(50).IsRequired();
            builder.HasOne(x => x.Content).WithMany(x => x.ContentLanguages).HasForeignKey(x => x.ContentId);
        }
    }
}
