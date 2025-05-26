using MediatR;
using Microsoft.AspNetCore.Mvc;
using Taxually.Application.Features.VatRegistration.Commands;

namespace Taxually.TechnicalTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VatRegistrationController : ControllerBase
    {
        private readonly IMediator _mediator;
        public VatRegistrationController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Registers a company for a VAT number in a given country
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateVatRegistrationRequestCommand request)
        {
            var response = await _mediator.Send(request);

            return NoContent();
        }
    }

}
