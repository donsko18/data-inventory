Imports System.Data.SqlClient
Public Class ReportsRawInOut
    Dim con As New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\donni\Documents\inventoryvb.mdf;Integrated Security=True;Connect Timeout=30")
    Public cmd As New SqlCommand
    Public rd As SqlDataReader
    Public da As New SqlDataAdapter
    Public ds As New DataSet

    Public Sub cetakallout()
        Dim cr As New Reportrawout1
        cmd.Connection = con
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "select rmwid,rmwdate,matid,matname,qty,unit,place,username from RmwTbl"
        da.SelectCommand = cmd
        ds.Tables.Clear()
        da.Fill(ds, "DataSet2")
        cr.SetDataSource(ds.Tables("DataSet2"))
        Formreportrawinout.CrystalReportViewer1.ReportSource = cr
        Formreportrawinout.Show()
        Formreportrawinout.WindowState = FormWindowState.Maximized
    End Sub
    Public Sub cetakallin()
        Dim cr As New Reportrawin1
        cmd.Connection = con
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "select rmiid,rmidate,matid,matname,qty,unit,username from RmiTbl"
        da.SelectCommand = cmd
        ds.Tables.Clear()
        da.Fill(ds, "DataSet1")
        cr.SetDataSource(ds.Tables("DataSet1"))
        Formreportrawinout.CrystalReportViewer1.ReportSource = cr
        Formreportrawinout.Show()
        Formreportrawinout.WindowState = FormWindowState.Maximized
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If RadioButton1.Checked = True Then
            cetakallin()
        ElseIf RadioButton2.Checked = True Then
            cetakallout()

        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged

    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub Label8_Click(sender As Object, e As EventArgs) Handles Label8.Click

    End Sub
End Class