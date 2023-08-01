Imports System.Data.SqlClient
Public Class edit_project
    Dim conn As New SqlConnection("Data Source=localhost;Initial Catalog=PM;Integrated Security=True;Pooling=False")
    Dim cmd As SqlCommand
    Dim reader As SqlDataReader
    Dim ds As DataSet
    Private projectid As String

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


    Public Sub getProjectId()
        Dim qy As String

        qy = "select project_id,project_name from project"
        conn.Open()
        cmd = New SqlCommand(qy, conn)
        reader = cmd.ExecuteReader()
        Do While reader.Read()
            ComboBox1.Items.Add($"{reader.GetInt32(0).ToString()}-{reader.GetString(1)}")
        Loop
        conn.Close()
    End Sub


    Public Sub populateProjectField(id)
        Dim qy As String
        qy = "select * from project where project_id ='" & id & "'"
        conn.Open()
        cmd = New SqlCommand(qy, conn)
        reader = cmd.ExecuteReader()
        If reader.Read() Then
            TextBox2.Text = reader.GetString(1)
            RichTextBox1.Text = reader.GetString(2)
            DateTimePicker1.Value = reader.GetDateTime(3)
            DateTimePicker2.Value = reader.GetDateTime(4)
            ComboBox2.Text = reader.GetString(5)
        End If
        conn.Close()

    End Sub

    Public Sub updateProject()
        Dim qy As String
        qy = "Update project set project_name='" & TextBox2.Text & "',project_description = '" & RichTextBox1.Text & "',startdate=CONVERT(DATETIME,'" & DateTimePicker1.Text & "', 103),enddate=CONVERT(DATETIME,'" & DateTimePicker2.Text & "', 103),project_statue='" & ComboBox1.Text & "' where project_id ='" & projectid & "'"
        conn.Open()
        Try
            cmd = New SqlCommand(qy, conn)
            cmd.ExecuteNonQuery()
            MsgBox("project updated Successfully")
        Catch ex As Exception
            MsgBox(ex.Message)
            conn.Close()
        End Try

    End Sub

    Public Sub deletePoject()
        Dim qy As String
        qy = "Delete from project where project_id ='" & projectid & "'"
        conn.Open()
        Try
            cmd = New SqlCommand(qy, conn)
            cmd.ExecuteNonQuery()
            MsgBox("project deleted Successfully")
        Catch ex As Exception
            MsgBox(ex.Message)

        End Try
        conn.Close()

    End Sub

    Private Sub edit_project_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        getProjectId()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Dim projectInfo As String()
        projectInfo = ComboBox1.SelectedItem.ToString().Split("-")
        projectid = projectInfo(0)
        populateProjectField(projectid)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        updateProject()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        deletePoject()
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