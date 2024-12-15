using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Threading; // For AsyncEventHandler

namespace PawfectMatch.Components.Services
{
    public class MessagingBrokerClient : IMessagingBrokerClient
    {
        // Updated event to match the interface
        public event AsyncEventHandler<string> MessageConsumed = default!;

        public async Task<string> PublishAsync(string topic, string message)
        {
            // Simulate publishing
            await Task.Delay(100); // Simulated delay for publishing
            return $"Message '{message}' published to topic '{topic}'.";
        }

        public async Task StartListeningAsync(string topic, CancellationToken token)
        {
            // Simulate message consumption
            await Task.Run(async () =>
            {
                while (!token.IsCancellationRequested)
                {
                    await Task.Delay(2000); // Simulated delay for message arrival

                    // Trigger the MessageConsumed event if there are subscribers
                    if (MessageConsumed != null)
                    {
                        await MessageConsumed.InvokeAsync(this, $"Sample message from topic {topic} at {DateTime.Now}");
                    }
                }
            }, token);
        }

        public void StopListening(CancellationToken token)
        {
            // Logic to stop listening (if needed)
            // In this case, cancellation of the token will handle it.
        }
    }
}
