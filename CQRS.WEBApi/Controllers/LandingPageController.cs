namespace StreamVibe.WEBApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LandingPageController(IMediator mediator) : ControllerBase
    {
        [HttpGet("devices")]
        public async Task<IActionResult> GetAllDevices(CancellationToken cancellationToken)
        {
            var devices = await mediator.Send(new GetAllDevicesQuery(), cancellationToken);
            var response=Response<List<DeviceDto>>.Success(devices, 200);
            return Ok(response);
        }
        [HttpGet("genres    ")]
        public async Task<IActionResult> GetAllGenres(GenreType? type,CancellationToken cancellationToken)
        {
            var genres = await mediator.Send(new GetAllGenresQuery(type), cancellationToken);
            var response = Response<List<GenreDto>>.Success(genres, 200);
            return Ok(response);
        }
        [HttpGet("faqs")]
        public async Task<IActionResult> GetAllFaqs(CancellationToken cancellationToken)
        {
            var faqs = await mediator.Send(new GetAllFaqsQuery(), cancellationToken);
            var response = Response<List<FaqDto>>.Success(faqs, 200);
            return Ok(response);
        }
        [HttpGet("plans")]
        public async Task<IActionResult> GetAllPricingPlans(string? billing, CancellationToken cancellationToken)
        {
            var plans = await mediator.Send(new GetPricingPlansQuery(billing), cancellationToken);
            var response = Response<List<PricingPlanDto>>.Success(plans, 200);
            return Ok(response);
        }
    }
}