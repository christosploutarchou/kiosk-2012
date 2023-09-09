Imports Oracle.DataAccess.Client

Public Class frmReceipts
    Dim userId As String
    Dim userUUIDs As ArrayList = New ArrayList()

    Private Sub frmReceipts_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtBoxFromTimeH.Text = "00"
        txtBoxFromTimeM.Text = "00"

        txtBoxToTimeH.Text = "23"
        txtBoxToTimeM.Text = "59"

        fillUsers()
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Dispose()
    End Sub

    Private Sub frmReceipts_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        frmMain.Show()
    End Sub

    Private Sub frmReceipts_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub rdbReceiptNum_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'txtReceiptNumber.Clear()
        'txtReceiptNumber.Visible = True
        'btnClearReceiptNumber.Visible = True
        'btnSearch.Visible = True

        'lblFromDate.Visible = False
        lblToDate.Visible = False
        dtpFrom.Visible = False
        dtpTo.Visible = False
    End Sub

    Private Sub rdbDates_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'txtReceiptNumber.Clear()
        'txtReceiptNumber.Visible = False
        'btnClearReceiptNumber.Visible = False

        'lblFromDate.Visible = True
        lblToDate.Visible = True
        dtpFrom.Visible = True
        dtpTo.Visible = True
        btnSearch.Visible = True
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim sql As String = ""
        Dim cmd As New OracleCommand("", conn)
        Dim dr As OracleDataReader
        Dim totalAmt As Double = 0

        If txtBoxFromTimeH.Text = String.Empty Or txtBoxFromTimeH.Text = String.Empty Or txtBoxToTimeH.Text = String.Empty Or txtBoxToTimeM.Text = String.Empty Then
            MessageBox.Show("Δεν έχετε επιλέξει ώρες αναζήτησης", "Αναζήτηση", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If Not IsNumeric(txtBoxFromTimeH.Text) Or Not IsNumeric(txtBoxFromTimeH.Text) Or Not IsNumeric(txtBoxToTimeH.Text) Or Not IsNumeric(txtBoxToTimeM.Text) Then
            MessageBox.Show("Οι ώρες (00 έως 23) και τα λεπτά (00 έως 59) αναζήτησης πρέπει να είναι αριθμοί ", "Αναζήτηση", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If CInt(txtBoxFromTimeH.Text) < 0 Or CInt(txtBoxFromTimeH.Text) > 23 Or CInt(txtBoxToTimeH.Text) < 0 Or CInt(txtBoxToTimeH.Text) > 23 Then
            MessageBox.Show("Οι ώρες αναζήτησης πρέπει να είναι μεταξύ 00 και 23", "Αναζήτηση", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If CInt(txtBoxFromTimeM.Text) < 0 Or CInt(txtBoxFromTimeM.Text) > 23 Or CInt(txtBoxToTimeM.Text) < 0 Or CInt(txtBoxToTimeM.Text) > 59 Then
            MessageBox.Show("Τα λεπτά αναζήτησης πρέπει να είναι μεταξύ 00 και 59", "Αναζήτηση", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Try
            lstBoxReceipts.Items.Clear()
            lstBoxSerno.Items.Clear()

            Dim dateFrom As String = CStr(dtpFrom.Value.Day) & "-" & findMonth(CStr(dtpFrom.Value.Month)) & "-" & CStr(dtpFrom.Value.Year).Substring(2, 2)
            Dim dateTo As String = CStr(dtpTo.Value.Day) & "-" & findMonth(CStr(dtpTo.Value.Month)) & "-" & CStr(dtpTo.Value.Year).Substring(2, 2)

            If Not chkBoxWithDisc.Checked And Not chkBoxWithRet.Checked Then
                'All receipts
                sql = "select created_on, serno, total_amt_with_disc from receipts r " & _
                      "where created_on BETWEEN " & _
                      "to_timestamp('" & dateFrom & " " & txtBoxFromTimeH.Text & ":" & txtBoxFromTimeM.Text & ":00', 'DD-MON-YY HH24:MI:SS') AND " & _
                      "to_timestamp('" & dateTo & " " & txtBoxToTimeH.Text & ":" & txtBoxToTimeM.Text & ":59', 'DD-MON-YY HH24:MI:SS') "
            ElseIf chkBoxWithDisc.Checked And chkBoxWithRet.Checked Then
                'Both discount and returns
                sql = "select distinct r.created_on, r.serno, total_amt_with_disc from receipts r " & _
                      "inner join receipts_det rd on r.serno = rd.receipt_serno " & _
                      "where total_discount > 0 and rd.amount < 0 and r.created_on BETWEEN " & _
                      "to_timestamp('" & dateFrom & " " & txtBoxFromTimeH.Text & ":" & txtBoxFromTimeM.Text & ":00', 'DD-MON-YY HH24:MI:SS') AND " & _
                      "to_timestamp('" & dateTo & " " & txtBoxToTimeH.Text & ":" & txtBoxToTimeM.Text & ":59', 'DD-MON-YY HH24:MI:SS') "
            ElseIf chkBoxWithDisc.Checked And Not chkBoxWithRet.Checked Then
                'Having discount
                sql = "select created_on, serno, total_amt_with_disc from receipts r " & _
                      "where total_discount > 0 and created_on BETWEEN " & _
                      "to_timestamp('" & dateFrom & " " & txtBoxFromTimeH.Text & ":" & txtBoxFromTimeM.Text & ":00', 'DD-MON-YY HH24:MI:SS') AND " & _
                      "to_timestamp('" & dateTo & " " & txtBoxToTimeH.Text & ":" & txtBoxToTimeM.Text & ":59', 'DD-MON-YY HH24:MI:SS') "
            ElseIf Not chkBoxWithDisc.Checked And chkBoxWithRet.Checked Then
                'Having returns
                sql = "select distinct r.created_on, serno, total_amt_with_disc from receipts r " & _
                      "inner join receipts_det rd on r.SERNO = rd.RECEIPT_SERNO " & _
                      "where rd.amount < 0 and r.created_on BETWEEN " & _
                      "to_timestamp('" & dateFrom & " " & txtBoxFromTimeH.Text & ":" & txtBoxFromTimeM.Text & ":00', 'DD-MON-YY HH24:MI:SS') AND " & _
                      "to_timestamp('" & dateTo & " " & txtBoxToTimeH.Text & ":" & txtBoxToTimeM.Text & ":59', 'DD-MON-YY HH24:MI:SS') "
            End If

            If cmbUsers.SelectedIndex <> -1 Then
                If Not cmbUsers.SelectedItem.Equals("Όλοι") Then
                    sql += " and created_by = '" & userUUIDs(cmbUsers.SelectedIndex) & "' "
                End If
            End If

            If chkBoxCash.Checked And Not chkBoxVISA.Checked Then
                sql += " and payment_type = 'C' "
            ElseIf Not chkBoxCash.Checked And chkBoxVISA.Checked Then
                sql += " and payment_type = 'V' "
            End If

            sql += " order by r.created_on"

            cmd = New OracleCommand(sql, conn)
            dr = cmd.ExecuteReader()
            Dim counter = 0
            While dr.Read()
                counter += 1
                lstBoxReceipts.Items.Add(CStr(dr(0)))
                lstBoxSerno.Items.Add(CInt(dr(1)))
                totalAmt += CDbl(dr(2))
            End While

            lblTotalReceipts.Text = "Αποδείξεις:  " & counter
            lblTotalRecAmt.Text = "Ποσό (€):  " & totalAmt.ToString("N2")

        Catch ex As Exception
            createExceptionFile(ex.Message, " " & sql)
            MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            cmd.Dispose()
        End Try
    End Sub

    Private Sub lstBoxReceipts_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstBoxReceipts.SelectedIndexChanged
        If lstBoxReceipts.SelectedIndex = -1 Then
            MessageBox.Show("Δεν έχετε επιλέξει απόδειξη για προβολή", "Προβολή Απόδειξης", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim cmd As New OracleCommand("", conn)
        Dim dr As OracleDataReader
        Dim sql As String = ""
        Try
            Dim index As Integer = lstBoxReceipts.SelectedIndex
            If index < 0 Then
                Exit Sub
            End If

            sql = "select r.serno, r.created_on, r.payment_type, u.username, r.total_amt, r.total_discount, r.total_amt_with_disc, " & _
                  "r.payment_amt, r.return_amt, r.total_vat19, r.total_vat5, p.description, rd.quantity, NVL(p.sell_amt,0) as sell_amt, " & _
                  "NVL(rd.amount,0) as sell_amt1, rd.amount rdamount, rd.vat rdvat, r.total_vat0, NVL(r.total_vat3,0), " & _
                  "from receipts r " & _
                  "inner join users u on u.uuid = r.created_by " & _
                  "inner join receipts_det rd on r.serno = rd.receipt_serno " & _
                  "inner join products p on p.serno = rd.product_serno " & _
                  "where (r.serno = " & lstBoxSerno.Items.Item(index) & ")"

            Dim serno As Integer = 0
            Dim createdOn As String = ""
            Dim paymentType As String = ""
            Dim cashier As String = ""
            Dim totalAmt As Double = 0
            Dim totalDiscount As Double = 0
            Dim totalDiscountWithDiscount As Double = 0
            Dim payment As Double = 0
            Dim returnAmt As Double = 0
            Dim totalVAT19 As Double = 0
            Dim totalVAT5 As Double = 0
            Dim totalVAT0 As Double = 0
            Dim totalVAT3 As Double = 0
            Dim vat As Integer = 0

            Dim salesDescription As String = ""
            Dim quantity As Integer = 0
            Dim sellAmt As Double = 0
            Dim sellAmtPerUnit As Double = 0

            cmd = New OracleCommand(sql, conn)
            dr = cmd.ExecuteReader

            While dr.Read()
                serno = dr("serno")
                createdOn = CStr(dr("created_on"))
                paymentType = dr("payment_type")
                cashier = dr("username")
                totalAmt = CDbl(dr("total_amt"))
                quantity = CInt(dr("quantity"))
                sellAmt = CDbl(dr("rdamount"))
                vat = CInt(dr("rdvat"))

                sellAmtPerUnit = sellAmt / quantity

                salesDescription += dr("description") & vbCrLf & sellAmtPerUnit.ToString("N2") & " " & "x " & quantity & " ( " & vat & "% VAT) = " & vbTab & sellAmt.ToString("N2") & vbCrLf
                totalDiscount = CDbl(dr("total_discount"))
                totalDiscountWithDiscount = CDbl(dr("total_amt_with_disc"))
                payment = CDbl(dr("payment_amt"))
                returnAmt = CDbl(dr("return_amt"))
                totalVAT19 = CDbl(dr("total_vat19"))
                totalVAT5 = CDbl(dr("total_vat5"))
                totalVAT0 = CDbl(dr("total_vat0"))
                totalVAT3 = CDbl(dr("total_vat3"))
            End While
            dr.Close()

            txtBoxReceiptNum.Text = serno
            txtBoxCreatedOn.Text = createdOn

            If paymentType = "V" Then
                paymentType = "VISA"
            Else
                paymentType = "CASH"
            End If
            txtBoxPaymentType.Text = paymentType
            txtBoxCashier.Text = cashier
            txtBoxDescription.Text = salesDescription
            txtBoxTotalAmt.Text = totalAmt.ToString("N2")
            txtBoxTotalDiscount.Text = totalDiscount.ToString("N2")
            txtBoxTotalWithDiscount.Text = totalDiscountWithDiscount.ToString("N2")
            txtBoxPayment.Text = payment.ToString("N2")
            txtBoxReturnAmt.Text = returnAmt.ToString("N2")
            txtBoxTotal19.Text = totalVAT19.ToString("N2")
            txtBoxTotal5.Text = totalVAT5.ToString("N2")
            txtBoxTotal0.Text = totalVAT0.ToString("N2")
            txtBoxTotal3.Text = totalVAT3.ToString("N2")
        Catch ex As Exception
            createExceptionFile(ex.Message, " " & sql)
            MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            cmd.Dispose()
        End Try
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If txtBoxReceiptNum.Text = String.Empty Then
            Exit Sub
        End If
        PrintDocument1.PrinterSettings.Copies = 1
        PrintDocument1.Print()
    End Sub

    Private Sub PrintDocument1_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        Dim headerFont As Font = New Drawing.Font(REPORT_FONT, 15, FontStyle.Bold)
        Dim reportFont As Font = New Drawing.Font(REPORT_FONT, 9)

        e.Graphics.DrawString(KIOSK_NAME, headerFont, Brushes.Black, 65, 0)
        e.Graphics.DrawString(COMPANY_NAME, reportFont, Brushes.Black, 95, 35)
        e.Graphics.DrawString(KIOSK_ADDRESS1, reportFont, Brushes.Black, 75, 50)
        e.Graphics.DrawString(KIOSK_ADDRESS2, reportFont, Brushes.Black, 95, 65)
        e.Graphics.DrawString(COMPANY_VAT, reportFont, Brushes.Black, 60, 80)
        e.Graphics.DrawString(SINGE_DASHED_LINE, reportFont, Brushes.Black, 0, 95)
        e.Graphics.DrawString("Date: " & DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), reportFont, Brushes.Black, 0, 110)

        Dim xMargin As Integer = 130

        e.Graphics.DrawString("Ημερομηνία: " & txtBoxCreatedOn.Text, reportFont, Brushes.Black, 0, xMargin)
        xMargin += 20
        e.Graphics.DrawString("Τρόπος Πληρωμής: " & txtBoxPaymentType.Text, reportFont, Brushes.Black, 0, xMargin)

        xMargin += 20
        e.Graphics.DrawString("Ταμείας: " & txtBoxCashier.Text, reportFont, Brushes.Black, 0, xMargin)

        xMargin += 20
        e.Graphics.DrawString("Ολικό Ποσό: " & txtBoxTotalAmt.Text, reportFont, Brushes.Black, 0, xMargin)

        xMargin += 20
        e.Graphics.DrawString("Έκπτωση: " & txtBoxTotalDiscount.Text, reportFont, Brushes.Black, 0, xMargin)

        xMargin += 20
        e.Graphics.DrawString("Ολικό με Έκπτωση: " & txtBoxTotalWithDiscount.Text, reportFont, Brushes.Black, 0, xMargin)

        xMargin += 20
        e.Graphics.DrawString("Ποσό Πληρωμής: " & txtBoxPayment.Text, reportFont, Brushes.Black, 0, xMargin)

        xMargin += 20
        e.Graphics.DrawString("Ποσό Επιστροφής: " & txtBoxReturnAmt.Text, reportFont, Brushes.Black, 0, xMargin)

        xMargin += 20
        e.Graphics.DrawString("Ποσό Φ.Π.Α 19%: " & txtBoxTotal19.Text, reportFont, Brushes.Black, 0, xMargin)

        xMargin += 20
        e.Graphics.DrawString("Ποσό Φ.Π.Α 5%: " & txtBoxTotal5.Text, reportFont, Brushes.Black, 0, xMargin)

        xMargin += 20
        e.Graphics.DrawString("Ποσό Φ.Π.Α 0%: " & txtBoxTotal0.Text, reportFont, Brushes.Black, 0, xMargin)

        xMargin += 20
        e.Graphics.DrawString("-------------------------------------------", reportFont, Brushes.Black, 0, xMargin)

        xMargin += 20
        e.Graphics.DrawString("Αναλυτική Κατάσταση: " & vbNewLine & txtBoxDescription.Text, reportFont, Brushes.Black, 0, xMargin)
    End Sub

    Private Sub fillUsers()
        Dim cmd As New OracleCommand("", conn)
        Dim sql As String = ""
        Try
            sql = "select uuid, username " & _
                  "from users "

            If Not chkBoxDeletedUsers.Checked Then
                sql += " where deleted = 0"
            End If

            sql += " order by username asc"

            cmd = New OracleCommand(sql, conn)
            cmd.CommandType = CommandType.Text
            Dim dr As OracleDataReader = cmd.ExecuteReader()
            userId = ""
            userUUIDs.Clear()
            cmbUsers.Items.Clear()

            userUUIDs.Add(-1)
            cmbUsers.Items.Add("Όλοι")
            While dr.Read()
                userUUIDs.Add(dr("uuid"))
                cmbUsers.Items.Add(dr("username"))
            End While
            dr.Close()
        Catch ex As Exception
            createExceptionFile(ex.Message, Sql)
            MessageBox.Show(ex.Message, "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            cmd.Dispose()
        End Try
    End Sub

    'Private Sub tabCtrlReceipts_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabCtrlReceipts.SelectedIndexChanged
    '    If tabCtrlReceipts.SelectedIndex = 1 Then
    '        fillReceiptInfo()
    '        'lblKioskName.Text = "XDRIVE EXPRESS"
    '        'lblKioskName.Font = 

    '    End If

    'End Sub

    'Private Sub fillReceiptInfo()
    '    Dim cmd As New OracleCommand("", conn)
    '    Dim sql As String = ""
    '    Try
    '        sql = "select paramkey, paramvalue " & _
    '              "from global_params " & _
    '              "where paramkey in ('receipt.font', 'kiosk.name.size', 'kiosk.name', 'kiosk.header1', 'kiosk.header2', " & _
    '              "                   'kiosk.header3', 'kiosk.header4', 'receipt.font.size')"

    '        cmd = New OracleCommand(sql, conn)
    '        cmd.CommandType = CommandType.Text
    '        Dim dr As OracleDataReader = cmd.ExecuteReader()

    '        Dim receiptFont As String = ""
    '        Dim kioskNameSize As Single
    '        Dim receiptFontSize As Single

    '        Dim kioskName As String = ""

    '        Dim kioskHeader1 As String = ""
    '        Dim kioskHeader2 As String = ""
    '        Dim kioskHeader3 As String = ""
    '        Dim kioskHeader4 As String = ""

    '        While dr.Read()
    '            If dr("paramkey").Equals("receipt.font") Then
    '                receiptFont = dr("paramvalue")
    '            ElseIf dr("paramkey").Equals("kiosk.name.size") Then
    '                kioskNameSize = dr("paramvalue")
    '            ElseIf dr("paramkey").Equals("kiosk.name") Then
    '                kioskName = dr("paramvalue")
    '            ElseIf dr("paramkey").Equals("kiosk.header1") Then
    '                kioskHeader1 = dr("paramvalue")
    '            ElseIf dr("paramkey").Equals("kiosk.header2") Then
    '                kioskHeader2 = dr("paramvalue")
    '            ElseIf dr("paramkey").Equals("kiosk.header3") Then
    '                kioskHeader3 = dr("paramvalue")
    '            ElseIf dr("paramkey").Equals("kiosk.header4") Then
    '                kioskHeader4 = dr("paramvalue")
    '            ElseIf dr("paramkey").Equals("receipt.font.size") Then
    '                receiptFontSize = dr("paramvalue")
    '            End If
    '        End While

    '        Dim font As Font = New Drawing.Font(receiptFont, receiptFontSize)

    '        lblKioskName.Text = kioskName
    '        lblKioskName.Font = New Drawing.Font(receiptFont, kioskNameSize, FontStyle.Bold)

    '        lblHeader1.Text = kioskHeader1
    '        lblHeader1.Font = font
    '        lblHeader2.Text = kioskHeader2
    '        lblHeader2.Font = font
    '        lblHeader3.Text = kioskHeader3
    '        lblHeader3.Font = font
    '        lblHeader4.Text = kioskHeader4
    '        lblHeader4.Font = font
    '        dr.Close()
    '    Catch ex As Exception
    '        createExceptionFile(ex.Message, sql)
    '        MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    Finally
    '        cmd.Dispose()
    '    End Try
    'End Sub

    Private Sub chkBoxDeletedUsers_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkBoxDeletedUsers.CheckedChanged
        fillUsers()
    End Sub
End Class