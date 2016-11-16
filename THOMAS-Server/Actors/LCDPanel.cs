using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using THOMASServer.Attributes;
using THOMASServer.Drivers;

namespace THOMASServer.Actors
{
    [Requirement(typeof(ArduinoDriver))]
    public class LCDPanel : IActor
    {
        public string Name => "LCDP-Panel";

        public LCDPanel(ArduinoDriver arduinoDriver)
        {

        }
    }
}
