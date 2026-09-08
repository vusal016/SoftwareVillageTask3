namespace StreamVibe.Domain.Entities
{
    public sealed class PlanFeatures: BaseEntity
    {
        private PlanFeatures()
        {
            
        }
        public PlanFeatures(string featureName, string featureValue, int orderNumber, Guid planId)
        {
            SetFeatureName(featureName);
            SetFeatureValue(featureValue);
            SetOrderNumber(orderNumber);
            SetPlanId(planId);
        }

        public string FeatureName { get;private set; }
        public string FeatureValue { get;private set; }
        public int OrderNumber { get;private set; }
        public Guid PlanId { get;private set; }
        public PricingPlan PricingPlan { get;private set; }

        private void SetFeatureName(string featureName)
        {
          ArgumentException.ThrowIfNullOrWhiteSpace(featureName, "Feature name can't be empty");
            FeatureName = featureName;
        }

        private void SetFeatureValue(string featureValue)
        {
          ArgumentException.ThrowIfNullOrWhiteSpace(featureValue, "Feature value can't be empty");
            FeatureValue = featureValue;
        }

        private void SetOrderNumber(int orderNumber)
        {
            if (orderNumber < 0)
                throw new ArgumentException("Order number can't be negative");
            OrderNumber = orderNumber;
        }
        private void SetPlanId(Guid planId)
        {
            if (planId == Guid.Empty)
                throw new ArgumentException("Plan ID cannot be empty.");
            PlanId = planId;
        }
    }
}
