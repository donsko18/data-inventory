Imports System.Data.SqlClient

Public Class FormCustomer
    Dim con As New SqlConnection("Data Source=mssql-112619-0.cloudclusters.net,19221;Initial Catalog=inventoryvb;User ID=donnie;Password=Donnievp890")
    Public Sub earlycond()
        TextBox1.Enabled = False
        TextBox2.Enabled = False
        TextBox3.Enabled = False
        TextBox4.Enabled = False
        TextBox5.Enabled = False
    End Sub
    Public Sub maincond()
        TextBox2.Enabled = True
        TextBox3.Enabled = True
        TextBox4.Enabled = True
        TextBox5.Enabled = True
    End Sub
    Public Sub isi()
        con.Open()
        Dim sql = "select * from CustTbl"
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
        cmd1 = New SqlCommand("select * from CustTbl where custid in (select max(custid) from CustTbl)", con)
        Dim autocode As String
        Dim hitung As Long
        rd = cmd1.ExecuteReader
        rd.Read()
        If Not rd.HasRows Then
            autocode = "CTR" + "001"
        Else
            hitung = Microsoft.VisualBasic.Right(rd.GetString(0), 3) + 1
            autocode = "CTR" + Microsoft.VisualBasic.Right("000" & hitung, 3)
        End If
        TextBox1.Text = autocode
        con.Close()
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Button1.Text = "Input" Then
            Button1.Text = "Save"
            Button3.Enabled = False
            Button2.Enabled = False
            Button4.Enabled = True
            maincond()
        Else

            If TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Or TextBox5.Text = "" Then
                MsgBox("Please insert the empty fields")
            Else
                Try
                    con.Open()
                    Dim query As String
                    Dim cmd As SqlCommand
                    query = "insert into CustTbl values('" & TextBox1.Text & "', '" & TextBox2.Text & "', '" & TextBox3.Text & "', '" & TextBox4.Text & "','" & TextBox5.Text & "')"

                    cmd = New SqlCommand(query, con)
                    cmd.ExecuteNonQuery()
                    MsgBox("Customer Added Successfully")
                    con.Close()
                    isi()
                    TextBox1.Text = ""
                    TextBox2.Text = ""
                    TextBox3.Text = ""
                    TextBox4.Text = ""
                    TextBox5.Text = ""
                Catch ex As Exception
                    MsgBox(ex.Message)

                End Try
                autoid()
                earlycond()
                Button1.Text = "Input"
                Button3.Enabled = True
                Button2.Enabled = True
                Button4.Enabled = False
            End If

        End If
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If Button3.Text = "Delete" Then
            Button3.Text = "Accept"
            Button4.Enabled = True
            Button1.Enabled = False
            Button2.Enabled = False
            DGV1.Enabled = True
        Else
            If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Then
                MsgBox("Click The Right Customers table To Delete It")

            Else
                con.Open()
                Dim query As String
                query = "delete from CustTbl where custid= '" & TextBox1.Text & "'"
                Dim cmd As SqlCommand
                cmd = New SqlCommand(query, con)
                cmd.ExecuteNonQuery()
                MsgBox("Item Deleted Successfully")
                con.Close()
                isi()
                TextBox2.Text = ""
                TextBox3.Text = ""
                TextBox4.Text = ""
                TextBox5.Text = ""
                Button2.Enabled = False
            End If
            Button3.Text = "Delete"
            Button4.Enabled = False
            Button1.Enabled = True
            DGV1.Enabled = False
        End If
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If Button2.Text = "Edit" Then
            Button2.Text = "Save"
            Button4.Enabled = True
            Button1.Enabled = False
            Button3.Enabled = False
            DGV1.Enabled = True
            maincond()
        Else
            If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox4.Text = "" Or TextBox3.Text = "" Or TextBox5.Text = "" Then
                MsgBox("Please Click the Customers in the table to Edit it")
            Else
                Dim sql = "update CustTbl set custname='" & TextBox2.Text & "',address='" & TextBox3.Text & "',custmail='" & TextBox4.Text & "',custphon='" & TextBox5.Text & "' where custid ='" & TextBox1.Text & "'"
                con.Open()
                Dim cmd As New SqlCommand(sql, con)
                cmd.ExecuteNonQuery()
                MsgBox("This Data Has Edited Successfully")
                con.Close()
                isi()
                TextBox1.Text = ""
                TextBox2.Text = ""
                TextBox3.Text = ""
                TextBox4.Text = ""
                TextBox5.Text = ""
            End If
            autoid()
            Button2.Text = "Edit"
            Button4.Enabled = False
            Button1.Enabled = True
            Button3.Enabled = True
            DGV1.Enabled = False
            earlycond()
        End If
    End Sub
    Private Sub DGV1_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DGV1.CellMouseClick
        TextBox1.Text = DGV1.Rows(e.RowIndex).Cells(0).Value
        TextBox2.Text = DGV1.Rows(e.RowIndex).Cells(1).Value
        TextBox3.Text = DGV1.Rows(e.RowIndex).Cells(2).Value
        TextBox4.Text = DGV1.Rows(e.RowIndex).Cells(3).Value
        TextBox5.Text = DGV1.Rows(e.RowIndex).Cells(4).Value
    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        autoid()
        Button1.Text = "Input"
        Button2.Text = "Edit"
        Button3.Text = "Delete"
        Button1.Enabled = True
        Button2.Enabled = True
        Button3.Enabled = True
        Button4.Enabled = False
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
    End Sub
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Me.Close()
    End Sub

    Private Sub FormCustomer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        autoid()
        isi()
        Button4.Enabled = False
        DGV1.Enabled = False
        earlycond()

    End Sub

End Class