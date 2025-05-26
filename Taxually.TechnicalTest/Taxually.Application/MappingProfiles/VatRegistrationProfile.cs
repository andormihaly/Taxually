using AutoMapper;
using Taxually.Application.Features.VatRegistration.Commands;
using Taxually.Domain;

namespace Taxually.Application.MappingProfiles
{
    public class VatRegistrationProfile : Profile
    {
        public VatRegistrationProfile()
        {
            CreateMap<CreateVatRegistrationRequestCommand, VatRegistrationRequest>().ReverseMap();
        }
    }
}
