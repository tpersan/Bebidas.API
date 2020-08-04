using Bebidas.Implementacao.Dto;
using Bebidas.Implementacao.Repositorio.Inclusao;
using Bebidas.Implementacao.ServiceBus;

namespace Bebidas.Implementacao.Servico
{
    public class CervejaServico : ICervejaServico
    {
        private readonly IInclusaoCerveja _inclusaoCerveja;
        private IServiceBusTopicService _serviceBus;

        public CervejaServico(IInclusaoCerveja inclusaoCerveja, IServiceBusTopicService serviceBusTopico)
        {
            _inclusaoCerveja = inclusaoCerveja;
            _serviceBus = serviceBusTopico;
        }


        public void IncluirCerveja(CervejaDto cerveja)
        {
            _inclusaoCerveja.Inserir(cerveja);
            _serviceBus.EnviarMensagem("CervejaIncluida", cerveja.Dados);            
        }


    }
}
