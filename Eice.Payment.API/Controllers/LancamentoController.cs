using Eice.Payment.API.Request;
using Eice.Payment.API.Response;
using Eice.Payment.Domain.Lancamento.Commands;
using Eice.Payment.Domain.Lancamento.Queries;
using Eice.Payment.Domain.Notification;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Eice.Payment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LancamentoController : CustomBaseController
    {
        public LancamentoController(IMediator mediator, INotificationHandler<ExceptionNotification> notifications) : base(mediator, notifications)
        {
        }

        [HttpPost]
        public async Task<IActionResult> CreateLancamento([FromBody] LancamentoCreateRequest request)
        {
            var idPartner = User.Claims.Where(x => x.Type == "id").FirstOrDefault().Value;

            var response = await _mediator.Send(new LancamentoCreateCommand
            {
                PartnerId = idPartner,
                CustomerId = request.CustomerId,
                Description = request.Description,
                Quantity = request.Quantity
            });
            return ResponseHandle(Ok(new ResponseDto<bool>() { Success = true, Data = response }));
        }

        //Autenticar por partner
        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetAllLancamento(string customerId)
        {
            var response = await _mediator.Send(new LancamentoGetAllQuery
            {
                CustomerId = customerId
            });
            return ResponseHandle(Ok(new ResponseDto<LancamentoDto>() { Success = true, Data = response }));
        }
    }
}
