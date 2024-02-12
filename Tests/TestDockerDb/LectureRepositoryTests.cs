using Microsoft.Extensions.DependencyInjection;
using StuddGokApi.Models;
using StuddGokApi.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDockerDb;

public class LectureRepositoryTests : BaseIntegrationTests
{
    private ILectureRepository? _lectureRepo;
    public LectureRepositoryTests(StuddGokApiWebAppFactory factory) : base(factory)
    {
        _lectureRepo = _scope.ServiceProvider.GetService<ILectureRepository>();
    }

    [Fact]
    public async Task CheckTeacher_When_CourseImpId_to_from_given_ShouldReturn_OneLecture()
    {
        // Arrange
        Lecture lecture = new Lecture
        {
            Id = 17,
            CourseImplementationId = 8,
            Theme = "Forelesning 12",
            Description = "TBA",
            StartTime = new DateTime(2024, 2, 13, 9, 0, 0),
            EndTime = new DateTime(2024, 2, 13, 14, 0, 0)
        };

        // Act
        Lecture? lec = await _lectureRepo!.CheckTeacher(8, new DateTime(2024, 2, 13, 11, 0, 0), new DateTime(2024, 2, 13, 13, 0, 0));

        //Assert
        Assert.NotNull(lec);
        Assert.Equal(lecture.Id, lec.Id);
    }

    [Fact]
    public async Task AddLectureAndVenueAsync_LectureAndVenueIdGiven_ReturnLectureAndLectureVenue()
    {
        // Arrange
        Lecture lecture = new Lecture
        {
            //Id = 20,
            CourseImplementationId = 8,
            Theme = "Forelesning 12",
            Description = "TBA",
            StartTime = new DateTime(2024, 3, 21, 9, 0, 0),
            EndTime = new DateTime(2024, 3, 21, 14, 0, 0),
        };

        // Act
        (Lecture?, LectureVenue?) lec = await _lectureRepo!.AddLectureAndVenueAsync(lecture, 1);

        //Assert
        Assert.NotNull(lec.Item1);
        Assert.NotNull(lec.Item2);
        Assert.Equal("Forelesning 12", lec.Item1.Theme);
    }

    [Fact]
    public async Task AddLecture()
    {
        // Arrange
        Lecture lecture = new Lecture
        {
            //Id = 20,
            CourseImplementationId = 8,
            Theme = "Forelesning 13",
            Description = "Balle",
            StartTime = new DateTime(2024, 3, 28, 9, 0, 0),
            EndTime = new DateTime(2024, 3, 28, 14, 0, 0),
        };

        // Act
        Lecture? lec = await _lectureRepo!.AddLectureAsync(lecture);

        //Assert
        Assert.NotNull(lec);
        Assert.Equal("Balle", lec.Description);
    }
}
