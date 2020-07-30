using Bebidas.API.Contratos.v2;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Bebidas.API.Controllers.v2
{
    [ApiController]
    [Route("v2/[controller]")]
    public class MathController : ControllerBase
    {
        private readonly ILogger<MathController> _logger;

        public MathController(ILogger<MathController> logger)
        {
            _logger = logger;
        }

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
        public ActionResult<decimal> Calcular(OperacaoMatematica operacao)
        {
            switch (operacao.tipoCalculo)
            {
                case TipoCalculo.Soma:
                    return Ok(operacao.primeiroValor + operacao.segundooValor);
                case TipoCalculo.Multiplicacao:
                    return Ok(operacao.primeiroValor * operacao.segundooValor);
                case TipoCalculo.Divisao:
                    return Ok(operacao.primeiroValor / operacao.segundooValor);
                case TipoCalculo.Subtracao:
                    return Ok(operacao.primeiroValor - operacao.segundooValor);
                default:
                    return BadRequest();
            }
        }

    }
}
