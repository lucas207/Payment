using Eice.Payment.API.Command;
using Eice.Payment.API.DTO;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eice.Payment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly ILogger<ClientController> _logger;
        private readonly IMediator _mediator;

        public ClientController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<ClientDto>> CreateClient([FromBody] ClientCreateCommand command)
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
