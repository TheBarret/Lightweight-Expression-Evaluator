Public Class Tokenizer
    Public Function Parse(input As String) As List(Of Token)
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
                Case "0"c To "9"c
                    stream.Add(Me.ConsumeDigits(input, i))
                Case " "c
                    Continue For
                Case Else
                    Throw New Exception(String.Format("unexpected character '{0}' at index {1}", input(i), i))
            End Select
        Next
        Return stream
    End Function
    Private Function ConsumeDigits(input As String, ByRef index As Integer) As Token
        Dim position As Integer = index, value As String = String.Empty
        For i As Integer = index To input.Length - 1
            If (input(i).IsNumber Or input(i) = ".") Then
                value += input(i)
                index = i
                Continue For
            End If
            Exit For
        Next
        Return New Token(Tokens.NUMBER, value, position)
    End Function
End Class