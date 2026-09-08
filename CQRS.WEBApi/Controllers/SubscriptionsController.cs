namespace StreamVibe.WEBApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionsController(IMediator mediator) : ControllerBase
    {
        [HttpGet("plans")]
        public async Task<IActionResult> GetAllPricingPlans(UserBillingCycle billing, CancellationToken cancellationToken)
        {
            var plans = await mediator.Send(new GetPlansWithFeaturesQuery(billing), cancellationToken);
            var response = Response<List<PricingPlanWithFeaturesDto>>.Success(plans, 200);
            return Ok(response);
        }
        [HttpPost("subscribe")]
        [Authorize]
        public async Task<IActionResult> CreateSubscription(CreateSubscriptionCommand command, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var subscriptionCommand = new CreateSubscriptionCommand(userId, command.PlanId, command.BillingCycle, command.IsTrial);
            var result = await mediator.Send(subscriptionCommand, cancellationToken);
            var response = Response<CreateSubDto>.Success(result, 201);
            return Ok(response);
        }
        [HttpGet("my")]
        [Authorize]
        public async Task<IActionResult> GetMySubscription(CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var result = await mediator.Send(new GetMySubscriptionsQuery(userId), cancellationToken);
            var response = Response<UserSubscriptionDto>.Success(result, 200);
            return Ok(response);
        }
        [HttpDelete("cancel")]
        [Authorize]
        public async Task<IActionResult> CancelSubscription(CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var result = await mediator.Send(new DeleteSubscriptionCommand(userId), cancellationToken);
            var response = Response<string>.Success(result, 200);
            return Ok(response);
        }
    }
}   