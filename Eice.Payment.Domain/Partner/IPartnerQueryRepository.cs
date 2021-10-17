using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eice.Payment.Domain.Partner
{
    public interface IPartnerQueryRepository
    {
        Task<PartnerEntity> Get(ObjectId Id);
        Task<IEnumerable<PartnerEntity>> GetAll();
    }
}
