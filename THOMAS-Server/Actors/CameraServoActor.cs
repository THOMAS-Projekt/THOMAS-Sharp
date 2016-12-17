using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using THOMASServer.Attributes;
using THOMASServer.Drivers;
using THOMASServer.Utils;

namespace THOMASServer.Actors
{
    [Requirement(typeof(ArduinoSensorsDriver))]
    public class CameraServoActor : IActor
    {
        public string Name => "Kamera-Servo";

        public CameraServoActor(ArduinoSensorsDriver arduinoDriver)
        {
            // TODO
        }
    }
}
