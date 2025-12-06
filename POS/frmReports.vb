Imports Oracle.DataAccess.Client
Imports System.Data.OleDb
Imports Microsoft.Office.Interop

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

    Private Sub frmReports_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        frmMain.Show()
    End Sub

    Private Sub frmReports_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub frmReports_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        rdbSalesPerProduct.Checked = True
        txtBoxBarcode.Focus()

        If isAdmin Or canViewReports Then
            rdbSalesPerProduct.Visible = True
            rdbSalesPerVAT.Visible = True
            rdbXReport.Visible = True
            rdbZReport.Visible = True
            rdbSalesPerCategory.Visible = True
            'rdbQuantity.Visible = True
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
            rdbQuantity.Visible = False
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

    Private Sub setVisibleFields(ByVal reportType As String)
        clearGridAndSetInvisible()
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

        If reportType.Equals(QNT_HISTORY) Or reportType.Equals(SALES_PER_VAT) Or reportType.Equals(X_REPORT) Or _
           reportType.Equals(Z_REPORT) Or reportType.Equals(HOURS_PER_USER) Or reportType.Equals(PAYMENTS) Or _
            reportType.Equals(LOGIN_HISTORY) Or reportType.Equals(SALES_PER_CATEGORY) Then
            btnSearch.Visible = True
        Else
            btnSearch.Visible = False
        End If

        If reportType.Equals(QNT_HISTORY) Or reportType.Equals(SALES_PER_PRODUCT) Or reportType.Equals(SALES_PER_VAT) Or _
           reportType.Equals(X_REPORT) Or reportType.Equals(Z_REPORT) Or reportType.Equals(HOURS_PER_USER) Or reportType.Equals(PAYMENTS) _
           Or reportType.Equals(LOGIN_HISTORY) Or reportType.Equals(SALES_PER_CATEGORY) Then
            showDateFields(True)
        Else
            showDateFields(False)
        End If

        If reportType.Equals(QUANTITY_PER_PRODUCT) Or reportType.Equals(BUY_SELL) Then
            btnPrint.Visible = False
            'btnExportToExcel.Visible = False
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
            'btnExportToExcel.Visible = False
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
            'btnExportToExcel.Visible = False
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
            'btnExportToExcel.Visible = False
            cmbUsers.Visible = False
            lblTotalHoursOrAmount.Visible = False
            txtBoxTotalHoursOrPayments.Visible = False
            cmbNoBarcode.Visible = False

        ElseIf reportType.Equals(X_REPORT) Then
            btnPrint.Visible = False
            'btnExportToExcel.Visible = False
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
            'btnExportToExcel.Visible = False
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
            'btnExportToExcel.Visible = False
            cmbNoBarcode.Visible = False

        ElseIf reportType.Equals(LOGIN_HISTORY) Then
            btnPrint.Visible = False
            'btnExportToExcel.Visible = False
            lblBarcode.Visible = False
            txtBoxBarcode.Visible = False
            txtBoxBarcode.Visible = False
            btnClearBarcode.Visible = False
            lblTotalHoursOrAmount.Visible = False
            txtBoxTotalHoursOrPayments.Visible = False
            cmbNoBarcode.Visible = False
            cmbUsers.Visible = False
            'fillUsers(1)

        ElseIf reportType.Equals(SALES_PER_CATEGORY) Then
            btnPrint.Visible = False
            'btnExportToExcel.Visible = False
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

    Private Sub rdbSalesPerProduct_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbSalesPerProduct.CheckedChanged
        If rdbSalesPerProduct.Checked Then
            setVisibleFields(SALES_PER_PRODUCT)
        End If
    End Sub

    Private Sub rdbBuySellSupplier_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbBuySellSupplier.CheckedChanged
        If rdbBuySellSupplier.Checked Then
            setVisibleFields(BUY_SELL)
        End If
    End Sub

    Private Sub rdbQuantity_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbQuantity.CheckedChanged
        If rdbQuantity.Checked Then
            setVisibleFields(QUANTITY_PER_PRODUCT)
        End If
    End Sub

    Private Sub rdbXReport_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbXReport.CheckedChanged
        If rdbXReport.Checked Then
            setVisibleFields(X_REPORT)
        End If
    End Sub

    Private Sub rdbZReport_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbZReport.CheckedChanged
        If rdbZReport.Checked Then
            setVisibleFields(Z_REPORT)
        End If
    End Sub

    Private Sub rdbSalesPerVAT_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbSalesPerVAT.CheckedChanged
        If rdbSalesPerVAT.Checked Then
            setVisibleFields(SALES_PER_VAT)
        End If
    End Sub

    Private Sub rdbSupplierPr_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbSupplierPr.CheckedChanged
        If rdbSupplierPr.Checked Then
            setVisibleFields(PRODUCTS_PER_SUPPLIER)
        End If
    End Sub

    Private Sub rdbUsers_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbUsers.CheckedChanged
        If rdbUsers.Checked Then
            setVisibleFields(HOURS_PER_USER)
        End If
    End Sub

    Private Sub rdbPayments_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbPayments.CheckedChanged
        If rdbPayments.Checked Then
            setVisibleFields(PAYMENTS)
        End If
    End Sub

    Private Sub rdbQntHistory_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbQntHistory.CheckedChanged
        If rdbQntHistory.Checked Then
            setVisibleFields(QNT_HISTORY)
        End If
    End Sub

    Private Sub rdbProfit_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbProfit.CheckedChanged
        clearGridAndSetInvisible()

        lblBarcode.Visible = False
        txtBoxBarcode.Visible = False
        btnClearBarcode.Visible = False
        btnPrint.Visible = False
        'btnExportToExcel.Visible = False
        cmbSupplier.Visible = False
        cmbUsers.Visible = False
        lblTotalHoursOrAmount.Visible = False
        txtBoxTotalHoursOrPayments.Visible = False
        cmbNoBarcode.Visible = False

        lblFromDate.Visible = True
        dtpFrom.Visible = True
        lblToDate.Visible = True
        dtpTo.Visible = True
        btnSearch.Visible = True
    End Sub

    Private Sub clearGridAndSetInvisible()
        dgvReports.Rows.Clear()
        dgvReports.Columns.Clear()

        'lblBarcode.Visible = False
        'txtBoxBarcode.Visible = False
        'btnClearBarcode.Visible = False
        'btnPrint.Visible = False
        'cmbSupplier.Visible = False
        'cmbUsers.Visible = False
        'lblTotalHoursOrAmount.Visible = False
        'txtBoxTotalHoursOrPayments.Visible = False
        'cmbNoBarcode.Visible = False
        'lblFromDate.Visible = False
        'dtpFrom.Visible = False
        'lblToDate.Visible = False
        'dtpTo.Visible = False
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim cmd As New OracleCommand("", conn)
        Dim sql As String = ""

        Try
            If rdbSalesPerCategory.Checked Then
                dgvReports.Columns.Clear()
                dgvReports.Rows.Clear()

                Dim dateFrom As String = CStr(dtpFrom.Value.Day) & "-" & findMonth(CStr(dtpFrom.Value.Month)) & "-" & CStr(dtpFrom.Value.Year).Substring(2, 2)
                Dim dateTo As String = CStr(dtpTo.Value.Day) & "-" & findMonth(CStr(dtpTo.Value.Month)) & "-" & CStr(dtpTo.Value.Year).Substring(2, 2)

                sql = "select NVL(sum(amount),0) total from receipts_det " & _
                      "where created_on BETWEEN " & _
                      "to_timestamp('" & dateFrom & " 00:00:00', 'DD-MON-YY HH24:MI:SS') AND " & _
                      "to_timestamp('" & dateTo & " 23:59:59', 'DD-MON-YY HH24:MI:SS')"

                Dim categoryName = "Όλες"
                Dim supplierName = "Όλοι"
                If cmbCategories.SelectedIndex <> -1 Then
                    If Not cmbCategories.SelectedItem.Equals("Όλες") Then
                        sql += " and product_serno in (select serno from products where CATEGORY_ID = '" & categoryUUIDs(cmbCategories.SelectedIndex) & "') "
                        categoryName = cmbCategories.SelectedItem
                    End If
                End If

                cmd = New OracleCommand(sql, conn)
                Dim total As Double = 0
                Using dr = cmd.ExecuteReader()
                    If dr.Read() Then
                        total = CStr(CDbl(dr(0)).ToString("#,##0.00"))
                    End If
                End Using

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
                If Not categoryName.Equals("Ολες") And cmbCategories.SelectedIndex > 0 Then
                    sql = "select s_name from suppliers " & _
                          "where uuid in (select supplier_id from products where serno in (" & _
                                            "select serno from products where CATEGORY_ID = '" & categoryUUIDs(cmbCategories.SelectedIndex) & "'))"

                    supplierName = ""
                    cmd = New OracleCommand(sql, conn)
                    Using dr = cmd.ExecuteReader()
                        While dr.Read()
                            supplierName += " " + CStr(dr(0))
                        End While
                    End Using                
                End If

                Dim row As String() = New String() {dtpFrom.Text, dtpTo.Text, total.ToString("N2"), categoryName, supplierName}
                dgvReports.Rows.Add(row)
                btnPrint.Visible = True
                'btnExportToExcel.Visible = True

            ElseIf rdbSalesPerVAT.Checked Then
                dgvReports.Columns.Clear()
                dgvReports.Rows.Clear()

                Dim dateFrom As String = CStr(dtpFrom.Value.Day) & "-" & findMonth(CStr(dtpFrom.Value.Month)) & "-" & CStr(dtpFrom.Value.Year).Substring(2, 2)
                Dim dateTo As String = CStr(dtpTo.Value.Day) & "-" & findMonth(CStr(dtpTo.Value.Month)) & "-" & CStr(dtpTo.Value.Year).Substring(2, 2)

                sql = "select NVL(sum(total_vat5),0), NVL(sum(total_vat19),0), NVL(sum(total_vat0),0), NVL(sum(total_vat3),0) from receipts " & _
                      "where created_on BETWEEN " & _
                      "to_timestamp('" & dateFrom & " 00:00:00', 'DD-MON-YY HH24:MI:SS') AND " & _
                      "to_timestamp('" & dateTo & " 23:59:59', 'DD-MON-YY HH24:MI:SS')"
                cmd = New OracleCommand(sql, conn)

                Dim totalVat0 As Double = 0
                Dim totalVat5 As Double = 0
                Dim totalVat19 As Double = 0
                Dim totalVat3 As Double = 0
                Using dr = cmd.ExecuteReader()
                    If dr.Read() Then
                        totalVat5 = CStr(CDbl(dr(0)).ToString("#,##0.00")) * (divideFactor5 / 100)
                        totalVat19 = CStr(CDbl(dr(1)).ToString("#,##0.00")) * (divideFactor19 / 100)
                        totalVat0 = CStr(CDbl(dr(2)).ToString("#,##0.00")) * (divideFactor0 / 100)
                        totalVat3 = CStr(CDbl(dr(3)).ToString("#,##0.00")) * (divideFactor3 / 100)
                    End If
                    dr.Close()
                End Using

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
                'btnExportToExcel.Visible = True

            ElseIf rdbXReport.Checked Then
                dgvReports.Columns.Clear()
                dgvReports.Rows.Clear()
                Dim dateFrom As String = CStr(dtpFrom.Value.Day) & "-" & findMonth(CStr(dtpFrom.Value.Month)) & "-" & CStr(dtpFrom.Value.Year).Substring(2, 2)
                Dim dateTo As String = CStr(dtpTo.Value.Day) & "-" & findMonth(CStr(dtpTo.Value.Month)) & "-" & CStr(dtpTo.Value.Year).Substring(2, 2)

                sql = "select from_date, to_date, u.username, total_receipts, total5percent, " & _
                      "       total19percent, initial_amt, payments, final_amt, NVL(description,''), total0percent, " & _
                      "       amount_laxeia, initialAmtLaxeia, amountvisa, finalamtlaxeia, total3percent " & _
                      "from x_report x " & _
                      "inner join users u on x.user_id = u.uuid " & _
                      "where (total_receipts > 0 or payments > 0) and created_on BETWEEN " & _
                      "to_timestamp('" & dateFrom & " 00:00:00', 'DD-MON-YY HH24:MI:SS') AND " & _
                      "to_timestamp('" & dateTo & " 23:59:59', 'DD-MON-YY HH24:MI:SS') "

                If cmbUsers.SelectedIndex <> -1 Then
                    If Not cmbUsers.SelectedItem.Equals("Όλοι") Then
                        sql += " and user_id = '" & userUUIDs(cmbUsers.SelectedIndex) & "' "
                    End If
                End If

                sql += " order by from_date, to_date"

                cmd = New OracleCommand(sql, conn)
                Using dr = cmd.ExecuteReader()
                    While dr.Read()

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

                        'dgvReports.Columns(13).Name = "Αναλυτική Κατάσταση"
                        'dgvReports.Columns(13).Width = 190

                        Dim total0percent As Double = CDbl(dr(10)) * (divideFactor0 / 100)
                        Dim total5percent As Double = CDbl(dr(4)) * (divideFactor5 / 100)
                        Dim total19percent As Double = CDbl(dr(5)) * (divideFactor19 / 100)
                        Dim initial_amt As Double = CDbl(dr(6))
                        Dim payments As Double = CDbl(dr(7))
                        Dim total3percent As Double = 0
                        If Not IsDBNull(dr(15)) Then
                            total3percent = CDbl(dr(15)) * (divideFactor3 / 100)
                        End If

                        Dim final_amt As Double = (total0percent + total3percent + total5percent + total19percent)
                        Dim amountLaxeia As Double = CDbl(dr(11))

                        Dim initialAmountLaxeia As Double = 0
                        If Not IsDBNull(dr(12)) Then
                            initialAmountLaxeia = CDbl(dr(12))
                        End If

                        Dim visaAmount As Double = 0
                        If Not IsDBNull(dr(13)) Then
                            visaAmount = CDbl(dr(13)) '* (divideFactor / 100)
                        End If

                        Dim salesDescription = ""
                        If Not dr.IsDBNull(9) Then
                            salesDescription = CStr(dr(9))
                        End If

                        Dim finalAmtLaxeia As Double = 0
                        If Not IsDBNull(dr(14)) Then
                            finalAmtLaxeia = CDbl(dr(14))
                        End If

                        Dim totalAmountDeliver As Double = (total0percent + total3percent + total5percent + total19percent + initial_amt) - payments - visaAmount

                        Dim row As String() = New String() {CStr(dr(0)), CStr(dr(1)), CStr(dr(2)), CInt(dr(3)), total0percent.ToString("N2"), total3percent.ToString("N2"), total5percent.ToString("N2"), total19percent.ToString("N2"), final_amt.ToString("N2"), initial_amt.ToString("N2"), payments.ToString("N2"), visaAmount.ToString("N2"), totalAmountDeliver.ToString("N2"), finalAmtLaxeia.ToString("N2")}
                        dgvReports.Rows.Add(row)
                    End While
                End Using
                btnPrint.Visible = True

            ElseIf rdbZReport.Checked Then
                clearGridAndSetInvisible()

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

                'If ((Not CStr(dtpFrom.Value.Day).Equals(CStr(dtpTo.Value.Day))) Or _
                '    (Not findMonth(CStr(dtpFrom.Value.Month)).Equals(findMonth(CStr(dtpTo.Value.Month)))) Or _
                '    (Not CStr(dtpFrom.Value.Year).Substring(2, 2).Equals(CStr(dtpTo.Value.Year).Substring(2, 2)))) Then
                'MessageBox.Show("Οι ημερομηνίες από-έως πρέπει να είναι οι ίδιες", "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error)
                'Exit Sub
                'End If

                Dim tmpFrom As Date = dtpFrom.Value.AddHours(-dtpFrom.Value.Hour)
                tmpFrom = tmpFrom.AddMinutes(-tmpFrom.Minute)
                tmpFrom = tmpFrom.AddSeconds(-tmpFrom.Second)

                Dim tmpTo As Date = dtpTo.Value.AddHours(-dtpTo.Value.Hour)
                tmpTo = tmpTo.AddMinutes(-tmpTo.Minute)
                tmpTo = tmpTo.AddSeconds(-tmpTo.Second)

                Dim dateFrom As String = CStr(tmpFrom.Day) & "-" & findMonth(CStr(tmpFrom.Month)) & "-" & CStr(tmpFrom.Year).Substring(2, 2)
                'Dim dateFrom As String = CStr(dtpFrom.Value.Day) & "-" & findMonth(CStr(dtpFrom.Value.Month)) & "-" & CStr(dtpFrom.Value.Year).Substring(2, 2)
                'Dim dateTo As String = CStr(dtpTo.Value.Day) & "-" & findMonth(CStr(dtpTo.Value.Month)) & "-" & CStr(dtpTo.Value.Year).Substring(2, 2)

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
                    Dim zDate As String

                    Dim tmpDate As String = CStr(tmpFrom.Day) & "-" & findMonth(CStr(tmpFrom.Month)) & "-" & CStr(tmpFrom.Year)
                    sql = "select z_seq, z_date, total_receipts, total_amount0, total_amount5, total_amount19, total_amount, nvl(total_amount3,0) from z_report " & _
                          "where z_date='" & tmpDate & "'"
                    cmd = New OracleCommand(sql, conn)
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

                            Dim row As String() = New String() {zseq, zDate, zDate, totalReceipts, totalVat0.ToString("N2"), totalVat3.ToString("N2"), totalVat5.ToString("N2"), _
                                    totalVat19.ToString("N2"), totalAll.ToString("N2")}
                            dgvReports.Rows.Add(row)

                        Else
                            sql = "select NVL(sum(total_vat5),0), NVL(sum(total_vat19),0), NVL(sum(total_vat0),0), NVL(sum(total_vat3),0), count(*) from receipts " & _
                              "where created_on BETWEEN " & _
                              "to_timestamp('" & dateFrom & " 00:00:00', 'DD-MON-YY HH24:MI:SS') AND " & _
                              "to_timestamp('" & dateFrom & " 23:59:59', 'DD-MON-YY HH24:MI:SS')"

                            cmd = New OracleCommand(sql, conn)
                            Using drInner = cmd.ExecuteReader()
                                If drInner.Read() Then
                                    totalVat5 = CStr(CDbl(drInner(0)).ToString("#,##0.00")) * (divideFactor5 / 100)
                                    totalVat19 = CStr(CDbl(drInner(1)).ToString("#,##0.00")) * (divideFactor19 / 100)
                                    totalVat0 = CStr(CDbl(drInner(2)).ToString("#,##0.00")) * (divideFactor0 / 100)
                                    totalVat3 = CStr(CDbl(drInner(3)).ToString("#,##0.00")) * (divideFactor3 / 100)
                                    totalReceipts = CInt(drInner(4))
                                End If
                            End Using
                            zseq = getZseq(tmpFrom)

                            If zseq = -1 Then
                                MessageBox.Show("Δεν έχετε εκτυπώσει όλα τα Z-Report των προηγούμενων ημερομηνιών", "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                Exit Sub
                            End If

                            Dim row As String() = New String() {zseq, tmpFrom.Day & "-" & tmpFrom.Month & "-" & tmpFrom.Year, _
                                                            tmpFrom.Day & "-" & tmpFrom.Month & "-" & tmpFrom.Year, _
                                                            totalReceipts, totalVat0.ToString("N2"), totalVat5.ToString("N2"), _
                                                            totalVat19.ToString("N2"), totalVat3.ToString("N2"), (totalVat0 + totalVat3 + totalVat5 + totalVat19).ToString("N2")}
                            dgvReports.Rows.Add(row)

                            sql = "update z_report set total_receipts = " & totalReceipts & ", " & _
                                  "                    total_amount0 = " & totalVat0 & ", " & _
                                  "                    total_amount3 = " & totalVat3 & ", " & _
                                  "                    total_amount5 = " & totalVat5 & ", " & _
                                  "                    total_amount19 = " & totalVat19 & ", " & _
                                  "                    total_amount = " & (totalVat0 + totalVat3 + totalVat5 + totalVat19) & " " & _
                                  "where z_seq = " & zseq & ""
                            cmd = New OracleCommand(sql, conn)
                            cmd.ExecuteNonQuery()
                        End If

                        btnPrint.Visible = True
                    End Using
                    tmpFrom = tmpFrom.AddDays(1)
                    dateFrom = CStr(tmpFrom.Day) & "-" & findMonth(CStr(tmpFrom.Month)) & "-" & CStr(tmpFrom.Year).Substring(2, 2)
                End While

            ElseIf rdbUsers.Checked Then
                clearGridAndSetInvisible()

                If cmbUsers.SelectedIndex = -1 Then
                    MessageBox.Show("Δεν έχετε επιλέξει χρήστη", "Επιλογή Χρήστη", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If

                Dim dateFrom As String = CStr(dtpFrom.Value.Day) & "-" & findMonth(CStr(dtpFrom.Value.Month)) & "-" & CStr(dtpFrom.Value.Year).Substring(2, 2)
                Dim dateTo As String = CStr(dtpTo.Value.Day) & "-" & findMonth(CStr(dtpTo.Value.Month)) & "-" & CStr(dtpTo.Value.Year).Substring(2, 2)

                sql = "select login_when, logout_when from sessions " & _
                      "where user_id = '" & userUUIDs(cmbUsers.SelectedIndex) & "' " & _
                      "and login_when BETWEEN " & _
                      "to_timestamp('" & dateFrom & " 00:00:00', 'DD-MON-YY HH24:MI:SS') AND " & _
                      "to_timestamp('" & dateTo & " 23:59:59', 'DD-MON-YY HH24:MI:SS') " & _
                      "order by login_when asc"
                cmd = New OracleCommand(sql, conn)

                dgvReports.ColumnCount = 3

                dgvReports.Columns(0).Name = FROM_DATE
                dgvReports.Columns(0).Width = 350

                dgvReports.Columns(1).Name = "Έως"
                dgvReports.Columns(1).Width = 350

                dgvReports.Columns(2).Name = "Διάρκεια (σε λεπτά)"
                dgvReports.Columns(2).Width = 350
                Dim totalHours As Double = 0
                Using dr = cmd.ExecuteReader()
                    While dr.Read
                        Dim loginWhen As Date = Now
                        If Not IsDBNull(dr(0)) Then
                            loginWhen = CDate(dr(0))
                        End If

                        Dim logoutWhen As Date = Now
                        If Not IsDBNull(dr(1)) Then
                            logoutWhen = CDate(dr(1))
                        End If

                        Dim dateDifference As Long = DateDiff(DateInterval.Minute, loginWhen, logoutWhen)
                        totalHours += (dateDifference / 60)
                        Dim row As String() = New String() {loginWhen, logoutWhen, dateDifference}
                        dgvReports.Rows.Add(row)
                    End While
                End Using

                txtBoxTotalHoursOrPayments.Text = totalHours.ToString("N2")
                btnPrint.Visible = True
                'btnExportToExcel.Visible = True

            ElseIf rdbProfit.Checked Then
                clearGridAndSetInvisible()
                Dim dateFrom As String = CStr(dtpFrom.Value.Day) & "-" & findMonth(CStr(dtpFrom.Value.Month)) & "-" & CStr(dtpFrom.Value.Year).Substring(2, 2)
                Dim dateTo As String = CStr(dtpTo.Value.Day) & "-" & findMonth(CStr(dtpTo.Value.Month)) & "-" & CStr(dtpTo.Value.Year).Substring(2, 2)

                sql = "select sum(rd.AMOUNT) TOTAL, sum(NVL(p.AMT_PROFIT,0)) PROFIT " & _
                      "from receipts_det rd " & _
                      "inner join products p on rd.PRODUCT_SERNO = p.SERNO " & _
                      "where CREATED_ON BETWEEN " & _
                      "to_timestamp('" & dateFrom & " 00:00:00', 'DD-MON-YY HH24:MI:SS') AND " & _
                      "to_timestamp('" & dateTo & " 23:59:59', 'DD-MON-YY HH24:MI:SS') "

                cmd = New OracleCommand(sql, conn)

                dgvReports.ColumnCount = 4

                dgvReports.Columns(0).Name = FROM_DATE
                dgvReports.Columns(0).Width = 150

                dgvReports.Columns(1).Name = "Έως"
                dgvReports.Columns(1).Width = 150

                dgvReports.Columns(2).Name = "Πωλήσεις"
                dgvReports.Columns(2).Width = 150

                dgvReports.Columns(3).Name = "Καθαρό Κέρδος"
                dgvReports.Columns(3).Width = 150
                Dim totalAmt As Double = 0
                Dim totalProfit As Double = 0
                Using dr = cmd.ExecuteReader()
                    While dr.Read
                        If Not IsDBNull(dr(0)) Then
                            totalAmt = CDbl(dr(0))
                        End If

                        If Not IsDBNull(dr(1)) Then
                            totalProfit = CDbl(dr(1))
                        End If

                        Dim row As String() = New String() {dateFrom, dateTo, totalAmt.ToString("N2"), totalProfit.ToString("N2")}
                        dgvReports.Rows.Add(row)
                    End While
                End Using
                btnPrint.Visible = True
            ElseIf rdbPayments.Checked Then
                dgvReports.Columns.Clear()
                dgvReports.Rows.Clear()
                Dim dateFrom As String = CStr(dtpFrom.Value.Day) & "-" & findMonth(CStr(dtpFrom.Value.Month)) & "-" & CStr(dtpFrom.Value.Year).Substring(2, 2)
                Dim dateTo As String = CStr(dtpTo.Value.Day) & "-" & findMonth(CStr(dtpTo.Value.Month)) & "-" & CStr(dtpTo.Value.Year).Substring(2, 2)

                sql = "select p.CREATED_ON, p.AMOUNT, u.USERNAME, NVL(p.vat, 'N/A'), NVL(p.amountvat,0), NVL(p.vat,-1), " & _
                      "NVL(s.s_name, ' ') s_name, NVL(inv_number, ' ') inv_number " & _
                      "from payments p " & _
                      "inner join users u on p.CREATED_BY = u.UUID " & _
                      "left join suppliers s on p.supplier_id = s.uuid " & _
                      "where p.created_on BETWEEN " & _
                      "to_timestamp('" & dateFrom & " 00:00:00', 'DD-MON-YY HH24:MI:SS') AND " & _
                      "to_timestamp('" & dateTo & " 23:59:59', 'DD-MON-YY HH24:MI:SS') " & _
                      "order by p.created_on desc"
                cmd = New OracleCommand(sql, conn)
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
                Using dr = cmd.ExecuteReader()
                    While dr.Read()

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

                txtBoxTotalHoursOrPayments.Text = "€" & TruncateDecimal(totalAmount + totalVATamount, 3).ToString
                lblAmountVAT.Text = "Φ.Π.Α για Επιστροφή: €" + TruncateDecimal(totalVATamount, 3).ToString + vbNewLine + _
                "Πληρωμές (με Φ.Π.Α) 0% : " + TruncateDecimal(totalPaymentsVat0 + totalVat0, 3).ToString + " , Φ.Π.Α. 0%: " + TruncateDecimal(totalVat0, 3).ToString + vbNewLine + _
                "Πληρωμές (με Φ.Π.Α) 3% : " + TruncateDecimal(totalPaymentsVat3 + totalVat3, 3).ToString + " , Φ.Π.Α. 3%: " + TruncateDecimal(totalVat3, 3).ToString + vbNewLine + _
                "Πληρωμές (με Φ.Π.Α) 5% : " + TruncateDecimal(totalPaymentsVat5 + totalVat5, 3).ToString + " , Φ.Π.Α. 5%: " + TruncateDecimal(totalVat5, 3).ToString + vbNewLine + _
                "Πληρωμές (με Φ.Π.Α) 19%: " + TruncateDecimal(totalPaymentsVat19 + totalVat19, 3).ToString + ", Φ.Π.Α. 19%: " + TruncateDecimal(totalVat19, 3).ToString
                btnPrint.Visible = True
                'btnExportToExcel.Visible = True

            ElseIf rdbQntHistory.Checked Then
                dgvReports.Columns.Clear()
                dgvReports.Rows.Clear()

                Dim dateFrom As String = CStr(dtpFrom.Value.Day) & "-" & findMonth(CStr(dtpFrom.Value.Month)) & "-" & CStr(dtpFrom.Value.Year).Substring(2, 2)
                Dim dateTo As String = CStr(dtpTo.Value.Day) & "-" & findMonth(CStr(dtpTo.Value.Month)) & "-" & CStr(dtpTo.Value.Year).Substring(2, 2)

                sql = "select (select barcode from BARCODES where product_serno = pa.PRODUCT_SERNO and rownum < 2) barcode, " & _
                      "p.DESCRIPTION, pa.PREV_QUANTITY, pa.NEW_QUANTITY, nvl(pa.PREV_ST_QNT,0), nvl(pa.NEW_ST_QNT,0), pa.MODIFIED_WHEN, " & _
                      "u.USERNAME, pa.OLD_PRICE, pa.NEW_PRICE " & _
                      "from products_audit pa " & _
                      "inner join products p on pa.PRODUCT_SERNO = p.serno " & _
                      "inner join users u on u.UUID = pa.MODIFIED_BY " & _
                      "where modified_when BETWEEN " & _
                      "to_timestamp('" & dateFrom & " 00:00:00', 'DD-MON-YY HH24:MI:SS') AND " & _
                      "to_timestamp('" & dateTo & " 23:59:59', 'DD-MON-YY HH24:MI:SS') order by pa.MODIFIED_WHEN desc"
                cmd = New OracleCommand(sql, conn)
                dgvReports.ColumnCount = 10

                'dgvReports.Columns(0).Name = FROM_DATE
                'dgvReports.Columns(0).Width = 150

                'dgvReports.Columns(1).Name = TO_DATE
                'dgvReports.Columns(1).Width = 150

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
                Using dr = cmd.ExecuteReader()
                    While dr.Read()
                        Dim row As String() = New String() {dr(0), dr(1), dr(2), dr(3), dr(4), dr(5), dr(6), dr(7), dr(8), dr(9)}
                        dgvReports.Rows.Add(row)
                    End While
                End Using

                btnPrint.Visible = True
                'btnExportToExcel.Visible = True

            ElseIf rdbSessions.Checked Then
                dgvReports.Columns.Clear()
                dgvReports.Rows.Clear()
                Dim dateFrom As String = CStr(dtpFrom.Value.Day) & "-" & findMonth(CStr(dtpFrom.Value.Month)) & "-" & CStr(dtpFrom.Value.Year).Substring(2, 2)
                Dim dateTo As String = CStr(dtpTo.Value.Day) & "-" & findMonth(CStr(dtpTo.Value.Month)) & "-" & CStr(dtpTo.Value.Year).Substring(2, 2)

                sql = "select u.username, login_when, logout_when, machine_name " & _
                      "from sessions s " & _
                      "inner join users u on s.user_id = u.uuid " & _
                      "where login_when BETWEEN " & _
                      "to_timestamp('" & dateFrom & " 00:00:00', 'DD-MON-YY HH24:MI:SS') AND " & _
                      "to_timestamp('" & dateTo & " 23:59:59', 'DD-MON-YY HH24:MI:SS') order by login_when"

                cmd = New OracleCommand(sql, conn)
                Using dr = cmd.ExecuteReader()
                    While dr.Read()

                        dgvReports.ColumnCount = 4

                        dgvReports.Columns(0).Name = "Χρήστης"
                        dgvReports.Columns(0).Width = 100

                        dgvReports.Columns(1).Name = "Από"
                        dgvReports.Columns(1).Width = 150

                        dgvReports.Columns(2).Name = "Έως"
                        dgvReports.Columns(2).Width = 150

                        dgvReports.Columns(3).Name = "Μηχανή"
                        dgvReports.Columns(3).Width = 100

                        Dim logoutWhen As String
                        If IsDBNull(dr(2)) Then
                            logoutWhen = ""
                        Else
                            logoutWhen = CStr(dr(2))
                        End If

                        Dim row As String() = New String() {CStr(dr(0)), CStr(dr(1)), logoutWhen, CStr(dr(3))}
                        dgvReports.Rows.Add(row)
                    End While
                End Using

                
                btnPrint.Visible = False
                'btnExportToExcel.Visible = False
            End If
        Catch ex As Exception
            createExceptionFile(ex.Message, " " & sql)
            MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            cmd.Dispose()
            formatDataGrid()
        End Try
    End Sub

    Private Sub txtBoxBarcode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBoxBarcode.TextChanged
        Dim found As Boolean = False
        Dim sql As String = ""
        Dim cmd As New OracleCommand(sql, conn)
        Try
            'Sales per product within a period
            If rdbSalesPerProduct.Checked Then
                sql = "select p.serno, p.sell_amt, p.avail_quantity, NVL(p.stock_quantity,0) from products p " & _
                      "where p.serno = (select b.product_serno from BARCODES b " & _
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
                    Dim dateFrom As String = CStr(dtpFrom.Value.Day) & "-" & findMonth(CStr(dtpFrom.Value.Month)) & "-" & CStr(dtpFrom.Value.Year).Substring(2, 2)
                    Dim dateTo As String = CStr(dtpTo.Value.Day) & "-" & findMonth(CStr(dtpTo.Value.Month)) & "-" & CStr(dtpTo.Value.Year).Substring(2, 2)

                    sql = "select NVL(sum(quantity),0) from receipts_det " & _
                          "where product_serno = " & productSerno & " " & _
                          "and created_on BETWEEN " & _
                          "to_timestamp('" & dateFrom & " 00:00:00', 'DD-MON-YY HH24:MI:SS') AND " & _
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

                    sql = "select p.description from products p " & _
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

            'Quantity Per Product
            If rdbQuantity.Checked Then
                sql = "select p.description, p.avail_quantity, p.stock_quantity from products p " & _
                      "where p.serno = (select b.product_serno from BARCODES b " & _
                      "where UPPER(b.barcode) =  '" & txtBoxBarcode.Text.ToUpper & "')"

                cmd = New OracleCommand(sql, conn)
                cmd.CommandType = CommandType.Text
                dgvReports.ColumnCount = 4

                dgvReports.Columns(0).Name = "Περιγραφή"
                dgvReports.Columns(0).Width = 620

                dgvReports.Columns(1).Name = "Barcode"
                dgvReports.Columns(1).Width = 100

                dgvReports.Columns(2).Name = "Διαθέσιμη Ποσότητα"
                dgvReports.Columns(2).Width = 129

                dgvReports.Columns(3).Name = "Ποσότητα Αποθήκης"
                dgvReports.Columns(3).Width = 100

                Using dr = cmd.ExecuteReader()
                    If dr.Read() Then
                        Dim row As String() = New String() {dr(0), txtBoxBarcode.Text, dr(1), dr(2)}
                        dgvReports.Rows.Add(row)
                        txtBoxBarcode.Clear()
                    End If
                End Using
            End If

            'Buy/Sell/Supplier/ Per Product
            If rdbBuySellSupplier.Checked Then
                sql = "select p.description, s.s_name, s.phone_1, p.buy_amt, p.sell_amt " & _
                      "from products p " & _
                      "inner join suppliers s on p.supplier_id = s.uuid " & _
                      "where p.serno = (select b.product_serno from barcodes b " & _
                      "where UPPER(b.barcode) =  '" & txtBoxBarcode.Text.ToUpper & "')"

                cmd = New OracleCommand(sql, conn)
                cmd.CommandType = CommandType.Text
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
                Using dr = cmd.ExecuteReader()
                    If dr.Read() Then
                        Dim tmpBuyAmt As Double = CDbl(dr(3))
                        Dim tmpSellAmt As Double = CDbl(dr(4))
                        Dim row As String() = New String() {dr(0), txtBoxBarcode.Text, dr(1), dr(2), tmpBuyAmt.ToString("N2"), tmpSellAmt.ToString("N2")}
                        dgvReports.Rows.Add(row)
                        txtBoxBarcode.Clear()
                    End If
                End Using
            End If
            btnPrint.Visible = True
            'btnExportToExcel.Visible = True
        Catch ex As Exception
            createExceptionFile(ex.Message, " " & sql)
            MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            cmd.Dispose()
        End Try
        formatDataGrid()
        txtBoxBarcode.Focus()
    End Sub

    Private Sub dgvReports_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvReports.CellClick
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
        formatDataGrid()
        txtBoxBarcode.Focus()
    End Sub

    Private Sub btnClearBarcode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearBarcode.Click
        txtBoxBarcode.Clear()
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
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
                'xMargin += 20
                'e.Graphics.DrawString("Αριθμός Αποδείξεων: " & dgvReports.Rows(i).Cells("Αποδείξεις").Value, reportFont, Brushes.Black, 0, xMargin)
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
                'e.Graphics.DrawString("Αναλυτική Κατάσταση: " & dgvReports.Rows(i).Cells("Αναλυτική Κατάσταση").Value, reportFont, Brushes.Black, 0, xMargin)
                'xMargin += 80
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

        ElseIf rdbQuantity.Checked = True Then
            For i = 0 To dgvReports.Rows.Count - 1
                e.Graphics.DrawString("Περιγραφή: " & dgvReports.Rows(i).Cells("Περιγραφή").Value, reportFont, Brushes.Black, 0, xMargin)
                xMargin += 20
                e.Graphics.DrawString("Barcode: " & dgvReports.Rows(i).Cells("Barcode").Value, reportFont, Brushes.Black, 0, xMargin)
                xMargin += 20
                e.Graphics.DrawString("Διαθέσιμη Ποσότητα: " & dgvReports.Rows(i).Cells("Διαθέσιμη Ποσότητα").Value, reportFont, Brushes.Black, 0, xMargin)
                xMargin += 20
                e.Graphics.DrawString("Ποσότητα Αποθήκης: " & dgvReports.Rows(i).Cells("Ποσότητα Αποθήκης").Value, reportFont, Brushes.Black, 0, xMargin)
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

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Dispose()
    End Sub

    Private Sub fillSuppliers()
        Dim cmd As New OracleCommand("", conn)
        Try
            cmd = New OracleCommand(Q_GET_SUPPLIERS, conn)
            cmd.CommandType = CommandType.Text
            Dim dr As OracleDataReader = cmd.ExecuteReader()
            supplierId = ""
            supplierUUIDs.Clear()
            cmbSupplier.Items.Clear()
            While dr.Read()
                supplierUUIDs.Add(dr("uuid"))
                cmbSupplier.Items.Add(dr("s_name"))
            End While
            dr.Close()
        Catch ex As Exception
            createExceptionFile(ex.Message, " " & Q_GET_SUPPLIERS)
            MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            cmd.Dispose()
        End Try
    End Sub

    Private Sub fillUsers(ByVal addAll As Integer)
        Dim cmd As New OracleCommand("", conn)
        Try
            cmd = New OracleCommand(Q_GET_USERS, conn)
            cmd.CommandType = CommandType.Text
            Dim dr As OracleDataReader = cmd.ExecuteReader()
            userId = ""
            userUUIDs.Clear()
            cmbUsers.Items.Clear()

            If addAll = 1 Then
                userUUIDs.Add(-1)
                cmbUsers.Items.Add("Όλοι")
            End If

            While dr.Read()
                userUUIDs.Add(dr("uuid"))
                cmbUsers.Items.Add(dr("username"))
            End While
            dr.Close()
        Catch ex As Exception
            createExceptionFile(ex.Message, " " & Q_GET_USERS)
            MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            cmd.Dispose()
        End Try
    End Sub

    Private Sub fillCategories(ByVal addAll As Integer)
        Dim cmd As New OracleCommand("", conn)
        Try
            cmd = New OracleCommand(Q_GET_CATEGORIES, conn)
            cmd.CommandType = CommandType.Text
            Dim dr As OracleDataReader = cmd.ExecuteReader()
            categoryId = ""
            categoryUUIDs.Clear()
            cmbCategories.Items.Clear()

            If addAll = 1 Then
                categoryUUIDs.Add(-1)
                cmbCategories.Items.Add("Όλες")
            End If

            While dr.Read()
                categoryUUIDs.Add(dr("uuid"))
                cmbCategories.Items.Add(dr("description"))
            End While
            dr.Close()
        Catch ex As Exception
            createExceptionFile(ex.Message, " " & Q_GET_CATEGORIES)
            MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            cmd.Dispose()
        End Try
    End Sub

    Private Sub fillProductsNoBarcode()
        cmbNoBarcode.Items.Clear()
        cmbNoBarcode.Items.Add("")
        'cmbNoBarcode.Items.Add("Φ.Π.Α 0%")
        'cmbNoBarcode.Items.Add("Φ.Π.Α 3%")
        cmbNoBarcode.Items.Add("Φ.Π.Α 5%")
        cmbNoBarcode.Items.Add("Φ.Π.Α 19%")
    End Sub

    Private Sub cmbSupplier_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSupplier.TextChanged
        clearGridAndSetInvisible()
        Dim cmd As New OracleCommand("", conn)
        Dim sql As String = ""
        Try
            If chkBoxSalesPerSupplier.Checked Then
                Dim dateFrom As String = CStr(dtpFrom.Value.Day) & "-" & findMonth(CStr(dtpFrom.Value.Month)) & "-" & CStr(dtpFrom.Value.Year).Substring(2, 2)
                Dim dateTo As String = CStr(dtpTo.Value.Day) & "-" & findMonth(CStr(dtpTo.Value.Month)) & "-" & CStr(dtpTo.Value.Year).Substring(2, 2)

                sql = "select (select s_name || ' ' || concat(phone_1,NVL(phone_2,'')) from suppliers " & _
                      "where uuid = '" & supplierUUIDs(cmbSupplier.SelectedIndex) & "'), " & _
                      "p.description ,sum(quantity), NVL(buy_amt_no_vat,0) buy_amt, NVL(sell_amt,0) sell_amt, " & _
                      "(select barcode from barcodes where barcodes.PRODUCT_SERNO = rd.PRODUCT_SERNO and rownum < 2), " & _
                      "to_number(avail_quantity,'99999'), " & _
                      "to_number(stock_quantity,'99999') " & _
                      "from receipts_det rd " & _
                      "inner join products p on p.SERNO = rd.PRODUCT_SERNO " & _
                      "and p.SUPPLIER_ID = '" & supplierUUIDs(cmbSupplier.SelectedIndex) & "'" & _
                      "and created_on BETWEEN " & _
                      "to_timestamp('" & dateFrom & " 00:00:00', 'DD-MON-YY HH24:MI:SS') AND " & _
                      "to_timestamp('" & dateTo & " 23:59:59', 'DD-MON-YY HH24:MI:SS')" & _
                      "group by rd.product_serno, p.description, buy_amt_no_vat, sell_amt, " & _
                      "to_number(avail_quantity,'99999'), to_number(stock_quantity,'99999') " & _
                      "order by to_number(avail_quantity,'99999') "
                cmd = New OracleCommand(sql, conn)
            Else
                cmd = New OracleCommand(Q_PRODUCTS_PER_SUPPLIER, conn)
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
            'btnExportToExcel.Visible = True
            dr.Close()
        Catch ex As Exception
            createExceptionFile(ex.Message, " " & sql)
            MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            cmd.Dispose()
            fillSuppliers()
            formatDataGrid()
        End Try
    End Sub

    Private Sub cmbUsers_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbUsers.SelectedIndexChanged
        dgvReports.Rows.Clear()
        dgvReports.Columns.Clear()
        'clearGridAndSetInvisible()
        txtBoxTotalHoursOrPayments.Text = "0"
    End Sub

    Private Sub cmbNoBarcode_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbNoBarcode.SelectedIndexChanged
        Dim tmpSerno As Integer = -1

        If cmbNoBarcode.Text.Equals("Φ.Π.Α 5%") Then
            tmpSerno = -308
        ElseIf cmbNoBarcode.Text.Equals("Φ.Π.Α 19%") Then
            tmpSerno = -309
        End If

        If tmpSerno = -1 Then
            Exit Sub
        Else
            Dim sql As String = ""
            Dim cmd As New OracleCommand(sql, conn)
            Dim dr As OracleDataReader

            Try
                Dim dateFrom As String = CStr(dtpFrom.Value.Day) & "-" & findMonth(CStr(dtpFrom.Value.Month)) & "-" & CStr(dtpFrom.Value.Year).Substring(2, 2)
                Dim dateTo As String = CStr(dtpTo.Value.Day) & "-" & findMonth(CStr(dtpTo.Value.Month)) & "-" & CStr(dtpTo.Value.Year).Substring(2, 2)

                sql = "select NVL(count(quantity),0), NVL(SUM(amount),0) from receipts_det " & _
                      "where product_serno = " & tmpSerno & " " & _
                      "and created_on BETWEEN " & _
                      "to_timestamp('" & dateFrom & " 00:00:00', 'DD-MON-YY HH24:MI:SS') AND " & _
                      "to_timestamp('" & dateTo & " 23:59:59', 'DD-MON-YY HH24:MI:SS')"

                cmd = New OracleCommand(sql, conn)
                cmd.CommandType = CommandType.Text
                dr = cmd.ExecuteReader()

                Dim totalQuantity As Integer = 0
                Dim totalAmount As Double = 0
                Dim productDescription As String = ""
                If dr.Read() Then
                    totalQuantity = CInt(dr(0))
                    totalAmount = CDbl(dr(1))
                End If

                addGridRows(1)

                Dim row As String() = New String() {dtpFrom.Text, dtpTo.Text, cmbNoBarcode.Text, "N/A", totalQuantity, totalAmount.ToString("N2"), "N/A", "N/A"}
                dgvReports.Rows.Add(row)
                txtBoxBarcode.Focus()
                dr.Close()
                btnPrint.Visible = True
                'btnExportToExcel.Visible = True
            Catch ex As Exception
                createExceptionFile(ex.Message, " " & sql)
                MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                cmd.Dispose()
                formatDataGrid()
            End Try
        End If
    End Sub

    Private Sub addGridRows(ByVal reportType As Integer)
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
        formatDataGrid()
    End Sub

    Private Sub formatDataGrid()
        If dgvReports.Rows.Count > 0 Then
            dgvReports.FirstDisplayedScrollingRowIndex = dgvReports.Rows.Count - 1
        End If
    End Sub

    Private Sub chkBoxSalesPerSupplier_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkBoxSalesPerSupplier.CheckedChanged
        If rdbSupplierPr.Checked Then
            If chkBoxSalesPerSupplier.Checked Then
                showDateFields(True)
            Else
                showDateFields(False)
                lblTotalSalesAmount.Text = ""
            End If
        End If
    End Sub

    Private Sub showDateFields(ByVal show As Boolean)
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

    Private Function getZseq(ByVal tmpFrom As Date) As Integer
        Dim zseq As Integer = -1

        Dim tmpDate As String = CStr(tmpFrom.Day) & "-" & findMonth(CStr(tmpFrom.Month)) & "-" & CStr(tmpFrom.Year)
        Dim sql As String = "select z_seq from z_report where z_date='" & tmpDate & "'"
        Dim cmd As New OracleCommand(sql, conn)
        Dim dr As OracleDataReader
        Try
            dr = cmd.ExecuteReader()
            If dr.Read() Then
                zseq = CInt(dr(0))
            Else
                If tmpFrom = startDate Then
                    sql = "insert into z_report (z_date, z_seq) " & _
                          "values               ('" & tmpDate & "', 1)"
                    cmd = New OracleCommand(sql, conn)
                    Using cmd                        cmd.ExecuteNonQuery()                    End Using
                    zseq = 1

                Else
                    'Check if z-report for previous date was printed
                    'if yes, increase by one and return value
                    Dim tmpFromMinus1 As Date = tmpFrom.AddDays(-1)
                    Dim tmpDateMinus1 = CStr(tmpFromMinus1.Day) & "-" & findMonth(CStr(tmpFromMinus1.Month)) & "-" & CStr(tmpFromMinus1.Year)
                    sql = "select z_seq from z_report where z_date='" & tmpDateMinus1 & "'"
                    cmd = New OracleCommand(sql, conn)
                    dr = cmd.ExecuteReader()
                    If dr.Read() Then
                        Dim tmpZseq As Integer = CInt(dr(0))
                        zseq = tmpZseq + 1
                        sql = "insert into z_report (z_date, z_seq) " & _
                              "values               ('" & tmpDate & "', '" & zseq & "')"
                        cmd = New OracleCommand(sql, conn)
                        Using cmd                            cmd.ExecuteNonQuery()                        End Using
                    End If
                End If
            End If
            dr.Close()
        Catch ex As Exception
            CreateExceptionFile(ex.Message, " ")
            MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            cmd.Dispose()
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

    Private Sub Export_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportToExcel.Click
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
            releaseObject(xlWorkSheet)
            releaseObject(xlWorkBook)
            releaseObject(xlApp)
        End Try
    End Sub


    Private Sub releaseObject(ByVal obj As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
        Finally
            GC.Collect()
        End Try
    End Sub

    Private Sub cmbCategories_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCategories.SelectedIndexChanged

    End Sub
End Class