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

namespace dbl.twins.sdk
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

    public class TwinPollClient
    {




    }

    /// <summary>
    /// Client that connects to Event Hub to recieve telmetry events
    /// </summary>
    public class TwinEHClient
    {
        private string _connectionString;
        public event EventHandler<KeyValuePair<string, string>> TelemetryUpdate;

        public TwinEHClient(string eventHubConnectionString)
        {
            _connectionString = eventHubConnectionString;
        }

        public async Task ConnectHub()
        {
            var consumerGroup = EventHubConsumerClient.DefaultConsumerGroupName;
            var consumer = new EventHubConsumerClient(consumerGroup,_connectionString);

            try
            {
                //using CancellationTokenSource cancellationSource = new CancellationTokenSource();
                //cancellationSource.CancelAfter(TimeSpan.FromSeconds(45));

                int eventsRead = 0;
                int maximumEvents = 30000;
                int count = 0;

                await foreach (PartitionEvent partitionEvent in consumer.ReadEventsAsync())
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
            catch ( System.Exception ex)
            {

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
