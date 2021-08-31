using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eice.Payment.Domain.Interface.Model
{
    public interface IBaseModel
    {
        public Guid Id { get; }
        public DateTime CreatedAt { get; }
        public DateTime? UpdatedAt { get; }
    }
}
