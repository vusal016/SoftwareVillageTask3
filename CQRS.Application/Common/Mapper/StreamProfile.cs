using StreamVibe.Application.Features.SendContactMessages;

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
            CreateMap<Genre, GenreContentDetailDto>();
            CreateMap<ContentPeople, CastContentDetailDto>()
            .ForCtorParam("Id", opt => opt.MapFrom(x => x.People.Id))
            .ForCtorParam("Name", opt => opt.MapFrom(x => x.People.Name))
            .ForCtorParam("AvatarUrl", opt => opt.MapFrom(x => x.People.AvatarUrl))
            .ForCtorParam("Nationality", opt => opt.MapFrom(x => x.People.Nationality))
            .ForCtorParam("CharacterName", opt => opt.MapFrom(x => x.CharacterName));
            CreateMap<People, PersonDto>();
            CreateMap<Content, ContentDetailDto>()
            .ForCtorParam("Genres", opt => opt.MapFrom(src => src.ContentGenres.Select(x => x.Genre)))
            .ForCtorParam("Languages", opt => opt.MapFrom(src => src.ContentLanguages.Select(x => x.Language)))
            .ForCtorParam("Casts", opt => opt.MapFrom(src => src.ContentPeople))
            .ForCtorParam("Directors", opt => opt.MapFrom(src => src.ContentPeople.Where(d => d.Type == RoleType.Director).Select(d => d.People)))
            .ForCtorParam("Musics", opt => opt.MapFrom(src => src.ContentPeople.Where(m => m.Type == RoleType.Music).Select(c => c.People)));
            CreateMap<Episodes, EpisodeDto>();
            CreateMap<Seasons, SeasonDto>()
            .ForCtorParam("Episodes", opt => opt.MapFrom(src => src.Episodes));
            CreateMap<Reviews, ContentReviewDto>();
            CreateMap<User, UserDto>();
            CreateMap<ContactMessages, ContactMessageDto>()
           .ForCtorParam("SuccessMessage", opt => opt.MapFrom(_ => string.Empty));
            CreateMap<SendContactMessagesCommand, ContactMessages>();
        }
    }
}