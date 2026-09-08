namespace StreamVibe.Domain.Entities
{
    public sealed class UserSubscription : BaseEntity
    {
        private UserSubscription()
        {

        }
        public UserSubscription(Guid userId, Guid planId, UserBillingCycle billingCycle, UserSubStatus status, bool isTrial, DateTime startedAt, DateTime expiresAt, DateTime updatedAt)
        {
            SetUserId(userId);
            SetPlanId(planId);
            SetBillingCycle(billingCycle);
            SetStatus(status);
            IsTrial = isTrial;
            StartedAt = startedAt;
            ExpiresAt = expiresAt;
            CreatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified);
            UpdatedAt = updatedAt;
        }
        public UserBillingCycle BillingCycle { get; private set; }
        public UserSubStatus Status { get; private set; }
        public bool IsTrial { get; private set; }
        public DateTime StartedAt { get; private set; }
        public DateTime ExpiresAt { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public Guid UserId { get; private set; }
        public User User { get; private set; }
        public Guid PlanId { get; private set; }
        public PricingPlan PricingPlan { get; private set; }

        private void SetUserId(Guid userId)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException("User ID cannot be empty.");
            UserId = userId;
        }
        private void SetPlanId(Guid planId)
        {
            if (planId == Guid.Empty)
                throw new ArgumentException("Plan ID cannot be empty.");
            PlanId = planId;
        }
        private void SetBillingCycle(UserBillingCycle billingCycle)
        {
            if (!Enum.IsDefined(billingCycle))
                throw new ArgumentException("Invalid billing cycle.type.");
            BillingCycle = billingCycle;
        }
        private void SetStatus(UserSubStatus status)
        {
            if (!Enum.IsDefined(status))
                throw new ArgumentException("Invalid subscription status.");
            Status = status;
        }
        public void CancelStatus()
        {
            Status = UserSubStatus.Cancelled;
        }
    }
}
