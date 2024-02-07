using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using StuddGokApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testcontainers.MySql;

namespace TestDockerDb;

public class StuddGokApiWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly MySqlContainer _mySqlContainer;

    public StuddGokApiWebAppFactory()
    {
        _mySqlContainer = new MySqlBuilder()
            .WithImage("anders71/studd_test_db")
            .WithDatabase("studd_gok_api")
            .WithUsername("ga-app")
            .WithPassword("ga-5ecret-%")
            .Build();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            // først ta vekk DbContextOptions
            var descriptor = services.FirstOrDefault(
                s => s.ServiceType == typeof(DbContextOptions<StuddGokDbContext>));
            if (descriptor is not null)
            {
                services.Remove(descriptor);
            }

            //var conn = $"Server=localhost;Port={_port};Database=ga_emne7_studentblogg; User ID=ga-app;Password=ga-5ecret-%;";

            // legger til ny DbContextOptions
            services.AddDbContext<StuddGokDbContext>(options =>
            {
                options.UseMySql(
                    _mySqlContainer.GetConnectionString(),
                    new MySqlServerVersion(new Version(8, 0, 33)),
                    builder =>
                    {
                        builder.EnableRetryOnFailure();
                    });
            });
        });
    }


    public async Task InitializeAsync()
    {
        await _mySqlContainer.StartAsync();
    }

    public new async Task DisposeAsync()
    {
        await _mySqlContainer.StopAsync();
    }
}
