using Eice.Payment.API.DTO;
using Eice.Payment.API.Notification;
using Eice.Payment.API.Query.Partner;
using MediatR;
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

        [HttpGet]
        public async Task<IActionResult> GetAllPartner()
        {
            var response = await _mediator.Send(new PartnerGetAllQuery());
            return await ResponseAsync(Ok(new ResponseDto<IEnumerable<PartnerDto>>() { Success = true, Data = response }));
        }
    }
}
