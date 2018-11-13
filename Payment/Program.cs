using System;

namespace Payment
{
    class Program
    {

        static void Main(string[] args)
        {
            int IdPayment = 0;
            string client = "Lucas";
            Decimal totalValue = new Decimal(1482.78);
            Decimal entryValue = new Decimal(300);
            DateTime expirationDate = new DateTime().AddDays(5);

            while (true)
            {
                IdPayment++;
                Payment payment = new Payment(IdPayment, client, totalValue, entryValue, expirationDate);
                payment.GeraParcelas(3);
                payment.ShowParcelas();
                Console.ReadLine();
            }

        }
    }
}
