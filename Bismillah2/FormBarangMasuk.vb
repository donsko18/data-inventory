Imports System.Data.SqlClient
Public Class FormBarangMasuk
    Dim con As New SqlConnection("Data Source=mssql-113670-0.cloudclusters.net,17729;Initial Catalog=inventoryvb;User ID=donnie;Password=Donnievp890")

    Dim autocode1 As String

    Public Sub earlycond()
        TextBox1.Enabled = False
        TextBox2.Enabled = False
        TextBox3.Enabled = False
        TextBox8.Enabled = False
        ComboBox1.Enabled = False
        TextBox4.Enabled = False
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox8.Text = ""
        ComboBox1.Text = ""
        DGV1.Enabled = True
    End Sub
    Public Sub maincond()
        TextBox8.Enabled = True
        ComboBox1.Enabled = True
    End Sub
    Public Sub isi()
        con.Open()
        Dim sql = "select * from FgTbl"
        Dim adapter As SqlDataAdapter
        adapter = New SqlDataAdapter(sql, con)
        Dim builder As SqlCommandBuilder
        builder = New SqlCommandBuilder(adapter)
        Dim ds As DataSet
        ds = New DataSet
        adapter.Fill(ds)
        DGV1.DataSource = ds.Tables(0)
        con.Close()
    End Sub
    Public Sub isiitem()
        con.Open()
        ComboBox1.Items.Clear()
        Dim rd As SqlDataReader
        Dim cmd As SqlCommand
        cmd = New SqlCommand("select * from ItemTbl", con)
        rd = cmd.ExecuteReader
        Do While rd.Read
            ComboBox1.Items.Add(rd.Item(0))
        Loop
        con.Close()
    End Sub
    Public Sub autoidin()
        con.Open()
        Dim rd As SqlDataReader
        Dim cmd1 As SqlCommand
        cmd1 = New SqlCommand("select * from WfgiTbl where wfgiid in (select max(wfgiid) from WfgiTbl)", con)
        Dim hitung As Long
        rd = cmd1.ExecuteReader
        rd.Read()
        If Not rd.HasRows Then
            autocode1 = "FGI" + "001"
        Else
            hitung = Microsoft.VisualBasic.Right(rd.GetString(0), 3) + 1
            autocode1 = "FGI" + Microsoft.VisualBasic.Right("000" & hitung, 3)
        End If
        con.Close()
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Button1.Text = "Input" Then
            Button1.Text = "Save"
            Call maincond()
        Else
            If ComboBox1.Text = "" Or TextBox8.Text = "" Then
                MsgBox("Your Field is not Complete Already, Please Insert!")
                Button1.Text = "Input"
            Else

                con.Open()
                Dim kurang1 As Integer
                kurang1 = Val(TextBox3.Text) + Val(TextBox8.Text)
                TextBox8.Text = kurang1
                Dim rd As SqlDataReader
                Dim cmd As SqlCommand
                cmd = New SqlCommand("select * from FgTbl where itemid = '" & ComboBox1.Text & "'", con)
                rd = cmd.ExecuteReader
                rd.Read()
                con.Close()
                con.Open()
                Dim minusstock2 = "update FgTbl set itemqty =" & TextBox8.Text & " where itemid='" & ComboBox1.Text & "'"
                cmd = New SqlCommand(minusstock2, con)
                cmd.ExecuteNonQuery()
                con.Close()
                con.Open()
                Dim cmd1 As SqlCommand
                Dim Insert As String = "insert into WfgiTbl values('" & TextBox1.Text & "', '" & ComboBox1.Text & "', '" & TextBox2.Text & "','" & TextBox8.Text & "','" & TextBox4.Text & "','" & dtp.Value.ToShortDateString & "')"
                cmd1 = New SqlCommand(Insert, con)
                cmd1.ExecuteNonQuery()
                MsgBox("Data INPUT SUCCESFULLY")
                con.Close()
                Button1.Text = "Input"
                TextBox1.Text = ""
                autoidin()
                TextBox1.Text = autocode1
            End If
            earlycond()
            isi()
        End If
    End Sub

    Private Sub FormBarangMasuk_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        earlycond()
        isi()
        isiitem()
        TextBox1.Text = ""
        autoidin()
        TextBox1.Text = autocode1
        TextBox4.Text = FormMenu.lblnamauser.Text
    End Sub
    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        con.Open()
        Dim rd As SqlDataReader
        Dim cmd As SqlCommand
        cmd = New SqlCommand("select * from FgTbl where itemid = '" & ComboBox1.Text & "'", con)
        rd = cmd.ExecuteReader
        rd.Read()
        If rd.HasRows Then
            TextBox2.Text = rd!itemname
            TextBox3.Text = rd!itemqty
        End If
        con.Close()
        TextBox8.Focus()
    End Sub
    Private Sub DGV1_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DGV1.CellMouseClick

        ComboBox1.Text = DGV1.Rows(e.RowIndex).Cells(0).Value
        TextBox2.Text = DGV1.Rows(e.RowIndex).Cells(1).Value
        TextBox3.Text = DGV1.Rows(e.RowIndex).Cells(2).Value
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Me.Close()
    End Sub
End Class