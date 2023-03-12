using Eice.Payment.Domain.Lancamento.Queries;
using System.Collections.Generic;

namespace Eice.Payment.Domain.Customer.Queries
{
    public class CustomerDetailDto : CustomerDto
    {
        public IList<LancamentoDto> Lancamentos { get; set; }
    }
}
