using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;

namespace UIRibbonTools
{
    static class ConsoleHelper
    {
        const int ERROR_ACCESS_DENIED = 5;

        public static bool Execute(string[] args)
        {
            //we can use it for a console build process
            int errorCode = 0;
            if (args.Length == 0 || (args.Length == 1 && File.Exists(args[0])))
                return false;
            //if (!NativeMethods.AttachConsole(NativeMethods.ATTACH_PARENT_PROCESS) && Marshal.GetLastWin32Error() == ERROR_ACCESS_DENIED)
            {
                if (NativeMethods.AllocConsole())
                {
                    try
                    {
                        ParseArgs(args);
                        Console.WriteLine();
                        ConsoleKey key = ConsoleKey.NoName;
                        while (key == ConsoleKey.NoName)
                            key = Console.ReadKey().Key;
                    }
                    finally
                    {
                        NativeMethods.FreeConsole();
                        Environment.Exit(errorCode);
                    }
                    return true;
                }
                else
                {
                    MessageBox.Show("No Console possible");
                }
            }
            return false;
        }

        private static void ParseArgs(string[] args)
        {
            if (args[0].Equals("/?", StringComparison.OrdinalIgnoreCase) || args[0].Equals("-h", StringComparison.OrdinalIgnoreCase) || args[0].Equals("--help", StringComparison.OrdinalIgnoreCase))
            {
                DisplayHelp();
                return;
            }
            if (args.Length == 2)
            {
                if (args[1].Equals("--build", StringComparison.OrdinalIgnoreCase))
                {
                    BuildRibbon(args[0]);
                    return;
                }
            }
            DisplayHelp();
        }

        private static void BuildRibbon(string path)
        {
            if (File.Exists(path))
            {
                BuildPreviewHelper.ConsoleBuild(path);
            }
            else
            {
                Console.WriteLine("Markup file doesn't exist");
            }
        }

        private static void DisplayHelp()
        {
            Console.WriteLine();
            Console.WriteLine("Build the ribbon files from the Markup file");
            Console.WriteLine();
            Console.WriteLine("Usage: UIRibbonTools [options]");
            Console.WriteLine("Usage: UIRibbonTools [path - to - markup][options]");
            Console.WriteLine();
            Console.WriteLine("Options:");
            Console.WriteLine("  /?|-h|--help Display help.");
            Console.WriteLine("  --build Build the ribbon files.");
            Console.WriteLine();
            Console.WriteLine("path-to-markup:");
            Console.WriteLine("  The path to a ribbon markup file.");
        }
    }
}
