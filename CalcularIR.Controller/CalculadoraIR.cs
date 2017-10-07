using System.Linq;
using CalcularIR.Model;
using System.Collections.Generic;
using System;

namespace CalcularIR.Controller
{
    /// <summary>
    /// Provê métodos para cálculo do imposto de renda.
    /// </summary>
    public class CalculadoraIR
    {
        public double SalarioMinimo = 0.0;
        public List<Aliquota> Aliquotas = new List<Aliquota>();
        public List<Func<Contribuinte, double>> Descontos = new List<Func<Contribuinte, double>>();

        public CalculadoraIR(double salarioMinimo)
        {
            SalarioMinimo = salarioMinimo;
        }

        public double CalcularIR(ref Contribuinte contribuinte)
        {
            double descontos = CalcularDescontos(contribuinte);
            double aliquota = 0.0;
            // Atribui desconto por dependente.
            contribuinte.RendaLiquidaMensal = contribuinte.RendaBrutaMensal - descontos;
            aliquota = DefineAliquota(contribuinte.RendaLiquidaMensal);
            contribuinte.ImpostoDeRendaDevido = contribuinte.RendaLiquidaMensal * aliquota;
            return contribuinte.ImpostoDeRendaDevido;
        }

        public void AdicionarDesconto(Func<Contribuinte, double> calculoDesconto)
        {
            Descontos.Add(calculoDesconto);
        }

        public void AdicionarAliquota(Aliquota aliquota)
        {
            Aliquotas.Add(aliquota);
        }

        /// <summary>
        /// Identifiqua qual alíquota utilizar para a renda líquida recebida.
        /// </summary>
        /// <param name="rendaLiquida">Renda líquida do contribuinte.</param>
        /// <returns></returns>
        private double DefineAliquota(double rendaLiquida)
        {
            Aliquota aliquota = Aliquotas.First(a => rendaLiquida >= a.QuantidadeSalariosPiso * SalarioMinimo && (rendaLiquida < a.QuantidadeSalariosTeto * SalarioMinimo || a.QuantidadeSalariosTeto == 0));
            return aliquota.Valor;
        }

        public double CalcularDescontos(Contribuinte contribuinte)
        {
            double descontos = 0.0;
            for (int i = 0; i < Descontos.Count; i++)
                descontos += Descontos[i](contribuinte);
            return descontos;
        }
    }
}
