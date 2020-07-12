using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Bebidas.API.Controllers.v2
{
    public class ParametroCalculo
    {
        public string TipoCalculo { get; set; }
        public decimal Valor1 { get; set; }
        public decimal Valor2 { get; set; }
    }

    [ApiController]
    [Route("v2/[controller]")]
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
        public ActionResult<decimal> Get(v2.ParametroCalculo parametro)
        {
            return Ok((parametro.Valor1 + parametro.Valor2).ToString());
        }


        public class TipoCalculo
        {
            public static string Soma { get { return "Soma"; } }
            public static string Divisao { get { return "Divisao"; } }
        }




    }
}
