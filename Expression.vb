Public Class Expression
    Private Property Expression As String
    Sub New(expression As String)
        Me.Expression = expression
    End Sub
    Public Function Evaluate(Optional round As Boolean = False) As Double
        Return Me.Evaluate(New Tree(Me.Tokenize(Me.Expression)).Build).ToNumber(round)
    End Function
    Public Function Tokenize(input As String) As List(Of Token)
        Dim stream As New List(Of Token)
        For i As Integer = 0 To input.Length - 1
            Select Case input(i)
                Case "+"c
                    stream.Add(New Token(Tokens.PLUS, input(i), i))
                Case "-"c
                    stream.Add(New Token(Tokens.MINUS, input(i), i))
                Case "*"c
                    stream.Add(New Token(Tokens.MULT, input(i), i))
                Case "/"c
                    stream.Add(New Token(Tokens.DIV, input(i), i))
                Case "%"c
                    stream.Add(New Token(Tokens.MODULO, input(i), i))
                Case "("c
                    stream.Add(New Token(Tokens.PARENTHESIS_OPEN, input(i), i))
                Case ")"c
                    stream.Add(New Token(Tokens.PARENTHESIS_CLOSE, input(i), i))
                Case "."c, "0"c To "9"c
                    Dim index As Integer = i, value As String = String.Empty
                    For j As Integer = index To input.Length - 1
                        If (input(j).IsNumber Or input(j) = ".") Then
                            value += input(j)
                            i = j
                            Continue For
                        End If
                        Exit For
                    Next
                    stream.Add(New Token(Tokens.NUMBER, value, index))
                Case " "c
                    Continue For
                Case Else
                    Throw New Exception(String.Format("unexpected character '{0}' at index {1}", input(i), i))
            End Select
        Next
        Return stream
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
        Else
            Throw New Exception(String.Format("unexpected value '{0}'", right.Value))
        End If
        Throw New Exception(String.Format("undefined operator type '{0}'", expression.Operator))
    End Function
    Private Function Evaluate(expression As Elements.Number) As TValue
        Return New TValue(expression.Value)
    End Function
End Class