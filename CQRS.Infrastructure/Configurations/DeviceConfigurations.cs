namespace StreamVibe.Infrastructure.Configurations
{
    public sealed class DeviceConfigurations : IEntityTypeConfiguration<Device>
    {
        public void Configure(EntityTypeBuilder<Device> builder)
        {

            builder.ToTable("devices");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id");

            builder.Property(x => x.Name)
                .HasColumnName("name")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasColumnName("description")
                .IsRequired();

            builder.Property(x => x.IconName)
                .HasColumnName("icon_name")
                .HasMaxLength(50);
        }
    }
}
