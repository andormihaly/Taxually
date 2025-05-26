using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Xml.Serialization;
using Taxually.Application.Features.VatRegistration.Commands;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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

            return Ok();
        }
    }

}
