Imports System.Data.SqlClient
Public Class assign_Form

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

    Public Sub insertEmployToTask(task_id As String, employ_id As Integer)
        Dim query As String
        Dim employTaskId As String
        employTaskId = task_id & "-" & employ_id.ToString()
        query = "INSERT Into employ_to_task values('" & employTaskId & "','" & task_id & "','" & employ_id & "')"
        cmd = New SqlCommand(query, conn)
        conn.Open()
        Try

            cmd.ExecuteNonQuery()
            MsgBox("Assigned succussfully")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        conn.Close()
    End Sub

    Public Sub insertEmployToProj(project_id As Integer, empl_id As Integer)
        Dim query As String

        query = "INSERT Into employ_to_proj values('" & project_id & "','" & empl_id & "')"
        cmd = New SqlCommand(query, conn)
        conn.Open()
        Try
            cmd.ExecuteNonQuery()
            MsgBox("Assigned succussfully")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        conn.Close()
    End Sub

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


    Public Sub getProjectId()
        Dim qy As String

        qy = "select project_id,project_name from project"
        conn.Open()
        cmd = New SqlCommand(qy, conn)
        reader = cmd.ExecuteReader()
        Do While reader.Read()
            ComboBox3.Items.Add($"{reader.GetInt32(0).ToString()}-{reader.GetString(1)}")
        Loop
        conn.Close()
    End Sub



    Public Sub getEmployeeId()
        Dim qy As String

        qy = "select empl_id,empl_name,empl_surname from employee"
        conn.Open()
        cmd = New SqlCommand(qy, conn)
        reader = cmd.ExecuteReader()
        Do While reader.Read()
            ComboBox1.Items.Add($"{reader.GetInt32(0).ToString()}-{reader.GetString(1)} {reader.GetString(2)}")
        Loop
        conn.Close()
    End Sub

    Private Sub assign_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        getEmployeeId()
        getTaskId()
        getProjectId()
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        ListBox1.Items.Add(ComboBox2.SelectedItem.ToString())
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ListBox1.Items.Clear()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        ListBox2.Items.Clear()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim emp_id As String()
        Dim taskInfo As String()
        Dim projInfo As String()
        emp_id = ComboBox1.SelectedItem.ToString.Split("-")

        If ListBox1.Items.Count > 0 Then
            For Each item In ListBox1.Items
                taskInfo = item.ToString().Split("-")
                insertEmployToTask(taskInfo(0), Integer.Parse(emp_id(0)))
            Next
        End If

        If ListBox2.Items.Count > 0 Then
            For Each item In ListBox2.Items

                projInfo = item.ToString().Split("-")
                insertEmployToProj(Integer.Parse(projInfo(0)), Integer.Parse(emp_id(0)))
            Next
        End If

    End Sub



    Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedIndexChanged
        ListBox2.Items.Add(ComboBox3.SelectedItem.ToString())
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