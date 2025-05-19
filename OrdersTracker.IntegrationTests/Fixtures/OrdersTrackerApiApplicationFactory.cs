using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace OrdersTracker.IntegrationTests.Fixtures;

public class OrdersTrackerApiApplicationFactory<TStartup> : WebApplicationFactory<TStartup>
    where TStartup : class
{
    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.UseEnvironment("Test");

        builder.ConfigureAppConfiguration((context, config) =>
        {
            config.Sources.Clear();

            config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            config.AddJsonFile("appsettings.Test.json", optional: false, reloadOnChange: true);
            config.AddEnvironmentVariables();
        });

        builder.ConfigureServices(services =>
        {
        });

        return base.CreateHost(builder);
    }
}
