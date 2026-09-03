namespace StreamVibe.Application.Features.SendContactMessages
{
    public record SendContactMessagesCommand
        (
        string FirstName,
        string LastName,
        string Email,
        string PhoneCountryCode,
        string PhoneNumber,
        string Message) : IRequest<ContactMessageDto>;
}
