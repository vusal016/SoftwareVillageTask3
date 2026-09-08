namespace StreamVibe.Application.Features.Profile.Commands.DeleteSubscriptionCommand
{
    public record DeleteSubscriptionCommand(Guid UserId) : IRequest<string>;
}
