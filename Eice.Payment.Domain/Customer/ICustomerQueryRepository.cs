using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eice.Payment.Domain.Customer
{
    public interface ICustomerQueryRepository
    {
        Task<CustomerEntity> Get(ObjectId Id);
        Task<IEnumerable<CustomerEntity>> GetAll();
    }
}
