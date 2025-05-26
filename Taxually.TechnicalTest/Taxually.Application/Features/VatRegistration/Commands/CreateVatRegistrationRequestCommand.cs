using MediatR;

namespace Taxually.Application.Features.VatRegistration.Commands
{
    public class CreateVatRegistrationRequestCommand : IRequest<Unit>
    {
        public string CompanyName { get; set; }
        public string CompanyId { get; set; }
        public string Country { get; set; }
    }
}
