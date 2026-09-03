namespace StreamVibe.Infrastructure.Configurations
{
    public sealed class ContactMessagesConfigurations : IEntityTypeConfiguration<ContactMessages>
    {
        public void Configure(EntityTypeBuilder<ContactMessages> builder)
        {
            builder.ToTable("contact_messages");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.FirstName).HasColumnName("first_name").IsRequired();
            builder.Property(x => x.LastName).HasColumnName("last_name").IsRequired();
            builder.Property(x => x.Email).HasColumnName("email").IsRequired();
            builder.Property(x => x.PhoneCountryCode).HasColumnName("phone_country_code").IsRequired();
            builder.Property(x => x.PhoneNumber).HasColumnName("phone_number").IsRequired();
            builder.Property(x => x.Message).HasColumnName("message").IsRequired();
            builder.Property(x => x.IsRead).HasColumnName("is_read").HasDefaultValue(false).IsRequired();
            builder.Property(x => x.CreatedAt).HasColumnName("created_at").IsRequired();
        }
    }
}
