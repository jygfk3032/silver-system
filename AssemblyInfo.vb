Imports System.Windows

Imports System.Runtime.InteropServices
<Assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly)>

Class MainWindow
    Inherits Window

    <DllImport("DwmApi.dll")>
    Public Shared Function DwmEnableBlurBehindWindow(hWnd As IntPtr, ByRef pBlurBehind As DWM_BLURBEHIND) As Integer
    End Function

    Public Structure DWM_BLURBEHIND
        Public dwFlags As UInteger
        Public fEnable As Boolean
        Public hRgnBlur As IntPtr
        Public fTransitionOnMaximized As Boolean
    End Structure

    Private Const DWM_BB_ENABLE As UInteger = &H1



End Class

'The ThemeInfo attribute describes where any theme specific and generic resource dictionaries can be found.
'1st parameter: where theme specific resource dictionaries are located
'(used if a resource is not found in the page,
' or application resource dictionaries)

'2nd parameter: where the generic resource dictionary is located
'(used if a resource is not found in the page,
'app, and any theme specific resource dictionaries)
