//*****************************************************************************
//
//  File:       ColorHelper.cs
//
//  Contents:   Class which supply color related helper functions for
//              transforming from one color format to the other
//
//*****************************************************************************

using System;
using System.Drawing;

namespace RibbonLib
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    /// <summary>
    /// Helper struct for Hue, Saturation, Luminance
    /// </summary>
    public struct HSL
    {
        public double H;
        public double S;
        public double L;

        public HSL(double h, double s, double l)
        {
            H = h;
            S = s;
            L = l;
        }
    }

    /// <summary>
    /// Helper struct for Hue, Saturation, Brightness
    /// </summary>
    public struct HSB
    {
        public byte H;
        public byte S;
        public byte B;
    }

    /// <summary>
    /// Class for color conversions
    /// </summary>
    public static class ColorHelper
    {
        /// <summary>
        /// Convert RGB Color to Ribbon Color format
        /// </summary>
        /// <param name="color">RGB Color</param>
        /// <returns>Ribbon Color format</returns>
        public static uint RGBToUInt32(Color color)
        {
            HSL hsl = RGBToHsl(color.R, color.G, color.B);
            //HSL hsl = RGBToHSL(color); //old one
            //HSB hsb = HSLToHSB(hsl);
            HSB hsb = HSLToHSB(new HSL(hsl.H / 360.0, hsl.S, hsl.L));
            uint result = HSBToUInt32(hsb);
            return result;
        }

        /// <summary>
        /// Convert Ribbon Color format to RGB Color
        /// </summary>
        /// <param name="value">Ribbon Color format</param>
        /// <returns>RGB Color</returns>
        public static Color UInt32ToRGB(uint value)
        {
            HSB hsb = UInt32ToHSB(value);
            HSL hsl = HSBToHSL(hsb);
            Color result = HslToRgb(hsl.H * 360.0, hsl.S, hsl.L);
            //Color result = HSLToRGB(hsl);
            return result;
        }

        //HSB.H [0..255], HSB.S [0..255], HSB.L [0..255]
        public static HSB UInt32ToHSB(uint value)
        {
            HSB hsb = new HSB();
            hsb.H = (byte)(value & 0xFF);
            hsb.S = (byte)((value >> 8) & 0xFF);
            hsb.B = (byte)((value >> 16) & 0xFF);
            return hsb;
        }

        //HSL.H [0..1], HSL.S [0..1], HSL.L [0..1]
        //HSB.H [0..255], HSB.S [0..255], HSB.L [0..255]
        /// <summary>
        /// HSB to HSL conversion. All values of HSL in Range [0..1]
        /// </summary>
        /// <param name="hsb"></param>
        /// <returns></returns>
        public static HSL HSBToHSL(HSB hsb)
        {
            HSL hsl = new HSL();
            double ld;
            // Convert B to L
            if (hsb.B == 0)
                ld = 0.0;
            else if (hsb.B == 0xff)
                ld = 1.0;
            else
                ld = Math.Exp((hsb.B - 257.7) / 149.9);

            // HSLToRGB requires H, L and S to be in 0..1 range.
            hsl.H = (hsb.H) / 255.0;
            hsl.S = (hsb.S) / 255.0;
            hsl.L = ld;
            return hsl;
        }

        //Conversion from W3C
        //H [0..360], S [0..1], L [0..1]
        static Color HslToRgb(double hue, double sat, double light)
        {
            double t1, t2;
            int r, g, b;
            hue = hue / 60.0;
            if (light <= 0.5)
            {
                t2 = light * (sat + 1);
            }
            else
            {
                t2 = light + sat - (light * sat);
            }
            t1 = light * 2 - t2;
            r = Convert.ToByte(HueToRgb(t1, t2, hue + 2) * 255);
            g = Convert.ToByte(HueToRgb(t1, t2, hue) * 255);
            b = Convert.ToByte(HueToRgb(t1, t2, hue - 2) * 255);
            return Color.FromArgb(r, g, b);
        }

        static double HueToRgb(double t1, double t2, double hue)
        {
            if (hue < 0) hue += 6;
            if (hue >= 6) hue -= 6;
            if (hue < 1) return (t2 - t1) * hue + t1;
            else if (hue < 3) return t2;
            else if (hue < 4) return (t2 - t1) * (4 - hue) + t1;
            else return t1;
        }

        //Conversion from W3C
        //H [0..360], S [0..1], L [0..1]
        //same conversion like Microsoft .NET Color.Hue, Color.Saturation, Color.Brightness
        static HSL RGBToHsl(byte r, byte g, byte b)
        {
            double min, max, l, s, maxcolor, h = 0;
            int i;
            double[] rgb = new double[3];
            HSL hsl = new HSL();
            rgb[0] = r / 255.0;
            rgb[1] = g / 255.0;
            rgb[2] = b / 255.0;
            min = rgb[0];
            max = rgb[0];
            maxcolor = 0;
            for (i = 0; i < rgb.Length - 1; i++)
            {
                if (rgb[i + 1] <= min) { min = rgb[i + 1]; }
                if (rgb[i + 1] >= max) { max = rgb[i + 1]; maxcolor = i + 1; }
            }
            if (r == g && g == b)
                h = 0;
            else
            {
                if (maxcolor == 0)
                {
                    h = (rgb[1] - rgb[2]) / (max - min);
                }
                if (maxcolor == 1)
                {
                    h = 2 + (rgb[2] - rgb[0]) / (max - min);
                }
                if (maxcolor == 2)
                {
                    h = 4 + (rgb[0] - rgb[1]) / (max - min);
                }

                if (double.NaN == h) { h = 0; }
                h = h * 60;
                if (h < 0) { h = h + 360; }
            }
            l = (min + max) / 2;
            if (min == max)
            {
                s = 0;
            }
            else
            {
                if (l < 0.5)
                {
                    s = (max - min) / (max + min);
                }
                else
                {
                    s = (max - min) / (2 - max - min);
                }
            }
            hsl.H = h; hsl.S = s; hsl.L = l;
            return hsl;
        }

        //HSL.H [0..1], HSL.S [0..1], HSL.L [0..1]
        //HSB.H [0..255], HSB.S [0..255], HSB.L [0..255]
        public static HSB HSLToHSB(HSL hsl)
        {
            HSB hsb;

            hsb.H = (byte)Math.Round(255.0 * hsl.H);
            hsb.S = (byte)Math.Round(255.0 * hsl.S);
            if (hsl.L < 0.1793)
                hsb.B = 0;
            else if (hsl.L > 0.9821)
                hsb.B = 0xff;
            else
                hsb.B = (byte)Math.Round(257.7 + 149.9 * Math.Log(hsl.L));

            return hsb;
        }

        //HSB.H [0..255], HSB.S [0..255], HSB.L [0..255]
        public static uint HSBToUInt32(HSB hsb)
        {
            return (uint)(hsb.H | (hsb.S << 8) | (hsb.B << 16));
        }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}
