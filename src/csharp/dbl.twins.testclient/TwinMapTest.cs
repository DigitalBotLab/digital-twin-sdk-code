using dbl.twins.sdk.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dbl.twins.consumer
{
    internal class TwinMapTest
    {
        public TwinMapTest()
        {

            // Create a Twin map
            TwinMap map = new TwinMap() {
                new DigitalTwin
                {
                    Model = "thermostat",
                    Dtid = "thermostat1-1",
                    Behaviours = new List<IBehaviour>()
                    {
                        new ColorTempBehaviour("/World/First/Lights/Rm_01", "Temperature"),
                        new ColorTempBehaviour("/World/First/Lights/Rm_02", "Temperature"),
                    } 
                },
                new DigitalTwin
                {
                    Model = "thermostat",
                    Dtid = "thermostat1-2",
                    Behaviours = new List<IBehaviour>()
                    {
                        new ColorTempBehaviour("/World/First/Lights/Rm_01", "Temperature"),
                        new ColorTempBehaviour("/World/First/Lights/Rm_02", "Temperature"),
                    }
                }
            };

            // Access and manipulate the list elements
            foreach (var twin in map)
            {
                Console.WriteLine("Model: " + twin.Model);
                Console.WriteLine("DTID: " + twin.Dtid);

                Console.WriteLine("Paths:");
                foreach (var behaviour in twin.Behaviours)
                {
                    Console.WriteLine("- " + behaviour.ToString());
                }

                Console.WriteLine();
            }
        }
    }
}
