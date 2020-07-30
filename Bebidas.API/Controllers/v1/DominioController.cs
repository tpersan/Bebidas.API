using System.Collections.Generic;
using System.Linq;
using API.Infraestrutura.Base.API;
using API.Infraestrutura.Base.Condicao;
using API.Infraestrutura.Base.Contexto;
using API.Infraestrutura.Contrato;
using Bebidas.API.Contratos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Bebidas.API.Controllers.v1
{
    [ApiController]
    [Route("v1/dominios")]
    public class DominioController : BaseApiController
    {
        private static List<string> dominios = new List<string>
                {
                    nameof(Fabricante),
                    nameof(TipoCerveja),
                    nameof(FormatoApresentacao)
                };

        public static List<string> Dominios { get => dominios; }

        private readonly ILogger<DominioController> _logger;

        public DominioController(IContexto contexto) : base(contexto) { }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(200)]

        public ActionResult<List<string>> Get()
        {
            var dominiosDisponiveis = Resultado<List<string>>()
                .DaOperacao("Listar Dominios")
                .V1()
                .SemGerenciarConexaoDoBancoDeDados()
                .Executar(() => ResultadoDaOperacao<List<string>>.ComValor(Dominios));

            return Ok(dominiosDisponiveis.Valor);
        }


        [HttpGet("{dominio}")]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404, Type = typeof(string))]
        [ProducesResponseType(500, Type = typeof(string))]

        public ActionResult<List<string>> GetOne([FromRoute] string dominio)
        {
            PreCondicao
                .Para("Obter um dominio")
                .EhSatisfeitaCom("Domínio deve estar preenchido", !string.IsNullOrEmpty(dominio));

            var dominioEncontrado = Resultado<List<string>>()
                .DaOperacao("Listar Dados de Dominio")
                .V1()
                .SemGerenciarConexaoDoBancoDeDados()
                .Executar(() =>
               {
                   if (!Dominios.Any(d => d.Equals(dominio)))
                       return ResultadoDaOperacao<List<string>>.ComMensagem("Domínio não está na lista de disponibilidade");

                   if (dominio == Dominios[0])
                       return ResultadoDaOperacao<List<string>>.ComValor(Fabricante.Todos());

                   if (dominio == Dominios[1])
                       return ResultadoDaOperacao<List<string>>.ComValor(TipoCerveja.Todos());

                   if (dominio == Dominios[2])
                       return ResultadoDaOperacao<List<string>>.ComValor(FormatoApresentacao.Todos());

                   return ResultadoDaOperacao<List<string>>.ComMensagemDeExcecao($"Erro ao Obter Domínio: '{dominio}'!");
               });


            if (dominioEncontrado.Valor == null)
                return NotFound(dominio);

            if (dominioEncontrado.HouveErrosDuranteProcessamento)
                return StatusCode(500, dominio);

            return Ok(dominioEncontrado.Valor);
        }

    }
}
