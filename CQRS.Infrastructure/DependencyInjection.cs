namespace StreamVibe.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StreamDb>(options =>
                 options.UseNpgsql(
                     configuration.GetConnectionString("DefaultConnection"),
                     o => o.MapEnum<GenreType>("type_genre", "public")
                   )
             );
            services.AddScoped<IStreamDb, StreamDb>();
            return services;
        }
    }
}