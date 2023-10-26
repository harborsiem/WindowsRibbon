using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Diagnostics;
using Microsoft.Win32.SafeHandles;

namespace UIRibbonTools
{
    class NativeMethods
    {
        public const int DT_RIGHT = 0x00000002;
        public const int DT_SINGLELINE = 0x00000020;
        public const int DT_NOCLIP = 0x00000100;
        public const int DT_NOPREFIX = 0x00000800;
        public const int DT_CALCRECT = 0x00000400;
        public const UInt32 WM_KEYDOWN = 0x0100;
        public const int WM_SETREDRAW = 0x0b;

        //Unsafe
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(HandleRef hWnd, int msg, IntPtr wParam, IntPtr lParam);
        //Unsafe
        [DllImport("user32.dll")]
        public static extern bool PostMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);
        //Unsafe
        [DllImport("user32.dll")]
        public static extern int PostMessage(HandleRef hwnd, int msg, IntPtr wparam, IntPtr lparam);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern uint GetConsoleProcessList(
                uint[] processList,
                uint processCount
                );
        /// <summary>
        /// allocates a new console for the calling process.
        /// </summary>
        /// <returns>If the function succeeds, the return value is nonzero.
        /// If the function fails, the return value is zero. 
        /// To get extended error information, call Marshal.GetLastWin32Error.</returns>
        [DllImport("kernel32", SetLastError = true)]
        public static extern bool AllocConsole();
        /// <summary>
        /// Detaches the calling process from its console
        /// </summary>
        /// <returns>If the function succeeds, the return value is nonzero.
        /// If the function fails, the return value is zero. 
        /// To get extended error information, call Marshal.GetLastWin32Error.</returns>
        [DllImport("kernel32", SetLastError = true)]
        public static extern bool FreeConsole();

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool AttachConsole(uint dwProcessId);

        [DllImport("kernel32.dll")]
        private static extern bool GetFileInformationByHandle(
        SafeFileHandle hFile,
        out BY_HANDLE_FILE_INFORMATION lpFileInformation
        );
        [DllImport("kernel32.dll")]
        private static extern SafeFileHandle GetStdHandle(UInt32 nStdHandle);
        [DllImport("kernel32.dll")]
        private static extern bool SetStdHandle(UInt32 nStdHandle, SafeFileHandle hHandle);
        [DllImport("kernel32.dll")]
        private static extern bool DuplicateHandle(
            IntPtr hSourceProcessHandle,
            SafeFileHandle hSourceHandle,
            IntPtr hTargetProcessHandle,
            out SafeFileHandle lpTargetHandle,
            UInt32 dwDesiredAccess,
            Boolean bInheritHandle,
            UInt32 dwOptions
            );

        public const uint ATTACH_PARENT_PROCESS = 0x0ffffffff;  // default value if not specifing a process ID
        private const UInt32 STD_OUTPUT_HANDLE = 0xFFFFFFF5;
        private const UInt32 STD_ERROR_HANDLE = 0xFFFFFFF4;
        private const UInt32 DUPLICATE_SAME_ACCESS = 2;

        struct BY_HANDLE_FILE_INFORMATION
        {
            public UInt32 FileAttributes;
            public System.Runtime.InteropServices.ComTypes.FILETIME CreationTime;
            public System.Runtime.InteropServices.ComTypes.FILETIME LastAccessTime;
            public System.Runtime.InteropServices.ComTypes.FILETIME LastWriteTime;
            public UInt32 VolumeSerialNumber;
            public UInt32 FileSizeHigh;
            public UInt32 FileSizeLow;
            public UInt32 NumberOfLinks;
            public UInt32 FileIndexHigh;
            public UInt32 FileIndexLow;
        }

        public static void InitConsoleHandles()
        {
            SafeFileHandle hStdOut, hStdErr, hStdOutDup, hStdErrDup;
            BY_HANDLE_FILE_INFORMATION bhfi;
            hStdOut = GetStdHandle(STD_OUTPUT_HANDLE);
            hStdErr = GetStdHandle(STD_ERROR_HANDLE);
            // Get current process handle
            IntPtr hProcess = Process.GetCurrentProcess().Handle;
            // Duplicate Stdout handle to save initial value
            DuplicateHandle(hProcess, hStdOut, hProcess, out hStdOutDup,
            0, true, DUPLICATE_SAME_ACCESS);
            // Duplicate Stderr handle to save initial value
            DuplicateHandle(hProcess, hStdErr, hProcess, out hStdErrDup,
            0, true, DUPLICATE_SAME_ACCESS);
            // Attach to console window – this may modify the standard handles
            AttachConsole(ATTACH_PARENT_PROCESS);
            // Adjust the standard handles
            if (GetFileInformationByHandle(GetStdHandle(STD_OUTPUT_HANDLE), out bhfi))
            {
                SetStdHandle(STD_OUTPUT_HANDLE, hStdOutDup);
            }
            else
            {
                SetStdHandle(STD_OUTPUT_HANDLE, hStdOut);
            }
            if (GetFileInformationByHandle(GetStdHandle(STD_ERROR_HANDLE), out bhfi))
            {
                SetStdHandle(STD_ERROR_HANDLE, hStdErrDup);
            }
            else
            {
                SetStdHandle(STD_ERROR_HANDLE, hStdErr);
            }
        }

        public static void SuspendPainting(IntPtr hWnd)
        {
            SendMessage(hWnd, WM_SETREDRAW, (IntPtr)0, IntPtr.Zero);
        }

