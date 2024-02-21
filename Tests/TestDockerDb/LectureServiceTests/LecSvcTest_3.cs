using StuddGokApi.DTOs;
using StuddGokApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDockerDb.LectureServiceTests;

public class LecSvcTest3 : BaseIntegrationTests
{
    public LecSvcTest3(StuddGokApiWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public void AddLectureAsync_WhenLectureDTOGivenTwice_ReturnLectureBookingFailure()
    {
        // Arrange
        var lectureService = GetService<ILectureService>();
        LectureDTO lDTO = new LectureDTO
        {
            CourseImplementationId = 8,
            StartTime = new DateTime(2024, 3, 21, 9, 0, 0),
            EndTime = new DateTime(2024, 3, 21, 14, 0, 0),
            Theme = "TBA",
            Description = "Se OneNote!",
            VenueIds = new List<int>() { 2 }
        };

        int lectureId = 0;
        // Act & Assert
        //Task lectureBooking = Task.Run(() => lectureService!.AddLectureAsync(lDTO))
        //    .ContinueWith(t =>
        //    {
        //        Assert.Equal(50, t.Result.NumStudents);
        //        Assert.True(t.Result.Success);

        //        lectureId = t.Result.LectureId;
        //    });

        //Task lectureBooking1 = Task.Run(() => lectureService!.AddLectureAsync(lDTO))
        //    .ContinueWith(t =>
        //    {
        //        Assert.Equal(50, t.Result.NumStudents);
        //        Assert.False(t.Result.Success);
        //    });

    }

    [Fact]
    public void AddLectureAsync_WhenLectureDTOGivenAndOther_ReturnLectureBookingSuccess()
    {
        // Arrange
        var lectureService = GetService<ILectureService>();
        LectureDTO lDTO = new LectureDTO
        {
            CourseImplementationId = 8,
            StartTime = new DateTime(2024, 3, 21, 9, 0, 0),
            EndTime = new DateTime(2024, 3, 21, 14, 0, 0),
            Theme = "TBA",
            Description = "Se OneNote!",
            VenueIds = new List<int>() { 2 }
        };

        LectureDTO lecDTO = new LectureDTO
        {
            CourseImplementationId = 8,
            StartTime = new DateTime(2024, 3, 22, 9, 0, 0),
            EndTime = new DateTime(2024, 3, 22, 14, 0, 0),
            Theme = "TBA",
            Description = "Se OneNote!",
            VenueIds = new List<int>() { 2 }
        };

        int lectureId = 0;
        // Act & Assert
        //Task lectureBooking = Task.Run(() => lectureService!.AddLectureAsync(lDTO))
        //    .ContinueWith(t =>
        //    {
        //        Assert.Equal(50, t.Result.NumStudents);
        //        Assert.True(t.Result.Success);

        //        lectureId = t.Result.LectureId;
        //    });


        //Task lectureBooking1 = Task.Run(() => lectureService!.AddLectureAsync(lecDTO))
        //    .ContinueWith(t =>
        //    {
        //        Assert.Equal(50, t.Result.NumStudents);
        //        Assert.True(t.Result.Success);
        //    });

    }

    [Fact]
    public void AddLectureAsync_WhenLectureDTOGivenAndDeleted_ReturnLecture()
    {
        // Arrange
        var lectureService = GetService<ILectureService>();
        LectureDTO lDTO = new LectureDTO
        {
            CourseImplementationId = 8,
            StartTime = new DateTime(2024, 3, 21, 9, 0, 0),
            EndTime = new DateTime(2024, 3, 21, 14, 0, 0),
            Theme = "TBA",
            Description = "Se OneNote!",
            VenueIds = new List<int>() { 2 }
        };

        int lectureId = 0;
        // Act & Assert
        //Task lectureBooking = Task.Run(() => lectureService!.AddLectureAsync(lDTO))
        //    .ContinueWith(t =>
        //    {
        //        lectureId = t.Result.LectureId;
        //    })
        //    .ContinueWith(x =>
        //    {
        //        Task lecture = Task.Run(() => lectureService!.DeleteLectureByIdAsync(lectureId))
        //            .ContinueWith(t =>
        //            {
        //                //Assert.Equal(lectureId, t.Result!.Id);
        //                Assert.Equal(1000, t.Result!.Id);
        //            });
        //    });
    }
}
