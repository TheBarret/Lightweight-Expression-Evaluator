Imports System.Globalization

Public Module Settings
    Public Property Float As NumberStyles
    Public Property Culture As CultureInfo
    Sub New()
        Settings.Float = NumberStyles.Float
        Settings.Culture = New CultureInfo("US-en")
    End Sub
End Module
