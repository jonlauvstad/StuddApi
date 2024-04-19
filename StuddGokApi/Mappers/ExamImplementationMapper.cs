using StuddGokApi.DTOs;
using StuddGokApi.Models;

namespace StuddGokApi.Mappers
{
    public class ExamImplementationMapper : IMapper<ExamImplementation, ExamImplementationDTO>
    {
        public ExamImplementationDTO MapToDTO(ExamImplementation model)
        {
            ExamImplementationDTO eiDTO = new ExamImplementationDTO()
            {
                Id = model.Id,
                ExamId = model.ExamId,
                VenueId = model.VenueId,
                StartTime = model.StartTime,
                EndTime = model.EndTime,
                //UserExamImplementation = model.UserExamImplementation,
                UserExamImplementation = model.UserExamImplementation
                                        .Select(x => new UserExamImplementation
                                        {
                                            UserId = x.UserId,
                                            ExamImplementationId = x.ExamImplementationId,
                                        }).ToList()
            };

            if ( model.Exam != null)
            {
                eiDTO.Category = model.Exam.Category;
                eiDTO.DurationHours = model.Exam.DurationHours;
                eiDTO.CourseImplementationId = model.Exam.CourseImplementationId;
                if (model.Exam.CourseImplementation != null)
                {
                    eiDTO.CourseImplementationName = model.Exam.CourseImplementation.Name;
                    eiDTO.CourseImplementationCode = model.Exam.CourseImplementation.Code;
                }
            }

            if ( model.Venue != null)
            {
                eiDTO.VenueName = model.Venue.Name;
                if (model.Venue.Location != null)
                {
                    eiDTO.Location = model.Venue.Location.Name;
                }
            }

            return eiDTO;
        }

        public ExamImplementation MapToModel(ExamImplementationDTO dto)
        {
            return new ExamImplementation()
            {
                Id = dto.Id,
                ExamId = dto.ExamId,
                VenueId = dto.VenueId,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
            };
        }
    }
}
