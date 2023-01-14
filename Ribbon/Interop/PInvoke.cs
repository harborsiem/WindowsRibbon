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
}
