namespace StreamVibe.Application.Features.Profile.Queries.GetProfile
{
    public record GetProfileQuery(Guid UserId) : IRequest<UserDto>;
}
