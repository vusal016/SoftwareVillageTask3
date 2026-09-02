namespace StreamVibe.Application.Features.Profile
{
    public record GetProfileQuery(Guid UserId) : IRequest<UserDto>;
}
