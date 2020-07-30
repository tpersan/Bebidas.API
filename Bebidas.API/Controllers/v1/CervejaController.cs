using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Bebidas.API.Contratos.v1;
using Bebidas.API.Contratos;
using API.Infraestrutura.Base.Condicao;
using contexto = API.Infraestrutura.Base.Contexto.ContextoPadrao.ContextoEstaticoPorThread;
using API.Infraestrutura.Contrato;
using API.Infraestrutura.Base.API;
using API.Infraestrutura.Base.Contexto;

namespace Bebidas.API.Controllers.v1
{
    [ApiController]
    [Route("v1/cervejas")]
    public class CervejaController : BaseApiController
    {
        //private readonly ILogger<CervejaController> _logger;

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
            return Ok(Cervejas
                .Skip(_offSet * _pageLimit)
                .Take(_pageLimit));
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
            try
            {
                Cervejas.Add(cerveja);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut("{rotulo}")]
        public IActionResult Update([FromRoute] string rotulo, [FromBody] Cerveja cerveja)
        {
            var cervejaRetirada = Cervejas.Find(c => c.Rotulo == rotulo);

            if (cervejaRetirada == null)
                return NotFound($"Não encontrada a Cerveja '{rotulo}'");

            Cervejas.Remove(cervejaRetirada);
            Cervejas.Add(cerveja);

            return Ok(cerveja.Rotulo);
        }

    }
}
