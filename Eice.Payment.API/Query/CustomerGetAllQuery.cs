using Eice.Payment.API.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eice.Payment.API.Query
{
    public class CustomerGetAllQuery : Query, IRequest<IEnumerable<CustomerDto>>
    {
        //public override bool IsValid()
        //{
        //    return true;
        //}
    }
}
