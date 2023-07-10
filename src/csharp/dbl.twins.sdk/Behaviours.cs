﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dbl.twins.sdk
{

    /// <summary>
    /// Applies Physics to the target element based on changes in telemetry data
    /// </summary>
    public abstract class PhysicsBehaviour : IBehaviour
    {
        public virtual void TelemetryUpdate(object updated) 
        {
            //Telemetry data has changed, apply physics
            SetPhysics(updated);
        }

        private void SetPhysics(object updated) { }

    }

    /// <summary>
    /// Change position of the target element based on changes in telemetry data
    /// </summary>
    public abstract class PositionBehaviour : IBehaviour
    {
        public virtual void TelemetryUpdate(object updated)
        {
            //Telemetry data has changed, update the position
            SetPosition(updated); 
        }
        public abstract void SetPosition(object updated);

    }

    /// <summary>
    /// Trigger an animation based on changes in telemetry data
    /// </summary>
    public abstract class AnimationBehaviour : IBehaviour
    {
        public virtual void TelemetryUpdate(object updated)
        {
            //Telemetry data has changed, update the position
            TriggerAnimation(updated);
        }
        public abstract void TriggerAnimation(object updated);

    }

    /// <summary>
    /// Change the value of properties on the target element based on changes in telemetry data
    /// </summary>
    public abstract class PropertyBehaviour : IBehaviour
    {

        public virtual void TelemetryUpdate(object updated) { 
            
        }

    }

}
