using Eice.Payment.API.Command.Lancamento;
using Eice.Payment.API.DTO;
using Eice.Payment.API.Notification;
using Eice.Payment.API.Query.Lancamento;
using MediatR;
using Microsoft.AspNetCore.Mvc;
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

        //Autenticar por partner
        [HttpPost]
        public async Task<IActionResult> CreateLancamento([FromBody] LancamentoCreateCommand command)
        {
            var response = await _mediator.Send(command);
            return await ResponseAsync(Ok(new ResponseDto<bool>() { Success = true, Data = response }));
        }

        //Autenticar por partner
        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetAllLancamento(string customerId)
        {
            var response = await _mediator.Send(new LancamentoGetAllQuery
            {
                CustomerId = customerId
            });
            return await ResponseAsync(Ok(new ResponseDto<LancamentoDto>() { Success = true, Data = response }));
        }
    }
}
