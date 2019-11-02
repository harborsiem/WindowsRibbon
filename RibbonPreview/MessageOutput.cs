using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using RibbonGenerator;

namespace RibbonPreview
{
    /// <summary>
    /// Writes the output to the console window.
    /// </summary>
    class MessageOutput : IMessageOutput, IDisposable
    {
        StringBuilder sb;
        StringWriter sw;

        public MessageOutput()
        {
            sb = new StringBuilder();
            sw = new StringWriter(sb);
        }

        public void WriteLine(string value)
        {
            sw.WriteLine(value);
        }

        public string GetString()
        {
            return sb.ToString();
        }

        public void Close()
        {
            Dispose();
        }

        public void Dispose()
        {
            sw.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
