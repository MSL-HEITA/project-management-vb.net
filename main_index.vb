Imports System.Data.SqlClient


Public Class main_index

    Dim conn As New SqlConnection("Data Source=localhost;Initial Catalog=PM;Integrated Security=True;Pooling=False")
    Dim builder As SqlCommandBuilder
    Dim da As SqlDataAdapter
    Dim ds As DataSet

    Private _username As String
    Private _position As String
    Private _emplFullName As String
    Public query As String
    Private _empl_id As Integer





    Public Sub New(username As String, position As String, emplFullName As String, empl_id As Integer)
        InitializeComponent()
        _username = username
        _position = position
        _emplFullName = emplFullName
        _empl_id = empl_id
    End Sub



    Public Sub populateTable(query)
        Dim rowCounter As Integer



        conn.Open()
        da = New SqlDataAdapter(query, conn)
        builder = New SqlCommandBuilder(da)
        ds = New DataSet
        da.Fill(ds)

        rowCounter = ds.Tables(0).Rows.Count
        If rowCounter = 0 Then

            DataGridView1.DataSource = "No Data Created Yet"
        Else
            DataGridView1.DataSource = ds.Tables(0)

        End If
        conn.Close()

    End Sub

    Public Shared Function checkRole(role As String) As Boolean
        If role.ToUpper = "ADMINISTRATOR" Then
            Return True

        Else
            Return False

        End If
    End Function


    Private Sub main_index_Load(sender As Object, e As EventArgs) Handles MyBase.Load



        Label3.Text = _emplFullName
        user_greet_2.Text = "(" & _username & ")"



        If checkRole(_position) Then
            query = "select * from project "
            populateTable(query)
        Else
            Button1.Enabled = False
            Button1.Visible = False
            Button2.Enabled = False
            Button2.Visible = False
            Button4.Enabled = False
            Button4.Visible = False
            Button3.Enabled = False
            Button3.Visible = False
            ComboBox1.Enabled = False
            ComboBox1.Visible = False


            query = $"select project.project_id,project.project_name,project.project_description,project.startdate,project.enddate,project.project_statue, employ_to_proj.empl_id  from project
 inner join employ_to_proj on project.project_id = employ_to_proj.project_id where exists (select project_id from employ_to_proj  where empl_id = {_empl_id});"
            populateTable(query)
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If ToolStripMenuItem1.Checked Then
            Dim project As New add_project(_username, _position, _emplFullName, _empl_id)
            project.Show()
            Me.Close()
        ElseIf TasksToolStripMenuItem.Checked Then
            Dim task As New add_task(_username, _position, _emplFullName, _empl_id)
            task.Show()

            Me.Close()
        ElseIf EmployeesToolStripMenuItem.Checked Then
            Dim employee As New add_employee(_username, _position, _emplFullName, _empl_id)
            employee.Show()
            Me.Close()
        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If ToolStripMenuItem1.Checked Then
            Dim editProject As New edit_project(_username, _position, _emplFullName, _empl_id)
            editProject.Show()
            Me.Hide()
        ElseIf TasksToolStripMenuItem.Checked Then
            Dim editTask As New edit_task(_username, _position, _emplFullName, _empl_id)
            editTask.Show()
            Me.Hide()
        ElseIf EmployeesToolStripMenuItem.Checked Then
            Dim editEmployee As New edit_employee(_username, _position, _emplFullName, _empl_id)
            editEmployee.Show()

            Me.Hide()
        End If

    End Sub

    Private Sub TasksToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TasksToolStripMenuItem.Click

        Dim p As Point
        p.X = 21
        p.Y = 121
        TasksToolStripMenuItem.Checked = True
        EmployeesToolStripMenuItem.Checked = False
        ToolStripMenuItem1.Checked = False


        If checkRole(_position) Then
            query = "select * from task"
            populateTable(query)
        Else

            Button2.Enabled = True
            Button2.Visible = True
            Button2.Location = p
            Button4.Enabled = False
            Button4.Visible = False

            query = "select task.task_id,task.task_name,task.task_description,task.task_startdate,task.task_enddate,task.task_statue,project.project_name
                    from task  inner join project  on task.project_id = project.project_id  
                    where exists (select task_id from employ_to_task  where employ_id ='" & _empl_id & "')"
            populateTable(query)
        End If

    End Sub

    Private Sub EmployeesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EmployeesToolStripMenuItem.Click
        EmployeesToolStripMenuItem.Checked = True
        ToolStripMenuItem1.Checked = False
        TasksToolStripMenuItem.Checked = False
        If checkRole(_position) Then
            query = "select empl_id,login_name,empl_name,empl_surname,position from employee "
            populateTable(query)
        Else
            Button2.Enabled = False
            Button2.Visible = False
            Button4.Enabled = False
            Button4.Visible = False
            query = "select empl_id,login_name,empl_name,empl_surname,position from employee where empl_id ='" & _empl_id & "' and login_name = '" & _username & "'"
            populateTable(query)
        End If
    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click

        ToolStripMenuItem1.Checked = True
        EmployeesToolStripMenuItem.Checked = True
        TasksToolStripMenuItem.Checked = False
        If checkRole(_position) Then
            query = "select * from project "
            populateTable(query)
        Else
            Button2.Enabled = False
            Button2.Visible = False
            Button4.Enabled = False
            Button4.Visible = False

            query = $"select project.project_id,project.project_name,project.project_description,project.startdate,project.enddate,project.project_statue, employ_to_proj.empl_id  from project
 inner join employ_to_proj on project.project_id = employ_to_proj.project_id where exists (select project_id from employ_to_proj  where empl_id = {_empl_id});"
            populateTable(query)
        End If
    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click
        Application.Exit()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim assignform As New assign_Form(_username, _position, _emplFullName, _empl_id)
        assignform.Show()
        Me.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click   'search button
        Dim qy As String
        If ComboBox1.SelectedItem.ToString() = "Projects with workers" Then
            qy = "Select project.project_id,project.project_name,project.project_description,project.startdate,project.enddate,project.project_statue, employee.login_name  from project
                inner Join employ_to_proj On project.project_id = employ_to_proj.project_id  inner join employee on employee.empl_id = employ_to_proj.empl_id"
            populateTable(qy)
        ElseIf ComboBox1.SelectedItem.ToString() = "Projects without workers" Then
            qy = "Select project.project_id,project.project_name,project.project_description,project.startdate,project.enddate,project.project_statue, employ_to_proj.project_id  from project
                left Join employ_to_proj On project.project_id = employ_to_proj.project_id  where employ_to_proj.project_id IS NULL"
            populateTable(qy)
        ElseIf ComboBox1.SelectedItem.ToString() = "Tasks with workers" Then
            qy = "Select task.task_id,task.task_name,task.task_description,task.task_startdate,task.task_enddate,task.task_statue,task.project_id , employee.login_name  from task
                inner Join employ_to_task On task.task_id = employ_to_task.task_id  inner join employee on employee.empl_id = employ_to_task.employ_id"
            populateTable(qy)
        ElseIf ComboBox1.SelectedItem.ToString() = "Tasks without workers" Then
            qy = "Select task.task_id,task.task_name,task.task_description,task.task_startdate,task.task_enddate,task.task_statue, employ_to_task.task_id  from task
                left Join employ_to_task On task.task_id = employ_to_task.task_id  where employ_to_task.task_id IS NULL"
            populateTable(qy)
        ElseIf ComboBox1.SelectedItem.ToString() = "Employees with Workload" Then
            qy = "Select employee.empl_id,employee.login_name,employee.empl_name,employee.empl_surname,employee.position,task.task_statue,task.task_name from employee
                inner Join employ_to_task On employee.empl_id = employ_to_task.employ_id  inner join task on task.task_id = employ_to_task.task_id"
            populateTable(qy)
        ElseIf ComboBox1.SelectedItem.ToString() = "Employees without workload " Then
            qy = "Select employee.empl_id,employee.login_name,employee.empl_name,employee.empl_surname,employee.position,employ_to_task.task_id from employee
                left Join employ_to_task On employee.empl_id = employ_to_task.employ_id  where employ_to_task.task_id IS NULL"
            populateTable(qy)
        End If

    End Sub
End Class