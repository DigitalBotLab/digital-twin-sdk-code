﻿
namespace dbl.twins.sdk
{

    /// <summary>
    /// Defines a custom map between Digital Twins and the Eelments the map to in a 3D scene
    /// </summary>
    interface ITwinMap { }


    /// <summary>
    /// Defines a custom behaviour
    /// </summary>
    public interface IBehaviour {
        abstract void TelemetryUpdate(object updated);

    }




}