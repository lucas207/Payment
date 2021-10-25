using Eice.Payment.API.Command.Oferta;
using Eice.Payment.API.DTO;
using Eice.Payment.API.Notification;
using Eice.Payment.API.Query.Oferta;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
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
            return await ResponseAsync(Ok(new ResponseDto<IEnumerable<OfertaDto>>() { Success = true, Data = response }));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOfertaById(string id)
        {
            throw new NotImplementedException();
        }
        //retornar moedas(Partner) disponiveis para negocição, por cliente, para criar uma oferta

        [HttpGet("GetOfertasDisponiveis/{id}")]
        //[Route("oi/{ido}")]
        public async Task<IActionResult> GetOfertasDisponiveis(string id)
        {
            return Ok();
        }

        [HttpGet("GetMinhasOfertas/{id}")]
        public async Task<IActionResult> GetMinhasOfertas(string id)
        {
            //ofertas abertas e executadas
            return await ResponseAsync(Ok());
        }
    }
}
