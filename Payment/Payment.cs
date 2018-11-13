using System;
using System.Collections.Generic;
using System.Text;

namespace Payment
{
    public class Payment
    {
        public int Id { get; set; }
        public string Client { get; set; }
        public Decimal TotalValue { get; set; }
        public Decimal EntryValue { get; set; }
        public DateTime ExpirationDate { get; }
        public List<Parcela> Parcelas { get; set; }


        public Payment(int idPayment, string client, Decimal totalValue, Decimal entryValue,
            DateTime expirationDate)
        {
            if (Validate())
            {
                Id = idPayment;
                Client = client;
                TotalValue = totalValue;
                EntryValue = entryValue;
                ExpirationDate = expirationDate;
                Parcelas = new List<Parcela>();
                Console.WriteLine("Payment Criado com sucesso " + Id);
            }
            else
            {
                Console.WriteLine("Valores invalidos");
            }
        }

        public void ShowParcelas()
        {
            foreach (var parcela in Parcelas)
            {
                if (parcela.IsEntrada)
                {
                    Console.WriteLine($"Entrada: R${parcela.Value}");
                }
                else if (parcela.PaymentDate > new DateTime())
                {
                    Console.WriteLine($"Pago em: {parcela.PaymentDate}, R${parcela.Value}");
                }
                else
                {
                    Console.WriteLine($"Não Pago, vencimento: {parcela.ExpirationDate}");
                }

            }
        }

        public void GeraParcelas(int numParcelas)
        {
            if (numParcelas > 0)
            {
                Parcela entrada = new Parcela() { Value = EntryValue, PaymentDate = DateTime.Now, IsEntrada = true };
                Parcelas.Add(entrada);

                Decimal divisionValue = TotalValue - EntryValue;
                Decimal parcelaValue = divisionValue / numParcelas;
                for (int i = 1; i <= numParcelas; i++)
                {
                    Parcela parcela = new Parcela() { Value = parcelaValue };
                    parcela.ExpirationDate = parcela.ExpirationDate.AddDays(ExpirationDate.Day - 1);
                    parcela.ExpirationDate = parcela.ExpirationDate.AddMonths(i);
                    Parcelas.Add(parcela);
                }
            }
            else
            {
                Console.WriteLine("Quantidade de Parcelas inválida");
            }
        }

        public bool Validate()
        {
            if (TotalValue >= EntryValue)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
