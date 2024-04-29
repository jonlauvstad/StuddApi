using FluentValidation;
using StuddGokApi.DTOs;

namespace StuddGokApi.Validators;

public class LectureDTOValidator : AbstractValidator<LectureDTO>
{
    public LectureDTOValidator()
    {
        RuleFor(x => x.CourseImplementationId)
                .NotEmpty().WithMessage("KursgjennomføringsId må være med.");
        RuleFor(x => x.StartTime)
                .NotEmpty().WithMessage("StartTidspunkt må være med.");
        RuleFor(x => x.EndTime)
                .NotEmpty().WithMessage("SluttTidspunkt må være med.");
    }
}
