Public Class Token
    Public Property Type As Tokens
    Public Property Value As String
    Public Property Index As Integer
    Sub New(type As Tokens, index As Integer)
        Me.Type = type
        Me.Index = index
        Me.Value = String.Empty
    End Sub
    Sub New(type As Tokens, value As String, index As Integer)
        Me.Type = type
        Me.Value = value
        Me.Index = index
    End Sub
    Public Overrides Function ToString() As String
        Return String.Format("{0}: {1}", Me.Index, Me.Value)
    End Function
End Class