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
    }


    public static class CervejaExtensions
    {
        public static Cerveja ConverterEmObjeto(this Cerveja cerveja, string dados)
        {
            return JsonConvert.DeserializeObject<Cerveja>(dados);
        }

        public static string ConverterEmTexto(this Cerveja cerveja)
        {
            if (string.IsNullOrEmpty(cerveja.Rotulo))
                return "Rótulo não foi Definido!";

            return JsonConvert.SerializeObject(cerveja, new JsonSerializerSettings
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


