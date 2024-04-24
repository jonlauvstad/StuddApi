using FluentValidation;
using StuddGokApi.DTOs;

namespace StuddGokApi.Validators
{
    public class VenueDTOValidator : AbstractValidator<VenueDTO>
    {
        public VenueDTOValidator() 
        {
            RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Lokalet må ha et navn.");

            RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Lokalet må ha en beskrivelse.");

            RuleFor(x => x.LocationId)
            .NotEmpty().WithMessage("Lokalet må ha en steds-id.");

            RuleFor(x => x.StreetAddress)
            .NotEmpty().WithMessage("Lokalet må ha en gateadresse.");

            RuleFor(x => x.PostCode)
            .NotEmpty().WithMessage("Lokalet må ha et postnummer.");

            RuleFor(x => x.City)
            .NotEmpty().WithMessage("Lokalet må ha et poststed.");

            RuleFor(x => x.Capacity)
            .NotEmpty().WithMessage("Det må oppgis en kapasitet for lokalet.");
        }
    }
}
