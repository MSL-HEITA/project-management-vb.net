Imports System.Data.SqlClient
Public Class add_employee
    Dim conn As New SqlConnection("Data Source=localhost;Initial Catalog=PM;Integrated Security=True;Pooling=False")
    Dim cmd As SqlCommand
    Dim reader As SqlDataReader
    Dim ds As DataSet

    Private _username As String
    Private _position As String
    Private _emplFullName As String
    Private _empl_id As Integer

    Public Sub New(username As String, position As String, emplFullName As String, empl_id As Integer)
        InitializeComponent()
        _username = username
        _position = position
        _emplFullName = emplFullName
        _empl_id = empl_id
    End Sub

    Public Sub InsertNewRecord()
        Using (conn)
            cmd = New SqlCommand()
            cmd.Connection = conn
            cmd.CommandText = "pAddUser"
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.AddWithValue("plogin_name", TextBox5.Text)
            cmd.Parameters.AddWithValue("pempl_name", TextBox2.Text)
            cmd.Parameters.AddWithValue("pempl_surname", TextBox3.Text)
            cmd.Parameters.AddWithValue("pposition", TextBox4.Text)
            cmd.Parameters.AddWithValue("ppassword", TextBox6.Text)
            conn.Open()
            cmd.ExecuteNonQuery()

            conn.Close()
        End Using
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox6.Text <> TextBox7.Text Then
            Label11.Text = "Password does not match!!!"
        Else
            Try
                InsertNewRecord()
                MsgBox("Employee Successfully added")
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
    End Sub



    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        Dim Home As New main_index(_username, _position, _emplFullName, _empl_id)
        Home.Show()
        Me.Close()
    End Sub

    Private Sub TasksToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TasksToolStripMenuItem.Click
        Dim Home As New main_index(_username, _position, _emplFullName, _empl_id)
        Home.Show()
        Me.Close()
    End Sub

    Private Sub EmployeesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EmployeesToolStripMenuItem.Click
        Dim Home As New main_index(_username, _position, _emplFullName, _empl_id)
        Home.Show()
        Me.Close()
    End Sub

    Private Sub TextBox7_TextChanged(sender As Object, e As EventArgs) Handles TextBox7.TextChanged

        If TextBox7.Text = TextBox6.Text Then
            Label2.ForeColor = Color.MediumTurquoise
            Label2.Text = "Password Match"
        Else
            Label2.ForeColor = Color.Red
            Label2.Text = "Password Not Match"
        End If
    End Sub
End Class