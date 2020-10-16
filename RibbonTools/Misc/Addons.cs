using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace UIRibbonTools
{
    public class Addons
    {
        public static string GetExactFilenameWithPath(string path)
        {
            path = Path.GetFullPath(path);
            string filename = Path.GetFileName(path);
            if (File.Exists(path))
            {
                string dir = Path.GetDirectoryName(path);
                string[] strArray = Directory.GetFileSystemEntries(dir, filename, SearchOption.TopDirectoryOnly);
                if (strArray.Length > 0)
                    return strArray[0];
            }
            return string.Empty;
        }

        public static void ForceDirectories(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException("Can't create directory", nameof(path));
            if (path.Length < 3 || Directory.Exists(path))
                return;
            Directory.CreateDirectory(path);
        }

        public static bool StartsText(string basePath, string pathTo)
        {
            if (basePath.Length > pathTo.Length)
                return false;
            string subDir = pathTo.Substring(0, basePath.Length);
            return (basePath.Equals(subDir, StringComparison.OrdinalIgnoreCase));
        }

        public static bool SameText(string string1, string string2)
        {
            return !((!string.IsNullOrEmpty(string1)) && (!string.Equals(string1, string2, StringComparison.OrdinalIgnoreCase)));
        }
    }
}
