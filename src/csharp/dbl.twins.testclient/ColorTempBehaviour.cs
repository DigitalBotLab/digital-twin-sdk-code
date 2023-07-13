using dbl.twins.sdk;
using System.Drawing;

namespace dbl.twins.consumer
{

    /// <summary>
    /// Implements a custom property behaviour to change the color of lights based on temperature data
    /// </summary>
    public class ColorTempBehaviour : PropertyBehaviour
    {
        string pathToObject ="";
        string telemetryProperty = "";

        Color tempColor;

        public Color TempColor { get => tempColor; set => tempColor=value; }

        public ColorTempBehaviour(string pathToObject, string telemetryProperty)
        {
            this.pathToObject=pathToObject;
            this.telemetryProperty=telemetryProperty;
        }

        public override void TelemetryUpdate(Dictionary<string, object> keyValues)
        {
            TempColor = GetColorForTemp((double)keyValues[telemetryProperty]);

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
                // anything below 60.0 is blue
                color_r = 0.0;
                color_g = 0.0;
                color_b = 1.0;
            }
            else if (temp < 67.5)
            {
                // from 60.0 to 67.5, interpolate from blue to cyan
                color_r = 0.0;
                color_g = 1.0 - (67.5 - temp) / 7.5;
                color_b = 1.0;
            }
            else if (temp < 75.0)
            {
                // from 67.5 to 75.0, interpolate from cyan to green
                color_r = 0.0;
                color_g = 1.0;
                color_b = (75.0 - temp) / 7.5;
            }
            else if (temp < 82.5)
            {
                // 75.0 to 82.5, interpolate from green to yellow
                color_r = 1.0 - (82.5 - temp) / 7.5;
                color_g = 1.0;
                color_b = 0.0;
            }
            else if (temp < 90)
            {
                // 82.5 to 90, interpolate from yellow to red
                color_r = 1.0;
                color_g = (90.0 - temp) / 7.5;
                color_b = 0.0;
            }
            else
            {
                // anything above 90.0 is red
                color_r = 1.0;
                color_g = 0.0;
                color_b = 0.0;
            }

            return Color.FromArgb(1, (int)(255*color_r), (int)(255*color_g), (int)(255*color_b));
        }


    }
}