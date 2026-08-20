namespace StreamVibe.Application.Common.Dtos
{
    public record GenreDto(
        Guid Id,
        string Name,
        string Slug,
        GenreType Type,
        string CoverImage_1,
        string CoverImage_2,
        string CoverImage_3,
        string CoverImage_4
        );
}