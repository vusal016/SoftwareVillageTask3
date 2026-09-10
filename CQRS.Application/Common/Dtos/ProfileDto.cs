namespace StreamVibe.Application.Common.Dtos
{
    public record ProfileDto
    (
       UserDto User,
       ProfileSubDto? Subscription
    );
}
