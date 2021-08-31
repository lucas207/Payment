using Eice.Payment.Domain.Model;
using System.Collections.Generic;

namespace Eice.Payment.Domain.Interface.Repository
{
    public interface IRepository<TEntity> where TEntity : BaseModel
    {
        TEntity Get(int Id);
        IEnumerable<TEntity> GetAll(int Id);
        bool Save(TEntity entity);
        bool Update(TEntity entity);
        bool Delete(TEntity entity);
    }
}
