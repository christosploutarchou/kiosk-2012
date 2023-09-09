Imports Oracle.DataAccess.Client

Public Class frmMain

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        If MessageBox.Show("Εξοδος;", "Εξοδος", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            If generateXreport(whois) Then
                If MessageBox.Show("Εκτύπωση Αναφοράς Βάρδιας;", "Εκτύπωση", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    PrintDocument1.PrinterSettings.Copies = 1
                    PrintDocument1.Print()
                End If
            End If
            logoutUserUUID(whois)
            Me.Dispose()
            frmLogin.Dispose()
            'frmLogin.Show()
        End If        
    End Sub

    Private Sub cmdUsers_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUsers.Click
        If Not isLoggedIn(username) Then
            MessageBox.Show("Ο χρήστης δεν ειναι συνδεμένος", "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        Me.Hide()
        frmNewUser.Show()
    End Sub

    Private Sub frmMain_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        btnExit_Click(sender, e)
    End Sub

    Private Sub frmMain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If isAdmin Then
            cmdUsers.Enabled = True
            btnCategories.Enabled = True
            btnProducts.Enabled = True
            btnReceipts.Enabled = True
            btnReports.Enabled = True
            btnUpdate.Enabled = True
            btnBackup.Enabled = True
            cmdSuppliers.Enabled = True
            txtBoxInitialFiscalAmt.ReadOnly = False
            lnkLabelMessages.Visible = True
            dgvMessages.Visible = True
            btnLottery.Enabled = True
        Else
            cmdUsers.Enabled = False
            cmdSuppliers.Enabled = False
            btnCategories.Enabled = False
            btnProducts.Enabled = False
            btnReceipts.Enabled = False
            btnReports.Enabled = False
            btnUpdate.Enabled = False
            btnBackup.Enabled = False
            btnLottery.Enabled = False
            txtBoxInitialFiscalAmt.ReadOnly = True
            lnkLabelMessages.Visible = False
            dgvMessages.Visible = False
        End If

        If canViewReports Then
            btnReports.Enabled = True
        Else
            btnReports.Enabled = False
        End If

        If canEditProducts Or canEditProductsFull Then
            btnProducts.Enabled = True
            cmdSuppliers.Enabled = True
            btnReports.Enabled = True
        End If

        txtBoxUser.Text = username
        checkMessages()
        getInitialAmt()

        Dim _contextmenu As New ContextMenuStrip
        _contextmenu.Items.Add("Copy")
        AddHandler _contextmenu.ItemClicked, AddressOf contextmenu_click
        For Each rw As DataGridViewRow In dgvMessages.Rows
            For Each c As DataGridViewCell In rw.Cells
                c.ContextMenuStrip = _contextmenu
            Next
        Next

        Dim _contextmenuMessages As New ContextMenuStrip
        _contextmenuMessages.Items.Add("Copy")
        AddHandler _contextmenuMessages.ItemClicked, AddressOf contextmenuMessages_click
        For Each rw As DataGridViewRow In dgvExpiry.Rows
            For Each c As DataGridViewCell In rw.Cells
                c.ContextMenuStrip = _contextmenuMessages
            Next
        Next
    End Sub

    Private Sub contextmenu_click(ByVal sender As System.Object, ByVal e As ToolStripItemClickedEventArgs)
        Dim clickCell As DataGridViewCell = dgvMessages.SelectedCells(0)
        Select Case e.ClickedItem.Text
            Case "Copy"
                Clipboard.SetText(clickCell.Value, TextDataFormat.Text)
        End Select
    End Sub

    Private Sub contextmenuMessages_click(ByVal sender As System.Object, ByVal e As ToolStripItemClickedEventArgs)
        Dim clickCell As DataGridViewCell = dgvExpiry.SelectedCells(0)
        Select Case e.ClickedItem.Text
            Case "Copy"
                Clipboard.SetText(clickCell.Value, TextDataFormat.Text)
        End Select
    End Sub

    Private Sub dgvMessages_CellMouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgvMessages.CellMouseDown
        If e.RowIndex <> -1 And e.ColumnIndex <> -1 Then
            If e.Button = MouseButtons.Right Then
                Dim clickCell As DataGridViewCell = sender.Rows(e.RowIndex).Cells(e.ColumnIndex)
                clickCell.Selected = True
            End If
        End If
    End Sub

    Private Sub dgvExpiry_CellMouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgvExpiry.CellMouseDown
        If e.RowIndex <> -1 And e.ColumnIndex <> -1 Then
            If e.Button = MouseButtons.Right Then
                Dim clickCell As DataGridViewCell = sender.Rows(e.RowIndex).Cells(e.ColumnIndex)
                clickCell.Selected = True
            End If
        End If
    End Sub

    Private Sub btnCategories_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCategories.Click
        If Not isLoggedIn(username) Then
            MessageBox.Show("Ο χρήστης δεν ειναι συνδεμένος", "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        Me.Hide()
        frmCategories.Show()
    End Sub

    Private Sub cmdSuppliers_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSuppliers.Click
        If Not isLoggedIn(username) Then
            MessageBox.Show("Ο χρήστης δεν ειναι συνδεμένος", "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        Me.Hide()
        frmSuppliers.Show()
    End Sub

    Private Sub btnProducts_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProducts.Click
        If Not isLoggedIn(username) Then
            MessageBox.Show("Ο χρήστης δεν ειναι συνδεμένος", "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        Me.Hide()
        frmProducts.Show()
    End Sub

    Private Sub checkMessages()
        Dim cmd As New OracleCommand("", conn)
        Dim dr As OracleDataReader
        Dim sql As String = ""
        Try
            sql = "select description, barcode, min_quantity, avail_quantity, s_name " & _
                  "from products p " & _
                  "inner join barcodes b on p.serno = b.product_serno " & _
                  "inner join suppliers s on p.supplier_id = s.uuid " & _
                  "where alert_on_min = 1 and (avail_quantity <= min_quantity) order by description asc"

            cmd = New OracleCommand(sql, conn)
            cmd.CommandType = CommandType.Text
            dr = cmd.ExecuteReader()
            Dim counter As Integer = 1

            dgvMessages.ColumnCount = 6
            dgvMessages.Columns(0).Name = "Α/Α"
            dgvMessages.Columns(0).Width = 30
            dgvMessages.Columns(1).Name = "Προϊόν"
            dgvMessages.Columns(1).Width = 100
            dgvMessages.Columns(2).Name = "Barcode"
            dgvMessages.Columns(2).Width = 97
            dgvMessages.Columns(3).Name = "Ελ. Ποσ."
            dgvMessages.Columns(3).Width = 45
            dgvMessages.Columns(4).Name = "Διαθ. Ποσ."
            dgvMessages.Columns(4).Width = 45
            dgvMessages.Columns(5).Name = "Προμηθευτής"
            dgvMessages.Columns(5).Width = 104

            dgvMessages.Rows.Clear()

            While dr.Read()
                Dim row As String() = New String() {counter, dr("description"), dr("barcode"), dr("min_quantity"), dr("avail_quantity"), dr("s_name")}
                dgvMessages.Rows.Add(row)
                counter += 1
            End While

            sql = "select description, barcode, expiry_date, s_name " & _
                  "from products p " & _
                  "inner join barcodes b on p.serno = b.product_serno " & _
                  "inner join suppliers s on p.supplier_id = s.uuid " & _
                  "where alert_on_expiry = 1 and alert_date <= (select sysdate from dual) "

            cmd = New OracleCommand(sql, conn)
            cmd.CommandType = CommandType.Text
            dr = cmd.ExecuteReader()
            counter = 1

            dgvExpiry.ColumnCount = 5
            dgvExpiry.Columns(0).Name = "Α/Α"
            dgvExpiry.Columns(0).Width = 30

            dgvExpiry.Columns(1).Name = "Προϊόν"
            dgvExpiry.Columns(1).Width = 130

            dgvExpiry.Columns(2).Name = "Barcode"
            dgvExpiry.Columns(2).Width = 107

            dgvExpiry.Columns(3).Name = "Ημερ. Λήξης"
            dgvExpiry.Columns(3).Width = 80

            dgvExpiry.Columns(4).Name = "Προμηθευτής"
            dgvExpiry.Columns(4).Width = 120

            dgvExpiry.Rows.Clear()

            While dr.Read()
                Dim row As String() = New String() {counter, dr("description"), dr("barcode"), CDate(dr("expiry_date")), dr("s_name")}
                dgvExpiry.Rows.Add(row)
                counter += 1
            End While

            Dim thisCulture = Globalization.CultureInfo.CurrentCulture
            Dim dayOfWeek As DayOfWeek = thisCulture.Calendar.GetDayOfWeek(Date.Today)
            Dim dayName As String = thisCulture.DateTimeFormat.GetDayName(dayOfWeek)

            sql = "select s_name, contact_name, phone_1, notes " & _
                  "from suppliers " & _
                  "where "

            If dayName.Equals("Monday") Then
                sql += " MON = 1"
            ElseIf dayName.Equals("Tuesday") Then
                sql += " TUE = 1"
            ElseIf dayName.Equals("Wednesday") Then
                sql += " WED = 1"
            ElseIf dayName.Equals("Thursday") Then
                sql += " THU = 1"
            ElseIf dayName.Equals("Friday") Then
                sql += " FRI = 1"
            Else
                sql += " MON = 1 AND TUE = 1 AND WED = 1 AND THU = 1 AND FRI = 1 "
            End If

            sql += " order by s_name asc"

            cmd = New OracleCommand(sql, conn)
            cmd.CommandType = CommandType.Text
            dr = cmd.ExecuteReader()
            counter = 1

            dgvSuppliers.ColumnCount = 5
            dgvSuppliers.Columns(0).Name = "Α/Α"
            dgvSuppliers.Columns(0).Width = 50

            dgvSuppliers.Columns(1).Name = "Προμηθευτής"
            dgvSuppliers.Columns(1).Width = 130

            dgvSuppliers.Columns(2).Name = "Όνομα"
            dgvSuppliers.Columns(2).Width = 107

            dgvSuppliers.Columns(3).Name = "Τηλέφωνο"
            dgvSuppliers.Columns(3).Width = 150

            dgvSuppliers.Columns(4).Name = "Σημειώσεις"
            dgvSuppliers.Columns(4).Width = 150

            dgvSuppliers.Rows.Clear()

            While dr.Read()
                Dim row As String() = New String() {counter, dr("s_name"), dr("contact_name"), dr("phone_1"), dr("notes")}
                dgvSuppliers.Rows.Add(row)
                counter += 1
            End While
            dr.Close()
        Catch ex As Exception
            createExceptionFile(ex.Message, " " & sql)
            MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            cmd.Dispose()
        End Try
    End Sub

    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        If Not isLoggedIn(username) Then
            MessageBox.Show("Ο χρήστης δεν ειναι συνδεμένος", "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        If Not IsNumeric(txtBoxInitialFiscalAmt.Text) Then
            MessageBox.Show("Το πεδίο 'Αρχικό Ποσό Ταμείου' πρέπει να είναι αριθμός", "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        Else
            Dim sql As String = ""
            Dim cmd As New OracleCommand("", conn)
            Try
                sql = "update global_params " & _
                      "set paramvalue = " & txtBoxInitialFiscalAmt.Text.Replace(",", ".") & " " & _
                      "where paramkey = 'init.fiscal.amt'"

                cmd = New OracleCommand(sql, conn)
                cmd.CommandType = CommandType.Text
                cmd.ExecuteReader()

                MessageBox.Show("Το ποσά έχουν αποθηκευτεί επιτυχώς", "Αποθήκευση αλλαγής", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                createExceptionFile(ex.Message, sql)
                MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                cmd.Dispose()
            End Try
        End If
    End Sub

    Private Sub getInitialAmt()
        Dim cmd As New OracleCommand("", conn)
        Try
            cmd = New OracleCommand(Q_GET_INITIAL_AMOUNT, conn)
            cmd.CommandType = CommandType.Text
            Dim dr As OracleDataReader = cmd.ExecuteReader()
            Dim initialAmount As Double = 0
            If dr.Read Then
                initialAmount = CDbl(dr(0))
            End If
            txtBoxInitialFiscalAmt.Text = initialAmount.ToString("N2")
            dr.Close()
        Catch ex As Exception
            createExceptionFile(ex.Message, " " & Q_GET_INITIAL_AMOUNT)
            MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            cmd.Dispose()
        End Try
    End Sub

    Private Sub btnPos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPos.Click
        If Not isLoggedIn(username) Then
            MessageBox.Show("Ο χρήστης δεν ειναι συνδεμένος", "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        Me.Hide()
        frmPOS.Show()
        If dualMonitor Then
            frmDual.Show()
        End If
    End Sub

    Private Sub btnReports_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReports.Click
        If Not isLoggedIn(username) Then
            MessageBox.Show("Ο χρήστης δεν ειναι συνδεμένος", "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        Me.Hide()
        frmReports.Show()
    End Sub

    Private Sub btnReceipts_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReceipts.Click
        If Not isLoggedIn(username) Then
            MessageBox.Show("Ο χρήστης δεν ειναι συνδεμένος", "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        Me.Hide()
        frmReceipts.Show()
    End Sub

    Private Sub lnkLabelMessages_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkLabelMessages.LinkClicked
        If Not isLoggedIn(username) Then
            MessageBox.Show("Ο χρήστης δεν ειναι συνδεμένος", "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        checkMessages()
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
        e.Graphics.DrawString("Date: " & DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), reportFont, Brushes.Black, 0, 110)
        e.Graphics.DrawString(SINGE_DASHED_LINE, reportFont, Brushes.Black, 0, 130)
        e.Graphics.DrawString("Χρήστης: " & getUser(whois), reportFont, Brushes.Black, 0, 160)

        Dim cmd As New OracleCommand("", conn)
        Dim dr As OracleDataReader
        Dim sql As String = ""
        Try
            sql = "select from_date, to_date, total_receipts, total5percent, total19percent, payments, " & _
                  "initial_amt, final_amt, description, total0percent, amount_laxeia, initialAmtLaxeia, amountVisa, NVL(finalAmtLaxeia,0), total3percent " & _
                  "from x_report " & _
                  "where user_id = '" & whois & "' and created_on = (select max(created_on) from x_report)"

            cmd = New OracleCommand(sql, conn)
            dr = cmd.ExecuteReader()

            Dim xMargin As Integer = 180
            If dr.Read Then
                e.Graphics.DrawString("Από: " & CStr(dr(0)), reportFont, Brushes.Black, 0, xMargin)
                xMargin += 20
                e.Graphics.DrawString("Έως: " & CStr(dr(1)), reportFont, Brushes.Black, 0, xMargin)

                If isAdmin Then
                    xMargin += 20
                    e.Graphics.DrawString("Αρ. Αποδείξεων: " & CStr(dr(2)), reportFont, Brushes.Black, 0, xMargin)
                End If
                
                Dim totalVat5 As Double = CDbl(dr(3))
                Dim totalVat19 As Double = CDbl(dr(4))
                Dim payments As Double = CDbl(dr(5))
                Dim initial As Double = CDbl(dr(6))
                Dim final As Double = CDbl(dr(7))
                Dim totalVat0 As Double = CDbl(dr(9))
                Dim amountLaxeia As Double = CDbl(dr(10))
                Dim initialAmountLaxeia As Double = CDbl(dr(11))
                Dim amountVisa As Double = CDbl(dr(12))
                Dim finalAmtLaxeia As Double = CDbl(dr(13))
                Dim totalVat3 As Double = CDbl(dr(14))

                Dim totalReceivedAmt As Double = totalVat0 + totalVat3 + totalVat5 + totalVat19
                Dim totalAmountToDeliver = (totalReceivedAmt + initial) - payments - amountVisa

                If isAdmin Then
                    xMargin += 20
                    e.Graphics.DrawString("Φ.Π.Α. 0%: " & totalVat0.ToString("N2"), reportFont, Brushes.Black, 0, xMargin)
                    xMargin += 20
                    e.Graphics.DrawString("Φ.Π.Α. 3%: " & totalVat3.ToString("N2"), reportFont, Brushes.Black, 0, xMargin)
                    xMargin += 20
                    e.Graphics.DrawString("Φ.Π.Α. 5%: " & totalVat5.ToString("N2"), reportFont, Brushes.Black, 0, xMargin)
                    xMargin += 20
                    e.Graphics.DrawString("Φ.Π.Α. 19%: " & totalVat19.ToString("N2"), reportFont, Brushes.Black, 0, xMargin)
                    xMargin += 20
                    e.Graphics.DrawString("Πληρωμές: " & payments.ToString("N2"), reportFont, Brushes.Black, 0, xMargin)
                End If
                
                xMargin += 20
                e.Graphics.DrawString("Αρχικό Ποσό: " & initial.ToString("N2"), reportFont, Brushes.Black, 0, xMargin)

                If isAdmin Then
                    xMargin += 20
                    e.Graphics.DrawString("Ποσό Είσπραξης: " & totalReceivedAmt.ToString("N2"), reportFont, Brushes.Black, 0, xMargin)
                    xMargin += 20
                    e.Graphics.DrawString("Ποσό VISA: " & amountVisa.ToString("N2"), reportFont, Brushes.Black, 0, xMargin)
                    xMargin += 20
                    e.Graphics.DrawString("Τελικό Ποσό Ταμείου για Παράδοση: " & totalAmountToDeliver.ToString("N2"), reportFont, Brushes.Black, 0, xMargin)
                End If
                
                xMargin += 20
                e.Graphics.DrawString("Ποσο λαχείων για Παράδοση: " & (finalAmtLaxeia).ToString("N2"), reportFont, Brushes.Black, 0, xMargin)

                If Not dr.IsDBNull(8) Then
                    e.Graphics.DrawString(dr(8), reportFont, Brushes.Black, 0, xMargin)
                End If
            End If
            dr.Close()
        Catch ex As Exception
            createExceptionFile(ex.Message, " " & sql)
            MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            cmd.Dispose()
        End Try
    End Sub

    Private Sub btnBackup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackup.Click
        Dim cmd As New OracleCommand("", conn)
        Try
            Dim path = "C:\"
            cmd = New OracleCommand(Q_EXPORT_DB, conn)
            cmd.CommandType = CommandType.Text
            Dim dr As OracleDataReader = cmd.ExecuteReader()
            If dr.Read Then
                path = CStr(dr(0))
            End If
            dr.Close()
            Dim fileName As String = Date.Now.Month.ToString & Date.Now.Day.ToString & Date.Now.Year.ToString & _
                                     Date.Now.Hour.ToString & Date.Now.Minute.ToString & Date.Now.Millisecond.ToString

            Shell("exp kiosk/oracle@orcl buffer=4096 grants=Y file=" & path & "backup" & fileName & ".dmp", vbNormalFocus)
        Catch ex As Exception
            createExceptionFile(ex.Message, " " & Q_EXPORT_DB)
            MessageBox.Show(CANNOT_EXPORT_DB_FROM_THIS_TERMINAL, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Warning)
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

    Private Sub txtBoxInitialFiscalAmt_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBoxInitialFiscalAmt.MouseEnter
        txtBoxInitialFiscalAmt.BackColor = Color.Bisque
        txtBoxInitialFiscalAmt.Focus()
    End Sub

    Private Sub txtBoxInitialFiscalAmt_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBoxInitialFiscalAmt.MouseLeave
        txtBoxInitialFiscalAmt.BackColor = Color.LemonChiffon
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInvoices.Click
        If Not isLoggedIn(username) Then
            MessageBox.Show("Ο χρήστης δεν ειναι συνδεμένος", "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        Me.Hide()
        frmInvoices.Show()
    End Sub

    Private Sub btnEditPos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditPos.Click
        If Not isLoggedIn(username) Then
            MessageBox.Show("Ο χρήστης δεν ειναι συνδεμένος", "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        Me.Hide()
        frmPOSEdit.Show()
    End Sub

    Private Sub btnLottery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLottery.Click
        If Not isLoggedIn(username) Then
            MessageBox.Show("Ο χρήστης δεν ειναι συνδεμένος", "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        Me.Hide()
        frmLottery.Show()
    End Sub
End Class