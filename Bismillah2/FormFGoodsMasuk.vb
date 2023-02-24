Imports System.Data.SqlClient
Public Class FormFGoodsMasuk
    Dim con As New SqlConnection("Data Source=mssql-112619-0.cloudclusters.net,19221;Initial Catalog=inventoryvb;User ID=donnie;Password=Donnievp890")
    Public Sub isi()
        con.Open()
        Dim sql = "select * from ItemTbl"
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
        cmd1 = New SqlCommand("select * from ItemTbl where itemid in (select max(itemid) from ItemTbl)", con)
        Dim autocode As String
        Dim hitung As Long
        rd = cmd1.ExecuteReader
        rd.Read()
        If Not rd.HasRows Then
            autocode = "FGS" + "001"
        Else
            hitung = Microsoft.VisualBasic.Right(rd.GetString(0), 3) + 1
            autocode = "FGS" + Microsoft.VisualBasic.Right("000" & hitung, 3)
        End If
        TextBox1.Text = autocode
        con.Close()
    End Sub
    Public Sub earlycond()
        TextBox2.Enabled = False
        TextBox4.Enabled = False
        Button1.Text = "Input"
        Button2.Text = "Edit"
    End Sub
    Public Sub maincond()
        TextBox2.Enabled = True
        TextBox4.Enabled = True
        DGV1.Enabled = True
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
                Button2.Enabled = True
            Else
                con.Open()
                Dim query As String
                Dim cmd As SqlCommand
                query = "insert into ItemTbl values('" & TextBox1.Text & "', '" & TextBox2.Text & "', '" & TextBox4.Text & "')"
                cmd = New SqlCommand(query, con)
                cmd.ExecuteNonQuery()
                con.Close()
                isi()
                con.Open()
                Dim Insert12 As String = "insert into FgTbl values('" & TextBox1.Text & "', '" & TextBox2.Text & "', " & Val(Label4.Text) & ")"
                cmd = New SqlCommand(Insert12, con)
                cmd.ExecuteNonQuery()
                MsgBox("Product Added Successfully")
                TextBox1.Text = ""
                TextBox2.Text = ""
                TextBox4.Text = ""
                con.Close()
                autoid()
                earlycond()
                Button2.Enabled = True
            End If
        End If
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If Button2.Text = "Edit" Then
            Button2.Text = "Save"
            DGV1.Enabled = True
            Button1.Enabled = False
            Call maincond()
        Else
            If TextBox1.Text = "" Then
                MsgBox("Please Choose That ID you want to Change on The Table")
                Button1.Enabled = True
                earlycond()
            Else
                Dim sql = "update ItemTbl set itemname='" & TextBox2.Text & "',itemdesc='" & TextBox4.Text & "' where itemid ='" & TextBox1.Text & "'"
                con.Open()
                Dim cmd As New SqlCommand(sql, con)
                cmd.ExecuteNonQuery()
                con.Close()
                con.Open()
                Dim rd As SqlDataReader
                Dim cmd1 As SqlCommand
                cmd1 = New SqlCommand("select * from FgTbl where itemid = '" & TextBox1.Text & "'", con)
                rd = cmd1.ExecuteReader
                rd.Read()
                con.Close()
                con.Open()
                Dim minusstock = "update FgTbl set itemname ='" & TextBox2.Text & "' where itemid='" & TextBox1.Text & "'"
                cmd1 = New SqlCommand(minusstock, con)
                cmd1.ExecuteNonQuery()
                MsgBox("Your Data was Changed Successfully")
                con.Close()
                isi()
                TextBox1.Text = ""
                TextBox2.Text = ""
                TextBox4.Text = ""
                earlycond()
                Button1.Enabled = True
                DGV1.Enabled = False
            End If

        End If

        autoid()
    End Sub
    Private Sub DGV1_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DGV1.CellMouseClick
        TextBox1.Text = DGV1.Rows(e.RowIndex).Cells(0).Value
        TextBox2.Text = DGV1.Rows(e.RowIndex).Cells(1).Value
        TextBox4.Text = DGV1.Rows(e.RowIndex).Cells(2).Value
    End Sub
    Private Sub Button6_Click_1(sender As Object, e As EventArgs) Handles Button6.Click
        Me.Close()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DGV1.Enabled = False
        earlycond()
        autoid()
        isi()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs)

    End Sub
End Class
