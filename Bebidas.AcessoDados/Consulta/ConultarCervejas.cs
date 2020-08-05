using API.Infraestrutura.Base.BancoDeDados;
using Bebidas.API.Contratos.v1;
using System.Data;

namespace Bebidas.AcessoDados.Consulta
{
    public class ConultarCervejas : Consulta<Cerveja>
    {
        public ConultarCervejas(BancoDeDadosConexao bancoDados) : base(bancoDados) { }

        protected override string InstrucaoSql => @"
            SELECT 
               dados
            FROM 
               Cervejas
            WHERE 
               cerveja LIKE #rotulo#";

        public Cerveja Obter(string rotulo)
        {
            consulta.AdicionarParametroDeEntrada("rotulo", rotulo);
            return consulta.ObterUm();
        }

        protected override Cerveja Coletar(IDataReader dr)
        {
            return new Cerveja().ConverterEmObjeto(ObterString(dr, Campos.Dados));
        }

        private static class Campos
        {
            public const int Dados = 0;
        }

    }
}
