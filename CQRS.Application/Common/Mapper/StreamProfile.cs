namespace StreamVibe.Application.Common.Mapper
{
    public sealed class StreamProfile : Profile
    {
        public StreamProfile()
        {
            CreateMap<Device, DeviceDto>();
            CreateMap<Genre, GenreDto>();
            CreateMap<Faq, FaqDto>();
            CreateMap<Content, ContentDto>();
            CreateMap<Content, HeroContentDto>();
            CreateMap<Genre, OurGenresDto>();
            CreateMap<Genre, TopTenGenreDto>();
            CreateMap<Content, TopTenContentDto>()
            .ForCtorParam("Genres", opt => opt.MapFrom(src => src.ContentGenres.Select(x => x.Genre)));
        }
    }
}