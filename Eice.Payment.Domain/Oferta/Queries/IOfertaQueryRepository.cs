using System.Collections.Generic;

namespace Eice.Payment.Domain.Oferta.Queries
{
    public interface IOfertaQueryRepository : IQueryRepository<OfertaEntity>
    {
        IEnumerable<OfertaEntity> GetByCustomer(string customerId);
    }
}
