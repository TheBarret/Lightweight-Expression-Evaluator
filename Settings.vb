Imports System.Globalization
Imports System.Runtime.InteropServices

Public Module Settings
    Public Property MaxLength As Integer
    Public Property Offset As Integer
    Public Property Float As NumberStyles
    Public Property Culture As CultureInfo
    Sub New()
        Settings.MaxLength = 15
        Settings.Float = NumberStyles.Float
        Settings.Culture = New CultureInfo("en-US")
        Settings.Offset = Marshal.SizeOf(GetType(Double))
    End Sub
End Module

