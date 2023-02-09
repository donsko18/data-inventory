Imports System.Data.SqlClient
Public Class FormMatMasuk
    Dim con As New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\donni\Documents\inventoryvb.mdf;Integrated Security=True;Connect Timeout=30")

    Dim autocode1 As String
    Public Sub earlycond()
        TextBox1.Enabled = False
        titem.Enabled = False
        tqty.Enabled = False
        twname.Enabled = False
        tstock.Enabled = False
        ComboBox1.Enabled = False
        TextBox2.Enabled = False
        titem.Text = ""
        tqty.Text = ""
        tstock.Text = ""
        TextBox6.Text = ""
        ComboBox1.Text = ""
        DGV1.Enabled = True
    End Sub
    Public Sub maincond()
        tqty.Enabled = True
        ComboBox1.Enabled = True
    End Sub
    Public Sub isi()
        con.Open()
        Dim sql = "select * from RawTbl"
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
        cmd = New SqlCommand("select * from MatTbl", con)
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
        cmd1 = New SqlCommand("select * from RmiTbl where rmiid in (select max(rmiid) from RmiTbl)", con)
        Dim hitung As Long
        rd = cmd1.ExecuteReader
        rd.Read()
        If Not rd.HasRows Then
            autocode1 = "RMI" + "001"
        Else
            hitung = Microsoft.VisualBasic.Right(rd.GetString(0), 3) + 1
            autocode1 = "RMI" + Microsoft.VisualBasic.Right("000" & hitung, 3)
        End If
        con.Close()
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Button1.Text = "Input" Then
            Button1.Text = "Save"
            Call maincond()
        Else
            If ComboBox1.Text = "" Or tqty.Text = "" Then
                MsgBox("Your Field is not Complete Already, Please Insert!")
                Button1.Text = "Input"
            Else

                con.Open()
                Dim kurang1 As Integer
                kurang1 = Val(tstock.Text) + Val(tqty.Text)
                tstock.Text = kurang1
                Dim rd As SqlDataReader
                Dim cmd As New SqlCommand
                cmd = New SqlCommand("select * from RawTbl where matid = '" & ComboBox1.Text & "'", con)
                rd = cmd.ExecuteReader
                rd.Read()
                con.Close()
                con.Open()
                Dim minusstock2 = "update RawTbl set matqty =" & tstock.Text & " where matid='" & ComboBox1.Text & "'"
                cmd = New SqlCommand(minusstock2, con)
                cmd.ExecuteNonQuery()
                con.Close()
                con.Open()
                Dim Insert2 As String = "insert into RmiTbl values('" & TextBox1.Text & "', '" & TextBox2.Text & "', '" & twname.Text & "','" & ComboBox1.Text & "','" & titem.Text & "','" & tqty.Text & "','" & TextBox6.Text & "','" & rmwdate.Value.ToShortDateString & "')"
                cmd = New SqlCommand(Insert2, con)
                cmd.ExecuteNonQuery()
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
        twname.Text = FormMenu.lblnamauser.Text
        TextBox2.Text = FormMenu.lblid.Text
    End Sub
    Private Sub DGV1_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DGV1.CellMouseClick

        ComboBox1.Text = DGV1.Rows(e.RowIndex).Cells(0).Value
        titem.Text = DGV1.Rows(e.RowIndex).Cells(1).Value
        tstock.Text = DGV1.Rows(e.RowIndex).Cells(2).Value
        TextBox6.Text = DGV1.Rows(e.RowIndex).Cells(3).Value

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        con.Open()
        Dim rd As SqlDataReader
        Dim cmd As SqlCommand
        cmd = New SqlCommand("select * from RawTbl where matid = '" & ComboBox1.Text & "'", con)
        rd = cmd.ExecuteReader
        rd.Read()
        If rd.HasRows Then
            titem.Text = rd!matname
            tstock.Text = rd!matqty
            TextBox6.Text = rd!matunit
        End If
        tqty.Focus()

        con.Close()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Me.Close()
    End Sub
End Class