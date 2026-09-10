namespace StreamVibe.WEBApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IMediator mediator): ControllerBase
    {
        /// <summary>
        /// Get the authenticated user's profile.
        /// </summary>
        /// <remarks>
        /// The returned ProfileDto includes a "subscription" field which can be null when the user has no active subscription.
        /// </remarks>
        /// <response code="200">Returns the user's profile.</response>
        /// <response code="401">Unauthorized (missing or invalid token).</response>
        [HttpGet("profile")]
        [Authorize]
        [ProducesResponseType(typeof(Response<ProfileDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetProfile(CancellationToken ct)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var result = await mediator.Send(new GetProfileQuery(userId), ct);
            var response = Response<ProfileDto>.Success(result, 200);
            return Ok(response);
        }
    }
}