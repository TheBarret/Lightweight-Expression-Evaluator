Public Class Abstractor
    Private Property Index As Integer
    Private Property Token As Token
    Private Property Stream As List(Of Token)
    Sub New(stream As List(Of Token))
        If (stream.Count > 0) Then
            Me.Index = 0
            Me.Stream = stream
            Me.Token = stream(0)
        End If
    End Sub
    Public Function Build() As Elements.Expression
        Dim expressions As Elements.Expression
        Do
            expressions = Me.Parse
        Loop While Me.Token.Type <> Tokens.END
        Return expressions
    End Function
    Private Function Parse() As Elements.Expression
        Return Me.AdditionOrSubtraction
    End Function
    Private Function AdditionOrSubtraction() As Elements.Expression
        Dim e As Elements.Expression = Me.MultiplicationOrDivision()
        While (Me.Token.Type = Tokens.PLUS) OrElse
              (Me.Token.Type = Tokens.MINUS)
            Dim Op As Tokens = Me.Token.Type
            Me.NextToken()
            e = New Elements.Binary(e, Op, Me.MultiplicationOrDivision)
        End While
        Return e
    End Function
    Private Function MultiplicationOrDivision() As Elements.Expression
        Dim e As Elements.Expression = Me.Value()
        While (Me.Token.Type = Tokens.MULT) OrElse
              (Me.Token.Type = Tokens.DIV) OrElse
              (Me.Token.Type = Tokens.MODULO)
            Dim Op As Tokens = Me.Token.Type
            Me.NextToken()
            e = New Elements.Binary(e, Op, Me.Value)
        End While
        Return e
    End Function
    Private Function Value() As Elements.Expression
        Dim e As Elements.Expression = Nothing
        Select Case Me.Token.Type
            Case Tokens.PARENTHESIS_OPEN
                Me.Match(Tokens.PARENTHESIS_OPEN)
                e = Me.Parse
                Me.Match(Tokens.PARENTHESIS_CLOSE)
            Case Tokens.NUMBER
                e = New Elements.Number(Me.Token.Value)
                Me.NextToken()
            Case Tokens.PLUS
                Me.NextToken()
                e = New Elements.Number(Me.Value).Positive
            Case Tokens.MINUS
                Me.NextToken()
                e = New Elements.Number(Me.Value).Negative
        End Select
        Return e
    End Function
    Private Function Match(expect As Tokens) As Token
        If (Not Me.Token.Type = expect) Then
            Throw New Exception(String.Format("expecting '{0}' at index {1}", expect, Me.Token.Index))
        End If
        Me.NextToken()
        Return Me.Token
    End Function
    Private Function NextMatch(expect As Tokens) As Token
        Me.NextToken()
        If (Not Me.Token.Type = expect) Then
            Throw New Exception(String.Format("expecting '{0}' at index {1}", expect, Me.Token.Index))
        End If
        Return Me.Token
    End Function
    Private Function NextToken() As Token
        If (Me.Index >= Me.Stream.Count - 1) Then
            Me.Token = New Token(Tokens.END, Me.Stream.Count - 1)
        Else
            Me.Index += 1
            Me.Token = Me.Stream(Me.Index)
        End If
        Return Me.Token
    End Function
End Class
