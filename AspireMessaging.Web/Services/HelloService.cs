using Azure.Messaging.ServiceBus;

namespace AspireMessaging.Web.Services
{
    public class HelloService
    {
        ServiceBusClient _client;
        public HelloService(ServiceBusClient client)
        {
            _client = client;
        }

        public ServiceBusClient Client => _client;
    }
}
