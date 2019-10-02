"{WindowsSDKToolsPath}UICC.exe" "{XmlFilename}" "{BmlFilename}" /res:"{RcFilename}" /header:"{HeaderFilename}"
"{WindowsSDKToolsPath}rc.exe" /v "{RcFilename}"
rem VS2010
rem cmd /c "("%VS100COMNTOOLS%..\..\VC\bin\vcvars32.bat") && ("%VS100COMNTOOLS%..\..\VC\bin\link.exe" /VERBOSE /NOENTRY /DLL /OUT:"{DllFilename}" "{ResFilename}")"
rem VS2012
rem cmd /c "("%VS110COMNTOOLS%..\..\VC\bin\vcvars32.bat") && ("%VS110COMNTOOLS%..\..\VC\bin\link.exe" /VERBOSE /NOENTRY /DLL /OUT:"{DllFilename}" "{ResFilename}")"
rem VS2013
rem cmd /c "("%VS120COMNTOOLS%..\..\VC\bin\vcvars32.bat") && ("%VS120COMNTOOLS%..\..\VC\bin\link.exe" /VERBOSE /NOENTRY /DLL /OUT:"{DllFilename}" "{ResFilename}")"
rem VS2015
rem cmd /c "("%VS140COMNTOOLS%..\..\VC\bin\vcvars32.bat") && ("%VS140COMNTOOLS%..\..\VC\bin\link.exe" /VERBOSE /NOENTRY /DLL /OUT:"{DllFilename}" "{ResFilename}")"

rem VS2017, Windows10 x64, rem previous line and delete rem next line
cmd /c "("C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\VC\Auxiliary\Build\vcvars32.bat") && ("C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\VC\Tools\MSVC\14.16.27023\bin\Hostx86\x86\link.exe" /VERBOSE /NOENTRY /DLL /MACHINE:X86 /OUT:"{DllFilename}" "{ResFilename}")"

rem VS2019, Windows10 x64, rem previous line and delete rem next line
rem cmd /c "("C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\VC\Auxiliary\Build\vcvars32.bat") && ("C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\VC\Tools\MSVC\14.22.27905\bin\Hostx86\x86\link.exe" /VERBOSE /NOENTRY /DLL /MACHINE:X86 /OUT:"{DllFilename}" "{ResFilename}")"
