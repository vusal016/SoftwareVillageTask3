namespace StreamVibe.WEBApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IMediator mediator): ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterCommand command,CancellationToken ct)
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
    }
}