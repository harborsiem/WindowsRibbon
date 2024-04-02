using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace RibbonLib.Interop
{
    internal static class PInvoke
    {
        /// <summary>Frees the loaded dynamic-link library (DLL) module and, if necessary, decrements its reference count.</summary>
        /// <param name="hLibModule">
        /// <para>A handle to the loaded library module. The <a href="https://docs.microsoft.com/windows/desktop/api/libloaderapi/nf-libloaderapi-loadlibrarya">LoadLibrary</a>, <a href="https://docs.microsoft.com/windows/desktop/api/libloaderapi/nf-libloaderapi-loadlibraryexa">LoadLibraryEx</a>, <a href="https://docs.microsoft.com/windows/desktop/api/libloaderapi/nf-libloaderapi-getmodulehandlea">GetModuleHandle</a>, or <a href="https://docs.microsoft.com/windows/desktop/api/libloaderapi/nf-libloaderapi-getmodulehandleexa">GetModuleHandleEx</a> function returns this handle.</para>
        /// <para><see href="https://docs.microsoft.com/windows/win32/api//libloaderapi/nf-libloaderapi-freelibrary#parameters">Read more on docs.microsoft.com</see>.</para>
        /// </param>
        /// <returns>
        /// <para>If the function succeeds, the return value is nonzero. If the function fails, the return value is zero. To get extended error information, call the <a href="/windows/desktop/api/errhandlingapi/nf-errhandlingapi-getlasterror">GetLastError</a> function.</para>
        /// </returns>
        /// <remarks>
        /// <para><see href="https://docs.microsoft.com/windows/win32/api//libloaderapi/nf-libloaderapi-freelibrary">Learn more about this API from docs.microsoft.com</see>.</para>
        /// </remarks>
        [DllImport("KERNEL32.dll", ExactSpelling = true, SetLastError = true)]
        //[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        //[SupportedOSPlatform("windows5.1.2600")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool FreeLibrary(IntPtr hLibModule);

        ///// <inheritdoc cref="LoadLibrary(winmdroot.Foundation.PCWSTR)"/>
        //[SupportedOSPlatform("windows5.1.2600")]
        [DllImport("KERNEL32.dll", ExactSpelling = true, EntryPoint = "LoadLibraryW", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern IntPtr LoadLibrary(string lpLibFileName);
        //{
        //    fixed (char* lpLibFileNameLocal = lpLibFileName)
        //    {
        //        winmdroot.Foundation.HINSTANCE __result = PInvoke.LoadLibrary(lpLibFileNameLocal);
        //        return __result;
        //    }
        //}

        ///// <summary>Loads the specified module into the address space of the calling process.</summary>
        ///// <param name="lpLibFileName">
        ///// <para>The name of the module. This can be either a library module (a .dll file) or an executable module (an .exe file). The name specified is the file name of the module and is not related to the name stored in the library module itself, as specified by the <b>LIBRARY</b> keyword in the module-definition (.def) file. If the string specifies a full path, the function searches only that path for the module. If the string specifies a relative path or a module name without a path, the function uses a standard search strategy to find the module; for more information, see the Remarks. If the function cannot find the  module, the function fails. When specifying a path, be sure to use backslashes (\\), not forward slashes (/). For more information about paths, see <a href="https://docs.microsoft.com/windows/desktop/FileIO/naming-a-file">Naming a File or Directory</a>. If the string specifies a module name without a path and the file name extension is omitted, the function appends the default library extension .dll to the module name. To prevent the function from appending .dll to the module name, include a trailing point character (.) in the module name string.</para>
        ///// <para><see href="https://docs.microsoft.com/windows/win32/api//libloaderapi/nf-libloaderapi-loadlibraryw#parameters">Read more on docs.microsoft.com</see>.</para>
        ///// </param>
        ///// <returns>
        ///// <para>If the function succeeds, the return value is a handle to the module. If the function fails, the return value is NULL. To get extended error information, call <a href="/windows/desktop/api/errhandlingapi/nf-errhandlingapi-getlasterror">GetLastError</a>.</para>
        ///// </returns>
        ///// <remarks>
        ///// <para><see href="https://docs.microsoft.com/windows/win32/api//libloaderapi/nf-libloaderapi-loadlibraryw">Learn more about this API from docs.microsoft.com</see>.</para>
        ///// </remarks>
        //[DllImport("KERNEL32.dll", ExactSpelling = true, EntryPoint = "LoadLibraryW", SetLastError = true)]
        ////[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        ////[SupportedOSPlatform("windows5.1.2600")]
        //public static extern winmdroot.Foundation.HINSTANCE LoadLibrary(winmdroot.Foundation.PCWSTR lpLibFileName);

        ///// <inheritdoc cref="LoadLibraryEx(winmdroot.Foundation.PCWSTR, winmdroot.Foundation.HANDLE, winmdroot.System.LibraryLoader.LOAD_LIBRARY_FLAGS)"/>
        //[SupportedOSPlatform("windows5.1.2600")]
        [DllImport("KERNEL32.dll", ExactSpelling = true, EntryPoint = "LoadLibraryExW", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern IntPtr LoadLibraryEx(string lpLibFileName, IntPtr hFile, LOAD_LIBRARY_FLAGS dwFlags);
        //{
        //    fixed (char* lpLibFileNameLocal = lpLibFileName)
        //    {
        //        winmdroot.Foundation.HINSTANCE __result = PInvoke.LoadLibraryEx(lpLibFileNameLocal, hFile, dwFlags);
        //        return __result;
        //    }
        //}

        /// <summary>The GetObject function retrieves information for the specified graphics object.</summary>
        /// <param name="h">A handle to the graphics object of interest. This can be a handle to one of the following: a logical bitmap, a brush, a font, a palette, a pen, or a device independent bitmap created by calling the <a href="https://docs.microsoft.com/windows/desktop/api/wingdi/nf-wingdi-createdibsection">CreateDIBSection</a> function.</param>
        /// <param name="c">The number of bytes of information to be written to the buffer.</param>
        /// <param name="pv">
        /// <para>A pointer to a buffer that receives the information about the specified graphics object. The following table shows the type of information the buffer receives for each type of graphics object you can specify with <i>hgdiobj</i>. </para>
        /// <para>This doc was truncated.</para>
        /// <para><see href="https://docs.microsoft.com/windows/win32/api//wingdi/nf-wingdi-getobjectw#parameters">Read more on docs.microsoft.com</see>.</para>
        /// </param>
        /// <returns>
        /// <para>If the function succeeds, and <i>lpvObject</i> is a valid pointer, the return value is the number of bytes stored into the buffer. If the function succeeds, and <i>lpvObject</i> is <b>NULL</b>, the return value is the number of bytes required to hold the information the function would store into the buffer. If the function fails, the return value is zero.</para>
        /// </returns>
        /// <remarks>
        /// <para><see href="https://docs.microsoft.com/windows/win32/api//wingdi/nf-wingdi-getobjectw">Learn more about this API from docs.microsoft.com</see>.</para>
        /// </remarks>
        [DllImport("GDI32.dll", ExactSpelling = true, EntryPoint = "GetObjectW")]
        //[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        //[SupportedOSPlatform("windows5.0")]
        public static extern unsafe int GetObject(IntPtr h, int c, [Optional] void* pv);

        /// <summary>The DeleteObject function deletes a logical pen, brush, font, bitmap, region, or palette, freeing all system resources associated with the object. After the object is deleted, the specified handle is no longer valid.</summary>
        /// <param name="ho">A handle to a logical pen, brush, font, bitmap, region, or palette.</param>
        /// <returns>
        /// <para>If the function succeeds, the return value is nonzero. If the specified handle is not valid or is currently selected into a DC, the return value is zero.</para>
        /// </returns>
        /// <remarks>
        /// <para><see href="https://docs.microsoft.com/windows/win32/api//wingdi/nf-wingdi-deleteobject">Learn more about this API from docs.microsoft.com</see>.</para>
        /// </remarks>
        [DllImport("GDI32.dll", ExactSpelling = true)]
        //[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        //[SupportedOSPlatform("windows5.0")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteObject(IntPtr ho);

        [DllImport("KERNEL32.dll", ExactSpelling = true, EntryPoint = "FindResourceW", CharSet= CharSet.Unicode)]
        //[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        public static extern IntPtr FindResource(IntPtr hModule, string lpName, string lpType);

        [DllImport("KERNEL32.dll", ExactSpelling = true, EntryPoint = "SizeofResource")]
        public static extern uint SizeofResource(IntPtr hModule, IntPtr hResInfo);

    }

    [Flags]
    //[global::System.CodeDom.Compiler.GeneratedCode("Microsoft.Windows.CsWin32", "0.2.176-beta+76e706ea41")]
    internal enum LOAD_LIBRARY_FLAGS : uint
    {
        DONT_RESOLVE_DLL_REFERENCES = 0x00000001,
        LOAD_LIBRARY_AS_DATAFILE = 0x00000002,
        LOAD_WITH_ALTERED_SEARCH_PATH = 0x00000008,
        LOAD_IGNORE_CODE_AUTHZ_LEVEL = 0x00000010,
        LOAD_LIBRARY_AS_IMAGE_RESOURCE = 0x00000020,
        LOAD_LIBRARY_AS_DATAFILE_EXCLUSIVE = 0x00000040,
        LOAD_LIBRARY_REQUIRE_SIGNED_TARGET = 0x00000080,
        LOAD_LIBRARY_SEARCH_DLL_LOAD_DIR = 0x00000100,
        LOAD_LIBRARY_SEARCH_APPLICATION_DIR = 0x00000200,
        LOAD_LIBRARY_SEARCH_USER_DIRS = 0x00000400,
        LOAD_LIBRARY_SEARCH_SYSTEM32 = 0x00000800,
        LOAD_LIBRARY_SEARCH_DEFAULT_DIRS = 0x00001000,
        LOAD_LIBRARY_SAFE_CURRENT_DIRS = 0x00002000,
        LOAD_LIBRARY_SEARCH_SYSTEM32_NO_FORWARDER = 0x00004000,
    }

    /// <summary>The BITMAP structure defines the type, width, height, color format, and bit values of a bitmap.</summary>
    /// <remarks>
    /// <para>The bitmap formats currently used are monochrome and color. The monochrome bitmap uses a one-bit, one-plane format. Each scan is a multiple of 16 bits. Scans are organized as follows for a monochrome bitmap of height <i>n</i>: <pre class="syntax" xml:space="preserve"><code> Scan 0 Scan 1 . . . Scan n-2 Scan n-1 </code></pre> The pixels on a monochrome device are either black or white. If the corresponding bit in the bitmap is 1, the pixel is set to the foreground color; if the corresponding bit in the bitmap is zero, the pixel is set to the background color. All devices that have the RC_BITBLT device capability support bitmaps. For more information, see <a href="https://docs.microsoft.com/windows/desktop/api/wingdi/nf-wingdi-getdevicecaps">GetDeviceCaps</a>. Each device has a unique color format. To transfer a bitmap from one device to another, use the <a href="https://docs.microsoft.com/windows/desktop/api/wingdi/nf-wingdi-getdibits">GetDIBits</a> and <a href="https://docs.microsoft.com/windows/desktop/api/wingdi/nf-wingdi-setdibits">SetDIBits</a> functions.</para>
    /// <para><see href="https://docs.microsoft.com/windows/win32/api//wingdi/ns-wingdi-bitmap#">Read more on docs.microsoft.com</see>.</para>
    /// </remarks>
    //[global::System.CodeDom.Compiler.GeneratedCode("Microsoft.Windows.CsWin32", "0.2.219-beta+aaafe7c65c")]
    public partial struct BITMAP
    {
        /// <summary>The bitmap type. This member must be zero.</summary>
        public int bmType;
        /// <summary>The width, in pixels, of the bitmap. The width must be greater than zero.</summary>
        public int bmWidth;
        /// <summary>The height, in pixels, of the bitmap. The height must be greater than zero.</summary>
        public int bmHeight;
        /// <summary>The number of bytes in each scan line. This value must be divisible by 2, because the system assumes that the bit values of a bitmap form an array that is word aligned.</summary>
        public int bmWidthBytes;
        /// <summary>The count of color planes.</summary>
        public ushort bmPlanes;
        /// <summary>The number of bits required to indicate the color of a pixel.</summary>
        public ushort bmBitsPixel;
        /// <summary>A pointer to the location of the bit values for the bitmap. The <b>bmBits</b> member must be a pointer to an array of character (1-byte) values.</summary>
        public unsafe void* bmBits;
    }

    ///// <summary>
    ///// A pointer to a null-terminated, constant character string.
    ///// </summary>
    //[DebuggerDisplay("{" + nameof(DebuggerDisplay) + "}")]
    //public unsafe partial struct PCWSTR
    //    : IEquatable<PCWSTR>
    //{
    //    public readonly char* Value;

    //    public PCWSTR(char* value) => this.Value = value;

    //    public static explicit operator char* (PCWSTR value) => value.Value;

    //    public static implicit operator PCWSTR(char* value) => new PCWSTR(value);

    //    public bool Equals(PCWSTR other) => this.Value == other.Value;

    //    public override bool Equals(object obj) => obj is PCWSTR other && this.Equals(other);

    //    public override int GetHashCode() => unchecked((int)this.Value);

    //    public int Length
    //    {
    //        get
    //        {
    //            char* p = this.Value;
    //            if (p == null)
    //                return 0;
    //            while (*p != '\0')
    //                p++;
    //            return checked((int)(p - this.Value));
    //        }
    //    }


    //    /// <summary>
    //    /// Returns a <see langword="string"/> with a copy of this character array, up to the first null character (exclusive).
    //    /// </summary>
    //    /// <returns>A <see langword="string"/>, or <see langword="null"/> if <see cref="Value"/> is <see langword="null"/>.</returns>
    //    public override string ToString() => this.Value == null ? null : new string(this.Value);

    //    //public ReadOnlySpan<char> AsSpan() => this.Value is null ? default(ReadOnlySpan<char>) : new ReadOnlySpan<char>(this.Value, this.Length);


    //    private string DebuggerDisplay => this.ToString();
    //}

}
