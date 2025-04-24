using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Wallet.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;

namespace Wallet.Tests.Integration;

public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");

        builder.ConfigureAppConfiguration((context, configBuilder) =>
        {
            configBuilder.Sources.Clear();
            configBuilder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            configBuilder.AddInMemoryCollection(new Dictionary<string, string>
            {
                { "Jwt:Key", "2vVV[.e$$*.Q!&J<8Tc~X,1Rmyp8w=$ABC" },
                { "Jwt:Issuer", "wallet-api" },
                { "Jwt:Audience", "wallet-api-users" },
                { "Jwt:ExpireMinutes", "30" }
            });
        });

        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<WalletDbContext>));
            if (descriptor != null)
                services.Remove(descriptor);

            var contextDescriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(WalletDbContext));
            if (contextDescriptor != null)
                services.Remove(contextDescriptor);

            services.AddEntityFrameworkInMemoryDatabase(); 
            var sp = services.BuildServiceProvider(); 

            services.AddDbContext<WalletDbContext>(options =>
            {
                options.UseInMemoryDatabase("WalletTestDb")
                       .UseInternalServiceProvider(sp);
            });
        });
    }
}
