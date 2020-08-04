using System.Threading.Tasks;

namespace Bebidas.Implementacao.ServiceBus
{
    public interface IServiceBusTopicService
    {
        Task EnviarMensagem(string topico, string mensagem);
    }
}
