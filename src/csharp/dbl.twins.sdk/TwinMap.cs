using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dbl.twins.sdk
{
    /// <summary>
    /// A map of digital twins and thier 3D scene behaviours
    /// </summary>
    internal class TwinMap: Dictionary<string, string>
    {
        



    }

    //
    //    TwinID -> List<3D Scene Element, Behaviour>
    //              Building1/Floor1/Room1/Light1, TempColorBehavior(settings)
    //              Building1/Floor1/Room1/Light2, TempColorBehavior(settings)
    //    
    //
    // Json map
    //[
    //    {
    //        "model": "thermostat",
    //        "dtid": "thermostat1-1",
    //        "paths": [ "/World/First/Lights/Rm_01" ]
    //},
    //    {
    //    "model": "thermostat",
    //        "dtid": "thermostat1-2",
    //        "paths": [ "/Root/Rooms_Modular_2_3/Ground/Lights/Rm_02" ]
    //    },
    //]
}
