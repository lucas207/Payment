using Eice.Payment.Domain.Customer;
using Eice.Payment.Domain.Customer.Queries;
using Eice.Payment.Domain.Notification;
using Eice.Payment.Domain.Partner.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Eice.Payment.Domain.Oferta.Queries
{
    internal class OfertaGetAvailableByCustomerQueryHandler : QueryHandler<OfertaEntity>, IRequestHandler<OfertaGetAvailableByCustomerQuery, IEnumerable<OfertaDto>>
    {
        private readonly IOfertaQueryRepository _ofertaQueryRepository;
        private readonly IPartnerQueryRepository _partnerRepository;
        private readonly ICustomerQueryRepository _customerQueryRepository;

        public OfertaGetAvailableByCustomerQueryHandler(IMediator bus, IOfertaQueryRepository queryRepository
            , IPartnerQueryRepository partnerQueryRepository, ICustomerQueryRepository customerQueryRepository) : base(bus, queryRepository)
        {
            _ofertaQueryRepository = queryRepository;
            _partnerRepository = partnerQueryRepository;
            _customerQueryRepository = customerQueryRepository;
        }

        public async Task<IEnumerable<OfertaDto>> Handle(OfertaGetAvailableByCustomerQuery request, CancellationToken cancellationToken)
        {
            try
            {
                IList<OfertaDto> resp = new List<OfertaDto>();
                //filter por habilitou negociações
                //var list = await _partnerRepository.GetAllEnableExchange();

                var customer = await _customerQueryRepository.Get(request.CustomerId);
                ////Obter contas do customer de todos partners
                //var contas = _customerQueryRepository.GetAllByCpf(customer.Cpf).Select(x => x.PartnerId).ToList();
                ////filtro partners habilitados e cliente tem conta
                //var partners = list.Where(x => contas.Contains(x.Id.ToString()));

                //validar customer tem conta na outra partner
                var coinsAvailable = await _bus.Send(new GetCoinsToTradeQuery
                {
                    CustomerId = request.CustomerId
                }, cancellationToken);
                var idCoinsAvailable = coinsAvailable.Select(x => x.Id);

                IEnumerable<OfertaEntity> ofertasEmAberto = await ObterOfertasEmAberto();

                IEnumerable<OfertaEntity> ofertasAvailableDoador = ObterOfertasValidasDoador(request, customer, idCoinsAvailable, ofertasEmAberto);

                //IEnumerable<OfertaEntity> ofertasAvailableReceptor = ObterOfertasValidasReceptor(request, customer, ofertasEmAberto);

                //IEnumerable<OfertaEntity> ofertasAvailable = ofertasAvailableDoador.Concat(ofertasAvailableReceptor);
                IEnumerable<OfertaEntity> ofertasAvailable = ofertasAvailableDoador;

                //validaçoes receptor
                //var opa2 = opa.Where(x => contas)


                //validar saldo das outras contas

                //obter moedas to trade
                //obter ofertas dessa moeda, para uma que o cliente tem conta
                //customer logado na G, só opera moeda G
                //customer logado na G, pode aceitar uma oferta doando moedax por G
                //customer doando G por outra moeda

                foreach (var entity in ofertasAvailable)
                {
                    resp.Add(new OfertaDto
                    {
                        Id = entity.Id.ToString(),
                        CoinIdOffer = entity.CoinOffer.Id.ToString(),
                        CoinNameOffer = entity.CoinOffer.Name,
                        CoinIdReceive = entity.CoinReceive.Id.ToString(),
                        CoinNameReceive = entity.CoinReceive.Name,
                        QuantityOffer = entity.QuantityOffer,
                        QuantityReceive = entity.QuantityReceive,
                        Status = entity.Status,
                        CreationTime = entity.Id.CreationTime
                    });
                }

                return resp;
            }
            catch (Exception ex)
            {
                await _bus.Publish(new ExceptionNotification("038", ex.Message, null), cancellationToken);
                return default;
            }
        }

        private IEnumerable<OfertaEntity> ObterOfertasValidasReceptor(OfertaGetAvailableByCustomerQuery request, CustomerEntity customer, IEnumerable<OfertaEntity> ofertasEmAberto)
        {
            //FALTA VALIDAR O EXCHANGE
            //validaçoes receptor
            var ofertasAvailable = new List<OfertaEntity>();

            //validar se moeda ofertada é a do partner logado
            var ofertasPartner = ofertasEmAberto.Where(x => x.CoinOffer.Id.ToString() == request.PartnerId /*|| x.CoinReceive.Id.ToString() == request.PartnerId*/);

            //obter saldo da outra partner e validar
            var contas = _customerQueryRepository.GetAllByCpf(customer.Cpf);

            foreach (var item in ofertasPartner)
            {
                //customer tem conta e tem saldo
                if (contas.Any(x => x.PartnerId == item.CoinReceive.Id.ToString()
                    && x.SaldoAtual >= item.QuantityReceive))
                {
                    ofertasAvailable.Add(item);
                }
            }
            return ofertasAvailable;
        }

        private static IEnumerable<OfertaEntity> ObterOfertasValidasDoador(OfertaGetAvailableByCustomerQuery request, CustomerEntity customer, IEnumerable<string> idCoinsAvailable, IEnumerable<OfertaEntity> ofertasEmAberto)
        {
            //validaçoes doador
            //validar se customer pode aceitar moeda ofertada
            var ofertasClienteTemConta = ofertasEmAberto.Where(x => idCoinsAvailable.Contains(x.CoinOffer.Id.ToString())/* || idCoinsAvailable.Contains(x.CoinReceive.Id.ToString())*/);

            //validar ofertas do partner logado
            //fazer primeiro só doando G, se o cara que criou a oferta recebe G
            var ofertasPartner = ofertasClienteTemConta.Where(x => /*x.CoinOffer.Id.ToString() == request.PartnerId ||*/ x.CoinReceive.Id.ToString() == request.PartnerId);


            //validar saldo dessa conta
            var ofertasAvailable = ofertasPartner.Where(x => customer.SaldoAtual >= x.QuantityReceive);
            return ofertasAvailable;
        }

        private async Task<IEnumerable<OfertaEntity>> ObterOfertasEmAberto()
        {
            var totalOfertas = await _ofertaQueryRepository.GetAll();

            //valida ofertas em aberto
            var ofertasEmAberto = totalOfertas.Where(x => x.Status == EStatusOferta.Open);
            return ofertasEmAberto;
        }
    }
}
