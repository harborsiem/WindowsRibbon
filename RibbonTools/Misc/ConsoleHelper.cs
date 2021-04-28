using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace UIRibbonTools
{
    static class ConsoleHelper
    {
        const int ERROR_ACCESS_DENIED = 5;

        public static bool Execute(string[] args)
        {
            //we can use it for a console build process
            //int errorCode = 0;
            if (args.Length == 0 || (args.Length == 1 && File.Exists(args[0])))
                return false;

            if (!NativeMethods.AttachConsole(NativeMethods.ATTACH_PARENT_PROCESS)) // && Marshal.GetLastWin32Error() == ERROR_ACCESS_DENIED)
            {
                if (NativeMethods.AllocConsole())
                {
                    return ExecuteConsole(args, false);
                }
                else
                {
                    MessageBox.Show("No Console possible");
                }
            }
            else
            {
                return ExecuteConsole(args, true);
            }
            return false;
        }

        private static bool ExecuteConsole(string[] args, bool attachedParent)
        {
            int errorCode = 0;
            bool withReadKey;
            try
            {
                withReadKey = ParseArgs(args);
                WriteLine();
                ConsoleKey key = ConsoleKey.NoName;
                if (!attachedParent && withReadKey)
                {
                    while (key == ConsoleKey.NoName)
                        key = Console.ReadKey().Key;
                }
            }
            finally
            {
                if (!attachedParent)
                {
                    NativeMethods.FreeConsole();
                }
                Environment.Exit(errorCode);
            }
            return true;
        }

        private static bool ParseArgs(string[] args)
        {
            if (args[0].Equals("/?", StringComparison.OrdinalIgnoreCase) || args[0].Equals("-h", StringComparison.OrdinalIgnoreCase) || args[0].Equals("--help", StringComparison.OrdinalIgnoreCase))
            {
                DisplayHelp();
                return true;
            }
            if (args.Length == 2) //@ parameter for resourceIdentifier ? or lastline comment with resourceIdentifier ?
            {
                if (args[1].Equals("--build", StringComparison.OrdinalIgnoreCase))
                {
                    BuildRibbon(args[0]);
                    return false;
                }
            }
            if (args.Length == 3) //@ parameter for resourceIdentifier ? or lastline comment with resourceIdentifier ?
            {
                if (args[2].Equals("--build", StringComparison.OrdinalIgnoreCase))
                {
                    BuildRibbon(args[0], args[1]);
                    return false;
                }
            }

            DisplayHelp();
            return true;
        }

        private static void BuildRibbon(string path, string resourceIdentifier = TRibbonObject.ApplicationDefaultName)
        {
            if (File.Exists(path))
            {
                Settings.Instance.Read(new System.Drawing.Size()); //We Read the Settings because we need the external Tools paths
                BuildPreviewHelper.ConsoleBuild(Addons.GetExactFilenameWithPath(path), resourceIdentifier);
            }
            else
            {
                WriteLine("Markup file doesn't exist");
            }
        }

        private static void DisplayHelp()
        {
            WriteLine();
            WriteLine();
            WriteLine("Build the ribbon files from the Xml-Markup file");
            WriteLine();
            WriteLine("Usage: RibbonTools [options]");
            WriteLine("Usage: RibbonTools [path - to - markup] [ResourceIdentifier] [options]");
            WriteLine();
            WriteLine("options:");
            WriteLine("  /?|-h|--help Display help.");
            WriteLine("  --build Build the ribbon files.");
            WriteLine();
            WriteLine("path-to-markup:");
            WriteLine("  The path to a ribbon markup file.");
            WriteLine("ResourceIdentifier:");
            WriteLine("  Optional parameter, don't use it with .NET");
            WriteLine();
            WriteLine("ResourceIdentifier is the Name parameter for the UICC.exe");
            WriteLine("ResourceIdentifier is sometimes called ResourceName");
        }

        private static void WriteLine(string line)
        {
            Console.WriteLine(line);
        }

        private static void WriteLine()
        {
            Console.WriteLine();
        }
    }
}
