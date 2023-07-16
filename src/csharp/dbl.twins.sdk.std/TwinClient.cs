using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Consumer;
using Azure.Messaging.EventHubs.Producer;

namespace dbl.twins.sdk.std
{
    
    public class TelemetryEventArgs: EventArgs
    {
        private string source;
        private string telemetryData;

        public TelemetryEventArgs(string source, string telemetryData)
        {
            this.source=source;
            this.telemetryData=telemetryData;
        }
    }

    /// <summary>
    /// Client that connects to Event Hub to recieve telmetry events
    /// </summary>
    public class TwinClient
    {
        private string _connectionString;
        private string _eventHubName;
        public event EventHandler<KeyValuePair<string, string>> TelemetryUpdate;

        public TwinClient(string eventHubConnectionString)
        {
            _connectionString = eventHubConnectionString;
            //Endpoint=sb://bldg-hubns-6uad.servicebus.windows.net/;SharedAccessKeyName=listener;SharedAccessKey=lEjVTywrizKSmlPXM6ZOeqlLDF+bw75Dp+AEhEVfOtM=;EntityPath=bldg-hub-6uad
            var parts = _connectionString.Split(new char[] { ';' });
            _eventHubName = parts[0];

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
                        OnTelemetryUpdate(subject, eventBodyString);
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

        //Raise Telemetry Event
        private void OnTelemetryUpdate(string subject, string data)
        {
            EventHandler<KeyValuePair<string, string>> handler = TelemetryUpdate;
            if (null != handler) handler(this, new KeyValuePair<string, string>(subject, data));
        }



    }
}
