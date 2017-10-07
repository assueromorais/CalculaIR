namespace CalcularIR.Model
{

    /// <summary>
    /// Representa uma alíquota de IR.
    /// </summary>
    public struct Aliquota
    {
        public int QuantidadeSalariosPiso;
        public int QuantidadeSalariosTeto;
        public double Valor;

        public Aliquota(int quantidadeSalariosPiso, int quantidadeSalariosTeto, double valor)
        {
            QuantidadeSalariosTeto= quantidadeSalariosTeto;
            QuantidadeSalariosPiso= quantidadeSalariosPiso;
            Valor = valor;
        }
    }

}
