using Eice.Payment.API.Notification;
using Eice.Payment.Domain.Customer;
using Eice.Payment.Domain.Lancamento;
using Eice.Payment.Domain.Partner;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Eice.Payment.API.Command.Lancamento
{
    public class LancamentoCreateCommandHandler : CommandHandler<CustomerEntity>, IRequestHandler<LancamentoCreateCommand, bool>
    {
        private readonly ICustomerCommandRepository _customerCommandRepository;
        private readonly IPartnerQueryRepository _partnerQueryRepository;
        private readonly ICustomerQueryRepository _customerQueryRepository;

        public LancamentoCreateCommandHandler(IMediator bus, 
            ICustomerCommandRepository customerCommandRepository,
            IPartnerQueryRepository partnerQueryRepository,
            ICustomerQueryRepository customerQueryRepository)
            : base(bus, customerCommandRepository)
        {
            _customerCommandRepository = customerCommandRepository;
            _partnerQueryRepository = partnerQueryRepository;
            _customerQueryRepository = customerQueryRepository;
        }

        public async Task<bool> Handle(LancamentoCreateCommand request, CancellationToken cancellationToken)
        {
            //if (!request.IsValid()) { GetNotificationsErrors(request); return default; }

            try
            {
                //Validar request.AuthenticationKey pertence ao PartnerId
                var partner = await _partnerQueryRepository.Get(request.PartnerId);
                if (partner is null)
                    throw new Exception("Parceiro não encontrado");

                if(partner.AuthenticationKey != request.AuthenticationKey)
                    throw new Exception("Erro de autenticação");

                //Validar request.PartnerId existe
                var customer = await _customerQueryRepository.Get(request.CustomerId);
                if (customer is null)
                    throw new Exception("Cliente não encontrado");


                customer.SaldoAtual += request.Quantity;
                //metodo to map
                LancamentoEntity novoLancamento = new()
                {
                    //CustomerId = request.CustomerId,
                    Quantity = request.Quantity,
                    Description = request.Description
                };
                //customer.Lancamento.Add(novoLancamento);

                await _commandRepository.Update(customer.Id, customer);//faz sentido ter esse? _commandRepository ou somente _customerCommandRepository
                return await _customerCommandRepository.InsertLancamento(customer, novoLancamento);
            }
            catch (Exception ex)
            {
                await _bus.Publish(new ExceptionNotification("500", ex.Message, null, ex.StackTrace));
                return default;
            }
        }
    }
}
