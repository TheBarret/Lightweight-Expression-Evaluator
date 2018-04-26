Imports System.Text
Imports System.IO
Imports System.IO.Compression

Public Module Extensions
    <Runtime.CompilerServices.Extension>
    Public Function ToSymbol(token As Tokens) As String
        Select Case token
            Case Tokens.PLUS : Return "+"
            Case Tokens.MINUS : Return "-"
            Case Tokens.MULT : Return "*"
            Case Tokens.DIV : Return "/"
            Case Tokens.MODULO : Return "%"
            Case Tokens.PARENTHESIS_OPEN : Return "("
            Case Tokens.PARENTHESIS_CLOSE : Return ")"
            Case Else : Return token.ToString
        End Select
    End Function
    <Runtime.CompilerServices.Extension>
    Public Function IsNumber(value As String) As Boolean
        Return Double.TryParse(value, Settings.Float, Settings.Culture, Nothing)
    End Function
    <Runtime.CompilerServices.Extension>
    Public Function IsNumber(value As Char) As Boolean
        Return Double.TryParse(value, Settings.Float, Settings.Culture, Nothing)
    End Function
    <Runtime.CompilerServices.Extension>
    Public Function IsNumberRelated(value As Char) As Boolean
        Return value = "." Or value = "E" Or value = "e"
    End Function
    <Runtime.CompilerServices.Extension>
    Public Function IsNumberRelated(value As String) As Boolean
        Return value = "." Or value = "E" Or value = "e"
    End Function
End Module
