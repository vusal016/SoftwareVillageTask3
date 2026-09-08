namespace StreamVibe.Application.Features.Profile.Commands.DeleteSubscriptionCommand
{
    public sealed class DeleteSubscriptionCommandHandler(IStreamDb streamDb) : IRequestHandler<DeleteSubscriptionCommand, string>
    {
        public async Task<string> Handle(DeleteSubscriptionCommand request, CancellationToken cancellationToken)
        {
            var subscription = await streamDb.UserSubscriptions.Where(x => x.UserId == request.UserId && x.Status == UserSubStatus.Active)
            .FirstOrDefaultAsync(cancellationToken);
            
            if (subscription == null) throw new InvalidOperationException("Subscription not found.");

            subscription.CancelStatus();

            var subscriptionHistory = new SubscriptionHistory(
                subscription.UserId,
                subscription.PlanId,
                subscription.BillingCycle,
                HistorySubStatus.Cancelled,
                subscription.IsTrial,
                subscription.StartedAt,
                subscription.ExpiresAt
           );

            await streamDb.SubscriptionHistories.AddAsync(subscriptionHistory, cancellationToken);
            streamDb.UserSubscriptions.Remove(subscription);
            await streamDb.SaveChangesAsync(cancellationToken);
            return "Subscription cancelled successfully.";
        }
    }
}