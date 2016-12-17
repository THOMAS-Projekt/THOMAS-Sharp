using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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

        public bool IsScanning { get; private set; } = false;

        private readonly ArduinoLidarDriver _arduinoLidarDriver;

        public LidarActor(ArduinoLidarDriver arduinoLidarDriver)
        {
            _arduinoLidarDriver = arduinoLidarDriver;
        }

        public bool StartScan(int roundsPerSecond)
        {
            if (IsScanning)
                return true;

            if (!_arduinoLidarDriver.StartScan(roundsPerSecond))
                return false;

            IsScanning = true;

            return true;
        }

        public bool StopScan()
        {
            if (!IsScanning)
                return true;

            if (!_arduinoLidarDriver.StopScan())
                return false;

            IsScanning = false;

            return true;
        }

        public void SaveScansToFile(string filename, int roundsPerSecond, int numberOfScans)
        {
            if (!StartScan(roundsPerSecond))
                return;

            StreamWriter scanFileWriter;

            try
            {
                scanFileWriter = new StreamWriter(filename, false);
            }
            catch (Exception ex)
            {
                Logger.Error($"Die Datei {filename} konnte nicht zum Schreiben geöffnet werden: {ex}");
                return;
            }
            
            int remainingScans = numberOfScans;
            float lastAngle = -1;

            bool isWriting = false;

            EventHandler<ScanValueReceivedEventArgs> writingHandler = null;
            writingHandler = (sender, e) =>
            {
                if (isWriting)
                    scanFileWriter.WriteLine($"{e.Angle};{e.Distance}");

                if (lastAngle != -1 && e.Angle < lastAngle)
                {
                    if (isWriting)
                    {
                        remainingScans--;
                    }
                    else
                    {
                        Logger.Info($"Beginne mit Schreiben von {numberOfScans} in die Datei {filename}");
                        isWriting = true;
                    }

                    if (remainingScans <= 0)
                    {
                        scanFileWriter.Flush();
                        scanFileWriter.Close();
                        scanFileWriter.Dispose();

                        _arduinoLidarDriver.ScanValueReceived -= writingHandler;

                        Logger.Info($"Schreiben der Datei {filename} abgeschlossen.");
                    }
                }

                lastAngle = e.Angle;
            };

            _arduinoLidarDriver.ScanValueReceived += writingHandler;
        }
    }
}
