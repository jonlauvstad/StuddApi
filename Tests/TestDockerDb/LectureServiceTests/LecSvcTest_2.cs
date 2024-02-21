using StuddGokApi.DTOs;
using StuddGokApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDockerDb.LectureServiceTests;

public class LecSvcTest_2 : BaseIntegrationTests

{
    public LecSvcTest_2(StuddGokApiWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public void Add2LectureAsync_WhenLectureDTOGivenNoVenue_ReturnLectureBooking()
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
        };

        // Act & Assert
        //Task lectureBooking = Task.Run(() => lectureService!.AddLectureAsync(lecDTO))
        //    .ContinueWith(t =>
        //    {
        //        Assert.Equal(50, t.Result.NumStudents);
        //        Assert.Equal(string.Empty, t.Result.VenueName);
        //        Assert.True(t.Result.Success);
        //    });
    }


    [Fact]
    public async Task Add3LectureAsync_WhenLectureDTOGivenBusyTeacher_ReturnLectureBooking()
    {
        // Arrange
        var lectureService = GetService<ILectureService>();
        LectureDTO lDTO = new LectureDTO
        {
            CourseImplementationId = 8,
            StartTime = new DateTime(2024, 2, 13, 9, 0, 0),
            EndTime = new DateTime(2024, 2, 13, 14, 0, 0),
            Theme = "TBA",
            Description = "Se OneNote!",
            VenueIds = new List<int>() { 1 }
        };

        // Act
        //LectureBooking lectureBooking = await lectureService!.AddLectureAsync(lDTO);

        ////Assert
        //Assert.NotNull(lectureBooking);
        //Assert.Equal(50, lectureBooking.NumStudents);
        //Assert.False(lectureBooking.Success);
    }


   
}
