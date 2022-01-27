using Eice.Payment.API.Command.Oferta;
using Eice.Payment.API.DTO;
using Eice.Payment.API.Notification;
using Eice.Payment.API.Query.Oferta;
using Eice.Payment.API.Request;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eice.Payment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OfertaController : CustomBaseController
    {
        public OfertaController(IMediator mediator, INotificationHandler<ExceptionNotification> notifications) : base(mediator, notifications)
        {
        }

        //pegar na auth PartnerId, AuthenticationKey
        [HttpPost]
        public async Task<IActionResult> CreateOferta([FromBody] OfertaCreateRequest request)
        {
            var idPartner = User.Claims.Where(x => x.Type == "id").FirstOrDefault().Value;

            var response = await _mediator.Send(new OfertaCreateCommand
            {
                PartnerId = idPartner,
                CustomerIdCreated = request.CustomerIdCreated,
                QuantityOffer = request.QuantityOffer,
                QuantityReceive = request.QuantityReceive,
                CoinIdReceive = request.CoinIdReceive,
            });
            return ResponseHandle(Ok(new ResponseDto<string>() { Success = true, Data = response }));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOferta()
        {
            var response = await _mediator.Send(new OfertaGetAllQuery());
            return ResponseHandle(Ok(new ResponseDto<IEnumerable<OfertaDto>>() { Success = true, Data = response }));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOfertaById(string id)
        {
            throw new NotImplementedException();
        }
        //retornar moedas(Partner) disponiveis para negocição, por cliente, para criar uma oferta

        [HttpGet("GetOfertasDisponiveis/{id}")]
        public async Task<IActionResult> GetOfertasDisponiveis(string id)
        {
            return Ok();
        }

        [HttpGet("GetMinhasOfertas/{id}")]
        public async Task<IActionResult> GetMinhasOfertas(string id)
        {
            //ofertas abertas e executadas
            return ResponseHandle(Ok());
        }

        //Aceitar oferta X
        [HttpPost("AceitarOferta")]
        public async Task<IActionResult> AceitarOferta([FromBody] OfertaAceitarRequest request)
        {
            var idPartner = User.Claims.Where(x => x.Type == "id").FirstOrDefault().Value;

            var response = await _mediator.Send(new OfertaEditCommand
            {
                PartnerId = idPartner,
                CustomerIdAccepted = request.CustomerIdAccepted,
                OfertaId = request.OfertaId
            });
            return ResponseHandle(Ok(new ResponseDto<bool>() { Success = true, Data = response }));
        }

        //Cancelar oferta X
    }
}
