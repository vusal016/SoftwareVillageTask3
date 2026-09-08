namespace StreamVibe.Infrastructure.Configurations
{
    public sealed class UserSubscriptionConfigurations : IEntityTypeConfiguration<UserSubscription>
    {
        public void Configure(EntityTypeBuilder<UserSubscription> builder)
        {
            builder.ToTable("user_subscriptions");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.UserId).HasColumnName("user_id").IsRequired();
            builder.HasIndex(x => x.UserId).IsUnique();
            builder.Property(x => x.PlanId).HasColumnName("plan_id").IsRequired();
            builder.Property(x => x.BillingCycle).HasColumnName("billing_cycle").IsRequired();
            builder.Property(x => x.IsTrial).HasColumnName("is_trial").HasDefaultValue(false).IsRequired();
            builder.Property(x => x.Status).HasColumnName("status").HasDefaultValue(UserSubStatus.Active).IsRequired();
            builder.Property(x => x.StartedAt).HasColumnName("started_at").HasColumnType("timestamp without time zone").IsRequired();
            builder.Property(x => x.ExpiresAt).HasColumnName("expires_at").HasColumnType("timestamp without time zone").IsRequired();
            builder.Property(x => x.CreatedAt).HasColumnName("created_at").HasColumnType("timestamp without time zone").IsRequired();
            builder.Property(x => x.UpdatedAt).HasColumnName("updated_at").HasColumnType("timestamp without time zone").IsRequired();
            builder.HasOne(x => x.User).WithMany(x => x.UserSubscriptions).HasForeignKey(x => x.UserId);
            builder.HasOne(x => x.PricingPlan).WithMany(x => x.UserSubscriptions).HasForeignKey(x => x.PlanId);
        }
    }
}
