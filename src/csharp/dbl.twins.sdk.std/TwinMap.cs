using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dbl.twins.sdk.core
{
        
    public class DigitalTwin
    {
        public string Model { get; set; }
        public string Dtid { get; set; }
        public List<IBehaviour> Behaviours { get; set; }
    }

    public class TwinMap : List<DigitalTwin> { }
}
