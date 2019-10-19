using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Bebidas.API.Contratos.v1;
using Bebidas.API.Contratos;
using API.Infraestrutura.Base.Condicao;
using API.Infraestrutura.Base.CaixaDeExecucao;
using API.Infraestrutura.Contrato;

namespace Bebidas.API.Controllers.v1
{
    [ApiController]
    [Route("v1/[controller]")]
    public class CervejasController : ControllerBase
    {
        private readonly ILogger<CervejasController> _logger;

        private static List<Cerveja> Cervejas;

        public CervejasController(ILogger<CervejasController> logger)
        {
            _logger = logger;

            if (Cervejas == null)
                Cervejas = new List<Cerveja>
                {
                    new Cerveja{ Rotulo="Skol", Marca=Marca.Ambev, Tipo=TipoCerveja.Pilsen,  Apresentacao = new FormaApresentacao{ Formato=FormatoApresentacao.Garrafa, Litragem=1 } },
                    new Cerveja{ Rotulo="Vertigem", Marca=Marca.HocusPocus, Tipo=TipoCerveja.IPA},
                    new Cerveja{ Rotulo="Caium", Marca=Marca.Colorado, Tipo=TipoCerveja.APA },
                };
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        public IActionResult Get()
        {
            return Ok(Cervejas);
        }



        [HttpGet("{rotulo}")]
        [Produces("application/json", Type = typeof(Cerveja))]
        [ProducesResponseType(200)]
        [ProducesResponseType(404, Type = typeof(string))]
        public ActionResult<Cerveja> GetOne([FromRoute]string rotulo)
        {
            Func<global::API.Infraestrutura.Base.BancoDeDados.IConexao> ConexaoSQLServer = null;


            PreCondicao
                .Para("Obter uma cerveja")
                .EhSatisfeitaCom("O rótulo precisa ser definido!", !string.IsNullOrWhiteSpace(rotulo))
                .E("A lista de Cervejas não pode estar vazia!", Cervejas.Any());


            var resultado = Resultado<Cerveja>
                .DaOperacao("Obter uma cerveja")
                .V1()
                .SemGerenciarConexaoDoBancoDeDados()
                .Rastrear("Rotulo", rotulo)
                .Executar(() =>
                        ResultadoDaOperacao<Cerveja>.ComValor(Cervejas.FirstOrDefault(c => c.Rotulo == rotulo)));

            PosCondicao
                .Para("Obter uma cerveja")
                .NaoSatisfeitaCom("Resultado deve estar preenchido", resultado.Valor == null);



            var cerveja = resultado.Valor;

            if (cerveja == null)
                return NotFound($"Não encontrada a Cerveja '{rotulo}'");

            return Ok(cerveja);
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(500, Type = typeof(string))]
        public IActionResult Add([FromBody]Cerveja cerveja)
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
        public IActionResult Update([FromRoute] string rotulo, [FromBody]Cerveja cerveja)
        {
            var cervejaRetirada = Cervejas.FirstOrDefault(c => c.Rotulo == rotulo);

            if (cervejaRetirada == null)
                return NotFound($"Não encontrada a Cerveja '{rotulo}'");

            Cervejas.Remove(cervejaRetirada);
            Cervejas.Add(cerveja);

            return Ok(cerveja.Rotulo);
        }

    }
}
