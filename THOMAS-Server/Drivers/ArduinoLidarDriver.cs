using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using THOMASServer.Attributes;
using THOMASServer.Drivers.Base;

namespace THOMASServer.Drivers
{
    [Config(nameof(ArduinoLidarDriver))]
    public class ArduinoLidarDriver : ArduinoBase, IDriver
    {
        public string Name => "Arduino-Lidar";

        public event EventHandler<ScanValueReceivedEventArgs> ScanValueReceived;

        private int _arduinoId;
        private int _baudrate;

        public ArduinoLidarDriver(XElement config)
        {
            _arduinoId = int.Parse(config.Element("ArduinoId").Value);
            _baudrate = int.Parse(config.Element("Baudrate").Value);
        }

        public bool Initialize()
        {
            bool connected = SearchAndConnect(_arduinoId, _baudrate);

            if (!connected)
                return false;

            if (!Setup())
                return false;

            PackageReceived += ArduinoLidarDriver_PackageReceived;

            return true;
        }

        public bool StartScan(int roundsPerSecond)
        {
            byte? response = WritePackageWithResponse(new[] { (byte)10, (byte)roundsPerSecond }, 1)?[0];

            if (response == 1)
                return true;

            Logger.Error($"Starten des Lidar-Scans fehlgeschlagen. Rückgabe: {response}");

            return false;
        }

        public bool StopScan()
        {
            byte? response = WritePackageWithResponse(new[] { (byte)11 }, 1)?[0];

            if (response == 1)
                return true;

            Logger.Error($"Stoppen des Lidar-Scans fehlgeschlagen. Rückgabe: {response}");

            return false;
        }

        private void ArduinoLidarDriver_PackageReceived(object sender, PackageReceivedEventArgs e)
        {
            switch (e.ResponseCommandByte)
            {
                // Messwert aufgenommen
                case 100:
                    ProcessScanValue(e.Package);

                    break;
            }
        }

        private void ProcessScanValue(byte[] package)
        {
            float angle = BitConverter.ToSingle(package,0);
            int distance = BitConverter.ToInt16(package, 4);

            ScanValueReceived?.Invoke(this, new ScanValueReceivedEventArgs(angle, distance));
        }
    }
}
