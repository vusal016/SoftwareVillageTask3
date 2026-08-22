namespace StreamVibe.Application.Common.Dtos
{
    public record OurGenresDto(
         Guid Id,
        string Name,
        string Slug,
        string CoverImage_1,
        string CoverImage_2,
        string CoverImage_3,
        string CoverImage_4
   );
}
