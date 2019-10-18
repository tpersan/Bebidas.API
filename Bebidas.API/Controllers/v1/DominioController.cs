using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bebidas.API.Contratos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Bebidas.API.Controllers.v1
{
    [ApiController]
    [Route("[controller]")]
    public class DominioController : ControllerBase
    {
        public static List<string> Dominios;

        private readonly ILogger<DominioController> _logger;

        public DominioController(ILogger<DominioController> logger)
        {
            _logger = logger;

            if (Dominios == null)
                Dominios = new List<string>
                {
                    "Marca",
                    "TipoCerveja",
                    "FormatoApresentacao"
                };
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
                return Ok(Marca.Todos());

            if (dominio == Dominios[1])
                return Ok(TipoCerveja.Todos());

            return StatusCode(500, dominio);
        }

    }
}
