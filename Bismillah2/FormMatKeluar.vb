Imports System.Data.SqlClient

Public Class FormMatkeluar
    Dim con As New SqlConnection("Data Source=mssql-112619-0.cloudclusters.net,19221;Initial Catalog=inventoryvb;User ID=donnie;Password=Donnievp890")
    Dim autocode As String
    Dim autocode1 As String
    Public Sub earlycond()
        TextBox1.Enabled = False
        titem.Enabled = False
        tqty.Enabled = False
        twname.Enabled = False
        tstock.Enabled = False
        ComboBox1.Enabled = False
        TextBox3.Enabled = False
        ComboBox3.Enabled = False
        titem.Text = ""
        tqty.Text = ""
        tstock.Text = ""
        TextBox6.Text = ""
        ComboBox1.Text = ""
        ComboBox3.Text = ""
        DGV1.Enabled = True
    End Sub
    Public Sub maincond()
        tqty.Enabled = True
        ComboBox1.Enabled = True
        ComboBox3.Enabled = True
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

    Public Sub isiplace()
        ComboBox3.Items.Clear()
        ComboBox3.Items.Add("Working Place A")
        ComboBox3.Items.Add("Working Place B")
        ComboBox3.Items.Add("Working Place C")
    End Sub
    Public Sub autoidout()
        con.Open()
        Dim rd As SqlDataReader
        Dim cmd1 As SqlCommand
        cmd1 = New SqlCommand("select * from RmwTbl where rmwid in (select max(rmwid) from RmwTbl)", con)
        Dim hitung As Long
        rd = cmd1.ExecuteReader
        rd.Read()
        If Not rd.HasRows Then
            autocode = "RMO" + "001"
        Else
            hitung = Microsoft.VisualBasic.Right(rd.GetString(0), 3) + 1
            autocode = "RMO" + Microsoft.VisualBasic.Right("000" & hitung, 3)
        End If
        con.Close()
    End Sub
    Private Sub FormBarangKeluar_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        earlycond()
        isi()
        isiitem()
        isiplace()
        autoidout()
        TextBox1.Text = autocode
        twname.Text = FormMenu.lblnamauser.Text
        TextBox3.Text = FormMenu.lblid.Text
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

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Button1.Text = "Input" Then
            Button1.Text = "Save"
            Call maincond()
        Else
            If ComboBox1.Text = "" Or tqty.Text = "" Then
                MsgBox("Your Field is not Complete Already, Please Insert!")
                Button1.Text = "Input"
            Else

                If ComboBox3.Text = "" Then
                            MsgBox("Your Field is not Complete Already, Please Insert!")
                        Else
                            If Val(tstock.Text) < Val(tqty.Text) Then
                                MsgBox("Your Item Quantity doesnt enough!")
                                Button1.Text = "Input"

                            Else
                                con.Open()
                                Dim kurang As Integer
                                kurang = Val(tstock.Text) - Val(tqty.Text)
                                tstock.Text = kurang
                                Dim rd As SqlDataReader
                                Dim cmd1 As SqlCommand
                                cmd1 = New SqlCommand("select * from RawTbl where matid = '" & ComboBox1.Text & "'", con)
                                rd = cmd1.ExecuteReader
                                rd.Read()
                                con.Close()
                                con.Open()
                                Dim minusstock = "update RawTbl set matqty =" & tstock.Text & " where matid='" & ComboBox1.Text & "'"
                                cmd1 = New SqlCommand(minusstock, con)
                                cmd1.ExecuteNonQuery()
                                con.Close()
                                con.Open()
                                Dim cmd As SqlCommand
                                Dim Insert As String = "insert into RmwTbl values('" & TextBox1.Text & "', '" & TextBox3.Text & "', '" & twname.Text & "','" & ComboBox1.Text & "','" & titem.Text & "','" & tqty.Text & "','" & TextBox6.Text & "','" & ComboBox3.Text & "','" & rmwdate.Value.ToShortDateString & "')"
                                cmd = New SqlCommand(Insert, con)
                                cmd.ExecuteNonQuery()
                                MsgBox("Data INPUT SUCCESFULLY")
                                con.Close()
                        Button1.Text = "Input"
                        TextBox1.Text = ""
                        autoidout()
                        TextBox1.Text = autocode
                    End If

                        End If
            End If
            earlycond()
            isi()
        End If
    End Sub
    Private Sub DGV1_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DGV1.CellMouseClick

        ComboBox1.Text = DGV1.Rows(e.RowIndex).Cells(0).Value
        titem.Text = DGV1.Rows(e.RowIndex).Cells(1).Value
        tstock.Text = DGV1.Rows(e.RowIndex).Cells(2).Value
        TextBox6.Text = DGV1.Rows(e.RowIndex).Cells(3).Value

    End Sub


    Private Sub Label16_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub TextBox6_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub tstock_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label15_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label13_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label14_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub rmwdate_ValueChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label12_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub twname_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label11_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub DGV1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)

    End Sub

    Private Sub Label9_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub tqty_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label8_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub titem_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs)

    End Sub
End Class