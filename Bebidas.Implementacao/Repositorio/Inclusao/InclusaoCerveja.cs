using API.Infraestrutura.Base.BancoDeDados;
using Bebidas.Implementacao.Dto;

namespace Bebidas.Implementacao.Repositorio.Inclusao
{
    public class InclusaoCerveja : Atualizacao<long>, IInclusaoCerveja
    {
        public InclusaoCerveja(BancoDeDadosConexao bancoDados) : base(bancoDados)
        {
        }

        protected override string InstrucaoSql => @"
            INSERT INTO CERVEJAS (CERVEJA, DADOS) 
            VALUES(#CERVEJA#, #DADOS#);";

        public int Inserir(CervejaDto cerveja)
        {
            var resultado = Executar(c =>
            {
                c.AdicionarParametroDeEntrada<string>("CERVEJA", cerveja.Nome);
                c.AdicionarParametroDeEntrada<string>("DADOS", cerveja.Dados);
                c.AdicionarParametroDeSaidaDoTipoIdentidade<int>("Id");
            });

            return (int)resultado.ValoresDeSaida["Id"];
        }
    }
}
