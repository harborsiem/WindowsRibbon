using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;
using System.Runtime.InteropServices;

namespace UIRibbonTools
{
    public class BmpEncoder
    {
        private const int FileHeaderSize = 14;
        private const int DBIV1Size = 40;
        private const int DBIV4Size = 108;
        private const int DBIV5Size = 124;
        private const byte ForcedAlpha = 0xff;

        // Bmp Header
        private const ushort Magic = 0x4d42; //"BM"
        private uint FileSize;
        private const int Unused = 0;
        private int DataOffset;

        // DBI Header
        private uint DBI_Size;
        private int Width;
        private int Height;
        private ushort NumberOfColorPlanes;
        private ushort BitsPerPixel;
        private uint DataType; //0 or 3
        private uint SizeOfRawBitmapData;
        private const int XPrintResolution = 0xec4; // (int)Math.Round(bitmap.VerticalResolution * 39.3701); // Pixels per meter
        private const int YPrintResolution = 0xec4; // (int)Math.Round(bitmap.VerticalResolution * 39.3701); // Pixels per meter
        private uint ColorInPalette;
        private uint ColorImportant;

        // DBI Extra for ARGB with no gamma correction
        private const uint RedChannelMask = 0xff0000;
        private const uint GreenChannelMask = 0xff00;
        private const uint BlueChannelMask = 0xff;
        private const uint AlphaChannelMask = 0xff000000;
        private const uint WindowColorSpace = 0x57696e20; //= "Win ",   sRGB = 0x73524742;
        private byte[] UnusedForsRGB = new byte[0x30]; //No gamma correction, 36 Bytes + 3 DWords
        private int Intent;
        private const uint ProfileData = 0;
        private const uint ProfileSize = 0;
        private const uint Reserved = 0;

        private string _path;

        /// <summary>
        /// Saves the image to a BMP file
        /// </summary>
        /// <param name="bmp">The image to be saved</param>
        /// <param name="path">The path of the file</param>
        /// <param name="encoding">The format of the file encoding. </param>
        /// <remarks></remarks>
        public void Encode(Image bmp, string path, BmpEncoding encoding)
        {
            if (bmp == null)
            {
                throw new ArgumentNullException(nameof(bmp), "Invalid bitmap");
            }
            if ((bmp.Height == 0) | (bmp.Width == 0))
            {
                throw new ArgumentException("Invalid bitmap", nameof(bmp));
            }
            _path = path;
            switch (encoding)
            {
                //case BmpEncoding.Auto:
                //    if (bmp.PixelFormat != PixelFormat.Format24bppRgb)
                //    {
                //        if (bmp.PixelFormat == PixelFormat.Format32bppArgb)
                //        {
                //            Encode32Bpp(bmp);
                //        }
                //        else
                //        {
                //            if (bmp.PixelFormat != PixelFormat.Format1bppIndexed)
                //            {
                //                throw new ArgumentException("Unsupported pixel format");
                //            }
                //            Encode1Bpp(bmp);
                //        }
                //        return;
                //    }
                //    Encode24Bpp(bmp);
                //    return;

                //case BmpEncoding.Encoding1BPP:
                //    if (bmp.PixelFormat != PixelFormat.Format1bppIndexed)
                //    {
                //        throw new ArgumentException("Unsupported conversion between the bitmap format and the encoding format");
                //    }
                //    Encode1Bpp(bmp);
                //    return;

                //case BmpEncoding.Encoding24BPP:
                //    if (!((bmp.PixelFormat == PixelFormat.Format24bppRgb) | (bmp.PixelFormat == PixelFormat.Format32bppArgb)))
                //    {
                //        throw new ArgumentException("Unsupported conversion between the bitmap format and the encoding format");
                //    }
                //    Encode24Bpp(bmp);
                //    return;

                case BmpEncoding.Encoding32BPP:
                    if (!((bmp.PixelFormat == PixelFormat.Format24bppRgb) | (bmp.PixelFormat == PixelFormat.Format32bppArgb)))
                    {
                        throw new ArgumentException("Unsupported conversion between the bitmap format and the encoding format");
                    }
                    Encode32Bpp(bmp);
                    return;
            }
        }

        private Bitmap CastImageToBitmap(Image bmp)
        {
            Bitmap bitmap = null;
            if (!(bmp is Bitmap))
            {
                if (!(bmp is Metafile))
                {
                    throw new ArgumentException("Unsupported image type");
                }
                Metafile metaFile = (Metafile)bmp;
                bitmap = new Bitmap(metaFile.Width, metaFile.Height);
                Graphics.FromImage(bitmap).DrawImage(metaFile, 0, 0, metaFile.Width, metaFile.Height);
            }
            if (bitmap == null)
            {
                bitmap = (Bitmap)bmp;
            }
            return bitmap;
        }

