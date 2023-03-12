using MediatR;
using System.Collections.Generic;

namespace Eice.Payment.Domain.Oferta.Queries
{
    public class GetCoinsToTradeQuery : Query, IRequest<IEnumerable<CoinDto>>
    {
        public string CustomerId { get; set; }
    }
}
