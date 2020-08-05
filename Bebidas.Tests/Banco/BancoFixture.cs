using API.Infraestrutura.Base;
using API.Infraestrutura.Base.BancoDeDados;
using Bebidas.Implementacao.BD;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using contexto = API.Infraestrutura.Base.Contexto.ContextoPadrao.ContextoEstaticoPorThread;

namespace Bebidas.Tests
{

    public class BancoFixture : IDisposable
    {
        public BancoDeDadosConexao BancoDados;
        public LocalWorkshop baseWorkshop;
        private const string NOME_BASE = "Workshop_padroesDEV";

        public BancoFixture()
        {
            BancoDados = new BancoDeDadosConexao(contexto.Instancia, new BancoDeDados());

            var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();

            Recurso.DefinirOrigemDoRecurso(new RecursoDeConfiguracao(configuration));

            BancoDados.Conexao();

            baseWorkshop = new LocalWorkshop(NOME_BASE);

            baseWorkshop.ExecutarCriacao();
        }


        void IDisposable.Dispose()
        {
            baseWorkshop.DestruirLocalDB();
            GC.SuppressFinalize(this);
        }
    }
}
