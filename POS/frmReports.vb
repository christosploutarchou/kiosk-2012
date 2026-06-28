Imports System.Data.OleDb
Imports System.Data.SQLite
Imports Microsoft.Office.Interop
Imports Oracle.DataAccess.Client
Imports SQLitePCL

Public Class frmReports

    Dim supplierId As String
    Dim supplierUUIDs As ArrayList = New ArrayList()

    Dim userId As String
    Dim userUUIDs As ArrayList = New ArrayList()
    Dim categoryId As String
    Dim categoryUUIDs As ArrayList = New ArrayList()

    Private Const SALES_PER_PRODUCT As String = "SALES_PER_PRODUCT"
    Private Const PAYMENTS As String = "PAYMENTS"
    Private Const SALES_PER_VAT As String = "SALES_PER_VAT"
    Private Const Z_REPORT As String = "Z_REPORT"
    Private Const X_REPORT As String = "X_REPORT"
    Private Const QUANTITY_PER_PRODUCT As String = "QUANTITY_PER_PRODUCT"
    Private Const HOURS_PER_USER As String = "HOURS_PER_USER"
    Private Const BUY_SELL As String = "BUY_SELL"
    Private Const PRODUCTS_PER_SUPPLIER As String = "PRODUCTS_PER_SUPPLIER"
    Private Const QNT_HISTORY As String = "QNT_HISTORY"
    Private Const LOGIN_HISTORY As String = "LOGIN_HISTORY"
    Private Const SALES_PER_CATEGORY As String = "SALES_PER_CATEGORY"

    Private Sub FrmReports_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        frmMain.Show()
    End Sub

    Private Sub FrmReports_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub FrmReports_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        rdbSalesPerProduct.Checked = True
        txtBoxBarcode.Focus()

        If isAdmin Or canViewReports Then
            rdbSalesPerProduct.Visible = True
            rdbSalesPerVAT.Visible = True
            rdbXReport.Visible = True
            rdbZReport.Visible = True
            rdbSalesPerCategory.Visible = True
            rdbBuySellSupplier.Visible = True
            rdbSupplierPr.Visible = True
            rdbUsers.Visible = True
            rdbPayments.Visible = True
            rdbQntHistory.Visible = True
            rdbSessions.Visible = True
        Else
            rdbSalesPerProduct.Visible = False
            rdbSalesPerVAT.Visible = False
            rdbXReport.Visible = False
            rdbZReport.Visible = False
            rdbBuySellSupplier.Visible = False
            rdbSupplierPr.Visible = False
            rdbUsers.Visible = False
            rdbPayments.Visible = False
            rdbQntHistory.Visible = False
            rdbSessions.Visible = False
            rdbSalesPerCategory.Visible = False
        End If

        If canEditProducts Or canEditProductsFull Then
            rdbSalesPerProduct.Visible = True
            rdbSupplierPr.Visible = True
            rdbSalesPerCategory.Visible = True
        End If
    End Sub

    Private Sub SetVisibleFields(ByVal reportType As String)
        ClearGridAndSetInvisible()
        lblTotalSalesAmount.ResetText()
        chkBoxSalesPerSupplier.Checked = False
        cmbCategories.Visible = False

        If reportType.Equals(QNT_HISTORY) Then
            txtBoxBarcode.Visible = False
            txtBoxBarcode.Visible = False
            btnClearBarcode.Visible = False
            cmbNoBarcode.Visible = False
            lblBarcode.Visible = False
        End If

        If reportType.Equals(PRODUCTS_PER_SUPPLIER) Then
            chkBoxSalesPerSupplier.Visible = True
            cmbSupplier.Visible = True
            fillSuppliers()
        Else
            chkBoxSalesPerSupplier.Visible = False
            cmbSupplier.Visible = False
        End If

        If reportType.Equals(PAYMENTS) And isAdmin Then
            lblAmountVAT.Visible = True
        Else
            lblAmountVAT.Visible = False
            lblAmountVAT.Text = ""
        End If

        If reportType.Equals(QNT_HISTORY) Or reportType.Equals(SALES_PER_VAT) Or reportType.Equals(X_REPORT) Or
           reportType.Equals(Z_REPORT) Or reportType.Equals(HOURS_PER_USER) Or reportType.Equals(PAYMENTS) Or
            reportType.Equals(LOGIN_HISTORY) Or reportType.Equals(SALES_PER_CATEGORY) Then
            btnSearch.Visible = True
        Else
            btnSearch.Visible = False
        End If

        If reportType.Equals(QNT_HISTORY) Or reportType.Equals(SALES_PER_PRODUCT) Or reportType.Equals(SALES_PER_VAT) Or
           reportType.Equals(X_REPORT) Or reportType.Equals(Z_REPORT) Or reportType.Equals(HOURS_PER_USER) Or reportType.Equals(PAYMENTS) _
           Or reportType.Equals(LOGIN_HISTORY) Or reportType.Equals(SALES_PER_CATEGORY) Then
            showDateFields(True)
        Else
            showDateFields(False)
        End If

        If reportType.Equals(QUANTITY_PER_PRODUCT) Or reportType.Equals(BUY_SELL) Then
            btnPrint.Visible = False
            cmbUsers.Visible = False
            lblTotalHoursOrAmount.Visible = False
            txtBoxTotalHoursOrPayments.Visible = False
            cmbNoBarcode.Visible = False

            btnClearBarcode.Visible = True
            lblBarcode.Visible = True
            txtBoxBarcode.Visible = True
            txtBoxBarcode.Focus()
            txtBoxBarcode.Visible = True
        End If

        If reportType.Equals(SALES_PER_PRODUCT) Then
            fillProductsNoBarcode()
            btnPrint.Visible = False
            cmbUsers.Visible = False
            lblTotalHoursOrAmount.Visible = False
            txtBoxTotalHoursOrPayments.Visible = False
            lblAmountVAT.Visible = True
            lblBarcode.Visible = True
            txtBoxBarcode.Visible = True
            txtBoxBarcode.Focus()
            txtBoxBarcode.Visible = True
            btnClearBarcode.Visible = True
            cmbNoBarcode.Visible = True

        ElseIf reportType.Equals(PAYMENTS) Then
            lblBarcode.Visible = False
            txtBoxBarcode.Visible = False
            btnClearBarcode.Visible = False
            btnPrint.Visible = False
            cmbUsers.Visible = False
            lblTotalHoursOrAmount.Visible = False
            txtBoxTotalHoursOrPayments.Visible = False
            cmbNoBarcode.Visible = False

            lblTotalHoursOrAmount.Visible = True
            txtBoxTotalHoursOrPayments.Visible = True
            txtBoxTotalHoursOrPayments.Text = "0"
            lblTotalHoursOrAmount.Text = "Σύνολο"

        ElseIf reportType.Equals(SALES_PER_VAT) Or reportType.Equals(Z_REPORT) Then
            lblBarcode.Visible = False
            txtBoxBarcode.Visible = False
            btnClearBarcode.Visible = False
            btnPrint.Visible = False
            cmbUsers.Visible = False
            lblTotalHoursOrAmount.Visible = False
            txtBoxTotalHoursOrPayments.Visible = False
            cmbNoBarcode.Visible = False

        ElseIf reportType.Equals(X_REPORT) Then
            btnPrint.Visible = False
            lblBarcode.Visible = False
            txtBoxBarcode.Visible = False
            txtBoxBarcode.Visible = False
            btnClearBarcode.Visible = False
            lblTotalHoursOrAmount.Visible = False
            txtBoxTotalHoursOrPayments.Visible = False
            cmbNoBarcode.Visible = False
            cmbUsers.Visible = True
            fillUsers(1)

        ElseIf reportType.Equals(PRODUCTS_PER_SUPPLIER) Then
            lblBarcode.Visible = False
            txtBoxBarcode.Visible = False
            btnClearBarcode.Visible = False
            btnPrint.Visible = False
            cmbUsers.Visible = False
            lblTotalHoursOrAmount.Visible = False
            txtBoxTotalHoursOrPayments.Visible = False
            cmbNoBarcode.Visible = False

        ElseIf reportType.Equals(HOURS_PER_USER) Then
            cmbUsers.Visible = True
            fillUsers(-1)
            lblTotalHoursOrAmount.Visible = True
            txtBoxTotalHoursOrPayments.Visible = True
            txtBoxTotalHoursOrPayments.Text = "0"
            lblTotalHoursOrAmount.Text = "Σύνολο Ωρών"

            lblBarcode.Visible = False
            txtBoxBarcode.Visible = False
            btnClearBarcode.Visible = False
            btnPrint.Visible = False
            cmbNoBarcode.Visible = False

        ElseIf reportType.Equals(LOGIN_HISTORY) Then
            btnPrint.Visible = False
            lblBarcode.Visible = False
            txtBoxBarcode.Visible = False
            txtBoxBarcode.Visible = False
            btnClearBarcode.Visible = False
            lblTotalHoursOrAmount.Visible = False
            txtBoxTotalHoursOrPayments.Visible = False
            cmbNoBarcode.Visible = False
            cmbUsers.Visible = False

        ElseIf reportType.Equals(SALES_PER_CATEGORY) Then
            btnPrint.Visible = False
            lblBarcode.Visible = False
            txtBoxBarcode.Visible = False
            txtBoxBarcode.Visible = False
            btnClearBarcode.Visible = False
            lblTotalHoursOrAmount.Visible = False
            txtBoxTotalHoursOrPayments.Visible = False
            cmbNoBarcode.Visible = False
            cmbUsers.Visible = False
            cmbCategories.Visible = True
            fillCategories(1)
        End If
    End Sub

    Private Sub ReportType_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbSalesPerProduct.CheckedChanged,
                                                                                                              rdbBuySellSupplier.CheckedChanged,
                                                                                                              rdbXReport.CheckedChanged,
                                                                                                              rdbZReport.CheckedChanged,
                                                                                                              rdbSalesPerVAT.CheckedChanged,
                                                                                                              rdbSupplierPr.CheckedChanged,
                                                                                                              rdbUsers.CheckedChanged,
                                                                                                              rdbPayments.CheckedChanged,
                                                                                                              rdbQntHistory.CheckedChanged
        Dim rdb As RadioButton = DirectCast(sender, RadioButton)
        If Not rdb.Checked Then Exit Sub

        Select Case rdb.Name
            Case "rdbSalesPerProduct"
                SetVisibleFields(SALES_PER_PRODUCT)
            Case "rdbBuySellSupplier"
                SetVisibleFields(BUY_SELL)
            Case "rdbXReport"
                SetVisibleFields(X_REPORT)
            Case "rdbZReport"
                SetVisibleFields(Z_REPORT)
            Case "rdbSalesPerVAT"
                SetVisibleFields(SALES_PER_VAT)
            Case "rdbSupplierPr"
                SetVisibleFields(PRODUCTS_PER_SUPPLIER)
            Case "rdbUsers"
                SetVisibleFields(HOURS_PER_USER)
            Case "rdbPayments"
                SetVisibleFields(PAYMENTS)
            Case "rdbQntHistory"
                SetVisibleFields(QNT_HISTORY)
        End Select
    End Sub

    Private Sub ClearGridAndSetInvisible()
        dgvReports.Rows.Clear()
        dgvReports.Columns.Clear()
    End Sub

    Private Function GetSalesAmount() As Double
        Dim sql As String = ""
        Dim categoryName As String = "Όλες"
        Dim supplierName As String = "Όλοι"
        If SqlLite Then
            Sql = "SELECT IFNULL(SUM(amount),0) total
                           FROM receipts_det
                           WHERE created_on BETWEEN @FROM AND @TO
                           AND kioskid = @KIOSKID "

            'Filter by cateogry
            If cmbCategories.SelectedIndex <> -1 Then
                If Not cmbCategories.SelectedItem.Equals("Όλες") Then
                    Sql &= " AND product_uuid IN
                                    (
                                        SELECT uuid
                                        FROM products
                                        WHERE category_id = @CATEGORY_ID
                                    )"
                    categoryName = cmbCategories.SelectedItem.ToString()
                End If
            End If

            Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                sqliteConn.Open()
                Using cmd As New SQLiteCommand(Sql, sqliteConn)

                    cmd.Parameters.AddWithValue("@FROM", dtpFrom.Value.Date)
                    cmd.Parameters.AddWithValue("@TO", dtpTo.Value.Date.AddDays(1).AddSeconds(-1))
                    cmd.Parameters.AddWithValue("@KIOSKID", kioskId)

                    If categoryName <> "Όλες" Then
                        cmd.Parameters.AddWithValue("@CATEGORY_ID", categoryUUIDs(cmbCategories.SelectedIndex))
                    End If

                    Using dr As SQLiteDataReader = cmd.ExecuteReader()
                        If dr.Read() Then
                            Return Convert.ToDouble(dr("total"))
                        End If
                    End Using
                End Using
            End Using
        Else
            Dim dateFrom As String = dtpFrom.Value.Day & "-" & FindMonth(dtpFrom.Value.Month.ToString()) & "-" & dtpFrom.Value.Year.ToString().Substring(2, 2)
            Dim dateTo As String = dtpTo.Value.Day & "-" & FindMonth(dtpTo.Value.Month.ToString()) & "-" & dtpTo.Value.Year.ToString().Substring(2, 2)

            Sql = "select NVL(sum(amount),0) total
                           from receipts_det
                           where created_on BETWEEN
                           to_timestamp('" & dateFrom & " 00:00:00','DD-MON-YY HH24:MI:SS')
                           AND
                           to_timestamp('" & dateTo & " 23:59:59','DD-MON-YY HH24:MI:SS')"

            If cmbCategories.SelectedIndex <> -1 Then
                If Not cmbCategories.SelectedItem.Equals("Όλες") Then
                    Sql &= " and product_serno in
                                    (
                                    select serno
                                    from products
                                    where category_id='" &
                            categoryUUIDs(cmbCategories.SelectedIndex) &
                            "')"
                    categoryName = cmbCategories.SelectedItem.ToString()
                End If
            End If

            Using cmd As New OracleCommand(Sql, conn)
                Using dr As OracleDataReader = cmd.ExecuteReader()
                    If dr.Read() Then
                        Return Convert.ToDouble(dr(0))
                    End If
                End Using
            End Using
        End If
        Return 0
    End Function

    Private Sub SalesPerCategorySearch()
        Dim WhoAmI As String = "frmReports.SalesPerCategorySearch"
        Dim sql As String = ""
        DgvReportCleanup()
        Try
            Dim categoryName As String = "Όλες"
            Dim supplierName As String = "Όλοι"

            'Get total amount from receipts det
            Dim total As Double = GetSalesAmount()

            dgvReports.ColumnCount = 5

            dgvReports.Columns(0).Name = FROM_DATE
            dgvReports.Columns(0).Width = 150

            dgvReports.Columns(1).Name = "Έως"
            dgvReports.Columns(1).Width = 150

            dgvReports.Columns(2).Name = "Ολικό Ποσό"
            dgvReports.Columns(2).Width = 100

            dgvReports.Columns(3).Name = "Κατηγορία"
            dgvReports.Columns(3).Width = 100

            dgvReports.Columns(4).Name = "Προμηθευτές"
            dgvReports.Columns(4).Width = 400

            'Get suppliers
            If Not categoryName.Equals("Όλες") AndAlso cmbCategories.SelectedIndex > 0 Then
                If SqlLite Then
                    Sql = "SELECT DISTINCT s.s_name
                               FROM suppliers s
                               WHERE s.uuid IN
                               (
                                   SELECT p.supplier_id
                                   FROM products p
                                   WHERE p.category_id = @CATEGORY_ID
                                     AND p.kioskid = @KIOSKID
                               )
                               ORDER BY s.s_name"

                    supplierName = ""

                    Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                        sqliteConn.Open()
                        Using cmd As New SQLiteCommand(Sql, sqliteConn)

                            cmd.Parameters.AddWithValue("@CATEGORY_ID", categoryUUIDs(cmbCategories.SelectedIndex))
                            cmd.Parameters.AddWithValue("@KIOSKID", kioskId)

                            Using dr As SQLiteDataReader = cmd.ExecuteReader()
                                While dr.Read()
                                    supplierName &= " " & dr("s_name").ToString()
                                End While
                            End Using
                        End Using
                    End Using
                Else
                    Sql = "SELECT DISTINCT s_name
                               FROM suppliers
                               WHERE uuid IN
                               (
                                   SELECT supplier_id
                                   FROM products
                                   WHERE serno IN
                                   (
                                       SELECT serno
                                       FROM products
                                       WHERE category_id = '" &
                                   categoryUUIDs(cmbCategories.SelectedIndex) &
                                   "'
                                   )
                               )
                               ORDER BY s_name"

                    supplierName = ""
                    Using cmd As New OracleCommand(Sql, conn)
                        Using dr As OracleDataReader = cmd.ExecuteReader()
                            While dr.Read()
                                supplierName &= " " & dr(0).ToString()
                            End While
                        End Using
                    End Using
                End If
            End If

            Dim row As String() = {dtpFrom.Text, dtpTo.Text, total.ToString("N2"), categoryName, supplierName.Trim()}
            dgvReports.Rows.Add(row)
            btnPrint.Visible = True
        Catch ex As Exception
            CreateExceptionFile(WhoAmI + " ", sql)
        Finally
            FormatDataGrid()
        End Try
    End Sub

    Private Sub DgvReportCleanup()
        dgvReports.Columns.Clear()
        dgvReports.Rows.Clear()
    End Sub

    Private Sub SalesPerVAT()
        Dim sql As String
        Dim totalVat0 As Double = 0
        Dim totalVat3 As Double = 0
        Dim totalVat5 As Double = 0
        Dim totalVat19 As Double = 0
        DgvReportCleanup()

        If SqlLite Then

            sql = "SELECT
                    IFNULL(SUM(total_vat5),0),
                    IFNULL(SUM(total_vat19),0),
                    IFNULL(SUM(total_vat0),0),
                    IFNULL(SUM(total_vat3),0)
                   FROM receipts
                   WHERE created_on BETWEEN @FROM AND @TO
            AND kioskid = @KIOSKID"

            Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                sqliteConn.Open()
                Using cmd As New SQLiteCommand(sql, sqliteConn)
                    cmd.Parameters.AddWithValue("@FROM", dtpFrom.Value.Date)
                    cmd.Parameters.AddWithValue("@TO", dtpTo.Value.Date.AddDays(1).AddSeconds(-1))
                    cmd.Parameters.AddWithValue("@KIOSKID", kioskId.ToString())
                    Using dr As SQLiteDataReader = cmd.ExecuteReader()
                        If dr.Read() Then
                            totalVat5 = Convert.ToDouble(dr(0)) * (divideFactor5 / 100)
                            totalVat19 = Convert.ToDouble(dr(1)) * (divideFactor19 / 100)
                            totalVat0 = Convert.ToDouble(dr(2)) * (divideFactor0 / 100)
                            totalVat3 = Convert.ToDouble(dr(3)) * (divideFactor3 / 100)
                        End If
                    End Using
                End Using
            End Using
        Else
            Dim dateFrom As String = dtpFrom.Value.Day & "-" & FindMonth(dtpFrom.Value.Month.ToString()) & "-" & dtpFrom.Value.Year.ToString().Substring(2, 2)
            Dim dateTo As String = dtpTo.Value.Day & "-" & FindMonth(dtpTo.Value.Month.ToString()) & "-" & dtpTo.Value.Year.ToString().Substring(2, 2)

            Sql = "SELECT
                            NVL(SUM(total_vat5),0),
                            NVL(SUM(total_vat19),0),
                            NVL(SUM(total_vat0),0),
                            NVL(SUM(total_vat3),0)
                       FROM receipts
                       WHERE created_on BETWEEN
                             TO_TIMESTAMP('" & dateFrom & " 00:00:00','DD-MON-YY HH24:MI:SS')
                         AND TO_TIMESTAMP('" & dateTo & " 23:59:59','DD-MON-YY HH24:MI:SS')"

            Using cmd As New OracleCommand(Sql, conn)
                Using dr As OracleDataReader = cmd.ExecuteReader()
                    If dr.Read() Then
                        totalVat5 = CDbl(dr(0)) * (divideFactor5 / 100)
                        totalVat19 = CDbl(dr(1)) * (divideFactor19 / 100)
                        totalVat0 = CDbl(dr(2)) * (divideFactor0 / 100)
                        totalVat3 = CDbl(dr(3)) * (divideFactor3 / 100)
                    End If
                End Using
            End Using
        End If

        dgvReports.ColumnCount = 7

        dgvReports.Columns(0).Name = FROM_DATE
        dgvReports.Columns(0).Width = 250

        dgvReports.Columns(1).Name = "Έως"
        dgvReports.Columns(1).Width = 250

        dgvReports.Columns(2).Name = "Ολικό Ποσό 0%"
        dgvReports.Columns(2).Width = 100

        dgvReports.Columns(3).Name = "Ολικό Ποσό 3%"
        dgvReports.Columns(3).Width = 100

        dgvReports.Columns(4).Name = "Ολικό Ποσό 5%"
        dgvReports.Columns(4).Width = 100

        dgvReports.Columns(5).Name = "Ολικό Ποσό 19%"
        dgvReports.Columns(5).Width = 100

        dgvReports.Columns(6).Name = "Συνολικό Ποσό"
        dgvReports.Columns(6).Width = 149

        Dim row As String() = New String() {dtpFrom.Text, dtpTo.Text, totalVat0.ToString("N2"), totalVat3.ToString("N2"), totalVat5.ToString("N2"), totalVat19.ToString("N2"), (totalVat0 + totalVat5 + totalVat19 + totalVat3).ToString("N2")}
        dgvReports.Rows.Add(row)
        btnPrint.Visible = True
    End Sub

    Private Sub SetXReportColumns()
        dgvReports.ColumnCount = 14

        dgvReports.Columns(0).Name = FROM_DATE
        dgvReports.Columns(0).Width = 130

        dgvReports.Columns(1).Name = "Έως"
        dgvReports.Columns(1).Width = 130

        dgvReports.Columns(2).Name = "Χρήστης"
        dgvReports.Columns(2).Width = 80

        dgvReports.Columns(3).Name = "Αποδείξεις"
        dgvReports.Columns(3).Width = 69

        dgvReports.Columns(4).Name = "Ποσό 0%"
        dgvReports.Columns(4).Width = 40

        dgvReports.Columns(5).Name = "Ποσό 3%"
        dgvReports.Columns(5).Width = 40

        dgvReports.Columns(6).Name = "Ποσό 5%"
        dgvReports.Columns(6).Width = 40

        dgvReports.Columns(7).Name = "Ποσό 19%"
        dgvReports.Columns(7).Width = 40

        dgvReports.Columns(8).Name = "Ποσό Πωλήσεων"
        dgvReports.Columns(8).Width = 70

        dgvReports.Columns(9).Name = "Αρχικό Ποσό"
        dgvReports.Columns(9).Width = 60

        dgvReports.Columns(10).Name = "Πληρωμές Προμηθευτών"
        dgvReports.Columns(10).Width = 80

        dgvReports.Columns(11).Name = "Ποσό VISA"
        dgvReports.Columns(11).Width = 50

        dgvReports.Columns(12).Name = "Τελικό Ποσό Ταμείου για Παράδωση"
        dgvReports.Columns(12).Width = 70

        dgvReports.Columns(13).Name = "Ποσό Λαχείων για Παράδωση"
        dgvReports.Columns(13).Width = 70
    End Sub

    Private Sub XReport()
        Dim sql As String
        Dim total0percent As Double
        Dim total3percent As Double
        Dim total5percent As Double
        Dim total19percent As Double
        Dim initial_amt As Double
        Dim payments As Double
        Dim visaAmount As Double
        Dim finalAmtLaxeia As Double
        Dim initialAmountLaxeia As Double
        Dim final_amt As Double
        Dim totalAmountDeliver As Double
        DgvReportCleanup()

        If SqlLite Then
            Sql = "SELECT
                            from_date,
                            to_date,
                            u.username,
                            IFNULL(total_receipts,0) total_receipts,
                            IFNULL(total5percent,0) total5percent,
                            IFNULL(total19percent,0) total19percent,
                            IFNULL(initial_amt,0) initial_amt,
                            IFNULL(payments,0) payments,
                            IFNULL(final_amt,0) final_amt,
                            IFNULL(description,''),
                            IFNULL(total0percent,0) total0percent,
                            IFNULL(amount_laxeia,0) amount_laxeia,
                            IFNULL(initialAmtLaxeia,0) initialAmtLaxeia,
                            IFNULL(amountVisa,0) amountVisa,
                            IFNULL(finalAmtLaxeia,0) finalAmtLaxeia,
                            IFNULL(total3percent,0) total3percent
                       FROM x_report x
                       INNER JOIN users u ON x.user_id = u.uuid
                       WHERE (total_receipts > 0 OR payments > 0)
                         AND created_on BETWEEN @FROM AND @TO
                         AND x.kioskid = @KIOSKID "

            If cmbUsers.SelectedIndex > 0 Then
                Sql &= " AND user_id = @USER_ID "
            End If

            Sql &= " ORDER BY from_date,to_date"

            Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                sqliteConn.Open()

                Using cmd As New SQLiteCommand(Sql, sqliteConn)
                    cmd.Parameters.AddWithValue("@FROM", dtpFrom.Value.Date)
                    cmd.Parameters.AddWithValue("@TO", dtpTo.Value.Date.AddDays(1).AddSeconds(-1))
                    cmd.Parameters.AddWithValue("@KIOSKID", kioskId)

                    If cmbUsers.SelectedIndex > 0 Then
                        cmd.Parameters.AddWithValue("@USER_ID", userUUIDs(cmbUsers.SelectedIndex))
                    End If

                    SetXReportColumns()
                    Using dr As SQLiteDataReader = cmd.ExecuteReader()

                        While dr.Read()

                            total0percent = CDbl(dr("total0percent")) * (divideFactor0 / 100)
                            total3percent = CDbl(dr("total3percent")) * (divideFactor3 / 100)
                            total5percent = CDbl(dr("total5percent")) * (divideFactor5 / 100)
                            total19percent = CDbl(dr("total19percent")) * (divideFactor19 / 100)

                            initial_amt = CDbl(dr("initial_amt"))
                            payments = CDbl(dr("payments"))

                            initialAmountLaxeia = If(IsDBNull(dr("initialAmtLaxeia")), 0, CDbl(dr("initialAmtLaxeia")))
                            visaAmount = If(IsDBNull(dr("amountVisa")), 0, CDbl(dr("amountVisa")))
                            finalAmtLaxeia = If(IsDBNull(dr("finalAmtLaxeia")), 0, CDbl(dr("finalAmtLaxeia")))

                            final_amt = total0percent + total3percent + total5percent + total19percent

                            totalAmountDeliver = final_amt + initial_amt - payments - visaAmount

                            Dim row As String() =
                                                {
                                                    CDate(dr("from_date")).ToString("dd/MM/yyyy HH:mm:ss"),
                                                    CDate(dr("to_date")).ToString("dd/MM/yyyy HH:mm:ss"),
                                                    dr("username").ToString(),
                                                    dr("total_receipts").ToString(),
                                                    total0percent.ToString("N2"),
                                                    total3percent.ToString("N2"),
                                                    total5percent.ToString("N2"),
                                                    total19percent.ToString("N2"),
                                                    final_amt.ToString("N2"),
                                                    initial_amt.ToString("N2"),
                                                    payments.ToString("N2"),
                                                    visaAmount.ToString("N2"),
                                                    totalAmountDeliver.ToString("N2"),
                                                    finalAmtLaxeia.ToString("N2")
                                                }
                            dgvReports.Rows.Add(row)
                        End While
                    End Using
                End Using
            End Using
        Else
            Dim dateFrom As String = dtpFrom.Value.Day & "-" & FindMonth(dtpFrom.Value.Month.ToString()) & "-" & dtpFrom.Value.Year.ToString().Substring(2, 2)
            Dim dateTo As String = dtpTo.Value.Day & "-" & FindMonth(dtpTo.Value.Month.ToString()) & "-" & dtpTo.Value.Year.ToString().Substring(2, 2)

            Sql = "SELECT from_date,
                              to_date,
                              u.username,
                              total_receipts,
                              total5percent,
                              total19percent,
                              initial_amt,
                              payments,
                              final_amt,
                              NVL(description,'') description,
                              total0percent,
                              amount_laxeia,
                              initialAmtLaxeia,
                              amountvisa,
                              finalamtlaxeia,
                              total3percent
                       FROM x_report x
                       INNER JOIN users u
                           ON x.user_id = u.uuid
                       WHERE (total_receipts > 0 OR payments > 0)
                         AND created_on BETWEEN
                             to_timestamp(:DATEFROM,'DD-MON-YY HH24:MI:SS')
                         AND
                             to_timestamp(:DATETO,'DD-MON-YY HH24:MI:SS') "

            If cmbUsers.SelectedIndex > 0 Then
                Sql &= " AND user_id = :USER_ID "
            End If

            Sql &= " ORDER BY from_date,to_date"

            DgvReportCleanup()
            SetXReportColumns()
            Using cmd As New OracleCommand(sql, conn)
                cmd.BindByName = True
                cmd.Parameters.Add("DATEFROM", OracleDbType.Varchar2).Value = dateFrom & " 00:00:00"
                cmd.Parameters.Add("DATETO", OracleDbType.Varchar2).Value = dateTo & " 23:59:59"

                If cmbUsers.SelectedIndex > 0 Then
                    cmd.Parameters.Add("USER_ID", OracleDbType.Varchar2).Value = userUUIDs(cmbUsers.SelectedIndex)
                End If

                Using dr As OracleDataReader = cmd.ExecuteReader()
                    While dr.Read()

                        total0percent = If(dr.IsDBNull(dr.GetOrdinal("TOTAL0PERCENT")), 0, CDbl(dr("TOTAL0PERCENT"))) * (divideFactor0 / 100)
                        total3percent = If(dr.IsDBNull(dr.GetOrdinal("TOTAL3PERCENT")), 0, CDbl(dr("TOTAL3PERCENT"))) * (divideFactor3 / 100)
                        total5percent = If(dr.IsDBNull(dr.GetOrdinal("TOTAL5PERCENT")), 0, CDbl(dr("TOTAL5PERCENT"))) * (divideFactor5 / 100)
                        total19percent = If(dr.IsDBNull(dr.GetOrdinal("TOTAL19PERCENT")), 0, CDbl(dr("TOTAL19PERCENT"))) * (divideFactor19 / 100)
                        initial_amt = If(dr.IsDBNull(dr.GetOrdinal("INITIAL_AMT")), 0, CDbl(dr("INITIAL_AMT")))
                        payments = If(dr.IsDBNull(dr.GetOrdinal("PAYMENTS")), 0, CDbl(dr("PAYMENTS")))
                        visaAmount = If(dr.IsDBNull(dr.GetOrdinal("AMOUNTVISA")), 0, CDbl(dr("AMOUNTVISA")))
                        finalAmtLaxeia = If(dr.IsDBNull(dr.GetOrdinal("FINALAMTLAXEIA")), 0, CDbl(dr("FINALAMTLAXEIA")))
                        final_amt = total0percent + total3percent + total5percent + total19percent
                        totalAmountDeliver = final_amt + initial_amt - payments - visaAmount

                        dgvReports.Rows.Add(
                                            CDate(dr("FROM_DATE")).ToString("dd/MM/yyyy HH:mm:ss"),
                                            CDate(dr("TO_DATE")).ToString("dd/MM/yyyy HH:mm:ss"),
                                            dr("USERNAME").ToString(),
                                            dr("TOTAL_RECEIPTS").ToString(),
                                            total0percent.ToString("N2"),
                                            total3percent.ToString("N2"),
                                            total5percent.ToString("N2"),
                                            total19percent.ToString("N2"),
                                            final_amt.ToString("N2"),
                                            initial_amt.ToString("N2"),
                                            payments.ToString("N2"),
                                            visaAmount.ToString("N2"),
                                            totalAmountDeliver.ToString("N2"),
                                            finalAmtLaxeia.ToString("N2"))
                    End While
                End Using
            End Using
        End If
        btnPrint.Visible = True
    End Sub

    Private Sub ZReport()
        Dim WhoAmI As String = "frmReports.ZReport"
        Dim sql As String
        ClearGridAndSetInvisible()

        Dim tmpFrom As Date = dtpFrom.Value.AddHours(-dtpFrom.Value.Hour)
        tmpFrom = tmpFrom.AddMinutes(-tmpFrom.Minute)
        tmpFrom = tmpFrom.AddSeconds(-tmpFrom.Second)

        Dim tmpTo As Date = dtpTo.Value.AddHours(-dtpTo.Value.Hour)
        tmpTo = tmpTo.AddMinutes(-tmpTo.Minute)
        tmpTo = tmpTo.AddSeconds(-tmpTo.Second)

        Dim dateFrom As String = CStr(tmpFrom.Day) & "-" & FindMonth(CStr(tmpFrom.Month)) & "-" & CStr(tmpFrom.Year).Substring(2, 2)

        While (1)
            If tmpFrom > tmpTo Then
                Exit Sub
            End If

            dgvReports.ColumnCount = 9

            dgvReports.Columns(0).Name = "Z"
            dgvReports.Columns(0).Width = 50

            dgvReports.Columns(1).Name = FROM_DATE
            dgvReports.Columns(1).Width = 250

            dgvReports.Columns(2).Name = "Έως"
            dgvReports.Columns(2).Width = 250

            dgvReports.Columns(3).Name = "Αποδείξεις"
            dgvReports.Columns(3).Width = 80

            dgvReports.Columns(4).Name = "Ολικό Ποσό 0%"
            dgvReports.Columns(4).Width = 90

            dgvReports.Columns(5).Name = "Ολικό Ποσό 3%"
            dgvReports.Columns(5).Width = 90

            dgvReports.Columns(6).Name = "Ολικό Ποσό 5%"
            dgvReports.Columns(6).Width = 90

            dgvReports.Columns(7).Name = "Ολικό Ποσό 19%"
            dgvReports.Columns(7).Width = 90

            dgvReports.Columns(8).Name = "Συνολικό Ποσό"
            dgvReports.Columns(8).Width = 100

            Dim totalReceipts As Integer = 0
            Dim totalVat0 As Double = 0
            Dim totalVat5 As Double = 0
            Dim totalVat19 As Double = 0
            Dim totalAll As Double = 0
            Dim totalVat3 As Double = 0
            Dim zseq As Integer = -1
            Dim zuuid As String
            Dim zDate As String


            If SqlLite Then
                Dim tmpDate As String = tmpFrom.Day & "-" & FindMonth(tmpFrom.Month.ToString()) & "-" & tmpFrom.Year
                sql = "SELECT                               
                                z_uuid,
                                z_date,
                                IFNULL(total_receipts,0) total_receipts,
                                IFNULL(total_amount0,0) total_amount0,
                                IFNULL(total_amount5,0) total_amount5,
                                IFNULL(total_amount19,0) total_amount19,
                                IFNULL(total_amount,0) total_amount,
                                IFNULL(total_amount3,0) total_amount3
                           FROM z_report
                           WHERE z_date=@ZDATE
                             AND kioskid=@KIOSKID"

                Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                    sqliteConn.Open()

                    Using cmd As New SQLiteCommand(Sql, sqliteConn)
                        cmd.Parameters.AddWithValue("@ZDATE", tmpDate)
                        cmd.Parameters.AddWithValue("@KIOSKID", kioskId)

                        Using dr As SQLiteDataReader = cmd.ExecuteReader()
                            If dr.Read() Then

                                zuuid = CStr(dr("z_uuid"))
                                zDate = dr("z_date").ToString()
                                totalReceipts = CInt(dr("total_receipts"))

                                totalVat0 = CDbl(dr("total_amount0"))
                                totalVat3 = CDbl(dr("total_amount3"))
                                totalVat5 = CDbl(dr("total_amount5"))
                                totalVat19 = CDbl(dr("total_amount19"))
                                totalAll = CDbl(dr("total_amount"))

                                dgvReports.Rows.Add(
                                                    zuuid,
                                                    zDate,
                                                    zDate,
                                                    totalReceipts,
                                                    totalVat0.ToString("N2"),
                                                    totalVat3.ToString("N2"),
                                                    totalVat5.ToString("N2"),
                                                    totalVat19.ToString("N2"),
                                                    totalAll.ToString("N2"))
                            Else
                                sql = "SELECT
                                                    IFNULL(SUM(total_vat5),0) total_vat5,
                                                    IFNULL(SUM(total_vat19),0) total_vat19,
                                                    IFNULL(SUM(total_vat0),0) total_vat0,
                                                    IFNULL(SUM(total_vat3),0) total_vat3,
                                                    COUNT(*)
                                               FROM receipts
                                               WHERE created_on BETWEEN @FROM AND @TO
                                                 AND kioskid=@KIOSKID"

                                Using cmd2 As New SQLiteCommand(Sql, sqliteConn)
                                    cmd2.Parameters.AddWithValue("@FROM", tmpFrom.Date)
                                    cmd2.Parameters.AddWithValue("@TO", tmpFrom.Date.AddDays(1).AddSeconds(-1))
                                    cmd2.Parameters.AddWithValue("@KIOSKID", kioskId)

                                    Using drInner As SQLiteDataReader = cmd2.ExecuteReader()
                                        If drInner.Read() Then
                                            totalVat5 = CDbl(drInner(0)) * (divideFactor5 / 100)
                                            totalVat19 = CDbl(drInner(1)) * (divideFactor19 / 100)
                                            totalVat0 = CDbl(drInner(2)) * (divideFactor0 / 100)
                                            totalVat3 = CDbl(drInner(3)) * (divideFactor3 / 100)

                                            totalReceipts = CInt(drInner(4))
                                        End If
                                    End Using
                                End Using

                                zuuid = GetZUuid(tmpFrom)

                                If zuuid.Equals("") Then
                                    MessageBox.Show("Δεν έχετε εκτυπώσει όλα τα Z-Report των προηγούμενων ημερομηνιών", "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                    Exit Sub
                                End If

                                Dim totalAmount As Double = totalVat0 + totalVat3 + totalVat5 + totalVat19
                                dgvReports.Rows.Add(
                                                    zuuid,
                                                    tmpFrom.ToString("dd-MM-yyyy"),
                                                    tmpFrom.ToString("dd-MM-yyyy"),
                                                    totalReceipts,
                                                    totalVat0.ToString("N2"),
                                                    totalVat3.ToString("N2"),
                                                    totalVat5.ToString("N2"),
                                                    totalVat19.ToString("N2"),
                                                    totalAmount.ToString("N2"))

                                sql =
                                    "UPDATE z_report
                                             SET total_receipts=@TOTAL_RECEIPTS,
                                                 total_amount0=@TOTAL0,
                                                 total_amount3=@TOTAL3,
                                                 total_amount5=@TOTAL5,
                                                 total_amount19=@TOTAL19,
                                                 total_amount=@TOTAL
                                             WHERE z_uuid=@ZUUID
                                               AND kioskid=@KIOSKID"

                                Using cmd3 As New SQLiteCommand(Sql, sqliteConn)
                                    cmd3.Parameters.AddWithValue("@TOTAL_RECEIPTS", totalReceipts)
                                    cmd3.Parameters.AddWithValue("@TOTAL0", totalVat0)
                                    cmd3.Parameters.AddWithValue("@TOTAL3", totalVat3)
                                    cmd3.Parameters.AddWithValue("@TOTAL5", totalVat5)
                                    cmd3.Parameters.AddWithValue("@TOTAL19", totalVat19)
                                    cmd3.Parameters.AddWithValue("@TOTAL", totalAmount)
                                    cmd3.Parameters.AddWithValue("@ZUUID", zuuid)
                                    cmd3.Parameters.AddWithValue("@KIOSKID", kioskId)
                                    cmd3.ExecuteNonQuery()
                                End Using
                            End If
                        End Using
                    End Using
                End Using
            Else

                Dim tmpDate As String = CStr(tmpFrom.Day) & "-" & FindMonth(CStr(tmpFrom.Month)) & "-" & CStr(tmpFrom.Year)
                Sql = "select z_seq, z_date, total_receipts, total_amount0, total_amount5, total_amount19, total_amount, nvl(total_amount3,0) from z_report " &
                      "where z_date='" & tmpDate & "'"
                Using cmd As New OracleCommand(Sql, conn)
                    Using dr = cmd.ExecuteReader()
                        If dr.Read Then
                            zseq = CInt(dr(0))
                            zDate = dr(1)
                            totalReceipts = CInt(dr(2))
                            totalVat0 = CStr(CDbl(dr(3)).ToString("#,##0.00"))
                            totalVat5 = CStr(CDbl(dr(4)).ToString("#,##0.00"))
                            totalVat19 = CStr(CDbl(dr(5)).ToString("#,##0.00"))
                            totalAll = CStr(CDbl(dr(6)).ToString("#,##0.00"))
                            totalVat3 = CStr(CDbl(dr(7)).ToString("#,##0.00"))

                            Dim row As String() = New String() {zseq, zDate, zDate, totalReceipts, totalVat0.ToString("N2"), totalVat3.ToString("N2"), totalVat5.ToString("N2"),
                                    totalVat19.ToString("N2"), totalAll.ToString("N2")}
                            dgvReports.Rows.Add(row)

                        Else
                            Sql = "select NVL(sum(total_vat5),0), NVL(sum(total_vat19),0), NVL(sum(total_vat0),0), NVL(sum(total_vat3),0), count(*) from receipts " &
                              "where created_on BETWEEN " &
                              "to_timestamp('" & dateFrom & " 00:00:00', 'DD-MON-YY HH24:MI:SS') AND " &
                              "to_timestamp('" & dateFrom & " 23:59:59', 'DD-MON-YY HH24:MI:SS')"

                            Using cmd2 As New OracleCommand(Sql, conn)
                                Using drInner = cmd2.ExecuteReader()
                                    If drInner.Read() Then
                                        totalVat5 = CStr(CDbl(drInner(0)).ToString("#,##0.00")) * (divideFactor5 / 100)
                                        totalVat19 = CStr(CDbl(drInner(1)).ToString("#,##0.00")) * (divideFactor19 / 100)
                                        totalVat0 = CStr(CDbl(drInner(2)).ToString("#,##0.00")) * (divideFactor0 / 100)
                                        totalVat3 = CStr(CDbl(drInner(3)).ToString("#,##0.00")) * (divideFactor3 / 100)
                                        totalReceipts = CInt(drInner(4))
                                    End If
                                End Using
                                zseq = GetZseq(tmpFrom)

                                If zseq = -1 Then
                                    MessageBox.Show("Δεν έχετε εκτυπώσει όλα τα Z-Report των προηγούμενων ημερομηνιών", "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                    Exit Sub
                                End If

                                Dim row As String() = New String() {zseq, tmpFrom.Day & "-" & tmpFrom.Month & "-" & tmpFrom.Year,
                                                                tmpFrom.Day & "-" & tmpFrom.Month & "-" & tmpFrom.Year,
                                                                totalReceipts, totalVat0.ToString("N2"), totalVat5.ToString("N2"),
                                                                totalVat19.ToString("N2"), totalVat3.ToString("N2"), (totalVat0 + totalVat3 + totalVat5 + totalVat19).ToString("N2")}
                                dgvReports.Rows.Add(row)

                                Sql = "update z_report set total_receipts = " & totalReceipts & ", " &
                                      "                    total_amount0 = " & totalVat0 & ", " &
                                      "                    total_amount3 = " & totalVat3 & ", " &
                                      "                    total_amount5 = " & totalVat5 & ", " &
                                      "                    total_amount19 = " & totalVat19 & ", " &
                                      "                    total_amount = " & (totalVat0 + totalVat3 + totalVat5 + totalVat19) & " " &
                                      "where z_seq = " & zseq & ""
                                Using cmd3 As New OracleCommand(Sql, conn)
                                    cmd3.ExecuteNonQuery()
                                End Using
                            End Using
                        End If
                    End Using
                End Using
                btnPrint.Visible = True
            End If
            tmpFrom = tmpFrom.AddDays(1)
            dateFrom = CStr(tmpFrom.Day) & "-" & FindMonth(CStr(tmpFrom.Month)) & "-" & CStr(tmpFrom.Year).Substring(2, 2)
        End While

    End Sub

    Private Sub BtnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim WhoAmI As String = "frmReports.BtnSearch_Click"
        Dim sql As String = ""

        Try
            If rdbSalesPerCategory.Checked Then
                SalesPerCategorySearch()

            ElseIf rdbSalesPerVAT.Checked Then
                SalesPerVAT()

            ElseIf rdbXReport.Checked Then
                XReport()

            ElseIf rdbZReport.Checked Then
                If dtpTo.Value < dtpFrom.Value Then
                    MessageBox.Show("Η ημερομηνία Έως δεν μπορεί να είναι μικρότερη από την ημερομηνία Από", "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If

                If dtpFrom.Value < startDate Then
                    MessageBox.Show("Η ημερομηνία Από δεν μπορεί να είναι μικρότερη από την αρχική ημερομηνία", "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If

                If dtpTo.Value < startDate Then
                    MessageBox.Show("Η ημερομηνία Έως δεν μπορεί να είναι μικρότερη από την αρχική ημερομηνία", "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
                ZReport()

            ElseIf rdbUsers.Checked Then
                ClearGridAndSetInvisible()
                If cmbUsers.SelectedIndex = -1 Then
                    MessageBox.Show("Δεν έχετε επιλέξει χρήστη", "Επιλογή Χρήστη", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
                GetHoursPerUser()

            ElseIf rdbPayments.Checked Then
                GetPayments()

            ElseIf rdbQntHistory.Checked Then
                GetQuantityHistory()

            ElseIf rdbSessions.Checked Then
                GetSessions()

            End If
        Catch ex As Exception
            CreateExceptionFile(WhoAmI + ex.Message, " " & sql)
            MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            FormatDataGrid()
        End Try
    End Sub
    Private Sub GetQuantityHistory()
        Dim sql As String
        DgvReportCleanup()

        dgvReports.ColumnCount = 10

        dgvReports.Columns(0).Name = "Barcode"
        dgvReports.Columns(0).Width = 100

        dgvReports.Columns(1).Name = "Προϊόν"
        dgvReports.Columns(1).Width = 130

        dgvReports.Columns(2).Name = "Προηγούμενη Ποσότητα"
        dgvReports.Columns(2).Width = 80

        dgvReports.Columns(3).Name = "Νέα Ποσότητα"
        dgvReports.Columns(3).Width = 80

        dgvReports.Columns(4).Name = "Προηγ.Ποσ. Αποθήκης"
        dgvReports.Columns(4).Width = 80

        dgvReports.Columns(5).Name = "Νέα Ποσ. Αποθήκης"
        dgvReports.Columns(5).Width = 100

        dgvReports.Columns(6).Name = "Ημερομηνία Αλλαγής"
        dgvReports.Columns(6).Width = 130

        dgvReports.Columns(7).Name = "Χρήστης Αλλαγής"
        dgvReports.Columns(7).Width = 80

        dgvReports.Columns(8).Name = "Προηγ. Τιμή"
        dgvReports.Columns(8).Width = 80

        dgvReports.Columns(9).Name = "Νέα Τιμή"
        dgvReports.Columns(9).Width = 100

        If SqlLite Then

            Sql = "SELECT
                            (SELECT barcode
                               FROM barcodes b
                              WHERE b.product_uuid = pa.product_uuid
                              LIMIT 1) AS barcode,
                            p.description,
                            pa.prev_quantity,
                            pa.new_quantity,
                            IFNULL(pa.prev_st_qnt,0),
                            IFNULL(pa.new_st_qnt,0),
                            pa.modified_when,
                            u.username,
                            pa.old_price,
                            pa.new_price
                       FROM products_audit pa
                       INNER JOIN products p
                           ON pa.product_uuid = p.uuid
                       INNER JOIN users u
                           ON pa.modified_by = u.uuid
                       WHERE pa.modified_when BETWEEN @FROM AND @TO
                         AND pa.kioskid = @KIOSKID
                       ORDER BY pa.modified_when DESC"

            Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                sqliteConn.Open()

                Using cmd As New SQLiteCommand(sql, sqliteConn)
                    cmd.Parameters.AddWithValue("@FROM", dtpFrom.Value.Date)
                    cmd.Parameters.AddWithValue("@TO", dtpTo.Value.Date.AddDays(1).AddSeconds(-1))
                    cmd.Parameters.AddWithValue("@KIOSKID", kioskId)

                    Using dr As SQLiteDataReader = cmd.ExecuteReader()
                        While dr.Read()

                            dgvReports.Rows.Add(
                                            dr("barcode").ToString(),
                                            dr("description").ToString(),
                                            dr("prev_quantity").ToString(),
                                            dr("new_quantity").ToString(),
                                            dr(4).ToString(),
                                            dr(5).ToString(),
                                            CDate(dr("modified_when")).ToString("dd/MM/yyyy HH:mm:ss"),
                                            dr("username").ToString(),
                                            dr("old_price").ToString(),
                                            dr("new_price").ToString())
                        End While
                    End Using
                End Using
            End Using
        Else
            Dim dateFrom As String = CStr(dtpFrom.Value.Day) & "-" & FindMonth(CStr(dtpFrom.Value.Month)) & "-" & CStr(dtpFrom.Value.Year).Substring(2, 2)
            Dim dateTo As String = CStr(dtpTo.Value.Day) & "-" & FindMonth(CStr(dtpTo.Value.Month)) & "-" & CStr(dtpTo.Value.Year).Substring(2, 2)

            Sql = "select (select barcode from BARCODES where product_serno = pa.PRODUCT_SERNO and rownum < 2) barcode,
                                  p.DESCRIPTION,
                                  pa.PREV_QUANTITY,
                                  pa.NEW_QUANTITY,
                                  nvl(pa.PREV_ST_QNT,0),
                                  nvl(pa.NEW_ST_QNT,0),
                                  pa.MODIFIED_WHEN,
                                  u.USERNAME,
                                  pa.OLD_PRICE,
                                  pa.NEW_PRICE
                           from products_audit pa
                           inner join products p on pa.PRODUCT_SERNO = p.serno
                           inner join users u on u.UUID = pa.MODIFIED_BY
                           where modified_when BETWEEN
                                 to_timestamp('" & dateFrom & " 00:00:00','DD-MON-YY HH24:MI:SS')
                             AND to_timestamp('" & dateTo & " 23:59:59','DD-MON-YY HH24:MI:SS')
                           order by pa.MODIFIED_WHEN desc"

            Using cmd As New OracleCommand(Sql, conn)
                Using dr As OracleDataReader = cmd.ExecuteReader()
                    While dr.Read()
                        Dim row As String() =
                                            {
                                                dr(0).ToString(),
                                                dr(1).ToString(),
                                                dr(2).ToString(),
                                                dr(3).ToString(),
                                                dr(4).ToString(),
                                                dr(5).ToString(),
                                                dr(6).ToString(),
                                                dr(7).ToString(),
                                                dr(8).ToString(),
                                                dr(9).ToString()
                                            }
                        dgvReports.Rows.Add(row)
                    End While
                End Using
            End Using
        End If
        btnPrint.Visible = True

    End Sub

    Private Sub GetPayments()
        Dim sql As String
        Dim totalAmount As Double = 0
        Dim totalVATamount As Double = 0
        Dim totalVat0 As Double = 0
        Dim totalVat3 As Double = 0
        Dim totalVat5 As Double = 0
        Dim totalVat19 As Double = 0
        Dim totalPaymentsVat0 As Double = 0
        Dim totalPaymentsVat3 As Double = 0
        Dim totalPaymentsVat5 As Double = 0
        Dim totalPaymentsVat19 As Double = 0
        DgvReportCleanup()

        dgvReports.ColumnCount = 9

        dgvReports.Columns(0).Name = FROM_DATE
        dgvReports.Columns(0).Width = 150

        dgvReports.Columns(1).Name = "Έως"
        dgvReports.Columns(1).Width = 150

        dgvReports.Columns(2).Name = "Ημερομηνία Πληρωμής"
        dgvReports.Columns(2).Width = 150

        dgvReports.Columns(3).Name = "Ποσό"
        dgvReports.Columns(3).Width = 50

        dgvReports.Columns(4).Name = "Χρήστης"
        dgvReports.Columns(4).Width = 100

        dgvReports.Columns(5).Name = "Φ.Π.Α"
        dgvReports.Columns(5).Width = 80

        dgvReports.Columns(6).Name = "Ποσό Φ.Π.Α"
        dgvReports.Columns(6).Width = 100

        dgvReports.Columns(7).Name = "Προμηθευτής"
        dgvReports.Columns(7).Width = 100

        dgvReports.Columns(8).Name = "Αρ. Τιμολογίου"
        dgvReports.Columns(8).Width = 100

        If SqlLite Then

            Sql = "SELECT
                            p.created_on,
                            p.amount,
                            u.username,
                            IFNULL(p.amountvat,0) amountvat,
                            IFNULL(p.vat,-1) vat,
                            IFNULL(s.s_name,' ') s_name,
                            IFNULL(inv_number,' ') inv_number
                       FROM payments p
                       INNER JOIN users u ON p.created_by = u.uuid
                       LEFT JOIN suppliers s ON p.supplier_id = s.uuid
                       WHERE p.created_on BETWEEN @FROM AND @TO
                         AND p.kioskid = @KIOSKID
                       ORDER BY p.created_on DESC"

            Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                sqliteConn.Open()

                Using cmd As New SQLiteCommand(sql, sqliteConn)
                    cmd.Parameters.AddWithValue("@FROM", dtpFrom.Value.Date)
                    cmd.Parameters.AddWithValue("@TO", dtpTo.Value.Date.AddDays(1).AddSeconds(-1))
                    cmd.Parameters.AddWithValue("@KIOSKID", kioskId)

                    Using dr As SQLiteDataReader = cmd.ExecuteReader()
                        While dr.Read()
                            Dim amount As Double = CDbl(dr("amount"))
                            totalAmount += amount
                            totalVATamount += CDbl(dr("amountvat"))

                            Select Case CInt(dr("vat"))
                                Case 0
                                    totalVat0 += CDbl(dr("amountvat"))
                                    totalPaymentsVat0 += amount
                                Case 3
                                    totalVat3 += CDbl(dr("amountvat"))
                                    totalPaymentsVat3 += amount
                                Case 5
                                    totalVat5 += CDbl(dr("amountvat"))
                                    totalPaymentsVat5 += amount
                                Case 19
                                    totalVat19 += CDbl(dr("amountvat"))
                                    totalPaymentsVat19 += amount
                            End Select

                            dgvReports.Rows.Add(
                                            dtpFrom.Text,
                                            dtpTo.Text,
                                            CDate(dr("created_on")).ToString("dd/MM/yyyy HH:mm:ss"),
                                            amount.ToString("N2"),
                                            dr("username").ToString(),
                                            dr("vat").ToString(),
                                            CDbl(dr("amountvat")).ToString("N2"),
                                            dr("s_name").ToString(),
                                            dr("inv_number").ToString())
                        End While
                    End Using
                End Using
            End Using
        Else

            Dim dateFrom As String = CStr(dtpFrom.Value.Day) & "-" & FindMonth(CStr(dtpFrom.Value.Month)) & "-" & CStr(dtpFrom.Value.Year).Substring(2, 2)
            Dim dateTo As String = CStr(dtpTo.Value.Day) & "-" & FindMonth(CStr(dtpTo.Value.Month)) & "-" & CStr(dtpTo.Value.Year).Substring(2, 2)

            Sql = "select p.CREATED_ON, p.AMOUNT, u.USERNAME, NVL(p.vat, 'N/A'), NVL(p.amountvat,0), NVL(p.vat,-1), " &
                  "NVL(s.s_name, ' ') s_name, NVL(inv_number, ' ') inv_number " &
                  "from payments p " &
                  "inner join users u on p.CREATED_BY = u.UUID " &
                  "left join suppliers s on p.supplier_id = s.uuid " &
                  "where p.created_on BETWEEN " &
                  "to_timestamp('" & dateFrom & " 00:00:00', 'DD-MON-YY HH24:MI:SS') AND " &
                  "to_timestamp('" & dateTo & " 23:59:59', 'DD-MON-YY HH24:MI:SS') " &
                  "order by p.created_on desc"
            Using cmd As New OracleCommand(Sql, conn)

                Using dr = cmd.ExecuteReader()
                    While dr.Read()
                        Dim amount As Double = 0
                        amount = CDbl(dr(1))
                        totalAmount += amount
                        totalVATamount += CDbl(dr(4))

                        If CInt(dr(5)) = 0 Then
                            totalVat0 += CDbl(dr(4))
                            totalPaymentsVat0 += amount
                        ElseIf CInt(dr(5)) = 3 Then
                            totalVat3 += CDbl(dr(4))
                            totalPaymentsVat3 += amount
                        ElseIf CInt(dr(5)) = 5 Then
                            totalVat5 += CDbl(dr(4))
                            totalPaymentsVat5 += amount
                        ElseIf CInt(dr(5)) = 19 Then
                            totalVat19 += CDbl(dr(4))
                            totalPaymentsVat19 += amount
                        End If

                        Dim row As String() = New String() {dateFrom, dateTo, CStr(dr(0)), amount.ToString("N2"), CStr(dr(2)), dr(3), dr(4), dr(6), dr(7)}
                        dgvReports.Rows.Add(row)
                    End While
                End Using
            End Using
        End If

        txtBoxTotalHoursOrPayments.Text = "€" & TruncateDecimal(totalAmount + totalVATamount, 3).ToString
        lblAmountVAT.Text = "Φ.Π.Α για Επιστροφή: €" + TruncateDecimal(totalVATamount, 3).ToString + vbNewLine +
        "Πληρωμές (με Φ.Π.Α) 0% : " + TruncateDecimal(totalPaymentsVat0 + totalVat0, 3).ToString + " , Φ.Π.Α. 0%: " + TruncateDecimal(totalVat0, 3).ToString + vbNewLine +
        "Πληρωμές (με Φ.Π.Α) 3% : " + TruncateDecimal(totalPaymentsVat3 + totalVat3, 3).ToString + " , Φ.Π.Α. 3%: " + TruncateDecimal(totalVat3, 3).ToString + vbNewLine +
        "Πληρωμές (με Φ.Π.Α) 5% : " + TruncateDecimal(totalPaymentsVat5 + totalVat5, 3).ToString + " , Φ.Π.Α. 5%: " + TruncateDecimal(totalVat5, 3).ToString + vbNewLine +
        "Πληρωμές (με Φ.Π.Α) 19%: " + TruncateDecimal(totalPaymentsVat19 + totalVat19, 3).ToString + ", Φ.Π.Α. 19%: " + TruncateDecimal(totalVat19, 3).ToString
        btnPrint.Visible = True
    End Sub

    Private Sub GetSessions()
        Dim sql As String
        DgvReportCleanup()
        dgvReports.ColumnCount = 4

        dgvReports.Columns(0).Name = "Χρήστης"
        dgvReports.Columns(0).Width = 100

        dgvReports.Columns(1).Name = "Από"
        dgvReports.Columns(1).Width = 150

        dgvReports.Columns(2).Name = "Έως"
        dgvReports.Columns(2).Width = 150

        dgvReports.Columns(3).Name = "Μηχανή"
        dgvReports.Columns(3).Width = 100

        If SqlLite Then

            Sql = "SELECT
                            u.username,
                            s.login_when,
                            s.logout_when,
                            s.machine_name
                       FROM sessions s
                       INNER JOIN users u
                           ON s.user_id = u.uuid
                       WHERE s.login_when BETWEEN @FROM AND @TO
                         AND s.kioskid = @KIOSKID
                       ORDER BY s.login_when"

            Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                sqliteConn.Open()

                Using cmd As New SQLiteCommand(Sql, sqliteConn)
                    cmd.Parameters.AddWithValue("@FROM", dtpFrom.Value.Date)
                    cmd.Parameters.AddWithValue("@TO", dtpTo.Value.Date.AddDays(1).AddSeconds(-1))
                    cmd.Parameters.AddWithValue("@KIOSKID", kioskId)

                    Using dr As SQLiteDataReader = cmd.ExecuteReader()
                        While dr.Read()
                            Dim logoutWhen As String = ""
                            If Not IsDBNull(dr("logout_when")) Then
                                logoutWhen = CDate(dr("logout_when")).ToString("dd/MM/yyyy HH:mm:ss")
                            End If

                            dgvReports.Rows.Add(dr("username").ToString(), CDate(dr("login_when")).ToString("dd/MM/yyyy HH:mm:ss"), logoutWhen, dr("machine_name").ToString())
                        End While
                    End Using
                End Using
            End Using
        Else
            Dim dateFrom As String = dtpFrom.Value.Day & "-" & FindMonth(dtpFrom.Value.Month.ToString()) & "-" & dtpFrom.Value.Year.ToString().Substring(2, 2)
            Dim dateTo As String = dtpTo.Value.Day & "-" & FindMonth(dtpTo.Value.Month.ToString()) & "-" & dtpTo.Value.Year.ToString().Substring(2, 2)

            Sql = "SELECT
                                u.username,
                                login_when,
                                logout_when,
                                machine_name
                           FROM sessions s
                           INNER JOIN users u
                               ON s.user_id = u.uuid
                           WHERE login_when BETWEEN
                                 TO_TIMESTAMP('" & dateFrom & " 00:00:00','DD-MON-YY HH24:MI:SS')
                             AND TO_TIMESTAMP('" & dateTo & " 23:59:59','DD-MON-YY HH24:MI:SS')
                           ORDER BY login_when"

            Using cmd As New OracleCommand(sql, conn)
                Using dr As OracleDataReader = cmd.ExecuteReader()
                    While dr.Read()
                        Dim logoutWhen As String = ""

                        If Not dr.IsDBNull(2) Then
                            logoutWhen = CDate(dr(2)).ToString("dd/MM/yyyy HH:mm:ss")
                        End If

                        dgvReports.Rows.Add(dr("USERNAME").ToString(), CDate(dr("LOGIN_WHEN")).ToString("dd/MM/yyyy HH:mm:ss"), logoutWhen, dr("MACHINE_NAME").ToString())
                    End While
                End Using
            End Using
        End If
        btnPrint.Visible = False
    End Sub

    Private Sub GetHoursPerUser()
        Dim sql As String
        Dim totalHours As Double

        DgvReportCleanup()
        dgvReports.Rows.Clear()
        dgvReports.Columns.Clear()

        dgvReports.ColumnCount = 3

        dgvReports.Columns(0).Name = FROM_DATE
        dgvReports.Columns(0).Width = 350

        dgvReports.Columns(1).Name = "Έως"
        dgvReports.Columns(1).Width = 350

        dgvReports.Columns(2).Name = "Διάρκεια (σε λεπτά)"
        dgvReports.Columns(2).Width = 350

        If SqlLite Then
            sql = "SELECT
                        login_when, logout_when
                   FROM sessions
                           WHERE user_id = @USER_ID
                             AND login_when BETWEEN @FROM AND @TO
                             AND kioskid = @KIOSKID
                           ORDER BY login_when ASC"

            Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                sqliteConn.Open()

                Using cmd As New SQLiteCommand(Sql, sqliteConn)
                    cmd.Parameters.AddWithValue("@USER_ID", userUUIDs(cmbUsers.SelectedIndex))
                    cmd.Parameters.AddWithValue("@FROM", dtpFrom.Value.Date)
                    cmd.Parameters.AddWithValue("@TO", dtpTo.Value.Date.AddDays(1).AddSeconds(-1))
                    cmd.Parameters.AddWithValue("@KIOSKID", kioskId)

                    Using dr As SQLiteDataReader = cmd.ExecuteReader()
                        While dr.Read()
                            Dim loginWhen As Date = If(IsDBNull(dr("login_when")), Date.MinValue, CDate(dr("login_when")))
                            Dim logoutWhen As Date = If(IsDBNull(dr("logout_when")), Date.Now, CDate(dr("logout_when")))
                            Dim dateDifference As Long = DateDiff(DateInterval.Minute, loginWhen, logoutWhen)
                            totalHours += dateDifference / 60.0

                            dgvReports.Rows.Add(loginWhen, logoutWhen, dateDifference)
                        End While
                    End Using
                End Using
            End Using
        Else
            Dim dateFrom As String = dtpFrom.Value.Day & "-" & FindMonth(dtpFrom.Value.Month.ToString()) & "-" & dtpFrom.Value.Year.ToString().Substring(2, 2)
            Dim dateTo As String = dtpTo.Value.Day & "-" & FindMonth(dtpTo.Value.Month.ToString()) & "-" & dtpTo.Value.Year.ToString().Substring(2, 2)

            Sql = "select login_when, logout_when
                           from sessions
                           where user_id = :USER_ID
                           and login_when BETWEEN
                               to_timestamp(:DATEFROM,'DD-MON-YY HH24:MI:SS')
                           and
                               to_timestamp(:DATETO,'DD-MON-YY HH24:MI:SS')
                           order by login_when"

            Using cmd As New OracleCommand(sql, conn)
                cmd.BindByName = True
                cmd.Parameters.Add("USER_ID", OracleDbType.Varchar2).Value = userUUIDs(cmbUsers.SelectedIndex)
                cmd.Parameters.Add("DATEFROM", OracleDbType.Varchar2).Value = dateFrom & " 00:00:00"
                cmd.Parameters.Add("DATETO", OracleDbType.Varchar2).Value = dateTo & " 23:59:59"

                Using dr As OracleDataReader = cmd.ExecuteReader()
                    While dr.Read()
                        Dim loginWhen As Date = If(dr.IsDBNull(0), Date.MinValue, CDate(dr(0)))
                        Dim logoutWhen As Date = If(dr.IsDBNull(1), Date.Now, CDate(dr(1)))
                        Dim dateDifference As Long = DateDiff(DateInterval.Minute, loginWhen, logoutWhen)

                        totalHours += dateDifference / 60.0

                        dgvReports.Rows.Add(loginWhen, logoutWhen, dateDifference)
                    End While
                End Using
            End Using
        End If
        txtBoxTotalHoursOrPayments.Text = totalHours.ToString("N2")
        btnPrint.Visible = True
    End Sub

    Private Sub txtBoxBarcode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBoxBarcode.TextChanged
        'TODO
        Dim found As Boolean = False
        Dim sql As String = ""
        Dim cmd As New OracleCommand(sql, conn)
        Try
            'Sales per product within a period
            If rdbSalesPerProduct.Checked Then
                sql = "select p.serno, p.sell_amt, p.avail_quantity, NVL(p.stock_quantity,0) from products p " &
                      "where p.serno = (select b.product_serno from BARCODES b " &
                      "where UPPER(b.barcode) =  '" & txtBoxBarcode.Text.ToUpper & "')"

                cmd = New OracleCommand(sql, conn)
                cmd.CommandType = CommandType.Text
                Dim productSerno As Integer = -1
                Dim sellAmt As Double = 0
                Dim availableQuantity As Integer = 0
                Dim stockQuantity As Integer = 0
                Using dr = cmd.ExecuteReader()
                    If dr.Read() Then
                        found = True
                        productSerno = CInt(dr(0))
                        sellAmt = CDbl(dr(1))
                        availableQuantity = CInt(dr(2))
                        stockQuantity = CInt(dr(3))
                    End If
                End Using

                If found Then
                    Dim dateFrom As String = CStr(dtpFrom.Value.Day) & "-" & FindMonth(CStr(dtpFrom.Value.Month)) & "-" & CStr(dtpFrom.Value.Year).Substring(2, 2)
                    Dim dateTo As String = CStr(dtpTo.Value.Day) & "-" & FindMonth(CStr(dtpTo.Value.Month)) & "-" & CStr(dtpTo.Value.Year).Substring(2, 2)

                    sql = "select NVL(sum(quantity),0) from receipts_det " &
                          "where product_serno = " & productSerno & " " &
                          "and created_on BETWEEN " &
                          "to_timestamp('" & dateFrom & " 00:00:00', 'DD-MON-YY HH24:MI:SS') AND " &
                          "to_timestamp('" & dateTo & " 23:59:59', 'DD-MON-YY HH24:MI:SS')"

                    cmd = New OracleCommand(sql, conn)
                    cmd.CommandType = CommandType.Text
                    Dim totalQuantity As Integer = 0
                    Dim totalAmount As Double = 0
                    Dim productDescription As String = ""
                    Using dr = cmd.ExecuteReader()
                        If dr.Read() Then
                            totalQuantity = CInt(dr(0))
                        End If
                    End Using

                    totalAmount = Math.Round(totalQuantity * sellAmt, 2)

                    sql = "select p.description from products p " &
                          "where p.serno = " & productSerno & ""

                    cmd = New OracleCommand(sql, conn)
                    cmd.CommandType = CommandType.Text
                    Using dr = cmd.ExecuteReader()
                        If dr.Read() Then
                            productDescription = CStr(dr(0))
                        End If
                    End Using

                    dgvReports.ColumnCount = 8

                    dgvReports.Columns(0).Name = FROM_DATE
                    dgvReports.Columns(0).Width = 160

                    dgvReports.Columns(1).Name = "Έως"
                    dgvReports.Columns(1).Width = 160

                    dgvReports.Columns(2).Name = "Περιγραφή"
                    dgvReports.Columns(2).Width = 220

                    dgvReports.Columns(3).Name = "Barcode"
                    dgvReports.Columns(3).Width = 100

                    dgvReports.Columns(4).Name = "Πωλήσεις"
                    dgvReports.Columns(4).Width = 70

                    dgvReports.Columns(5).Name = "Ποσό Πωλήσεων"
                    dgvReports.Columns(5).Width = 80

                    dgvReports.Columns(6).Name = "Διαθέσιμη Ποσότητα"
                    dgvReports.Columns(6).Width = 80

                    dgvReports.Columns(7).Name = "Ποσότητα Αποθήκης"
                    dgvReports.Columns(7).Width = 80

                    Dim row As String() = New String() {dtpFrom.Text, dtpTo.Text, productDescription, txtBoxBarcode.Text, totalQuantity, totalAmount.ToString("N2"), availableQuantity, stockQuantity}
                    dgvReports.Rows.Add(row)
                    txtBoxBarcode.Clear()
                End If
                txtBoxBarcode.Focus()
            End If

            'Buy/Sell/Supplier/ Per Product
            If rdbBuySellSupplier.Checked Then

                If SqlLite Then

                    sql = "SELECT
                                p.description,
                                s.s_name,
                                s.phone_1,
                                p.buy_amt,
                                p.sell_amt
                           FROM products p
                           INNER JOIN suppliers s
                               ON p.supplier_id = s.uuid
                           WHERE p.uuid =
                                 (
                                     SELECT b.product_uuid
                                     FROM barcodes b
                                     WHERE UPPER(b.barcode) = @BARCODE
                                     LIMIT 1
                                 )
                             AND p.kioskid = @KIOSKID"

                    dgvReports.Rows.Clear()
                    dgvReports.Columns.Clear()

                    dgvReports.ColumnCount = 6

                    dgvReports.Columns(0).Name = "Περιγραφή"
                    dgvReports.Columns(0).Width = 359

                    dgvReports.Columns(1).Name = "Barcode"
                    dgvReports.Columns(1).Width = 150

                    dgvReports.Columns(2).Name = "Προμηθευτής"
                    dgvReports.Columns(2).Width = 130

                    dgvReports.Columns(3).Name = "Τηλέφωνο"
                    dgvReports.Columns(3).Width = 110

                    dgvReports.Columns(4).Name = "Τιμή Αγοράς (με Φ.Π.Α)"
                    dgvReports.Columns(4).Width = 100

                    dgvReports.Columns(5).Name = "Τιμή Πώλησης"
                    dgvReports.Columns(5).Width = 100

                    Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")

                        sqliteConn.Open()

                        Using cmd1 As New SQLiteCommand(sql, sqliteConn)

                            cmd1.Parameters.AddWithValue("@BARCODE", txtBoxBarcode.Text.ToUpper())
                            cmd1.Parameters.AddWithValue("@KIOSKID", kioskId)

                            Using dr As SQLiteDataReader = cmd1.ExecuteReader()

                                If dr.Read() Then

                                    Dim tmpBuyAmt As Double = CDbl(dr("buy_amt"))
                                    Dim tmpSellAmt As Double = CDbl(dr("sell_amt"))

                                    dgvReports.Rows.Add(
                                                    dr("description").ToString(),
                                                    txtBoxBarcode.Text,
                                                    dr("s_name").ToString(),
                                                    dr("phone_1").ToString(),
                                                    tmpBuyAmt.ToString("N2"),
                                                    tmpSellAmt.ToString("N2"))
                                    txtBoxBarcode.Clear()
                                End If
                            End Using
                        End Using
                    End Using
                Else
                    sql = "select p.description, s.s_name, s.phone_1, p.buy_amt, p.sell_amt " &
                          "from products p " &
                          "inner join suppliers s on p.supplier_id = s.uuid " &
                          "where p.serno = (select b.product_serno from barcodes b " &
                          "where UPPER(b.barcode) = '" & txtBoxBarcode.Text.ToUpper & "')"

                    Using cmd1 As New OracleCommand(sql, conn)

                        cmd1.CommandType = CommandType.Text

                        Using dr As OracleDataReader = cmd1.ExecuteReader()

                            If dr.Read() Then

                                Dim tmpBuyAmt As Double = CDbl(dr(3))
                                Dim tmpSellAmt As Double = CDbl(dr(4))

                                dgvReports.Rows.Add(
                                                    dr(0).ToString(),
                                                    txtBoxBarcode.Text,
                                                    dr(1).ToString(),
                                                    dr(2).ToString(),
                                                    tmpBuyAmt.ToString("N2"),
                                                    tmpSellAmt.ToString("N2"))
                                txtBoxBarcode.Clear()
                            End If
                        End Using
                    End Using
                End If
            End If
            btnPrint.Visible = True

        Catch ex As Exception
            CreateExceptionFile(ex.Message, " " & sql)
            MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        FormatDataGrid()
        txtBoxBarcode.Focus()
    End Sub

    Private Sub DgvReports_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvReports.CellClick
        Dim index As Integer
        Try
            index = dgvReports.SelectedRows.Item(0).Index
        Catch ex As Exception
            index = -1
            txtBoxBarcode.Focus()
            Exit Sub
        End Try

        If MessageBox.Show(DELETE_SELECTED_LINE, DELETE_LINE, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            dgvReports.AllowUserToDeleteRows = True
            dgvReports.Rows.RemoveAt(index)
            dgvReports.Refresh()
        End If
        FormatDataGrid()
        txtBoxBarcode.Focus()
    End Sub

    Private Sub BtnClearBarcode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearBarcode.Click
        txtBoxBarcode.Clear()
    End Sub

    Private Sub BtnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
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

        If Not rdbZReport.Checked Then
            e.Graphics.DrawString("Date: " & DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), reportFont, Brushes.Black, 0, 110)
        End If

        Dim xMargin As Integer = 130
        If rdbZReport.Checked Then
            For i = 0 To dgvReports.Rows.Count - 1
                xMargin += 10
                Dim headerFontBold As Font = New Drawing.Font(REPORT_FONT, 12, FontStyle.Bold)
                e.Graphics.DrawString("Z-Report", headerFontBold, Brushes.Black, 0, xMargin)
                xMargin += 20
                e.Graphics.DrawString("Z-: " & dgvReports.Rows(i).Cells("Z").Value, reportFont, Brushes.Black, 0, xMargin)
                xMargin += 20
                e.Graphics.DrawString("Από: " & dgvReports.Rows(i).Cells(FROM_DATE).Value, reportFont, Brushes.Black, 0, xMargin)
                xMargin += 20
                e.Graphics.DrawString("Έως: " & dgvReports.Rows(i).Cells("Έως").Value, reportFont, Brushes.Black, 0, xMargin)
                xMargin += 20
                e.Graphics.DrawString("Ποσό 0%: " & dgvReports.Rows(i).Cells("Ολικό Ποσό 0%").Value, reportFont, Brushes.Black, 0, xMargin)
                xMargin += 20
                e.Graphics.DrawString("Ποσό 3%: " & dgvReports.Rows(i).Cells("Ολικό Ποσό 3%").Value, reportFont, Brushes.Black, 0, xMargin)
                xMargin += 20
                e.Graphics.DrawString("Ποσό 5%: " & dgvReports.Rows(i).Cells("Ολικό Ποσό 5%").Value, reportFont, Brushes.Black, 0, xMargin)
                xMargin += 20
                e.Graphics.DrawString("Ποσό 19%: " & dgvReports.Rows(i).Cells("Ολικό Ποσό 19%").Value, reportFont, Brushes.Black, 0, xMargin)
                xMargin += 20
                e.Graphics.DrawString("Τελικό Ποσό: " & dgvReports.Rows(i).Cells("Συνολικό Ποσό").Value, reportFont, Brushes.Black, 0, xMargin)
                xMargin += 80
            Next

        ElseIf rdbXReport.Checked Then
            For i = 0 To dgvReports.Rows.Count - 1
                e.Graphics.DrawString("Από: " & dgvReports.Rows(i).Cells(FROM_DATE).Value, reportFont, Brushes.Black, 0, xMargin)
                xMargin += 20
                e.Graphics.DrawString("Έως: " & dgvReports.Rows(i).Cells("Έως").Value, reportFont, Brushes.Black, 0, xMargin)
                xMargin += 20
                e.Graphics.DrawString("Χρήστης: " & dgvReports.Rows(i).Cells("Χρήστης").Value, reportFont, Brushes.Black, 0, xMargin)
                xMargin += 20
                e.Graphics.DrawString("Αποδείξεις: " & dgvReports.Rows(i).Cells("Αποδείξεις").Value, reportFont, Brushes.Black, 0, xMargin)
                xMargin += 20
                e.Graphics.DrawString("Ποσό 0%: " & dgvReports.Rows(i).Cells("Ποσό 0%").Value, reportFont, Brushes.Black, 0, xMargin)
                xMargin += 20
                e.Graphics.DrawString("Ποσό 3%: " & dgvReports.Rows(i).Cells("Ποσό 3%").Value, reportFont, Brushes.Black, 0, xMargin)
                xMargin += 20
                e.Graphics.DrawString("Ποσό 5%: " & dgvReports.Rows(i).Cells("Ποσό 5%").Value, reportFont, Brushes.Black, 0, xMargin)
                xMargin += 20
                e.Graphics.DrawString("Ποσό 19%: " & dgvReports.Rows(i).Cells("Ποσό 19%").Value, reportFont, Brushes.Black, 0, xMargin)
                xMargin += 20
                e.Graphics.DrawString("Ποσό Πωλήσεων: " & dgvReports.Rows(i).Cells("Ποσό Πωλήσεων").Value, reportFont, Brushes.Black, 0, xMargin)
                xMargin += 20
                e.Graphics.DrawString("Αρχικό Ποσό: " & dgvReports.Rows(i).Cells("Αρχικό Ποσό").Value, reportFont, Brushes.Black, 0, xMargin)
                xMargin += 20
                e.Graphics.DrawString("Πληρωμές Προμηθευτών: " & dgvReports.Rows(i).Cells("Πληρωμές Προμηθευτών").Value, reportFont, Brushes.Black, 0, xMargin)
                xMargin += 20
                e.Graphics.DrawString("Ποσό VISA: " & dgvReports.Rows(i).Cells("Ποσό VISA").Value, reportFont, Brushes.Black, 0, xMargin)
                xMargin += 20
                e.Graphics.DrawString("Τελικό Ποσό Ταμείου για Παράδωση: " & dgvReports.Rows(i).Cells("Τελικό Ποσό Ταμείου για Παράδωση").Value, reportFont, Brushes.Black, 0, xMargin)
                xMargin += 20
                e.Graphics.DrawString("Ποσό Λαχείων για Παράδωση: " & dgvReports.Rows(i).Cells("Ποσό Λαχείων για Παράδωση").Value, reportFont, Brushes.Black, 0, xMargin)
                xMargin += 30
                e.Graphics.DrawString(SINGE_DASHED_LINE, reportFont, Brushes.Black, 0, xMargin)
                xMargin += 20
            Next

        ElseIf rdbSalesPerProduct.Checked = True Then
            For i = 0 To dgvReports.Rows.Count - 1
                e.Graphics.DrawString("Από: " & dgvReports.Rows(i).Cells(FROM_DATE).Value, reportFont, Brushes.Black, 0, xMargin)
                xMargin += 20
                e.Graphics.DrawString("Έως: " & dgvReports.Rows(i).Cells("Έως").Value, reportFont, Brushes.Black, 0, xMargin)
                xMargin += 20
                e.Graphics.DrawString("Περιγραφή: " & dgvReports.Rows(i).Cells("Περιγραφή").Value, reportFont, Brushes.Black, 0, xMargin)
                xMargin += 20
                e.Graphics.DrawString("Barcode: " & dgvReports.Rows(i).Cells("Barcode").Value, reportFont, Brushes.Black, 0, xMargin)
                xMargin += 20
                e.Graphics.DrawString("Πωλήσεις: " & dgvReports.Rows(i).Cells("Πωλήσεις").Value, reportFont, Brushes.Black, 0, xMargin)
                xMargin += 20
                e.Graphics.DrawString("Ποσό Πωλήσεων: " & dgvReports.Rows(i).Cells("Ποσό Πωλήσεων").Value, reportFont, Brushes.Black, 0, xMargin)
                xMargin += 20
                e.Graphics.DrawString("Διαθέσιμη Ποσότητα: " & dgvReports.Rows(i).Cells("Διαθέσιμη Ποσότητα").Value, reportFont, Brushes.Black, 0, xMargin)
                xMargin += 20
                e.Graphics.DrawString("Ποσότητα Αποθήκης: " & dgvReports.Rows(i).Cells("Ποσότητα Αποθήκης").Value, reportFont, Brushes.Black, 0, xMargin)
                xMargin += 20
                e.Graphics.DrawString(SINGE_DASHED_LINE, reportFont, Brushes.Black, 0, xMargin)
                xMargin += 20
            Next

        ElseIf rdbSalesPerVAT.Checked = True Then
            For i = 0 To dgvReports.Rows.Count - 1
                e.Graphics.DrawString("Από: " & dgvReports.Rows(i).Cells(FROM_DATE).Value, reportFont, Brushes.Black, 0, xMargin)
                xMargin += 20
                e.Graphics.DrawString("Έως: " & dgvReports.Rows(i).Cells("Έως").Value, reportFont, Brushes.Black, 0, xMargin)
                xMargin += 20
                e.Graphics.DrawString("Ολικό Ποσό 0%: " & dgvReports.Rows(i).Cells("Ολικό Ποσό 0%").Value, reportFont, Brushes.Black, 0, xMargin)
                xMargin += 20
                e.Graphics.DrawString("Ολικό Ποσό 3%: " & dgvReports.Rows(i).Cells("Ολικό Ποσό 3%").Value, reportFont, Brushes.Black, 0, xMargin)
                xMargin += 20
                e.Graphics.DrawString("Ολικό Ποσό 5%: " & dgvReports.Rows(i).Cells("Ολικό Ποσό 5%").Value, reportFont, Brushes.Black, 0, xMargin)
                xMargin += 20
                e.Graphics.DrawString("Ολικό Ποσό 19%: " & dgvReports.Rows(i).Cells("Ολικό Ποσό 19%").Value, reportFont, Brushes.Black, 0, xMargin)
                xMargin += 20
                e.Graphics.DrawString("Συνολικό Ποσό: " & dgvReports.Rows(i).Cells("Συνολικό Ποσό").Value, reportFont, Brushes.Black, 0, xMargin)
                xMargin += 20
                e.Graphics.DrawString(SINGE_DASHED_LINE, reportFont, Brushes.Black, 0, xMargin)
                xMargin += 20
            Next

        ElseIf rdbBuySellSupplier.Checked = True Then
            For i = 0 To dgvReports.Rows.Count - 1
                e.Graphics.DrawString("Περιγραφή: " & dgvReports.Rows(i).Cells("Περιγραφή").Value, reportFont, Brushes.Black, 0, xMargin)
                xMargin += 20
                e.Graphics.DrawString("Barcode: " & dgvReports.Rows(i).Cells("Barcode").Value, reportFont, Brushes.Black, 0, xMargin)
                xMargin += 20
                e.Graphics.DrawString("Προμηθευτής: " & dgvReports.Rows(i).Cells("Προμηθευτής").Value, reportFont, Brushes.Black, 0, xMargin)
                xMargin += 20
                e.Graphics.DrawString("Τηλέφωνο: " & dgvReports.Rows(i).Cells("Τηλέφωνο").Value, reportFont, Brushes.Black, 0, xMargin)
                xMargin += 20
                e.Graphics.DrawString("Τιμή Αγοράς (με Φ.Π.Α): " & dgvReports.Rows(i).Cells("Τιμή Αγοράς (με Φ.Π.Α)").Value, reportFont, Brushes.Black, 0, xMargin)
                xMargin += 20
                e.Graphics.DrawString("Τιμή Πώλησης: " & dgvReports.Rows(i).Cells("Τιμή Πώλησης").Value, reportFont, Brushes.Black, 0, xMargin)
                xMargin += 20
                e.Graphics.DrawString(SINGE_DASHED_LINE, reportFont, Brushes.Black, 0, xMargin)
                xMargin += 20
            Next

        ElseIf rdbSupplierPr.Checked = True Then
            For i = 0 To dgvReports.Rows.Count - 1
                If i = 0 Then
                    Dim reportFontBold As Font = New Drawing.Font(REPORT_FONT, 9, FontStyle.Bold)
                    e.Graphics.DrawString("Προμηθευτής: " & dgvReports.Rows(i).Cells("Προμηθευτής").Value, reportFontBold, Brushes.Black, 0, xMargin)
                    xMargin += 20
                    If Not chkBoxSalesPerSupplier.Checked Then
                        e.Graphics.DrawString("Τηλέφωνο(α): " & dgvReports.Rows(i).Cells("Τηλέφωνο(α)").Value, reportFontBold, Brushes.Black, 0, xMargin)
                        xMargin += 20
                    End If
                    e.Graphics.DrawString(SINGE_DASHED_LINE, reportFont, Brushes.Black, 0, xMargin)
                End If

                xMargin += 20
                e.Graphics.DrawString(dgvReports.Rows(i).Cells("Α/Α").Value & ". " & dgvReports.Rows(i).Cells("Προϊόν").Value, reportFont, Brushes.Black, 0, xMargin)

                If chkBoxSalesPerSupplier.Checked Then
                    xMargin += 20
                    e.Graphics.DrawString("Πωλήσεις: " & dgvReports.Rows(i).Cells("Πωλήσεις").Value, reportFont, Brushes.Black, 0, xMargin)
                End If

                If isAdmin Then
                    xMargin += 20
                    e.Graphics.DrawString("Τιμή Αγοράς: " & dgvReports.Rows(i).Cells("Τιμή Αγοράς").Value, reportFont, Brushes.Black, 0, xMargin)
                End If

                xMargin += 20
                e.Graphics.DrawString("Τιμή Πώλησης: " & dgvReports.Rows(i).Cells("Τιμή Πώλησης").Value, reportFont, Brushes.Black, 0, xMargin)
                xMargin += 20
                e.Graphics.DrawString("Barcode: " & dgvReports.Rows(i).Cells("Barcode").Value, reportFont, Brushes.Black, 0, xMargin)
                xMargin += 20
                e.Graphics.DrawString("Διαθέσιμη Ποσότητα: " & dgvReports.Rows(i).Cells("Διαθέσιμη Ποσότητα").Value, reportFont, Brushes.Black, 0, xMargin)
                xMargin += 20
                e.Graphics.DrawString("Ποσότητα Αποθήκης: " & dgvReports.Rows(i).Cells("Ποσότητα Αποθήκης").Value, reportFont, Brushes.Black, 0, xMargin)
                xMargin += 20
                e.Graphics.DrawString(SINGE_DASHED_LINE, reportFont, Brushes.Black, 0, xMargin)
            Next

        ElseIf rdbUsers.Checked Then
            For i = 0 To dgvReports.Rows.Count - 1
                If i = 0 Then
                    Dim reportFontBold As Font = New Drawing.Font(REPORT_FONT, 9, FontStyle.Bold)
                    e.Graphics.DrawString("Χρήστης: " & cmbUsers.Text, reportFontBold, Brushes.Black, 0, xMargin)
                    xMargin += 20
                    e.Graphics.DrawString("Από: " & dtpFrom.Text, reportFontBold, Brushes.Black, 0, xMargin)
                    xMargin += 20
                    e.Graphics.DrawString("Έως: " & dtpTo.Text, reportFontBold, Brushes.Black, 0, xMargin)
                    xMargin += 20
                    e.Graphics.DrawString("Σύνολο Ωρών: " & txtBoxTotalHoursOrPayments.Text, reportFontBold, Brushes.Black, 0, xMargin)
                    xMargin += 20
                    e.Graphics.DrawString(SINGE_DASHED_LINE, reportFont, Brushes.Black, 0, xMargin)
                End If

                xMargin += 20
                e.Graphics.DrawString("Από: " & dgvReports.Rows(i).Cells(FROM_DATE).Value, reportFont, Brushes.Black, 0, xMargin)
                xMargin += 20
                e.Graphics.DrawString("Έως: " & dgvReports.Rows(i).Cells("Έως").Value, reportFont, Brushes.Black, 0, xMargin)
                xMargin += 20
                e.Graphics.DrawString("Διάρκεια (σε λεπτά): " & dgvReports.Rows(i).Cells("Διάρκεια (σε λεπτά)").Value, reportFont, Brushes.Black, 0, xMargin)
                xMargin += 20
                e.Graphics.DrawString(SINGE_DASHED_LINE, reportFont, Brushes.Black, 0, xMargin)
            Next

        ElseIf rdbPayments.Checked Then
            For i = 0 To dgvReports.Rows.Count - 1
                If i = 0 Then
                    Dim reportFontBold As Font = New Drawing.Font(REPORT_FONT, 9, FontStyle.Bold)
                    e.Graphics.DrawString(FROM_DATE & ": " & dtpFrom.Text, reportFontBold, Brushes.Black, 0, xMargin)
                    xMargin += 20
                    e.Graphics.DrawString(TO_DATE & ": " & dtpTo.Text, reportFontBold, Brushes.Black, 0, xMargin)
                    xMargin += 20
                    e.Graphics.DrawString(SINGE_DASHED_LINE, reportFont, Brushes.Black, 0, xMargin)
                End If
                xMargin += 20
                e.Graphics.DrawString("Ημερομηνία Πληρωμής: " & dgvReports.Rows(i).Cells("Ημερομηνία Πληρωμής").Value, reportFont, Brushes.Black, 0, xMargin)
                xMargin += 20
                e.Graphics.DrawString("Ποσό: " & dgvReports.Rows(i).Cells("Ποσό").Value, reportFont, Brushes.Black, 0, xMargin)
                xMargin += 20
                e.Graphics.DrawString("Χρήστης: " & dgvReports.Rows(i).Cells("Χρήστης").Value, reportFont, Brushes.Black, 0, xMargin)
                xMargin += 20
                e.Graphics.DrawString("Χρήστης: " & dgvReports.Rows(i).Cells("Χρήστης").Value, reportFont, Brushes.Black, 0, xMargin)
                xMargin += 20
                e.Graphics.DrawString("Φ.Π.Α: " & dgvReports.Rows(i).Cells("Φ.Π.Α").Value, reportFont, Brushes.Black, 0, xMargin)
                xMargin += 20
                e.Graphics.DrawString("Ποσό Φ.Π.Α: " & dgvReports.Rows(i).Cells("Ποσό Φ.Π.Α").Value, reportFont, Brushes.Black, 0, xMargin)
                xMargin += 20
                e.Graphics.DrawString("Προμηθευτής: " & dgvReports.Rows(i).Cells("Προμηθευτής").Value, reportFont, Brushes.Black, 0, xMargin)
                xMargin += 20
                e.Graphics.DrawString("Αρ. Τιμολογίου: " & dgvReports.Rows(i).Cells("Αρ. Τιμολογίου").Value, reportFont, Brushes.Black, 0, xMargin)
                xMargin += 20
                e.Graphics.DrawString(SINGE_DASHED_LINE, reportFont, Brushes.Black, 0, xMargin)
            Next

        ElseIf rdbQntHistory.Checked Then

            For i = 0 To dgvReports.Rows.Count - 1
                If i = 0 Then
                    Dim reportFontBold As Font = New Drawing.Font(REPORT_FONT, 9, FontStyle.Bold)
                    e.Graphics.DrawString(FROM_DATE & ": " & dtpFrom.Text, reportFontBold, Brushes.Black, 0, xMargin)
                    xMargin += 20
                    e.Graphics.DrawString(TO_DATE & ": " & dtpTo.Text, reportFontBold, Brushes.Black, 0, xMargin)
                    xMargin += 20
                    e.Graphics.DrawString(SINGE_DASHED_LINE, reportFont, Brushes.Black, 0, xMargin)
                End If
                xMargin += 20
                e.Graphics.DrawString(BARCODE & ": " & dgvReports.Rows(i).Cells(BARCODE).Value, reportFont, Brushes.Black, 0, xMargin)
                xMargin += 20
                e.Graphics.DrawString(PRODUCT & ": " & dgvReports.Rows(i).Cells(PRODUCT).Value, reportFont, Brushes.Black, 0, xMargin)
                xMargin += 20
                e.Graphics.DrawString(PREV_QNT & ": " & dgvReports.Rows(i).Cells(PREV_QNT).Value, reportFont, Brushes.Black, 0, xMargin)
                xMargin += 20
                e.Graphics.DrawString(NEW_QNT & ": " & dgvReports.Rows(i).Cells(NEW_QNT).Value, reportFont, Brushes.Black, 0, xMargin)
                xMargin += 20

                e.Graphics.DrawString("Προηγ.Ποσ. Αποθήκης" & ": " & dgvReports.Rows(i).Cells("Προηγ.Ποσ. Αποθήκης").Value, reportFont, Brushes.Black, 0, xMargin)
                xMargin += 20
                e.Graphics.DrawString("Νέα Ποσ. Αποθήκης" & ": " & dgvReports.Rows(i).Cells("Νέα Ποσ. Αποθήκης").Value, reportFont, Brushes.Black, 0, xMargin)
                xMargin += 20

                e.Graphics.DrawString(CHANGE_DATE & ": " & dgvReports.Rows(i).Cells(CHANGE_DATE).Value, reportFont, Brushes.Black, 0, xMargin)
                xMargin += 20
                e.Graphics.DrawString(CHANGE_USER & ": " & dgvReports.Rows(i).Cells(CHANGE_USER).Value, reportFont, Brushes.Black, 0, xMargin)
                xMargin += 20
                e.Graphics.DrawString(SINGE_DASHED_LINE, reportFont, Brushes.Black, 0, xMargin)
            Next
        End If
    End Sub

    Private Sub CmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Dispose()
    End Sub

    Private Sub FillSuppliers()
        Dim WhoAmI As String = "frmReports.FillSuppliers"
        Dim sql As String = ""
        Try
            supplierId = ""
            supplierUUIDs.Clear()
            cmbSupplier.Items.Clear()

            If SqlLite Then
                sql = "SELECT uuid,
                          s_name,
                          phone_1,
                          phone_2
                   FROM suppliers
                   WHERE kioskid = @KIOSKID
                   ORDER BY s_name"

                Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                    sqliteConn.Open()
                    Using cmd As New SQLiteCommand(sql, sqliteConn)
                        cmd.Parameters.AddWithValue("@KIOSKID", kioskId)
                        Using dr As SQLiteDataReader = cmd.ExecuteReader()
                            While dr.Read()
                                supplierUUIDs.Add(dr("uuid").ToString())
                                cmbSupplier.Items.Add(dr("s_name").ToString())
                            End While
                        End Using
                    End Using
                End Using
            Else
                sql = "SELECT uuid,
                          s_name,
                          phone_1,
                          phone_2
                   FROM suppliers
                   ORDER BY s_name"

                Using cmd As New OracleCommand(sql, conn)
                    cmd.CommandType = CommandType.Text
                    Using dr As OracleDataReader = cmd.ExecuteReader()
                        While dr.Read()
                            supplierUUIDs.Add(dr("uuid").ToString())
                            cmbSupplier.Items.Add(dr("s_name").ToString())
                        End While
                    End Using
                End Using
            End If
        Catch ex As Exception
            CreateExceptionFile(WhoAmI & " " & ex.Message, sql)
            MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FillUsers(ByVal addAll As Integer)
        Dim WhoAmI As String = "frmReports.FillUsers"
        Dim sql As String = ""

        Try
            userId = ""
            userUUIDs.Clear()
            cmbUsers.Items.Clear()

            If addAll = 1 Then
                userUUIDs.Add(-1)
                cmbUsers.Items.Add("Όλοι")
            End If

            If SqlLite Then
                sql = "SELECT uuid, username
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
                                userUUIDs.Add(dr("uuid").ToString())
                                cmbUsers.Items.Add(dr("username").ToString())
                            End While
                        End Using
                    End Using
                End Using
            Else
                sql = "SELECT uuid, username
                   FROM users
                   WHERE deleted = 0
                   ORDER BY username"

                Using cmd As New OracleCommand(sql, conn)
                    cmd.CommandType = CommandType.Text
                    Using dr As OracleDataReader = cmd.ExecuteReader()
                        While dr.Read()
                            userUUIDs.Add(dr("uuid").ToString())
                            cmbUsers.Items.Add(dr("username").ToString())
                        End While
                    End Using
                End Using
            End If
        Catch ex As Exception
            CreateExceptionFile(WhoAmI & " " & ex.Message, sql)
            MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FillCategories(ByVal addAll As Integer)
        Dim WhoAmI As String = "frmReports.FillCategories"
        Dim sql As String = ""

        Try
            categoryId = ""
            categoryUUIDs.Clear()
            cmbCategories.Items.Clear()

            If addAll = 1 Then
                categoryUUIDs.Add(-1)
                cmbCategories.Items.Add("Όλες")
            End If

            If SqlLite Then
                sql = "Select uuid, description, vat
                   FROM categories
                   WHERE kioskid = @KIOSKID
                   ORDER BY description"

                Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                    sqliteConn.Open()
                    Using cmd As New SQLiteCommand(sql, sqliteConn)
                        cmd.Parameters.AddWithValue("@KIOSKID", kioskId)
                        Using dr As SQLiteDataReader = cmd.ExecuteReader()
                            While dr.Read()
                                categoryUUIDs.Add(dr("uuid").ToString())
                                cmbCategories.Items.Add(dr("description").ToString())
                            End While
                        End Using
                    End Using
                End Using
            Else
                sql = "SELECT uuid, description, vat
                   FROM categories
                   ORDER BY description"

                Using cmd As New OracleCommand(sql, conn)
                    cmd.CommandType = CommandType.Text
                    Using dr As OracleDataReader = cmd.ExecuteReader()
                        While dr.Read()
                            categoryUUIDs.Add(dr("uuid").ToString())
                            cmbCategories.Items.Add(dr("description").ToString())
                        End While
                    End Using
                End Using
            End If
        Catch ex As Exception
            CreateExceptionFile(WhoAmI + " " + ex.Message, sql)
            MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FillProductsNoBarcode()
        cmbNoBarcode.Items.Clear()
        cmbNoBarcode.Items.Add("")
        cmbNoBarcode.Items.Add("Φ.Π.Α 5%")
        cmbNoBarcode.Items.Add("Φ.Π.Α 19%")
    End Sub

    Private Sub cmbSupplier_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSupplier.TextChanged
        'TODO
        ClearGridAndSetInvisible()
        Dim cmd As New OracleCommand("", conn)
        Dim sql As String = ""
        Try
            If chkBoxSalesPerSupplier.Checked Then
                Dim dateFrom As String = CStr(dtpFrom.Value.Day) & "-" & FindMonth(CStr(dtpFrom.Value.Month)) & "-" & CStr(dtpFrom.Value.Year).Substring(2, 2)
                Dim dateTo As String = CStr(dtpTo.Value.Day) & "-" & FindMonth(CStr(dtpTo.Value.Month)) & "-" & CStr(dtpTo.Value.Year).Substring(2, 2)

                sql = "select (select s_name || ' ' || concat(phone_1,NVL(phone_2,'')) from suppliers " &
                      "where uuid = '" & supplierUUIDs(cmbSupplier.SelectedIndex) & "'), " &
                      "p.description ,sum(quantity), NVL(buy_amt_no_vat,0) buy_amt, NVL(sell_amt,0) sell_amt, " &
                      "(select barcode from barcodes where barcodes.PRODUCT_SERNO = rd.PRODUCT_SERNO and rownum < 2), " &
                      "to_number(avail_quantity,'99999'), " &
                      "to_number(stock_quantity,'99999') " &
                      "from receipts_det rd " &
                      "inner join products p on p.SERNO = rd.PRODUCT_SERNO " &
                      "and p.SUPPLIER_ID = '" & supplierUUIDs(cmbSupplier.SelectedIndex) & "'" &
                      "and created_on BETWEEN " &
                      "to_timestamp('" & dateFrom & " 00:00:00', 'DD-MON-YY HH24:MI:SS') AND " &
                      "to_timestamp('" & dateTo & " 23:59:59', 'DD-MON-YY HH24:MI:SS')" &
                      "group by rd.product_serno, p.description, buy_amt_no_vat, sell_amt, " &
                      "to_number(avail_quantity,'99999'), to_number(stock_quantity,'99999') " &
                      "order by to_number(avail_quantity,'99999') "
                cmd = New OracleCommand(sql, conn)
            Else
                'TODO
                sql = "select serno, s_name, concat(phone_1,NVL(phone_2,'')) phone, description, " &
                                             "NVL(BUY_AMT_NO_VAT,0), NVL(sell_amt,0), " &
                                             "(select barcode from barcodes where barcodes.PRODUCT_SERNO = serno and rownum < 2) barcode, " &
                                             "to_number(avail_quantity,'99999'), " &
                                             "to_number(stock_quantity,'99999') " &
                                             "from products p " &
                                             "inner join suppliers s on p.supplier_id = s.uuid " &
                                             "where p.supplier_id = :1 " &
                                             "order by to_number(avail_quantity,'99999'"
                cmd = New OracleCommand(sql, conn)
                Dim supplierIDparam As New OracleParameter
                supplierIDparam.OracleDbType = OracleDbType.Varchar2
                supplierIDparam.Value = supplierUUIDs(cmbSupplier.SelectedIndex)
                cmd.Parameters.Add(supplierIDparam)
                sql = cmd.CommandText
            End If

            cmd.CommandType = CommandType.Text
            Dim dr As OracleDataReader = cmd.ExecuteReader()
            supplierId = ""
            supplierUUIDs.Clear()
            cmbSupplier.Items.Clear()

            dgvReports.ColumnCount = 9
            dgvReports.Columns(0).Name = "Α/Α"
            dgvReports.Columns(0).Width = 50
            dgvReports.Columns(1).Name = "Προμηθευτής"
            dgvReports.Columns(1).Width = 180

            If chkBoxSalesPerSupplier.Checked Then
                dgvReports.Columns(2).Name = "Προϊόν"
                dgvReports.Columns(2).Width = 180
                dgvReports.Columns(3).Name = "Πωλήσεις"
                dgvReports.Columns(3).Width = 180
            Else
                dgvReports.Columns(2).Name = "Τηλέφωνο(α)"
                dgvReports.Columns(2).Width = 110
                dgvReports.Columns(3).Name = "Προϊόν"
                dgvReports.Columns(3).Width = 180
            End If

            dgvReports.Columns(4).Name = "Τιμή Αγοράς χωρίς ΦΠΑ"
            dgvReports.Columns(4).Width = 90
            dgvReports.Columns(5).Name = "Τιμή Πώλησης"
            dgvReports.Columns(5).Width = 90
            dgvReports.Columns(6).Name = "Barcode"
            dgvReports.Columns(6).Width = 100
            dgvReports.Columns(7).Name = "Διαθέσιμη Ποσότητα"
            dgvReports.Columns(7).Width = 80
            dgvReports.Columns(8).Name = "Ποσότητα Αποθήκης"
            dgvReports.Columns(8).Width = 80

            Dim counter As Integer = 1
            Dim totalSalesAmt As Double = 0
            While dr.Read()
                Dim row As String()
                Dim buyAmt As String = "N/A"
                If isAdmin Then
                    If chkBoxSalesPerSupplier.Checked Then
                        buyAmt = dr(3)
                    Else
                        buyAmt = dr(4)
                    End If
                End If

                If chkBoxSalesPerSupplier.Checked Then
                    totalSalesAmt += (CInt(dr(2)) * CDbl(dr(4)))
                    row = New String() {counter, dr(0), dr(1), dr(2), buyAmt, dr(4), dr(5), CInt(dr(6)), CInt(dr(7))}
                Else
                    row = New String() {counter, dr(1), dr(2), dr(3), buyAmt, dr(5), dr(6), CInt(dr(7)), CInt(dr(8))}
                End If
                dgvReports.Rows.Add(row)
                counter += 1
            End While

            If chkBoxSalesPerSupplier.Checked And isAdmin Then
                lblTotalSalesAmount.Text = "Συνολικό Ποσό Πωλήσεων: €" & totalSalesAmt.ToString("N2")
            End If

            btnPrint.Visible = True
            dr.Close()
        Catch ex As Exception
            CreateExceptionFile(ex.Message, " " & sql)
            MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            cmd.Dispose()
            fillSuppliers()
            formatDataGrid()
        End Try
    End Sub

    Private Sub CmbUsers_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbUsers.SelectedIndexChanged
        dgvReports.Rows.Clear()
        dgvReports.Columns.Clear()
        txtBoxTotalHoursOrPayments.Text = "0"
    End Sub

    Private Sub CmbNoBarcode_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbNoBarcode.SelectedIndexChanged
        Dim tmpSerno As Integer = -1
        Dim productUUID As String = ""

        If cmbNoBarcode.Text.Equals("Φ.Π.Α 5%") Then
            tmpSerno = -308
            'TODO
            'productUUID = VAT5_PRODUCT_UUID
        ElseIf cmbNoBarcode.Text.Equals("Φ.Π.Α 19%") Then
            tmpSerno = -309
            'TODO
            'productUUID = VAT19_PRODUCT_UUID
        End If

        If tmpSerno = -1 Then Exit Sub

        Dim sql As String = ""

        Try
            Dim totalQuantity As Integer = 0
            Dim totalAmount As Double = 0

            If SqlLite Then
                sql = "SELECT
                        IFNULL(COUNT(quantity),0),
                        IFNULL(SUM(amount),0)
                   FROM receipts_det
                   WHERE product_uuid=@PRODUCT_UUID
                     AND created_on BETWEEN @FROM AND @TO
                     AND kioskid=@KIOSKID"

                Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                    sqliteConn.Open()
                    Using cmd As New SQLiteCommand(sql, sqliteConn)
                        cmd.Parameters.AddWithValue("@PRODUCT_UUID", productUUID)
                        cmd.Parameters.AddWithValue("@FROM", dtpFrom.Value.Date)
                        cmd.Parameters.AddWithValue("@TO", dtpTo.Value.Date.AddDays(1).AddSeconds(-1))
                        cmd.Parameters.AddWithValue("@KIOSKID", kioskId)

                        Using dr As SQLiteDataReader = cmd.ExecuteReader()
                            If dr.Read() Then
                                totalQuantity = CInt(dr(0))
                                totalAmount = CDbl(dr(1))
                            End If
                        End Using
                    End Using
                End Using
            Else
                Dim dateFrom As String = dtpFrom.Value.Day & "-" & FindMonth(dtpFrom.Value.Month.ToString()) & "-" & dtpFrom.Value.Year.ToString().Substring(2, 2)
                Dim dateTo As String = dtpTo.Value.Day & "-" & FindMonth(dtpTo.Value.Month.ToString()) & "-" & dtpTo.Value.Year.ToString().Substring(2, 2)

                sql = "SELECT
                        NVL(COUNT(quantity),0),
                        NVL(SUM(amount),0)
                   FROM receipts_det
                   WHERE product_serno = :PRODUCT_SERNO
                     AND created_on BETWEEN
                         TO_TIMESTAMP(:DATEFROM,'DD-MON-YY HH24:MI:SS')
                     AND
                         TO_TIMESTAMP(:DATETO,'DD-MON-YY HH24:MI:SS')"

                Using cmd As New OracleCommand(sql, conn)
                    cmd.BindByName = True
                    cmd.Parameters.Add("PRODUCT_SERNO", OracleDbType.Int32).Value = tmpSerno
                    cmd.Parameters.Add("DATEFROM", OracleDbType.Varchar2).Value = dateFrom & " 00:00:00"
                    cmd.Parameters.Add("DATETO", OracleDbType.Varchar2).Value = dateTo & " 23:59:59"

                    Using dr As OracleDataReader = cmd.ExecuteReader()
                        If dr.Read() Then
                            totalQuantity = CInt(dr(0))
                            totalAmount = CDbl(dr(1))
                        End If
                    End Using
                End Using
            End If

            AddGridRows(1)

            dgvReports.Rows.Add(
            dtpFrom.Text,
            dtpTo.Text,
            cmbNoBarcode.Text,
            "N/A",
            totalQuantity,
            totalAmount.ToString("N2"),
            "N/A",
            "N/A")

            txtBoxBarcode.Focus()
            btnPrint.Visible = True

        Catch ex As Exception
            CreateExceptionFile(ex.Message, sql)
            MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            FormatDataGrid()
        End Try
    End Sub

    Private Sub AddGridRows(ByVal reportType As Integer)
        If reportType = 1 Then
            dgvReports.ColumnCount = 8

            dgvReports.Columns(0).Name = FROM_DATE
            dgvReports.Columns(0).Width = 160

            dgvReports.Columns(1).Name = "Έως"
            dgvReports.Columns(1).Width = 160

            dgvReports.Columns(2).Name = "Περιγραφή"
            dgvReports.Columns(2).Width = 220

            dgvReports.Columns(3).Name = "Barcode"
            dgvReports.Columns(3).Width = 100

            dgvReports.Columns(4).Name = "Πωλήσεις"
            dgvReports.Columns(4).Width = 70

            dgvReports.Columns(5).Name = "Ποσό Πωλήσεων"
            dgvReports.Columns(5).Width = 80

            dgvReports.Columns(6).Name = "Διαθέσιμη Ποσότητα"
            dgvReports.Columns(6).Width = 80

            dgvReports.Columns(7).Name = "Ποσότητα Αποθήκης"
            dgvReports.Columns(7).Width = 80
        End If
        FormatDataGrid()
    End Sub

    Private Sub FormatDataGrid()
        If dgvReports.Rows.Count > 0 Then
            dgvReports.FirstDisplayedScrollingRowIndex = dgvReports.Rows.Count - 1
        End If
    End Sub

    Private Sub ChkBoxSalesPerSupplier_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkBoxSalesPerSupplier.CheckedChanged
        If rdbSupplierPr.Checked Then
            If chkBoxSalesPerSupplier.Checked Then
                ShowDateFields(True)
            Else
                ShowDateFields(False)
                lblTotalSalesAmount.Text = ""
            End If
        End If
    End Sub

    Private Sub ShowDateFields(ByVal show As Boolean)
        If show Then
            lblFromDate.Visible = True
            lblToDate.Visible = True
            dtpFrom.Visible = True
            dtpTo.Visible = True
        Else
            lblFromDate.Visible = False
            lblToDate.Visible = False
            dtpFrom.Visible = False
            dtpTo.Visible = False
        End If
    End Sub

    Private Function GetZUuid(ByVal tmpFrom As Date) As String
        Dim WhoAmI As String = "frmReportsGetZUuid"
        Dim sql As String = ""
        Dim zuuid As String = ""
        Dim tmpDate As String = tmpFrom.Day & "-" & FindMonth(tmpFrom.Month.ToString()) & "-" & tmpFrom.Year

        Try
            If SqlLite Then

                Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                    sqliteConn.Open()
                    sql = "SELECT IFNULL(z_uuid,'') z_uuid
                          FROM z_report
                          WHERE z_date=@ZDATE
                          AND kioskid=@KIOSKID"

                    Using cmd As New SQLiteCommand(sql, sqliteConn)
                        cmd.Parameters.AddWithValue("@ZDATE", tmpDate)
                        cmd.Parameters.AddWithValue("@KIOSKID", kioskId)

                        Using dr As SQLiteDataReader = cmd.ExecuteReader()
                            If dr.Read() Then
                                zuuid = CStr(dr("z_uuid"))
                            Else
                                If tmpFrom.Date = startDate.Date Then
                                    sql = "INSERT INTO z_report
                                       (z_date, z_uuid, kioskid)
                                       VALUES
                                       (@ZDATE,1,@KIOSKID)"

                                    Using insertCmd As New SQLiteCommand(sql, sqliteConn)
                                        insertCmd.Parameters.AddWithValue("@ZDATE", tmpDate)
                                        insertCmd.Parameters.AddWithValue("@KIOSKID", kioskId)
                                        insertCmd.ExecuteNonQuery()
                                    End Using
                                    zuuid = Guid.NewGuid().ToString("N").ToUpper()
                                Else
                                    Dim tmpFromMinus1 As Date = tmpFrom.AddDays(-1)
                                    Dim tmpDateMinus1 As String = tmpFromMinus1.Day & "-" & FindMonth(tmpFromMinus1.Month.ToString()) & "-" & tmpFromMinus1.Year

                                    sql = "SELECT z_uuid
                                            FROM z_report
                                            WHERE z_date=@ZDATE
                                                AND kioskid=@KIOSKID"

                                    Using prevCmd As New SQLiteCommand(sql, sqliteConn)
                                        prevCmd.Parameters.AddWithValue("@ZDATE", tmpDateMinus1)
                                        prevCmd.Parameters.AddWithValue("@KIOSKID", kioskId)

                                        Using prevDr As SQLiteDataReader = prevCmd.ExecuteReader()
                                            If prevDr.Read() Then
                                                zuuid = Guid.NewGuid().ToString("N").ToUpper()
                                                sql = "INSERT INTO z_report
                                                       (z_date,z_uuid,kioskid)
                                                       VALUES
                                                       (@ZDATE,@ZUUID,@KIOSKID)"

                                                Using insertCmd As New SQLiteCommand(sql, sqliteConn)
                                                    insertCmd.Parameters.AddWithValue("@ZDATE", tmpDate)
                                                    insertCmd.Parameters.AddWithValue("@ZUUID", zuuid)
                                                    insertCmd.Parameters.AddWithValue("@KIOSKID", kioskId)
                                                    insertCmd.ExecuteNonQuery()
                                                End Using
                                            End If
                                        End Using
                                    End Using
                                End If
                            End If
                        End Using
                    End Using
                End Using
            End If
        Catch ex As Exception
            CreateExceptionFile(WhoAmI + " " + ex.Message, sql)
            MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return zuuid
    End Function

    Private Function GetZseq(ByVal tmpFrom As Date) As Integer
        Dim WhoAmI As String = "GetZseq"
        Dim sql As String = ""
        Dim zseq As Integer = -1
        Dim tmpDate As String = tmpFrom.Day & "-" & FindMonth(tmpFrom.Month.ToString()) & "-" & tmpFrom.Year

        Try
            sql = "SELECT z_seq
                      FROM z_report
                      WHERE z_date=:ZDATE"

            Using cmd As New OracleCommand(sql, conn)
                cmd.Parameters.Add("ZDATE", OracleDbType.Varchar2).Value = tmpDate
                Using dr As OracleDataReader = cmd.ExecuteReader()
                    If dr.Read() Then
                        zseq = CInt(dr("z_seq"))
                    Else
                        If tmpFrom.Date = startDate.Date Then
                            sql = "INSERT INTO z_report
                                       (z_date,z_seq)
                                       VALUES
                                       (:ZDATE,1)"

                            Using insertCmd As New OracleCommand(sql, conn)
                                insertCmd.Parameters.Add("ZDATE", OracleDbType.Varchar2).Value = tmpDate
                                insertCmd.ExecuteNonQuery()
                            End Using
                            zseq = 1
                        Else
                            Dim tmpFromMinus1 As Date = tmpFrom.AddDays(-1)
                            Dim tmpDateMinus1 As String = tmpFromMinus1.Day & "-" & FindMonth(tmpFromMinus1.Month.ToString()) & "-" & tmpFromMinus1.Year

                            sql = "SELECT z_seq
                                       FROM z_report
                                       WHERE z_date=:ZDATE"

                            Using prevCmd As New OracleCommand(sql, conn)
                                prevCmd.Parameters.Add("ZDATE", OracleDbType.Varchar2).Value = tmpDateMinus1

                                Using prevDr As OracleDataReader = prevCmd.ExecuteReader()
                                    If prevDr.Read() Then
                                        zseq = CInt(prevDr("z_seq")) + 1
                                        sql = "INSERT INTO z_report
                                               (z_date,z_seq)
                                               VALUES
                                               (:ZDATE,:ZSEQ)"

                                        Using insertCmd As New OracleCommand(sql, conn)
                                            insertCmd.Parameters.Add("ZDATE", OracleDbType.Varchar2).Value = tmpDate
                                            insertCmd.Parameters.Add("ZSEQ", OracleDbType.Int32).Value = zseq
                                            insertCmd.ExecuteNonQuery()
                                        End Using
                                    End If
                                End Using
                            End Using
                        End If
                    End If
                End Using
            End Using
        Catch ex As Exception
            CreateExceptionFile(WhoAmI + " " + ex.Message, Sql)
            MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return zseq
    End Function

    Private Sub rdbSessions_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbSessions.CheckedChanged
        If rdbSessions.Checked Then
            setVisibleFields(LOGIN_HISTORY)
        End If
    End Sub

    Private Sub rdbSalesPerCategory_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbSalesPerCategory.CheckedChanged
        If rdbSalesPerCategory.Checked Then
            setVisibleFields(SALES_PER_CATEGORY)
        End If
    End Sub

    Private Sub Export_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim xlApp As Microsoft.Office.Interop.Excel.Application = Nothing
        Dim xlWorkBook As Microsoft.Office.Interop.Excel.Workbook = Nothing
        Dim xlWorkSheet As Microsoft.Office.Interop.Excel.Worksheet = Nothing

        Try
            xlApp = New Microsoft.Office.Interop.Excel.Application()
            xlWorkBook = xlApp.Workbooks.Add()
            xlWorkSheet = CType(xlWorkBook.Sheets(1), Microsoft.Office.Interop.Excel.Worksheet)

            ' Export headers
            For col As Integer = 0 To dgvReports.Columns.Count - 1
                xlWorkSheet.Cells(1, col + 1) = dgvReports.Columns(col).HeaderText
            Next

            ' Export data
            For row As Integer = 0 To dgvReports.RowCount - 2
                For col As Integer = 0 To dgvReports.Columns.Count - 1
                    Dim cellValue = dgvReports(col, row).Value
                    xlWorkSheet.Cells(row + 2, col + 1) = If(cellValue IsNot Nothing, cellValue.ToString(), "")
                Next
            Next

            Dim filePath As String = "C:\vbexcel.xlsx"
            xlWorkSheet.SaveAs(filePath)
            xlWorkBook.Close(SaveChanges:=False)
            xlApp.Quit()

            MessageBox.Show("Η αναφορά αποθηκεύτηκε στο " & filePath, "Αποθήκευση αναφοράς", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show("Σφάλμα κατά την εξαγωγή: " & ex.Message, "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            ReleaseObject(xlWorkSheet)
            ReleaseObject(xlWorkBook)
            ReleaseObject(xlApp)
        End Try
    End Sub

    Private Sub ReleaseObject(ByVal obj As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
        Finally
            GC.Collect()
        End Try
    End Sub

End Class