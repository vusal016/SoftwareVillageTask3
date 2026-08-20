namespace StreamVibe.Domain.Entities
{
    public sealed class PricingPlan : BaseEntity
    {
        private PricingPlan()
        {
            
        }
        public PricingPlan(string name, string description, decimal priceMonthly, decimal priceYearly, bool isPopular)
        {
            SetName(name);
            SetDescription(description);
            SetPriceMonthly(priceMonthly);
            SetPriceYearly(priceYearly);
            IsPopular = isPopular;
        }
        public string Name { get;private set; }
        public string Description { get;private set; }
        public decimal PriceMonthly { get; private set; }
        public decimal PriceYearly { get; private set; }
        public bool IsPopular { get; private set; }

        private void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name can't be empty");
            Name = name;
        }
        private void SetDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Description can't be empty");
            Description = description;
        }
        private void SetPriceMonthly(decimal priceMonthly)
        {
            if (priceMonthly < 0)
                throw new ArgumentException("Price monthly can't be negative");
            PriceMonthly = priceMonthly;
        }
        private void SetPriceYearly(decimal priceYearly)
        {
            if (priceYearly < 0)
                throw new ArgumentException("Price yearly can't be negative");
            PriceYearly = priceYearly;
        }
    }
}
