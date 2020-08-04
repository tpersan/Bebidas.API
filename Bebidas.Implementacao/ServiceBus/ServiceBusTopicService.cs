using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bebidas.Implementacao.ServiceBus
{
    public class ServiceBusTopicService : IServiceBusTopicService
    {
        private readonly IConfiguration _configuration;
        private readonly IDictionary<string, ITopicClient> _topicos;

        public ServiceBusTopicService(IConfiguration configuration)
        {
            _configuration = configuration;
            _topicos = new Dictionary<string, ITopicClient>();
        }

        public async Task EnviarMensagem(string topico, string mensagem)
        {
            ITopicClient client = null;

            lock (_topicos)
            {
                if (!_topicos.TryGetValue(topico, out client))
                {
                    client = new TopicClient(_configuration.GetValue<string>("ServiceBusConnectionString"),
                                                  topico);
                    _topicos.Add(topico, client);
                }
            }

            var message = new Message(Encoding.UTF8.GetBytes(mensagem));

            await client.SendAsync(message);
        }
    }
}
