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
    /// <summary>
    /// Client that connects to Event Hub to recieve telmetry events
    /// </summary>
    public class TwinClient
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
            var consumer = new EventHubConsumerClient(consumerGroup,_connectionString,_eventHubName);

            try
            {
                using CancellationTokenSource cancellationSource = new CancellationTokenSource();
                cancellationSource.CancelAfter(TimeSpan.FromSeconds(45));

                int eventsRead = 0;
                int maximumEvents = 30000;
                int count = 0;

                await foreach (PartitionEvent partitionEvent in consumer.ReadEventsAsync(cancellationSource.Token))
                {
                    count++;
                    string readFromPartition = partitionEvent.Partition.PartitionId;
                    byte[] eventBodyBytes = partitionEvent.Data.EventBody.ToArray();
                    string eventBodyString = Encoding.UTF8.GetString(eventBodyBytes);
                    string subject = "";
                    if (partitionEvent.Data.Properties.ContainsKey("cloudEvents:subject")) {
                        subject = partitionEvent.Data.Properties["cloudEvents:subject"].ToString();
                    }

                    Console.WriteLine($"{count} - Read event {subject} : {eventBodyString}");
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
