using Bebidas.AcessoDados.Consulta;
using Bebidas.API.Contratos;
using Bebidas.API.Contratos.v1;
using Xunit;

namespace Bebidas.Tests
{
    [Collection("Repositorio Collection")]
    public class InclusaoBebidasTest
    {

        private readonly BancoFixture bancoWorkShop;

        public InclusaoBebidasTest(BancoFixture banco)
        {
            bancoWorkShop = banco;
        }


        [Fact]
        public void InclusaoDevePersistirDadosDaCerveja()
        {
            var rotuloCriado = @"Cerveja de Teste";

            var inclusaoCerveja = new Bebidas.AcessoDados.Atualizacao.InclusaoCerveja(bancoWorkShop.BancoDados);

            var id = inclusaoCerveja.Inserir(new Cerveja
            {
                Rotulo = rotuloCriado,
                Apresentacao = new FormaApresentacao { Formato = "Teste" },
                Fabricante = Fabricante.Ambev,
                TipoCerveja = TipoCerveja.Golden
            });


            Assert.False(id == 0);
            ConsultarDadosDaCerveja(rotuloCriado);
        }

        private void ConsultarDadosDaCerveja(string rotuloCriado)
        {
            var retorno = new ConultarCervejas(bancoWorkShop.BancoDados).Obter(rotuloCriado);

            Assert.NotNull(retorno);
            Assert.Equal(retorno.Rotulo, rotuloCriado.ToUpper());
            Assert.Equal(retorno.Fabricante.Nome, Fabricante.Ambev.Nome.ToUpper());
            Assert.Equal(retorno.TipoCerveja.Estilo, TipoCerveja.Golden.Estilo.ToUpper());
            Assert.NotNull(retorno.Apresentacao);
            Assert.Equal(retorno.Apresentacao.Formato, "TESTE");

        }

    }
}
