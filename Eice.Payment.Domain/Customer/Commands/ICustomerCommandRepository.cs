﻿using Eice.Payment.Domain.Lancamento;
using System.Threading.Tasks;

namespace Eice.Payment.Domain.Customer.Commands
{
    public interface ICustomerCommandRepository : ICommandRepository<CustomerEntity>
    {
        Task<bool> InsertLancamento(CustomerEntity customerEntity, LancamentoEntity lancamentoEntity);
    }
}