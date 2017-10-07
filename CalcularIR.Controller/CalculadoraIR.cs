using System.Linq;
namespace CalcularIR.Controller
{
    /// <summary>
    /// Provê métodos para cálculo do imposto de renda.
    /// </summary>
    public class CalculadoraIR
    {
        private double SalarioMinimo = 0.0;

        public CalculadoraIR(double salarioMinimo)
        {
            SalarioMinimo = salarioMinimo;
        }

        public double CalcularIR(double rendaBrutaMensal, int dependentes)
        {
            // Atribui desconto por dependente.
            double rendaLiquidaMensal = rendaBrutaMensal - (dependentes * .05 * SalarioMinimo);
            double aliquota = DefineAliquota(rendaLiquidaMensal);
            return rendaLiquidaMensal * aliquota;
        }

        private double DefineAliquota(double rendaLiquida)
        {
            if (rendaLiquida <= SalarioMinimo * 2)
                return 0;
            else if (rendaLiquida <= SalarioMinimo * 4)
                return 0.075;
            else if (rendaLiquida <= SalarioMinimo * 5)
                return 0.15;
            else if (rendaLiquida <= SalarioMinimo * 7)
                return 0.225;
            else
                return 0.275;
        }

    }
}
