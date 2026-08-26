namespace StreamVibe.Infrastructure.Configurations
{
    public sealed class PeopleConfigurations : IEntityTypeConfiguration<People>
    {
        public void Configure(EntityTypeBuilder<People> builder)
        {
            builder.ToTable("people");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.Name).HasColumnName("name").HasMaxLength(100).IsRequired();
            builder.Property(x => x.AvatarUrl).HasColumnName("avatar_url").IsRequired();
            builder.Property(x => x.Nationality).HasColumnName("nationality").HasMaxLength(50).IsRequired();
        }
    }
}
