using API.Infraestrutura.Base.BancoDeDados;
using Bebidas.API.Contratos.v1;

namespace Bebidas.AcessoDados.Atualizacao
{
    public class InclusaoCerveja : Atualizacao<long>, IInclusaoCerveja
    {
        public InclusaoCerveja(BancoDeDadosConexao bancoDados) : base(bancoDados)
        {
        }

        protected override string InstrucaoSql => @"
            INSERT INTO CERVEJAS (CERVEJA, DADOS) 
            VALUES(#CERVEJA#, #DADOS#);";

        public int Inserir(Cerveja cerveja)
        {
            var resultado = Executar(c =>
            {
                c.AdicionarParametroDeEntrada("CERVEJA", cerveja.Rotulo);
                c.AdicionarParametroDeEntrada("DADOS", cerveja.Dados);
                c.AdicionarParametroDeSaidaDoTipoIdentidade<int>("Id");
            });

            return (int)resultado.ValoresDeSaida["Id"];
        }
    }
}
