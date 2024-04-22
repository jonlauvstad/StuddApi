using FluentValidation;
using StuddGokApi.DTOs;

namespace StuddGokApi.Validators;

public class ExamGroupDTOValidator : AbstractValidator<ExamGroupDTO>
{
    public ExamGroupDTOValidator()
    {
        RuleFor(x => x.ExamId)
            .NotEmpty().WithMessage("EksamensGruppe-oppføringen må ha en 'ExamId'.");

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("EksamensGruppen-oppføringen må ha en 'UserId'.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("EksamensGruppen-oppføringen må ha et navn.");
    }
}
