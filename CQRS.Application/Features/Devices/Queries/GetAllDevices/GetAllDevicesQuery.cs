namespace StreamVibe.Application.Features.Devices.Queries.GetAllDevices
{
    public record GetAllDevicesQuery : IRequest<List<DeviceDto>>;
}