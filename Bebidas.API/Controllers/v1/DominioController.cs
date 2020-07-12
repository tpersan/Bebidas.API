using System.Collections.Generic;
using System.Linq;
using Bebidas.API.Contratos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Bebidas.API.Controllers.v1
{
    [ApiController]
    [Route("v1/dominios")]
    public class DominioController : ControllerBase
    {
        private static List<string> dominios = new List<string>
                {
                    nameof(Fabricante),
                    nameof(TipoCerveja),
                    nameof(FormatoApresentacao)
                };

        public static List<string> Dominios { get => dominios; }

        private readonly ILogger<DominioController> _logger;

        public DominioController(ILogger<DominioController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(200)]

        public ActionResult<List<string>> Get()
        {
            return Ok(Dominios);
        }


        [HttpGet("{dominio}")]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404, Type = typeof(string))]
        [ProducesResponseType(500, Type = typeof(string))]

        public ActionResult<List<string>> GetOne([FromRoute] string dominio)
        {
            if (!Dominios.Any(d => d.Equals(dominio)))
                return NotFound(dominio);

            if (dominio == Dominios[0])
                return Ok(Fabricante.Todos());

            if (dominio == Dominios[1])
                return Ok(TipoCerveja.Todos());

            if (dominio == Dominios[2])
                return Ok(FormatoApresentacao.Todos());

            return StatusCode(500, dominio);
        }

    }
}
