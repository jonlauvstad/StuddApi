using Microsoft.Extensions.Logging;
using Moq;
using StuddGokApi.Data;
using StuddGokApi.DTMs;
using StuddGokApi.Models;
using StuddGokApi.Repositories;
using StuddGokApi.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests;

public class LectureRepositoryTests : IClassFixture<TestFixture>
{
    private readonly StuddGokDbContext _dbContext;
    private readonly ILectureRepository _lectureRepo;

    public LectureRepositoryTests(TestFixture fixture)
    {
        _dbContext = fixture.DbContext;
        _lectureRepo = new LectureRepository(_dbContext, new Mock<ILogger<LectureRepository>>().Object);
    }

    [Fact]
    public async void CheckTeacher_When_CourseImpId_to_from_given_ShouldReturn_OneLecture()
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
        Lecture? lec = await _lectureRepo.CheckTeacher(8, new DateTime(2024, 2, 13, 11, 0, 0), new DateTime(2024, 2, 13, 13, 0, 0));

        // Assert
        Assert.NotNull(lec);
        Assert.Equal(lecture.Id, lec.Id);
    }

    [Fact]
    public async void CheckTeacher_When_CourseImpId_to_from_given_ShouldReturn_EmptyIEnumerable()
    {
        // Act
        Lecture? lecture = await _lectureRepo.CheckTeacher(8, new DateTime(2024, 6, 25, 11, 0, 0), new DateTime(2024, 6, 25, 13, 0, 0));

        // Assert
        Assert.Null(lecture);
    }

}
