using Eice.Payment.Domain.Notification;
using Eice.Payment.Domain.Customer;
using MediatR;
using MongoDB.Bson;
using System;
using System.Threading;
using System.Threading.Tasks;
using Eice.Payment.Domain.Customer.Commands;
using Eice.Payment.Domain.Partner.Queries;
using Eice.Payment.Domain.Customer.Queries;

namespace Eice.Payment.Domain.Lancamento.Commands
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
            if (!request.IsValid()) { GetNotificationsErrors(request); return default; }

            try
            {
                await ValidarPartner(request);

                var customer = await _customerQueryRepository.Get(request.CustomerId);
                if (customer is null || customer.PartnerId != request.PartnerId)
                    throw new Exception("Cliente não encontrado");


                customer.SaldoAtual += request.Quantity;
                //metodo to map
                LancamentoEntity novoLancamento = new()
                {
                    Id = ObjectId.GenerateNewId(),
                    Quantity = request.Quantity,
                    Description = request.Description
                };
                customer.Lancamentos.Add(novoLancamento);

                return await _commandRepository.Update(customer.Id, customer);
                //return await _customerCommandRepository.InsertLancamento(customer, novoLancamento);
            }
            catch (Exception ex)
            {
                await _bus.Publish(new ExceptionNotification("020", ex.Message, null, ex.StackTrace), cancellationToken);
                return default;
            }
        }

        private async Task ValidarPartner(LancamentoCreateCommand request)
        {
            //Validar request.PartnerId existe
            var partner = await _partnerQueryRepository.Get(request.PartnerId);
            if (partner is null)
                throw new Exception("Parceiro não encontrado");

            ////Validar request.AuthenticationKey pertence ao PartnerId
            //if (partner.AuthenticationKey != request.AuthenticationKey)
            //    throw new Exception("Erro de autenticação");
        }
    }
}
