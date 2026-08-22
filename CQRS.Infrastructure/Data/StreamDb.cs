namespace StreamVibe.Infrastructure.Data
{
    public sealed class StreamDb(DbContextOptions<StreamDb> options) : DbContext(options), IStreamDb
    {
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Faq> Faqs { get; set; }
        public DbSet<PricingPlan> PricingPlans { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<ContentGenres> ContentGenres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(StreamDb).Assembly);
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}