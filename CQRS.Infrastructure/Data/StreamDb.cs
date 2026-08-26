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
        public DbSet<ContentLanguages> ContentLanguages { get; set; }
        public DbSet<People> Peoples { get; set; }
        public DbSet<Seasons> Seasons { get; set; }
        public DbSet<Episodes> Episodes { get; set; }
        public DbSet<Reviews> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(StreamDb).Assembly);
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            //Hele implement etmemisem novbeti tasklarda istifade edecem
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}