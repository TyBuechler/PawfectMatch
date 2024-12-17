using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Threading; // Ensure this is added

namespace PawfectMatch.Components.Services
{
    public interface IMessagingBrokerClient
    {
        Task<string> PublishAsync(string topic, string message);

        event AsyncEventHandler<string> MessageConsumed;

        Task StartListeningAsync(string topicName, CancellationToken cancellationToken);

        void StopListening(CancellationToken cancellationToken);
    }
}

