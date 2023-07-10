using dbl.twins.sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dbl.twins.consumer
{
    internal class TwinClient
    {
        public TwinClient()
        {
            // Create a Twin map

            TwinMap map = new TwinMap() {
                new DigitalTwin
                {
                    Model = "thermostat",
                    Dtid = "thermostat1-1",
                    Paths = new List<string> { "/World/First/Lights/Rm_01" }
                },
                new DigitalTwin
                {
                    Model = "thermostat",
                    Dtid = "thermostat1-2",
                    Paths = new List<string> { "/Root/Rooms_Modular_2_3/Ground/Lights/Rm_02" }
                }
            };

            // Access and manipulate the list elements
            foreach (var twin in map)
            {
                Console.WriteLine("Model: " + twin.Model);
                Console.WriteLine("DTID: " + twin.Dtid);

                Console.WriteLine("Paths:");
                foreach (var path in twin.Paths)
                {
                    Console.WriteLine("- " + path);
                }

                Console.WriteLine();
            }
        }
    }
}
