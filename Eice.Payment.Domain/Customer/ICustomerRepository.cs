using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eice.Payment.Domain.Customer
{
    public interface ICustomerRepository
    {
        Task<ObjectId> Create(Customer entity);
        Task<Customer> Get(ObjectId Id);
        Task<IEnumerable<Customer>> GetAll();
        Task<bool> Update(ObjectId Id, Customer entity);
        Task<bool> Delete(ObjectId Id);
    }
}
