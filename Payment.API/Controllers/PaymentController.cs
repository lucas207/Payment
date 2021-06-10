using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Payment.API.Commands;
using Payment.API.Model;
using Payment.API.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly ILogger<PaymentController> _logger;
        private readonly IMediator _mediator;
        public PaymentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<PaymentModel>> GetProdutos(int id)
        {
            try
            {
                _logger.LogInformation($"Veeash id: {id}");
                //validation
                var command = new GetPaymentByIdQuery { Id = id };
                var response = await _mediator.Send(command);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Poota que paril, vc Errrou! {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<PaymentModel>> CreatePayment([FromBody] PaymentCreateCommand command)
        {
            try
            {
                //validate
                _logger.LogInformation($"Veeash command: {command}");
                var response = await _mediator.Send(command);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Poota que paril, vc Errrou! {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
    }
}
