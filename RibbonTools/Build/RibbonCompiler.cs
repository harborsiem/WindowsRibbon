using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace UIRibbonTools
{
    public enum RibbonCompileResult
    {
        Ok, NoFilename, NoTools, RibbonCompilerError,
        ResourceCompilerError, LinkError, HeaderConversionError,
        NoOutput, Exception
    };

    class RibbonCompiler
    {
        private const string Quote = "\"";
        private string _uiccPath = Settings.Instance.RibbonCompilerPath; //@"C:\Program Files (x86)\Windows Kits\10\bin\10.0.18362.0\x86\UICC.exe";
        private string _rcPath = Settings.Instance.ResourceCompilerPath; //@"C:\Program Files (x86)\Windows Kits\10\bin\10.0.18362.0\x86\rc.exe";
        private string _vcvarsPath = Settings.Instance.Vcvars32BatPath; //@"C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\VC\Auxiliary\Build\vcvars32.bat";
        private string _linkPath = Settings.Instance.LinkPath; //@"C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\VC\Tools\MSVC\14.25.28610\bin\Hostx86\x86\link.exe";
        private List<string> _messages = new List<string>();
        private Manager _manager;

        public RibbonCompiler(Manager manager)
        {
            _manager = manager;
        }

        public IList<string> Messages { get => _messages; }

        public string RibbonDll { get; private set; }

        public static string QuotedString(string value)
        {
            if (value != null)
                return Quote + value + Quote;
            return value;
        }

        //note: resourceName APPLICATION is converted by uicc.exe to APPLICATION_RIBBON to the resourcefile
        //that is the only allowed resourceName for .NET Ribbon
        //here called resourceIdentifier because we have a naming conflict with Ribbon.ResourceName
        //Ribbon.ResourceIdentifier is the name parameter for the UICC Compiler
        public RibbonCompileResult Compile(string filename, string resourceIdentifier)
        {
            RibbonCompileResult result = RibbonCompileResult.Ok;
            string docDir;
            string bmlFilename;
            string bmlFileParam;
            string headerFilename;
            string headerFileParam;
            string rcFilename;
            string rcFileParam;
            string nameParam;
            string resFilename;
            string dllFilename;

            RibbonDll = null;

            if (filename == null || !File.Exists(filename))
            {
                return RibbonCompileResult.NoFilename;
            }

            if (!Settings.Instance.ToolsAvailable())
            {
                return RibbonCompileResult.NoTools;
            }

            //if (!Settings.Instance.AllowChangingResourceName)
            //    resourceIdentifier = TRibbonObject.ApplicationDefaultName;

            docDir = Path.GetDirectoryName(filename);

            bmlFilename = Path.ChangeExtension(filename, ".bml");
            bmlFileParam = QuotedString(bmlFilename);
            headerFilename = Path.ChangeExtension(filename, ".h");
            headerFileParam = string.Format("\"/header:{0}\"", headerFilename);
            rcFilename = Path.ChangeExtension(filename, ".rc");
            rcFileParam = string.Format("\"/res:{0} \"", rcFilename);
            nameParam = string.Format("\"/name:{0}\"", resourceIdentifier);
            resFilename = Path.ChangeExtension(filename, ".res");
            dllFilename = Path.ChangeExtension(filename, ".ribbondll");

            _manager.CleanupFiles.AddRange(new string[] { bmlFilename, dllFilename });
            if (Settings.Instance.DeleteResFile)
            {
                _manager.CleanupFiles.Add(rcFilename);
                _manager.CleanupFiles.Add(resFilename);
            }

            // Run ribbon compiler UICC.exe to convert the markup XML to a header, a resource and a bml file.
            if (!Execute(_uiccPath, docDir, "\"/W0\"", QuotedString(filename), bmlFileParam, headerFileParam, rcFileParam, nameParam))
                result = RibbonCompileResult.RibbonCompilerError;

            if (result == RibbonCompileResult.Ok)
            {
                // Run the resource compiler, so that we can include the file into a .ribbon file by the Linker
                if (!Execute(_rcPath, docDir, QuotedString(rcFilename)))
                    result = RibbonCompileResult.ResourceCompilerError;
            }

            if (result == RibbonCompileResult.Ok)
            {

                //string batFile = BuildBatFile(docDir, "cmd", "/c", BuildLinkCommand("/NOENTRY", "/DLL", "/MACHINE:X86", "/OUT:" + QuotedString(dllFilePath), QuotedString(resFilePath)));

                //if (!ExecuteBat(batFile, docDir))
                //if (!Execute(@"cmd.exe", docDir, "/c", BuildLinkCommand("/NOENTRY", "/DLL", "/MACHINE:X86", "/OUT:" + QuotedString(dllFilePath), QuotedString(resFilePath))))
                if (!Execute(_linkPath, Path.GetDirectoryName(_linkPath), "/NOENTRY", "/DLL", "/MACHINE:X86", "/OUT:" + QuotedString(dllFilename), QuotedString(resFilename)))
                    //don't use /VERBOSE
                    result = RibbonCompileResult.LinkError;
                else
                    RibbonDll = dllFilename;

            }

            if (result != RibbonCompileResult.Ok)
            {
                if (!File.Exists(bmlFilename) || !File.Exists(rcFilename))
                    throw new FaildException("uicc.exe failed to generate .bml or .rc file!");
                if (!File.Exists(resFilename))
                    throw new FaildException("rc.exe failed to generate binary .res file!");
                if (!File.Exists(dllFilename))
                    throw new FaildException("link.exe failed to generate binary resource .dll file!");
            }

            return result;
        }

        private string BuildCommmands(params string[] parameter)
        {
            StringBuilder option = new StringBuilder();
            for (int i = 0; i < parameter.Length; i++)
            {
                if (i > 0)
                    option.Append(" ");
                option.Append(parameter[i]);
            }
            return option.ToString();
        }

        private bool Execute(string application, string currentDir, params string[] parameter)
        {
            string[] lines;
            string option = BuildCommmands(parameter);

            if (File.Exists(application))
            {
                ProcessStartInfo startInfo = new ProcessStartInfo(application);
                startInfo.RedirectStandardOutput = true;
                startInfo.RedirectStandardError = true;
                startInfo.UseShellExecute = false;
                startInfo.CreateNoWindow = true;
                startInfo.Arguments = option;
                startInfo.WorkingDirectory = currentDir;

                Process process = Process.Start(startInfo);
                process.WaitForExit();

                string allOutput = process.StandardOutput.ReadToEnd();
                lines = allOutput.Split(new[] { '\r', '\n' }, StringSplitOptions.None);
                _messages.AddRange(lines);
                return (process.ExitCode == 0);
            }
            return false;
        }

        //not used
        private string BuildLinkCommand(params string[] parameter)
        {
            StringBuilder result = new StringBuilder();
            string option = BuildCommmands(parameter);
            result.Append("\"(");
            result.Append(QuotedString(_vcvarsPath));
            result.Append(") && (");
            result.Append(QuotedString(_linkPath));
            result.Append(" ");
            result.Append(option);
            result.Append(")\"");
            return result.ToString();
        }

        //not used
        private string BuildBatFile(string currentDir, params string[] parameter)
        {
            string option = BuildCommmands(parameter);
            string result = Path.Combine(currentDir, "Link.bat");
            File.WriteAllText(result, option);
            return result;
        }

        //not used
        private bool ExecuteBat(string application, string currentDir, params string[] parameter)
        {
            string option = BuildCommmands(parameter);

            {
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo.FileName = application;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.CreateNoWindow = true;
                proc.OutputDataReceived += new System.Diagnostics.DataReceivedEventHandler(proc_OutputDataReceived);

                proc.Start();
                proc.BeginOutputReadLine();

                proc.WaitForExit();
                return (proc.ExitCode == 0);
            }
        }

        /// <summary>
        /// Forward the sub console window output to the message output
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void proc_OutputDataReceived(object sender, System.Diagnostics.DataReceivedEventArgs e)
        {
            _messages.Add(e.Data);
            //if (_output != null)
            //    _output.WriteLine(e.Data);
        }
    }
}
