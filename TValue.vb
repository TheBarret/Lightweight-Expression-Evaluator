Public Class TValue
    Public Property Value As Object
    Sub New(value As Object)
        Me.Value = value
    End Sub
    Public Function IsNumber() As Boolean
        Return TypeOf Me.Value Is Double Or TypeOf Me.Value Is Int64
    End Function
    Public Function ToNumber(Optional round As Boolean = False) As Double
        Return If(round, Math.Round(CType(Me.Value, Double)), CType(Me.Value, Double))
    End Function
    Public Overrides Function ToString() As String
        Return String.Format("{0}", Me.Value)
    End Function
End Class

