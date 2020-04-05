"{WindowsSDKToolsPath}UICC.exe" "{XmlFilename}" "{BmlFilename}" /res:"{RcFilename}" /header:"{HeaderFilename}"
"{WindowsSDKToolsPath}rc.exe" /v "{RcFilename}"
cmd /c "{LinkerCommand}"
