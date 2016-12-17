using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using THOMASServer.Attributes;
using THOMASServer.Drivers.Base;

namespace THOMASServer.Drivers
{
    [Config(nameof(ArduinoSensorsDriver))]
    public class ArduinoSensorsDriver : ArduinoBase, IDriver
    {
        public string Name => "Arduino-Sensorik";

        private int _arduinoId;
        private int _baudrate;

        public ArduinoSensorsDriver(XElement config)
        {
            _arduinoId = int.Parse(config.Element("ArduinoId").Value);
            _baudrate = int.Parse(config.Element("Baudrate").Value);
        }

        public bool Initialize()
        {
            bool connected = SearchAndConnect(_arduinoId, _baudrate);

            if (connected)
                Logger.Info("Verbindung zum Arduino hergestellt.");

            return connected;
        }
    }
}
