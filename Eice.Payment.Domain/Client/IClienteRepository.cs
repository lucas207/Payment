using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eice.Payment.Domain.Client
{
    public interface IClienteRepository
    {
        Task<ObjectId> Create(Client entity);
        Task<Client> Get(ObjectId Id);
        Task<IEnumerable<Client>> GetAll();
        Task<bool> Update(ObjectId Id, Client entity);
        Task<bool> Delete(ObjectId Id);
    }
}
