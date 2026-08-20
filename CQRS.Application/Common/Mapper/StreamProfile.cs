namespace StreamVibe.Application.Common.Mapper
{
    public sealed class StreamProfile : Profile
    {
        public StreamProfile()
        {
            CreateMap<Device, DeviceDto>();
            CreateMap<Genre, GenreDto>();
            CreateMap<Faq, FaqDto>();
        }
    }
}