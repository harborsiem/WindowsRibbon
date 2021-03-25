using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.IO;

namespace UIRibbonTools
{
    public sealed class AlphaBitmap
    {
        private AlphaBitmap() { }

        private static Bitmap TryConvertToAlphaBitmap(Bitmap bitmap)
        {
            if (bitmap.PixelFormat == PixelFormat.Format32bppRgb && bitmap.RawFormat.Guid == ImageFormat.Bmp.Guid)
            {
                int length = bitmap.Width * bitmap.Height;
                int[] bmpScan = new int[length];
                BitmapData bmpData = bitmap.LockBits(new Rectangle(new Point(), bitmap.Size), ImageLockMode.ReadOnly, bitmap.PixelFormat);
                Marshal.Copy(bmpData.Scan0, bmpScan, 0, length);
                bitmap.UnlockBits(bmpData);
                if (IsAnyAlpha(bmpScan))
                {
                    Bitmap alpha = new Bitmap(bitmap.Width, bitmap.Height, PixelFormat.Format32bppArgb);
                    BitmapData alphaData = alpha.LockBits(new Rectangle(new Point(), alpha.Size), ImageLockMode.ReadWrite, alpha.PixelFormat);
                    Marshal.Copy(bmpScan, 0, alphaData.Scan0, length);
                    alpha.UnlockBits(alphaData);
                    return alpha;
                }
            }
            return bitmap;
        }

