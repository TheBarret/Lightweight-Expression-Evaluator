Public Class Expression
    Private Property Expression As String
    Sub New(expression As String)
        Me.Expression = expression
    End Sub
    Public Function Evaluate(Optional round As Boolean = False) As Double
        Return Me.Evaluate(New Tree(New Tokenizer().Parse(Me.Expression)).Build).ToNumber(round)
    End Function
    Private Function Evaluate(expression As Elements.Expression) As TValue
        Select Case expression.GetType
            Case GetType(Elements.Unary)
                Return Me.Evaluate(CType(expression, Elements.Unary))
            Case GetType(Elements.Binary)
                Return Me.Evaluate(CType(expression, Elements.Binary))
            Case GetType(Elements.Number)
                Return Me.Evaluate(CType(expression, Elements.Number))
        End Select
        Throw New Exception(String.Format("undefined expression type '{0}'", expression.GetType))
    End Function
    Private Function Evaluate(expression As Elements.Unary) As TValue
        Dim right As TValue = Me.Evaluate(expression.Right)
        Select Case expression.Left
            Case Tokens.PLUS
                If (right.IsNumber) Then Return New TValue(right.ToNumber * 1)
                Throw New Exception(String.Format("unexpected value '{0}'", right.Value))
            Case Tokens.MINUS
                If (right.IsNumber) Then Return New TValue(right.ToNumber * -1)
                Throw New Exception(String.Format("unexpected value '{0}'", right.Value))
        End Select
        Throw New Exception(String.Format("undefined operator type '{0}'", expression.Left))
    End Function
    Private Function Evaluate(expression As Elements.Binary) As TValue
        Dim left As TValue = Me.Evaluate(expression.Left)
        Dim right As TValue = Me.Evaluate(expression.Right)
        If (left.IsNumber AndAlso right.IsNumber) Then
            Select Case expression.Operator
                Case Tokens.PLUS
                    Return New TValue(left.ToNumber + right.ToNumber)
                Case Tokens.MINUS
                    Return New TValue(left.ToNumber - right.ToNumber)
                Case Tokens.MULT
                    Return New TValue(left.ToNumber * right.ToNumber)
                Case Tokens.DIV
                    If (right.ToNumber >= 1) Then Return New TValue(left.ToNumber / right.ToNumber)
                    Throw New Exception(String.Format("division by zero '{0}'", expression))
                Case Tokens.MODULO
                    If (right.ToNumber >= 1) Then Return New TValue(left.ToNumber Mod right.ToNumber)
                    Throw New Exception(String.Format("division by zero '{0}'", expression))
            End Select
        End If
        Throw New Exception(String.Format("undefined operator type '{0}'", expression.Operator))
    End Function
    Private Function Evaluate(expression As Elements.Number) As TValue
        Return New TValue(expression.Value)
    End Function
End Class