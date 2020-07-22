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
        public static int EnsureRange(int value, int min, int max)
        {
            if (value >= min && value <= max)
                return value;
            if (value < min)
                return min;
            return max;
        }

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

        public static bool IsBmpFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentNullException(nameof(fileName));
            if (File.Exists(fileName))
            {
                byte[] bytes = File.ReadAllBytes(fileName);
                if (bytes.Length > 54)
                {
                    string headerMark = Encoding.ASCII.GetString(bytes, 0, 2);
                    if (headerMark.Equals("BM"))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static Bitmap BitmapFromFile(string fileName, bool highContrast = false)
        {
            Bitmap bitmap = null;
            if (!highContrast && !Path.GetExtension(fileName).Equals(".PNG", StringComparison.OrdinalIgnoreCase))
            {
                byte[] bytes = File.ReadAllBytes(fileName);
                string headerMark = Encoding.ASCII.GetString(bytes, 0, 2);
                if (headerMark.Equals("BM"))
                {
                    int offBits = BitConverter.ToInt32(bytes, 10); //normally 54
                    int width = BitConverter.ToInt32(bytes, 18);
                    int height = BitConverter.ToInt32(bytes, 22);
                    int length = BitConverter.ToInt16(bytes, 2) - offBits;
                    NativeMethods.BitmapCompressionMode compression = (NativeMethods.BitmapCompressionMode)BitConverter.ToUInt32(bytes, 30);
                    short bitCount = BitConverter.ToInt16(bytes, 28);
                    //Make some tests if it is a ARGB Bitmap
                    if (bitCount == 32 && compression == NativeMethods.BitmapCompressionMode.BI_RGB)
                    {
                        GCHandle gcH = GCHandle.Alloc(bytes, GCHandleType.Pinned);
                        IntPtr scan0 = gcH.AddrOfPinnedObject() + offBits;
                        bitmap = new Bitmap(width, height, 4 * width, PixelFormat.Format32bppArgb, scan0);
                        if (height > 0)
                            bitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);
                        gcH.Free();
                        //scan0 is copied to other location, we can test it here
                        //BitmapData data = bitmap.LockBits(new Rectangle(new Point(), bitmap.Size), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
                        //IntPtr ptr = data.Scan0;
                        //bitmap.UnlockBits(data);
                        return bitmap;
                    }
                }
            }
            //What shall we do with .png files or .bmp files with smaller formats ?
            bitmap = new Bitmap(fileName);
            if (!highContrast)
                if (!(bitmap.PixelFormat == PixelFormat.Format32bppArgb || bitmap.PixelFormat == PixelFormat.Format32bppPArgb))
                    bitmap.MakeTransparent(bitmap.GetPixel(0, 0));
                else
                {
                    if ((int)bitmap.HorizontalResolution != 96)
                    {
                        bitmap.SetResolution(96.0f, 96.0f); //only png bitmaps can have other resolution
                    }
                }
            return bitmap;
        }

        public static Bitmap GetManagedARGBBitmap(IntPtr hBmp)
        {
            // Create the BITMAP structure and get info from our nativeHBitmap
            NativeMethods.BITMAP bitmapStruct = new NativeMethods.BITMAP();
            NativeMethods.GetObjectBitmap(hBmp, Marshal.SizeOf(bitmapStruct), ref bitmapStruct);

            Bitmap managedBitmap;
            if (bitmapStruct.bmBitsPixel == 32)
            {
                // Create the managed bitmap using the pointer to the pixel data of the native HBitmap
                managedBitmap = new Bitmap(
                    bitmapStruct.bmWidth, bitmapStruct.bmHeight, bitmapStruct.bmWidthBytes, PixelFormat.Format32bppArgb, bitmapStruct.bmBits);
                if (bitmapStruct.bmHeight > 0)
                    managedBitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);
            }
            else
            {
                managedBitmap = Bitmap.FromHbitmap(hBmp);
                managedBitmap.MakeTransparent();
            }
            return managedBitmap;
        }

        public static Bitmap ImageFromFile(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));
            IntPtr handle = IntPtr.Zero;
            handle = NativeMethods.LoadImage(IntPtr.Zero, path, (uint)NativeMethods.ImageType.IMAGE_BITMAP, 0, 0,
                (uint)(NativeMethods.ImageLoad.LR_LOADFROMFILE | NativeMethods.ImageLoad.LR_CREATEDIBSECTION));
            return GetManagedARGBBitmap(handle);
        }
    }
}
