Imports System.Data.SqlClient
Public Class ReportFgInOut
    Dim con As New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\donni\Documents\inventoryvb.mdf;Integrated Security=True;Connect Timeout=30")
    Public cmd As New SqlCommand
    Public rd As SqlDataReader
    Public da As New SqlDataAdapter
    Public ds As New DataSet

    Public Sub cetakallout()
        Dim cr As New ReportFGout
        cmd.Connection = con
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "select wfgid,itemid,itemname,wfgqty,custname,name,wfgdate from WfgTbl"
        da.SelectCommand = cmd
        ds.Tables.Clear()
        da.Fill(ds, "Dataset4")
        cr.SetDataSource(ds.Tables("DataSet4"))
        FormReportFginout.CrystalReportViewer1.ReportSource = cr
        FormReportFginout.Show()
        FormReportFginout.WindowState = FormWindowState.Maximized
    End Sub
    Public Sub cetakallin()
        Dim cr As New ReportFGin
        cmd.Connection = con
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "select wfgiid,itemid,itemname,wfgiqty,name,wfgidate from WfgiTbl"
        da.SelectCommand = cmd
        ds.Tables.Clear()
        da.Fill(ds, "DataSet3")
        cr.SetDataSource(ds.Tables("DataSet3"))
        FormReportFginout.CrystalReportViewer1.ReportSource = cr
        FormReportFginout.Show()
        FormReportFginout.WindowState = FormWindowState.Maximized
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
End Class