using Eice.Payment.API.Response;
using Eice.Payment.Domain.Notification;
using Eice.Payment.Domain.Partner.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eice.Payment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartnerController : CustomBaseController
    {
        public PartnerController(IMediator mediator, INotificationHandler<ExceptionNotification> notifications) : base(mediator, notifications)
        {
        }

        [HttpPost("Authenticate"), AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody] PartnerAuthenticateQuery partnerAuthenticateQuery)
        {
            var response = await _mediator.Send(partnerAuthenticateQuery);
            return ResponseHandle(Ok(new ResponseDto<string>() { Success = true, Data = response }));
        }

        [HttpGet("GetAll")]
        [Authorize]
        public async Task<IActionResult> GetAllEnablePartner()
        {
            var aa = User.Identity.Name;
            var response = await _mediator.Send(new PartnerGetAllQuery());
            return ResponseHandle(Ok(new ResponseDto<IEnumerable<PartnerDto>>() { Success = true, Data = response }));
        }
        
        //Obter Total Moedas fornecidas

        //Taxa da proporção do valor da moeda em relação aos outros partners
    }
}
