using System.Collections.Generic;

namespace Eice.Payment.Domain.Client
{
    public interface IClienteRepository
    {
        ClientModel Get(int Id);
        IEnumerable<ClientModel> GetAll(int Id);
        bool Save(ClientModel entity);
        bool Update(ClientModel entity);
        bool Delete(ClientModel entity);
    }
}
