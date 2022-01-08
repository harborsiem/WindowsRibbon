using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace UIRibbonTools
{
    //Wingdi.h
    public enum GamutMappingIntent : uint
    {
        LCS_GM_ABS_COLORIMETRIC = 0x00000008,
        LCS_GM_BUSINESS = 0x00000001,
        LCS_GM_GRAPHICS = 0x00000002,
        LCS_GM_IMAGES = 0x00000004
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

    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    public struct BITMAPFILEHEADER
    {
        //Size is 14 Bytes
        public ushort bfType;
        public uint bfSize;
        public ushort bfReserved1;
        public ushort bfReserved2;
        public uint bfOffBits;
        public static BITMAPFILEHEADER Create() => new BITMAPFILEHEADER
        {
            bfType = 0x4d42, //"BM"
        };
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct BITMAPCOREHEADER
    {
        public uint bcSize; //Size = 0x0c (12)
        public ushort bcWidth;
        public ushort bcHeight;
        public ushort bcPlanes;
        public ushort bcBitCount;
        public static BITMAPCOREHEADER Create() => new BITMAPCOREHEADER
        {
            bcSize = (uint)Marshal.SizeOf<BITMAPCOREHEADER>()
        };
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct BITMAPINFOHEADER
    {
        public uint biSize; //Size = 0x28 (40)
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
        public static BITMAPINFOHEADER Create() => new BITMAPINFOHEADER
        {
            biSize = (uint)Marshal.SizeOf<BITMAPINFOHEADER>()
        };
    }

    //Undocumented, Used by Adobe Photoshop
    [StructLayout(LayoutKind.Sequential)]
    public struct BITMAPV2HEADER
    {
        public uint biSize; //Size = 0x34 (52)
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
        public uint biRedMask;
        public uint biGreenMask;
        public uint biBlueMask;
        public static BITMAPV2HEADER Create() => new BITMAPV2HEADER
        {
            biSize = (uint)Marshal.SizeOf<BITMAPV2HEADER>()
        };
    }

    //Undocumented, Used by Adobe Photoshop
    [StructLayout(LayoutKind.Sequential)]
    public struct BITMAPV3HEADER
    {
        public uint biSize; //Size = 0x38 (56)
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
        public uint biRedMask;
        public uint biGreenMask;
        public uint biBlueMask;
        public uint biAlphaMask;
        public static BITMAPV3HEADER Create() => new BITMAPV3HEADER
        {
            biSize = (uint)Marshal.SizeOf<BITMAPV3HEADER>()
        };
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct BITMAPV4HEADER
    {
        public uint biSize; //Size = 0x6c (108)
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
        public uint biRedMask;
        public uint biGreenMask;
        public uint biBlueMask;
        public uint biAlphaMask;
        public uint biCSType;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 36)]
        public byte[] biEndPoints;
        public uint biGammaRed;
        public uint biGammaGreen;
        public uint biGammaBlue;
        public static BITMAPV4HEADER Create() => new BITMAPV4HEADER
        {
            biSize = (uint)Marshal.SizeOf<BITMAPV4HEADER>()
        };
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct BITMAPV5HEADER
    {
        public uint biSize; //Size = 0x7c (124)
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
        public uint biRedMask;
        public uint biGreenMask;
        public uint biBlueMask;
        public uint biAlphaMask;
        public uint biCSType;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 36)]
        public byte[] biEndPoints;
        public uint biGammaRed;
        public uint biGammaGreen;
        public uint biGammaBlue;
        public uint biIntent; //enum GamutMappingIntent
        public uint biProfileData;
        public uint biProfileSize;
        public uint biReserved;
        public static BITMAPV5HEADER Create() => new BITMAPV5HEADER
        {
            biSize = (uint)Marshal.SizeOf<BITMAPV5HEADER>()
        };
    }
}
