using System;
using System.Collections.Generic;
using System.Text;

namespace Bebidas.API.Contratos.v1
{
    public class Bebida
    {
        public string Rotulo { get; set; }
        public string DescricaoComercial { get; set; }
        public string Marca { get; set; }
        public string Tipo { get; set; }
        public string PrincipaisIngredientes { get; set; }
        public FormaApresentacao Apresentacao { get; set; }
    }

    public class Suco : Bebida
    {
        public string Fruta { get; set; }
        public decimal PercentualMistura { get; set; }
    }

    public class Cerveja : Bebida
    {
        public decimal TeorAlcoolico { get; set; }
        public decimal TemperaturaIndicadaConsumo { get; set; }
    }
}


