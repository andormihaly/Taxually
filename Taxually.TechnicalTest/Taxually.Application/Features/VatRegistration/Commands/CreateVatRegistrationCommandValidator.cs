using FluentValidation;

namespace Taxually.Application.Features.VatRegistration.Commands
{
    public class CreateVatRegistrationCommandValidator : AbstractValidator<CreateVatRegistrationRequestCommand>
    {
        private List<string> supportedCountries=new List<string>() { "GB", "FR", "DE"};
    
        public CreateVatRegistrationCommandValidator()
        {
            RuleFor(request => request.CompanyId)
            .NotEmpty().WithMessage("CompanyId is required")
            .NotNull().WithMessage("CompanyId is required");

            RuleFor(request => request.CompanyName)
            .NotEmpty().WithMessage("CompanyName is required")
            .NotNull().WithMessage("CompanyName is required");


            RuleFor(request => request.Country)
                .Must(x => supportedCountries.Contains(x))
                .WithMessage("Currently supported countries : " + String.Join(", ", supportedCountries));
        }
    }
}

