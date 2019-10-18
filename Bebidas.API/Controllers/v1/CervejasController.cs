using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Bebidas.API.Contratos.v1;

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
        [ProducesResponseType(404)]
        public ActionResult<Cerveja> GetOne([FromRoute]string rotulo)
        {
            var cerveja = Cervejas.FirstOrDefault(c => c.Rotulo == rotulo);

            if (cerveja == null)
                return NotFound($"Não encontrada a Cerveja '{rotulo}'");

            return Ok(cerveja);
        }

        [HttpPost]
        public IActionResult Add([FromBody]Cerveja cerveja)
        {
            Cervejas.Add(cerveja);

            return Ok();
        }

        [HttpPut("{rotulo}")]
        public IActionResult Update([FromRoute] string rotulo, [FromBody]Cerveja cerveja)
        {
            var cervejaRetirada = Cervejas.FirstOrDefault(c => c.Rotulo == rotulo);

            if (cervejaRetirada == null)
                return NotFound($"Não encontrada a Cerveja '{rotulo}'");

            Cervejas.Remove(cervejaRetirada);
            Cervejas.Add(cerveja);

            return Ok();
        }

    }
}
