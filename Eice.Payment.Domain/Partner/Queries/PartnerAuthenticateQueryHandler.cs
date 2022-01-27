using Eice.Payment.Domain.Authentication;
using Eice.Payment.Domain.Notification;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Eice.Payment.Domain.Partner.Queries
{
    public class PartnerAuthenticateQueryHandler : QueryHandler<PartnerEntity>, IRequestHandler<PartnerAuthenticateQuery, string>
    {
        private readonly IPartnerQueryRepository _partnerQueryRepository;
        private readonly ITokenService _tokenService;

        public PartnerAuthenticateQueryHandler(IMediator bus, IPartnerQueryRepository partnerQueryRepository,
            ITokenService tokenService) : base(bus, partnerQueryRepository)
        {
            _partnerQueryRepository = partnerQueryRepository;
            _tokenService = tokenService;
        }

        public async Task<string> Handle(PartnerAuthenticateQuery request, CancellationToken cancellationToken)
        {
            try
            {
                //colocar sistema de Login e Senha?
                PartnerEntity partner = await _partnerQueryRepository.GetByAuthenticationKey(request.AuthenticationKey);
                if (partner is null)
                    throw new Exception("Parceiro não encontrado");

                // Gera o Token
                var token =  _tokenService.GenerateToken(partner);

                return token;
            }
            catch (Exception ex)
            {
                await _bus.Publish(new ExceptionNotification("046", ex.Message, null, ex.StackTrace), cancellationToken);
                return default;
            }
        }
    }
}
