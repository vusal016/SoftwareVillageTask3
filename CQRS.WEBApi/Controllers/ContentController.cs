namespace StreamVibe.WEBApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllContents(ContentType? type, string? filter, int limit = 10)
        {
            var contents = await mediator.Send(new GetAllContentsQuery(type, filter, limit));
            var response = Response<List<ContentDto>>.Success(contents, 200);
            return Ok(response);
        }
        [HttpGet("hero")]
        public async Task<IActionResult> GetHeroContents()
        {
            var contents = await mediator.Send(new GetAllContentForHeroQuery());
            var response = Response<List<HeroContentDto>>.Success(contents, 200);
            return Ok(response);
        }
        [HttpGet("genres")]
        public async Task<IActionResult> GetOurGenres(GenreType? type)
        {
            var genres = await mediator.Send(new GetOurGenresQuery(type));
            var response = Response<List<OurGenresDto>>.Success(genres, 200);
            return Ok(response);
        }
        [HttpGet("top-ten")]
        public async Task<IActionResult> GetTopTenRankContents(ContentType? type)
        {
            var contents = await mediator.Send(new GetTopTenRankContentQuery(type));
            var response = Response<List<TopTenContentDto>>.Success(contents, 200);
            return Ok(response);
        }
    }
}