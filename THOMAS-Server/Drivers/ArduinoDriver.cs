using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace THOMASServer.Drivers
{
    public class ArduinoDriver : IDriver
    {
        public string Name => "Arduino";

        public bool Initialize()
        {
            return true; // BÖSE
        }
    }
}