        private static bool IsAnyAlpha(int[] bmpScan)
        {
            bool result = false;
            for (int i = 0; i < bmpScan.Length; i++)
            {
                if (((uint)bmpScan[i] & 0xff000000) < 0xff000000)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public static Bitmap TryCreateAlphaBitmap(Stream stream)
        {
            Bitmap bmp = new Bitmap(stream);
            return TryConvertToAlphaBitmap(bmp);
        }

        /// <summary>
        /// Load a Bitmap (with transparency) from file (*.bmp or *.png)
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>The Bitmap with fully transparency if available</returns>
        public static Bitmap TryCreateAlphaBitmap(string filename)
        {
            Bitmap bmp = new Bitmap(filename);
            return TryConvertToAlphaBitmap(bmp);
        }

        /// <summary>
        /// Load a Bitmap (with transparency) from file (*.bmp or *.png) via file content or Bitmap ctor
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="highContrast"></param>
        /// <returns>The Bitmap with fully transparency if available</returns>
        public static Bitmap TryAlphaBitmapFromFile(string fileName, bool highContrast = false)
        {
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentNullException(nameof(fileName));
            if (!File.Exists(fileName))
                throw new ArgumentException("File does not exist", nameof(fileName));
            Bitmap bitmap = new Bitmap(fileName);
            if (!highContrast)
            {
                bitmap = TryConvertToAlphaBitmap(bitmap);
            }
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

        private static byte[] IsBmpFile(string fileName)
        {
            byte[] bytes = File.ReadAllBytes(fileName);
            if (bytes.Length > 54)
            {
                string headerMark = Encoding.ASCII.GetString(bytes, 0, 2);
                if (headerMark.Equals("BM"))
                {
                    return bytes;
                }
            }
            return null;
        }

        /// <summary>
        /// Load a Bitmap (with transparency) from file (*.bmp or *.png) via file content or Bitmap ctor
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="highContrast"></param>
        /// <returns>The Bitmap with fully transparency if available</returns>
        public static Bitmap BitmapFromFile(string fileName, bool highContrast = false)
        {
            Bitmap bitmap = null;
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentNullException(nameof(fileName));
            if (!File.Exists(fileName))
                throw new ArgumentException("File does not exist", nameof(fileName));
            byte[] bytes = IsBmpFile(fileName);
            if (!highContrast && bytes != null)
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
                    return bitmap;
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

        /// <summary>
        /// Get the managed ARGB Bitmap
        /// </summary>
        /// <param name="hBitmap">Handle to a Bitmap</param>
        /// <returns>The Bitmap with fully transparency if available</returns>
        public static Bitmap GetManagedARGBBitmap(IntPtr hBitmap)
        {
            // Create the BITMAP structure and get info from our nativeHBitmap
            NativeMethods.BITMAP bitmapStruct = new NativeMethods.BITMAP();
            NativeMethods.GetObjectBitmap(hBitmap, Marshal.SizeOf(bitmapStruct), ref bitmapStruct);

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
                managedBitmap = Bitmap.FromHbitmap(hBitmap);
                managedBitmap.MakeTransparent();
            }
            return managedBitmap;
        }

        /// <summary>
        /// Load a Bitmap (with transparency) from file (*.bmp or *.png) via Windows API
        /// </summary>
        /// <param name="path"></param>
        /// <returns>The Bitmap with fully transparency if available</returns>
        public static Bitmap ImageFromFile(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));
            if (!File.Exists(path))
                throw new ArgumentException("File does not exist", nameof(path));
            char[] buffer = new char[2];
            StreamReader sr = File.OpenText(path);
            int length = sr.ReadBlock(buffer, 0, 2);
            long baseLength = sr.BaseStream.Length;
            sr.Close();
            if (baseLength > 54 && length == 2 && buffer[0] == 'B' && buffer[1] == 'M')
            {
                IntPtr handle = IntPtr.Zero;
                handle = NativeMethods.LoadImage(IntPtr.Zero, path, (uint)NativeMethods.ImageType.IMAGE_BITMAP, 0, 0,
                    (uint)(NativeMethods.ImageLoad.LR_LOADFROMFILE | NativeMethods.ImageLoad.LR_CREATEDIBSECTION));
                return GetManagedARGBBitmap(handle);
            }

            Bitmap bitmap = new Bitmap(path);
            if ((int)bitmap.HorizontalResolution != 96)
            {
                bitmap.SetResolution(96.0f, 96.0f); //only png bitmaps can have other resolution
            }
            return bitmap;
        }

        /// <summary>
        /// Set a RGB Color value if the alpha is 0, best guess is Color.LightGray.ToArgb() & 0xffffff
        /// </summary>
        /// <param name="bitmap">The Bitmap</param>
        /// <param name="transparentRGB">The RGB Color if alpha == 0</param>
        /// <returns>The converted Bitmap</returns>
        public static Bitmap SetTransparentRGB(Bitmap bitmap, int transparentRGB)
        {
            int x, y;
            IntPtr p;
            if (bitmap.PixelFormat != PixelFormat.Format32bppArgb)
                bitmap.MakeTransparent(bitmap.GetPixel(0, 0));
            BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);
            for (y = 0; y < bitmap.Height; y++)
            {
                p = data.Scan0 + y * 4 * bitmap.Width;
                for (x = 0; x < bitmap.Width; x++)
                {
                    uint value = (uint)Marshal.ReadInt32(p);
                    if ((value & 0xff000000) == 0)
                        Marshal.WriteInt32(p, transparentRGB);
                    p = p + 4;
                }
            }

            bitmap.UnlockBits(data);
            return bitmap;
        }


        class NativeMethods
        {

            public enum BitmapCompressionMode : uint
            {
                BI_RGB = 0,
                BI_RLE8 = 1,
                BI_RLE4 = 2,
                BI_BITFIELDS = 3,
                BI_JPEG = 4,
                BI_PNG = 5
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct BITMAPINFOHEADER
            {
                public uint biSize;
                public int biWidth;
                public int biHeight;
                public ushort biPlanes;
                public ushort biBitCount;
                public BitmapCompressionMode biCompression;
                public uint biSizeImage;
                public int biXPelsPerMeter;
                public int biYPelsPerMeter;
                public uint biClrUsed;
                public uint biClrImportant;

                public void Init()
                {
                    biSize = (uint)Marshal.SizeOf(this);
                }
            }

            [DllImport("gdi32", CharSet = CharSet.Auto, EntryPoint = "GetObject")]
            public static extern int GetObjectBitmap(IntPtr hObject, int nCount, ref BITMAP lpObject);

            /// <summary>
            /// The BITMAP structure defines the type, width, height, color format, and bit values of a bitmap.
            /// </summary>
            [Serializable]
            [StructLayout(LayoutKind.Sequential)]
            public struct BITMAP
            {
                /// <summary>
                /// The bitmap type. This member must be zero.
                /// </summary>
                public int bmType;

