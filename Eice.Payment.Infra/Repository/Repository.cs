using Eice.Payment.Domain.Interface.Repository;
using Eice.Payment.Domain.Model;
using System;
using System.Collections.Generic;

namespace Eice.Payment.Infra.Repository
{
    //DELETA ISSO?
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseModel
    {
        private readonly MongoDbContext dbContext;

        public Repository()
        {
            dbContext = new MongoDbContext();
        }

        public bool Delete(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public TEntity Get(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> GetAll(int Id)
        {
            throw new NotImplementedException();
        }

        public bool Save(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
