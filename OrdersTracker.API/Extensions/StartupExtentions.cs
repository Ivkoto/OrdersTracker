namespace OrdersTracker.API.Extensions;

public static class StartupExtentions
{
    public static void AllowedOriginsPolicy(this IServiceCollection services, string[] allowedOrigins)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowedOriginsPolicy", policy =>
            {
                policy.WithOrigins(allowedOrigins!)
                      .WithMethods("GET")
                      .AllowAnyHeader()
                      .SetPreflightMaxAge(TimeSpan.FromHours(1));
            });
        });
    }
}

