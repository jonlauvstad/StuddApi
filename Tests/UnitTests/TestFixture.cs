using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StuddGokApi.Data;
using StuddGokApi.Repositories;
using StuddGokApi.Repositories.Interfaces;
using StuddGokApi.Services;
using StuddGokApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests;

public class TestFixture : IDisposable
{
    public StuddGokDbContext DbContext { get; }
    public ILectureRepository LectureRepository { get; }
    public IVenueRepository VenueRepository { get; }
    //public ILectureService LectureService { get; }

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

        LectureRepository = new LectureRepository(DbContext);
        VenueRepository = new VenueRepository(DbContext);
        //LectureService = new LectureService(LectureRepository, VenueRepository);
    }

        public void Dispose()
    {
        DbContext.Dispose();
    }
}
