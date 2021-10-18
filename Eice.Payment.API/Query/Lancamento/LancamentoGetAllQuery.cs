using Eice.Payment.API.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eice.Payment.API.Query.Lancamento
{
    public class LancamentoGetAllQuery : Query, IRequest<LancamentoDto>
    {
        public string CustomerId { get; set; }
    }
}
