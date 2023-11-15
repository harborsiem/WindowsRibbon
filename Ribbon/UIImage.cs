using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using RibbonLib.Interop;

namespace RibbonLib
{
    /// <summary>
    /// Helper class for IUIImage interface.
    /// This class supports Bitmaps with Alpha channel if available (PixelFormat.Format32bppArgb)
    /// </summary>
    public sealed class UIImage : IDisposable
    {
        private static IUIImageFromBitmap s_imageFactory;
        //static UIImage()
        //{
        //    s_imageFactory = new UIRibbonImageFromBitmapFactory() as IUIImageFromBitmap;
        //}
        private static bool s_initByRibbon;

        private IUIImage _handle;
        private IntPtr _hbitmap; //HBitmap
        private object _lockObject = new object();
        private bool _factoryDispose;

        private UIImage()
        {
            if (s_imageFactory == null)
            {
                lock (_lockObject)
                {
                    if (s_imageFactory == null)
                        s_imageFactory = new UIRibbonImageFromBitmapFactory() as IUIImageFromBitmap;
                }
            }
        }

        /// <summary>
        /// This ctor is only called by the Ribbon framework
        /// Ribbon framework has to Dispose when framework is destroyed
        /// </summary>
        /// <param name="imageFromBitmap"></param>
        internal UIImage(IUIImageFromBitmap imageFromBitmap)
        {
            if (s_imageFactory == null)
            {
                s_imageFactory = imageFromBitmap;
                _factoryDispose = true;
                s_initByRibbon = true;
            }
        }

        /// <summary>
        /// Ctor with IUIImage
        /// </summary>
        /// <param name="handle"></param>
        public unsafe UIImage(IUIImage handle) : this()
        {
            if (handle == null)
                throw new ArgumentNullException(nameof(handle));
            _handle = handle;
            _handle.GetBitmap(out _hbitmap);
            GetBitmapProperties();
        }

        /// <summary>
        /// Ctor for a bitmap file (*.bmp, *.png)
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="highContrast"></param>
        public UIImage(string filename, bool highContrast = false) : this()
        {
            if (!File.Exists(filename))
                throw new FileNotFoundException(nameof(filename));
            Load(filename, highContrast);
        }

        /// <summary>
        /// Ctor for a resource dll
        /// </summary>
        /// <param name="instance">Use MarkupHandle from Ribbon class</param>
        /// <param name="resourceName"></param>
        public UIImage(IntPtr instance, string resourceName) : this()
        {
            if (string.IsNullOrEmpty(resourceName))
                throw new ArgumentNullException(nameof(resourceName));
            Load(instance, resourceName);
        }

        /// <summary>
        /// Ctor for a resource dll
        /// </summary>
        /// <param name="instance">Use MarkupHandle from Ribbon class</param>
        /// <param name="resourceId">Id from the RibbonMarkup.h file</param>
        public UIImage(IntPtr instance, ushort resourceId) : this()
        {
            if (resourceId < 1)
                throw new ArgumentOutOfRangeException(nameof(resourceId));
            Load(instance, resourceId);
        }

        /// <summary>
        /// Ctor for a Bitmap. The Bitmap will be converted to a Bitmap with Alpha channel
        /// </summary>
        /// <param name="bitmap"></param>
        public unsafe UIImage(Bitmap bitmap) : this()
        {
            if (bitmap == null)
                throw new ArgumentNullException(nameof(bitmap));
            bitmap = TryGetAlphaBitmap(bitmap);
            s_imageFactory.CreateImage(bitmap.GetHbitmap(), Ownership.Transfer, out _handle);
            _handle.GetBitmap(out _hbitmap);
            GetBitmapProperties();
        }

        /// <summary>
        /// Height of the image
        /// </summary>
        public int Height { get; private set; }

        /// <summary>
        /// Width of the image
        /// </summary>
        public int Width { get; private set; }

        /// <summary>
        /// Number of bits per pixel for the bitmap image. When this value equals 32,
        /// this means that the bitmap has an alpha channel.
        /// </summary>
        public ushort BitsPerPixel { get; private set; }

        /// <summary>
        /// Bitmap handle HBITMAP
        /// </summary>
        public IntPtr HBitmap { get { return GetHBitmap(); } }

        /// <summary>
        /// The IUIImage interface, Low-level handle to the image
        /// </summary>
        public IUIImage Handle { get { return _handle; } }

