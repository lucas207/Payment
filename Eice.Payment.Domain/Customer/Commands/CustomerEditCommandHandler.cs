using Eice.Payment.Domain.Customer.Queries;
using Eice.Payment.Domain.Notification;
using Eice.Payment.Domain.Partner.Queries;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Eice.Payment.Domain.Customer.Commands
{
    public class CustomerEditCommandHandler : CommandHandler<CustomerEntity>, IRequestHandler<CustomerEditCommand, bool>
    {
        private readonly ICustomerQueryRepository _customerQueryRepository;
        private readonly IPartnerQueryRepository _partnerQueryRepository;

        public CustomerEditCommandHandler(IMediator bus, 
            ICustomerCommandRepository customerCommandRepository,
            IPartnerQueryRepository partnerQueryRepository,
            ICustomerQueryRepository customerQueryRepository) : base(bus, customerCommandRepository)
        {
            _customerQueryRepository = customerQueryRepository;
            _partnerQueryRepository = partnerQueryRepository;
        }

        public async Task<bool> Handle(CustomerEditCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) { GetNotificationsErrors(request); return default; }

            try
            {
                CustomerEntity customer = await ValidarCustomer(request);

                customer.Name = request.Name;

                return await _commandRepository.Update(customer.Id, customer);
            }
            catch (Exception ex)
            {
                await _bus.Publish(new ExceptionNotification("011", ex.Message, null), cancellationToken);
                return default;
            }
        }

        private async Task<CustomerEntity> ValidarCustomer(CustomerEditCommand request)
        {
            var partner = await _partnerQueryRepository.Get(request.PartnerId);
            if (partner is null)
                throw new Exception("Parceiro não encontrado");

            var customer = await _customerQueryRepository.Get(request.Id);
            if (customer is null || customer.PartnerId != request.PartnerId)
                throw new Exception("Cliente não encontrado");

            return customer;
        }
    }
}
