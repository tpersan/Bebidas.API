using API.Infraestrutura.Base;
using API.Infraestrutura.Base.BancoDeDados;
using System.Configuration;

namespace Bebidas.Implementacao.BD
{
    public class BancoDeDados : IBancoDeDados
    {
        public ConnectionStringSettings ObterConfiguracao()
        {
            return new ConnectionStringSettings(
                Recurso.Obter().StringDeConexao("Nome"),
                Recurso.Obter().StringDeConexao("ConnectionString"),
                Recurso.Obter().StringDeConexao("Provider"));
        }

        public string ObterID()
        {
            return System.Guid.NewGuid().ToString();
        }
    }
}