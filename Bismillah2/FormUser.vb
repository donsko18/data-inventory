Imports System.Data.SqlClient

Public Class FormUser
    Dim con As New SqlConnection("Data Source=mssql-112619-0.cloudclusters.net,19221;Initial Catalog=inventoryvb;User ID=donnie;Password=Donnievp890")

    Public Sub earlycond()
        TextBox2.Enabled = False
        TextBox3.Enabled = False
        TextBox4.Enabled = False
        TextBox5.Enabled = False
        TextBox6.Enabled = False
        TextBox7.Enabled = False
        ComboBox1.Enabled = False
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
        TextBox6.Text = ""
        TextBox7.Text = ""
        ComboBox1.Text = ""
        Label11.Text = ""

        TextBox4.UseSystemPasswordChar = True


    End Sub
    Public Sub maincond()
        TextBox2.Enabled = True
        TextBox3.Enabled = True
        TextBox4.Enabled = True
        TextBox5.Enabled = True
        TextBox6.Enabled = True
        TextBox7.Enabled = True
        ComboBox1.Enabled = True

    End Sub
    Public Sub isi()
        con.Open()
        Dim sql = "select * from IdTbl"
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
        cmd1 = New SqlCommand("select * from IdTbl where userid in (select max(userid) from IdTbl)", con)
        Dim autocode As String
        Dim hitung As Long
        rd = cmd1.ExecuteReader
        rd.Read()
        If Not rd.HasRows Then
            autocode = "USR" + "001"
        Else
            hitung = Microsoft.VisualBasic.Right(rd.GetString(0), 3) + 1
            autocode = "USR" + Microsoft.VisualBasic.Right("000" & hitung, 3)
        End If
        TextBox1.Text = autocode
        con.Close()
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Button1.Text = "Input" Then
            MessageBox.Show("To complete the Data Input, You Must insert all the main fields", "NOTE")
            maincond()
            Button1.Text = "Save"
            Button2.Enabled = False
            Button3.Enabled = False
            Button4.Enabled = True
        Else
            If TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Or TextBox5.Text = "" Or TextBox6.Text = "" Or TextBox7.Text = "" Or ComboBox1.Text = "" Then
                MsgBox("You didnt Input all Field already, Please Re-Input again")
            Else
                Try

                    ''Label11.Text = "C:\Tugas Kuliah\Semester 7\Kerja Praktek\Web geming\logo.png"
                    con.Open()
                    Dim query As String
                    Dim cmd As SqlCommand
                    query = "insert into IdTbl values('" & TextBox1.Text & "', '" & TextBox2.Text & "', '" & TextBox3.Text & "', '" & TextBox4.Text & "','" & TextBox5.Text & "','" & TextBox6.Text & "','" & TextBox7.Text & "','" & ComboBox1.Text & "')"

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
                    TextBox6.Text = ""
                    TextBox7.Text = ""
                    ComboBox1.Text = ""
                    Label11.Text = ""
                Catch ex As Exception
                    MsgBox(ex.Message)

                End Try
                autoid()

                earlycond()
            End If
            Button1.Text = "Input"
            Button2.Enabled = True
            Button3.Enabled = True
            Button4.Enabled = False
        End If
    End Sub



    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If Button2.Text = "Edit" Then
            Button2.Text = "Save"
            maincond()
            Button1.Enabled = False
            Button3.Enabled = False
            Button4.Enabled = True
            DGV1.Enabled = True
        Else
            If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox4.Text = "" Or TextBox3.Text = "" Or TextBox5.Text = "" Or TextBox6.Text = "" Or TextBox7.Text = "" Or ComboBox1.Text = "" Then
                MsgBox("Please Click the right ID to Edit this fields")

            Else
                Dim sql = "update IdTbl set username='" & TextBox2.Text & "',name='" & TextBox3.Text & "',password='" & TextBox4.Text & "',adduser='" & TextBox5.Text & "',usermail='" & TextBox6.Text & "',userphon='" & TextBox7.Text & "',userlvl='" & ComboBox1.Text & "' where userid ='" & TextBox1.Text & "'"
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
                TextBox6.Text = ""
                TextBox7.Text = ""
                ComboBox1.Text = ""
                Label11.Text = ""
            End If
            Button2.Text = "Edit"
            Button1.Enabled = True
            Button3.Enabled = True
            Button4.Enabled = False
            DGV1.Enabled = False
            autoid()
            earlycond()
        End If
    End Sub
    Private Sub DGV1_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DGV1.CellMouseClick
        TextBox1.Text = DGV1.Rows(e.RowIndex).Cells(0).Value
        TextBox2.Text = DGV1.Rows(e.RowIndex).Cells(1).Value
        TextBox3.Text = DGV1.Rows(e.RowIndex).Cells(2).Value
        TextBox4.Text = DGV1.Rows(e.RowIndex).Cells(3).Value
        TextBox5.Text = DGV1.Rows(e.RowIndex).Cells(4).Value
        TextBox6.Text = DGV1.Rows(e.RowIndex).Cells(5).Value
        TextBox7.Text = DGV1.Rows(e.RowIndex).Cells(6).Value
        ComboBox1.Text = DGV1.Rows(e.RowIndex).Cells(7).Value

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
                MsgBox("Click The Right Users table To Delete It")
            Else
                con.Open()
                Dim query As String
                query = "delete from IdTbl where userid= '" & TextBox1.Text & "'"
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
                TextBox6.Text = ""
                TextBox7.Text = ""
                ComboBox1.Text = ""
                Button2.Enabled = False
            End If
            earlycond()
            Button3.Text = "Delete"
            Button4.Enabled = False
            Button3.Enabled = False
            Button1.Enabled = True
            DGV1.Enabled = False
        End If
    End Sub
    Private Sub FormUser_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        earlycond()
        autoid()
        isi()
        Button4.Enabled = False
        DGV1.Enabled = False
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Me.Close()

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        autoid()
        earlycond()
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
        TextBox6.Text = ""
        TextBox7.Text = ""
        ComboBox1.Text = ""
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            eyeoff.Visible = False
            eyeon.Visible = True
            TextBox4.UseSystemPasswordChar = False
        Else
            eyeoff.Visible = True
            eyeon.Visible = False
            TextBox4.UseSystemPasswordChar = True
        End If
    End Sub
End Class