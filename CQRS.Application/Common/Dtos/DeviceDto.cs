namespace StreamVibe.Application.Common.Dtos
{
    public record DeviceDto(
        Guid Id,
        string Name,
        string Description,
        string IconName
        );
}
