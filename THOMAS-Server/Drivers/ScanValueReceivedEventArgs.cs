using System;

namespace THOMASServer.Drivers
{
    public class ScanValueReceivedEventArgs : EventArgs
    {
        public float Angle { get; }
        public int Distance { get; }

        public ScanValueReceivedEventArgs(float angle, int distance)
        {
            Angle = angle;
            Distance = distance;
        }
    }
}