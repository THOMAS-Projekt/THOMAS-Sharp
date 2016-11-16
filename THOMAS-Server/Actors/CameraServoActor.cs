using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using THOMASServer.Attributes;
using THOMASServer.Drivers;
using THOMASServer.Utils;

namespace THOMASServer.Actors
{
    [Requirement(typeof(ArduinoDriver))]
    public class CameraServoActor : IActor
    {
        public string Name => "Kamera-Servo";

        public CameraServoActor(ArduinoDriver arduinoDriver)
        {
            // TODO
        }
    }
}
