Imports System.Data.SqlClient
Public Class FormBarangKeluar
    Dim con As New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\donni\Documents\inventoryvb.mdf;Integrated Security=True;Connect Timeout=30")
    Dim autocode As String

    Public Sub earlycond()
        TextBox1.Enabled = False
        TextBox2.Enabled = False
        TextBox3.Enabled = False
        TextBox4.Enabled = False
        TextBox5.Enabled = False
        TextBox6.Enabled = False
        TextBox7.Enabled = False
        TextBox6.Enabled = False
        TextBox8.Enabled = False
        ComboBox1.Enabled = False
        ComboBox2.Enabled = False
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
        TextBox6.Text = ""
        TextBox7.Text = ""
        TextBox8.Text = ""
        ComboBox1.Text = ""
        ComboBox2.Text = ""
        DGV1.Enabled = True
    End Sub
    Public Sub maincond()
        TextBox8.Enabled = True
        ComboBox1.Enabled = True
        ComboBox2.Enabled = True
    End Sub
    Public Sub autoidout()
        con.Open()
        Dim rd As SqlDataReader
        Dim cmd1 As SqlCommand
        cmd1 = New SqlCommand("select * from WfgTbl where wfgid in (select max(wfgid) from WfgTbl)", con)
        Dim hitung As Long
        rd = cmd1.ExecuteReader
        rd.Read()
        If Not rd.HasRows Then
            autocode = "FGO" + "001"
        Else
            hitung = Microsoft.VisualBasic.Right(rd.GetString(0), 3) + 1
            autocode = "FGO" + Microsoft.VisualBasic.Right("000" & hitung, 3)
        End If
        con.Close()
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
    Public Sub isicust()
        con.Open()
        ComboBox2.Items.Clear()
        Dim rd As SqlDataReader
        Dim cmd As SqlCommand
        cmd = New SqlCommand("select * from CustTbl", con)
        rd = cmd.ExecuteReader
        Do While rd.Read
            ComboBox2.Items.Add(rd.Item(0))
        Loop
        con.Close()
    End Sub
    Private Sub FormBarangKeluar_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        earlycond()
        isi()
        isiitem()
        isicust()

        TextBox1.Text = ""
        autoidout()
        TextBox1.Text = autocode
        TextBox9.Text = FormMenu.lblnamauser.Text
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

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        con.Open()
        Dim rd As SqlDataReader
        Dim cmd As SqlCommand
        cmd = New SqlCommand("select * from CustTbl where custid = '" & ComboBox2.Text & "'", con)
        rd = cmd.ExecuteReader
        rd.Read()
        If rd.HasRows Then
            TextBox4.Text = rd!custname
            TextBox5.Text = rd!address
            TextBox6.Text = rd!custmail
            TextBox7.Text = rd!custphon
        End If
        con.Close()
        Button1.Focus()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Button1.Text = "Input" Then
            Button1.Text = "Save"
            Call maincond()
        Else
            If ComboBox1.Text = "" Or TextBox8.Text = "" Or ComboBox2.Text = "" Then
                MsgBox("Your Field is not Complete Already, Please Insert!")
                Button1.Text = "Input"
            Else

                If Val(TextBox3.Text) < Val(TextBox8.Text) Then
                            MsgBox("Your Item Quantity doesnt enough!")
                            Button1.Text = "Input"

                        Else
                            con.Open()
                            Dim kurang As Integer
                            kurang = Val(TextBox3.Text) - Val(TextBox8.Text)
                            TextBox3.Text = kurang
                            Dim rd As SqlDataReader
                            Dim cmd1 As SqlCommand
                            cmd1 = New SqlCommand("select * from FgTbl where itemid = '" & ComboBox1.Text & "'", con)
                            rd = cmd1.ExecuteReader
                            rd.Read()
                            con.Close()
                            con.Open()
                            Dim minusstock = "update FgTbl set itemqty =" & TextBox3.Text & " where itemid='" & ComboBox1.Text & "'"
                            cmd1 = New SqlCommand(minusstock, con)
                            cmd1.ExecuteNonQuery()
                            con.Close()
                            con.Open()
                            Dim cmd As New SqlCommand
                    Dim Insert As String = "insert into WfgTbl values('" & TextBox1.Text & "', '" & ComboBox1.Text & "', '" & TextBox2.Text & "','" & TextBox8.Text & "','" & ComboBox2.Text & "','" & TextBox4.Text & "','" & TextBox5.Text & "','" & TextBox6.Text & "','" & TextBox7.Text & "','" & TextBox9.Text & "','" & dtp.Value.ToShortDateString & "')"
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
            earlycond()
            isi()
        End If
    End Sub
    Private Sub DGV1_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DGV1.CellMouseClick

        ComboBox1.Text = DGV1.Rows(e.RowIndex).Cells(0).Value
        TextBox2.Text = DGV1.Rows(e.RowIndex).Cells(1).Value
        TextBox3.Text = DGV1.Rows(e.RowIndex).Cells(2).Value
    End Sub


    Private Sub TextBox8_TabIndexChanged(sender As Object, e As EventArgs) Handles TextBox8.TabIndexChanged

    End Sub

    Private Sub Label18_Click(sender As Object, e As EventArgs) Handles Label18.Click

    End Sub

    Private Sub Label17_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub TextBox7_TextChanged(sender As Object, e As EventArgs) Handles TextBox7.TextChanged

    End Sub

    Private Sub Label14_Click(sender As Object, e As EventArgs) Handles Label14.Click

    End Sub

    Private Sub Label13_Click(sender As Object, e As EventArgs) Handles Label13.Click

    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click

    End Sub

    Private Sub dtp_ValueChanged(sender As Object, e As EventArgs) Handles dtp.ValueChanged

    End Sub

    Private Sub TextBox5_TextChanged(sender As Object, e As EventArgs) Handles TextBox5.TextChanged

    End Sub

    Private Sub Label12_Click(sender As Object, e As EventArgs) Handles Label12.Click

    End Sub

    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles TextBox4.TextChanged

    End Sub

    Private Sub Label11_Click(sender As Object, e As EventArgs) Handles Label11.Click

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub Label9_Click(sender As Object, e As EventArgs) Handles Label9.Click

    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click

    End Sub

    Private Sub DGV1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGV1.CellContentClick

    End Sub

    Private Sub TextBox8_TextChanged(sender As Object, e As EventArgs) Handles TextBox8.TextChanged

    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged

    End Sub

    Private Sub Label10_Click(sender As Object, e As EventArgs) Handles Label10.Click

    End Sub

    Private Sub Label8_Click(sender As Object, e As EventArgs) Handles Label8.Click

    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click

    End Sub

    Private Sub TextBox6_TextChanged(sender As Object, e As EventArgs) Handles TextBox6.TextChanged

    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged

    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click

    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click

    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub
End Class
