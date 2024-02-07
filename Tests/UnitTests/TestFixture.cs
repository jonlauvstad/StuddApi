using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StuddGokApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests;

public class TestFixture : IDisposable
{

    public StuddGokDbContext DbContext { get; }

    public TestFixture()
    {
        // Initialize your DbContext using the configuration from the main project
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        var options = new DbContextOptionsBuilder<StuddGokDbContext>()
            .UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 21)))
            .Options;

        DbContext = new StuddGokDbContext(options);
    }

        public void Dispose()
    {
        DbContext.Dispose();
    }
}
