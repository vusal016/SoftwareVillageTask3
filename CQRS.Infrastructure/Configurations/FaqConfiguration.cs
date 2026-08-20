namespace StreamVibe.Infrastructure.Configurations
{
    public sealed class FaqConfiguration : IEntityTypeConfiguration<Faq>
    {
        public void Configure(EntityTypeBuilder<Faq> builder)
        {
            builder.ToTable("faqs");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.Question).HasColumnName("question").HasMaxLength(300).IsRequired();
            builder.Property(x => x.Answer).HasColumnName("answer").IsRequired();
            builder.Property(x => x.OrderNumber).HasColumnName("order_number").IsRequired();
        }
    }
}
