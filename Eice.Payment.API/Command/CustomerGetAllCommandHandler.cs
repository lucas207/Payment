using Eice.Payment.API.DTO;
using Eice.Payment.API.Notification;
using Eice.Payment.Domain.Customer;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Eice.Payment.API.Command
{
    public class CustomerGetAllCommandHandler : CommandHandler, IRequestHandler<CustomerGetAllCommand, IEnumerable<CustomerDto>>
    {
        private readonly ICustomerRepository _clienteRepository;
        public CustomerGetAllCommandHandler(IMediator bus, ICustomerRepository clienteRepository) : base(bus)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<IEnumerable<CustomerDto>> Handle(CustomerGetAllCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) { GetNotificationsErrors(request); return default; }

            try
            {
                var list = await _clienteRepository.GetAll();

                List<CustomerDto> resp = new List<CustomerDto>();
                foreach (var item in list)
                {
                    resp.Add(new CustomerDto { Id = item.Id.ToString(), CpfCnpj = item.CpfCnpj, Name = item.Name, TipoPessoa = item.TipoPessoa });
                }

                return resp;
            }
            catch (Exception ex)
            {
                await _bus.Publish(new ExceptionNotification("500", ex.Message, null, ex.StackTrace));
                return default;
            }
        }
    }
}
