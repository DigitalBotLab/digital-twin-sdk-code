
using dbl.twins.sdk.std;
using System;
using System.Collections.Generic;
using UnityEngine;
using static dbl.dts.unityplugin.UnityDigitalTwinClient;


namespace dbl.dts.unityplugin
{

    //Event System! - OBSERVER
    public class LightColorTemperatureBehaviour : MonoBehaviour, IBehaviour
    {
        UnityEngine.Color tempColor;
        public string mappedTwin;
        public Light mappedLight;


        void Start()
        {
            UnityDigitalTwinClient.instance.onDigitalTwinTelemetryUpdate +=Instance_onDigitalTwinTelemetryUpdate1;
        }

        private void Instance_onDigitalTwinTelemetryUpdate1(string arg1, string arg2)
        {
            TelemetryUpdate(new KeyValuePair<string, string>(arg1, arg2));
        }

        void Update()
        {

        }

        public void TelemetryUpdate(KeyValuePair<string, string> keyValues)
        {
            //Telemetry Update received!
            //Ignore it if its not for this mapped object
            if (keyValues.Key == mappedTwin) 
            {
                tempColor = GetColorForTemp(double.Parse(keyValues.Value));

                try
                {
                    if (tempColor != null)
                    {
                        if ((int)UnityDigitalTwinClient.instance.logLevel >= 0)
                        {
                            Debug.Log("Setting Light " + mappedLight.name + " to " + tempColor.ToString());
                        }
                        mappedLight.color = tempColor;
                    }
                }
                catch (NullReferenceException ex)
                {
                    Debug.Log("mappedLight was not set in the inspector");
                }
            }
        }

        private Color GetColorForTemp(double temp)
        {
            // convert temperature to an RGB color
            // smoothly interpolating between the colors

            // <= 60.0 -> blue
            // == 67.5 -> cyan
            // == 75.0 -> green
            // == 82.5 -> yellow
            // >= 90.0 -> red

            // init rgb values
            double color_r = 0.0;
            double color_g = 0.0;
            double color_b = 0.0;

            if (temp < 60.0)
            {
                return Color.blue;
            }
            else if (temp < 67.5)
            {
                // from 60.0 to 67.5, interpolate from blue to cyan
                return Color.cyan;
            }
            else if (temp < 75.0)
            {
                // from 67.5 to 75.0, interpolate from cyan to green
                return Color.green;
            }
            else if (temp < 82.5)
            {
                return Color.yellow;
            }
            else if (temp < 90)
            {
                return Color.red;
            }
            else
            {
                return Color.magenta;
            }
        }


    }
}