        /// <remarks>
        /// Using Windows DIB Header BITMAPV5INFOHEADER
        /// DataType:  BI_BITFIELDS (3)
        /// For compatibility with the GDI bitmap formats:
        /// RGBAX is 8.8.8.8.0
        /// Red mask = 0xFF0000
        /// Green mask = 0xFF00
        /// Blue mask = 0xFF
        /// Alpha mask = 0xFF000000
        /// No Gamma correction
        /// No Compression
        /// Encoding 32 bits bitmap to 32 bits file takes ~2.5ms per megapixel
        /// Forcing 24 bits bitmap to 32 bits file takes ~10.0ms per megapixel
        /// </remarks>
        private void Encode32Bpp(Image bmp)
        {
            Bitmap bitmap = CastImageToBitmap(bmp);
            BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, bitmap.PixelFormat);
            int paddingLength = 0;
            int rowSize = data.Stride < 0 ? -data.Stride : data.Stride;
            using (BinaryWriter bw = new BinaryWriter(new FileStream(_path, FileMode.Create)))
            {
                // Encodes header
                bw.Write(Magic);
                uint rawDataLength;
                if (bitmap.PixelFormat == PixelFormat.Format24bppRgb)
                {
                    paddingLength = 4 - ((data.Width * 3) % 4);
                    if (paddingLength == 4)
                    {
                        paddingLength = 0;
                    }
                    rawDataLength = (uint)(((rowSize - paddingLength) * data.Height * 4) / 3);
                }
                else
                    rawDataLength = (uint)(rowSize * data.Height);
                FileSize = (uint)(FileHeaderSize + DBIV5Size + rawDataLength);
                //=============== Read just for forced (Padding)===================================
                bw.Write(FileSize);
                bw.Write(Unused);
                DataOffset = FileHeaderSize + DBIV5Size;
                bw.Write(DataOffset);
                // Encodes DBI Header
                DBI_Size = DBIV5Size;
                bw.Write(DBI_Size);
                Width = bitmap.Width;
                bw.Write(Width);
                Height = bitmap.Height;
                bw.Write(Height);
                NumberOfColorPlanes = 1;
                bw.Write(NumberOfColorPlanes);
                BitsPerPixel = 32;
                bw.Write(BitsPerPixel);
                DataType = 3;
                bw.Write(DataType);
                SizeOfRawBitmapData = rawDataLength;
                bw.Write(SizeOfRawBitmapData);
                bw.Write(XPrintResolution);
                bw.Write(YPrintResolution);
                ColorInPalette = 0;
                bw.Write(ColorInPalette);
                ColorImportant = 0;
                bw.Write(ColorImportant);
                // Encodes DBI Extra for ARGB
                bw.Write(RedChannelMask);
                bw.Write(GreenChannelMask);
                bw.Write(BlueChannelMask);
                bw.Write(AlphaChannelMask);
                bw.Write(WindowColorSpace);
                bw.Write(UnusedForsRGB);

                Intent = (int)GamutMappingIntent.LCS_GM_IMAGES;
                bw.Write(Intent);
                bw.Write(ProfileData);
                bw.Write(ProfileSize);
                bw.Write(Reserved);

                // Encodes image data
                byte[] rowBytes = new byte[rowSize];
                if (bitmap.PixelFormat == PixelFormat.Format24bppRgb)
                {
                    // Forces 24 bits bitmap to 32 bits file
                    IntPtr ptr = data.Scan0 + ((bmp.Height - 1) * data.Stride);
                    byte[] bytesWithAlpha = new byte[((rowBytes.Length - paddingLength) * 4) / 3];

                    for (int i = (bmp.Height - 1); i >= 0; i--, ptr -= data.Stride)
                    //for (IntPtr ptr = data.Scan0 + ((bmp.Height - 1) * data.Stride); ptr.ToInt64() >= data.Scan0.ToInt64(); ptr -= data.Stride)
                    {
                        Marshal.Copy(ptr, rowBytes, 0, data.Stride);
                        int index = 0;
                        int num = rowBytes.Length - paddingLength;
                        for (int x = 0; x < num; x++)
                        {
                            bytesWithAlpha[index] = rowBytes[x];
                            index++;
                            if ((x % 3) == 2)
                            {
                                bytesWithAlpha[index] = ForcedAlpha;
                                index++;
                            }
                        }
                        bw.Write(bytesWithAlpha);
                    }
                }
                else
                {
                    // 32 bits bitmap to 32 bits file 
                    IntPtr ptr = data.Scan0 + ((bmp.Height - 1) * data.Stride);
                    for (int i = (bmp.Height - 1); i >= 0; i--, ptr -= data.Stride)
                    //for (IntPtr ptr = data.Scan0 + ((bmp.Height - 1) * data.Stride); ptr.ToInt64() >= data.Scan0.ToInt64(); ptr -= data.Stride)
                    {
                        Marshal.Copy(ptr, rowBytes, 0, rowSize);
                        bw.Write(rowBytes);
                    }
                }
            }
            bitmap.UnlockBits(data);
        }

    }
}
