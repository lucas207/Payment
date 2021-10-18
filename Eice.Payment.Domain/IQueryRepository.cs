using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eice.Payment.Domain
{
    public interface IQueryRepository<T>
    {
        Task<T> Get(ObjectId Id);
        Task<IEnumerable<T>> GetAll();
    }
}
