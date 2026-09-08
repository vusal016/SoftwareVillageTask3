namespace StreamVibe.Application.Features.Profile.Queries.GetMySubscriptions
{
    public sealed class GetMySubscriptionsQueryHandler(IStreamDb streamDb) : IRequestHandler<GetMySubscriptionsQuery, UserSubscriptionDto>
    {
        public async Task<UserSubscriptionDto> Handle(GetMySubscriptionsQuery request, CancellationToken cancellationToken)
        {
            var existSubscription = await streamDb.UserSubscriptions.AsNoTracking()
                .Where(s => s.UserId == request.UserId && s.Status == UserSubStatus.Active)
                .Include(s => s.PricingPlan).FirstOrDefaultAsync(cancellationToken);
            if (existSubscription == null) throw new KeyNotFoundException("No active subscription");
            var plan = new PricingPlanSubDto(
                 existSubscription.PricingPlan.Id,
               existSubscription.PricingPlan.Name,
               existSubscription.PricingPlan.Description,
               existSubscription.BillingCycle == UserBillingCycle.Monthly ? existSubscription.PricingPlan.PriceMonthly : existSubscription.PricingPlan.PriceYearly
             );

            return new UserSubscriptionDto(
                existSubscription.Id,
                existSubscription.BillingCycle,
                existSubscription.IsTrial,
                existSubscription.Status,
                existSubscription.StartedAt,
               existSubscription.ExpiresAt,
              plan
           );
        }
    }
}