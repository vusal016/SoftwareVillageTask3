namespace StreamVibe.Application.Features.Profile.Commands.CreateSubscriptionCommand
{
    public sealed class CreateSubscriptionPlanCommandHandler(IStreamDb streamDb,IMapper mapper) : IRequestHandler<CreateSubscriptionCommand, CreateSubDto>
    {
        public async Task<CreateSubDto> Handle(CreateSubscriptionCommand request, CancellationToken cancellationToken)
        {
            var subscriptionExist = await streamDb.UserSubscriptions.AnyAsync(s => s.UserId == request.UserId && (s.Status == UserSubStatus.Active || s.Status == UserSubStatus.Trial), cancellationToken);
            if (subscriptionExist) throw new InvalidOperationException("You already have an active subscription.");
            var planExist = await streamDb.PricingPlans.AnyAsync(p => p.Id == request.PlanId, cancellationToken);
            if (!planExist) throw new KeyNotFoundException("Plan not found.");

            var currentTime = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified);
            var status = request.IsTrial ? UserSubStatus.Trial : UserSubStatus.Active;
            var expiresAt = request.IsTrial ? currentTime.AddDays(7):
             request.BillingCycle switch
            {
                UserBillingCycle.Monthly => currentTime.AddDays(30),
                UserBillingCycle.Yearly => currentTime.AddDays(365),
               _ => throw new ArgumentException("Invalid billing cycle.")
            };

            var newSubscription = new UserSubscription(
              request.UserId,
              request.PlanId,
              request.BillingCycle,
              status,
              request.IsTrial,
              currentTime,
              expiresAt,
              currentTime
          );

            await streamDb.UserSubscriptions.AddAsync(newSubscription, cancellationToken);
            await streamDb.SaveChangesAsync(cancellationToken);
            return mapper.Map<CreateSubDto>(newSubscription);
        }
    }
}                       