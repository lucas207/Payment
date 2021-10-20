﻿using Eice.Payment.API.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eice.Payment.API.Query.Customer
{
    public class CustomerGetByIdQuery : IRequest<CustomerDto>
    {
        public Guid Id { get; set; }

        //public override bool IsValid()
        //{
        //    return true;
        //}
    }
}