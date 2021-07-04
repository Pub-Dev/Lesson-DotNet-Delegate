using System;
using System.Threading;
using static PubDev.DelegateVsFuncVsAction.Pagamento;

namespace PubDev.DelegateVsFuncVsAction
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var pagamento = new Pagamento();

            PagamentoProcessado pagamentoProcessado = (itemId) =>
            {
                Console.WriteLine($"Item - {itemId} processado!");
            };

            pagamento.Processar(pagamentoProcessado);

            pagamento.Calcular(itemId =>
            {
                Console.WriteLine($"Item - {itemId} calculado!");

                return Math.Pow(itemId, 0.15d);
            });
        }
    }

    public class Pagamento
    {
        public delegate void PagamentoProcessado(int itemId);

        public void Processar(PagamentoProcessado pagamentoProcessado)
        {
            for (var i = 0; i <= 10; i++)
            {
                //Simulando pagamento
                Thread.Sleep(1000);
                //Status Processamento
                pagamentoProcessado.Invoke(i + 1);
            }
        }

        public delegate double PagamentoCalculado(int itemId);

        public void Calcular(PagamentoCalculado pagamentoCalculado)
        {
            for (var i = 0; i <= 10; i++)
            {
                //Simulando pagamento
                Thread.Sleep(1000);

                //Status Calculo
                int pagamentoId = i + 1;

                double valor = pagamentoCalculado.Invoke(pagamentoId);

                Console.WriteLine($"Valor calculado para pagamento {pagamentoId} = {valor:C2}");
            }
        }
    }
}
