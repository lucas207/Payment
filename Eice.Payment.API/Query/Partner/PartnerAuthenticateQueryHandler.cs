using Eice.Payment.API.Authentication;
using Eice.Payment.API.Notification;
using Eice.Payment.Domain.Partner;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Eice.Payment.API.Query.Partner
{
    public class PartnerAuthenticateQueryHandler : QueryHandler<PartnerEntity>, IRequestHandler<PartnerAuthenticateQuery, string>
    {
        private readonly IPartnerQueryRepository _partnerQueryRepository;
        private readonly IConfiguration _configuration;

        public PartnerAuthenticateQueryHandler(IMediator bus, IPartnerQueryRepository partnerQueryRepository,
            IConfiguration configuration) : base(bus, partnerQueryRepository)
        {
            _partnerQueryRepository = partnerQueryRepository;
            _configuration = configuration;
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
                var token =  new TokenService(_configuration).GenerateToken(partner);

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
