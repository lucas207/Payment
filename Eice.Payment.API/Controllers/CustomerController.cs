using Eice.Payment.API.Request;
using Eice.Payment.API.Response;
using Eice.Payment.Domain.Customer.Commands;
using Eice.Payment.Domain.Customer.Queries;
using Eice.Payment.Domain.Notification;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eice.Payment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomerController : CustomBaseController
    {
        public CustomerController(IMediator mediator, INotificationHandler<ExceptionNotification> notifications) : base(mediator, notifications)
        {
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CustomerCreateRequest request)
        {
            var idPartner = User.Claims.Where(x => x.Type == "id").FirstOrDefault().Value;

            var response = await _mediator.Send(new CustomerCreateCommand
            {
                Cpf = request.Cpf,
                Name = request.Name,
                PartnerId = idPartner
            });
            return ResponseHandle(Ok(new ResponseDto<string>() { Success = true, Data = response }));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomer()
        {
            var idPartner = User.Claims.Where(x => x.Type == "id").FirstOrDefault().Value;

            var response = await _mediator.Send(new CustomerGetAllQuery
            {
                PartnerId = idPartner
            });
            return ResponseHandle(Ok(new ResponseDto<IEnumerable<CustomerDto>>() { Success = true, Data = response }));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer(string id)
        {
            var response = await _mediator.Send(new CustomerGetByIdQuery
            {
                Id = id
            });
            return ResponseHandle(Ok(new ResponseDto<CustomerDetailDto>() { Success = true, Data = response }));
        }

        [HttpPut]
        public async Task<IActionResult> EditCustomer([FromBody] CustomerEditRequest request)
        {
            var idPartner = User.Claims.Where(x => x.Type == "id").FirstOrDefault().Value;

            var response = await _mediator.Send(new CustomerEditCommand
            {
                Id = request.Id,
                Name = request.Name,
                PartnerId = idPartner
            });
            return ResponseHandle(Ok(new ResponseDto<bool>() { Success = true, Data = response }));
        }
    }
}
