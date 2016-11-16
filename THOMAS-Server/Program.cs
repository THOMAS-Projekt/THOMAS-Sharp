using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using THOMASServer.Pools;

namespace THOMASServer
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            Logger.Info("Starten...");

            new ModuleManager().Initialize();

            Logger.Info("Bereit.");

            // TODO: Minimale konsole
            Console.ReadLine();
        }
    }
}
