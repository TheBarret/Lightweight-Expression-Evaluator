# Lightweight-Expression-Evaluator
Super lightweight expression evaluator 

Usage:
```
Dim expression As String = "(1E2+3.14)/5--.5"

Dim result as double = New Expression(expression).Evaluate
```

From byte array:
```
Dim expression As String = "(1E2+3.14)/5--.5"

Dim bytes() As Byte = New Expression(expression).Serialize()

Dim result as double = New Expression(bytes).Evaluate
```

Demo:
```
[Input]                : (1E2+3.14)/5--.5 [100 + 3,14 / 5 - -0,5]

[Output]         Result: 21,128

[Input]                : 65000000896405311332358118430964735000000206411500000022463 [42bytes]
[Serialized]     Result: 21,128
```
