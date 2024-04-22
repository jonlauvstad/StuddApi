using FluentValidation;
using StuddGokApi.DTOs;

namespace StuddGokApi.Validators;

public class ExamDTOValidator : AbstractValidator<ExamDTO>
{
    public ExamDTOValidator()
    {
        RuleFor(x => x.CourseImplementationId)
            .NotEmpty().WithMessage("Eksamenen må ha en 'CourseImplementationId'.");

        RuleFor(x => x.Category)
            .NotEmpty().WithMessage("Eksamenen må ha en 'Category'.");

        RuleFor(x => x.DurationHours)
            .NotEmpty().WithMessage("Eksamenen må ha 'DurationHours' oppgitt som 'float'.");

        RuleFor(x => x.PeriodStart)
            .NotEmpty().WithMessage("Eksamenen må ha et starttidspunkt.");

        RuleFor(x => x.PeriodEnd)
            .NotEmpty().WithMessage("Eksamenen må ha et slutttidspunkt.");
    }
}
