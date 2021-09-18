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
    public class CustomerController : CustomBaseController
    {
        //private readonly ILogger<ClientController> _logger;
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator, INotificationHandler<ExceptionNotification> notifications) : base(notifications)
        {
            _mediator = mediator;
            //_logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CustomerCreateCommand command)
        {
            var response = await _mediator.Send(command);
            return await ResponseAsync(Ok(new ResponseDto<string>() { Success = true, Data = response }));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomer()
        {
            var response = await _mediator.Send(new CustomerGetAllCommand());
            return await ResponseAsync(Ok(new ResponseDto<IEnumerable<CustomerDto>>() { Success = true, Data = response }));
        }
    }
}
