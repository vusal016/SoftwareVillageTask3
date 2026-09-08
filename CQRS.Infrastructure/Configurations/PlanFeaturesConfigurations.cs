namespace StreamVibe.Infrastructure.Configurations
{
    public sealed class PlanFeaturesConfigurations : IEntityTypeConfiguration<PlanFeatures>
    {
        public void Configure(EntityTypeBuilder<PlanFeatures> builder)
        {
            builder.ToTable("plan_features");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.PlanId).HasColumnName("plan_id").IsRequired();
            builder.Property(x => x.FeatureName).HasColumnName("feature_name").HasMaxLength(100).IsRequired();
            builder.Property(x => x.FeatureValue).HasColumnName("feature_value").HasMaxLength(255).IsRequired();
            builder.Property(x => x.OrderNumber).HasColumnName("order_number").IsRequired();
            builder.HasOne(x => x.PricingPlan).WithMany(x => x.PlanFeatures).HasForeignKey(x => x.PlanId);
        }
    }
}
