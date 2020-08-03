namespace Bebidas.API.Contratos.v2
{
    public class OperacaoMatematica
    {
        public TipoCalculo tipoCalculo { get; set; }
        public int primeiroValor { get; set; }
        public int segundooValor { get; set; }
    }

    public enum TipoCalculo
    {
        Soma = 1,
        Multiplicacao = 2,
        Divisao = 3,
        Subtracao = 4
    }

}


