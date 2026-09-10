namespace StreamVibe.WEBApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionsController(IMediator mediator) : ControllerBase
    {
        /// <summary>
        /// Get available pricing plans with their features.
        /// </summary>
        /// <remarks>
        /// The <paramref name="billing"/> query parameter accepts the following values: "monthly", "yearly". Default: "monthly".
        /// The response contains a list of plans and each plan includes a "features" array describing included features.
        /// </remarks>
        /// <response code="200">Returns a list of pricing plans with features.</response>
        [HttpGet("plans")]
        [ProducesResponseType(typeof(Response<List<PricingPlanWithFeaturesDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllPricingPlans(UserBillingCycle billing = UserBillingCycle.Monthly, CancellationToken cancellationToken = default)
        {
            var plans = await mediator.Send(new GetPlansWithFeaturesQuery(billing), cancellationToken);
            var response = Response<List<PricingPlanWithFeaturesDto>>.Success(plans, 200);
            return Ok(response);
        }

        /// <summary>
        /// Create a subscription for the authenticated user.
        /// </summary>
        /// <param name="command">Request body containing planId, billingCycle ("monthly"/"yearly"), and isTrial flag.</param>
        /// <response code="201">Subscription created successfully.</response>
        /// <response code="400">Bad request (invalid parameters).</response>
        /// <response code="401">Unauthorized (missing or invalid token).</response>
        /// <response code="404">Plan not found.</response>
        /// <response code="409">Conflict (e.g., user already has an active subscription).</response>
        [HttpPost("subscribe")]
        [Authorize]
        [ProducesResponseType(typeof(Response<CreateSubDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> CreateSubscription(CreateSubscriptionCommand command, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var subscriptionCommand = new CreateSubscriptionCommand(userId, command.PlanId, command.BillingCycle, command.IsTrial);
            var result = await mediator.Send(subscriptionCommand, cancellationToken);
            var response = Response<CreateSubDto>.Success(result, 201);
            return Ok(response);
        }

        /// <summary>
        /// Get the authenticated user's current subscription.
        /// </summary>
        /// <response code="200">Returns the user's subscription.</response>
        /// <response code="401">Unauthorized (missing or invalid token).</response>
        /// <response code="404">Subscription not found.</response>
        [HttpGet("my")]
        [Authorize]
        [ProducesResponseType(typeof(Response<UserSubscriptionDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMySubscription(CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var result = await mediator.Send(new GetMySubscriptionsQuery(userId), cancellationToken);
            var response = Response<UserSubscriptionDto>.Success(result, 200);
            return Ok(response);
        }

        /// <summary>
        /// Cancel the authenticated user's subscription.
        /// </summary>
        /// <response code="200">Subscription cancelled successfully.</response>
        /// <response code="401">Unauthorized (missing or invalid token).</response>
        [HttpDelete("cancel")]
        [Authorize]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CancelSubscription(CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var result = await mediator.Send(new DeleteSubscriptionCommand(userId), cancellationToken);
            var response = Response<string>.Success(result, 200);
            return Ok(response);
        }
    }
}   