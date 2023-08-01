Imports System.Data.SqlClient
Public Class login

    Dim conn As New SqlConnection("Data Source=localhost;Initial Catalog=PM;Integrated Security=True;Pooling=False")
    Dim cmd As SqlCommand
    Dim reader As SqlDataReader
    Dim da As SqlDataAdapter
    Dim ds As DataSet

    Public position As String
    Public emplFullName As String
    Public username As String
    Private usernamePassword As String
    Public empl_id As Integer



    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim query As String
        Dim a As Integer



        username = usernametab.Text()
        usernamePassword = passtab.Text()

        If username = "" Then
            MsgBox("Enter the username")
        ElseIf usernamePassword = "" Then
            MsgBox("Enter the password")
        Else

            conn.Open()
            query = "select * from employee where login_name='" & username & "'and password_hash= HASHBYTES('SHA2_512' ,N'" & usernamePassword & "')"

            cmd = New SqlCommand(query, conn)
            da = New SqlDataAdapter(cmd)
            ds = New DataSet()
            da.Fill(ds)
            a = ds.Tables(0).Rows.Count

            If a = 0 Then
                MsgBox("Wrong Username or password")

            Else

                reader = cmd.ExecuteReader()
                Do While reader.Read()
                    empl_id = empl_id & reader.GetInt32(0)
                    emplFullName = emplFullName & reader.GetString(2) & " " & reader.GetString(3)
                    position = position & reader.GetString(4)


                Loop

                Dim Home As New main_index(username, position, emplFullName, empl_id)
                Home.Show()
                Me.Close()




            End If

            conn.Close()

        End If


    End Sub


End Class