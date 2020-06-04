using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace UIRibbonTools
{
    /// <summary>
    /// Writes the output to the console window.
    /// </summary>
    class ConsoleMessageOutput : IMessageOutput, IDisposable
    {
        private StringBuilder _sb;
        private StringWriter _sw;

        public ConsoleMessageOutput()
        {
            _sb = new StringBuilder();
            _sw = new StringWriter(_sb);
        }

        #region IOutput Members

        public void WriteLine(string value)
        {
            _sw.WriteLine(value);
            System.Console.WriteLine(value);
        }

        #endregion

        public string GetString()
        {
            return _sb.ToString();
        }

        public void Close()
        {
            Dispose();
        }

        public void Dispose()
        {
            _sw.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
