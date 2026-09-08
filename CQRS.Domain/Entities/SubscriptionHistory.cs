namespace StreamVibe.Domain.Entities
{
    public sealed class SubscriptionHistory:BaseEntity
    {
        private SubscriptionHistory()
        {

        }
        public SubscriptionHistory(Guid userId, Guid planId, UserBillingCycle billingCycle, HistorySubStatus status, bool isTrial, DateTime startedAt, DateTime expiredAt)
        {
            SetUserId(userId);
            SetPlanId(planId);
            SetBillingCycle(billingCycle);
            SetStatus(status);
            StartedAt = startedAt;
            ExpiredAt = expiredAt;
            IsTrial = isTrial;
            CreatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified);
        }

        public bool IsTrial { get;private set; }
        public UserBillingCycle BillingCycle { get;private set; }
        public HistorySubStatus Status { get;private set; }
        public DateTime StartedAt { get;private set; }
        public DateTime ExpiredAt { get; private set; }
        public DateTime CreatedAt { get; private set; }
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
            PlanId =     planId;
        }
        private void SetStatus(HistorySubStatus status)
        {
            if (!Enum.IsDefined(status))
                throw new ArgumentException("Invalid subscription status.");
            Status = status;
        }
        private void SetBillingCycle(UserBillingCycle billingCycle)
        {
            if (!Enum.IsDefined(billingCycle))
                throw new ArgumentException("Invalid billing cycle.type.");
            BillingCycle = billingCycle;
        }
    }
}
