using Eice.Payment.API.Notification;
using Eice.Payment.Domain.Customer;
using Eice.Payment.Domain.Oferta;
using Eice.Payment.Domain.Partner;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Eice.Payment.API.Command.Oferta
{
    public class OfertaCreateCommandHandler : CommandHandler<OfertaEntity>, IRequestHandler<OfertaCreateCommand, string>
    {
        private readonly IPartnerQueryRepository _partnerQueryRepository;
        private readonly ICustomerQueryRepository _customerQueryRepository;

        public OfertaCreateCommandHandler(IMediator bus,
            IOfertaCommandRepository commandRepository,
            IPartnerQueryRepository partnerQueryRepository,
            ICustomerQueryRepository customerQueryRepository) : base(bus, commandRepository)
        {
            _partnerQueryRepository = partnerQueryRepository;
            _customerQueryRepository = customerQueryRepository;
        }

        public async Task<string> Handle(OfertaCreateCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) { GetNotificationsErrors(request); return default; }

            try
            {
                //Validar request.PartnerId existe
                var partner = await _partnerQueryRepository.Get(request.PartnerId);
                if (partner is null)
                    throw new Exception("Parceiro não encontrado");

                //Validar se o customer existe e é desse partner
                var customer = await _customerQueryRepository.Get(request.CustomerIdCreated);
                if (customer is null || customer.PartnerId != request.PartnerId)
                    throw new Exception("Cliente não encontrado");

                //Validar CustomerIdCreated tem conta nas 2 partners
                var clienteExisteOutroPartner = _customerQueryRepository.GetAllFromPartnerId(request.CoinIdReceive)
                    .Any(x => x.Cpf == customer.Cpf);
                if (!clienteExisteOutroPartner)
                    throw new Exception("Cliente não está apto a negociar esta moeda");

                //Validar saldo do CustomerIdCreated
                if (customer.SaldoAtual < request.QuantityOffer)
                    throw new Exception("Saldo insuficiente");

                var coinWished = await _partnerQueryRepository.Get(request.CoinIdReceive);

                //metodo to map
                OfertaEntity entity = new()
                {
                    CustomerCreated = new()
                    {
                        Id = customer.Id,
                        Name = customer.Name
                    },
                    QuantityOffer = request.QuantityOffer,
                    CoinOffer = new()
                    {
                        Id = partner.Id,
                        Name = partner.CoinName
                    },
                    QuantityReceive = request.QuantityReceive,
                    CoinReceive = new()
                    {
                        Id = coinWished.Id,
                        Name = coinWished.CoinName
                    },
                    Status = EStatusOferta.Open
                };

                await _commandRepository.Create(entity);

                return entity.Id.ToString();
            }
            catch (Exception ex)
            {
                await _bus.Publish(new ExceptionNotification("030", ex.Message, null, ex.StackTrace), cancellationToken);
                return default;
            }
        }

    }
}
