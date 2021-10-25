using Eice.Payment.API.Notification;
using Eice.Payment.Domain;
using Eice.Payment.Domain.Oferta;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Eice.Payment.API.Command.Oferta
{
    public class OfertaCreateCommandHandler : CommandHandler<OfertaEntity>, IRequestHandler<OfertaCreateCommand, string>
    {
        public OfertaCreateCommandHandler(IMediator bus, IOfertaCommandRepository commandRepository) : base(bus, commandRepository)
        {
        }

        public async Task<string> Handle(OfertaCreateCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) { GetNotificationsErrors(request); return default; }

            try
            {
                //Validar request.PartnerId existe
                //Validar request.AuthenticationKey pertence ao PartnerId
                //Validar CustomerIdCreated tem conta nas 2 partners
                //Validar saldo do CustomerIdCreated

                //metodo to map
                OfertaEntity entity = new()
                {
                    CustomerIdCreated = request.CustomerIdCreated,
                    QuantityOffer = request.QuantityOffer,
                    QuantityReceive = request.QuantityReceive,
                    CoinIdReceive = request.CoinIdReceive,
                    Status = EStatusOffer.Open
                };

                await _commandRepository.Create(entity);

                return entity.Id.ToString();
            }
            catch (Exception ex)
            {
                await _bus.Publish(new ExceptionNotification("500", ex.Message, null, ex.StackTrace), cancellationToken);
                return default;
            }
        }
    }
}
