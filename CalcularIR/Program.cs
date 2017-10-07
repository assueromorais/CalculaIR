using System;
using System.Collections.Generic;
using System.Linq;
using CalcularIR.Model;
using CalcularIR.Controller;

namespace CalcularIR
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Contribuinte> contribuintes = new List<Contribuinte>();
            Contribuinte contribuinte;
            bool encerrar = false;
            string cpf = null;
            ExibirMensagemIntroducao();
            while (!encerrar)
            {
                Console.WriteLine("CPF do contribuinte (informe zero para concluir):");
                cpf = Console.ReadLine();
                if (cpf.Trim() != "0")
                {
                    contribuinte = new Contribuinte();
                    contribuinte.CPF = cpf;
                    Console.WriteLine("Nome:");
                    contribuinte.Nome = Console.ReadLine();
                    contribuinte.TotalDependentes = (int)LerNumero("Total de dependentes:");
                    contribuinte.RendaBrutaMensal = LerNumero("Renda bruta mensal:");
                    contribuintes.Add(contribuinte);
                    contribuinte = null;
                }
                else
                {
                    encerrar = true;
                }
            }
            if (contribuintes.Count > 0)
            {
                double salarioMinimo = LerNumero("Informe o salário mínimo em vigor:");
                // Prepara a calculadora de IR.
                CalculadoraIR calculadora = new CalculadoraIR(salarioMinimo);
                calculadora.AdicionarDesconto(delegate (Contribuinte c)
                {
                    return c.TotalDependentes * .05 * salarioMinimo;
                });

                calculadora.AdicionarAliquota(new Aliquota(0, 2, 0));
                calculadora.AdicionarAliquota(new Aliquota(2, 4, .075));
                calculadora.AdicionarAliquota(new Aliquota(4, 5, .15));
                calculadora.AdicionarAliquota(new Aliquota(5, 7, .225));
                calculadora.AdicionarAliquota(new Aliquota(7, 0, .275));

                Console.WriteLine();
                Console.WriteLine("Calculando IRs...");
                for (int i = 0; i < contribuintes.Count; i++)
                {
                    contribuinte = contribuintes[i];
                    calculadora.CalcularIR(ref contribuinte);
                    contribuintes[i] = contribuinte;
                }
                ExibirResultado(contribuintes);
            }
            else
            {
                Console.WriteLine("Nenhum contribuinte informado");
            }
            Console.WriteLine();
            Console.WriteLine("Obrigado por utilizar nossa calculadora!");
            Console.ReadKey(true);
        }

        private static void ExibirMensagemIntroducao()
        {
            Console.WriteLine("Calculadora de imposto de renda (c) 2017 por Assuéro Araújo Morais v1.0.0");
            Console.WriteLine();
            Console.WriteLine("Calcula o imposto de renda de um ou mais contribuintes.");
            Console.WriteLine("Para calcular informe: CPF, nome, número de dependentes e renda bruta mensal de cada contribuinte.");
            Console.WriteLine("Para encerrar informe o número zero como CPF");
            Console.WriteLine();
        }

        /// <summary>
        /// Escreve o resultado do cálculo de IR dos contribuintes na tela.
        /// </summary>
        /// <param name="contribuintes"></param>
        private static void ExibirResultado(List<Contribuinte> contribuintes)
        {
            Console.WriteLine();
            Console.WriteLine("Resultado(s):");
            Console.WriteLine("".PadRight(Console.WindowWidth, '='));
            contribuintes = contribuintes.OrderBy(c => c.ImpostoDeRendaDevido).ThenBy(c => c.Nome).ToList();
            for (int i = 0; i < contribuintes.Count; i++)
            {
                Console.WriteLine(string.Format("{0} - Imposto devido: {2:C}", contribuintes[i].Nome.ToUpper(), contribuintes[i].RendaBrutaMensal, contribuintes[i].ImpostoDeRendaDevido));
            }
            Console.WriteLine("".PadRight(Console.WindowWidth, '='));
        }

        /// <summary>
        /// Lê um número digitado pelo usuário, exibindo mensagem de falha caso não seja fornecido um número.
        /// </summary>
        /// <param name="numero"></param>
        private static double LerNumero(string prompt)
        {
            string numeroS = null;
            double numero = 0.0;
            Console.WriteLine(prompt);
            numeroS = Console.ReadLine();
            while (!double.TryParse(numeroS, out numero))
            {
                Console.WriteLine("Formato incorreto!");
                Console.WriteLine(prompt);
                numeroS = Console.ReadLine();
            }
            return numero;
        }
    }
}