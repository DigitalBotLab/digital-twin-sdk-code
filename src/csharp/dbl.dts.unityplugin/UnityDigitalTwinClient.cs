﻿using dbl.twins.sdk.std;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace dbl.dts.unityplugin
{
    public class UnityDigitalTwinClient : MonoBehaviour
    {
        //Event System! - SUBJECT
        public static UnityDigitalTwinClient instance;

        private static TwinClient client;
        private static int updateCounter;

        //Endpoint=sb://tnd-twinns-bsta.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=p5wtMNMDI8F/YDsbKiPsrYr6IOReceuVz+AEhLvj5r4=

        public string eventHubConnString;
        public LogLevel logLevel;

        public enum LogLevel
        {
            Debug=0,
            Info=1,
            Warning=2,
            Error=3
        }

        public event Action<string, string> onDigitalTwinTelemetryUpdate;


        private void Awake()
        {
            instance = this;
            try
            {
                client = new TwinClient(eventHubConnString);
                client.TelemetryUpdate +=Client_TelemetryUpdate;
                client.Connected +=Client_Connected; ;
            }
            catch (NullReferenceException ex)
            {
                if ((int)logLevel >= 1)
                {
                    Debug.Log("event hub connection string was not set in the inspector");
                }
            }
        }

        private void Client_Connected(object sender, ConnectionEventArgs e)
        {
            Debug.Log(DateTime.Now + " - " + e.message);
        }

        private void Client_TelemetryUpdate(object sender, TelemetryEventArgs e)
        {
            if (onDigitalTwinTelemetryUpdate != null)
            {
                //Parse the data, extract the Value
                updateCounter++;
                onDigitalTwinTelemetryUpdate(e.source, e.telemetryData);
                if ((int)logLevel < 1)
                {
                    Debug.Log($"Telemetry Update {updateCounter}");
                }
            }
        }

        async void Start()
        {
            await client.ConnectHub();
        }


    }
}
