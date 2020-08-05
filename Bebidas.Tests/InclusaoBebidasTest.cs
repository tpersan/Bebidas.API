using Bebidas.API.Contratos;
using System.Data;
using System.Dynamic;
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

            var id = inclusaoCerveja.Inserir(new API.Contratos.v1.Cerveja
            {
                Rotulo = rotuloCriado,
                Apresentacao = new API.Contratos.v1.FormaApresentacao { Formato = "Teste" },
                Fabricante = Fabricante.Ambev,
                TipoCerveja = TipoCerveja.Golden
            });


            Assert.False(id == 0);

            var retorno = ObterCerveja(rotuloCriado);

            Assert.NotNull(retorno);
            Assert.NotNull(retorno.Dados);
            Assert.Equals(retorno.Rotulo, rotuloCriado);
            Assert.Equals(retorno.Id, id);
        }




        protected dynamic ObterCerveja(string rotulo)
        {
            var command = bancoWorkShop.BancoDados.Conexao().DbConnection.CreateCommand();
            command.CommandText = $"select rotulo, dados, id from cervejas where rotulo = '{rotulo}'";

            IDataReader dr = command.ExecuteReader();
            dynamic retorno = new ExpandoObject();

            if (dr.Read())
            {
                retorno.Rotulo = dr["rotulo"];
                retorno.Dados = dr["dados"];
                retorno.Id = dr["id"];
            }

            return retorno;
        }
    }
}
