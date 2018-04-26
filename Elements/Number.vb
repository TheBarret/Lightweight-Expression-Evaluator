Namespace Elements
    Public Class Number
        Inherits Expression
        Public Property Value As Double
        Sub New(value As String)
            If (Not value.IsNumber And Not value.IsNumberRelated) Then Throw New Exception(String.Format("invalid number format '{0}'", value))
            Me.Value = Double.Parse(value, Settings.Float, Settings.Culture)
        End Sub
        Sub New(value As Elements.Expression)
            If (Not TypeOf value Is Number) Then Throw New Exception(String.Format("invalid number format '{0}'", value))
            Me.Value = CType(value, Number).Value
        End Sub
        Public Function Negative() As Number
            Me.Value *= -1
            Return Me
        End Function
        Public Function Positive() As Number
            Me.Value *= 1
            Return Me
        End Function
        Public Overrides Function ToString() As String
            Return String.Format("{0}", Me.Value)
        End Function
    End Class
End Namespace