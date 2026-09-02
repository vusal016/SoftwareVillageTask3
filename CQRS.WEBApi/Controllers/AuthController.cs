namespace StreamVibe.WEBApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IMediator mediator) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterCommand command, CancellationToken ct)
        {
            var result = await mediator.Send(command, ct);
            var response = Response<AuthResponseDto>.Success(result, 201);
            return Ok(response);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginCommand command, CancellationToken ct)
        {
            var result = await mediator.Send(command, ct);
            var response = Response<AuthResponseDto>.Success(result, 200);
            return Ok(response);
        }
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(GetRefreshTokenCommand command, CancellationToken ct)
        {
            var result = await mediator.Send(command, ct);
            var response = Response<RefreshTokenDto>.Success(result, 200);
            return Ok(response);
        }
        [HttpPost("logout")]
        public async Task<IActionResult> Logout(LogoutCommand command, CancellationToken ct)
        {
            await mediator.Send(command, ct);
            var response = Response<string>.Success("Logged out successfully", 200);
            return Ok(response);
        }
        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetMe(CancellationToken ct)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var result = await mediator.Send(new GetProfileQuery(userId), ct);
            var response = Response<UserDto>.Success(result, 200);
            return Ok(response);
        }
    }
}