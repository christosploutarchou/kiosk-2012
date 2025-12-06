Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.OleDb

Public Class frmLogin

    Private Sub FrmLogin_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        If (openConn()) Then
            LogoutUser(username)
            closeConn()
        End If
    End Sub

    Private Sub FrmLogin_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Not openConn()) Then
            Me.Dispose()
        End If

        If computerName = "-1" Then
            Me.Dispose()
        End If
        isPasswordEncrypted()
        getKIOSKParams()
        FillUsersList()
        lblLoginTitle1.Text = LOGIN_TITLE1
        lblLoginTitle2.Text = LOGIN_TITLE2
    End Sub

    Private Sub FillUsersList()
        Dim q As String = "SELECT username FROM users WHERE deleted = 0 ORDER BY username"
        Try
            Using cmd As New OracleCommand(q, conn)
                cmd.CommandType = CommandType.Text

                Using dr As OracleDataReader = cmd.ExecuteReader()
                    lstboxUsers.BeginUpdate()
                    lstboxUsers.Items.Clear()

                    While dr.Read()
                        lstboxUsers.Items.Add(dr.GetString(dr.GetOrdinal("username")))
                    End While

                    lstboxUsers.EndUpdate()
                End Using
            End Using

        Catch ex As Exception
            createExceptionFile(ex.Message, " " & q)
            MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        closeConn()
        Me.Dispose()
    End Sub

    'Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
    '    txtBoxPassword.Clear()
    'End Sub

    'Private Sub NumberButton_Click(sender As Object, e As EventArgs) _
    'Handles btn0.Click, btn1.Click, btn2.Click, btn3.Click, btn4.Click, btn5.Click, btn6.Click, btn7.Click, btn8.Click, btn9.Click

    'Dim btn As Button = DirectCast(sender, Button)
    '   txtBoxPassword.Text &= btn.Text
    'End Sub

    Private Sub KeypadButton_Click(sender As Object, e As EventArgs) _
    Handles btn0.Click, btn1.Click, btn2.Click, btn3.Click, btn4.Click, btn5.Click, btn6.Click, btn7.Click, btn8.Click, btn9.Click, btnBack.Click, btnClear.Click

        Dim btn As Button = DirectCast(sender, Button)

        Select Case btn.Name
            Case "btnBack"
                If txtBoxPassword.Text.Length > 0 Then
                    txtBoxPassword.Text = txtBoxPassword.Text.Remove(txtBoxPassword.Text.Length - 1)
                End If

            Case "btnClear"
                txtBoxPassword.Clear()

            Case Else
                txtBoxPassword.Text &= btn.Text
        End Select
    End Sub

    'Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
    'If txtBoxPassword.Text.Length > 0 Then
    '       txtBoxPassword.Text = txtBoxPassword.Text.Substring(0, txtBoxPassword.Text.Length - 1)
    'End If
    'End Sub

    Private Sub BtnLogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogin.Click

        'If Not CheckHhdSerial() Then
        'Exit Sub
        'End If

        getVat3PercentColumns()
        'isBoxReport()

        username = lstboxUsers.Text
        whois = ""
        Dim sql As String = ""

        'If Not username.Equals("unlock") Then
        'If Not checkIfAllowConnection() Then
        'MessageBox.Show("Έχει ξεπεραστει ο μέγιστος αριθμός σημείων", "Σφάλμα σύνεδης", MessageBoxButtons.OK, MessageBoxIcon.Stop)
        'Exit Sub
        'End If
        'End If

        If Not IsLoggedIn(username) Then
            Dim cmd As New OracleCommand
            Try
                cmd = New OracleCommand(GET_USER_INFO, conn)
                sql = GET_USER_INFO
                Dim userNameparam As New OracleParameter
                userNameparam.OracleDbType = OracleDbType.Varchar2
                userNameparam.Value = username

                Dim passparam As New OracleParameter
                passparam.OracleDbType = OracleDbType.Varchar2
                passparam.Value = getEncryptedValue(txtBoxPassword.Text)

                cmd.Parameters.Add(userNameparam)
                cmd.Parameters.Add(passparam)

                cmd.CommandType = CommandType.Text
                Dim isUnlock As Boolean = False
                Using drLogged = cmd.ExecuteReader()
                    If drLogged.Read() Then
                        If CInt(drLogged.GetValue(3)) = 1 Then
                            canViewReports = True
                        Else
                            canViewReports = False
                        End If

                        If CInt(drLogged.GetValue(4)) = 1 Then
                            canEditProducts = True
                        Else
                            canEditProducts = False
                        End If

                        If CInt(drLogged.GetValue(5)) = 1 Then
                            canEditProductsFull = True
                        Else
                            canEditProductsFull = False
                        End If

                        If CInt(drLogged.GetValue(1)) = 1 Then
                            isAdmin = True
                        Else
                            isAdmin = False
                        End If

                        whois = drLogged.GetValue(0)
                        Me.Hide()
                        txtBoxPassword.Clear()
                        If CInt(drLogged.GetValue(2)) = 1 Then
                            isUnlock = True
                            frmUnlockUser.Show()
                            'ElseIf Not isAdmin And Not canEditProducts And Not canEditProductsFull Then
                        Else
                            If Not isAdmin Then
                                frmPOS.Show()
                            Else
                                frmMain.Show()
                            End If

                            If dualMonitor Then
                                frmDual.Show()
                            End If
                            'Else
                            'frmMain.Show()
                        End If
                    Else
                        MessageBox.Show(INVALID_LOGIN, ERROR_MSG, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        drLogged.Close()
                        cmd.Dispose()
                        Exit Sub
                    End If
                    cmd.Dispose()
                End Using

                If Not isUnlock Then
                    Dim amountLaxeia As Double = 0
                    amountLaxeia = getAmountLaxeia()

                    Dim computerNameparam As New OracleParameter
                    computerNameparam.OracleDbType = OracleDbType.Varchar2
                    computerNameparam.Value = computerName

                    Dim userNameInsert As New OracleParameter
                    userNameInsert.OracleDbType = OracleDbType.Varchar2
                    userNameInsert.Value = username

                    Dim amountLaxeiaParam As New OracleParameter
                    amountLaxeiaParam.OracleDbType = OracleDbType.Decimal
                    amountLaxeiaParam.Value = amountLaxeia

                    cmd = New OracleCommand(NEW_SESSION, conn)
                    sql = NEW_SESSION
                    cmd.Parameters.Add(computerNameparam)
                    cmd.Parameters.Add(userNameInsert)
                    cmd.Parameters.Add(amountLaxeiaParam)

                    cmd.CommandType = CommandType.Text
                    cmd.ExecuteReader()
                End If
            Catch ex As Exception
                CreateExceptionFile(ex.Message, " " & sql)
                MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                cmd.Dispose()
            End Try
        Else
            MessageBox.Show(USER_ALREADY_LOGGED_IN, ERROR_MSG, MessageBoxButtons.OK, MessageBoxIcon.Error)
            username = ""
            lstboxUsers.SelectedIndex = -1
        End If
    End Sub

    'Private Function checkIfAllowConnection() As Boolean
    'Dim cmd As New OracleCommand("", conn)
    'Dim dr As OracleDataReader
    'Dim currentSessions As Integer = 0
    'Dim maxAllowedSessions As Integer = 1000 'PERIPTERO 130 = 2, XDRIVE=UNLIMITED (1000)
    'Try
    '       cmd = New OracleCommand("select count(*) from sessions where is_active=1", conn)
    '      cmd.CommandType = CommandType.Text
    '     dr = cmd.ExecuteReader()
    'If dr.Read() Then
    '           currentSessions = CInt(dr(0))
    'End If
    '       dr.Close()
    'Catch ex As Exception
    '       createExceptionFile(ex.Message, "")
    '      MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
    'Finally
    '       cmd.Dispose()
    'End Try
    '
    'If currentSessions >= maxAllowedSessions Then
    'Return False
    'End If
    'Return True
    'End Function

    Private Sub lstboxUsers_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstboxUsers.SelectedIndexChanged
        txtBoxPassword.Clear()
    End Sub

    Protected Overrides ReadOnly Property CreateParams() As CreateParams
        'Disables X button
        Get
            Dim param As CreateParams = MyBase.CreateParams
            param.ClassStyle = param.ClassStyle Or &H200
            Return param
        End Get
    End Property

    Dim mouseIndex As Integer = -1

    Private Sub Control_MouseEnter(sender As Object, e As EventArgs) _
        Handles txtBoxPassword.MouseEnter, lstboxUsers.MouseEnter

        Dim ctrl As Control = DirectCast(sender, Control)
        ctrl.BackColor = Color.Bisque
        ctrl.Focus()
    End Sub

    Private Sub Control_MouseLeave(sender As Object, e As EventArgs) _
        Handles txtBoxPassword.MouseLeave, lstboxUsers.MouseLeave

        Dim ctrl As Control = DirectCast(sender, Control)
        ctrl.BackColor = Color.LemonChiffon
    End Sub

End Class
