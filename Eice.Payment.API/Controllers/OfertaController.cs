using Eice.Payment.API.Request;
using Eice.Payment.API.Response;
using Eice.Payment.Domain.Notification;
using Eice.Payment.Domain.Oferta;
using Eice.Payment.Domain.Oferta.Commands;
using Eice.Payment.Domain.Oferta.Queries;
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

        [HttpGet("AvailableCoins/{customerId}")]
        public async Task<IActionResult> MoedasDisponiveis(string customerId)
        {
            var EnableExchanges = bool.Parse(User.Claims.Where(x => x.Type == "EnableExchanges").FirstOrDefault().Value);
            //verificar se o parter authenticado liberou negociações
            if (!EnableExchanges)
                return BadRequest();

            var response = await _mediator.Send(new GetCoinsToTradeQuery
            {
                CustomerId = customerId
            });
            return ResponseHandle(Ok(new ResponseDto<IEnumerable<CoinDto>>() { Success = true, Data = response }));
        }

        [HttpPost]
        public async Task<IActionResult> CreateOferta([FromBody] OfertaCreateRequest request)
        {
            var idPartner = User.Claims.Where(x => x.Type == "id").FirstOrDefault().Value;
            var EnableExchanges = bool.Parse(User.Claims.Where(x => x.Type == "EnableExchanges").FirstOrDefault().Value);
            //verificar se o parter authenticado liberou negociações
            if (!EnableExchanges)
                return BadRequest();

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

        [Obsolete("teste")]
        [HttpGet]
        public async Task<IActionResult> GetAllOferta()
        {
            var response = await _mediator.Send(new OfertaGetAllQuery());
            return ResponseHandle(Ok(new ResponseDto<IEnumerable<OfertaDto>>() { Success = true, Data = response }));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOfertaById(string id)
        {
            return Ok();
        }

        [HttpGet("Disponiveis/{customerId}")]
        public async Task<IActionResult> GetOfertasDisponiveis(string customerId)
        {
            var idPartner = User.Claims.Where(x => x.Type == "id").FirstOrDefault().Value;
            var EnableExchanges = bool.Parse(User.Claims.Where(x => x.Type == "EnableExchanges").FirstOrDefault().Value);
            //verificar se o parter authenticado liberou negociações
            if (!EnableExchanges)
                return BadRequest();

            var response = await _mediator.Send(new OfertaGetAvailableByCustomerQuery
            {
                PartnerId = idPartner,
                CustomerId = customerId
            });
            return ResponseHandle(Ok(new ResponseDto<IEnumerable<OfertaDto>>() { Success = true, Data = response }));
        }

        [HttpGet("Customer/{customerId}")]
        public async Task<IActionResult> GetMinhasOfertas(string customerId, EStatusOferta? status = null)
        {
            var response = await _mediator.Send(new OfertaGetByCustomerQuery { CustomerId = customerId, Status = status });
            return ResponseHandle(Ok(new ResponseDto<IEnumerable<OfertaDto>>() { Success = true, Data = response }));
        }

        //Aceitar oferta X
        [HttpPost("AceitarOferta")]
        public async Task<IActionResult> AceitarOferta([FromBody] OfertaAceitarRequest request)
        {
            var idPartner = User.Claims.Where(x => x.Type == "id").FirstOrDefault().Value;
            var EnableExchanges = bool.Parse(User.Claims.Where(x => x.Type == "EnableExchanges").FirstOrDefault().Value);
            //verificar se o parter authenticado liberou negociações
            if (!EnableExchanges)
                return BadRequest();

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
