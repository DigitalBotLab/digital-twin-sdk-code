using System;
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
    class PhysicsBehaviour : IBehaviour
    {
        public void TelemetryUpdate() { SetPhysics(); }

        private void SetPhysics() { }

    }

    /// <summary>
    /// Can change position of the target element based on changes in telemetry data
    /// </summary>
    class PositionBehaviour : IBehaviour
    {
        public void TelemetryUpdate() {

            //Telemetry data has changed, update the position
            SetPosition(); 
        }
        private void SetPosition() { }

    }

    /// <summary>
    /// Can change the value of properties on the target element based on changes in telemetry data
    /// </summary>
    class PropertyBehaviour : IBehaviour, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public string? PropertyName;

        public string? PropertyValue;

        public string? PropertyType;

        public void TelemetryUpdate() { 
            
            //Telemetry data has changed, update the property
            SetProperty(); 
        }
        private void SetProperty() {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
            }
        }


    }

}
