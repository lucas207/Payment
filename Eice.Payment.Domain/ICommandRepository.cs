using MongoDB.Bson;
using System.Threading.Tasks;

namespace Eice.Payment.Domain
{
    public interface ICommandRepository<T>
    {
        Task<ObjectId> Create(T entity);
        Task<bool> Update(ObjectId Id, T entity);
        Task<bool> Delete(ObjectId Id);
    }
}
