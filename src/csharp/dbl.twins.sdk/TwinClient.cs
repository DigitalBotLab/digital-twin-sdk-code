using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Consumer;
using Azure.Messaging.EventHubs.Producer;

namespace dbl.twins.sdk
{

    internal class TwinClient
    {
        private string _connectionString;
        private string _eventHubName;

        public TwinClient(string eventHubConnectionString, string eventHubName)
        {
            _connectionString = eventHubConnectionString;
            _eventHubName = eventHubName;
        }

        public async Task ConnectHub()
        {
            var consumerGroup = EventHubConsumerClient.DefaultConsumerGroupName;

            var consumer = new EventHubConsumerClient(
                consumerGroup,
                _connectionString,
                _eventHubName);

            try
            {
                using CancellationTokenSource cancellationSource = new CancellationTokenSource();
                cancellationSource.CancelAfter(TimeSpan.FromSeconds(45));

                int eventsRead = 0;
                int maximumEvents = 3;

                await foreach (PartitionEvent partitionEvent in consumer.ReadEventsAsync(cancellationSource.Token))
                {
                    string readFromPartition = partitionEvent.Partition.PartitionId;
                    byte[] eventBodyBytes = partitionEvent.Data.EventBody.ToArray();

                    Debug.WriteLine($"Read event of length {eventBodyBytes.Length} from {readFromPartition}");
                    eventsRead++;

                    if (eventsRead >= maximumEvents)
                    {
                        break;
                    }
                }
            }
            catch (TaskCanceledException)
            {
                // This is expected if the cancellation token is
                // signaled.
            }
            finally
            {
                await consumer.CloseAsync();
            }
        }



    }
}
