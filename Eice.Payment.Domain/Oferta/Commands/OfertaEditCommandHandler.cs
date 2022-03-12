using Eice.Payment.Domain.Customer;
using Eice.Payment.Domain.Customer.Queries;
using Eice.Payment.Domain.Lancamento.Commands;
using Eice.Payment.Domain.Notification;
using Eice.Payment.Domain.Oferta.Queries;
using Eice.Payment.Domain.Partner.Queries;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Eice.Payment.Domain.Oferta.Commands
{
    //Aceitar Oferta
    public class OfertaEditCommandHandler : CommandHandler<OfertaEntity>, IRequestHandler<OfertaEditCommand, bool>
    {
        //private IOfertaCommandRepository _ofertaCommandRepository;
        private readonly IOfertaQueryRepository _ofertaQueryRepository;
        private readonly IPartnerQueryRepository _partnerQueryRepository;
        private readonly ICustomerQueryRepository _customerQueryRepository;

        public OfertaEditCommandHandler(IMediator bus, IOfertaCommandRepository commandRepository,
            IOfertaQueryRepository ofertaQueryRepository,
            IPartnerQueryRepository partnerQueryRepository,
            ICustomerQueryRepository customerQueryRepository) : base(bus, commandRepository)
        {
            _ofertaQueryRepository = ofertaQueryRepository;
            _partnerQueryRepository = partnerQueryRepository;
            _customerQueryRepository = customerQueryRepository;
        }

        public async Task<bool> Handle(OfertaEditCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) { GetNotificationsErrors(request); return default; }
            try
            {
                //Validar request.PartnerId existe
                var partner = await _partnerQueryRepository.Get(request.PartnerId);
                if (partner is null)
                    throw new Exception("Parceiro não encontrado");

                //Validar se oferta existe e se está aberta
                var oferta = await _ofertaQueryRepository.Get(request.OfertaId);
                if (oferta is null || oferta.Status != EStatusOferta.Open)
                    throw new Exception("Oferta inválida");

                //Validar se o customer existe e é desse partner
                var customer = await _customerQueryRepository.Get(request.CustomerIdAccepted);
                if (customer is null || customer.PartnerId != request.PartnerId)
                    throw new Exception("Cliente não encontrado");

                //Validar CustomerIdCreated tem conta nas 2 partners
                var minhaOutraConta = _customerQueryRepository.GetAllFromPartnerId(oferta.CoinOffer.Id.ToString())
                    .Where(x => x.Cpf == customer.Cpf).FirstOrDefault();
                if (minhaOutraConta is null)
                    throw new Exception("Cliente não está apto a negociar esta moeda");

                //Validar saldo do CustomerIdCreated
                if (customer.SaldoAtual < oferta.QuantityReceive)
                    throw new Exception("Saldo insuficiente");

                await TransferirRecursosAsync(customer, oferta, minhaOutraConta);

                oferta.CustomerAccepted = new()
                {
                    Id = customer.Id,
                    Name = customer.Name
                };
                oferta.Status = EStatusOferta.Executed;


                return await _commandRepository.Update(oferta.Id, oferta);
            }
            catch (Exception ex)
            {
                await _bus.Publish(new ExceptionNotification("031", ex.Message, null), cancellationToken);
                return default;
            }
        }

        private async Task TransferirRecursosAsync(CustomerEntity customerAccepted, OfertaEntity oferta, CustomerEntity minhaOutraConta)
        {
            //customer criou -genial e +xp
            //transferir a coin offer
            //tira do cara
            await _bus.Send(new LancamentoCreateCommand
            {
                CustomerId = oferta.CustomerCreated.Id.ToString(),
                PartnerId = oferta.CoinOffer.Id.ToString(),
                Quantity = -oferta.QuantityOffer,
                Description = $"Trocado por {oferta.QuantityReceive} {oferta.CoinReceive.Name}"
            });

            //identificar minha outra conta
            //var minhaOutraConta = _customerQueryRepository.GetAllFromPartnerId(oferta.CoinReceive.Id.ToString())
            //        .Where(x => x.Cpf == customerAccepted.Cpf).FirstOrDefault();

            //coloca pra mim
            await _bus.Send(new LancamentoCreateCommand
            {
                CustomerId = minhaOutraConta.Id.ToString(),
                PartnerId = oferta.CoinOffer.Id.ToString(),
                Quantity = oferta.QuantityOffer,
                Description = $"Trocado por {oferta.QuantityReceive} {oferta.CoinReceive.Name}"
            });

            //customer aceitou +genial e -xp
            //transferir a coin receive
            //tira de mim
            await _bus.Send(new LancamentoCreateCommand
            {
                CustomerId = customerAccepted.Id.ToString(),
                PartnerId = oferta.CoinReceive.Id.ToString(),
                Quantity = -oferta.QuantityReceive,
                Description = $"Trocado por {oferta.QuantityOffer} {oferta.CoinOffer.Name}"
            });
            //identificar a outra conta do cara
            var contaDoCara = await _customerQueryRepository.Get(oferta.CustomerCreated.Id);
            var outraContaDoCara = _customerQueryRepository.GetAllFromPartnerId(oferta.CoinReceive.Id.ToString())
                    .Where(x => x.Cpf == contaDoCara.Cpf).FirstOrDefault();
            //coloca pro cara
            await _bus.Send(new LancamentoCreateCommand
            {
                CustomerId = outraContaDoCara.Id.ToString(),//identificar a outra conta pelo cpf
                PartnerId = oferta.CoinReceive.Id.ToString(),
                Quantity = oferta.QuantityReceive,
                Description = $"Trocado por {oferta.QuantityOffer} {oferta.CoinOffer.Name}"
            });
        }
    }
}
