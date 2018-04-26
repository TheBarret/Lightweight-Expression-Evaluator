Imports Expressions

Module Program

    Sub Main()
        Try

            '// ----------------------------------------------------------------------
            '// Evaluate from string

            Dim expression As String = "(1E2+3.14)/5--.5"
            Dim tree As Elements.Expression = New Expression(expression).Parse

            Console.WriteLine(String.Empty)

            Console.WriteLine("[Input]                : {0} [{1}]", expression, tree.ToString)

            Console.WriteLine(String.Empty)

            Console.WriteLine("[Output]         Result: {0}", New Expression(expression).Evaluate)

            '// ----------------------------------------------------------------------
            '// Evaluate from byte array

            Dim bytes() As Byte = New Expression(expression).Serialize()

            Console.WriteLine(String.Empty)

            Console.WriteLine("[Input]                : {0} [{1}bytes]", String.Concat(bytes), bytes.Length)
            Console.WriteLine("[Serialized]     Result: {0}", New Expression(bytes).Evaluate)

        Catch ex As Exception
            Console.WriteLine("Error: {0}", ex.Message)
        End Try
        Console.Read()
    End Sub
End Module
