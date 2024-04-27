using FluentValidation;
using StuddGokApi.DTOs;

namespace StuddGokApi.Validators;

public class ExamImplementationDTOValidator :AbstractValidator<ExamImplementationDTO>
{
    public ExamImplementationDTOValidator()
    {
        RuleFor(x => x.ExamId)
                .NotEmpty().WithMessage("EksamensId må være med.");
        RuleFor(x => x.VenueId)
                .NotEmpty().WithMessage("RomId må være med.");
        RuleFor(x => x.StartTime)
                .NotEmpty().WithMessage("StartTidspunkt må være med.");
        RuleFor(x => x.EndTime)
                .NotEmpty().WithMessage("SluttTidspunkt må være med.");

    }
}
