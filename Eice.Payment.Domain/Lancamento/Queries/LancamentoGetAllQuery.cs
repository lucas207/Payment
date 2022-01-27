using MediatR;

namespace Eice.Payment.Domain.Lancamento.Queries
{
    public class LancamentoGetAllQuery : Query, IRequest<LancamentoDto>
    {
        public string CustomerId { get; set; }
    }
}
