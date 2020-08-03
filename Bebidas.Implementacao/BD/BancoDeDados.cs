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
            return "d98ebf39-613e-4dea-8ab9-1adad0dc358d";
        }
    }
}