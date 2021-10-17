using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eice.Payment.Domain.Customer
{
    public interface ICustomerCommandRepository
    {
        Task<ObjectId> Create(CustomerEntity entity);
        Task<bool> Update(ObjectId Id, CustomerEntity entity);
        Task<bool> Delete(ObjectId Id);
    }
}
