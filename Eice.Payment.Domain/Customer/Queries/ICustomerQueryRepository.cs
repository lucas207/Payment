using System.Collections.Generic;

namespace Eice.Payment.Domain.Customer.Queries
{
    public interface ICustomerQueryRepository : IQueryRepository<CustomerEntity>
    {
        IEnumerable<CustomerEntity> GetAllFromPartnerId(string partnerId);
    }
}
