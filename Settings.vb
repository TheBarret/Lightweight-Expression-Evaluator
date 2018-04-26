Imports System.Globalization
Imports System.Runtime.InteropServices

Public Module Settings
    Public Property Offset As Integer
    Public Property Float As NumberStyles
    Public Property Culture As CultureInfo
    Sub New()
        Settings.Float = NumberStyles.Float
        Settings.Culture = New CultureInfo("US-en")
        Settings.Offset = Marshal.SizeOf(GetType(Double))
    End Sub
End Module