                /// <summary>
                /// The width, in pixels, of the bitmap. The width must be greater than zero.
                /// </summary>
                public int bmWidth;

                /// <summary>
                /// The height, in pixels, of the bitmap. The height must be greater than zero.
                /// </summary>
                public int bmHeight;

                /// <summary>
                /// The number of bytes in each scan line. This value must be divisible by 2, because the system assumes that the bit 
                /// values of a bitmap form an array that is word aligned.
                /// </summary>
                public int bmWidthBytes;

                /// <summary>
                /// The count of color planes.
                /// </summary>
                public short bmPlanes;

                /// <summary>
                /// The number of bits required to indicate the color of a pixel.
                /// </summary>
                public short bmBitsPixel;

                /// <summary>
                /// A pointer to the location of the bit values for the bitmap. The bmBits member must be a pointer to an array of 
                /// character (1-byte) values.
                /// </summary>
                public IntPtr bmBits;
            }

            [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
            public static extern IntPtr LoadImage(IntPtr hinst, string lpszName, uint uType,
                int cxDesired, int cyDesired, uint fuLoad);

            public enum ImageType
            {
                IMAGE_BITMAP = 0,
                IMAGE_ICON = 1,
                IMAGE_CURSOR = 2
            }

            [Flags]
            public enum ImageLoad
            {
                LR_CREATEDIBSECTION = 0x00002000,
                //When the uType parameter specifies IMAGE_BITMAP, causes the function to return a DIB section bitmap rather than a compatible bitmap. This flag is useful for loading a bitmap without mapping it to the colors of the display device. 
                LR_DEFAULTCOLOR = 0x00000000,
                //The default flag; it does nothing. All it means is "not LR_MONOCHROME". 
                LR_DEFAULTSIZE = 0x00000040,
                //Uses the width or height specified by the system metric values for cursors or icons, if the cxDesired or cyDesired values are set to zero. If this flag is not specified and cxDesired and cyDesired are set to zero, the function uses the actual resource size. If the resource contains multiple images, the function uses the size of the first image. 
                LR_LOADFROMFILE = 0x00000010,
                //Loads the stand-alone image from the file specified by lpszName (icon, cursor, or bitmap file). 
                LR_LOADMAP3DCOLORS = 0x00001000,
                //Searches the color table for the image and replaces the following shades of gray with the corresponding 3-D color.
                //Dk Gray, RGB(128,128,128) with COLOR_3DSHADOW
                //Gray, RGB(192,192,192) with COLOR_3DFACE
                //Lt Gray, RGB(223, 223, 223) with COLOR_3DLIGHT
                //Do not use this option if you are loading a bitmap with a color depth greater than 8bpp.
                LR_LOADTRANSPARENT = 0x00000020,
                //Retrieves the color value of the first pixel in the image and replaces the corresponding entry in the color table with the default window color (COLOR_WINDOW). All pixels in the image that use that entry become the default window color. This value applies only to images that have corresponding color tables.
                //Do not use this option if you are loading a bitmap with a color depth greater than 8bpp.
                //If fuLoad includes both the LR_LOADTRANSPARENT and LR_LOADMAP3DCOLORS values, LR_LOADTRANSPARENT takes precedence.However, the color table entry is replaced with COLOR_3DFACE rather than COLOR_WINDOW.
                LR_MONOCHROME = 0x00000001,
                //Loads the image in black and white.
                LR_SHARED = 0x00008000,
                //Shares the image handle if the image is loaded multiple times.If LR_SHARED is not set, a second call to LoadImage for the same resource will load the image again and return a different handle.
                //When you use this flag, the system will destroy the resource when it is no longer needed.
                //Do not use LR_SHARED for images that have non-standard sizes, that may change after loading, or that are loaded from a file.
                //When loading a system icon or cursor, you must use LR_SHARED or the function will fail to load the resource.
                //This function finds the first image in the cache with the requested resource name, regardless of the size requested.
                LR_VGACOLOR = 0x00000080
            }
        }
    }
}
