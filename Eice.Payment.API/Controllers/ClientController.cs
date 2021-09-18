using Eice.Payment.API.Command;
using Eice.Payment.API.DTO;
using Eice.Payment.API.Notification;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eice.Payment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : CustomBaseController
    {
        //private readonly ILogger<ClientController> _logger;
        private readonly IMediator _mediator;

        public ClientController(IMediator mediator, INotificationHandler<ExceptionNotification> notifications) : base(notifications)
        {
            _mediator = mediator;
            //_logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateClient([FromBody] ClientCreateCommand command)
        {
            var response = await _mediator.Send(command);
            return await ResponseAsync(Ok(new ResponseDto<string>() { Success = true, Data = response }));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllClient()
        {
            var response = await _mediator.Send(new ClientGetAllCommand());
            return await ResponseAsync(Ok(new ResponseDto<IEnumerable<ClientDto>>() { Success = true, Data = response }));
        }
    }
}
