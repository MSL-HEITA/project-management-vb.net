Imports System.Data.SqlClient
Public Class edit_employee

    Dim conn As New SqlConnection("Data Source=localhost;Initial Catalog=PM;Integrated Security=True;Pooling=False")
    Dim cmd As SqlCommand
    Dim reader As SqlDataReader
    Dim ds As DataSet
    Private employeeid As String
    Private passwordConfirmed As Boolean


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


    Public Sub populateEmployeeField(id)
        Dim qy As String
        qy = "select * from employee where empl_id ='" & id & "'"
        conn.Open()
        cmd = New SqlCommand(qy, conn)
        reader = cmd.ExecuteReader()
        If reader.Read() Then
            TextBox2.Text = reader.GetString(2)
            TextBox3.Text = reader.GetString(3)
            TextBox4.Text = reader.GetString(4)
            TextBox5.Text = reader.GetString(1)

        End If
        conn.Close()

    End Sub

    Public Sub updateEmployee()
        Dim qy As String
        If passwordConfirmed Then
            qy = "Update employee set login_name='" & TextBox5.Text & "',empl_name= '" & TextBox2.Text & "',empl_surname= '" & TextBox3.Text & "',position='" & TextBox4.Text & "',password_hash= HASHBYTES('SHA2_512', '" & TextBox6.Text & "') where empl_id ='" & employeeid & "'"
        Else
            qy = "Update employee set login_name='" & TextBox5.Text & "',empl_name= '" & TextBox2.Text & "',empl_surname= '" & TextBox3.Text & "',position='" & TextBox4.Text & "' where empl_id ='" & employeeid & "'"
        End If
        conn.Open()
        Try
            cmd = New SqlCommand(qy, conn)
            cmd.ExecuteNonQuery()
            MsgBox("employee updated Successfully")
        Catch ex As Exception
            MsgBox(ex.Message)
            conn.Close()
        End Try

    End Sub


    Public Sub deleteEmployee()
        Dim qy As String
        qy = "Delete from employee where employ_id ='" & employeeid & "'"
        conn.Open()
        Try
            cmd = New SqlCommand(qy, conn)
            cmd.ExecuteNonQuery()
            MsgBox("employee deleted Successfully")
        Catch ex As Exception
            MsgBox(ex.Message)

        End Try
        conn.Close()
    End Sub

    Public Shared Function checkRole(role As String) As Boolean
        If role.ToUpper = "ADMINISTRATOR" Then
            Return True

        Else
            Return False

        End If
    End Function

    Private Sub edit_employee_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Not checkRole(_position) Then
            Button2.Enabled = False
        End If
        getEmployeeId()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        updateEmployee()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Dim employeeInfo As String()
        employeeInfo = ComboBox1.SelectedItem.ToString().Split("-")
        employeeid = employeeInfo(0)
        populateEmployeeField(employeeInfo(0))
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        deleteEmployee()
    End Sub

    Private Sub TextBox6_TextChanged(sender As Object, e As EventArgs) Handles TextBox6.TextChanged
        If TextBox1.Text = TextBox6.Text Then
            Label11.ForeColor = Color.MediumTurquoise
            Label11.Text = "Password  match"
            passwordConfirmed = True
        Else
            Label11.ForeColor = Color.Red
            Label11.Text = "Password does not match"
            passwordConfirmed = False
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
End Class