
using System.Collections.Generic;

namespace dbl.twins.sdk.core
{

    /// <summary>
    /// Defines a custom map between Digital Twins and the Eelments the map to in a 3D scene
    /// </summary>
    interface ITwinMap { }


    /// <summary>
    /// Defines a custom behaviour
    /// </summary>
    public interface IBehaviour {
        void TelemetryUpdate(KeyValuePair<string, string> keyValues);

    }




}