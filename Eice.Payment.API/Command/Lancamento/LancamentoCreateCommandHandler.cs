using Eice.Payment.API.Notification;
using Eice.Payment.Domain.Lancamento;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Eice.Payment.API.Command.Lancamento
{
    public class LancamentoCreateCommandHandler : CommandHandler<LancamentoEntity>, IRequestHandler<LancamentoCreateCommand, string>
    {
        public LancamentoCreateCommandHandler(IMediator bus, ILancamentoCommandRepository lancamentoCommandRepository) 
            : base(bus, lancamentoCommandRepository)
        {
        }

        public async Task<string> Handle(LancamentoCreateCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) { GetNotificationsErrors(request); return default; }

            try
            {
                //Validar request.PartnerId existe
                //Validar request.AuthenticationKey pertence ao PartnerId

                //metodo to map
                LancamentoEntity entity = new()
                {
                    CustomerId = request.CustomerId,
                    Quantity = request.Quantity
                };

                await _commandRepository.Create(entity);

                return entity.Id.ToString();

            }
            catch (Exception ex)
            {
                await _bus.Publish(new ExceptionNotification("500", ex.Message, null, ex.StackTrace));
                return default;
            }
        }
    }
}
