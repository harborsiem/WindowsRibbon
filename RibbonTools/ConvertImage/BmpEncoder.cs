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
        private const uint FileHeaderSize = 14;
        //private const int DBIV1Size = 40;
        //private const int DBIV4Size = 108;
        //private const int DBIV5Size = 124;
        private const byte ForcedAlpha = 0xff;

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
            BITMAPFILEHEADER bmpFH = BITMAPFILEHEADER.Create();
            BITMAPV5HEADER bmpV5 = BITMAPV5HEADER.Create();
            Bitmap bitmap = CastImageToBitmap(bmp);
            BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, bitmap.PixelFormat);
            int paddingLength = 0;
            int rowSize = data.Stride < 0 ? -data.Stride : data.Stride;
            using (BinaryWriter bw = new BinaryWriter(new FileStream(_path, FileMode.Create)))
            {
                // Encodes file header
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
                bmpFH.bfSize = FileHeaderSize + bmpV5.biSize + rawDataLength;
                bmpFH.bfOffBits = FileHeaderSize + bmpV5.biSize;
                //=============== Read just for forced (Padding)===================================
                bw.Write(bmpFH.bfType);
                bw.Write(bmpFH.bfSize);
                bw.Write(bmpFH.bfReserved1);
                bw.Write(bmpFH.bfReserved2);
                bw.Write(bmpFH.bfOffBits);

                // Encodes DBI Header
                bmpV5.biWidth = bitmap.Width;
                bmpV5.biHeight = bitmap.Height;
                bmpV5.biPlanes = 1;
                bmpV5.biBitCount = 32;
                bmpV5.biCompression = BitmapCompressionMode.BI_BITFIELDS;
                bmpV5.biSizeImage = rawDataLength;
                bmpV5.biXPelsPerMeter = 0xec4; // (int)Math.Round(bitmap.VerticalResolution * 39.3701); // Pixels per meter
                bmpV5.biYPelsPerMeter = 0xec4; // (int)Math.Round(bitmap.VerticalResolution * 39.3701); // Pixels per meter
                bmpV5.biClrUsed = 0;
                bmpV5.biClrImportant = 0;
                // Encodes DBI Extra for ARGB
                bmpV5.biRedMask = 0xff0000;
                bmpV5.biGreenMask = 0xff00;
                bmpV5.biBlueMask = 0xff;
                bmpV5.biAlphaMask = 0xff000000;
                bmpV5.biCSType = 0x57696e20; //= "Win ",   sRGB = 0x73524742;
                bmpV5.biEndPoints = new byte[36];
                bmpV5.biGammaRed = 0;
                bmpV5.biGammaGreen = 0;
                bmpV5.biGammaBlue = 0;
                bmpV5.biIntent = (int)GamutMappingIntent.LCS_GM_IMAGES;
                bmpV5.biProfileData = 0;
                bmpV5.biProfileSize = 0;
                bmpV5.biReserved = 0;

                bw.Write(bmpV5.biSize);
                bw.Write(bmpV5.biWidth);
                bw.Write(bmpV5.biHeight);
                bw.Write(bmpV5.biPlanes);
                bw.Write(bmpV5.biBitCount);
                bw.Write((uint)bmpV5.biCompression);
                bw.Write(bmpV5.biSizeImage);
                bw.Write(bmpV5.biXPelsPerMeter);
                bw.Write(bmpV5.biYPelsPerMeter);
                bw.Write(bmpV5.biClrUsed);
                bw.Write(bmpV5.biClrImportant);

                bw.Write(bmpV5.biRedMask);
                bw.Write(bmpV5.biGreenMask);
                bw.Write(bmpV5.biBlueMask);
                bw.Write(bmpV5.biAlphaMask);
                bw.Write(bmpV5.biCSType);

                bw.Write(bmpV5.biEndPoints);
                bw.Write(bmpV5.biGammaRed);
                bw.Write(bmpV5.biGammaGreen);
                bw.Write(bmpV5.biGammaBlue);
                bw.Write(bmpV5.biIntent);
                bw.Write(bmpV5.biProfileData);
                bw.Write(bmpV5.biProfileSize);
                bw.Write(bmpV5.biReserved);

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
