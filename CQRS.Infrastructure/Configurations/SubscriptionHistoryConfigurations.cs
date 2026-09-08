namespace StreamVibe.Infrastructure.Configurations
{
    public sealed class SubscriptionHistoryConfigurations : IEntityTypeConfiguration<SubscriptionHistory>
    {
        public void Configure(EntityTypeBuilder<SubscriptionHistory> builder)
        {
            builder.ToTable("subscription_history");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.UserId).HasColumnName("user_id").IsRequired();
            builder.Property(x => x.PlanId).HasColumnName("plan_id").IsRequired();
            builder.Property(x => x.BillingCycle).HasColumnName("billing_cycle").IsRequired();
            builder.Property(x => x.IsTrial).HasColumnName("is_trial").HasDefaultValue(false).IsRequired();
            builder.Property(x => x.Status).HasColumnName("status").IsRequired();
            builder.Property(x => x.StartedAt).HasColumnName("started_at").HasColumnType("timestamp without time zone").IsRequired();
            builder.Property(x => x.ExpiredAt).HasColumnName("expired_at").HasColumnType("timestamp without time zone").IsRequired();
            builder.Property(x => x.CreatedAt).HasColumnName("created_at").HasColumnType("timestamp without time zone").IsRequired();
            builder.HasOne(x => x.User).WithMany(x => x.SubscriptionHistories).HasForeignKey(x => x.UserId);
            builder.HasOne(x => x.PricingPlan).WithMany(x => x.SubscriptionHistories).HasForeignKey(x => x.PlanId);
        }
    }
}
