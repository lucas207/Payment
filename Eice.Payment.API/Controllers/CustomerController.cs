using Eice.Payment.API.Command;
using Eice.Payment.API.DTO;
using Eice.Payment.API.Notification;
using Eice.Payment.API.Query.Customer;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eice.Payment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : CustomBaseController
    {
        public CustomerController(IMediator mediator, INotificationHandler<ExceptionNotification> notifications) : base(mediator, notifications)
        {
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
            var response = await _mediator.Send(new CustomerGetAllQuery());
            return await ResponseAsync(Ok(new ResponseDto<IEnumerable<CustomerDto>>() { Success = true, Data = response }));
        }
    }
}
