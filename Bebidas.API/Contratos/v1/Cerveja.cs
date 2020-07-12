using System.Collections.Generic;

namespace Bebidas.API.Contratos.v1
{
    public class Cerveja
    {
        public string Rotulo { get; set; }
        public string DescricaoComercial { get; set; }
        public Fabricante Fabricante { get; set; }
        public TipoCerveja TipoCerveja { get; set; }
        public List<string> Ingredientes { get; set; }
        public FormaApresentacao Apresentacao { get; set; }
        public decimal TeorAlcoolico { get; set; }
        public decimal TemperaturaIndicadaConsumo { get; set; }
    }


}


