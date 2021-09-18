using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eice.Payment.Domain.Customer
{
    public interface ICustomerCommandRepository
    {
        Task<ObjectId> Create(Customer entity);
        Task<bool> Update(ObjectId Id, Customer entity);
        Task<bool> Delete(ObjectId Id);
    }
}
