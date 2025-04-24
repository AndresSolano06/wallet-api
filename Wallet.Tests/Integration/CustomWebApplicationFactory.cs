using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Wallet.Infrastructure.Persistence;
using Wallet.API;

namespace Wallet.Tests.Integration;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<WalletDbContext>));
            if (descriptor is not null) services.Remove(descriptor);

            services.AddDbContext<WalletDbContext>(options =>
            {
                options.UseInMemoryDatabase("WalletTestDb");
            });
        });
    }
}
