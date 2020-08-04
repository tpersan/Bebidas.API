using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
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

        public string Dados
        {
            get
            {
                if (string.IsNullOrEmpty(this.Rotulo))
                    return "Rótulo não foi Definido!";

                return JsonConvert.SerializeObject(this, new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                    NullValueHandling = NullValueHandling.Ignore,
                    ContractResolver = new DefaultContractResolver
                    {
                        NamingStrategy = new CamelCaseNamingStrategy()
                    }
                });
            }
        }

    }


}


