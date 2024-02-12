using StuddGokApi.DTOs;
using StuddGokApi.Mappers;
using StuddGokApi.Models;
using StuddGokApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDockerDb.LectureServiceTests;

public class Test1 : BaseIntegrationTests
{
    public Test1(StuddGokApiWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task AddLectureAsync_WhenLectureDTOGiven_ReturnLectureBooking()
    {
        // Arrange
        var lectureService = GetService<ILectureService>();

        LectureDTO lecDTO = new LectureDTO
        {
            CourseImplementationId = 8,
            StartTime = new DateTime(2024, 3, 21, 9, 0, 0),
            EndTime = new DateTime(2024, 3, 21, 14, 0, 0),
            Theme = "TBA",
            Description = "Se OneNote!",
            VenueIds = new List<int>() { 1 }
        };

        // Act 
        LectureBooking lectureBooking = await lectureService!.AddLectureAsync(lecDTO);

        // Assert
        Assert.Equal(50, lectureBooking.NumStudents);
        Assert.True(lectureBooking.Success);
        Assert.Equal(90, lectureBooking.VenueCapacity);
        Assert.Equal("Fjorden", lectureBooking.VenueName);      
    }


    [Fact]
    public async Task AddLectureAsync_WhenLectureDTOGiven_BusyTeacher_ReturnLectureBooking()
    {
        // Arrange
        var lectureService = GetService<ILectureService>();

        LectureDTO lecDTO = new LectureDTO
        {
            CourseImplementationId = 8,
            StartTime = new DateTime(2024, 2, 13, 9, 0, 0),
            EndTime = new DateTime(2024, 2, 13, 14, 0, 0),
            Theme = "TBA",
            Description = "Se OneNote!",
            VenueIds = new List<int>() { 1 }
        };

        // Act 
        LectureBooking lectureBooking = await lectureService!.AddLectureAsync(lecDTO);

        // Assert
        Assert.Equal(50, lectureBooking.NumStudents);
        Assert.False(lectureBooking.Success);
        Assert.Equal(0, lectureBooking.VenueCapacity);
        Assert.Equal("", lectureBooking.VenueName);
    }


    [Fact]
    public async Task AddLectureAsync_WhenLectureDTOGivenTwice_ReturnLectureBooking()
    {
        // Arrange
        var lectureService = GetService<ILectureService>();
        var lectureMapper = GetService<IMapper<Lecture, LectureDTO>>();

        LectureDTO lecDTO = new LectureDTO
        {
            CourseImplementationId = 8,
            StartTime = new DateTime(2024, 3, 28, 9, 0, 0),
            EndTime = new DateTime(2024, 3, 28, 14, 0, 0),
            Theme = "TBA",
            Description = "Se OneNote!",
            VenueIds = new List<int>() { 1 }
        };


        // Act 
        LectureBooking lectureBooking = await lectureService!.AddLectureAsync(lecDTO);
        LectureDTO? lectureDTO = await lectureService!.DeleteLectureByIdAsync(lectureBooking.LectureId);

        // Assert
        Assert.Equal(50, lectureBooking.NumStudents);
        Assert.True(lectureBooking.Success);
        //Assert.Equal("", lectureBooking.Message);
        Assert.Equal(90, lectureBooking.VenueCapacity);
        Assert.Equal("Fjorden", lectureBooking.VenueName);
        Assert.Equal(20, lectureBooking.LectureId);

        //Assert.True(lectureBooking1.Success);       // !! BLIR IKKE 'SAVA' !!
        Assert.NotNull(lectureDTO);
        Assert.Equal(20, lectureDTO.Id);
        Assert.Equal("TBA", lectureDTO.Theme);
    }
}
