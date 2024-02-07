using Microsoft.EntityFrameworkCore;
using StuddGokApi.Data;
using StuddGokApi.DTMs;
using StuddGokApi.Repositories;
using StuddGokApi.Repositories.Interfaces;

namespace UnitTests;

public class VenueRepositoryTests : IClassFixture<TestFixture>
{
    //private static readonly StuddGokDbContext _dbContext = new StuddGokDbContext(
    //    new DbContextOptionsBuilder<StuddGokDbContext>()
    //        .UseMySql("Server=localhost;Database=studd_gok_api; User ID=ga-app;Password=ga-5ecret-%;", 
    //            new MySqlServerVersion(new Version(8, 0, 21)))
    //        .Options

    //);
    private readonly StuddGokDbContext _dbContext;
    private readonly IVenueRepository _venueRepo; //= new VenueRepository(_dbContext);
                                                  
    public VenueRepositoryTests(TestFixture fixture)
    {
        _dbContext = fixture.DbContext;
        _venueRepo = new VenueRepository(_dbContext);
    }

    [Fact]
    public async void CheckVenue_When_VenueId_to_from_given_ShouldReturn_OneEvent()
    {
        // Arrange
        Event evt = new Event
        {
            UnderlyingId = 1,
            Time = new DateTime(2024, 5, 22, 9, 0, 0),
            Type = "Eksamen",
            TypeEng = "ExamImplementation",
            CourseImplementationId = 8,
            CourseImpCode = "CWAKV24SA",
            CourseImpName = "Cloud-WebArkitektur-Container V24 Sandefjord",
            TimeEnd = new DateTime(2024, 5, 22, 14, 0, 0),
        };

        // Act
        Event ev = await _venueRepo.CheckVenue(2, new DateTime(2024, 5, 22, 9, 0, 0), new DateTime(2024, 5, 22, 14, 0, 0));

        // Assert
        Assert.NotNull(ev);
        Assert.Equal(evt.UnderlyingId, ev.UnderlyingId);
        Assert.Equal(evt.Type, ev.Type);
        Assert.Equal(evt.CourseImplementationId, ev.CourseImplementationId);
        Assert.Equal(evt.Type, ev.Type);
    }

    [Fact]
    public async void CheckVenue_When_VenueId_to_from_given_ShouldReturn_EmptyIEnumerable()
    {
        // Act
        Event ev = await _venueRepo.CheckVenue(2, new DateTime(2024, 6, 23, 9, 0, 0), new DateTime(2024, 6, 23, 14, 0, 0));

        // Assert
        Assert.Null(ev);
    }
}