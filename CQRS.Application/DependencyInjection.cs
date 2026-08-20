namespace StreamVibe.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
            services.AddAutoMapper(cfg => cfg.AddProfile<StreamProfile>());
            services.AddFusionCache().WithDefaultEntryOptions(options =>
            {
                options.Duration = TimeSpan.FromMinutes(5);
            });
            return services;
        }
    }
}