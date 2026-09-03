namespace StreamVibe.Application.Features.SendContactMessages
{
    public sealed class SendContactMessagesCommandHandler(IStreamDb streamDb,IMapper mapper) : IRequestHandler<SendContactMessagesCommand, ContactMessageDto>
    {
        public async Task<ContactMessageDto> Handle(SendContactMessagesCommand request, CancellationToken cancellationToken)
        {
            var message=mapper.Map<ContactMessages>(request);
            await streamDb.ContactMessages.AddAsync(message, cancellationToken);
            await streamDb.SaveChangesAsync(cancellationToken);
            var result = mapper.Map<ContactMessageDto>(message);
            return result with { SuccessMessage = "Your message has been sent successfully" };
        }
    }
}