Imports System.Data.SqlClient
Public Class FormMenu
    Dim con As New SqlConnection("Data Source=mssql-112619-0.cloudclusters.net,19221;Initial Catalog=inventoryvb;User ID=donnie;Password=Donnievp890")

    Public Sub totalrawinv()
        con.Open()
        Dim rd As SqlDataReader
        Dim cmd1 As SqlCommand
        cmd1 = New SqlCommand("select SUM(matqty) as total from RawTbl", con)
        rd = cmd1.ExecuteReader
        rd.Read()
        If rd.HasRows Then
            lblrwtot.Text = rd!total
        Else
            lblrwtot.Text = "0"
        End If
        con.Close()
    End Sub
    Public Sub totalrawin()
        con.Open()
        Dim rd As SqlDataReader
        Dim cmd1 As SqlCommand
        cmd1 = New SqlCommand("select SUM(qty) as total from RmiTbl", con)
        rd = cmd1.ExecuteReader
        rd.Read()
        If rd.HasRows Then
            lblrawmaterialsin.Text = rd!total
        Else
            lblrawmaterialsin.Text = "0"
        End If
        con.Close()
    End Sub
    Public Sub totalrawout()
        con.Open()
        Dim rd As SqlDataReader
        Dim cmd1 As SqlCommand
        cmd1 = New SqlCommand("select SUM(qty) as total from RmwTbl", con)
        rd = cmd1.ExecuteReader
        rd.Read()
        If rd.HasRows Then
            lblrawmaterialsout.Text = rd!total
        Else
            lblrawmaterialsout.Text = "0"
        End If
        con.Close()
    End Sub
    Public Sub totalfginv()
        con.Open()
        Dim rd As SqlDataReader
        Dim cmd1 As SqlCommand
        cmd1 = New SqlCommand("select SUM(itemqty) as total from FgTbl", con)
        rd = cmd1.ExecuteReader
        rd.Read()
        If rd.HasRows Then
            lblfgtot.Text = rd!total
        Else
            lblfgtot.Text = "0"
        End If
        con.Close()
    End Sub
    Public Sub totalfgin()
        con.Open()
        Dim rd As SqlDataReader
        Dim cmd1 As SqlCommand
        cmd1 = New SqlCommand("select SUM(wfgiqty) as total from WfgiTbl", con)
        rd = cmd1.ExecuteReader
        rd.Read()
        If rd.HasRows Then
            lblfgin.Text = rd!total
        Else
            lblfgin.Text = "0"
        End If
        con.Close()
    End Sub
    Public Sub totalfgout()
        con.Open()
        Dim rd As SqlDataReader
        Dim cmd1 As SqlCommand
        cmd1 = New SqlCommand("select SUM(wfgqty) as total from WfgTbl", con)
        rd = cmd1.ExecuteReader
        rd.Read()
        If rd.HasRows Then
            lblfgout.Text = rd!total
        Else
            lblfgout.Text = "0"
        End If
        con.Close()
    End Sub
    Public Sub bukaapk()
        paneldashboard.Visible = False
        paneldatamaster.Visible = False
        panelinven.Visible = False
        panelreport.Visible = False
        FormUser.Close()
        FormBarangKeluar.Close()
        FormCustomer.Close()
        FormFGoodsMasuk.Close()
        FormFMaterialsMasuk.Close()
        FormMatkeluar.Close()
        ReportFgInOut.Close()
        ReportFGPerPeriods.Close()
        ReportsRawInOut.Close()
        ReportsRawPerPeriods.Close()
        FormBarangMasuk.Close()
        FormMatMasuk.Close()

    End Sub
    Public Sub munculfoto()
        '' PictureBox1.ImageLocation = labelurl.Text
        ''PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
        bukaapk()
        paneldashboard.Visible = True
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If Me.WindowState = FormWindowState.Normal Then
            Me.WindowState = FormWindowState.Maximized
        Else
            Me.WindowState = FormWindowState.Normal
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Application.Exit()
    End Sub

    Private Sub FormMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        paneldashboard.Visible = True
        totalrawinv()
        totalfginv()
        totalrawin()
        totalrawout()
        totalfgin()
        totalfgout()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        bukaapk()
        totalrawinv()
        totalfginv()
        totalrawin()
        totalrawout()
        totalfgin()
        totalfgout()
        paneldashboard.Visible = True
        dashlabel.Visible = True
        userlbl.Visible = False
        masterlbl.Visible = False
        invenlbl.Visible = False
        reportlbl.Visible = False
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click

        bukaapk()
        FormUser.MdiParent = Me
        FormUser.Show()
        userlbl.Visible = True
        masterlbl.Visible = False
        invenlbl.Visible = False
        reportlbl.Visible = False
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        bukaapk()
        paneldatamaster.Visible = True
        dashlabel.Visible = False
        userlbl.Visible = False
        masterlbl.Visible = True
        invenlbl.Visible = False
        reportlbl.Visible = False
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        bukaapk()
        panelinven.Visible = True
        dashlabel.Visible = False
        userlbl.Visible = False
        masterlbl.Visible = False
        invenlbl.Visible = True
        reportlbl.Visible = False
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        bukaapk()
        panelreport.Visible = True
        dashlabel.Visible = False
        userlbl.Visible = False
        masterlbl.Visible = False
        invenlbl.Visible = False
        reportlbl.Visible = True
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        If MessageBox.Show("Apakah Anda yakin ingin Log Out ?", "Info", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
            bukaapk()
            lblnamauser.Text = ""
            lbllevel.Text = ""
            lblid.Text = ""
            FormLogin.Show()
            FormLogin.TextBox1.Text = ""
            FormLogin.TextBox2.Text = ""
            FormLogin.TextBox1.Focus()
            Me.Close()
        End If
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        bukaapk()
        FormFMaterialsMasuk.MdiParent = Me
        FormFMaterialsMasuk.Show()
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        bukaapk()
        FormFGoodsMasuk.MdiParent = Me
        FormFGoodsMasuk.Show()

    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        bukaapk()
        FormCustomer.MdiParent = Me
        FormCustomer.Show()
    End Sub

    Private Sub btnrwinout_Click(sender As Object, e As EventArgs) Handles btnrwout.Click
        bukaapk()
        FormMatkeluar.MdiParent = Me
        FormMatkeluar.Show()

    End Sub

    Private Sub btnfginout_Click(sender As Object, e As EventArgs) Handles btnfgout.Click
        bukaapk()
        FormBarangKeluar.MdiParent = Me
        FormBarangKeluar.Show()

    End Sub

    Private Sub Button20_Click(sender As Object, e As EventArgs) Handles Button20.Click
        bukaapk()
        ReportsRawPerPeriods.MdiParent = Me
        ReportsRawPerPeriods.Show()
    End Sub

    Private Sub Button21_Click(sender As Object, e As EventArgs) Handles Button21.Click
        bukaapk()
        ReportsRawInOut.MdiParent = Me
        ReportsRawInOut.Show()
    End Sub

    Private Sub Button25_Click(sender As Object, e As EventArgs) Handles Button25.Click
        bukaapk()
        ReportFgInOut.MdiParent = Me
        ReportFgInOut.Show()
    End Sub

    Private Sub Button24_Click(sender As Object, e As EventArgs) Handles Button24.Click
        bukaapk()
        ReportFGPerPeriods.MdiParent = Me
        ReportFGPerPeriods.Show()
    End Sub
    Private Sub btnrwin_Click(sender As Object, e As EventArgs) Handles btnrwin.Click
        bukaapk()
        FormMatMasuk.MdiParent = Me
        FormMatMasuk.Show()
    End Sub

    Private Sub btnfgin_Click(sender As Object, e As EventArgs) Handles btnfgin.Click
        bukaapk()
        FormBarangMasuk.MdiParent = Me
        FormBarangMasuk.Show()
    End Sub
End Class