Imports System.Data.SQLite
Imports System.Reflection
Imports Oracle.DataAccess.Client

Public Class frmMain
    Private Sub BtnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        If MessageBox.Show("Εξοδος;", "Εξοδος", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            'TODO
            If GenerateXreport(whois) Then
                If MessageBox.Show("Εκτύπωση Αναφοράς Βάρδιας;", "Εκτύπωση", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    PrintDocument1.PrinterSettings.Copies = 1
                    PrintDocument1.Print()
                End If
            End If
            LogoutUserUUID(whois)
            Me.Dispose()
            frmLogin.Dispose()
        End If
    End Sub

    Private Sub CmdUsers_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUsers.Click
        If Not IsLoggedIn(username) Then
            MessageBox.Show("Ο χρήστης δεν ειναι συνδεμένος", "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        Me.Hide()
        frmNewUser.Show()
    End Sub

    Private Sub FrmMain_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        BtnExit_Click(sender, e)
    End Sub

    Private Sub FrmMain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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

    Private Sub Contextmenu_click(ByVal sender As System.Object, ByVal e As ToolStripItemClickedEventArgs)
        Dim clickCell As DataGridViewCell = dgvMessages.SelectedCells(0)
        Select Case e.ClickedItem.Text
            Case "Copy"
                Clipboard.SetText(clickCell.Value, TextDataFormat.Text)
        End Select
    End Sub

    Private Sub ContextmenuMessages_click(ByVal sender As System.Object, ByVal e As ToolStripItemClickedEventArgs)
        Dim clickCell As DataGridViewCell = dgvExpiry.SelectedCells(0)
        Select Case e.ClickedItem.Text
            Case "Copy"
                Clipboard.SetText(clickCell.Value, TextDataFormat.Text)
        End Select
    End Sub

    Private Sub DgvMessages_CellMouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgvMessages.CellMouseDown
        If e.RowIndex <> -1 And e.ColumnIndex <> -1 Then
            If e.Button = MouseButtons.Right Then
                Dim clickCell As DataGridViewCell = sender.Rows(e.RowIndex).Cells(e.ColumnIndex)
                clickCell.Selected = True
            End If
        End If
    End Sub

    Private Sub DgvExpiry_CellMouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgvExpiry.CellMouseDown
        If e.RowIndex <> -1 And e.ColumnIndex <> -1 Then
            If e.Button = MouseButtons.Right Then
                Dim clickCell As DataGridViewCell = sender.Rows(e.RowIndex).Cells(e.ColumnIndex)
                clickCell.Selected = True
            End If
        End If
    End Sub

    Private Sub BtnCategories_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCategories.Click
        If Not IsLoggedIn(username) Then
            MessageBox.Show("Ο χρήστης δεν ειναι συνδεμένος", "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        Me.Hide()
        frmCategories.Show()
    End Sub

    Private Sub CmdSuppliers_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSuppliers.Click
        If Not IsLoggedIn(username) Then
            MessageBox.Show("Ο χρήστης δεν ειναι συνδεμένος", "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        Me.Hide()
        frmSuppliers.Show()
    End Sub

    Private Sub BtnProducts_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProducts.Click
        If Not IsLoggedIn(username) Then
            MessageBox.Show("Ο χρήστης δεν ειναι συνδεμένος", "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        Me.Hide()
        frmProducts.Show()
    End Sub

    Private Sub LoadLowStockMessages()
        Dim WhoAmI As String = "LoadLowStockMessages"
        Dim counter As Integer = 1
        Dim sql As String

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
        Try
            If SqlLite Then

                sql =
                    "SELECT
                    p.description,
                    b.barcode,
                    p.min_quantity,
                    p.avail_quantity,
                    s.s_name
                FROM products p
                INNER JOIN barcodes b
                    ON p.uuid = b.product_uuid
                INNER JOIN suppliers s
                    ON p.supplier_id = s.uuid
                WHERE p.alert_on_min = 1
                  AND p.avail_quantity <= p.min_quantity
                  AND p.kioskid = @KIOSKID
                ORDER BY p.description"

                Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                    sqliteConn.Open()
                    Using cmd As New SQLiteCommand(sql, sqliteConn)
                        cmd.Parameters.AddWithValue("@KIOSKID", kioskId)
                        Using dr As SQLiteDataReader = cmd.ExecuteReader()
                            While dr.Read()
                                dgvMessages.Rows.Add(
                                counter,
                                dr("description"),
                                dr("barcode"),
                                dr("min_quantity"),
                                dr("avail_quantity"),
                                dr("s_name"))

                                counter += 1
                            End While
                        End Using
                    End Using
                End Using
            Else
                sql =
                    "SELECT
                    description,
                    barcode,
                    min_quantity,
                    avail_quantity,
                    s_name
                FROM products p
                INNER JOIN barcodes b
                    ON p.serno = b.product_serno
                INNER JOIN suppliers s
                    ON p.supplier_id = s.uuid
                WHERE alert_on_min = 1
                  AND avail_quantity <= min_quantity
                ORDER BY description"

                Using cmd As New OracleCommand(sql, conn)
                    Using dr As OracleDataReader = cmd.ExecuteReader()
                        While dr.Read()
                            dgvMessages.Rows.Add(
                            counter,
                            dr("description"),
                            dr("barcode"),
                            dr("min_quantity"),
                            dr("avail_quantity"),
                            dr("s_name"))

                            counter += 1
                        End While
                    End Using
                End Using
            End If
        Catch ex As Exception
            CreateExceptionFile(WhoAmI + " " + ex.Message, "")
        End Try
    End Sub

    Private Sub LoadExpiryMessages()
        Dim WhoAmI As String = "LoadExpiryMessages"
        Dim counter As Integer
        Dim sql As String

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

        Try
            If SqlLite Then
                sql =
                    "SELECT
                    p.description,
                    b.barcode,
                    p.expiry_date,
                    s.s_name
                FROM products p
                INNER JOIN barcodes b
                    ON p.uuid = b.product_uuid
                INNER JOIN suppliers s
                    ON p.supplier_id = s.uuid
                WHERE p.alert_on_expiry = 1
                  AND p.alert_date <= CURRENT_TIMESTAMP
                  AND p.kioskid = @KIOSKID
                ORDER BY p.description"

                Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                    sqliteConn.Open()

                    Using cmd As New SQLiteCommand(sql, sqliteConn)
                        cmd.Parameters.AddWithValue("@KIOSKID", kioskId)
                        Using dr As SQLiteDataReader = cmd.ExecuteReader()
                            While dr.Read()
                                dgvExpiry.Rows.Add(
                                counter,
                                dr("description"),
                                dr("barcode"),
                                If(IsDBNull(dr("expiry_date")), "", CDate(dr("expiry_date")).ToString("dd/MM/yyyy")),
                                dr("s_name"))

                                counter += 1
                            End While
                        End Using
                    End Using
                End Using
            Else
                sql =
                    "SELECT
                    description,
                    barcode,
                    expiry_date,
                    s_name
                FROM products p
                INNER JOIN barcodes b
                    ON p.serno = b.product_serno
                INNER JOIN suppliers s
                    ON p.supplier_id = s.uuid
                WHERE alert_on_expiry = 1
                  AND alert_date <= SYSDATE
                ORDER BY description"

                Using cmd As New OracleCommand(sql, conn)
                    Using dr As OracleDataReader = cmd.ExecuteReader()
                        While dr.Read()
                            dgvExpiry.Rows.Add(
                            counter,
                            dr("description"),
                            dr("barcode"),
                            CDate(dr("expiry_date")).ToString("dd/MM/yyyy"),
                            dr("s_name"))

                            counter += 1
                        End While
                    End Using
                End Using
            End If
        Catch ex As Exception
            CreateExceptionFile(WhoAmI + " " + ex.Message, "")
        End Try
    End Sub

    Private Sub LoadSupplierMessages()
        Dim WhoAmI As String = "LoadSupplierMessages"
        Dim counter As Integer
        Dim sql As String

        Dim thisCulture = Globalization.CultureInfo.CurrentCulture
        Dim dayOfWeek As DayOfWeek = thisCulture.Calendar.GetDayOfWeek(Date.Today)
        Dim dayName As String = thisCulture.DateTimeFormat.GetDayName(dayOfWeek)

        Counter = 1

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

        Try
            sql = "SELECT s_name, contact_name, phone_1, notes FROM suppliers WHERE "

            If dayName = "Monday" Then
                sql &= "MON = 1"
            ElseIf dayName = "Tuesday" Then
                sql &= "TUE = 1"
            ElseIf dayName = "Wednesday" Then
                sql &= "WED = 1"
            ElseIf dayName = "Thursday" Then
                sql &= "THU = 1"
            ElseIf dayName = "Friday" Then
                sql &= "FRI = 1"
            Else
                sql &= "MON = 1 AND TUE = 1 AND WED = 1 AND THU = 1 AND FRI = 1"
            End If

            If SqlLite Then
                sql &= " AND KIOSKID = @KIOSKID ORDER BY s_name ASC"

                Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                    sqliteConn.Open()

                    Using cmd As New SQLiteCommand(sql, sqliteConn)
                        cmd.Parameters.AddWithValue("@KIOSKID", kioskId)
                        Using dr As SQLiteDataReader = cmd.ExecuteReader()

                            While dr.Read()
                                dgvSuppliers.Rows.Add(
                                counter,
                                dr("s_name"),
                                dr("contact_name"),
                                dr("phone_1"),
                                dr("notes"))

                                counter += 1
                            End While
                        End Using
                    End Using
                End Using
            Else
                sql &= " ORDER BY s_name ASC"
                Using cmd As New OracleCommand(sql, conn)
                    Using dr As OracleDataReader = cmd.ExecuteReader()
                        While dr.Read()
                            dgvSuppliers.Rows.Add(
                            counter,
                            dr("s_name"),
                            dr("contact_name"),
                            dr("phone_1"),
                            dr("notes"))

                            counter += 1
                        End While
                    End Using
                End Using
            End If
        Catch ex As Exception
            CreateExceptionFile(WhoAmI + " " + ex.Message, "")
        End Try
    End Sub

    Private Sub CheckMessages()
        LoadLowStockMessages()
        LoadExpiryMessages()
        LoadSupplierMessages()
    End Sub

    Private Sub BtnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Dim WhoAmI As String = "frmMain.BtnUpdate_Click"

        If Not IsLoggedIn(username) Then
            MessageBox.Show("Ο χρήστης δεν ειναι συνδεμένος", "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If Not IsNumeric(txtBoxInitialFiscalAmt.Text) Then
            MessageBox.Show("Το πεδίο 'Αρχικό Ποσό Ταμείου' πρέπει να είναι αριθμός", "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim sql As String = ""
        Dim amount As Decimal = Convert.ToDecimal(txtBoxInitialFiscalAmt.Text)

        Try
            If SqlLite Then
                sql =
                    "UPDATE GLOBAL_PARAMS
                     SET PARAMVALUE = @PARAMVALUE,
                         UPDATED_AT = CURRENT_TIMESTAMP,
                         SYNC_STATUS = '1'
                     WHERE PARAMKEY = 'init.fiscal.amt'
                     AND KIOSKID = @KIOSKID"

                Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                    sqliteConn.Open()
                    Using cmd As New SQLiteCommand(sql, sqliteConn)
                        cmd.Parameters.AddWithValue("@PARAMVALUE", amount)
                        cmd.Parameters.AddWithValue("@KIOSKID", kioskId)
                        cmd.ExecuteNonQuery()
                    End Using
                End Using
            Else
                sql =
                    "UPDATE GLOBAL_PARAMS
                        SET PARAMVALUE = :PARAMVALUE,
                            UPDATED_AT = SYSTIMESTAMP
                        WHERE PARAMKEY = 'init.fiscal.amt'"

                Using cmd As New OracleCommand(sql, conn)
                    cmd.BindByName = True
                    cmd.Parameters.Add("PARAMVALUE", OracleDbType.Decimal).Value = amount
                    cmd.ExecuteNonQuery()
                End Using
            End If
            MessageBox.Show("Το ποσά έχουν αποθηκευτεί επιτυχώς", "Αποθήκευση αλλαγής", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            CreateExceptionFile(WhoAmI + " " + ex.Message, sql)
            MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GetInitialAmt()
        Dim WhoAmI As String = "GetInitialAmt"
        Dim sql As String = "SELECT paramvalue FROM global_params WHERE paramkey = 'init.fiscal.amt'"
        Dim initialAmount As Double = 0

        Try
            If SqlLite Then
                sql &= " AND KIOSKID = @KIOSKID"

                Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                    sqliteConn.Open()
                    Using cmd As New SQLiteCommand(sql, sqliteConn)
                        cmd.Parameters.AddWithValue("@KIOSKID", kioskId)
                        Using dr As SQLiteDataReader = cmd.ExecuteReader()
                            If dr.Read() Then
                                initialAmount = If(IsDBNull(dr(0)), 0, CDbl(dr(0)))
                            End If
                        End Using
                    End Using
                End Using
            Else
                Using cmd As New OracleCommand(sql, conn)
                    cmd.CommandType = CommandType.Text
                    Using dr As OracleDataReader = cmd.ExecuteReader()
                        If dr.Read() Then
                            initialAmount = If(dr.IsDBNull(0), 0, CDbl(dr(0)))
                        End If
                    End Using
                End Using
            End If

            txtBoxInitialFiscalAmt.Text = initialAmount.ToString("N2")

        Catch ex As Exception
            CreateExceptionFile(WhoAmI + " " + ex.Message, sql)
            MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BtnPos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPos.Click
        If Not IsLoggedIn(username) Then
            MessageBox.Show("Ο χρήστης δεν ειναι συνδεμένος", "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        Me.Hide()
        frmPOS.Show()
        If dualMonitor Then
            frmDual.Show()
        End If
    End Sub

    Private Sub BtnReports_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReports.Click
        If Not IsLoggedIn(username) Then
            MessageBox.Show("Ο χρήστης δεν ειναι συνδεμένος", "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        Me.Hide()
        frmReports.Show()
    End Sub

    Private Sub BtnReceipts_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReceipts.Click
        If Not IsLoggedIn(username) Then
            MessageBox.Show("Ο χρήστης δεν ειναι συνδεμένος", "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        Me.Hide()
        frmReceipts.Show()
    End Sub

    Private Sub LnkLabelMessages_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkLabelMessages.LinkClicked
        If Not IsLoggedIn(username) Then
            MessageBox.Show("Ο χρήστης δεν ειναι συνδεμένος", "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        CheckMessages()
    End Sub

    Private Sub PrintDocument1_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        Dim WhoAmI As String = "frmMain.PrintDocument1_PrintPage"
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

        Dim sql As String = ""
        Try
            If SqlLite Then
                sql = "Select FROM_DATE, TO_DATE, total_receipts, total5percent, total19percent, payments, 
                       initial_amt, final_amt, description, total0percent, amount_laxeia, initialAmtLaxeia, amountVisa, IFNULL(finalAmtLaxeia, 0), total3percent
                       From x_report
                       Where user_id =@USER_ID
                          And kioskid=@KIOSKID
                        ORDER BY datetime(created_on) DESC
                        LIMIT 1"

                Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                    sqliteConn.Open()
                    Using cmd As New SQLiteCommand(sql, sqliteConn)
                        cmd.Parameters.AddWithValue("@USER_ID", whois)
                        cmd.Parameters.AddWithValue("@KIOSKID", kioskId)
                        Using dr As SQLiteDataReader = cmd.ExecuteReader()
                            Dim xMargin As Integer = 180
                            If dr.Read() Then

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
                        End Using
                    End Using
                End Using
            Else
                sql = "select from_date, to_date, total_receipts, total5percent, total19percent, payments,
                  initial_amt, final_amt, description, total0percent,
                  amount_laxeia, initialAmtLaxeia, amountVisa,
                  NVL(finalAmtLaxeia,0), total3percent
                   from x_report
                   where user_id = '" & whois & "'
                     and created_on = (select max(created_on) from x_report)"

                Using cmd As New OracleCommand(sql, conn)

                    Using dr As OracleDataReader = cmd.ExecuteReader()

                        If dr.Read() Then

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
                        End If
                    End Using
                End Using
            End If
        Catch ex As Exception
            CreateExceptionFile(WhoAmI + " " + ex.Message, " " & sql)
            MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BtnBackup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackup.Click
        Dim cmd As New OracleCommand("", conn)
        Try
            If SqlLite Then
                Exit Sub
            End If
            Dim path = "C:\"
            cmd = New OracleCommand(Q_EXPORT_DB, conn)
            cmd.CommandType = CommandType.Text
            Dim dr As OracleDataReader = cmd.ExecuteReader()
            If dr.Read Then
                path = CStr(dr(0))
            End If
            dr.Close()
            Dim fileName As String = Date.Now.Month.ToString & Date.Now.Day.ToString & Date.Now.Year.ToString &
                                     Date.Now.Hour.ToString & Date.Now.Minute.ToString & Date.Now.Millisecond.ToString

            Shell("exp kiosk/oracle@orcl buffer=4096 grants=Y file=" & path & "backup" & fileName & ".dmp", vbNormalFocus)
        Catch ex As Exception
            CreateExceptionFile(ex.Message, " " & Q_EXPORT_DB)
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

    Private Sub TxtBoxInitialFiscalAmt_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBoxInitialFiscalAmt.MouseEnter
        txtBoxInitialFiscalAmt.BackColor = Color.Bisque
        txtBoxInitialFiscalAmt.Focus()
    End Sub

    Private Sub TxtBoxInitialFiscalAmt_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBoxInitialFiscalAmt.MouseLeave
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

    Private Sub BtnEditPos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditPos.Click
        If Not IsLoggedIn(username) Then
            MessageBox.Show("Ο χρήστης δεν ειναι συνδεμένος", "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        Me.Hide()
        frmPOSEdit.Show()
    End Sub

    Private Sub BtnLottery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLottery.Click
        If Not IsLoggedIn(username) Then
            MessageBox.Show("Ο χρήστης δεν ειναι συνδεμένος", "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        Me.Hide()
        frmLottery.Show()
    End Sub

End Class