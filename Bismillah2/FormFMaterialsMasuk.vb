Imports System.Data.SqlClient

Public Class FormFMaterialsMasuk
    Dim con As New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\donni\Documents\inventoryvb.mdf;Integrated Security=True;Connect Timeout=30")
    Public Sub isi()
        con.Open()
        Dim sql = "select * from MatTbl"
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
    Public Sub autoid()
        con.Open()
        Dim rd As SqlDataReader
        Dim cmd1 As SqlCommand
        cmd1 = New SqlCommand("select * from MatTbl where matid in (select max(matid) from MatTbl)", con)
        Dim autocode As String
        Dim hitung As Long
        rd = cmd1.ExecuteReader
        rd.Read()
        If Not rd.HasRows Then
            autocode = "RWM" + "001"
        Else
            hitung = Microsoft.VisualBasic.Right(rd.GetString(0), 3) + 1
            autocode = "RWM" + Microsoft.VisualBasic.Right("000" & hitung, 3)
        End If
        TextBox1.Text = autocode
        con.Close()
    End Sub
    Public Sub earlycond()
        TextBox2.Enabled = False
        TextBox4.Enabled = False
        DGV1.Enabled = False
        Button1.Text = "Input"
        Button2.Text = "Edit"
        ComboBox1.Text = ""
    End Sub
    Public Sub maincond()
        TextBox2.Enabled = True
        TextBox4.Enabled = True
    End Sub
    Public Sub isibox()
        ComboBox1.Items.Clear()
        ComboBox1.Items.Add("Kilos")
        ComboBox1.Items.Add("Liter")
        ComboBox1.Items.Add("Pcs")
        ComboBox1.Items.Add("Packs")
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Button1.Text = "Input" Then
            Button1.Text = "Save"
            Button2.Enabled = False
            Call maincond()
        Else
            If TextBox2.Text = "" Or TextBox4.Text = "" Then
                MsgBox("Please insert the empty fields")
                earlycond()
                isibox()
                Button2.Enabled = True
            Else
                con.Open()
                Dim query As String
                Dim cmd As SqlCommand
                query = "insert into MatTbl values('" & TextBox1.Text & "', '" & TextBox2.Text & "', '" & TextBox4.Text & "', '" & ComboBox1.Text & "')"
                cmd = New SqlCommand(query, con)
                cmd.ExecuteNonQuery()
                con.Close()
                con.Open()
                Dim Insert12 As String = "insert into RawTbl values('" & TextBox1.Text & "', '" & TextBox2.Text & "', " & Val(Label4.Text) & ",'" & ComboBox1.Text & "')"
                cmd = New SqlCommand(Insert12, con)
                cmd.ExecuteNonQuery()
                MsgBox("Product Added Successfully")
                TextBox1.Text = ""
                TextBox2.Text = ""
                TextBox4.Text = ""
                ComboBox1.Text = ""
                con.Close()
                autoid()
                earlycond()
                isibox()
                isi()
            End If
        End If
        Button2.Enabled = True
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If Button2.Text = "Edit" Then
            Button2.Text = "Save"
            Button1.Enabled = False
            DGV1.Enabled = True
            Call maincond()
        Else
            If TextBox1.Text = "" Then
                MsgBox("Please Choose That ID you want to Change on The Table")
                Button1.Enabled = True
                earlycond()
                isibox()
                DGV1.Enabled = False
            Else
                Dim sql = "update MatTbl set matname='" & TextBox2.Text & "',matdesc='" & TextBox4.Text & "',matunit='" & ComboBox1.Text & "' where matid ='" & TextBox1.Text & "'"
                con.Open()
                Dim cmd As New SqlCommand(sql, con)
                cmd.ExecuteNonQuery()
                con.Close()
                con.Open()
                Dim rd As SqlDataReader
                Dim cmd1 As SqlCommand
                cmd1 = New SqlCommand("select * from RawTbl where matid = '" & TextBox1.Text & "'", con)
                rd = cmd1.ExecuteReader
                rd.Read()
                con.Close()
                con.Open()
                Dim minusstock = "update RawTbl set matname ='" & TextBox2.Text & "',matunit='" & ComboBox1.Text & "' where matid='" & TextBox1.Text & "'"
                cmd1 = New SqlCommand(minusstock, con)
                cmd1.ExecuteNonQuery()
                MsgBox("Your Data was Changed Successfully")
                con.Close()
                isi()
                TextBox1.Text = ""
                TextBox2.Text = ""
                TextBox4.Text = ""
                ComboBox1.Text = ""
                earlycond()
                DGV1.Enabled = False
            End If

        End If
        Button1.Enabled = True
        autoid()
    End Sub
    Private Sub DGV1_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DGV1.CellMouseClick
        TextBox1.Text = DGV1.Rows(e.RowIndex).Cells(0).Value
        TextBox2.Text = DGV1.Rows(e.RowIndex).Cells(1).Value
        TextBox4.Text = DGV1.Rows(e.RowIndex).Cells(2).Value
        ComboBox1.Text = DGV1.Rows(e.RowIndex).Cells(3).Value
    End Sub
    Private Sub Button6_Click_1(sender As Object, e As EventArgs) Handles Button6.Click
        Me.Close()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        earlycond()
        autoid()
        isi()
        isibox()
    End Sub


End Class
