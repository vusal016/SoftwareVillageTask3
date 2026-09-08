namespace StreamVibe.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StreamDb>(options =>
                 options.UseNpgsql(
                     configuration.GetConnectionString("DefaultConnection"),
                     o =>
                     {
                         o.MapEnum<GenreType>("type_genre", "public");
                         o.MapEnum<ContentType>("type_content", "public");
                         o.MapEnum<RoleType>("type_content_people", "public");
                         o.MapEnum<UserBillingCycle>("user_billing_cycle", "public");
                         o.MapEnum<UserSubStatus>("user_substatus", "public");  
                         o.MapEnum<HistorySubStatus>("history_substatus", "public");
                     }
                   )
             );
            services.AddScoped<IStreamDb, StreamDb>();
            services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));

            var jwtSettings = configuration.GetSection(JwtSettings.SectionName).Get<JwtSettings>();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }
            )
            .AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwtSettings.Audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddSingleton<ITokenProvider, TokenProvider>();
            services.AddSingleton<IPasswordHasher, PasswordHasher>();
            return services;
        }
    }
}