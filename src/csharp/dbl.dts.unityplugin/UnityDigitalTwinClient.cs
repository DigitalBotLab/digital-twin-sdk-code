using dbl.twins.sdk.std;
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

        private TwinClient client;

        //Endpoint=sb://tnd-twinns-bsta.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=p5wtMNMDI8F/YDsbKiPsrYr6IOReceuVz+AEhLvj5r4=

        public string eventHubConnString;
        public event Action<string, string> onDigitalTwinTelemetryUpdate;


        public UnityDigitalTwinClient()
        {

            try
            {
                client = new TwinClient(eventHubConnString);
                client.TelemetryUpdate +=Client_TelemetryUpdate;
            }
            catch (NullReferenceException ex)
            {
                Debug.Log("event hub connection string was not set in the inspector");
            }
        }

        private void Awake()
        {
            instance = this;
        }

        void Start()
        {
            client.ConnectHub();
        }

        private void Client_TelemetryUpdate(object sender, KeyValuePair<string, string> e)
        {
            if (onDigitalTwinTelemetryUpdate != null)
            {
                onDigitalTwinTelemetryUpdate(e.Key, e.Value);
            }
        }

    }
}
