using System;

namespace THOMASServer.Drivers.Base
{
    public class PackageReceivedEventArgs : EventArgs
    {
        public byte ResponseCommandByte { get; }
        public byte[] Package { get; }

        public PackageReceivedEventArgs(byte responseCommandByte, byte[] package)
        {
            ResponseCommandByte = responseCommandByte;
            Package = package;
        }
    }
}