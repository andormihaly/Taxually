using AutoMapper;
using MediatR;
using Taxually.Application.Exceptions;
using Taxually.Application.Persistence.TaxRequest;


namespace Taxually.Application.Features.VatRegistration.Commands
{
    public class CreateVatRegistrationRequestCommandHandler : IRequestHandler<CreateVatRegistrationRequestCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly ITaxRequestManager _taxRequestManager;

        public CreateVatRegistrationRequestCommandHandler(IMapper mapper, ITaxRequestManager taxRequestManager)
        {
            _mapper = mapper;
            _taxRequestManager = taxRequestManager;
        }

        public async Task<Unit> Handle(CreateVatRegistrationRequestCommand request, CancellationToken token)
        {
            var validator = new CreateVatRegistrationCommandValidator();
            
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {

                throw new BadRequestException("Invalid Tax Request", validationResult);
            }

            var registrationRequest = _mapper.Map<Domain.VatRegistrationRequest>(request);

            await _taxRequestManager.ResolveRequest(registrationRequest);

            return Unit.Value;
        }
    }
}
