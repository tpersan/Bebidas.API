using Bebidas.AcessoDados.Atualizacao;
using Bebidas.API.Contratos.v1;
using Bebidas.Implementacao.ServiceBus;

namespace Bebidas.Implementacao.Servico
{
    public class CervejaServico : ICervejaServico
    {
        private readonly IInclusaoCerveja _inclusaoCerveja;
        private readonly IServiceBusTopicService _serviceBus;

        public CervejaServico(IInclusaoCerveja inclusaoCerveja, IServiceBusTopicService serviceBusTopico)
        {
            _inclusaoCerveja = inclusaoCerveja;
            _serviceBus = serviceBusTopico;
        }


        public void IncluirCerveja(Cerveja cerveja)
        {
            _inclusaoCerveja.Inserir(cerveja);
            _serviceBus.EnviarMensagem("CervejaIncluida", cerveja.ConvertDados());
        }


    }
}
