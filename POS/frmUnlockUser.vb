Imports Oracle.DataAccess.Client

Public Class frmUnlockUser

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Dispose()
    End Sub

    Private Sub frmUnlockUser_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        frmLogin.Show()
    End Sub

    Private Sub frmUnlockUser_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub fillLists()
        Dim cmd As New OracleCommand("", conn)
        Dim dr As OracleDataReader
        Try
            cmd = New OracleCommand(GET_ACTIVE_USERS, conn)
            cmd.CommandType = CommandType.Text
            dr = cmd.ExecuteReader()
            lstBoxLockedUsers.Items.Clear()
            lstBoxUUIDS.Items.Clear()
            While dr.Read()
                lstBoxLockedUsers.Items.Add(dr("username"))
                lstBoxUUIDS.Items.Add(dr("user_id"))
            End While
            dr.Close()
        Catch ex As Exception
            createExceptionFile(ex.Message, " " & GET_ACTIVE_USERS)
            MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            cmd.Dispose()
        End Try
    End Sub

    Private Sub frmUnlockUser_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fillLists()
    End Sub

    Private Sub btnUnlock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUnlock.Click
        If lstBoxLockedUsers.SelectedIndex = -1 Then
            Exit Sub
        End If

        Dim cmd As New OracleCommand("", conn)
        Try
            generateXreport(lstBoxUUIDS.Items.Item(lstBoxLockedUsers.SelectedIndex).ToString())
            PrintDocument1.PrinterSettings.Copies = 1
            PrintDocument1.Print()

            cmd = New OracleCommand(UNLOCK_USER, conn)
            Dim userIdparam As New OracleParameter
            userIdparam.OracleDbType = OracleDbType.Varchar2
            userIdparam.Value = lstBoxUUIDS.Items.Item(lstBoxLockedUsers.SelectedIndex).ToString()
            cmd.Parameters.Add(userIdparam)
            cmd.CommandType = CommandType.Text
            cmd.ExecuteReader()

            fillLists()
        Catch ex As Exception
            createExceptionFile(ex.Message, " " & UNLOCK_USER)
            MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            cmd.Dispose()
        End Try
    End Sub

    Private Sub PrintDocument1_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        Dim headerFont As Font = New Drawing.Font(REPORT_FONT, 15, FontStyle.Bold)
        Dim reportFont As Font = New Drawing.Font(REPORT_FONT, 9)
        Dim reportFontSmall As Font = New Drawing.Font(REPORT_FONT, 9)

        e.Graphics.DrawString(KIOSK_NAME, headerFont, Brushes.Black, 65, 0)
        e.Graphics.DrawString(COMPANY_NAME, reportFont, Brushes.Black, 95, 35)
        e.Graphics.DrawString(KIOSK_ADDRESS1, reportFont, Brushes.Black, 75, 50)
        e.Graphics.DrawString(KIOSK_ADDRESS2, reportFont, Brushes.Black, 95, 65)
        e.Graphics.DrawString(COMPANY_VAT, reportFont, Brushes.Black, 60, 80)

        e.Graphics.DrawString(SINGE_DASHED_LINE, reportFont, Brushes.Black, 0, 95)
        e.Graphics.DrawString("Χρήστης: " & getUserByUsername(lstBoxLockedUsers.Text), reportFont, Brushes.Black, 0, 110)

        Dim cmd As New OracleCommand("", conn)
        Dim dr As OracleDataReader

        Try
            cmd = New OracleCommand(GET_TOTALS_FOR_X_REPORT, conn)
            Dim userIdparam As New OracleParameter
            userIdparam.OracleDbType = OracleDbType.Varchar2
            userIdparam.Value = lstBoxUUIDS.Items.Item(lstBoxLockedUsers.SelectedIndex)
            cmd.Parameters.Add(userIdparam)
            dr = cmd.ExecuteReader()
            If dr.Read Then
                Dim xmargin As Integer = 150

                e.Graphics.DrawString("Από: " & CStr(dr(0)), reportFont, Brushes.Black, 0, xmargin)
                xmargin += 20
                e.Graphics.DrawString("Έως: " & CStr(dr(1)), reportFont, Brushes.Black, 0, xmargin)
                'xmargin += 20
                'e.Graphics.DrawString("Αρ. Αποδείξεων: " & CStr(dr(2)), reportFont, Brushes.Black, 0, xmargin)

                Dim totalVat5 As Double = CDbl(dr(3))
                Dim totalVat19 As Double = CDbl(dr(4))
                Dim payments As Double = CDbl(dr(5))
                Dim initial As Double = CDbl(dr(6))
                Dim final As Double = CDbl(dr(7))
                Dim totalVat0 As Double = CDbl(dr(9))
                Dim amountLaxeia As Double = CDbl(dr(10))
                Dim initialAmountLaxeia As Double = CDbl(dr(11))
                Dim amountVisa As Double = CDbl(dr(12))

                Dim totalReceivedAmt As Double = totalVat0 + totalVat5 + totalVat19
                Dim totalAmountToDeliver = (totalReceivedAmt + initial) - payments - amountVisa

                xmargin += 20
                'e.Graphics.DrawString("Φ.Π.Α. 0%: " & totalVat0.ToString("N2"), reportFont, Brushes.Black, 0, xmargin)
                'xmargin += 20
                'e.Graphics.DrawString("Φ.Π.Α. 5%: " & totalVat5.ToString("N2"), reportFont, Brushes.Black, 0, xmargin)
                'xmargin += 20
                'e.Graphics.DrawString("Φ.Π.Α. 19%: " & totalVat19.ToString("N2"), reportFont, Brushes.Black, 0, xmargin)
                'xmargin += 20
                'e.Graphics.DrawString("Πληρωμές: " & payments.ToString("N2"), reportFont, Brushes.Black, 0, xmargin)
                'xmargin += 20
                e.Graphics.DrawString("Αρχικό Ποσό: " & initial.ToString("N2"), reportFont, Brushes.Black, 0, xmargin)
                xmargin += 20
                'e.Graphics.DrawString("Ποσό Είσπραξης: " & totalReceivedAmt.ToString("N2"), reportFont, Brushes.Black, 0, xmargin)
                'xmargin += 20
                'e.Graphics.DrawString("Ποσό VISA: " & amountVisa.ToString("N2"), reportFont, Brushes.Black, 0, xmargin)
                'xmargin += 20
                'e.Graphics.DrawString("Τελικό Ποσό Ταμείου για Παράδωση: " & totalAmountToDeliver.ToString("N2"), reportFont, Brushes.Black, 0, xmargin)
                xmargin += 20
                e.Graphics.DrawString("Ποσο λαχείων για Παράδωση: " & (initialAmountLaxeia - amountLaxeia).ToString("N2"), reportFont, Brushes.Black, 0, xmargin)

                'Dim reportFontUnderline As Font = New Drawing.Font(REPORT_FONT, 9, FontStyle.Underline)
                'xmargin += 20
                'e.Graphics.DrawString("Αναλυτική Κατάσταση: ", reportFontUnderline, Brushes.Black, 0, xmargin)
                'Dim salesDescription = ""
                'If Not dr.IsDBNull(8) Then
                'salesDescription = CStr(dr(8))
                'End If
                'xmargin += 20
                'e.Graphics.DrawString(salesDescription, reportFont, Brushes.Black, 0, xmargin)
            End If
            dr.Close()
        Catch ex As Exception
            createExceptionFile(ex.Message, " " & GET_TOTALS_FOR_X_REPORT)
            MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            cmd.Dispose()
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