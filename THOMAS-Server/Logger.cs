using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace THOMASServer
{
    public static class Logger
    {
        public static bool DebugEnabled { get; set; } = true;

        public static void Debug(string message, [CallerFilePath] string filePath = null, [CallerMemberName]string memberName = null, [CallerLineNumber] int lineNumber = 0)
        {
            if (!DebugEnabled)
                return;

            WriteLog(ConsoleColor.DarkGreen, "DEBUG", message, filePath, memberName, lineNumber);
        }

        public static void Info(string message, [CallerFilePath] string filePath = null, [CallerMemberName]string memberName = null, [CallerLineNumber] int lineNumber = 0)
        {
            WriteLog(ConsoleColor.Cyan, "INFO", message, filePath, memberName, lineNumber);
        }

        public static void Warning(string message, [CallerFilePath] string filePath = null, [CallerMemberName]string memberName = null, [CallerLineNumber] int lineNumber = 0)
        {
            WriteLog(ConsoleColor.Yellow, "WARNING", message, filePath, memberName, lineNumber);
        }

        public static void Error(string message, [CallerFilePath] string filePath = null, [CallerMemberName]string memberName = null, [CallerLineNumber] int lineNumber = 0)
        {
            WriteLog(ConsoleColor.DarkRed, "ERROR", message, filePath, memberName, lineNumber);
        }

        public static void Wtf(string message, [CallerFilePath] string filePath = null, [CallerMemberName]string memberName = null, [CallerLineNumber] int lineNumber = 0)
        {
            WriteLog(ConsoleColor.Magenta, "WTF!?", $"WTF ARE YOU DOING?\n{message}", filePath, memberName, lineNumber);
        }

        private static void WriteLog(ConsoleColor color, string level, string message, string filePath, string memberName, int lineNumber)
        {
            string fileName = Path.GetFileName(filePath);

            string header = $"[{level} {fileName}, {memberName}:{lineNumber}] ";

            Console.ForegroundColor = color;
            Console.Write(header);
            Console.ResetColor();

            string[] lines = message.Split('\n');

            bool firstLine = true;
            foreach (string line in lines)
            {
                if (!firstLine)
                    for (int i = 0; i < header.Length; i++)
                        Console.Write(' ');

                Console.WriteLine(line);

                firstLine = false;
            }
        }
    }
}
