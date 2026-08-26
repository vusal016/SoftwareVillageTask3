namespace StreamVibe.Infrastructure.Configurations
{
    public sealed class ContentPeopleConfiguration : IEntityTypeConfiguration<ContentPeople>
    {
        public void Configure(EntityTypeBuilder<ContentPeople> builder)
        {
            builder.ToTable("content_people");
            builder.Ignore(x => x.Id);
            builder.HasKey(x => new { x.ContentId, x.PeopleId });
            builder.Property(x => x.ContentId).HasColumnName("content_id").IsRequired();
            builder.Property(x => x.PeopleId).HasColumnName("people_id").IsRequired();
            builder.Property(x => x.CharacterName).HasColumnName("character_name").HasMaxLength(100).IsRequired();
            builder.Property(x => x.Type).HasColumnName("role_type").IsRequired();
            builder.HasOne(x => x.Content).WithMany(x => x.ContentPeople).HasForeignKey(x => x.ContentId);
            builder.HasOne(x => x.People).WithMany(x => x.ContentPeople).HasForeignKey(x => x.PeopleId);
        }
    }
}
