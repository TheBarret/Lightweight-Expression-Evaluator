Namespace Elements
    Public Class Number
        Inherits Expression
        Public Property Value As Double
        Sub New(value As String)
            If (Not value.IsNumber) Then Throw New Exception(String.Format("unable to parse '{0}' to a number", value))
            Me.Value = Double.Parse(value, Settings.Float, Settings.Culture)
        End Sub
        Public Overrides Function ToString() As String
            Return String.Format("{0}", Me.Value)
        End Function
    End Class
End Namespace