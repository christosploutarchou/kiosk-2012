Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.OleDb

Public Class frmLogin

    Private Sub frmLogin_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        If (openConn()) Then
            logoutUser(username)
            closeConn()
        End If
    End Sub

    Private Sub frmLogin_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Not openConn()) Then
            Me.Dispose()
        End If

        If computerName = "-1" Then
            Me.Dispose()
        End If
        isPasswordEncrypted()
        getKIOSKParams()
        fillUsersList()
        lblLoginTitle1.Text = LOGIN_TITLE1
        lblLoginTitle2.Text = LOGIN_TITLE2
    End Sub

    Private Sub fillUsersList()
        Dim cmd As New OracleCommand(GET_USERS_ON_LOGIN, conn)
        Dim counter As Integer = 0
        Dim dr As OracleDataReader
        Try
            cmd.CommandType = CommandType.Text
            dr = cmd.ExecuteReader()
            While dr.Read()
                lstboxUsers.Items.Add(dr("username"))
            End While
            dr.Close()
        Catch ex As Exception
            createExceptionFile(ex.Message, " " & GET_USERS_ON_LOGIN)
            MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            cmd.Dispose()            
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        closeConn()
        Me.Dispose()
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        txtBoxPassword.Clear()
    End Sub

    Private Sub btn7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn7.Click
        txtBoxPassword.Text += "7"
    End Sub

    Private Sub btn8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn8.Click
        txtBoxPassword.Text += "8"
    End Sub

    Private Sub btn9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn9.Click
        txtBoxPassword.Text += "9"
    End Sub

    Private Sub btn4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn4.Click
        txtBoxPassword.Text += "4"
    End Sub

    Private Sub btn5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn5.Click
        txtBoxPassword.Text += "5"
    End Sub

    Private Sub btn6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn6.Click
        txtBoxPassword.Text += "6"
    End Sub

    Private Sub btn1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn1.Click
        txtBoxPassword.Text += "1"
    End Sub

    Private Sub btn2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn2.Click
        txtBoxPassword.Text += "2"
    End Sub

    Private Sub btn3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn3.Click
        txtBoxPassword.Text += "3"
    End Sub

    Private Sub btn0_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn0.Click
        txtBoxPassword.Text += "0"
    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        If txtBoxPassword.Text.Length > 0 Then
            txtBoxPassword.Text = txtBoxPassword.Text.Substring(0, txtBoxPassword.Text.Length - 1)
        End If
    End Sub

    Private Sub btnLogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogin.Click

        If Not CheckHhdSerial() Then
            Exit Sub
        End If

        username = lstboxUsers.Text
        whois = ""
        Dim sql As String = ""

        If Not username.Equals("unlock") Then
            If Not checkIfAllowConnection() Then
                MessageBox.Show("Έχει ξεπεραστει ο μέγιστος αριθμός σημείων", "Σφάλμα σύνεδης", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                Exit Sub
            End If            
        End If

        If Not isLoggedIn(username) Then
            Dim cmd As New OracleCommand
            Dim dr As OracleDataReader
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
                dr = cmd.ExecuteReader()

                Dim isUnlock As Boolean = False

                If dr.Read() Then
                    If CInt(dr.GetValue(3)) = 1 Then
                        canViewReports = True
                    Else
                        canViewReports = False
                    End If

                    If CInt(dr.GetValue(4)) = 1 Then
                        canEditProducts = True
                    Else
                        canEditProducts = False
                    End If

                    If CInt(dr.GetValue(5)) = 1 Then
                        canEditProductsFull = True
                    Else
                        canEditProductsFull = False
                    End If

                    If CInt(dr.GetValue(1)) = 1 Then
                        isAdmin = True
                    Else
                        isAdmin = False
                    End If

                    whois = dr.GetValue(0)
                    Me.Hide()
                    txtBoxPassword.Clear()
                    If CInt(dr.GetValue(2)) = 1 Then
                        isUnlock = True
                        frmUnlockUser.Show()
                        'ElseIf Not isAdmin And Not canEditProducts And Not canEditProductsFull Then
                    Else
                        frmPOS.Show()
                        If dualMonitor Then
                            frmDual.Show()
                        End If
                        'Else
                        'frmMain.Show()
                    End If
                Else
                    MessageBox.Show(INVALID_LOGIN, ERROR_MSG, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    dr.Close()
                    cmd.Dispose()
                    Exit Sub
                End If
                cmd.Dispose()

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
                dr.Close()
            Catch ex As Exception
                createExceptionFile(ex.Message, " " & sql)
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

    Private Function checkIfAllowConnection() As Boolean
        Dim cmd As New OracleCommand("", conn)
        Dim dr As OracleDataReader
        Dim currentSessions As Integer = 0
        Dim maxAllowedSessions As Integer = 1000 'PERIPTERO 130 = 2, XDRIVE=UNLIMITED (1000)
        Try
            cmd = New OracleCommand("select count(*) from sessions where is_active=1", conn)
            cmd.CommandType = CommandType.Text
            dr = cmd.ExecuteReader()
            If dr.Read() Then
                currentSessions = CInt(dr(0))
            End If
            dr.Close()
        Catch ex As Exception
            createExceptionFile(ex.Message, "")
            MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            cmd.Dispose()
        End Try

        If currentSessions >= maxAllowedSessions Then
            Return False
        End If
        Return True
    End Function

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

    'Private Sub ListBox1_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) _
    '                               Handles lstboxUsers.MouseMove
    '    Dim index As Integer = lstboxUsers.IndexFromPoint(e.Location)
    '    If index <> mouseIndex Then
    '        If mouseIndex > -1 Then
    '            Dim oldIndex As Integer = mouseIndex
    '            mouseIndex = -1
    '            If oldIndex <= lstboxUsers.Items.Count - 1 Then
    '                lstboxUsers.Invalidate(lstboxUsers.GetItemRectangle(oldIndex))
    '            End If
    '        End If
    '        mouseIndex = index
    '        If mouseIndex > -1 Then
    '            lstboxUsers.Invalidate(lstboxUsers.GetItemRectangle(mouseIndex))
    '        End If
    '    End If

    '    If mouseIndex > -1 Then
    '        lstboxUsers.SelectedIndex = mouseIndex
    '        lstboxUsers_SelectedIndexChanged(sender, e)
    '    End If
    'End Sub

    Private Sub txtBoxPassword_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBoxPassword.MouseEnter
        txtBoxPassword.BackColor = Color.Bisque
        txtBoxPassword.Focus()
    End Sub

    Private Sub txtBoxPassword_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBoxPassword.MouseLeave
        txtBoxPassword.BackColor = Color.LemonChiffon
    End Sub

    Private Sub lstboxUsers_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstboxUsers.MouseEnter
        lstboxUsers.BackColor = Color.Bisque
        lstboxUsers.Focus()
    End Sub

    Private Sub lstboxUsers_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstboxUsers.MouseLeave
        lstboxUsers.BackColor = Color.LemonChiffon
    End Sub
End Class
