namespace StreamVibe.Application.Features.Profile.Queries.GetProfile
{
    public record GetMeQuery(Guid UserId) : IRequest<UserDto>;
}
