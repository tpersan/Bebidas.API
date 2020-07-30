using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Bebidas.API.Contratos.v1;
using Bebidas.API.Contratos;
using API.Infraestrutura.Base.Condicao;
using API.Infraestrutura.Contrato;
using API.Infraestrutura.Base.API;
using API.Infraestrutura.Base.Contexto;

namespace Bebidas.API.Controllers.v1
{
    [ApiController]
    [Route("v1/cervejas")]
    public class CervejaController : BaseApiController
    {
        private static List<Cerveja> Cervejas = new List<Cerveja>
        {
            new Cerveja{ Rotulo="Skol", Fabricante=Fabricante.Ambev, TipoCerveja=TipoCerveja.Pilsen,
                Apresentacao = new FormaApresentacao{ Formato=FormatoApresentacao.Garrafa, Litragem=1 } },
            new Cerveja{ Rotulo="Vertigem", Fabricante=Fabricante.HocusPocus, TipoCerveja=TipoCerveja.IPA},
            new Cerveja{ Rotulo="Caium", Fabricante=Fabricante.Colorado, TipoCerveja=TipoCerveja.APA },
            new Cerveja{ Rotulo="ApaCadabra", Fabricante=Fabricante.HocusPocus, TipoCerveja=TipoCerveja.APA },
            new Cerveja{ Rotulo="Imperio", Fabricante=Fabricante.Ambev, TipoCerveja=TipoCerveja.Pilsen },
        };

        public CervejaController(IContexto contexto) : base(contexto) { }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        public IActionResult Get([FromQuery] int _pageLimit = 10, [FromQuery] int _offSet = 0)
        {
            var cervejas = Resultado<List<Cerveja>>()
                .DaOperacao("Pesquisar cervejas")
                .V1()
                .SemGerenciarConexaoDoBancoDeDados()
                .Executar(() =>
                {                    
                    return ResultadoDaOperacao<List<Cerveja>>.ComValor(Cervejas
                                .Skip(_offSet * _pageLimit)
                                .Take(_pageLimit)
                                .ToList());
                });

            return Ok(cervejas.Valor);
        }

        /// <summary>
        /// Busca uma cerveja pelo rótulo
        /// </summary>
        /// <param name="rotulo"></param>
        /// <returns>Retorna uma cerveja</returns>
        /// <response code="200">Dados da cerveja obtida</response>
        /// <response code="404">Cerveja não encontrada</response>  
        [HttpGet("{rotulo}", Name = "Obter Uma Cerveja")]
        [Produces("application/json", Type = typeof(Cerveja))]
        [ProducesResponseType(200)]
        [ProducesResponseType(404, Type = typeof(string))]
        public ActionResult<Cerveja> ObterUmaCerveja([FromRoute] string rotulo)
        {
            PreCondicao
                .Para("Obter uma cerveja")
                .EhSatisfeitaCom("O rótulo precisa ser definido!", !string.IsNullOrWhiteSpace(rotulo))
                .E("A lista de Cervejas não pode estar vazia!", Cervejas.Count > 0);

            var cerveja = Resultado<Cerveja>()
                .DaOperacao("Obter uma Cerveja")
                .V1()
                .SemGerenciarConexaoDoBancoDeDados()
                .Rastrear("Rotulo", rotulo)
                .Executar(
                    () => ResultadoDaOperacao<Cerveja>.ComValor(Cervejas.FirstOrDefault(c => c.Rotulo == rotulo)));


            if (cerveja == null || cerveja.HouveErrosDuranteProcessamento)
                return NotFound($"Não encontrada a Cerveja '{rotulo}'");

            return Ok(cerveja.Valor);
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(500, Type = typeof(string))]
        public IActionResult Add([FromBody] Cerveja cerveja)
        {
            PreCondicao
              .Para("Adicionar uma cerveja")
              .EhSatisfeitaCom("Preencha os dados da cerveja!", cerveja != null)
              .E("O rótulo deve ser preenchido", !string.IsNullOrWhiteSpace(cerveja.Rotulo));

            Resultado<bool>()
                .DaOperacao("Adicionar uma cerveja")
                .V1()
                .SemGerenciarConexaoDoBancoDeDados()
                .Rastrear("Rotulo", cerveja.Rotulo)
                .Executar(() =>
                {
                    Cervejas.Add(cerveja);
                    return ResultadoDaOperacao<bool>.ComValor(true);
                });

            return Ok();
        }

        [HttpPut("{rotulo}")]
        public IActionResult Update([FromRoute] string rotulo, [FromBody] Cerveja cerveja)
        {
            PreCondicao
              .Para("Atualizar uma cerveja")
              .EhSatisfeitaCom("Preencha os dados da cerveja!", cerveja != null)
              .E("O rótulo deve ser preenchido", !string.IsNullOrWhiteSpace(cerveja.Rotulo))
              .E("O rótulo a alterar deve ser o mesmo do rótulo atualizado", rotulo == cerveja.Rotulo);

            var cervejaRetirada = Cervejas.Find(c => c.Rotulo == rotulo);

            if (cervejaRetirada == null)
                return NotFound($"Não encontrada a Cerveja '{rotulo}'");

            Resultado<bool>()
                .DaOperacao("Atualizar uma cerveja")
                .V1()
                .SemGerenciarConexaoDoBancoDeDados()
                .Rastrear("rotulo", cerveja.Rotulo)
                .Executar(() =>
                {
                    Cervejas.Remove(cervejaRetirada);
                    Cervejas.Add(cerveja);
                    return ResultadoDaOperacao<bool>.ComValor(true);
                });

            return Ok(cerveja.Rotulo);
        }

    }
}
