using System;
using System.ComponentModel.DataAnnotations;

namespace Eice.Payment.Infra.Model
{
    public class ClientModel : BaseModel
    {
        //public ClientModel(string name, int cpfCnpj)
        //{
        //    Id = Guid.NewGuid();
        //    Name = name;
        //    CpfCnpj = cpfCnpj;
        //}

        [Required]
        public string Name { get; set; }
        public int CpfCnpj { get; set; }
        public ETipoPessoa TipoPessoa { get; set; }
    }

    public enum ETipoPessoa
    {
        Fisica,
        Juridica
    }
}
