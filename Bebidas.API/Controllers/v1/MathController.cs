using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Bebidas.API.Contratos.v1;

namespace Bebidas.API.Controllers.v1
{
    [ApiController]
    [Route("v1/[controller]")]
    public class MathController : ControllerBase
    {
        private readonly ILogger<MathController> _logger;

        public MathController(ILogger<MathController> logger) { _logger = logger; }

        /// <summary>
        /// Realiza uma Operação matemática
        /// </summary>
        /// <param name="operacao"></param>
        /// <returns>Retorna o resultado da operação matemática</returns>
        /// <response code="200">Resultado do cálculo</response>
        /// <response code="500">Ops</response>
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        public ActionResult<int> Calcular(Soma operacao)
        {
            return Ok((operacao.primeiroValor + operacao.segundooValor));
        }
    }
}
