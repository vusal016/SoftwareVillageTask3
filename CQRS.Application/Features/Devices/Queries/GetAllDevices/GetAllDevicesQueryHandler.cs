namespace StreamVibe.Application.Features.Devices.Queries.GetAllDevices
{
    public sealed class GetAllDevicesQueryHandler(IStreamDb streamDb, IMapper mapper,IFusionCache cache) : IRequestHandler<GetAllDevicesQuery, List<DeviceDto>>
    {
        public async Task<List<DeviceDto>> Handle(GetAllDevicesQuery request, CancellationToken cancellationToken)
        {
            return await cache.GetOrSetAsync(
                 "getall_devices",
                 async token =>
                 {
                     var devices =await streamDb.Devices.AsNoTracking().Take(6).ToListAsync(cancellationToken);
                     var deviceDtos = mapper.Map<List<DeviceDto>>(devices);
                     return deviceDtos;
                 },
                 token: cancellationToken
                );
        }
    }
}