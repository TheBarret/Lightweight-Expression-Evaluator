Imports Expressions

Module Program

    Sub Main()
        Try
            Console.WriteLine("Result: {0}", New Expression("12.5 + -(2/5.1) / 5").Evaluate)
        Catch ex As Exception
            Console.WriteLine("Error: {0}", ex.Message)
        End Try
        Console.Read()
    End Sub

End Module
