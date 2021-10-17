﻿using System;

namespace Eice.Payment.API.DTO
{
    public class CustomerDto
    {
        public string Id { get; set; }
        public string PartnerId { get; set; }
        public string Cpf { get; set; }
        public DateTime CreationTime { get; set; }

        public string CpfFormated
        {
            get
            {
                return Convert.ToUInt64(Cpf).ToString(@"000\.000\.000\-00");
            }
        }
    }
}
