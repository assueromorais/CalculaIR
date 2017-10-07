namespace CalcularIR.Model
{

    /// <summary>
    /// Representa um contribuinte da Receita Federal.
    /// </summary>
    public class Contribuinte
    {
        public string CPF { get; set; }
        public string Nome { get; set; }
        public int TotalDependentes { get; set; }
        public double RendaBrutaMensal { get; set; }
        public double RendaLiquidaMensal { get; set; }
        public double ImpostoDeRendaDevido { get; set; }
    }

}
