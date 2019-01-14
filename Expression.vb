Imports System.Text

Public Class Expression
    Private Property Expression As String
    Sub New(expression As String)
        Me.Expression = expression
    End Sub
    Sub New(sequence As Byte())
        Me.Expression = Me.Deserialize(sequence)
    End Sub
    Public Function Parse() As Elements.Expression
        Return New Abstractor(Me.Tokenize(Me.Expression)).Build
    End Function
    Public Function Evaluate(Optional round As Boolean = False) As Double
        If (Me.Expression.Length > 0) Then
            Return Me.Evaluate(New Abstractor(Me.Tokenize(Me.Expression)).Build).ToNumber(round)
        End If
        Return 0
    End Function
    Public Function Serialize() As Byte()
        If (Me.Expression.Length > 0) Then
            Dim buffer As New List(Of Byte)
            For Each t As Token In Me.Tokenize(Me.Expression)
                buffer.Add(Convert.ToByte(t.Type))
                If (t.Type = Tokens.NUMBER) Then
                    buffer.AddRange(BitConverter.GetBytes(Double.Parse(t.Value, Settings.Float, Settings.Culture)))
                End If
            Next
            Return buffer.ToArray
        End If
        Throw New Exception("no expression")
    End Function
    Public Function Deserialize(bytes As Byte()) As String
        If (bytes.Length > 0) Then
            Dim buffer As New StringBuilder
            For i As Integer = 0 To bytes.Count - 1
                Select Case bytes(i)
                    Case CInt(Tokens.PLUS)
                        buffer.Append("+")
                    Case CInt(Tokens.MINUS)
                        buffer.Append("-")
                    Case CInt(Tokens.MULT)
                        buffer.Append("*")
                    Case CInt(Tokens.DIV)
                        buffer.Append("/")
                    Case CInt(Tokens.MODULO)
                        buffer.Append("%")
                    Case CInt(Tokens.PARENTHESIS_OPEN)
                        buffer.Append("(")
                    Case CInt(Tokens.PARENTHESIS_CLOSE)
                        buffer.Append(")")
                    Case CInt(Tokens.NUMBER)
                        Dim value() As Byte = New Byte(Settings.Offset) {}
                        For j As Integer = 1 To Settings.Offset
                            value(j - 1) = bytes(i + j)
                        Next
                        i += Settings.Offset
                        buffer.Append(BitConverter.ToDouble(value, 0).ToString.Replace(",", "."))
                End Select
            Next
            Return buffer.ToString
        End If
        Throw New Exception("no input")
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
                 Case "."c, "E"c, "e"c, "0"c To "9"c
                    Dim index As Integer = i, value As String = String.Empty
                    For j As Integer = index To input.Length - 1
                        If (input(j).IsNumber Or input(j).IsNumberRelated) Then
                            value += input(j)
                            i = j
                            Continue For
                        ElseIf (j > 0 AndAlso Char.ToLower(input(j - 1)) = "e" And (input(j) = "-" Or input(j) = "+")) Then
                            value += input(j)
                            i = j
                            Continue For
                        End If
                        Exit For
                    Next
                    If (value.Length > Settings.MaxLength) Then
                        Throw New Exception(String.Format("Value '{0}' exceeds limit of {1} characters", value, Settings.MaxLength))
                    End If
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
            Case GetType(Elements.Binary)
                Return Me.Evaluate(CType(expression, Elements.Binary))
            Case GetType(Elements.Number)
                Return New TValue(CType(expression, Elements.Number).Value)
        End Select
        Throw New Exception(String.Format("undefined expression type '{0}'", expression.GetType))
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
                    If (right.ToNumber <> 0) Then Return New TValue(left.ToNumber / right.ToNumber)
                    Throw New Exception(String.Format("division by zero '{0}'", expression))
                Case Tokens.MODULO
                    If (right.ToNumber <> 0) Then Return New TValue(left.ToNumber Mod right.ToNumber)
                    Throw New Exception(String.Format("division by zero '{0}'", expression))
            End Select
        Else
            Throw New Exception(String.Format("unexpected value '{0}'", right.Value))
        End If
        Throw New Exception(String.Format("undefined operator type '{0}'", expression.Operator))
    End Function
End Class
