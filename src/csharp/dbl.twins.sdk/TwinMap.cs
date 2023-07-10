using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dbl.twins.sdk
{
        
    public class DigitalTwin
    {
        public string Model { get; set; }
        public string Dtid { get; set; }
        public List<string> Paths { get; set; }
    }

    public class TwinMap : List<DigitalTwin>
    {
        public TwinMap()
        {

        }

          
    }
}
