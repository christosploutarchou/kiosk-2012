Imports System.Data.SQLite
Imports Oracle.DataAccess.Client
Public Class frmNewUser
    Dim mouseIndex As Integer = -1
    Private Sub FrmNewUser_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        frmMain.Show()
    End Sub

    Private Sub FrmNewUser_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub FrmNewUser_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        FillUsersList()
        rdbNewUser.Checked = True
    End Sub

    Private Sub FillUsersList()
        Dim sql As String = ""
        Try
            lstBoxUsers.Items.Clear()
            If SqlLite Then
                sql = "SELECT username
                   FROM users
                   WHERE deleted = 0
                     AND kioskid = @KIOSKID
                   ORDER BY username"

                Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                    sqliteConn.Open()
                    Using cmd As New SQLiteCommand(sql, sqliteConn)
                        cmd.Parameters.AddWithValue("@KIOSKID", kioskId)
                        Using dr As SQLiteDataReader = cmd.ExecuteReader()
                            While dr.Read()
                                lstBoxUsers.Items.Add(dr("username").ToString())
                            End While
                        End Using
                    End Using
                End Using
            Else
                sql = "SELECT username
                   FROM users
                   WHERE deleted = 0
                   ORDER BY username"

                Using cmd As New OracleCommand(sql, conn)
                    cmd.CommandType = CommandType.Text
                    Using dr As OracleDataReader = cmd.ExecuteReader()
                        While dr.Read()
                            lstBoxUsers.Items.Add(dr("username").ToString())
                        End While
                    End Using
                End Using
            End If
        Catch ex As Exception
            CreateExceptionFile(ex.Message, sql)
            MessageBox.Show(ex.Message, "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Dispose()
    End Sub

    Private Sub LstBoxUsers_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstBoxUsers.SelectedIndexChanged

        If rdbNewUser.Checked Then Exit Sub

        txtBoxUsername.ReadOnly = True
        lnkLabelChangePass.Visible = True
        btnSave.Visible = True
        btnDelete.Visible = True
        Dim sql As String = ""

        Try
            If SqlLite Then
                sql = "SELECT
                        fullname,
                        phone,
                        address,
                        id_num,
                        IFNULL(access_level,0),
                        IFNULL(view_reports,0),
                        IFNULL(edit_prod,0),
                        IFNULL(edit_prod_full,0)
                   FROM users
                   WHERE username=@USERNAME
                     AND kioskid=@KIOSKID"

                Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                    sqliteConn.Open()
                    Using cmd As New SQLiteCommand(sql, sqliteConn)
                        cmd.Parameters.AddWithValue("@USERNAME", lstBoxUsers.Text)
                        cmd.Parameters.AddWithValue("@KIOSKID", kioskId)
                        Using dr As SQLiteDataReader = cmd.ExecuteReader()
                            If dr.Read() Then
                                txtBoxFullName.Text = dr("fullname").ToString()
                                txtBoxPhone.Text = dr("phone").ToString()
                                txtBoxAddress.Text = dr("address").ToString()
                                txtBoxIdentity.Text = dr("id_num").ToString()
                                txtBoxUsername.Text = lstBoxUsers.Text

                                chkBoxAdmin.Checked = Convert.ToInt32(dr("access_level")) = 1
                                chkBoxReports.Checked = Convert.ToInt32(dr("view_reports")) = 1
                                chkBoxEditProd.Checked = Convert.ToInt32(dr("edit_prod")) = 1
                                chkBoxEditProdFull.Checked = Convert.ToInt32(dr("edit_prod_full")) = 1
                            End If
                        End Using
                    End Using
                End Using
            Else
                sql = "SELECT
                        fullname,
                        phone,
                        address,
                        id_num,
                        NVL(access_level,0),
                        NVL(view_reports,0),
                        NVL(edit_prod,0),
                        NVL(edit_prod_full,0)
                   FROM users
                   WHERE username = :USERNAME"

                Using cmd As New OracleCommand(sql, conn)
                    cmd.Parameters.Add("USERNAME", OracleDbType.Varchar2).Value = lstBoxUsers.Text
                    Using dr As OracleDataReader = cmd.ExecuteReader()
                        If dr.Read() Then
                            txtBoxFullName.Text = dr.GetValue(0).ToString()
                            txtBoxPhone.Text = dr.GetValue(1).ToString()
                            txtBoxAddress.Text = dr.GetValue(2).ToString()
                            txtBoxIdentity.Text = dr.GetValue(3).ToString()
                            txtBoxUsername.Text = lstBoxUsers.Text

                            chkBoxAdmin.Checked = CInt(dr.GetValue(4)) = 1
                            chkBoxReports.Checked = CInt(dr.GetValue(5)) = 1
                            chkBoxEditProd.Checked = CInt(dr.GetValue(6)) = 1
                            chkBoxEditProdFull.Checked = CInt(dr.GetValue(7)) = 1

                        End If
                    End Using
                End Using
            End If
        Catch ex As Exception
            CreateExceptionFile(ex.Message, sql)
            MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ListBox1_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles lstBoxUsers.MouseMove
        Dim index As Integer = lstBoxUsers.IndexFromPoint(e.Location)
        If index <> mouseIndex Then
            If mouseIndex > -1 Then
                Dim oldIndex As Integer = mouseIndex
                mouseIndex = -1
                If oldIndex <= lstBoxUsers.Items.Count - 1 Then
                    lstBoxUsers.Invalidate(lstBoxUsers.GetItemRectangle(oldIndex))
                End If
            End If
            mouseIndex = index
            If mouseIndex > -1 Then
                lstBoxUsers.Invalidate(lstBoxUsers.GetItemRectangle(mouseIndex))
            End If
        End If

        If mouseIndex > -1 Then
            lstBoxUsers.SelectedIndex = mouseIndex
            LstBoxUsers_SelectedIndexChanged(sender, e)
        End If
    End Sub

    Private Sub BtnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If MessageBox.Show("Διαγραφή χρήστη;", "Διαγραφή", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) <> DialogResult.Yes Then
            Exit Sub
        End If

        Dim sql As String = ""
        Dim newUserName As String = lstBoxUsers.Text & "DEL"
        Try
            If SqlLite Then
                sql = "UPDATE users
                   SET username=@USERNAME,
                       deleted=1,
                       deleted_by=@DELETED_BY
                   WHERE username=@OLD_USERNAME
                     AND kioskid=@KIOSKID"

                Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                    sqliteConn.Open()
                    Using cmd As New SQLiteCommand(sql, sqliteConn)
                        cmd.Parameters.AddWithValue("@USERNAME", newUserName)
                        cmd.Parameters.AddWithValue("@DELETED_BY", whois)
                        cmd.Parameters.AddWithValue("@OLD_USERNAME", lstBoxUsers.Text)
                        cmd.Parameters.AddWithValue("@KIOSKID", kioskId)
                        cmd.ExecuteNonQuery()
                    End Using
                End Using
            Else
                sql = "UPDATE users
                   SET username=:USERNAME,
                       deleted=1,
                       deleted_by=:DELETED_BY
                   WHERE username=:OLD_USERNAME"

                Using cmd As New OracleCommand(sql, conn)
                    cmd.Parameters.Add("USERNAME", OracleDbType.Varchar2).Value = newUserName
                    cmd.Parameters.Add("DELETED_BY", OracleDbType.Varchar2).Value = whois
                    cmd.Parameters.Add("OLD_USERNAME", OracleDbType.Varchar2).Value = lstBoxUsers.Text
                    cmd.ExecuteNonQuery()
                End Using
            End If
            FillUsersList()
            ResetFields()
        Catch ex As Exception
            CreateExceptionFile(ex.Message, sql)
            MessageBox.Show(ex.Message, "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ResetFields()
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

    Private Function UserNameExists(ByVal username As String) As Boolean
        Dim sql As String = ""
        Try
            If SqlLite Then
                sql = "SELECT COUNT(*)
                   FROM users
                   WHERE UPPER(username)=@USERNAME
                     AND deleted=0
                     AND kioskid=@KIOSKID"

                Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                    sqliteConn.Open()
                    Using cmd As New SQLiteCommand(sql, sqliteConn)
                        cmd.Parameters.AddWithValue("@USERNAME", username.ToUpper())
                        cmd.Parameters.AddWithValue("@KIOSKID", kioskId)
                        Return Convert.ToInt32(cmd.ExecuteScalar()) > 0
                    End Using
                End Using
            Else
                sql = "SELECT COUNT(*)
                   FROM users
                   WHERE UPPER(username)=:USERNAME
                     AND deleted=0"

                Using cmd As New OracleCommand(sql, conn)
                    cmd.Parameters.Add("USERNAME", OracleDbType.Varchar2).Value = username.ToUpper()
                    Return Convert.ToInt32(cmd.ExecuteScalar()) > 0
                End Using
            End If
        Catch ex As Exception
            CreateExceptionFile(ex.Message, sql)
            MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    Private Sub BtnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
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

                If (UserNameExists(txtBoxUsername.Text.ToUpper)) Then
                    MessageBox.Show("Το username που επιλέξατε είναι ήδη καταχωρημένο", "Πληροφορία", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If

                InsertIntoUsers(accessLevel)
            Else
                If lblNewPassword.Visible And txtBoxPassword.Text = String.Empty Then
                    MessageBox.Show("Νέος Κωδικός: Υποχρεωτικό Πεδίο", "Πληροφορία", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If

                UpdateExistingUser(accessLevel)

            End If
            FillUsersList()
            rdbNewUser.Checked = True
            ResetFields()
            txtBoxPassword.Visible = True
            btnSave.Visible = True
            txtBoxPassword.Clear()
        Catch ex As Exception
            CreateExceptionFile(ex.Message, sql)
            MessageBox.Show(ex.Message, "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub UpdateExistingUser(ByVal accessLevel As Integer)
        Dim WhoAmI As String = "frmNewUser.UpdateExistingUser"
        Dim sql As String = ""

        Try
            If SqlLite Then

                sql =
                "UPDATE users
             SET fullname=@FULLNAME,
                 phone=@PHONE,
                 id_num=@ID_NUM,
                 address=@ADDRESS,
                 access_level=@ACCESS_LEVEL,
                 view_reports=@VIEW_REPORTS,
                 edit_prod=@EDIT_PROD,
                 edit_prod_full=@EDIT_PROD_FULL"

                If lblNewPassword.Visible Then
                    sql &= ", pass=@PASS"
                End If

                sql &= "
             WHERE username=@USERNAME
               AND kioskid=@KIOSKID"

                Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                    sqliteConn.Open()
                    Using cmd As New SQLiteCommand(sql, sqliteConn)
                        cmd.Parameters.AddWithValue("@FULLNAME", txtBoxFullName.Text)
                        cmd.Parameters.AddWithValue("@PHONE", txtBoxPhone.Text)
                        cmd.Parameters.AddWithValue("@ID_NUM", txtBoxIdentity.Text)
                        cmd.Parameters.AddWithValue("@ADDRESS", txtBoxAddress.Text)
                        cmd.Parameters.AddWithValue("@ACCESS_LEVEL", accessLevel)
                        cmd.Parameters.AddWithValue("@VIEW_REPORTS", canViewReports)
                        cmd.Parameters.AddWithValue("@EDIT_PROD", canEditProducts)
                        cmd.Parameters.AddWithValue("@EDIT_PROD_FULL", canEditProductsFull)

                        If lblNewPassword.Visible Then
                            cmd.Parameters.AddWithValue("@PASS", getEncryptedValue(txtBoxPassword.Text))
                        End If

                        cmd.Parameters.AddWithValue("@USERNAME", lstBoxUsers.Text)
                        cmd.Parameters.AddWithValue("@KIOSKID", kioskId)
                        cmd.ExecuteNonQuery()
                    End Using
                End Using
            Else
                sql =
                "UPDATE users
             SET fullname=:FULLNAME,
                 phone=:PHONE,
                 id_num=:ID_NUM,
                 address=:ADDRESS,
                 access_level=:ACCESS_LEVEL,
                 view_reports=:VIEW_REPORTS,
                 edit_prod=:EDIT_PROD,
                 edit_prod_full=:EDIT_PROD_FULL"

                If lblNewPassword.Visible Then
                    sql &= ", pass=:PASS"
                End If

                sql &= "
             WHERE username=:USERNAME"

                Using cmd As New OracleCommand(sql, conn)
                    cmd.BindByName = True
                    cmd.Parameters.Add("FULLNAME", OracleDbType.Varchar2).Value = txtBoxFullName.Text
                    cmd.Parameters.Add("PHONE", OracleDbType.Varchar2).Value = txtBoxPhone.Text
                    cmd.Parameters.Add("ID_NUM", OracleDbType.Varchar2).Value = txtBoxIdentity.Text
                    cmd.Parameters.Add("ADDRESS", OracleDbType.Varchar2).Value = txtBoxAddress.Text
                    cmd.Parameters.Add("ACCESS_LEVEL", OracleDbType.Int32).Value = accessLevel
                    cmd.Parameters.Add("VIEW_REPORTS", OracleDbType.Int32).Value = canViewReports
                    cmd.Parameters.Add("EDIT_PROD", OracleDbType.Int32).Value = canEditProducts
                    cmd.Parameters.Add("EDIT_PROD_FULL", OracleDbType.Int32).Value = canEditProductsFull

                    If lblNewPassword.Visible Then
                        cmd.Parameters.Add("PASS", OracleDbType.Varchar2).Value = getEncryptedValue(txtBoxPassword.Text)
                    End If

                    cmd.Parameters.Add("USERNAME", OracleDbType.Varchar2).Value = lstBoxUsers.Text
                    cmd.ExecuteNonQuery()
                End Using
            End If
        Catch ex As Exception
            CreateExceptionFile(WhoAmI + " " + ex.Message, sql)
        End Try
    End Sub

    Private Sub InsertIntoUsers(ByVal accessLevel As Integer)
        Dim WhoAmI = "frmNewUsers.InsertIntoUsers"
        Dim sql As String = ""
        Try
            If SqlLite Then

                sql =
                "INSERT INTO users
            (
                uuid,
                username,
                phone,
                pass,
                id_num,
                fullname,
                deleted,
                created_by,
                address,
                access_level,
                view_reports,
                edit_prod,
                edit_prod_full,
                kioskid
            )
            VALUES
            (
                @UUID,
                @USERNAME,
                @PHONE,
                @PASS,
                @ID_NUM,
                @FULLNAME,
                0,
                @CREATED_BY,
                @ADDRESS,
                @ACCESS_LEVEL,
                @VIEW_REPORTS,
                @EDIT_PROD,
                @EDIT_PROD_FULL,
                @KIOSKID
            )"

                Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                    sqliteConn.Open()
                    Using cmd As New SQLiteCommand(sql, sqliteConn)
                        cmd.Parameters.AddWithValue("@UUID", Guid.NewGuid().ToString("N").ToUpper())
                        cmd.Parameters.AddWithValue("@USERNAME", txtBoxUsername.Text)
                        cmd.Parameters.AddWithValue("@PHONE", txtBoxPhone.Text)
                        cmd.Parameters.AddWithValue("@PASS", getEncryptedValue(txtBoxPassword.Text))
                        cmd.Parameters.AddWithValue("@ID_NUM", txtBoxIdentity.Text)
                        cmd.Parameters.AddWithValue("@FULLNAME", txtBoxFullName.Text)
                        cmd.Parameters.AddWithValue("@CREATED_BY", whois)
                        cmd.Parameters.AddWithValue("@ADDRESS", txtBoxAddress.Text)
                        cmd.Parameters.AddWithValue("@ACCESS_LEVEL", accessLevel)
                        cmd.Parameters.AddWithValue("@VIEW_REPORTS", canViewReports)
                        cmd.Parameters.AddWithValue("@EDIT_PROD", canEditProducts)
                        cmd.Parameters.AddWithValue("@EDIT_PROD_FULL", canEditProductsFull)
                        cmd.Parameters.AddWithValue("@KIOSKID", kioskId)
                        cmd.ExecuteNonQuery()
                    End Using
                End Using
            Else
                sql =
                        "INSERT INTO users
                    (UUID, username, PHONE, PASS, ID_NUM, FULLNAME,
                        DELETED, CREATED_BY, ADDRESS,
                        ACCESS_LEVEL, VIEW_REPORTS, EDIT_PROD, EDIT_PROD_FULL)
                        VALUES
                    (sys_guid(),
                        :USERNAME,
                        :PHONE,
                        :PASS,
                        :ID_NUM,
                        :FULLNAME,
                        0,
                        :CREATED_BY,
                        :ADDRESS,
                        :ACCESS_LEVEL,
                        :VIEW_REPORTS,
                        :EDIT_PROD,
                        :EDIT_PROD_FULL)"

                Using cmd As New OracleCommand(sql, conn)
                    cmd.Parameters.Add("USERNAME", OracleDbType.Varchar2).Value = txtBoxUsername.Text
                    cmd.Parameters.Add("PHONE", OracleDbType.Varchar2).Value = txtBoxPhone.Text
                    cmd.Parameters.Add("PASS", OracleDbType.Varchar2).Value = getEncryptedValue(txtBoxPassword.Text)
                    cmd.Parameters.Add("ID_NUM", OracleDbType.Varchar2).Value = txtBoxIdentity.Text
                    cmd.Parameters.Add("FULLNAME", OracleDbType.Varchar2).Value = txtBoxFullName.Text
                    cmd.Parameters.Add("CREATED_BY", OracleDbType.Varchar2).Value = whois
                    cmd.Parameters.Add("ADDRESS", OracleDbType.Varchar2).Value = txtBoxAddress.Text
                    cmd.Parameters.Add("ACCESS_LEVEL", OracleDbType.Int32).Value = accessLevel
                    cmd.Parameters.Add("VIEW_REPORTS", OracleDbType.Int32).Value = canViewReports
                    cmd.Parameters.Add("EDIT_PROD", OracleDbType.Int32).Value = canEditProducts
                    cmd.Parameters.Add("EDIT_PROD_FULL", OracleDbType.Int32).Value = canEditProductsFull
                    cmd.ExecuteNonQuery()
                End Using
            End If
        Catch ex As Exception
            CreateExceptionFile(WhoAmI + " " + ex.Message, Sql)
        End Try
    End Sub

    Private Sub LnkLabelChangePass_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkLabelChangePass.LinkClicked
        lblNewPassword.Visible = True
        txtBoxPassword.Visible = True
    End Sub

    Private Sub RdbNewUser_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbNewUser.CheckedChanged
        setFields()
    End Sub

    Private Sub RdbExisting_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbExisting.CheckedChanged
        SetFields()
    End Sub

    Private Sub RdbNewUser_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdbNewUser.MouseHover
        rdbNewUser.Checked = True
        rdbExisting.Checked = False
    End Sub

    Private Sub RdbExisting_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdbExisting.MouseHover
        rdbNewUser.Checked = False
        rdbExisting.Checked = True
    End Sub

    Private Sub SetFields()
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

    Private Sub BtnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
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

    Private Sub TxtBoxFullName_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBoxFullName.MouseEnter
        txtBoxFullName.BackColor = Color.Bisque
    End Sub

    Private Sub TxtBoxFullName_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBoxFullName.MouseLeave
        txtBoxFullName.BackColor = Color.LemonChiffon
    End Sub

End Class