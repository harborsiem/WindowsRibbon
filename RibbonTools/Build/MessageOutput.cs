using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace UIRibbonTools
{
    /// <summary>
    /// Writes the output to the console window.
    /// </summary>
    class MessageOutput : IMessageOutput, IDisposable
    {
        private StringBuilder _sb;
        private StringWriter _sw;

        public MessageOutput()
        {
            _sb = new StringBuilder();
            _sw = new StringWriter(_sb);
        }

        public void WriteLine(string value)
        {
            _sw.WriteLine(value);
        }

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
