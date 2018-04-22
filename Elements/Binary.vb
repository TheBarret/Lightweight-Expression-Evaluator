Namespace Elements
    Public Class Binary
        Inherits Expression
        Public Property Left As Expression
        Public Property Right As Expression
        Public Property [Operator] As Tokens
        Sub New(left As Expression, [operator] As Tokens, right As Expression)
            Me.Left = left
            Me.Right = right
            Me.Operator = [operator]
        End Sub
        Public Overrides Function ToString() As String
            Return String.Format("{0} {1} {2}", Me.Left, Me.Operator.ToSymbol, Me.Right)
        End Function
    End Class
End Namespace