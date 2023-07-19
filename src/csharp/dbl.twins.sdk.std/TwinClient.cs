using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Consumer;
using Azure.Messaging.EventHubs.Producer;
using Newtonsoft.Json;

namespace dbl.twins.sdk.std
{


    //For DT Telemetry Patch Events
    public class PatchOperation
    {
        public int value { get; set; }
        public string path { get; set; }
        public string op { get; set; }
    }

    public class JsonOperation
    {
        public string op { get; set; }
        public string path { get; set; }
        public int value { get; set; }
    }


    public class ConnectionEventArgs : EventArgs
    {
        public bool isConnected;
        public string message;

        public ConnectionEventArgs(bool isConnected, string message)
        {
            this.isConnected = isConnected;
            this.message = message;
        }
    }

    public class TelemetryEventArgs : EventArgs
    {
        public string source;
        public string telemetryData;

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
        public event EventHandler<TelemetryEventArgs> TelemetryUpdate;
        public event EventHandler<ConnectionEventArgs> Connected;

        public TwinClient(string eventHubConnectionString)
        {
            _connectionString = eventHubConnectionString;
            var parts = _connectionString.Split(new char[] { ';' });
            _eventHubName = parts[0];

        }

        public async Task ConnectHub()
        {
            var consumerGroup = EventHubConsumerClient.DefaultConsumerGroupName;
            var consumer = new EventHubConsumerClient(consumerGroup, _connectionString);

            try
            {
                OnConnected(true, "Connected");
                using CancellationTokenSource cancellationSource = new CancellationTokenSource();
                //cancellationSource.CancelAfter(TimeSpan.FromSeconds(45));

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
                    if (partitionEvent.Data.Properties.ContainsKey("cloudEvents:source"))
                    {
                        subject = partitionEvent.Data.Properties["cloudEvents:source"].ToString();

                        char delimiter = '/';
                        string[] substrings = subject.Split(delimiter);
                        if (subject.Contains("thermostat1"))
                        {
                            Console.WriteLine("");
                        }

                        OnTelemetryUpdate(substrings[substrings.Length-1], eventBodyString);
                    }

                    Console.WriteLine($"{count} - Read event {subject} : {eventBodyString}");


                    eventsRead++;

                    if (eventsRead >= maximumEvents)
                    {
                        OnConnected(false, "Maximum Events reached");
                        break;
                    }
                }
            }
            catch (TaskCanceledException)
            {
                OnConnected(false, "Cancelled");
                // This is expected if the cancellation token is
                // signaled.
            }
            catch (System.Exception ex)
            {
                OnConnected(false, ex.ToString());
            }
            finally
            {
                OnConnected(false, "Connection Closed");
                await consumer.CloseAsync();
            }
        }

        private void OnConnected(bool isConnected, string msg)
        {
            EventHandler<ConnectionEventArgs> handler = Connected;
            if (null != handler)
            {
                Connected(this, new ConnectionEventArgs(isConnected, msg));
            }
        }

        //Raise Telemetry Event
        private void OnTelemetryUpdate(string subject, string data)
        {
            EventHandler<TelemetryEventArgs> handler = TelemetryUpdate;
            if (null != handler)
            {
                data = data.Replace("[", "").Replace("]", "");

                JsonOperation deserializedObject = JsonConvert.DeserializeObject<JsonOperation>(data);

                if (deserializedObject.path != null)
                {

                    Console.WriteLine("Model ID: " + deserializedObject.path);
                    Console.WriteLine("Patch:");

                    handler(this, new TelemetryEventArgs(subject, deserializedObject.value.ToString()));

                }

            }
        }
    }
}