        private void ConvertToAlphaBitmap(Bitmap bitmap)
        {
            if ((bitmap.PixelFormat == PixelFormat.Format32bppArgb) || (bitmap.Width == 0) || (bitmap.Height == 0))
                return;
            bitmap.MakeTransparent();
        }

        private unsafe IntPtr GetHBitmap()
        {
            if (_hbitmap != IntPtr.Zero)
                return _hbitmap;
            if (Handle != null)
            {
                Handle.GetBitmap(out _hbitmap);
                return _hbitmap;
            }
            return IntPtr.Zero;
        }

        private unsafe void GetBitmapProperties()
        {
            if (_hbitmap != IntPtr.Zero)
            {
                NativeMethods.BITMAP props = new NativeMethods.BITMAP();
                NativeMethods.GetObject(_hbitmap, sizeof(NativeMethods.BITMAP), &props);
                Width = props.bmWidth;
                Height = props.bmHeight;
                BitsPerPixel = props.bmBitsPixel;
            }
        }

        private unsafe IntPtr CreatePreMultipliedBitmap(IntPtr bitmap)
        {
            IntPtr DC;
            NativeMethods.BITMAPINFO info;
            int i, width, height, A, R, G, B;
            //int p;
            IntPtr data = IntPtr.Zero;
            IntPtr result = IntPtr.Zero;
            DC = NativeMethods.CreateCompatibleDC(IntPtr.Zero);
            try
            {
                info = new NativeMethods.BITMAPINFO(); // FillChar(Info, sizeof(Info), 0);
                info.bmiHeader.Create();
                if (NativeMethods.GetDIBits(DC, HBitmap, 0, 0, null, ref info, NativeMethods.DIB_USAGE.DIB_RGB_COLORS) != 0)
                {
                    width = info.bmiHeader.biWidth;
                    height = Math.Abs(info.bmiHeader.biHeight);
                    info.bmiHeader.biHeight = -height;
                    data = Marshal.AllocHGlobal(width * height * 4);
                    if (NativeMethods.GetDIBits(DC, HBitmap, 0, (uint)height, data, ref info, NativeMethods.DIB_USAGE.DIB_RGB_COLORS) != 0)
                    {
                        uint* p = (uint*)data;
                        uint value;
                        for (i = 0; i < (width * height); i++)
                        {
                            value = *p;
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
                            *p = value;
                            p++;
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

        private void Drawx(Graphics target, int xTarget, int yTarget, int wTarget, int hTarget)
        {
            IntPtr srcDC; // HDC;
            IntPtr bitmap, oldBitmap; // HBitmap;
            NativeMethods.BLENDFUNCTION BF;

            bitmap = GetHBitmap();
            if (bitmap == IntPtr.Zero)
                return;

            srcDC = NativeMethods.CreateCompatibleDC(IntPtr.Zero);
            try
            {
                if (BitsPerPixel == 32)
                {
                    //AlphaBlend requires that the bitmap is pre - multiplied with the Alpha
                    //values.
                    bitmap = CreatePreMultipliedBitmap(bitmap);
                    if (bitmap == IntPtr.Zero)
                        return;
                    try
                    {
                        oldBitmap = NativeMethods.SelectObject(srcDC, bitmap);
                        BF = new NativeMethods.BLENDFUNCTION()
                        {
                            BlendOp = NativeMethods.AC_SRC_OVER,
                            BlendFlags = 0,
                            SourceConstantAlpha = 255,
                            AlphaFormat = NativeMethods.AC_SRC_ALPHA
                        };
                        IntPtr hdc = target.GetHdc();
                        NativeMethods.AlphaBlend(hdc, xTarget, yTarget, wTarget, hTarget,
                          srcDC, 0, 0, Width, Height, BF);
                        target.ReleaseHdc(hdc);
                        NativeMethods.SelectObject(srcDC, oldBitmap);
                    }
                    finally
                    {
                        NativeMethods.DeleteObject(bitmap);
                    }
                }
                else
                {
                    try
                    {
                        oldBitmap = NativeMethods.SelectObject(srcDC, bitmap);
                        IntPtr hdc = target.GetHdc();
                        NativeMethods.BitBlt(hdc, xTarget, yTarget, Width, Height, srcDC, 0, 0, NativeMethods.ROP_CODE.SRCCOPY);
                        target.ReleaseHdc(hdc);
                        NativeMethods.SelectObject(srcDC, oldBitmap);
                    }
                    finally
                    {
                        NativeMethods.DeleteObject(bitmap);
                    }
                }
            }
            finally
            {
                NativeMethods.DeleteDC(srcDC);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="xTarget"></param>
        /// <param name="yTarget"></param>
        /// <param name="wTarget"></param>
        /// <param name="hTarget"></param>
        public void Draw(Graphics target, int xTarget, int yTarget, int wTarget, int hTarget)
        {
            target.DrawImage(GetBitmap(), xTarget, yTarget, wTarget, hTarget);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="xTarget"></param>
        /// <param name="yTarget"></param>
        public void Draw(Graphics target, int xTarget, int yTarget)
        {
            Draw(target, xTarget, yTarget, Width, Height);
        }

        /// <summary>
        /// Get the Bitmap
        /// </summary>
        /// <returns>Bitmap</returns>
        public Bitmap GetBitmap()
        {
            if (_hbitmap == IntPtr.Zero)
                return null;
            return FromHbitmap(_hbitmap);
        }

        private void Load(string filename, bool highContrast)
        {
            _handle = null;
            _hbitmap = IntPtr.Zero;
            string ext = Path.GetExtension(filename).ToUpperInvariant();
            if (ext == ".BMP")
                LoadBmp(filename, highContrast);
            else if (ext == ".PNG")
                LoadPng(filename, highContrast);
            else
                throw new ArgumentException("Unsupported image file extensions ()", nameof(filename));
            GetBitmapProperties();
            DoChanged();
        }

        void DoChanged()
        {

        }

        private unsafe void Load(IntPtr instance, ushort resourceId)
        {
            Load(instance, ((char*)resourceId));
        }

        //void Load(ushort resourceId)
        //{
        //    // ?
        //}

        private unsafe void Load(IntPtr instance, string resourceName)
        {
            fixed (char* resourceNameLocal = resourceName)
                Load(instance, resourceNameLocal);
        }

        private unsafe void Load(IntPtr instance, char* resourceName)
        {
            _hbitmap = (NativeMethods.LoadImage(instance, (IntPtr)resourceName, NativeMethods.GDI_IMAGE_TYPE.IMAGE_BITMAP, 0, 0, NativeMethods.IMAGE_FLAGS.LR_CREATEDIBSECTION));
            if (_hbitmap == IntPtr.Zero)
            {
                //Lookup for the Bitmap resource in the resource folder IMAGE
                //Maybe it is a Bitmap V5 or a PNG Bitmap
                IntPtr hResource = IntPtr.Zero;
                bool error = false;
                fixed (char* imageLocal = "IMAGE")
                {
                    hResource = NativeMethods.FindResource(instance, (IntPtr)resourceName, (IntPtr)imageLocal);
                }
                if (hResource != IntPtr.Zero)
                {
                    uint imageSize = NativeMethods.SizeofResource(instance, hResource);
                    if (imageSize != 0)
                    {
                        IntPtr res = NativeMethods.LoadResource(instance, hResource);
                        IntPtr pResourceData = NativeMethods.LockResource(res);
                        if (pResourceData != IntPtr.Zero)
                        {
                            byte[] imageData = new byte[imageSize];
                            Marshal.Copy((IntPtr)pResourceData, imageData, 0, (int)imageSize);
                            MemoryStream stream = new MemoryStream(imageData);
                            Bitmap bmp = new Bitmap(stream);
                            _hbitmap = bmp.GetHbitmap();
                        }
                        else
                            error = true;
                    }
                    else
                        error = true;
                }
                else
                    error = true;
                if (error)
                    throw new ArgumentException("Can not find resourceName");
            }
            try
            {
                s_imageFactory.CreateImage(_hbitmap, Ownership.Transfer, out _handle);
                GetBitmapProperties();
            }
            catch
            {
                NativeMethods.DeleteObject(_hbitmap);
                _hbitmap = IntPtr.Zero;
            }
        }

        private unsafe void LoadBmp(string filename, bool highContrast)
        {
            Bitmap bmp = null;
            try
            {
                bmp = new Bitmap(filename);
                bmp = TryGetAlphaBitmap(bmp);
                if (!highContrast)
                    ConvertToAlphaBitmap(bmp);
                s_imageFactory.CreateImage(bmp.GetHbitmap(), Ownership.Copy, out _handle);
                _handle.GetBitmap(out _hbitmap);
            }
            finally
            {
                bmp?.Dispose();
            }
        }

        private unsafe void LoadPng(string filename, bool highContrast)
        {
            Bitmap bmp = null;
            try
            {
                bmp = new Bitmap(filename);
                if (!highContrast)
                    ConvertToAlphaBitmap(bmp);
                s_imageFactory.CreateImage(bmp.GetHbitmap(), Ownership.Copy, out _handle);
                _handle.GetBitmap(out _hbitmap);
            }
            finally
            {
                bmp?.Dispose();
            }
        }

        private unsafe static Bitmap TryGetAlphaBitmap(Bitmap bitmap)
        {
            if (bitmap.PixelFormat == PixelFormat.Format32bppRgb && bitmap.RawFormat.Guid == ImageFormat.Bmp.Guid)
            {
                BitmapData bmpData = null;
                BitmapData alphaData = null;
                Bitmap alpha = null;
                try
                {
                    bmpData = bitmap.LockBits(new Rectangle(new Point(), bitmap.Size), ImageLockMode.ReadOnly, bitmap.PixelFormat);
                    if (BitmapHasAlpha(bmpData))
                    {
                        //alpha = new Bitmap(bitmap.Width, bitmap.Height, bmpData.Stride, PixelFormat.Format32bppArgb, bmpData.Scan0);
                        alpha = new Bitmap(bitmap.Width, bitmap.Height, PixelFormat.Format32bppArgb);
                        alphaData = alpha.LockBits(new Rectangle(new Point(), alpha.Size), ImageLockMode.WriteOnly, alpha.PixelFormat);
                        CopyBitmapData(bmpData, alphaData);
                        return alpha;
                    }
                }
                finally
                {
                    if (bmpData != null)
                        bitmap.UnlockBits(bmpData);
                    if (alpha != null)
                    {
                        alpha.UnlockBits(alphaData);
                        bitmap.Dispose();
                    }
                }
            }
            return bitmap;
        }

        //From Microsoft System.Drawing.Icon.cs
        private unsafe static bool BitmapHasAlpha(BitmapData bmpData)
        {
            bool hasAlpha = false;
            for (int i = 0; i < bmpData.Height; i++)
            {
                for (int j = 3; j < Math.Abs(bmpData.Stride); j += 4)
                {
                    // Stride here is fine since we know we're doing this on the whole image.
                    unsafe
                    {
                        byte* candidate = unchecked(((byte*)bmpData.Scan0.ToPointer()) + (i * bmpData.Stride) + j);
                        if (*candidate != 0)
                        {
                            hasAlpha = true;
                            return hasAlpha;
                        }
                    }
                }
            }

            return false;
        }

        private unsafe static void CopyBitmapData(BitmapData sourceData, BitmapData targetData)
        {
            byte* srcPtr = (byte*)sourceData.Scan0;
            byte* destPtr = (byte*)targetData.Scan0;

            //Debug.Assert(sourceData.Height == targetData.Height, "Unexpected height. How did this happen?");
            int height = Math.Min(sourceData.Height, targetData.Height);
            int bytesToCopyEachIter = Math.Abs(targetData.Stride);

            for (int i = 0; i < height; i++)
            {
                if (IntPtr.Size == 4)
                    NativeMethods.RtlMoveMemory4(destPtr, srcPtr, bytesToCopyEachIter);
                else
                    NativeMethods.RtlMoveMemory8(destPtr, srcPtr, bytesToCopyEachIter);
                //Buffer.MemoryCopy(srcPtr, destPtr, bytesToCopyEachIter, bytesToCopyEachIter);
                srcPtr += sourceData.Stride;
                destPtr += targetData.Stride;
            }

            //GC.KeepAlive(null); // finalizer mustn't deallocate data blobs while this method is running
        }

        /// <summary>
        /// Get the managed ARGB Bitmap if possible (32 bit per pixel)
        /// </summary>
        /// <param name="hBitmap">Handle to a Bitmap</param>
        /// <returns>The Bitmap with fully transparency if available</returns>
        public unsafe static Bitmap FromHbitmap(IntPtr hBitmap)
        {
            if (hBitmap == IntPtr.Zero)
                throw new ArgumentNullException(nameof(hBitmap));
            // Create the BITMAP structure and get info from our nativeHBitmap
            NativeMethods.BITMAP bitmapStruct = new NativeMethods.BITMAP();
            int bitmapSize = Marshal.SizeOf(bitmapStruct);
            int size = NativeMethods.GetObjectBitmap(hBitmap, bitmapSize, ref bitmapStruct);
            //if (size != bitmapSize)
            //    return null;
            Bitmap managedBitmap;
            if (Has32BitAlpha(ref bitmapStruct))
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
            }
            NativeMethods.DeleteObject(hBitmap);
            return managedBitmap;
        }

        private static unsafe bool Has32BitAlpha(ref NativeMethods.BITMAP bitmapStruct)
        {
            if (bitmapStruct.bmBitsPixel == 32)
            {
                for (int i = 0; i < bitmapStruct.bmHeight; i++)
                {
                    for (int j = 3; j < Math.Abs(bitmapStruct.bmWidthBytes); j += 4)
                    {
                        // Stride here is fine since we know we're doing this on the whole image.
                        unsafe
                        {
                            byte* candidate = unchecked(((byte*)bitmapStruct.bmBits) + (i * bitmapStruct.bmWidthBytes) + j);
                            if (*candidate != 0)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {

            }
            if (_handle != null)
            {
                NativeMethods.DeleteObject(_hbitmap);
                _handle = null;
            }
            if (s_imageFactory != null && _factoryDispose)
            {
                s_imageFactory = null;
                s_initByRibbon = false;
            }
        }

        /// <summary>
        /// Should only be used at the end of an application (FormClose)
        /// </summary>
        public static void Destroy()
        {
            if (!s_initByRibbon && s_imageFactory != null)
            {
                Marshal.ReleaseComObject(s_imageFactory);
                s_imageFactory = null;
            }
        }

        class NativeMethods
        {
            [DllImport("KERNEL32.dll", EntryPoint = "RtlMoveMemory")]
            internal unsafe static extern void RtlMoveMemory4(void* dest, void* src, int cb);

            [DllImport("KERNEL32.dll", EntryPoint = "RtlMoveMemory")]
            internal unsafe static extern void RtlMoveMemory8(void* dest, void* src, long cb);

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

            [StructLayout(LayoutKind.Sequential)]
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

            public enum BI_COMPRESSION : uint
            {
                BI_RGB = 0,
                BI_RLE8 = 1,
                BI_RLE4 = 2,
                BI_BITFIELDS = 3,
                BI_JPEG = 4,
                BI_PNG = 5
            }

            [StructLayout(LayoutKind.Sequential)]
            public unsafe struct BITMAPINFOHEADER
            {
                public uint biSize;
                public int biWidth;
                public int biHeight;
                public ushort biPlanes;
                public ushort biBitCount;
                public BI_COMPRESSION biCompression;
                public uint biSizeImage;
                public int biXPelsPerMeter;
                public int biYPelsPerMeter;
                public uint biClrUsed;
                public uint biClrImportant;

                public void Create()
                {
                    biSize = (uint)sizeof(BITMAPINFOHEADER);
                }
            }

            public enum DIB_USAGE : uint
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
            public static extern int GetDIBits([In] IntPtr hdc, [In] IntPtr hbmp, uint uStartScan, uint cScanLines, [Out] byte[] lpvBits, [In, Out] ref BITMAPINFO lpbi, DIB_USAGE uUsage);

            [DllImport("gdi32.dll", EntryPoint = "GetDIBits")]
            public static extern int GetDIBits([In] IntPtr hdc, [In] IntPtr hbmp, uint uStartScan, uint cScanLines, [Out] IntPtr lpvBits, ref BITMAPINFO lpbi, DIB_USAGE uUsage);

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

            public struct BLENDFUNCTION
            {
                public byte BlendOp;
                public byte BlendFlags;
                public byte SourceConstantAlpha;
                public byte AlphaFormat;
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
            public static extern bool BitBlt([In] IntPtr hdc, int nXDest, int nYDest, int nWidth, int nHeight, [In] IntPtr hdcSrc, int nXSrc, int nYSrc, ROP_CODE dwRop);

            /// <summary>
            ///     Specifies a raster-operation code. These codes define how the color data for the
            ///     source rectangle is to be combined with the color data for the destination
            ///     rectangle to achieve the final color.
            /// </summary>
            public enum ROP_CODE : uint
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
            public unsafe static extern int GetObject(IntPtr hgdiobj, int cbBuffer, void* lpvObject);

            [DllImport("gdi32", CharSet = CharSet.Auto, EntryPoint = "GetObject")]
            public static extern int GetObjectBitmap(IntPtr hObject, int nCount, ref BITMAP lpObject);


            [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
            public static extern IntPtr LoadImage(IntPtr hinst, IntPtr lpszName, GDI_IMAGE_TYPE type,
                int cxDesired, int cyDesired, IMAGE_FLAGS fuLoad);

            public enum GDI_IMAGE_TYPE
            {
                IMAGE_BITMAP = 0,
                IMAGE_ICON = 1,
                IMAGE_CURSOR = 2
            }

            [Flags]
            public enum IMAGE_FLAGS
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

            [DllImport("kernel32.dll")]
            public static extern IntPtr FindResource(IntPtr hModule, string lpName, IntPtr lpType);

            [DllImport("kernel32.dll")]
            public static extern IntPtr FindResource(IntPtr hModule, IntPtr lpName, IntPtr lpType);

            [DllImport("kernel32.dll")]
            public static extern IntPtr FindResource(IntPtr hModule, IntPtr lpName, string lpType);

            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern uint SizeofResource(IntPtr hModule, IntPtr hResInfo);

            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern IntPtr LoadResource(IntPtr hModule, IntPtr hResInfo);

            [DllImport("kernel32.dll")]
            public static extern IntPtr LockResource(IntPtr hResData);

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
                public ushort bmPlanes;

                /// <summary>
                /// The number of bits required to indicate the color of a pixel.
                /// </summary>
                public ushort bmBitsPixel;

                /// <summary>
                /// A pointer to the location of the bit values for the bitmap. The bmBits member must be a pointer to an array of 
                /// character (1-byte) values.
                /// </summary>
                public IntPtr bmBits;
            }
        }
    }

    //static class RibbonIIDGuid
    //{
    //    public const string IUIImage = "23c8c838-4de6-436b-ab01-5554bb7c30dd";
    //    public const string IUIImageFromBitmap = "18aba7f3-4c1c-4ba2-bf6c-f5c3326fa816";

    //    public const string UIRibbonImageFromBitmapFactory = "0F7434B6-59B6-4250-999E-D168D6AE4293";
    //}

    //// Container for bitmap image
    //[ComImport]
    //[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    //[Guid(RibbonIIDGuid.IUIImage)]
    //public interface IUIImage
    //{
    //    // Retrieves a bitmap to display as an icon in the ribbon and context popup UI of the Windows Ribbon (Ribbon) framework.
    //    [PreserveSig]
    //    HRESULT GetBitmap([Out] out IntPtr bitmap);
    //}

    //public enum UI_OWNERSHIP
    //{
    //    UI_OWNERSHIP_TRANSFER = 0,   // IUIImage now owns HBITMAP.
    //    UI_OWNERSHIP_COPY = 1,       // IUIImage creates a copy of HBITMAP. Caller still owns HBITMAP.
    //}

    //// Produces containers for bitmap images
    //[ComImport]
    //[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    //[Guid(RibbonIIDGuid.IUIImageFromBitmap)]
    //public interface IUIImageFromBitmap
    //{
    //    // Creates an IUIImage object from a bitmap image.
    //    [PreserveSig]
    //    HRESULT CreateImage(IntPtr bitmap, UI_OWNERSHIP options, [Out, MarshalAs(UnmanagedType.Interface)] out IUIImage image);
    //}

    //// UIRibbonImageFromBitmapFactory class
    //[ComImport]
    //[ClassInterface(ClassInterfaceType.None)]
    //[Guid(RibbonIIDGuid.UIRibbonImageFromBitmapFactory)]
    //public class UIRibbonImageFromBitmapFactory
    //{
    //    // implements IUIImageFromBitmap
    //}

    ///// <summary>
    ///// HRESULT Wrapper
    ///// </summary>
    //public enum HRESULT : uint
    //{
    //    S_OK = 0x00000000,
    //    S_FALSE = 0x00000001,
    //    E_ABORT = 0x80004004,
    //    E_FAIL = 0x80004005,
    //    E_NOTIMPL = 0x80004001,
    //}
}
