Namespace Elements
    Public Class Unary
        Inherits Expression
        Public Property Left As Tokens
        Public Property Right As Expression
        Sub New(left As Tokens, right As Expression)
            Me.Left = left
            Me.Right = right
        End Sub
        Public Overrides Function ToString() As String
            Return String.Format("{0}({1})", Me.Left.ToSymbol, Me.Right)
        End Function
    End Class
End Namespace