Imports System.Data.SqlClient
Public Class add_project
    Dim conn As New SqlConnection("Data Source=localhost;Initial Catalog=PM;Integrated Security=True;Pooling=False")
    Dim cmd As SqlCommand
    Dim da As SqlDataAdapter
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


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim query As String

        query = "insert into project values('" & TextBox2.Text & "' ,'" & RichTextBox1.Text & "',CONVERT(DATETIME,'" & DateTimePicker1.Text & "', 103),CONVERT(DATETIME,'" & DateTimePicker2.Text & "',	103),'" & ComboBox1.SelectedItem.ToString() & "')"
        Try
            conn.Open()
            cmd = New SqlCommand(query, conn)
            cmd.ExecuteNonQuery()
            MsgBox("Project Added Successfully")

        Catch ex As Exception
            MsgBox(ex.Message)
            conn.Close()
        End Try

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
End Class