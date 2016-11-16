using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using THOMASServer.Attributes;

namespace THOMASServer.Drivers
{
    [Config(nameof(ArduinoDriver))]
    public class ArduinoDriver : IDriver
    {
        public string Name => "Arduino";

        public ArduinoDriver(XElement config)
        {

        }

        public bool Initialize()
        {
            return true; // True, wenn der arduino ininialisiert werden konnte, sonst false
        }
    }
}
