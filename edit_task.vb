Imports System.Data.SqlClient
Public Class edit_task

    'connection string mysql databas and variable

    Dim conn As New SqlConnection("Data Source=localhost;Initial Catalog=PM;Integrated Security=True;Pooling=False")
    Dim cmd As SqlCommand
    Dim reader As SqlDataReader
    Dim ds As DataSet
    Private taskid As String

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

    Public Shared Function checkRole(role As String) As Boolean
        If role.ToUpper = "ADMINISTRATOR" Then
            Return True

        Else
            Return False

        End If
    End Function

    'method to get task id

    Public Sub getTaskId()
        Dim qy As String

        qy = "select task_id,task_name from task"
        conn.Open()
        cmd = New SqlCommand(qy, conn)
        reader = cmd.ExecuteReader()
        Do While reader.Read()
            ComboBox2.Items.Add($"{reader.GetString(0)}-{reader.GetString(1)}")
        Loop
        conn.Close()
    End Sub

    ' method to get to populate task field based on the id

    Public Sub populateTaskField(id)
        Dim qy As String
        qy = "select * from task where task_id ='" & id & "'"
        conn.Open()
        cmd = New SqlCommand(qy, conn)
        reader = cmd.ExecuteReader()
        If reader.Read() Then
            TextBox2.Text = reader.GetString(1)
            RichTextBox1.Text = reader.GetString(2)
            DateTimePicker1.Value = reader.GetDateTime(3)
            DateTimePicker2.Value = reader.GetDateTime(4)
            ComboBox1.Text = reader.GetString(5)
        End If
        conn.Close()

    End Sub



    Public Sub deleteTask()
        Dim qy As String
        qy = "Delete from task where task_id ='" & taskid & "'"
        conn.Open()
        Try
            cmd = New SqlCommand(qy, conn)
            cmd.ExecuteNonQuery()
            MsgBox("task deleted Successfully")
        Catch ex As Exception
            MsgBox(ex.Message)
            conn.Close()
        End Try

    End Sub

    Public Sub updateTask()
        Dim qy As String
        qy = "Update task set task_name='" & TextBox2.Text & "',task_description = '" & RichTextBox1.Text & "',task_startdate=CONVERT(DATETIME,'" & DateTimePicker1.Text & "', 103),task_enddate=CONVERT(DATETIME,'" & DateTimePicker2.Text & "', 103),task_statue='" & ComboBox1.Text & "' where task_id ='" & taskid & "'"
        conn.Open()
        Try
            cmd = New SqlCommand(qy, conn)
            cmd.ExecuteNonQuery()
            MsgBox("task updated Successfully")
        Catch ex As Exception
            MsgBox(ex.Message)

        End Try
        conn.Close()
    End Sub

    Private Sub edit_task_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Not checkRole(_position) Then
            TextBox2.Enabled = False
            DateTimePicker1.Enabled = False
            DateTimePicker2.Enabled = False
            ComboBox3.Enabled = False
            RichTextBox1.Enabled = False
            Button2.Enabled = False
        End If
        getTaskId()
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        Dim taskInfo As String()
        taskInfo = ComboBox2.SelectedItem.ToString().Split("-")
        taskid = taskInfo(0)
        populateTaskField(taskInfo(0))

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        updateTask()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        deleteTask()
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