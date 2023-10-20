using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dbl.twins.sdk.core
{

    /// <summary>
    /// Applies Physics to the target element based on changes in telemetry data
    /// </summary>
    public interface IPhysicsBehaviour : IBehaviour
    {
        public virtual void TelemetryUpdate(KeyValuePair<string, string> keyValues)
        {
            //Telemetry data has changed, apply physics
            SetPhysics(new object());
        }

        void SetPhysics(object updated);

    }

    /// <summary>
    /// Change position of the target element based on changes in telemetry data
    /// </summary>
    public interface IPositionBehaviour : IBehaviour
    {
        public virtual void TelemetryUpdate(KeyValuePair<string, string> keyValues)
        {
            //Telemetry data has changed, update the position
            SetPosition(new object()); 
        }
        void SetPosition(object updated);

    }

    /// <summary>
    /// Trigger an animation based on changes in telemetry data
    /// </summary>
    public interface IAnimationBehaviour : IBehaviour
    {
        public virtual void TelemetryUpdate(KeyValuePair<string, string> keyValues)
        {
            //Telemetry data has changed, update the position
            TriggerAnimation(new object());
        }
        void TriggerAnimation(object updated);

    }

    /// <summary>
    /// Change the value of properties on the target element based on changes in telemetry data
    /// </summary>
    public interface IPropertyBehaviour : IBehaviour
    {

        public virtual void TelemetryUpdate(KeyValuePair<string, string> keyValues) { 
            
        }

        void SetProperty(object updated);
    }

}
