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
    public class CervejaController : ControllerBase
    {
        private readonly ILogger<CervejaController> _logger;

        private static List<Cerveja> Cervejas;

        public CervejaController(ILogger<CervejaController> logger)
        {
            _logger = logger;

            if (Cervejas == null)
                Cervejas = new List<Cerveja>
                {
                    new Cerveja{ Rotulo="Skol", Marca=Marca.Ambev, Tipo=TipoCerveja.Pilsen,  Apresentacao = new FormaApresentacao{ Formato=FormatoApresentacao.Garrafa, Litragem=1 } },
                    new Cerveja{ Rotulo="Vertigem", Marca=Marca.HocusPocus, Tipo=TipoCerveja.IPA},
                    new Cerveja{ Rotulo="Caium", Marca=Marca.Colorado, Tipo=TipoCerveja.APA },
                    new Cerveja{ Rotulo="ApaCadabra", Marca=Marca.HocusPocus, Tipo=TipoCerveja.APA },
                    new Cerveja{ Rotulo="Imperio", Marca=Marca.Ambev, Tipo=TipoCerveja.Pilsen },
                };
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        public IActionResult Get([FromQuery] int _pageLimit = 10, [FromQuery]int _offSet = 0)
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
        [HttpGet("{rotulo}", Name = "ObterUmaCerveja")]
        [Produces("application/json", Type = typeof(Cerveja))]
        [ProducesResponseType(200)]
        [ProducesResponseType(404, Type = typeof(string))]
        public ActionResult<Cerveja> ObterUmaCerveja([FromRoute]string rotulo)
        {
            PreCondicao
                .Para("Obter uma cerveja")
                .EhSatisfeitaCom("O rótulo precisa ser definido!", !string.IsNullOrWhiteSpace(rotulo))
                .E("A lista de Cervejas não pode estar vazia!", Cervejas.Any());


            var cerveja = Cervejas.FirstOrDefault(c => c.Rotulo == rotulo);

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
