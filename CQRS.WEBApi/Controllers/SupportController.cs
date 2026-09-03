namespace StreamVibe.WEBApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupportController(IMediator mediator) : ControllerBase
    {
        [HttpPost("contact")]
        public async Task<IActionResult> Contact(SendContactMessagesCommand command, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(command, cancellationToken);
            var response = Response<ContactMessageDto>.Success(result, 200);
            return Ok(response);
        }
    }
}