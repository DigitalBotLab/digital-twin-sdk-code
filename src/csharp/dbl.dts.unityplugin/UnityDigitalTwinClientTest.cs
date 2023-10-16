using dbl.twins.sdk.core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Unity.Jobs;
using UnityEngine;

namespace dbl.dts.unityplugin
{
    /// <summary>
    /// Test Client - Testing IJob Service Interface, not currently working.  Use UnityDigitalTwinClient!
    /// </summary>
    public class UnityDigitalTwinClientTest : MonoBehaviour
    {
        //Event System! - SUBJECT
        public static UnityDigitalTwinClientTest instance;

        public string eventHubConnString;
        public LogLevel logLevel;
        public event Action<string, string> onDigitalTwinTelemetryUpdate;

        public enum LogLevel
        {
            Debug = 0,
            Info = 1,
            Warning = 2,
            Error = 3
        }
        private void Awake()
        {
            instance = this;
        }


        struct ClientJob : IJob
        {
            public event Action<string, string> _onDigitalTwinTelemetryUpdate;
            private static TwinClient _client;
            private static int _updateCounter;
            private string _eventHubConnString;
            private LogLevel _logLevel;

            public bool IsIdle { get; set; }

            public ClientJob(string eventHubConnString, LogLevel logLevel, Action<string, string> onDigitalTwinTelemetryUpdate)
            {
                _eventHubConnString = eventHubConnString;
                _logLevel = logLevel;
                IsIdle = false;
                _onDigitalTwinTelemetryUpdate = onDigitalTwinTelemetryUpdate;
            }

            public void Execute()
            {
                try
                {
                    _client = new TwinClient(_eventHubConnString);
                    _client.TelemetryUpdate += Client_TelemetryUpdate;
                    _client.Connected +=Client_Connected;

                    _client.ConnectHub();
                }
                catch (NullReferenceException ex)
                {
                    if ((int)_logLevel >= 1)
                    {
                        Debug.Log("event hub connection string was not set in the inspector");
                    }
                }
            }

            private void Client_TelemetryUpdate(object sender, TelemetryEventArgs e)
            {
                if (_onDigitalTwinTelemetryUpdate != null)
                {
                    //Parse the data, extract the Value
                    _updateCounter++;
                    _onDigitalTwinTelemetryUpdate(e.source, e.telemetryData);
                    if ((int)_logLevel < 1)
                    {
                        Debug.Log($"Telemetry Update {_updateCounter}");
                    }
                }
            }

            private void Client_Connected(object sender, ConnectionEventArgs e)
            {
                Debug.Log(DateTime.Now + " - " + e.message);
            }
            
        }

      

        

        

        void Start()
        {
            //Read for 24 hours
            var client = new ClientJob()
            {
                
            };
            

            JobHandle clientHandle  = client.Schedule();

            if (client.IsIdle)
            {
                clientHandle.Complete();
            }
        }

        //public void Execute()
        //{
        //    client.ConnectHub();
        //}
    }
}
