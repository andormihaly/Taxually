using MediatR;

namespace Taxually.Application.Features.VatRegistration.Commands
{
    public class CreateVatRegistrationRequestCommand : IRequest<Unit>
    {
        public string CompanyName { get; set; } = string.Empty;
        public string CompanyId { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
    }
}
