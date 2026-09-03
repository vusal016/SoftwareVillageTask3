namespace StreamVibe.Application.Common.Interfaces
{
    public interface IStreamDb
    {
        DbSet<Genre> Genres { get; set; }
        DbSet<Device> Devices { get; set; }
        DbSet<Faq> Faqs { get; set; }
        DbSet<PricingPlan> PricingPlans { get; set; }
        DbSet<Content> Contents { get; set; }
        DbSet<ContentGenres> ContentGenres { get; set; }
        DbSet<ContentLanguages> ContentLanguages { get; set; }
        DbSet<People> Peoples { get; set; }
        DbSet<Seasons> Seasons { get; set; }
        DbSet<Episodes> Episodes { get; set; }
        DbSet<Reviews> Reviews { get; set; }
        DbSet <User> Users { get; set; }
        DbSet<RefreshToken> RefreshTokens { get; set; }
        DbSet<ContactMessages> ContactMessages { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}