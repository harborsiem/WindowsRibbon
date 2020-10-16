using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace UIRibbonTools
{
    public sealed class UIImage : IUIImage
    {
        static IUIImageFromBitmap s_imageFactory;
        static UIImage()
        {
            s_imageFactory = new UIRibbonImageFromBitmapFactory() as IUIImageFromBitmap;
        }

        private IUIImage _uiImage;
        private IntPtr _bitmapHandle; //HBitmap
        private int _width;
        private int _height;
        private int _bitsPerPixel;

        // Width of the image
        public int Width { get => _width; }

        // Height of the image
        public int Height { get => _height; }

        // Number of bits per pixel for the bitmap image. When this value equals 32,
        //this means that the bitmap has an alpha channel.
        public int BitsPerPixel { get => _bitsPerPixel; }

        // Low-level handle to the image
        public IUIImage Handle { get => _uiImage; }

        // Bitmap handle
        public IntPtr BitmapHandle
        {
            get
            {
                IntPtr bmap;
                GetBitmap(out bmap);
                return bmap;
            }
        } // HBitmap

        public UIImage(string fileName, bool highContrast = false)
        {
            Load(fileName, highContrast);
        }

        //public void Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}

        //private void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {

        //    }
        //    Marshal.ReleaseComObject(s_imageFactory);
        //    s_imageFactory = null;
        //}

        public static bool IsBmpFile(string fileName)
        {
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

            // Create the managed bitmap using the pointer to the pixel data of the native HBitmap
            Bitmap managedBitmap = new Bitmap(
                bitmapStruct.bmWidth, bitmapStruct.bmHeight, bitmapStruct.bmWidthBytes, PixelFormat.Format32bppArgb, bitmapStruct.bmBits);
            if (bitmapStruct.bmHeight > 0)
                managedBitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);
            return managedBitmap;
        }

        public void Load(string fileName, bool highContrast = false)
        {
            _uiImage = null;
            _bitmapHandle = IntPtr.Zero;
            if (s_imageFactory != null)
            {
                Bitmap bitmap = null;
                try
                {
                    bitmap = BitmapFromFile(fileName, highContrast);
                    if (!highContrast)
                        ConvertToAlphaBitmap(bitmap);
                    s_imageFactory.CreateImage(bitmap.GetHbitmap(), Ownership.Copy, out _uiImage);
                    _uiImage.GetBitmap(out _bitmapHandle);
                }
                finally
                {
                    if (bitmap != null)
                        bitmap.Dispose();
                }
            }
            GetBitmapProperties();
            DoChanged();
        }

        void GetBitmapProperties()
        {
            if (_bitmapHandle != IntPtr.Zero)
            {
                NativeMethods.BITMAP bmpScreen = new NativeMethods.BITMAP();
                GCHandle hndl = GCHandle.Alloc(bmpScreen, GCHandleType.Pinned);
                IntPtr ptrToBitmap = hndl.AddrOfPinnedObject();
                NativeMethods.GetObject(_bitmapHandle, Marshal.SizeOf<NativeMethods.BITMAP>(), ptrToBitmap);
                bmpScreen = Marshal.PtrToStructure<NativeMethods.BITMAP>(ptrToBitmap);
                _width = bmpScreen.bmWidth;
                _height = bmpScreen.bmHeight;
                _bitsPerPixel = bmpScreen.bmBitsPixel;
                hndl.Free();

                //Bitmap bitmap = Bitmap.FromHbitmap(_bitmapHandle);
                //_width = bitmap.Width;
                //_height = bitmap.Height;
                //_bitsPerPixel = Image.GetPixelFormatSize(bitmap.PixelFormat);
            }
        }

        void DoChanged()
        {

        }

        private void ConvertToAlphaBitmap(Bitmap bitmap)
        {
            uint transparentColor;
            int x, y;
            IntPtr p;

            if ((bitmap.PixelFormat == PixelFormat.Format32bppArgb) || (bitmap.Width == 0) || (bitmap.Height == 0))
                return;
            BitmapData data = bitmap.LockBits(new Rectangle(new Point(), bitmap.Size), ImageLockMode.ReadWrite, bitmap.PixelFormat);
            data.PixelFormat = PixelFormat.Format32bppArgb;
            transparentColor = (uint)Marshal.ReadInt32(data.Scan0);
            for (y = 0; y < bitmap.Height; y++)
            {
                p = data.Scan0 + y * 4 * bitmap.Width;
                for (x = 0; x < bitmap.Width; x++)
                {
                    uint value = (uint)Marshal.ReadInt32(p);
                    if (value == transparentColor)
                        Marshal.WriteInt32(p, 0);
                    else
                        Marshal.WriteInt32(p, unchecked((int)(0xff000000 | value)));
                    p = p + 4;
                }
            }
            bitmap.UnlockBits(data);
        }

        private IntPtr CreatePreMultipliedBitmap(IntPtr bitmap)
        {
            IntPtr DC;
            NativeMethods.BITMAPINFO info;
            int i, width, height, A, R, G, B;
            int p;
            IntPtr data = IntPtr.Zero;
            IntPtr dataPlusP;
            IntPtr result = IntPtr.Zero;
            DC = NativeMethods.CreateCompatibleDC(IntPtr.Zero);
            try
            {
                info = new NativeMethods.BITMAPINFO(); // FillChar(Info, sizeof(Info), 0);
                info.bmiHeader.Init();
                if (NativeMethods.GetDIBits(DC, BitmapHandle, 0, 0, null, ref info, NativeMethods.DIB_Color_Mode.DIB_RGB_COLORS) != 0)
                {
                    width = info.bmiHeader.biWidth;
                    height = Math.Abs(info.bmiHeader.biHeight);
                    info.bmiHeader.biHeight = -height;
                    data = Marshal.AllocHGlobal(width * height * 4);
                    if (NativeMethods.GetDIBits(DC, BitmapHandle, 0, (uint)height, data, ref info, NativeMethods.DIB_Color_Mode.DIB_RGB_COLORS) != 0)
                    {
                        p = 0;
                        uint value;
                        for (i = 0; i < (width * height); i++)
                        {
                            dataPlusP = data + p;
                            value = (uint)Marshal.ReadInt32(dataPlusP);
                            A = (int)(value >> 24);
                            R = (int)(value >> 16) & 0xff;
                            G = (int)(value >> 8) & 0xff;
                            B = (int)value & 0xff;

                            //This should be: R= (R * A) div 255, but this is much faster and
                            //good enough for display purposes.
                            R = (A * R + 255) >> 8;
                            G = (A * G + 255) >> 8;
                            B = (A * B + 255) >> 8;

                            value = (uint)((A << 24) | (R << 16) | (G << 8) | B);
                            Marshal.WriteInt32(dataPlusP, unchecked((int)value));
                            p += 4;
                        }

                        result = NativeMethods.CreateBitmap(width, height, 1, 32, data);
                    }
                }
            }
            finally
            {
                NativeMethods.DeleteDC(DC);
                if (data != IntPtr.Zero)
                    Marshal.FreeHGlobal(data);
            }
            return result;
        }

        public void Draw(Graphics target, int xTarget, int yTarget, int wTarget, int hTarget)
        {
            IntPtr srcDC; // HDC;
            IntPtr bitmap, oldBitmap; // HBitmap;
            NativeMethods.BLENDFUNCTION BF;

            GetBitmap(out bitmap);
            if (bitmap == IntPtr.Zero)
                return;

            srcDC = NativeMethods.CreateCompatibleDC(IntPtr.Zero);
            try
            {
                if (_bitsPerPixel == 32)
                {
                    //AlphaBlend requires that the bitmap is pre - multiplied with the Alpha
                    //values.
                    bitmap = CreatePreMultipliedBitmap(bitmap);
                    if (bitmap == IntPtr.Zero)
                        return;
                    try
                    {
                        oldBitmap = NativeMethods.SelectObject(srcDC, bitmap);
                        BF = new NativeMethods.BLENDFUNCTION(NativeMethods.AC_SRC_OVER, 0, 255, NativeMethods.AC_SRC_ALPHA);
                        //BF.BlendOp = NativeMethods.AC_SRC_OVER;
                        //BF.BlendFlags = 0;
                        //BF.SourceConstantAlpha = 255;
                        //BF.AlphaFormat = NativeMethods.AC_SRC_ALPHA;
                        NativeMethods.AlphaBlend(target.GetHdc(), xTarget, yTarget, wTarget, hTarget,
                          srcDC, 0, 0, _width, _height, BF);
                        NativeMethods.SelectObject(srcDC, oldBitmap);
                    }
                    finally
                    {
                        NativeMethods.DeleteObject(bitmap);
                        target.ReleaseHdc();
                    }
                }
                else
                {
                    oldBitmap = NativeMethods.SelectObject(srcDC, bitmap);
                    NativeMethods.BitBlt(target.GetHdc(), xTarget, yTarget, _width, _height, srcDC, 0, 0, NativeMethods.TernaryRasterOperations.SRCCOPY);
                    NativeMethods.SelectObject(srcDC, oldBitmap);
                    target.ReleaseHdc();
                }
            }
            finally
            {
                NativeMethods.DeleteDC(srcDC);
            }
        }

        public void Draw(Graphics target, int xTarget, int yTarget)
        {
            Draw(target, xTarget, yTarget, _width, _height);
        }

        public HRESULT GetBitmap(out IntPtr bitmap)
        {
            if (_bitmapHandle != IntPtr.Zero)
            {
                bitmap = _bitmapHandle;
            }
            else if (_uiImage != null)
            {
                _uiImage.GetBitmap(out _bitmapHandle);
                bitmap = _bitmapHandle;
            }
            else
            {
                bitmap = IntPtr.Zero;
            }
            return HRESULT.S_OK;
        }

        class NativeMethods
        {
            /// <summary>
            ///        Creates a memory device context (DC) compatible with the specified device.
            /// </summary>
            /// <param name="hdc">A handle to an existing DC. If this handle is NULL,
            ///        the function creates a memory DC compatible with the application's current screen.</param>
            /// <returns>
            ///        If the function succeeds, the return value is the handle to a memory DC.
            ///        If the function fails, the return value is <see cref="System.IntPtr.Zero"/>.
            /// </returns>
            [DllImport("gdi32.dll", EntryPoint = "CreateCompatibleDC", SetLastError = true)]
            public static extern IntPtr CreateCompatibleDC([In] IntPtr hdc);

            [StructLayout(LayoutKind.Sequential, Pack = 1)]
            public struct RGBQUAD
            {
                public byte rgbBlue;
                public byte rgbGreen;
                public byte rgbRed;
                public byte rgbReserved;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct BITMAPINFO
            {
                public BITMAPINFOHEADER bmiHeader;
                [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
                public RGBQUAD[] bmiColors;
            }

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

            public enum DIB_Color_Mode : uint
            {
                DIB_RGB_COLORS = 0,
                DIB_PAL_COLORS = 1
            }

            /// <summary>
            ///        Retrieves the bits of the specified compatible bitmap and copies them into a buffer as a DIB using the specified format.
            /// </summary>
            /// <param name="hdc">A handle to the device context.</param>
            /// <param name="hbmp">A handle to the bitmap. This must be a compatible bitmap (DDB).</param>
            /// <param name="uStartScan">The first scan line to retrieve.</param>
            /// <param name="cScanLines">The number of scan lines to retrieve.</param>
            /// <param name="lpvBits">A pointer to a buffer to receive the bitmap data. If this parameter is <see cref="IntPtr.Zero"/>, the function passes the dimensions and format of the bitmap to the <see cref="BITMAPINFO"/> structure pointed to by the <paramref name="lpbi"/> parameter.</param>
            /// <param name="lpbi">A pointer to a <see cref="BITMAPINFO"/> structure that specifies the desired format for the DIB data.</param>
            /// <param name="uUsage">The format of the bmiColors member of the <see cref="BITMAPINFO"/> structure. It must be one of the following values.</param>
            /// <returns>If the lpvBits parameter is non-NULL and the function succeeds, the return value is the number of scan lines copied from the bitmap.
            /// If the lpvBits parameter is NULL and GetDIBits successfully fills the <see cref="BITMAPINFO"/> structure, the return value is nonzero.
            /// If the function fails, the return value is zero.
            /// This function can return the following value: ERROR_INVALID_PARAMETER (87 (0×57))</returns>
            [DllImport("gdi32.dll", EntryPoint = "GetDIBits")]
            public static extern int GetDIBits([In] IntPtr hdc, [In] IntPtr hbmp, uint uStartScan, uint cScanLines, [Out] byte[] lpvBits, [In, Out] ref BITMAPINFO lpbi, DIB_Color_Mode uUsage);

            [DllImport("gdi32.dll", EntryPoint = "GetDIBits")]
            public static extern int GetDIBits([In] IntPtr hdc, [In] IntPtr hbmp, uint uStartScan, uint cScanLines, [Out] IntPtr lpvBits, ref BITMAPINFO lpbi, DIB_Color_Mode uUsage);

            /// <summary>Selects an object into the specified device context (DC). The new object replaces the previous object of the same type.</summary>
            /// <param name="hdc">A handle to the DC.</param>
            /// <param name="hgdiobj">A handle to the object to be selected.</param>
            /// <returns>
            ///   <para>If the selected object is not a region and the function succeeds, the return value is a handle to the object being replaced. If the selected object is a region and the function succeeds, the return value is one of the following values.</para>
            ///   <para>SIMPLEREGION - Region consists of a single rectangle.</para>
            ///   <para>COMPLEXREGION - Region consists of more than one rectangle.</para>
            ///   <para>NULLREGION - Region is empty.</para>
            ///   <para>If an error occurs and the selected object is not a region, the return value is <c>NULL</c>. Otherwise, it is <c>HGDI_ERROR</c>.</para>
            /// </returns>
            /// <remarks>
            ///   <para>This function returns the previously selected object of the specified type. An application should always replace a new object with the original, default object after it has finished drawing with the new object.</para>
            ///   <para>An application cannot select a single bitmap into more than one DC at a time.</para>
            ///   <para>ICM: If the object being selected is a brush or a pen, color management is performed.</para>
            /// </remarks>
            [DllImport("gdi32.dll", EntryPoint = "SelectObject")]
            public static extern IntPtr SelectObject([In] IntPtr hdc, [In] IntPtr hgdiobj);

            [StructLayout(LayoutKind.Sequential, Pack = 1)]
            public struct BLENDFUNCTION
            {
                byte BlendOp;
                byte BlendFlags;
                byte SourceConstantAlpha;
                byte AlphaFormat;

                public BLENDFUNCTION(byte op, byte flags, byte alpha, byte format)
                {
                    BlendOp = op;
                    BlendFlags = flags;
                    SourceConstantAlpha = alpha;
                    AlphaFormat = format;
                }
            }

            //
            // currently defined blend operation
            //
            public const int AC_SRC_OVER = 0x00;

            //
            // currently defined alpha format
            //
            public const int AC_SRC_ALPHA = 0x01;

            [DllImport("Msimg32.dll", EntryPoint = "AlphaBlend")] //"gdi32.dll"
            public static extern bool AlphaBlend(IntPtr hdcDest, int nXOriginDest, int nYOriginDest,
                int nWidthDest, int nHeightDest,
                IntPtr hdcSrc, int nXOriginSrc, int nYOriginSrc, int nWidthSrc, int nHeightSrc,
                BLENDFUNCTION blendFunction);

            /// <summary>Deletes a logical pen, brush, font, bitmap, region, or palette, freeing all system resources associated with the object. After the object is deleted, the specified handle is no longer valid.</summary>
            /// <param name="hObject">A handle to a logical pen, brush, font, bitmap, region, or palette.</param>
            /// <returns>
            ///   <para>If the function succeeds, the return value is nonzero.</para>
            ///   <para>If the specified handle is not valid or is currently selected into a DC, the return value is zero.</para>
            /// </returns>
            /// <remarks>
            ///   <para>Do not delete a drawing object (pen or brush) while it is still selected into a DC.</para>
            ///   <para>When a pattern brush is deleted, the bitmap associated with the brush is not deleted. The bitmap must be deleted independently.</para>
            /// </remarks>
            [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool DeleteObject([In] IntPtr hObject);

            /// <summary>
            ///    Performs a bit-block transfer of the color data corresponding to a
            ///    rectangle of pixels from the specified source device context into
            ///    a destination device context.
            /// </summary>
            /// <param name="hdc">Handle to the destination device context.</param>
            /// <param name="nXDest">The leftmost x-coordinate of the destination rectangle (in pixels).</param>
            /// <param name="nYDest">The topmost y-coordinate of the destination rectangle (in pixels).</param>
            /// <param name="nWidth">The width of the source and destination rectangles (in pixels).</param>
            /// <param name="nHeight">The height of the source and the destination rectangles (in pixels).</param>
            /// <param name="hdcSrc">Handle to the source device context.</param>
            /// <param name="nXSrc">The leftmost x-coordinate of the source rectangle (in pixels).</param>
            /// <param name="nYSrc">The topmost y-coordinate of the source rectangle (in pixels).</param>
            /// <param name="dwRop">A raster-operation code.</param>
            /// <returns>
            ///    <c>true</c> if the operation succeedes, <c>false</c> otherwise. To get extended error information, call <see cref="System.Runtime.InteropServices.Marshal.GetLastWin32Error"/>.
            /// </returns>
            [DllImport("gdi32.dll", EntryPoint = "BitBlt", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool BitBlt([In] IntPtr hdc, int nXDest, int nYDest, int nWidth, int nHeight, [In] IntPtr hdcSrc, int nXSrc, int nYSrc, TernaryRasterOperations dwRop);

            /// <summary>
            ///     Specifies a raster-operation code. These codes define how the color data for the
            ///     source rectangle is to be combined with the color data for the destination
            ///     rectangle to achieve the final color.
            /// </summary>
            public enum TernaryRasterOperations : uint
            {
                /// <summary>dest = source</summary>
                SRCCOPY = 0x00CC0020,
                /// <summary>dest = source OR dest</summary>
                SRCPAINT = 0x00EE0086,
                /// <summary>dest = source AND dest</summary>
                SRCAND = 0x008800C6,
                /// <summary>dest = source XOR dest</summary>
                SRCINVERT = 0x00660046,
                /// <summary>dest = source AND (NOT dest)</summary>
                SRCERASE = 0x00440328,
                /// <summary>dest = (NOT source)</summary>
                NOTSRCCOPY = 0x00330008,
                /// <summary>dest = (NOT src) AND (NOT dest)</summary>
                NOTSRCERASE = 0x001100A6,
                /// <summary>dest = (source AND pattern)</summary>
                MERGECOPY = 0x00C000CA,
                /// <summary>dest = (NOT source) OR dest</summary>
                MERGEPAINT = 0x00BB0226,
                /// <summary>dest = pattern</summary>
                PATCOPY = 0x00F00021,
                /// <summary>dest = DPSnoo</summary>
                PATPAINT = 0x00FB0A09,
                /// <summary>dest = pattern XOR dest</summary>
                PATINVERT = 0x005A0049,
                /// <summary>dest = (NOT dest)</summary>
                DSTINVERT = 0x00550009,
                /// <summary>dest = BLACK</summary>
                BLACKNESS = 0x00000042,
                /// <summary>dest = WHITE</summary>
                WHITENESS = 0x00FF0062,
                /// <summary>
                /// Capture window as seen on screen.  This includes layered windows 
                /// such as WPF windows with AllowsTransparency="true"
                /// </summary>
                CAPTUREBLT = 0x40000000
            }

            /// <summary>Deletes the specified device context (DC).</summary>
            /// <param name="hdc">A handle to the device context.</param>
            /// <returns><para>If the function succeeds, the return value is nonzero.</para><para>If the function fails, the return value is zero.</para></returns>
            /// <remarks>An application must not delete a DC whose handle was obtained by calling the <c>GetDC</c> function. Instead, it must call the <c>ReleaseDC</c> function to free the DC.</remarks>
            [DllImport("gdi32.dll", EntryPoint = "DeleteDC")]
            public static extern bool DeleteDC([In] IntPtr hdc);

            [DllImport("gdi32.dll")]
            public static extern IntPtr CreateBitmap(int nWidth, int nHeight, uint cPlanes, uint cBitsPerPel, IntPtr lpvBits);

            [DllImport("gdi32.dll")]
            public static extern int GetObject(IntPtr hgdiobj, int cbBuffer, IntPtr lpvObject);

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
        }
    }

    static class RibbonIIDGuid
    {
        public const string IUIImage = "23c8c838-4de6-436b-ab01-5554bb7c30dd";
        public const string IUIImageFromBitmap = "18aba7f3-4c1c-4ba2-bf6c-f5c3326fa816";

        public const string UIRibbonImageFromBitmapFactory = "0F7434B6-59B6-4250-999E-D168D6AE4293";
    }

    // Container for bitmap image
    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid(RibbonIIDGuid.IUIImage)]
    public interface IUIImage
    {
        // Retrieves a bitmap to display as an icon in the ribbon and context popup UI of the Windows Ribbon (Ribbon) framework.
        [PreserveSig]
        HRESULT GetBitmap([Out] out IntPtr bitmap);
    }

    public enum Ownership
    {
        Transfer = 0,   // IUIImage now owns HBITMAP.
        Copy = 1,       // IUIImage creates a copy of HBITMAP. Caller still owns HBITMAP.
    }

    // Produces containers for bitmap images
    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid(RibbonIIDGuid.IUIImageFromBitmap)]
    public interface IUIImageFromBitmap
    {
        // Creates an IUIImage object from a bitmap image.
        [PreserveSig]
        HRESULT CreateImage(IntPtr bitmap, Ownership options, [Out, MarshalAs(UnmanagedType.Interface)] out IUIImage image);
    }

    // UIRibbonImageFromBitmapFactory class
    [ComImport]
    [ClassInterface(ClassInterfaceType.None)]
    [Guid(RibbonIIDGuid.UIRibbonImageFromBitmapFactory)]
    public class UIRibbonImageFromBitmapFactory
    {
        // implements IUIImageFromBitmap
    }

    /// <summary>
    /// HRESULT Wrapper
    /// </summary>
    public enum HRESULT : uint
    {
        S_OK = 0x00000000,
        S_FALSE = 0x00000001,
        E_ABORT = 0x80004004,
        E_FAIL = 0x80004005,
        E_NOTIMPL = 0x80004001,
    }
}
