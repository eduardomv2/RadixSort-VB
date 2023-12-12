Imports System

Module Module1
    ' Función para obtener el dígito en una posición específica
    Function GetDigit(number As Integer, position As Integer) As Integer
        Return (number \ Math.Pow(10, position)) Mod 10
    End Function

    ' Función para encontrar el valor máximo en el array
    Function FindMax(array As Integer()) As Integer
        Dim max As Integer = array(0)
        For i As Integer = 1 To array.Length - 1
            If array(i) > max Then
                max = array(i)
            End If
        Next
        Return max
    End Function

    ' Función principal del algoritmo Radix Sort
    Sub RadixSort(array As Integer())
        ' Registra el tiempo de inicio
        Dim startTime As DateTime = DateTime.Now

        ' Encuentra el valor máximo en el array
        Dim max As Integer = FindMax(array)

        ' Itera sobre cada posición del dígito (de derecha a izquierda)
        For position As Integer = 0 To CInt(Math.Floor(Math.Log10(max)) + 1)
            ' Llama a CountingSort para ordenar en la posición actual
            CountingSort(array, position)

            ' Imprime el estado actual del array después de la iteración
            Console.WriteLine($"Iteración {position + 1}: ")
            PrintArray(array)
        Next

        ' Registra el tiempo de finalización
        Dim endTime As DateTime = DateTime.Now

        ' Calcula y muestra el tiempo total de ejecución
        Dim duration As TimeSpan = endTime - startTime
        Console.WriteLine($"Tiempo de ejecución: {duration.TotalMilliseconds} ms")
    End Sub

    ' Función de ordenación usando Counting Sort en el dígito específico
    Sub CountingSort(array As Integer(), position As Integer)
        Dim output(array.Length - 1) As Integer
        Dim count(9) As Integer

        ' Inicializa el array de conteo
        For i As Integer = 0 To 9
            count(i) = 0
        Next

        ' Cuenta la frecuencia de cada dígito en la posición actual
        For i As Integer = 0 To array.Length - 1
            count(GetDigit(array(i), position)) += 1
        Next

        ' Ajusta el array de conteo para tener las posiciones correctas
        For i As Integer = 1 To 9
            count(i) += count(i - 1)
        Next

        ' Construye el array de salida usando el array de conteo
        For i As Integer = array.Length - 1 To 0 Step -1
            output(count(GetDigit(array(i), position)) - 1) = array(i)
            count(GetDigit(array(i), position)) -= 1
        Next

        ' Copia el array de salida al array original
        For i As Integer = 0 To array.Length - 1
            array(i) = output(i)
        Next
    End Sub

    ' Función para imprimir el estado actual del array
    Sub PrintArray(array As Integer())
        For Each item As Integer In array
            Console.Write(item & " ")
        Next
        Console.WriteLine()
    End Sub

    ' Programa principal para probar Radix Sort
    Sub Main()
        Dim array As Integer() = {170, 45, 75, 90, 802, 24, 2, 66}
        Console.WriteLine("Array original:")
        PrintArray(array)

        ' Llama a RadixSort para ordenar el array
        RadixSort(array)

        Console.WriteLine(vbLf & "Array ordenado:")

        PrintArray(array)
        Console.ReadKey()
    End Sub
End Module
