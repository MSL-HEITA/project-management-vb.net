Imports System.Data.SqlClient
Public Class add_task

    'coneetiion string,mysql command

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


    ' method for proect ID

    Public Sub getProjectId()
        Dim qy As String
        qy = "select project_id,project_name from project"
        conn.Open()
        cmd = New SqlCommand(qy, conn)
        reader = cmd.ExecuteReader()
        Do While reader.Read()
            ComboBox3.Items.Add(reader.GetInt32(0).ToString & "-" & reader.GetString(1))
        Loop
        conn.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim proj_id As String()
        Dim qy As String
        Dim startdate As DateTime
        Dim enterstartdate As DateTime = DateTimePicker1.Text
        Dim enterenddate As DateTime = DateTimePicker2.Text
        Dim enddate As DateTime
        proj_id = ComboBox3.SelectedItem.ToString().Split("-")
        qy = $"select startdate,enddate from project where project_id = {Integer.Parse(proj_id(0))}"
        conn.Open()
        cmd = New SqlCommand(qy, conn)
        reader = cmd.ExecuteReader()
        Do While reader.Read()
            startdate = reader.GetDateTime(0)
            enddate = reader.GetDateTime(1)
        Loop
        conn.Close()

        If startdate < enterstartdate And enddate > enterenddate Then

            Dim query As String

            query = "insert into task values( '" & TextBox1.Text & "' ,'" & TextBox2.Text & "' ,'" & RichTextBox1.Text & "',CONVERT(DATETIME,'" & DateTimePicker1.Text & "', 103),CONVERT(DATETIME,'" & DateTimePicker2.Text & "',	103),'" & ComboBox1.SelectedItem.ToString() & "','" & proj_id(0) & "')"
            Try
                conn.Open()
                cmd = New SqlCommand(query, conn)
                cmd.ExecuteNonQuery()
                MsgBox("task Added Successfully")

            Catch ex As Exception
                MsgBox(ex.Message)

            End Try
            conn.Close()
        Else
            MsgBox("TASK DATE CONFLICTS WITH THE pROJECT DATE ")
        End If



    End Sub

    '

    Private Sub add_task_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        getProjectId()
    End Sub

    'code for back menu items (left top corder)

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