        public static void ResumePainting(IntPtr hWnd)
        {
            SendMessage(hWnd, WM_SETREDRAW, (IntPtr)1, IntPtr.Zero);
        }

        public enum COMBOBOXINFO_BUTTON_STATE : uint
        {
            STATE_SYSTEM_NONE = 0,
            STATE_SYSTEM_INVISIBLE = 0x00008000,
            STATE_SYSTEM_PRESSED = 0x00000008,
            STATE_SYSTEM_FOCUSABLE = 1048576U,
            STATE_SYSTEM_OFFSCREEN = 65536U,
            STATE_SYSTEM_UNAVAILABLE = 1U,
        }

        public struct COMBOBOXINFO
        {
            public Int32 cbSize;
            public RECT rcItem;
            public RECT rcButton;
            public COMBOBOXINFO_BUTTON_STATE buttonState;
            public IntPtr hwndCombo;
            public IntPtr hwndEdit;
            public IntPtr hwndList;
            public static COMBOBOXINFO Create() => new COMBOBOXINFO { cbSize = Marshal.SizeOf<COMBOBOXINFO>() };
        }

        [DllImport("user32.dll")]
        public static extern bool GetComboBoxInfo(IntPtr hWnd, ref COMBOBOXINFO pcbi);


        /// <summary>
        /// Describes the width, height, and location of a rectangle.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;

            public RECT(int left, int top, int right, int bottom)
            {
                Left = left;
                Top = top;
                Right = right;
                Bottom = bottom;
            }

            public RECT(Rectangle rectangle)
            {
                Left = rectangle.X;
                Top = rectangle.Y;
                Right = rectangle.Right;
                Bottom = rectangle.Bottom;
            }

            public static RECT FromXYWH(int x, int y, int width, int height)
            {
                return new RECT(x, y, x + width, y + height);
            }

            public System.Drawing.Size Size
            {
                get
                {
                    return new System.Drawing.Size(this.Right - this.Left, this.Bottom - this.Top);
                }
            }

            public Rectangle ToRectangle()
            {
                Rectangle r = new Rectangle(Left, Top, Right - Left, Bottom - Top);
                return r;
            }
        }

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern int DrawText(IntPtr hdc, string lpchText, int cchText,
          ref RECT lprc, uint dwDTFormat);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

        public const int TRANSPARENT = 1;
        public const int OPAQUE = 2;
        public enum BACKGROUND_MODE : int
        {
            OPAQUE = 2,
            TRANSPARENT = 1,
        }

        [DllImport("gdi32.dll")]
        public static extern int SetBkMode(IntPtr hdc, int iBkMode);

        [DllImport("gdi32.dll")]
        public static extern uint SetTextColor(IntPtr hdc, int crColor);

        [DllImport("gdi32.dll")]
        static extern int GetDeviceCaps(IntPtr hdc, int nIndex);

        public enum GET_DEVICE_CAPS_INDEX : int
        {
            DRIVERVERSION = 0,
            TECHNOLOGY = 2,
            HORZSIZE = 4,
            VERTSIZE = 6,
            HORZRES = 8,
            VERTRES = 10,
            BITSPIXEL = 12,
            PLANES = 14,
            NUMBRUSHES = 16,
            NUMPENS = 18,
            NUMMARKERS = 20,
            NUMFONTS = 22,
            NUMCOLORS = 24,
            PDEVICESIZE = 26,
            CURVECAPS = 28,
            LINECAPS = 30,
            POLYGONALCAPS = 32,
            TEXTCAPS = 34,
            CLIPCAPS = 36,
            RASTERCAPS = 38,
            ASPECTX = 40,
            ASPECTY = 42,
            ASPECTXY = 44,
            LOGPIXELSX = 88,
            LOGPIXELSY = 90,
            SIZEPALETTE = 104,
            NUMRESERVED = 106,
            COLORRES = 108,
            PHYSICALWIDTH = 110,
            PHYSICALHEIGHT = 111,
            PHYSICALOFFSETX = 112,
            PHYSICALOFFSETY = 113,
            SCALINGFACTORX = 114,
            SCALINGFACTORY = 115,
            VREFRESH = 116,
            DESKTOPVERTRES = 117,
            DESKTOPHORZRES = 118,
            BLTALIGNMENT = 119,
            SHADEBLENDCAPS = 120,
            COLORMGMTCAPS = 121,
        }

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int LoadString(IntPtr hInstance, uint uID, StringBuilder lpBuffer, int nBufferMax);

        public double DpiScaling()
        {
            Graphics g = Graphics.FromHwnd(IntPtr.Zero);
            IntPtr desktop = g.GetHdc();
            int LogicalScreenHeight = GetDeviceCaps(desktop, (int)GET_DEVICE_CAPS_INDEX.VERTRES);
            int PhysicalScreenHeight = GetDeviceCaps(desktop, (int)GET_DEVICE_CAPS_INDEX.DESKTOPVERTRES);
            int logpixelsy = GetDeviceCaps(desktop, (int)GET_DEVICE_CAPS_INDEX.LOGPIXELSY);
            float screenScalingFactor = (float)PhysicalScreenHeight / (float)LogicalScreenHeight;
            float dpiScalingFactor = (float)logpixelsy / (float)96;

            if (screenScalingFactor > 1 ||
                dpiScalingFactor > 1)
            {
                // do something nice for people who can't see very well...
            }
            return dpiScalingFactor;
        }
    }
}
