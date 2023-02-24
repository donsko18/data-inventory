Imports System.Data.SqlClient
Public Class ReportsRawPerPeriods
    Dim con As New SqlConnection("Data Source=mssql-112619-0.cloudclusters.net,19221;Initial Catalog=inventoryvb;User ID=donnie;Password=Donnievp890")
    Public cmd As New SqlCommand
    Public rd As SqlDataReader
    Public da As New SqlDataAdapter
    Public ds As New DataSet
    Public Sub tglin()
        ComboBox1.Items.Clear()
        ComboBox2.Items.Clear()
        con.Open()
        Dim sql As String
        sql = "select distinct rmidate from RmiTbl"
        cmd = New SqlCommand(sql, con)
        rd = cmd.ExecuteReader
        While rd.Read
            ComboBox1.Items.Add(rd!rmidate)
            ComboBox2.Items.Add(rd!rmidate)
        End While
        con.Close()
    End Sub
    Public Sub tglout()
        ComboBox1.Items.Clear()
        ComboBox2.Items.Clear()
        con.Open()
        Dim sql As String
        sql = "select distinct rmwdate from RmwTbl"
        cmd = New SqlCommand(sql, con)
        rd = cmd.ExecuteReader
        While rd.Read
            ComboBox1.Items.Add(rd!rmwdate)
            ComboBox2.Items.Add(rd!rmwdate)
        End While
        con.Close()
    End Sub

    Public Sub cetakoutpertgl()
        Dim cr As New ReportRawOut
        Dim tanggal1 As CrystalDecisions.CrystalReports.Engine.TextObject = cr.ReportDefinition.Sections(1).ReportObjects("text9")
        tanggal1.Text = "Tanggal :" & ComboBox1.Text & " s/d " & ComboBox2.Text
        cmd.Connection = con
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "select rmwid,rmwdate,matid,matname,qty,unit,username,place from RmwTbl where rmwdate between '" & ComboBox1.Text & "' and '" & ComboBox2.Text & "'"
        da.SelectCommand = cmd
        ds.Tables.Clear()
        da.Fill(ds, "DataSet2")
        cr.SetDataSource(ds.Tables("DataSet2"))
        Formreportrawinout.CrystalReportViewer1.ReportSource = cr
        Formreportrawinout.Show()
        Formreportrawinout.WindowState = FormWindowState.Maximized
    End Sub
    Public Sub cetakinpertgl()
        Dim cr As New ReportRawIn
        Dim tanggal1 As CrystalDecisions.CrystalReports.Engine.TextObject = cr.ReportDefinition.Sections(1).ReportObjects("text9")
        tanggal1.Text = "Tanggal :" & ComboBox1.Text & " s/d " & ComboBox2.Text
        cmd.Connection = con
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "select rmiid,rmidate,matid,matname,qty,unit,username from RmiTbl where rmidate between '" & ComboBox1.Text & "' and '" & ComboBox2.Text & "'"
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
            cetakinpertgl()
        ElseIf RadioButton2.Checked = True Then
            cetakoutpertgl()

        End If
        ComboBox1.Text = ""
        ComboBox2.Text = ""
    End Sub


    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        tglin()
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        tglout()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
End Class