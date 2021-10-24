using Eice.Payment.API.Command.Oferta;
using Eice.Payment.API.DTO;
using Eice.Payment.API.Notification;
using Eice.Payment.API.Query.Oferta;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eice.Payment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfertaController : CustomBaseController
    {
        public OfertaController(IMediator mediator, INotificationHandler<ExceptionNotification> notifications) : base(mediator, notifications)
        {
        }

        //pegar na auth PartnerId, AuthenticationKey
        [HttpPost]
        public async Task<IActionResult> CreateOferta([FromBody] OfertaCreateCommand command)
        {
            var response = await _mediator.Send(command);
            return await ResponseAsync(Ok(new ResponseDto<string>() { Success = true, Data = response }));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOferta()
        {
            var response = await _mediator.Send(new OfertaGetAllQuery());
            return await ResponseAsync(Ok(new ResponseDto<IEnumerable<object>>() { Success = true, Data = response }));
        }

    }
}
