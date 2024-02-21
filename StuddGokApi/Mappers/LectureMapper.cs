﻿using StuddGokApi.DTOs;
using StuddGokApi.Models;
using StuddGokApi.Repositories.Interfaces;

namespace StuddGokApi.Mappers;

public class LectureMapper : IMapper<Lecture, LectureDTO>
{
    private readonly ILectureRepository _lectureRepository;
    private readonly ILogger<LectureMapper> _logger;

    public LectureMapper(ILectureRepository lectureRepository, ILogger<LectureMapper> logger)
    {
        _lectureRepository = lectureRepository;
        _logger = logger;
    }

    public LectureDTO MapToDTO(Lecture model)
    {
        //IEnumerable<User> teachers = await _lectureRepository.GetTeachersByCourseImplementationId(model.CourseImplementationId);

        IEnumerable<Venue> venues = from lecVen in model.LectureVenues select lecVen.Venue;
        _logger.LogDebug($"VENUES-LENGTH:  venues-length: {venues.Count()}");

        return new LectureDTO
        {
            Id = model.Id,
            CourseImplementationId = model.CourseImplementationId,
            Theme = model.Theme,
            Description = model.Description,
            StartTime = model.StartTime,
            EndTime = model.EndTime,

            //LectureVenues = model.LectureVenues,
            //Attendances = model.Attendances,
            VenueIds = from lecVen in model.LectureVenues select lecVen.VenueId,
            VenueNamesList = from ven in venues select ven.Name,

            CourseImplementationName = model.CourseImplementation!.Name,
            CourseImplementationCode = model.CourseImplementation.Code,

            //TeacherName = 
        };
    }

    public Lecture MapToModel(LectureDTO dto)
    {
        return new Lecture
        {
            Id = dto.Id,
            CourseImplementationId = dto.CourseImplementationId,
            Theme = dto.Theme,
            Description = dto.Description,
            StartTime = dto.StartTime,
            EndTime = dto.EndTime,
        };
    }
}
