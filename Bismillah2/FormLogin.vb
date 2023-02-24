Imports System.Data.SqlClient
Public Class FormLogin
    Dim con As New SqlConnection("Data Source=mssql-112619-0.cloudclusters.net,19221;Initial Catalog=inventoryvb;User ID=donnie;Password=Donnievp890")
    Private Sub FormLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox2.UseSystemPasswordChar = True
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text = "" And TextBox2.Text = "" Then
            MessageBox.Show("Isi Username dan Password terlebih dahulu!")
        Else
            con.Open()
            Dim cmd As SqlCommand
            Dim rd As SqlDataReader
            cmd = New SqlCommand("select * from IdTbl where username='" & TextBox1.Text & "' and password='" & TextBox2.Text & "'", con)
            rd = cmd.ExecuteReader
            rd.Read()
            If Not rd.HasRows Then
                MsgBox("Username atau Password yang anda masukan salah!")
                TextBox1.Text = ""
                TextBox2.Text = ""
                TextBox1.Focus()
            Else
                Me.Hide()
                FormMenu.Show()
                FormMenu.lblid.Text = rd!userid
                FormMenu.lblnamauser.Text = rd!username
                FormMenu.lbllevel.Text = rd!userlvl
                If FormMenu.lbllevel.Text = "WH Employee" Then
                    FormMenu.Button6.Visible = False
                    FormMenu.btnfgin.Visible = False
                    FormMenu.lblin.Visible = False
                    FormMenu.btnrwin.Visible = False
                    FormMenu.lblrwin.Visible = False
                Else
                    FormMenu.Button6.Visible = True
                    FormMenu.btnfgin.Visible = False
                    FormMenu.lblin.Visible = False
                    FormMenu.btnrwin.Visible = False
                    FormMenu.lblrwin.Visible = False
                End If

            End If
            con.Close()
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            eyeslash.Visible = False
            eye.Visible = True
            TextBox2.UseSystemPasswordChar = False
        Else
            eyeslash.Visible = True
            eye.Visible = False
            TextBox2.UseSystemPasswordChar = True
        End If
    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Chr(13) Then
            TextBox2.Focus()
        End If
    End Sub

    Private Sub TextBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox2.KeyPress
        If e.KeyChar = Chr(13) Then
            If TextBox1.Text = "" And TextBox2.Text = "" Then
                MessageBox.Show("Isi Username dan Password terlebih dahulu!")
            Else
                con.Open()
                Dim cmd As SqlCommand
                Dim rd As SqlDataReader
                cmd = New SqlCommand("select * from IdTbl where username='" & TextBox1.Text & "' and password='" & TextBox2.Text & "'", con)
                rd = cmd.ExecuteReader
                rd.Read()
                If Not rd.HasRows Then
                    MsgBox("Username atau Password yang anda masukan salah!")
                    TextBox1.Text = ""
                    TextBox2.Text = ""
                    TextBox1.Focus()
                Else
                    Me.Hide()
                    FormMenu.Show()
                    FormMenu.lblid.Text = rd!userid
                    FormMenu.lblnamauser.Text = rd!username
                    FormMenu.lbllevel.Text = rd!userlvl
                    If FormMenu.lbllevel.Text = "WH Employee" Then
                        FormMenu.Button6.Visible = False
                        FormMenu.btnfgin.Visible = False
                        FormMenu.lblin.Visible = False
                        FormMenu.btnrwin.Visible = False
                        FormMenu.lblrwin.Visible = False
                    Else
                        FormMenu.Button6.Visible = True
                        FormMenu.btnfgin.Visible = True
                        FormMenu.lblin.Visible = True
                        FormMenu.btnrwin.Visible = True
                        FormMenu.lblrwin.Visible = True
                    End If

                End If
                con.Close()
            End If
        End If
    End Sub
End Class