Imports System.Globalization

Public Module Settings
    Public Property Numbers As Char()
    Public Property Operators As Char()
    Public Property Float As NumberStyles
    Public Property Culture As CultureInfo
    Sub New()
        Settings.Float = NumberStyles.Float
        Settings.Culture = New CultureInfo("US-en")
        Settings.Operators = New Char() {"+"c, "-"c, "*"c, "/"c, "%"c}
        Settings.Numbers = New Char() {"0"c, "1"c, "2"c, "3"c, "4"c, "5"c, "6"c, "7"c, "8"c, "9"c}
    End Sub
End Module
