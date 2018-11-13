using System;
using System.Collections.Generic;
using System.Text;

namespace Payment
{
    public class Parcela
    {
        public Decimal Value { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime PaymentDate { get; set; }
        public bool IsEntrada { get; set; }

        public Parcela()
        {
            PaymentDate = new DateTime(0);
            ExpirationDate = ExpirationDate.AddYears(DateTime.Now.Year - 1).AddMonths(DateTime.Now.Month - 1);
        }

        public void Pagar(DateTime date)
        {
            PaymentDate = date;
        }
    }
}
