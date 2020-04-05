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
    sealed class UIImage : IUIImage
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

        public static Bitmap BitmapFromFile(string fileName, bool highContrast = false)
        {
            Bitmap bitmap = null;
            if (!highContrast && !Path.GetExtension(fileName).Equals("PNG", StringComparison.OrdinalIgnoreCase))
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
                    //@ Make some tests if it is a ARGB Bitmap
                    if (bitCount == 32 && compression == NativeMethods.BitmapCompressionMode.BI_RGB) //width * Math.Abs(height) * 4 == length
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
            return bitmap;
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
