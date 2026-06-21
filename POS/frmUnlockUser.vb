Imports System.Data.SQLite
Imports Oracle.DataAccess.Client

Public Class frmUnlockUser

    Private Sub BtnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Dispose()
    End Sub

    Private Sub FrmUnlockUser_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        frmLogin.Show()
    End Sub

    Private Sub FrmUnlockUser_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub FillLists()
        Dim WhoAmI As String = "frmUnlockUser.FillLists"
        Dim sql As String = ""

        lstBoxLockedUsers.Items.Clear()
        lstBoxUUIDS.Items.Clear()

        Try
            If SqlLite Then
                Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                    sqliteConn.Open()

                    sql =
                        "SELECT s.user_id, u.username " &
                        "FROM sessions s " &
                        "INNER JOIN users u ON u.uuid = s.user_id " &
                        "WHERE s.is_active = 1 " &
                        "AND s.kioskid = @KIOSKID"

                    Using cmd As New SQLiteCommand(sql, sqliteConn)
                        cmd.Parameters.AddWithValue("@KIOSKID", kioskId)

                        Using dr As SQLiteDataReader = cmd.ExecuteReader()
                            While dr.Read()
                                lstBoxLockedUsers.Items.Add(dr("username").ToString())
                                lstBoxUUIDS.Items.Add(dr("user_id").ToString())
                            End While
                        End Using
                    End Using
                End Using
            Else
                sql =
                    "select user_id, username " &
                    "from sessions s " &
                    "inner join users u on u.uuid = s.user_id " &
                    "where is_active = 1"

                Using cmd As New OracleCommand(sql, conn)
                    cmd.CommandType = CommandType.Text

                    Using dr As OracleDataReader = cmd.ExecuteReader()
                        While dr.Read()
                            lstBoxLockedUsers.Items.Add(dr("username").ToString())
                            lstBoxUUIDS.Items.Add(dr("user_id").ToString())
                        End While
                    End Using
                End Using
            End If
        Catch ex As Exception
            CreateExceptionFile(WhoAmI & " " & ex.Message, sql)
            MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FrmUnlockUser_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        FillLists()
    End Sub

    Private Sub BtnUnlock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUnlock.Click
        Dim WhoAmI As String = "frmUnlockUser.BtnUnlock_Click"
        Dim sql As String = ""

        If lstBoxLockedUsers.SelectedIndex = -1 Then Exit Sub

        Try
            ' Print X Report
            GenerateXreport(lstBoxUUIDS.Items(lstBoxLockedUsers.SelectedIndex).ToString())

            PrintDocument1.PrinterSettings.Copies = 1
            PrintDocument1.Print()

            If SqlLite Then
                Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                    sqliteConn.Open()

                    sql =
                        "UPDATE SESSIONS " &
                        "SET IS_ACTIVE = 0, " &
                        "    LOGOUT_WHEN = CURRENT_TIMESTAMP, " &
                        "    UPDATED_AT = CURRENT_TIMESTAMP, " &
                        "    SYNC_STATUS = 1 " &
                        "WHERE USER_ID = @USER_ID " &
                        "AND IS_ACTIVE = 1 " &
                        "AND KIOSKID = @KIOSKID"

                    Using cmd As New SQLiteCommand(sql, sqliteConn)
                        cmd.Parameters.AddWithValue("@USER_ID",
                        lstBoxUUIDS.Items(lstBoxLockedUsers.SelectedIndex).ToString())
                        cmd.Parameters.AddWithValue("@KIOSKID", kioskId)
                        cmd.ExecuteNonQuery()
                    End Using
                End Using

                Try
                    Dim sync As New SyncTables()
                    sync.SyncSessions()
                Catch ex As Exception
                End Try
            Else

                sql =
                    "UPDATE SESSIONS " &
                    "SET IS_ACTIVE = 0, " &
                    "    LOGOUT_WHEN = SYSTIMESTAMP " &
                    "WHERE USER_ID = :USER_ID " &
                    "AND IS_ACTIVE = 1"

                Using cmd As New OracleCommand(sql, conn)
                    cmd.CommandType = CommandType.Text
                    cmd.Parameters.Add("USER_ID", OracleDbType.Varchar2).Value =
                    lstBoxUUIDS.Items(lstBoxLockedUsers.SelectedIndex).ToString()
                    cmd.ExecuteNonQuery()
                End Using
            End If
            FillLists()
        Catch ex As Exception
            CreateExceptionFile(ex.Message, sql)
            MessageBox.Show(WhoAmI + " " + ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub PrintDocument1_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        Dim WhoAmI As String = "frmUnlockUser.PrintDocument1_PrintPage"
        Dim headerFont As Font = New Drawing.Font(REPORT_FONT, 15, FontStyle.Bold)
        Dim reportFont As Font = New Drawing.Font(REPORT_FONT, 9)
        Dim reportFontSmall As Font = New Drawing.Font(REPORT_FONT, 9)

        e.Graphics.DrawString(KIOSK_NAME, headerFont, Brushes.Black, 65, 0)
        e.Graphics.DrawString(COMPANY_NAME, reportFont, Brushes.Black, 95, 35)
        e.Graphics.DrawString(KIOSK_ADDRESS1, reportFont, Brushes.Black, 75, 50)
        e.Graphics.DrawString(KIOSK_ADDRESS2, reportFont, Brushes.Black, 95, 65)
        e.Graphics.DrawString(COMPANY_VAT, reportFont, Brushes.Black, 60, 80)

        e.Graphics.DrawString(SINGE_DASHED_LINE, reportFont, Brushes.Black, 0, 95)
        e.Graphics.DrawString("Χρήστης: " & GetUserByUsername(lstBoxLockedUsers.Text),
                          reportFont, Brushes.Black, 0, 110)

        Dim sql As String = ""

        Try
            If SqlLite Then
                Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                    sqliteConn.Open()

                    sql =
                        "SELECT from_date,
                                to_date,
                                total_receipts,
                                total5percent,
                                total19percent,
                                payments,
                                initial_amt,
                                final_amt,
                                description,
                                total0percent,
                                amount_laxeia,
                                initialAmtLaxeia,
                                amountVisa,
                                total3percent
                            FROM x_report
                            WHERE user_id = @USER_ID
                            AND kioskid = @KIOSKID
                            ORDER BY created_on DESC
                            LIMIT 1"

                    Using cmd As New SQLiteCommand(sql, sqliteConn)

                        cmd.Parameters.AddWithValue("@USER_ID",
                        lstBoxUUIDS.Items(lstBoxLockedUsers.SelectedIndex).ToString())
                        cmd.Parameters.AddWithValue("@KIOSKID", kioskId)

                        Using dr As SQLiteDataReader = cmd.ExecuteReader()
                            If dr.Read() Then
                                Dim xmargin As Integer = 150
                                e.Graphics.DrawString("Από: " & dr("from_date").ToString(), reportFont, Brushes.Black, 0, xmargin)
                                xmargin += 20
                                e.Graphics.DrawString("Έως: " & dr("to_date").ToString(), reportFont, Brushes.Black, 0, xmargin)

                                Dim totalVat5 As Double = CDbl(dr("total5percent"))
                                Dim totalVat19 As Double = CDbl(dr("total19percent"))
                                Dim payments As Double = CDbl(dr("payments"))
                                Dim initial As Double = CDbl(dr("initial_amt"))
                                Dim finalAmt As Double = CDbl(dr("final_amt"))
                                Dim totalVat0 As Double = CDbl(dr("total0percent"))
                                Dim amountLaxeia As Double = CDbl(dr("amount_laxeia"))
                                Dim initialAmountLaxeia As Double = CDbl(dr("initialAmtLaxeia"))
                                Dim amountVisa As Double = CDbl(dr("amountVisa"))
                                Dim totalVat3 As Double = CDbl(dr("total3percent"))

                                Dim totalReceivedAmt As Double = totalVat0 + totalVat3 + totalVat5 + totalVat19

                                Dim totalAmountToDeliver As Double = (totalReceivedAmt + initial) - payments - amountVisa

                                xmargin += 20
                                e.Graphics.DrawString("Αρχικό Ποσό: " & initial.ToString("N2"), reportFont, Brushes.Black, 0, xmargin)
                                xmargin += 40
                                e.Graphics.DrawString("Ποσο λαχείων για Παράδωση: " & (initialAmountLaxeia - amountLaxeia).ToString("N2"), reportFont, Brushes.Black, 0, xmargin)
                            End If
                        End Using
                    End Using
                End Using
            Else
                sql =
                    "select from_date,
                            to_date,
                            total_receipts,
                            total5percent,
                            total19percent,
                            payments,
                            initial_amt,
                            final_amt,
                            description,
                            total0percent,
                            amount_laxeia,
                            initialAmtLaxeia,
                            amountVisa,
                            total3percent
                     from x_report
                     where user_id = :1
                     and created_on =
                        (select max(created_on)
                           from x_report)"

                Using cmd As New OracleCommand(sql, conn)

                    Dim userIdParam As New OracleParameter
                    userIdParam.OracleDbType = OracleDbType.Varchar2
                    userIdParam.Value =
                    lstBoxUUIDS.Items(lstBoxLockedUsers.SelectedIndex).ToString()

                    cmd.Parameters.Add(userIdParam)

                    Using dr As OracleDataReader = cmd.ExecuteReader()
                        If dr.Read() Then
                            Dim xmargin As Integer = 150
                            e.Graphics.DrawString("Από: " & CStr(dr(0)), reportFont, Brushes.Black, 0, xmargin)
                            xmargin += 20
                            e.Graphics.DrawString("Έως: " & CStr(dr(1)), reportFont, Brushes.Black, 0, xmargin)

                            Dim totalVat5 As Double = CDbl(dr(3))
                            Dim totalVat19 As Double = CDbl(dr(4))
                            Dim payments As Double = CDbl(dr(5))
                            Dim initial As Double = CDbl(dr(6))
                            Dim finalAmt As Double = CDbl(dr(7))
                            Dim totalVat0 As Double = CDbl(dr(9))
                            Dim amountLaxeia As Double = CDbl(dr(10))
                            Dim initialAmountLaxeia As Double = CDbl(dr(11))
                            Dim amountVisa As Double = CDbl(dr(12))
                            Dim totalVat3 As Double = CDbl(dr(13))

                            Dim totalReceivedAmt As Double = totalVat0 + totalVat3 + totalVat5 + totalVat19
                            Dim totalAmountToDeliver As Double = (totalReceivedAmt + initial) - payments - amountVisa
                            xmargin += 20
                            e.Graphics.DrawString("Αρχικό Ποσό: " & initial.ToString("N2"), reportFont, Brushes.Black, 0, xmargin)
                            xmargin += 40
                            e.Graphics.DrawString("Ποσο λαχείων για Παράδωση: " & (initialAmountLaxeia - amountLaxeia).ToString("N2"), reportFont, Brushes.Black, 0, xmargin)
                        End If
                    End Using
                End Using
            End If
        Catch ex As Exception
            CreateExceptionFile(WhoAmI + " " + ex.Message, sql)
            MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Protected Overrides ReadOnly Property CreateParams() As CreateParams
        'Disables X button
        Get
            Dim param As CreateParams = MyBase.CreateParams
            param.ClassStyle = param.ClassStyle Or &H200
            Return param
        End Get
    End Property
End Class