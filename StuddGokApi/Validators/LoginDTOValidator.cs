using FluentValidation;
using StuddGokApi.DTOs;

namespace StuddGokApi.Validators;

public class LoginDTOValidator : AbstractValidator<LoginDTO>
{
    public LoginDTOValidator()
    {
        RuleFor(x => x.GokstadEmail)
                .NotEmpty().WithMessage("Emailadresse må være med.")
                .EmailAddress().WithMessage("Må ha en gyldig e-mail adresse.");

        RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Passord må være med.");

    }
}
