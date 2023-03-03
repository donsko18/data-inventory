Imports System.Data.SqlClient
Public Class ReportFGPerPeriods
    Dim con As New SqlConnection("Data Source=mssql-113670-0.cloudclusters.net,17729;Initial Catalog=inventoryvb;User ID=donnie;Password=Donnievp890")
    Public cmd As New SqlCommand
    Public rd As SqlDataReader
    Public da As New SqlDataAdapter
    Public ds As New DataSet
    Public Sub tglin()
        ComboBox1.Items.Clear()
        ComboBox2.Items.Clear()
        con.Open()
        Dim sql As String
        sql = "select distinct wfgidate from WfgiTbl"
        cmd = New SqlCommand(sql, con)
        rd = cmd.ExecuteReader
        While rd.Read
            ComboBox1.Items.Add(rd!wfgidate)
            ComboBox2.Items.Add(rd!wfgidate)
        End While
        con.Close()
    End Sub
    Public Sub tglout()
        ComboBox1.Items.Clear()
        ComboBox2.Items.Clear()
        con.Open()
        Dim sql As String
        sql = "select distinct wfgdate from WfgTbl"
        cmd = New SqlCommand(sql, con)
        rd = cmd.ExecuteReader
        While rd.Read
            ComboBox1.Items.Add(rd!wfgdate)
            ComboBox2.Items.Add(rd!wfgdate)
        End While
        con.Close()
    End Sub
    Public Sub cetakoutpertgl()
        Dim cr As New ReportFgout1
        Dim tanggal1 As CrystalDecisions.CrystalReports.Engine.TextObject = cr.ReportDefinition.Sections(1).ReportObjects("text9")
        tanggal1.Text = "Tanggal :" & ComboBox1.Text & " s/d " & ComboBox2.Text
        cmd.Connection = con
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "select wfgid,itemid,itemname,wfgqty,custname,name,wfgdate from WfgTbl where wfgdate between '" & ComboBox1.Text & "' and '" & ComboBox2.Text & "'"
        da.SelectCommand = cmd
        ds.Tables.Clear()
        da.Fill(ds, "DataSet4")
        cr.SetDataSource(ds.Tables("DataSet4"))
        FormReportFginout.CrystalReportViewer1.ReportSource = cr
        FormReportFginout.Show()
        FormReportFginout.WindowState = FormWindowState.Maximized
    End Sub
    Public Sub cetakinpertgl()
        Dim cr As New ReportFGin1
        Dim tanggal1 As CrystalDecisions.CrystalReports.Engine.TextObject = cr.ReportDefinition.Sections(1).ReportObjects("text9")
        tanggal1.Text = "Tanggal :" & ComboBox1.Text & " s/d " & ComboBox2.Text
        cmd.Connection = con
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "select wfgiid,itemid,itemname,wfgiqty,name,wfgidate from WfgiTbl where wfgidate between '" & ComboBox1.Text & "' and '" & ComboBox2.Text & "'"
        da.SelectCommand = cmd
        ds.Tables.Clear()
        da.Fill(ds, "DataSet3")
        cr.SetDataSource(ds.Tables("DataSet3"))
        FormReportFginout.CrystalReportViewer1.ReportSource = cr
        FormReportFginout.Show()
        FormReportFginout.WindowState = FormWindowState.Maximized
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If RadioButton1.Checked = True Then
            cetakinpertgl()
        ElseIf RadioButton2.Checked = True Then
            cetakoutpertgl()

        End If
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        tglin()
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        tglout()

    End Sub
End Class