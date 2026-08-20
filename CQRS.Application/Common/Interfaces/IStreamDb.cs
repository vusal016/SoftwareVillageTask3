namespace StreamVibe.Application.Common.Interfaces
{
    public interface IStreamDb
    {
        DbSet<Genre> Genres { get; set; }
        DbSet<Device> Devices { get; set; }
        DbSet<Faq> Faqs { get; set; }
        DbSet<PricingPlan> PricingPlans { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}