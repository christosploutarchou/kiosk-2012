Imports Oracle.DataAccess.Client
Public Class frmNewUser
    Private Sub frmNewUser_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        frmMain.Show()
    End Sub

    Private Sub frmNewUser_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub frmNewUser_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fillUsersList()
        rdbNewUser.Checked = True
    End Sub

    Private Sub fillUsersList()
        Dim cmd As New OracleCommand("", conn)
        Dim dr As OracleDataReader
        Dim sql As String = ""
        Try
            sql = "select username from users where deleted = 0"
            cmd = New OracleCommand(sql, conn)
            Dim counter As Integer = 0
            cmd.CommandType = CommandType.Text
            dr = cmd.ExecuteReader()
            lstBoxUsers.Items.Clear()
            While dr.Read()
                lstBoxUsers.Items.Add(dr("username"))
            End While
            dr.Close()
        Catch ex As Exception
            createExceptionFile(ex.Message, sql)
            MessageBox.Show(ex.Message, "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            cmd.Dispose()
        End Try
    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Dispose()
    End Sub

    Private Sub lstBoxUsers_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstBoxUsers.SelectedIndexChanged
        If rdbNewUser.Checked Then
            Exit Sub
        Else
            txtBoxUsername.ReadOnly = True
            lnkLabelChangePass.Visible = True
            btnSave.Visible = True
            btnDelete.Visible = True

            Dim cmd As New OracleCommand("", conn)
            Dim dr As OracleDataReader
            Dim sql As String = ""
            Try
                sql = "select fullname, phone, address, id_num, nvl(access_level,0), nvl(view_reports,0), nvl(edit_prod,0),  nvl(edit_prod_full,0) from users " & _
                      "where username = '" & lstBoxUsers.Text & "'"
                cmd = New OracleCommand(sql, conn)
                Dim counter As Integer = 0
                cmd.CommandType = CommandType.Text

                dr = cmd.ExecuteReader()

                If dr.Read() Then
                    txtBoxFullName.Text = dr.GetValue(0)
                    txtBoxPhone.Text = dr.GetValue(1)
                    txtBoxAddress.Text = dr.GetValue(2)
                    txtBoxIdentity.Text = dr.GetValue(3)
                    txtBoxUsername.Text = lstBoxUsers.Text
                    If CInt(dr.GetValue(4)) = 1 Then
                        chkBoxAdmin.Checked = True
                    Else
                        chkBoxAdmin.Checked = False
                    End If

                    If CInt(dr.GetValue(5)) = 1 Then
                        chkBoxReports.Checked = True
                    Else
                        chkBoxReports.Checked = False
                    End If

                    If CInt(dr.GetValue(6)) = 1 Then
                        chkBoxEditProd.Checked = True
                    Else
                        chkBoxEditProd.Checked = False
                    End If

                    If CInt(dr.GetValue(7)) = 1 Then
                        chkBoxEditProdFull.Checked = True
                    Else
                        chkBoxEditProdFull.Checked = False
                    End If
                End If
                dr.Close()
            Catch ex As Exception
                createExceptionFile(ex.Message, " " & sql)
                MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                cmd.Dispose()
            End Try
        End If
    End Sub

    Dim mouseIndex As Integer = -1
    Private Sub ListBox1_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) _
                                   Handles lstBoxUsers.MouseMove
        Dim index As Integer = lstboxUsers.IndexFromPoint(e.Location)
        If index <> mouseIndex Then
            If mouseIndex > -1 Then
                Dim oldIndex As Integer = mouseIndex
                mouseIndex = -1
                If oldIndex <= lstboxUsers.Items.Count - 1 Then
                    lstboxUsers.Invalidate(lstboxUsers.GetItemRectangle(oldIndex))
                End If
            End If
            mouseIndex = index
            If mouseIndex > -1 Then
                lstboxUsers.Invalidate(lstboxUsers.GetItemRectangle(mouseIndex))
            End If
        End If

        If mouseIndex > -1 Then
            lstboxUsers.SelectedIndex = mouseIndex
            lstboxUsers_SelectedIndexChanged(sender, e)
        End If
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If MessageBox.Show("Διαγραφή χρήστη;", "Διαγραγή", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Dim cmd As New OracleCommand("", conn)
            Dim sql As String = ""
            Dim newUserName = lstBoxUsers.Text + "DEL"
            Try
                sql = "update users set username = '" & newUserName & "', deleted=1, deleted_by = '" & whois & "' " & _
                      "where username = '" & lstBoxUsers.Text & "'"
                cmd = New OracleCommand(sql, conn)
                cmd.CommandType = CommandType.Text
                cmd.ExecuteNonQuery()
                cmd.Dispose()
                fillUsersList()
                resetFields()
            Catch ex As Exception
                createExceptionFile(ex.Message, Sql)
                MessageBox.Show(ex.Message, "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                cmd.Dispose()
            End Try
        Else
            Exit Sub
        End If
    End Sub

    Private Sub resetFields()
        txtBoxFullName.Clear()
        txtBoxPhone.Clear()
        txtBoxAddress.Clear()
        txtBoxIdentity.Clear()
        txtBoxUsername.Clear()
        chkBoxAdmin.Checked = False
        btnDelete.Visible = False
        btnSave.Visible = False
        lblNewPassword.Visible = False
        txtBoxPassword.Visible = False
        chkBoxReports.Checked = False
        chkBoxEditProd.Checked = False
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim accessLevel As Integer
        Dim canViewReports As Integer
        Dim canEditProducts As Integer
        Dim canEditProductsFull As Integer

        If chkBoxAdmin.Checked Then
            accessLevel = 1
        Else
            accessLevel = 0
        End If

        If chkBoxReports.Checked Then
            canViewReports = 1
        Else
            canViewReports = 0
        End If

        If chkBoxEditProd.Checked Then
            canEditProducts = 1
        Else
            canEditProducts = 0
        End If

        If chkBoxEditProdFull.Checked Then
            canEditProductsFull = 1
        Else
            canEditProductsFull = 0
        End If

        Dim sql As String = ""
        Dim cmd As New OracleCommand(sql, conn)
        Dim dr As OracleDataReader

        Try
            If rdbNewUser.Checked Then
                If txtBoxFullName.Text = String.Empty Or txtBoxPhone.Text = String.Empty Or txtBoxAddress.Text = String.Empty Or txtBoxIdentity.Text = String.Empty Or txtBoxUsername.Text = String.Empty Then
                    MessageBox.Show("Υπάρχουν κενά πεδία", "Πληροφορία", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If

                If Not IsNumeric(txtBoxPassword.Text) Then
                    MessageBox.Show("Ο κωδικός πρέπει να αποτελείται μόνο από αριθμούς", "Πληροφορία", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If

                sql = "select count(*) from users where upper(username) = '" & txtBoxUsername.Text.ToUpper & "' and deleted = 0 "
                cmd = New OracleCommand(sql, conn)
                cmd.CommandType = CommandType.Text
                dr = cmd.ExecuteReader()
                If dr.Read Then
                    If CInt(dr(0)) > 0 Then
                        MessageBox.Show("Το username που επιλέξατε είναι ήδη καταχωρημένο", "Πληροφορία", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End If
                End If
                dr.Close()

                sql = "insert into users (UUID, username, PHONE, PASS, ID_NUM, FULLNAME, DELETED, CREATED_BY, ADDRESS, ACCESS_LEVEL, VIEW_REPORTS, EDIT_PROD, EDIT_PROD_FULL) " & _
                      "values (sys_guid(), '" & txtBoxUsername.Text.Replace("\'", "\`") & "'," & _
                      "                    '" & txtBoxPhone.Text.Replace("\'", "\`") & "'," & _
                      "                    '" & getEncryptedValue(txtBoxPassword.Text.Replace("\'", "\`")) & "'," & _
                      "                    '" & txtBoxIdentity.Text.Replace("\'", "\`") & "', " & _
                      "                    '" & txtBoxFullName.Text.Replace("\'", "\`") & "', " & _
                      "                    0, " & _
                      "                    '" & whois & "', " & _
                      "                    '" & txtBoxAddress.Text.Replace("\'", "\`") & "', " & _
                      "                     " & accessLevel & ", " & _
                      "                     " & canViewReports & ", " & _
                      "                     " & canEditProducts & ", " & _
                      "                     " & canEditProductsFull & ") "
            Else
                If lblNewPassword.Visible And txtBoxPassword.Text = String.Empty Then
                    MessageBox.Show("Νέος Κωδικός: Υποχρεωτικό Πεδίο", "Πληροφορία", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If

                sql = "update users set fullname = '" & txtBoxFullName.Text.Replace("\'", "\`") & "'," & _
                      "                 phone = '" & txtBoxPhone.Text.Replace("\'", "\`") & "'," & _
                      "                 id_num = '" & txtBoxIdentity.Text.Replace("\'", "\`") & "'," & _
                      "                 address = '" & txtBoxAddress.Text.Replace("\'", "\`") & "', " & _
                      "                 access_level = " & accessLevel & ",  " & _
                      "                 view_reports = " & canViewReports & ",  " & _
                      "                 edit_prod = " & canEditProducts & ",  " & _
                      "                 edit_prod_full = " & canEditProductsFull & "  "

                If lblNewPassword.Visible Then
                    sql += ",pass = '" & getEncryptedValue(txtBoxPassword.Text.Replace("\'", "\`")) & "' "
                End If

                sql += " where username = '" & lstBoxUsers.Text & "'"

            End If
            cmd = New OracleCommand(sql, conn)
            cmd.CommandType = CommandType.Text
            cmd.ExecuteNonQuery()
            fillUsersList()
            rdbNewUser.Checked = True
            resetFields()
            txtBoxPassword.Visible = True
            btnSave.Visible = True
            txtBoxPassword.Clear()
        Catch ex As Exception
            createExceptionFile(ex.Message, sql)
            MessageBox.Show(ex.Message, "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally            
            cmd.Dispose()
        End Try
    End Sub

    Private Sub lnkLabelChangePass_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkLabelChangePass.LinkClicked
        lblNewPassword.Visible = True
        txtBoxPassword.Visible = True
    End Sub

    Private Sub rdbNewUser_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbNewUser.CheckedChanged
        setFields()
    End Sub

    Private Sub rdbExisting_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbExisting.CheckedChanged
        setFields()
    End Sub

    Private Sub rdbNewUser_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdbNewUser.MouseHover
        rdbNewUser.Checked = True
        rdbExisting.Checked = False
    End Sub

    Private Sub rdbExisting_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdbExisting.MouseHover
        rdbNewUser.Checked = False
        rdbExisting.Checked = True
    End Sub

    Private Sub setFields()
        If rdbNewUser.Checked = True Then
            lblPassword.Visible = True
            txtBoxPassword.Visible = True
            lblNewPassword.Visible = False
            btnSave.Visible = True
            txtBoxFullName.Clear()
            txtBoxPhone.Clear()
            txtBoxAddress.Clear()
            txtBoxIdentity.Clear()
            txtBoxUsername.Clear()
            txtBoxPassword.Clear()
            txtBoxUsername.ReadOnly = False
            chkBoxAdmin.Checked = False
            chkBoxEditProd.Checked = False
            chkBoxEditProdFull.Checked = False
            chkBoxReports.Checked = False
            btnDelete.Visible = False
            lnkLabelChangePass.Visible = False
            btnClear.Visible = True
        Else
            btnSave.Visible = False
            lblPassword.Visible = False
            txtBoxPassword.Visible = False
            btnClear.Visible = False
        End If
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        txtBoxAddress.Clear()
        txtBoxFullName.Clear()
        txtBoxIdentity.Clear()
        txtBoxPassword.Clear()
        txtBoxPhone.Clear()
        txtBoxUsername.Clear()
        chkBoxAdmin.Checked = False
        chkBoxReports.Checked = False
        chkBoxEditProd.Checked = False
        chkBoxEditProdFull.Checked = False
    End Sub

    Private Sub txtBoxFullName_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBoxFullName.MouseEnter
        txtBoxFullName.BackColor = Color.Bisque
    End Sub

    Private Sub txtBoxFullName_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBoxFullName.MouseLeave
        txtBoxFullName.BackColor = Color.LemonChiffon
    End Sub

 

End Class