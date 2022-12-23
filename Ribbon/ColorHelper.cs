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
    public partial struct UI_HSBCOLOR
    : IEquatable<UI_HSBCOLOR>
    {
        public readonly uint Value;
        public UI_HSBCOLOR(uint value) => this.Value = value;
        public UI_HSBCOLOR(byte hue, byte saturation, byte brightness) =>
            this.Value = (uint)(hue | (saturation << 8) | (brightness << 16));
        public static implicit operator uint(UI_HSBCOLOR value) => value.Value;
        public static explicit operator UI_HSBCOLOR(uint value) => new UI_HSBCOLOR(value);
        public static bool operator ==(UI_HSBCOLOR left, UI_HSBCOLOR right) => left.Value == right.Value;
        public static bool operator !=(UI_HSBCOLOR left, UI_HSBCOLOR right) => !(left == right);

        public bool Equals(UI_HSBCOLOR other) => this.Value == other.Value;

        public override bool Equals(object obj) => obj is UI_HSBCOLOR other && this.Equals(other);

        public override int GetHashCode() => this.Value.GetHashCode();

        public byte Hue => (byte)Value;

        public byte Saturation => (byte)(Value >> 8);

        public byte Brightness => (byte)(Value >> 16);
    }

    public partial struct COLORREF
    : IEquatable<COLORREF>
    {
        public readonly uint Value;
        public COLORREF(uint value) => this.Value = value;
        public static implicit operator uint(COLORREF value) => value.Value;
        public static explicit operator COLORREF(uint value) => new COLORREF(value);
        public static bool operator ==(COLORREF left, COLORREF right) => left.Value == right.Value;
        public static bool operator !=(COLORREF left, COLORREF right) => !(left == right);

        public bool Equals(COLORREF other) => this.Value == other.Value;

        public override bool Equals(object obj) => obj is COLORREF other && this.Equals(other);

        public override int GetHashCode() => this.Value.GetHashCode();

        public byte GetRValue => (byte)(Value & 0xff);

        public byte GetGValue => (byte)(Value >> 8 & 0xff);

        public byte GetBValue => (byte)(Value >> 16 & 0xff);

        public COLORREF(Color color) => this.Value = (uint)ColorTranslator.ToWin32(color);
        public Color ToColor() { return ColorTranslator.FromWin32((int)Value); }
    }

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
    /// Class for color conversions
    /// </summary>
    public static class ColorHelper
    {
        /// <summary>
        /// User defined function to convert from Color to HSB
        /// </summary>
        public static Func<Color, uint> UserColorToHSB { private get; set; }

        /// <summary>
        /// User defined function to convert from HSB to Color
        /// </summary>
        public static Func<uint, Color> UserHSBtoColor { private get; set; }

        /// <summary>
        /// Convert RGB Color to Ribbon HSB Color format
        /// </summary>
        /// <param name="color">RGB Color</param>
        /// <returns>Ribbon Color format</returns>
        public static uint ColorToHSB(Color color)
        {
            if (UserColorToHSB != null)
                return UserColorToHSB(color);
            return MSDocLike.ColorToHSB(color);
        }

        /// <summary>
        /// Convert Ribbon HSB Color format to RGB Color
        /// </summary>
        /// <param name="value">Ribbon Color format</param>
        /// <returns>RGB Color</returns>
        public static Color HSBtoColor(uint value)
        {
            if (UserHSBtoColor != null)
                return UserHSBtoColor(value);
            return MSDocLike.HSBtoColor(new UI_HSBCOLOR(value));
        }

        public static class MSDocLike
        {
            /// <summary>
            /// Convert RGB Color to Ribbon HSB Color format
            /// </summary>
            /// <param name="color">RGB Color</param>
            /// <returns>Ribbon Color format</returns>
            public static uint ColorToHSB(Color color)
            {
                HSL hsl = RGBToHsl(color.R, color.G, color.B);
                UI_HSBCOLOR hsb = HSLToHSB(new HSL(hsl.H / 360.0, hsl.S, hsl.L));
                return hsb;
            }

            /// <summary>
            /// Convert Ribbon HSB Color format to RGB Color
            /// </summary>
            /// <param name="hsb">Ribbon Color format</param>
            /// <returns>RGB Color</returns>
            public static Color HSBtoColor(UI_HSBCOLOR hsb)
            {
                HSL hsl = HSBToHSL(hsb);
                Color result = HslToRgb(hsl.H * 360.0, hsl.S, hsl.L);
                return result;
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
            public static UI_HSBCOLOR HSLToHSB(HSL hsl)
            {
                byte hue, saturation, brightness;

                hue = (byte)Math.Round(255.0 * hsl.H);
                saturation = (byte)Math.Round(255.0 * hsl.S);
                if (hsl.L < 0.1793)
                    brightness = 0;
                else if (hsl.L > 0.9821)
                    brightness = 0xff;
                else
                    brightness = (byte)Math.Round(257.7 + 149.9 * Math.Log(hsl.L));

                return new UI_HSBCOLOR(hue, saturation, brightness);
            }

            //HSL.H [0..1], HSL.S [0..1], HSL.L [0..1]
            //HSB.H [0..255], HSB.S [0..255], HSB.L [0..255]
            /// <summary>
            /// HSB to HSL conversion. All values of HSL in Range [0..1]
            /// </summary>
            /// <param name="hsb"></param>
            /// <returns></returns>
            public static HSL HSBToHSL(UI_HSBCOLOR hsb)
            {
                HSL hsl = new HSL();
                byte brightness = hsb.Brightness;
                double ld;
                // Convert Brightness to Luminance
                if (brightness == 0)
                    ld = 0.0;
                else if (brightness == 0xff)
                    ld = 1.0;
                else
                    ld = Math.Exp((brightness - 257.7) / 149.9);

                // HSLToRGB requires H, L and S to be in 0..1 range.
                hsl.H = (hsb.Hue) / 255.0;
                hsl.S = (hsb.Saturation) / 255.0;
                hsl.L = ld;
                return hsl;
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
        }

        public static class Arik
        {
            /// <summary>
            /// Convert RGB Color to Ribbon HSB Color format
            /// </summary>
            /// <param name="color">RGB Color</param>
            /// <returns>Ribbon Color format</returns>
            public static uint ColorToHSB(Color color)
            {
                HSL hsl = RGB2HSL(color);
                return HSL2HSB(hsl);
            }

            /// <summary>
            /// Convert Ribbon HSB Color format to RGB Color
            /// </summary>
            /// <param name="hsb">Ribbon Color format</param>
            /// <returns>RGB Color</returns>
            public static Color HSBtoColor(UI_HSBCOLOR hsb)
            {
                HSL hsl = HSBToHSL(hsb);
                Color result = HSL2RGB(hsl);
                return result;
            }

            // based on http://www.geekymonkey.com/Programming/CSharp/RGB2HSL_HSL2RGB.htm
            // Given H,S,L in range of 0-1
            // Returns a Color (RGB struct) in range of 0-255
            public static Color HSL2RGB(HSL hsl)
            {
                double v;
                double r, g, b;

                r = hsl.L;   // default to gray
                g = hsl.L;
                b = hsl.L;
                v = (hsl.L <= 0.5) ? (hsl.L * (1.0 + hsl.S)) : (hsl.L + hsl.S - hsl.L * hsl.S);
                if (v > 0)
                {
                    double m;
                    double sv;
                    int sextant;
                    double fract, vsf, mid1, mid2;

                    m = hsl.L + hsl.L - v;
                    sv = (v - m) / v;
                    hsl.H *= 6.0;
                    sextant = (int)hsl.H;
                    fract = hsl.H - sextant;
                    vsf = v * sv * fract;
                    mid1 = m + vsf;
                    mid2 = v - vsf;

                    switch (sextant)
                    {
                        case 0:
                            r = v;
                            g = mid1;
                            b = m;
                            break;

                        case 1:
                            r = mid2;
                            g = v;
                            b = m;
                            break;

                        case 2:
                            r = m;
                            g = v;
                            b = mid1;
                            break;

                        case 3:
                            r = m;
                            g = mid2;
                            b = v;
                            break;

                        case 4:
                            r = mid1;
                            g = m;
                            b = v;
                            break;

                        case 5:
                            r = v;
                            g = m;
                            b = mid2;
                            break;
                    }
                }
                return Color.FromArgb(Convert.ToByte(r * 255.0f), Convert.ToByte(g * 255.0f), Convert.ToByte(b * 255.0f));
            }

            //HSL.H [0..1], HSL.S [0..1], HSL.L [0..1]
            //HSB.H [0..255], HSB.S [0..255], HSB.L [0..255]
            /// <summary>
            /// HSB to HSL conversion. All values of HSL in Range [0..1]
            /// </summary>
            /// <param name="hsb"></param>
            /// <returns></returns>
            public static HSL HSBToHSL(UI_HSBCOLOR hsb)
            {
                HSL hsl = new HSL();
                double ld;
                byte brightness = hsb.Brightness;
                // Convert B to L
                if (brightness == 0)
                    ld = 0.0;
                else if (brightness == 0xff)
                    ld = 1.0;
                else
                    ld = Math.Exp((brightness - 257.7) / 149.9);

                // HSLToRGB requires H, L and S to be in 0..1 range.
                hsl.H = (hsb.Hue) / 255.0;
                hsl.S = (hsb.Saturation) / 255.0;
                hsl.L = ld;
                return hsl;
            }

            // Given a Color (RGB Struct) in range of 0-255
            // Return H,S,L in range of 0-1
            public static HSL RGB2HSL(Color rgb)
            {
                HSL hsl;

                double r = rgb.R / 255.0;
                double g = rgb.G / 255.0;
                double b = rgb.B / 255.0;
                double v;
                double m;
                double vm;
                double r2, g2, b2;

                hsl.H = 0; // default to black
                hsl.S = 0;
                hsl.L = 0;
                v = Math.Max(r, g);
                v = Math.Max(v, b);
                m = Math.Min(r, g);
                m = Math.Min(m, b);
                hsl.L = (m + v) / 2.0;

                if (hsl.L <= 0.0)
                {
                    return hsl;
                }

                vm = v - m;
                hsl.S = vm;

                if (hsl.S > 0.0)
                {
                    hsl.S /= (hsl.L <= 0.5) ? (v + m) : (2.0 - v - m);
                }
                else
                {
                    return hsl;
                }

                r2 = (v - r) / vm;
                g2 = (v - g) / vm;
                b2 = (v - b) / vm;

                if (r == v)
                {
                    hsl.H = (g == m ? 5.0 + b2 : 1.0 - g2);
                }
                else if (g == v)
                {
                    hsl.H = (b == m ? 1.0 + r2 : 3.0 - b2);
                }
                else
                {
                    hsl.H = (r == m ? 3.0 + g2 : 5.0 - r2);
                }

                hsl.H /= 6.0;

                return hsl;
            }

            public static UI_HSBCOLOR HSL2HSB(HSL hsl)
            {
                byte hue, saturation, brightness;
                hue = (byte)(255.0 * hsl.H);
                saturation = (byte)(255.0 * hsl.S);
                if ((0.1793 <= hsl.L) && (hsl.L <= 0.9821))
                {
                    brightness = (byte)(257.7 + 149.9 * Math.Log(hsl.L));
                }
                else
                {
                    brightness = 0;
                }

                return new UI_HSBCOLOR(hue, saturation, brightness);
            }
        }

        public static class Tortoise
        {
            ///// <summary>
            ///// Main function
            ///// </summary>
            ///// <param name="hsb"></param>
            ///// <returns></returns>
            //public static Color HSBtoColor(UI_HSBCOLOR hsb)
            //{
            //    float brightness = (float)(hsb.Brightness / 255.0);
            //    float saturation = (float)(hsb.Saturation / 255.0);
            //    float hue = (float)(hsb.Hue / 255.0);

            //    throw new NotSupportedException("Not supported, take JavaAwt function"); // return Color.FromArgb((int)HSBtoRGB(hue, saturation, brightness));
            //}

            public static uint ColorToHSB(Color color)
            {
                ColorToHSB(color, out byte hue, out byte saturation, out byte brightness);
                return new UI_HSBCOLOR(hue, saturation, brightness);
            }

            public static uint ColorRefToHSB(COLORREF color)
            {
                RGBtoHSB(color, out byte hue, out byte saturation, out byte brightness);
                return new UI_HSBCOLOR(hue, saturation, brightness);
            }

            private static void ColorToHSB(Color rgb, out byte hue, out byte saturation, out byte brightness)
            {
                RGBtoHSB(new COLORREF((uint)ColorTranslator.ToWin32(rgb)), out hue, out saturation, out brightness);
            }

            public static void RGBtoHSB(COLORREF rgb, out byte hue, out byte saturation, out byte brightness)
            {
                byte r = rgb.GetRValue;
                byte g = rgb.GetGValue;
                byte b = rgb.GetBValue;
                byte minRGB = Math.Min(Math.Min(r, g), b);
                byte maxRGB = Math.Max(Math.Max(r, g), b);
                byte delta = (byte)(maxRGB - minRGB);
                double l = maxRGB;
                double s = 0.0;
                double h = 0.0;
                if (maxRGB == 0)
                {
                    hue = 0;
                    saturation = 0;
                    brightness = 0;
                    return;
                }
                if (maxRGB != 0)
                    s = (255.0 * delta) / maxRGB;

                if ((byte)(s) != 0)
                {
                    if (r == maxRGB)
                        h = 0 + 43 * (double)(g - b) / delta;
                    else if (g == maxRGB)
                        h = 85 + 43 * (double)(b - r) / delta;
                    else if (b == maxRGB)
                        h = 171 + 43 * (double)(r - g) / delta;
                }
                else
                    h = 0.0;

                hue = (byte)(h);
                saturation = (byte)(s);
                brightness = (byte)(l);
            }
        }

        public static class JavaAwt
        {
            /// <summary>
            /// Main function
            /// </summary>
            /// <param name="hsb"></param>
            /// <returns></returns>
            public static Color HSBtoColor(UI_HSBCOLOR hsb)
            {
                float brightness = (float)(hsb.Brightness / 255.0);
                float saturation = (float)(hsb.Saturation / 255.0);
                float hue = (float)(hsb.Hue / 255.0);

                return Color.FromArgb((int)HSBtoRGB(hue, saturation, brightness));
            }

            /// <summary>
            /// Converts the components of a color, as specified by the HSB
            /// model, to an equivalent set of values for the default RGB model.
            /// 
            /// The {@code saturation} and {@code brightness} components
            /// should be floating-point values between zero and one
            /// (numbers in the range 0.0-1.0).  The {@code hue} component
            /// can be any floating-point number.  The floor of this number is
            /// subtracted from it to create a fraction between 0 and 1.  This
            /// fractional number is then multiplied by 360 to produce the hue
            /// angle in the HSB color model.
            /// 
            /// The integer that is returned by {@code HSBtoRGB} encodes the
            /// value of a color in bits 0-23 of an integer value that is the same
            /// format used by the method {@link #getRGB() getRGB}.
            /// This integer can be supplied as an argument to the
            /// {@code Color} constructor that takes a single integer argument.
            /// </summary>
            /// <param name="hue">the hue component of the color</param>
            /// <param name="saturation">the saturation of the color</param>
            /// <param name="brightness">the brightness of the color</param>
            /// <returns>the RGB value of the color with the indicated hue,
            /// saturation, and brightness.</returns>
            /// <remarks>From java.awt.Color</remarks>
            public static uint HSBtoRGB(float hue, float saturation, float brightness)
            {
                int r = 0, g = 0, b = 0;
                if (saturation == 0)
                {
                    r = g = b = (int)(brightness * 255.0f + 0.5f);
                }
                else
                {
                    float h = (hue - (float)Math.Floor(hue)) * 6.0f;
                    float f = h - (float)Math.Floor(h);
                    float p = brightness * (1.0f - saturation);
                    float q = brightness * (1.0f - saturation * f);
                    float t = brightness * (1.0f - (saturation * (1.0f - f)));
                    switch ((int)h)
                    {
                        case 0:
                            r = (int)(brightness * 255.0f + 0.5f);
                            g = (int)(t * 255.0f + 0.5f);
                            b = (int)(p * 255.0f + 0.5f);
                            break;
                        case 1:
                            r = (int)(q * 255.0f + 0.5f);
                            g = (int)(brightness * 255.0f + 0.5f);
                            b = (int)(p * 255.0f + 0.5f);
                            break;
                        case 2:
                            r = (int)(p * 255.0f + 0.5f);
                            g = (int)(brightness * 255.0f + 0.5f);
                            b = (int)(t * 255.0f + 0.5f);
                            break;
                        case 3:
                            r = (int)(p * 255.0f + 0.5f);
                            g = (int)(q * 255.0f + 0.5f);
                            b = (int)(brightness * 255.0f + 0.5f);
                            break;
                        case 4:
                            r = (int)(t * 255.0f + 0.5f);
                            g = (int)(p * 255.0f + 0.5f);
                            b = (int)(brightness * 255.0f + 0.5f);
                            break;
                        case 5:
                            r = (int)(brightness * 255.0f + 0.5f);
                            g = (int)(p * 255.0f + 0.5f);
                            b = (int)(q * 255.0f + 0.5f);
                            break;
                    }
                }
                return (0xff000000 | ((uint)r << 16) | ((uint)g << 8) | ((uint)b << 0));
            }

            /// <summary>
            /// Main function
            /// </summary>
            /// <param name="color"></param>
            /// <returns></returns>
            public static UI_HSBCOLOR ColorToHSB(Color color)
            {
                float[] floatHSB = RGBtoHSB(color.R, color.G, color.B, new float[3]);
                return new UI_HSBCOLOR((byte)(floatHSB[0] * 255.0f), (byte)(floatHSB[1] * 255.0f), (byte)(floatHSB[2] * 255.0f));
            }

            /// <summary>
            /// Converts the components of a color, as specified by the default RGB
            /// model, to an equivalent set of values for hue, saturation, and
            /// brightness that are the three components of the HSB model.
            /// 
            /// If the {@code hsbvals} argument is {@code null}, then a
            /// new array is allocated to return the result. Otherwise, the method
            /// returns the array {@code hsbvals}, with the values put into
            /// that array.
            /// </summary>
            /// <param name="r">the red component of the color</param>
            /// <param name="g">the green component of the color</param>
            /// <param name="b">the blue component of the color</param>
            /// <param name="hsbvals">the array used to return the
            ///               three HSB values, or {@code null}</param>
            /// <returns>an array of three elements containing the hue, saturation,
            /// and brightness (in that order), of the color with
            /// the indicated red, green, and blue components.</returns>
            /// <remarks>From java.awt.Color</remarks>
            public static float[] RGBtoHSB(int r, int g, int b, float[] hsbvals)
            {
                float hue, saturation, brightness;
                if (hsbvals == null)
                {
                    hsbvals = new float[3];
                }
                int cmax = (r > g) ? r : g;
                if (b > cmax) cmax = b;
                int cmin = (r < g) ? r : g;
                if (b < cmin) cmin = b;

                brightness = ((float)cmax) / 255.0f;
                if (cmax != 0)
                    saturation = ((float)(cmax - cmin)) / ((float)cmax);
                else
                    saturation = 0;
                if (saturation == 0)
                    hue = 0;
                else
                {
                    float redc = ((float)(cmax - r)) / ((float)(cmax - cmin));
                    float greenc = ((float)(cmax - g)) / ((float)(cmax - cmin));
                    float bluec = ((float)(cmax - b)) / ((float)(cmax - cmin));
                    if (r == cmax)
                        hue = bluec - greenc;
                    else if (g == cmax)
                        hue = 2.0f + redc - bluec;
                    else
                        hue = 4.0f + greenc - redc;
                    hue = hue / 6.0f;
                    if (hue < 0)
                        hue = hue + 1.0f;
                }
                hsbvals[0] = hue;
                hsbvals[1] = saturation;
                hsbvals[2] = brightness;
                return hsbvals;
            }

            //**
            // * Creates a {@code Color} object based on the specified values
            // * for the HSB color model.
            // * 
            // * The {@code s} and {@code b} components should be
            // * floating-point values between zero and one
            // * (numbers in the range 0.0-1.0).  The {@code h} component
            // * can be any floating-point number.  The floor of this number is
            // * subtracted from it to create a fraction between 0 and 1.  This
            // * fractional number is then multiplied by 360 to produce the hue
            // * angle in the HSB color model.
            // * @param  h   the hue component
            // * @param  s   the saturation of the color
            // * @param  b   the brightness of the color
            // * @return  a {@code Color} object with the specified hue,
            // *                                 saturation, and brightness.
            // */
            //public static Color GetHSBColor(float h, float s, float b)
            //{
            //    return Color.FromArgb((int)HSBtoRGB(h, s, b));
            //}
        }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}
