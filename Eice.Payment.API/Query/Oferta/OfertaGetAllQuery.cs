﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eice.Payment.API.Query.Oferta
{
    public class OfertaGetAllQuery : Query, IRequest<IEnumerable<object>>
    {
    }
}
