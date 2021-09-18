using Eice.Payment.Domain.Interface.Model;
using Eice.Payment.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eice.Payment.Domain.Client
{
    public class Client : BaseModel
    {
        //public ClientModel(string name, int cpfCnpj)
        //{
        //    Id = Guid.NewGuid();
        //    Name = name;
        //    CpfCnpj = cpfCnpj;
        //}

        [Required]
        public string Name { get; set; }
        public string CpfCnpj { get; set; }
        public ETipoPessoa TipoPessoa { get; set; }

    }

    public enum ETipoPessoa
    {
        Fisica,
        Juridica
    }
}
