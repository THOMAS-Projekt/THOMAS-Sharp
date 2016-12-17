using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace THOMASServer.Drivers.Base
{
    public abstract class ArduinoBase
    {
        public event EventHandler<PackageReceivedEventArgs> PackageReceived;

        private SerialPort _serialPort;

        protected bool SearchAndConnect(int arduinoId, int baudrate)
        {
            foreach (string portName in SerialPort.GetPortNames())
            {
                if (!portName.Contains("COM") && !portName.Contains("ACM"))
                    continue;

                _serialPort = new SerialPort(portName, baudrate) { DtrEnable = true };

                try
                {
                    Logger.Debug($"Verbinde mit {portName}...");

                    _serialPort.Open();
                    _serialPort.DiscardInBuffer();

                    byte? receivedArduinoId = ReadPackage(1)?[0];
                    if (receivedArduinoId == arduinoId)
                    {
                        Logger.Debug("Verbindung zum Arduino hergestellt.");

                        BeginReceive();

                        return true;
                    }

                    Logger.Debug($"Ignoriere Arduino an Port {portName}, falsche Arduino-ID: {receivedArduinoId}");
                    _serialPort.Close();
                }
                catch (Exception ex)
                {
                    Logger.Warning($"Kein Zugriff auf Port {portName}: {ex.Message}");
                }
            }

            return false;
        }

        protected bool Setup()
        {
            byte? response = WritePackageWithResponse(new[] { (byte)1 }, 1)?[0];

            if (response == 1)
            {
                Logger.Debug("Arduino initialisiert.");
                return true;
            }

            Logger.Error($"Initialisierung vom Arduino am Port {_serialPort.PortName} fehlgeschlagen. Rückgabe: {response}");

            return false;
        }

        protected void WritePackage(byte[] package)
        {
            int packageLength = package.Length;

            if (packageLength > byte.MaxValue)
            {
                Logger.Error($"Ungültige Paketlänge von {packageLength} Bytes.");
                return;
            }

            _serialPort.Write(new[] { (byte)packageLength }, 0, 1);
            _serialPort.Write(package, 0, packageLength);
        }

        protected byte[] WritePackageWithResponse(byte[] package, byte expectedPackageLength = 0)
        {
            WritePackage(package);

            byte[] response = null;

            using (ManualResetEvent responseReceived = new ManualResetEvent(false))
            {
                PackageReceived += (sender, e) =>
                {
                    // Entspricht das Kommandobyte der Antwort, dem der Anfrage?
                    if (e.ResponseCommandByte == package[0])
                    {
                        response = e.Package;
                        responseReceived.Set();
                    }
                };

                responseReceived.WaitOne();
            }

            if (response.Length != expectedPackageLength)
                Logger.Warning($"Paketlänge beträgt {response.Length}, aber {expectedPackageLength} erwartet.");

            return response;
        }

        private byte[] ReadPackage()
        {
            int packageLength = _serialPort.ReadByte();

            if (packageLength <= 0)
            {
                Logger.Error($"Fehler beim Lesen der Paketlänge von {_serialPort.PortName}.");
                return null;
            }

            byte[] package = new byte[packageLength];
            int bytesReceived = 0;

            while (bytesReceived < packageLength)
            {
                int received = _serialPort.Read(package, bytesReceived, packageLength - bytesReceived);

                if (received <= 0)
                {
                    Logger.Error($"Fehler beim Lesen des Paketes von {_serialPort.PortName}.");
                    return null;
                }

                bytesReceived += received;
            }

            return package;
        }

        private byte[] ReadPackage(byte expectedPackageLength)
        {
            byte[] package = ReadPackage();

            if (package.Length != expectedPackageLength)
                Logger.Warning($"Paketlänge beträgt {package.Length}, aber {expectedPackageLength} erwartet.");

            return package;
        }

        private void BeginReceive()
        {
            new Task(() =>
            {
                byte[] package;

                while (true)
                {
                    package = ReadPackage();

                    if (package == null)
                        continue;

                    byte responseCommandByte = package[0];
                    byte[] response = package.Skip(1).ToArray();

                    PackageReceived?.Invoke(this, new PackageReceivedEventArgs(responseCommandByte, response));
                }
            }).Start();
        }
    }
}
