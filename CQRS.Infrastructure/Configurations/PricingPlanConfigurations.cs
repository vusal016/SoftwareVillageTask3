namespace StreamVibe.Infrastructure.Configurations
{
    public sealed class PricingPlanConfigurations : IEntityTypeConfiguration<PricingPlan>
    {
        public void Configure(EntityTypeBuilder<PricingPlan> builder)
        {
            builder.ToTable("pricing_plans");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.Name).HasColumnName("name").HasMaxLength(50).IsRequired();
            builder.Property(x => x.Description).HasColumnName("description").IsRequired();
            builder.Property(x => x.PriceMonthly).HasColumnName("price_monthly").HasPrecision(6, 2).IsRequired();
            builder.Property(x => x.PriceYearly).HasColumnName("price_yearly").HasPrecision(6, 2).IsRequired();
            builder.Property(x => x.IsPopular).HasColumnName("is_popular").HasDefaultValue(false).IsRequired();
        }
    }
}
