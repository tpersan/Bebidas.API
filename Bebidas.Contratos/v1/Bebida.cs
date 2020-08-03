namespace Bebidas.API.Contratos.v1
{
    public abstract class Bebida
    {
        public string Rotulo { get; set; }
        public string DescricaoComercial { get; set; }
        public string Marca { get; set; }
        public string Tipo { get; set; }
        public string PrincipaisIngredientes { get; set; }
        public FormaApresentacao Apresentacao { get; set; }
    }
}


