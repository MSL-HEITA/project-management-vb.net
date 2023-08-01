Public Class main_Form1
    Private Sub main_Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim count As Integer = 0
        Dim log As New login





        While (count <= 100)
            'Threading.Thread.Sleep(600)
            Label4.Text = count
            ProgressBar1.Increment(1)

            count = count + 1
        End While
        Console.WriteLine(count)

        If count = 101 Then
            Console.WriteLine("form login")
            Me.Hide()
            log.Show()





        End If

    End Sub
End Class
