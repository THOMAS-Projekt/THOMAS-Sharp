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
            new ModuleManager().Initialize();

            Console.WriteLine("Ich lebe!");

            Console.ReadKey();
        }
    }
}
