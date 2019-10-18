using System;
using System.Collections.Generic;
using System.Text;

namespace Bebidas.API.Contratos.v1
{
    public class Cerveja : Bebida
    {
        public decimal TeorAlcoolico { get; set; }
        public decimal TemperaturaIndicadaConsumo { get; set; }
    }
}


