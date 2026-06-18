Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Data.SQLite
'Imports Microsoft.Office.Interop.Excel
Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types


Public Class frmLogin
    Private Sub FrmLogin_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        If SqlLite Then
            LogoutUser(username)
        Else
            If (OpenConn()) Then
                LogoutUser(username)
                CloseConn()
            End If
        End If

    End Sub

    Private Sub FrmLogin_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        GetParams()
        If (Not OpenConn()) Then
            Me.Dispose()
        End If

        'If C:/sqlite.txt exists, create the datbase and set the flag to use sqlite instead of oracle
        If SqlLiteEnabled() Then
            SqlLite = True
            Dim sync As New SyncTables()
            Try
                CreateSqliteTableStructure()
                sync.SyncGlobalParams()
                sync.SyncUsers()
                sync.SyncLottery()
                sync.SyncVatTypes()
                sync.SyncSuppliers()
                sync.SyncCategories()
                sync.SyncProducts()
                sync.SyncBarcodes()
                'drop primary key once all references to product serno removed (other tables)

                StartSyncService()

            Catch ex As Exception
                MessageBox.Show(ex.Message)
                ' Oracle unavailable
                ' Continue with local SQLite
            End Try

        End If

        If computerName = "-1" Then
            Me.Dispose()
        End If
        IsPasswordEncrypted()
        GetKIOSKParams()
        FillUsersList()
        lblLoginTitle1.Text = LOGIN_TITLE1
        lblLoginTitle2.Text = LOGIN_TITLE2
    End Sub

    Private Sub FillUsersList()
        Dim WhoAmI As String = "FillUsersList"
        Dim sql As String = "SELECT username FROM users WHERE deleted = 0 "
        Try
            If SqlLite Then
                sql += "AND KIOSKID = :KIOSKID "
            End If
            sql += "ORDER BY username"

            If SqlLite Then
                Using conn As New SQLiteConnection("Data Source=kiosk.db")
                    conn.Open()
                    Using cmd As New SQLiteCommand(sql, conn)
                        cmd.Parameters.AddWithValue(":KIOSKID", kioskId)
                        Using dr As SQLiteDataReader = cmd.ExecuteReader()
                            lstboxUsers.BeginUpdate()
                            lstboxUsers.Items.Clear()

                            While dr.Read()
                                lstboxUsers.Items.Add(dr.GetString(dr.GetOrdinal("username")))
                            End While
                            lstboxUsers.EndUpdate()
                        End Using
                    End Using
                End Using
            Else
                Using cmd As New OracleCommand(sql, conn)
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
            End If
        Catch ex As Exception
            CreateExceptionFile(WhoAmI + " " + ex.Message, " " & sql)
            MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BtnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        '15/06 - TODO
        CloseConn()
        Me.Dispose()
    End Sub

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

    Private Sub BtnLogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogin.Click
        '15/06 - TODO
        Dim WhoAmI As String = "BtnLogin_Click"

        'If Not CheckHhdSerial() Then
        'Exit Sub
        'End If

        '3% VAT is already part of the tables, no need to execute
        'GetVat3PercentColumns()

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
            'Dim cmd As New OracleCommand
            Try
                If SqlLite Then
                    sql =
                        "SELECT " &
                        "UUID, " &
                        "ACCESS_LEVEL, " &
                        "IFNULL(IS_UNLOCK,0) IS_UNLOCK, " &
                        "IFNULL(VIEW_REPORTS,0) VIEW_REPORTS, " &
                        "IFNULL(EDIT_PROD,0) EDIT_PROD, " &
                        "IFNULL(EDIT_PROD_FULL,0) EDIT_PROD_FULL " &
                        "FROM USERS " &
                        "WHERE KIOSKID = :KIOSKID " &
                        "AND USERNAME = :USERNAME " &
                        "AND PASS = :PASS"
                Else
                    sql =
                        "SELECT " &
                        "UUID, " &
                        "ACCESS_LEVEL, " &
                        "NVL(IS_UNLOCK,0), " &
                        "NVL(VIEW_REPORTS,0), " &
                        "NVL(EDIT_PROD,0), " &
                        "NVL(EDIT_PROD_FULL,0) " &
                        "FROM USERS " &
                        "WHERE USERNAME = :USERNAME " &
                        "AND PASS = :PASS"
                End If

                Dim isUnlock As Boolean = False
                If SqlLite Then
                    Using conn As New SQLiteConnection("Data Source=kiosk.db")
                        conn.Open()

                        Using cmd As New SQLiteCommand(sql, conn)
                            cmd.Parameters.AddWithValue(":KIOSKID", kioskId)
                            cmd.Parameters.AddWithValue(":USERNAME", username)
                            cmd.Parameters.AddWithValue(":PASS", getEncryptedValue(txtBoxPassword.Text))

                            Using drLogged As SQLiteDataReader = cmd.ExecuteReader()
                                If drLogged.Read() Then
                                    canViewReports = Convert.ToInt32(drLogged("VIEW_REPORTS")) = 1
                                    canEditProducts = Convert.ToInt32(drLogged("EDIT_PROD")) = 1
                                    canEditProductsFull = Convert.ToInt32(drLogged("EDIT_PROD_FULL")) = 1
                                    isAdmin = Convert.ToInt32(drLogged("ACCESS_LEVEL")) = 1
                                    isUnlock = Convert.ToInt32(drLogged("IS_UNLOCK")) = 1
                                    whois = drLogged("UUID").ToString()

                                    Me.Hide()
                                    txtBoxPassword.Clear()

                                    If isUnlock Then
                                        frmUnlockUser.Show()
                                    Else
                                        If Not isAdmin Then
                                            frmPOS.Show()
                                        Else
                                            frmMain.Show()
                                        End If

                                        If dualMonitor Then
                                            frmDual.Show()
                                        End If
                                    End If
                                Else
                                    MessageBox.Show(INVALID_LOGIN, ERROR_MSG, MessageBoxButtons.OK, MessageBoxIcon.Error)
                                    Exit Sub
                                End If
                            End Using
                        End Using
                    End Using
                Else
                    Dim cmd = New OracleCommand(sql, conn)

                    Dim userNameparam As New OracleParameter
                    userNameparam.OracleDbType = OracleDbType.Varchar2
                    userNameparam.Value = username

                    Dim passparam As New OracleParameter
                    passparam.OracleDbType = OracleDbType.Varchar2
                    passparam.Value = getEncryptedValue(txtBoxPassword.Text)

                    cmd.Parameters.Add(userNameparam)
                    cmd.Parameters.Add(passparam)

                    cmd.CommandType = CommandType.Text

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
                            Else
                                If Not isAdmin Then
                                    frmPOS.Show()
                                Else
                                    frmMain.Show()
                                End If

                                If dualMonitor Then
                                    frmDual.Show()
                                End If
                            End If
                        Else
                            MessageBox.Show(INVALID_LOGIN, ERROR_MSG, MessageBoxButtons.OK, MessageBoxIcon.Error)
                            drLogged.Close()
                            cmd.Dispose()
                            Exit Sub
                        End If
                        cmd.Dispose()
                    End Using
                End If


                If Not isUnlock Then
                    Dim amountLaxeia As Double = 0
                    amountLaxeia = GetAmountLaxeia()

                    Dim computerNameparam As New OracleParameter
                    computerNameparam.OracleDbType = OracleDbType.Varchar2
                    computerNameparam.Value = computerName

                    Dim userNameInsert As New OracleParameter
                    userNameInsert.OracleDbType = OracleDbType.Varchar2
                    userNameInsert.Value = username

                    Dim amountLaxeiaParam As New OracleParameter
                    amountLaxeiaParam.OracleDbType = OracleDbType.Decimal
                    amountLaxeiaParam.Value = amountLaxeia

                    If SqlLite Then
                        sql =
                            "INSERT INTO SESSIONS
                            (
                                UUID,
                                LOGIN_WHEN,
                                IS_ACTIVE,
                                MACHINE_NAME,
                                USER_ID,
                                AMOUNTLAXEIAONLOGIN,
                                KIOSKID
                            )
                            VALUES
                            (
                                @UUID,
                                CURRENT_TIMESTAMP,
                                1,
                                @MACHINE_NAME,
                                (
                                    SELECT UUID
                                    FROM USERS
                                    WHERE USERNAME = @USERNAME
                                    AND KIOSKID = @KIOSKID
                                ),
                                @AMOUNT,
                                @KIOSKID
                            )"

                        Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                            sqliteConn.Open()
                            Using cmd As New SQLiteCommand(sql, sqliteConn)
                                cmd.Parameters.AddWithValue("@UUID", Guid.NewGuid().ToString("N").ToUpper())
                                cmd.Parameters.AddWithValue("@MACHINE_NAME", computerName)
                                cmd.Parameters.AddWithValue("@USERNAME", username)
                                cmd.Parameters.AddWithValue("@AMOUNT", amountLaxeia)
                                cmd.Parameters.AddWithValue("@KIOSKID", kioskId)
                                cmd.ExecuteNonQuery()
                            End Using
                        End Using
                    Else
                        sql = "insert into sessions
                               (uuid, login_when, is_active, machine_name, user_id, amountLaxeiaOnLogin)
                               values
                               (sys_guid(),
                                (Select systimestamp from dual),
                                1,
                                :1,
                                (select uuid from users where username = :2),
                                :3)"

                        Dim cmd = New OracleCommand(sql, conn)
                        cmd.Parameters.Add(computerNameparam)
                        cmd.Parameters.Add(userNameInsert)
                        cmd.Parameters.Add(amountLaxeiaParam)
                        cmd.CommandType = CommandType.Text
                        cmd.ExecuteNonQuery()
                    End If
                End If
            Catch ex As Exception
                CreateExceptionFile(WhoAmI + " " + ex.Message, " " & sql)
                MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
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
