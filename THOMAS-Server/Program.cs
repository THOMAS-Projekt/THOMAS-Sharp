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
        private const string FileName = "config.xml";

        private static void Main(string[] args)
        { 
            Logger.Info("Starten...");

            ApplicationConfig applicationConfig = new ApplicationConfig(FileName);

            Logger.DebugEnabled = applicationConfig.Debug;

            Logger.Debug("HI!");
            new ModuleManager(applicationConfig).Initialize();

            Logger.Info("Bereit.");

            // TODO: Minimale konsole
            Console.ReadLine();
        }
    }
}
