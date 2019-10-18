using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Bebidas.API.Contratos.v1;
using Bebidas.API.Contratos;

namespace Bebidas.API.Controllers.v1
{
    [ApiController]
    [Route("v1/[controller]")]
    public class MathController : ControllerBase
    {
        private readonly ILogger<MathController> _logger;

        public MathController(ILogger<MathController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        public ActionResult<int> Get(TipoCalculo tipo, int v1, int v2)
        {
            return Ok((v1 + v2).ToString());
        }


        public class TipoCalculo
        {
            public static string Soma { get { return "Soma"; } }
        }
    }
}
