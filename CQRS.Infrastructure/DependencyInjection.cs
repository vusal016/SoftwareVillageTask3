namespace StreamVibe.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StreamDb>(options =>
                 options.UseNpgsql(
                     configuration.GetConnectionString("DefaultConnection"),
                     o => {
                         o.MapEnum<GenreType>("type_genre", "public");
                         o.MapEnum<ContentType>("type_content", "public");
                         o.MapEnum<RoleType>("type_content_people", "public");
                     }
                   )
             );
            services.AddScoped<IStreamDb, StreamDb>();
            return services;
        }
    }
}