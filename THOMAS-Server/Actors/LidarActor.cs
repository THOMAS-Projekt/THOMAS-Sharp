using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using THOMASServer.Attributes;
using THOMASServer.Drivers;

namespace THOMASServer.Actors
{
    [Requirement(typeof(ArduinoLidarDriver))]
    public class LidarActor : IActor
    {
        public string Name => "Lidar";

        private readonly ArduinoLidarDriver _arduinoLidarDriver;

        public LidarActor(ArduinoLidarDriver arduinoLidarDriver)
        {
            _arduinoLidarDriver = arduinoLidarDriver;
        }

        public bool StartScan(int roundsPerSecond) => _arduinoLidarDriver.StartScan(roundsPerSecond);
        public bool StopScan() => _arduinoLidarDriver.StopScan();
    }
}
