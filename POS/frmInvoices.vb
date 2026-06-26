Imports System.Data.SQLite
Imports Oracle.DataAccess.Client

Public Class frmInvoices
    Dim supplierId As String
    Dim supplierUUIDs As ArrayList = New ArrayList()
    Dim printAmt As String
    Dim printVatType As String
    Dim printSupplierName As String
    Public resetOnChange = 0

    Structure InvoiceObj
        'Oracle
        Dim serno As Integer
        'Sqlite
        Dim uuid As String
        Dim invNumber As String
        Dim supplierID As String
        Dim totalAmt As Double
        Dim closed As Integer
        Dim invDate As String
        Dim supplierName As String
        Dim extraDiscount As Double
    End Structure

    Public invoices As New ArrayList

    Private Sub FrmInvoices_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        frmMain.Show()
    End Sub

    Private Sub FrmInvoices_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub FrmInvoices_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        FillSuppliers()
        rdbNewInvoice.Checked = True
    End Sub

    Private Sub FillSuppliers()
        Dim sql As String = "SELECT
                                    UUID,
                                    S_NAME,
                                    PHONE_1,
                                    PHONE_2 
                              FROM SUPPLIERS
                              WHERE KIOSKID=@KIOSKID 
                              ORDER BY S_NAME ASC"
        Try
            If SqlLite Then
                Dim sup As New Supplier
                For Each dr As Hashtable In sup.GetSuppliers()
                    supplierUUIDs.Add(dr("uuid").ToString())
                    cmbSuppliers.Items.Add(dr("s_name").ToString())
                    cmbBoxSupFilter.Items.Add(dr("s_name").ToString())
                Next
            Else
                Using cmd As New OracleCommand(sql, conn)
                    Using dr As OracleDataReader = cmd.ExecuteReader()
                        While dr.Read()
                            supplierUUIDs.Add(dr("uuid").ToString())
                            cmbSuppliers.Items.Add(dr("s_name").ToString())
                            cmbBoxSupFilter.Items.Add(dr("s_name").ToString())
                        End While
                    End Using
                End Using
            End If
        Catch ex As Exception
            CreateExceptionFile(ex.Message, " " & sql)
            MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FillInvoicesList()
        Dim WhoAmI As String = "frmInvoices.FillInvoicesList"
        Dim sql As String = ""
        Try
            lstBoxInvNumber.Items.Clear()
            invoices.Clear()

            If SqlLite Then
                sql =
                    "
                    SELECT 
                        i.uuid,
                        i.serno,
                        i.i_number,
                        i.supplier_id,
                        i.total_amt,
                        IFNULL(i.closed,1) closed,
                        substr(i.inv_date,1,18) inv_date,
                        s.s_name,
                        i.extra_disc
                    FROM invoices i
                    INNER JOIN suppliers s ON i.supplier_id = s.uuid
                    WHERE i.kioskid=@KIOSKID
                    "

                If rdbClosedInvoices.Checked Then
                    sql &= " AND closed=1 "
                ElseIf rdbOpenInvoices.Checked Then
                    sql &= " AND closed=0 "
                End If

                If cmbBoxSupFilter.SelectedIndex > 0 Then
                    sql &= "
                            AND supplier_id =
                            (
                                SELECT uuid
                                FROM suppliers
                                WHERE s_name=@SUPPLIER
                            )
                            "
                End If

                If chkBoxDate.Checked Then
                    sql &= " AND date(inv_date)=date(@INVDATE) "
                End If

                sql &= " ORDER BY datetime(INV_DATE) DESC"

                Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                    sqliteConn.Open()
                    Using cmd As New SQLiteCommand(sql, sqliteConn)
                        cmd.Parameters.AddWithValue("@KIOSKID", kioskId)

                        If cmbBoxSupFilter.SelectedIndex > 0 Then
                            cmd.Parameters.AddWithValue("@SUPPLIER", cmbBoxSupFilter.Items(cmbBoxSupFilter.SelectedIndex).ToString())
                        End If

                        If chkBoxDate.Checked Then
                            cmd.Parameters.AddWithValue("@INVDATE", dtpDate.Value.ToString("yyyy-MM-dd"))
                        End If

                        Using dr As SQLiteDataReader = cmd.ExecuteReader()
                            While dr.Read()
                                lstBoxInvNumber.Items.Add(dr("i_number").ToString())
                                Dim tmpInvoiceObj As InvoiceObj
                                tmpInvoiceObj.uuid = dr("uuid").ToString()
                                If dr.IsDBNull(dr.GetOrdinal("serno")) Then
                                    tmpInvoiceObj.serno = 0
                                Else
                                    tmpInvoiceObj.serno = Convert.ToInt32(dr("serno"))
                                End If
                                tmpInvoiceObj.invNumber = dr("i_number").ToString()
                                tmpInvoiceObj.supplierID = dr("supplier_id").ToString()
                                tmpInvoiceObj.totalAmt = Convert.ToDouble(dr("total_amt"))
                                tmpInvoiceObj.closed = Convert.ToInt32(dr("closed"))
                                tmpInvoiceObj.invDate = dr("inv_date").ToString()
                                tmpInvoiceObj.supplierName = dr("s_name").ToString()
                                tmpInvoiceObj.extraDiscount = Convert.ToDouble(dr("extra_disc"))
                                invoices.Add(tmpInvoiceObj)
                            End While
                        End Using
                    End Using
                End Using
            Else
                sql =
                    "select serno, i_number, supplier_id, total_amt, NVL(closed,1) closed, substr(inv_date,0,18) inv_date, s_name, extra_disc " &
                    "from invoices i " &
                    "inner join suppliers s on i.supplier_id = s.uuid "

                If rdbClosedInvoices.Checked Then
                    sql &= "where closed=1 "
                ElseIf rdbOpenInvoices.Checked Then
                    sql &= "where closed=0 "
                End If

                If cmbBoxSupFilter.SelectedIndex > 0 Then
                    sql &= " and supplier_id =
                        (select uuid from suppliers where s_name = '" &
                        cmbBoxSupFilter.Items(
                        cmbBoxSupFilter.SelectedIndex) &
                        "') "
                End If

                If chkBoxDate.Checked Then
                    Dim dateFilter As String = CStr(dtpDate.Value.Day) & "-" & FindMonth(CStr(dtpDate.Value.Month)) & "-" & CStr(dtpDate.Value.Year).Substring(2, 2)
                    sql &=
                        " and inv_date BETWEEN " &
                        "to_timestamp('" & dateFilter & " 00:00:00','DD-MON-YY HH24:MI:SS') AND " &
                        "to_timestamp('" & dateFilter & " 23:59:59','DD-MON-YY HH24:MI:SS')"
                End If

                sql &= " order by serno desc"

                Using cmd As New OracleCommand(sql, conn)
                    Using dr As OracleDataReader = cmd.ExecuteReader()
                        While dr.Read()
                            lstBoxInvNumber.Items.Add(dr("i_number"))
                            Dim tmpInvoiceObj As New InvoiceObj

                            tmpInvoiceObj.serno = dr("serno")
                            tmpInvoiceObj.invNumber = dr("i_number")
                            tmpInvoiceObj.supplierID = dr("supplier_id")
                            tmpInvoiceObj.totalAmt = dr("total_amt")
                            tmpInvoiceObj.closed = dr("closed")
                            tmpInvoiceObj.invDate = dr("inv_date")
                            tmpInvoiceObj.supplierName = dr("s_name")
                            tmpInvoiceObj.extraDiscount = dr("extra_disc")
                            invoices.Add(tmpInvoiceObj)
                        End While
                    End Using
                End Using
            End If
        Catch ex As Exception
            CreateExceptionFile(WhoAmI & " " & ex.Message, sql)
            MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BtnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        If dgvProductsAndQnt.Rows.Count > 0 Then
            MessageBox.Show("Δεν μπορεί να γίνει έξοδος όταν υπάρχουν προϊοντα καταχωρημένα", "Σφάλμα Εξόδου", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        Me.Dispose()
    End Sub

    Private Sub NewOrExistingInvoice() Handles rdbNewInvoice.CheckedChanged, rdbExisting.CheckedChanged

        If rdbNewInvoice.Checked Then
            If resetOnChange = 1 Then
                resetFields()
                'New
                btnClear.Visible = True
                btnSave.Visible = True
                DateTimePicker1.Visible = True
                cmbSuppliers.Enabled = True
                chkBoxTmpSave.Enabled = True
                cmbSuppliers.Visible = True

                txtBoxInvDateRO.Visible = False
                dgvProductsAndQnt.ReadOnly = False
                txtBoxBarcode.ReadOnly = False
                txtBoxTotalAmt.ReadOnly = False
                txtBoxTotalAmt.ReadOnly = False
                txtInvNumber.ReadOnly = False
                txtBoxSNameRO.Visible = False
                lstBoxInvNumber.Enabled = False
                btnOverrideExisting.Visible = False
                txtBoxExtraDiscount.ReadOnly = False
                grbBoxInvType.Visible = False
                txtBoxExtraDiscount.Text = 0
            End If
        Else

            If dgvProductsAndQnt.Rows.Count > 0 Then
                resetOnChange = 0
                rdbNewInvoice.Checked = True
                MessageBox.Show("Υπάρχει ενεργή γραμμή", "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            Else
                resetOnChange = 1
            End If

            'Existing
            grbBoxInvType.Visible = True
            txtBoxInvDateRO.Visible = True
            dgvProductsAndQnt.ReadOnly = True
            txtBoxBarcode.ReadOnly = True
            txtBoxTotalAmt.ReadOnly = True
            txtBoxTotalAmt.ReadOnly = True
            txtInvNumber.ReadOnly = True
            txtBoxSNameRO.Visible = True
            lstBoxInvNumber.Enabled = True

            btnClear.Visible = False
            chkBoxTmpSave.Enabled = False
            DateTimePicker1.Visible = False
            btnSave.Visible = False
            cmbSuppliers.Visible = False
            cmbSuppliers.Enabled = False
            txtBoxExtraDiscount.ReadOnly = True
        End If
    End Sub

    Private Sub ResetFields()
        txtBoxTotalAmt.Clear()
        txtInvNumber.Clear()
        txtBoxTotalAmt.Clear()
        txtBoxExtraDiscount.Clear()
        txtBoxInvDateRO.Clear()
        txtBoxSNameRO.Clear()
        dgvProductsAndQnt.Rows.Clear()
        cmbSuppliers.SelectedIndex = -1
        txtBoxExtraDiscount.Clear()
    End Sub

    Private Sub LstBoxName_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstBoxInvNumber.SelectedIndexChanged
        If rdbNewInvoice.Checked Then
            Exit Sub
        End If

        Dim index As Integer = lstBoxInvNumber.SelectedIndex
        If index < 0 Then
            Exit Sub
        End If
        dgvProductsAndQnt.Rows.Clear()

        Dim tmpInvoice As InvoiceObj = invoices.Item(index)
        txtInvNumber.Text = tmpInvoice.invNumber
        txtBoxTotalAmt.Text = tmpInvoice.totalAmt
        txtBoxExtraDiscount.Text = tmpInvoice.extraDiscount
        txtBoxInvDateRO.Text = tmpInvoice.invDate
        txtBoxSNameRO.Text = tmpInvoice.supplierName

        If tmpInvoice.closed = 0 Then
            chkBoxTmpSave.Checked = True
            chkBoxTmpSave.Enabled = True
            btnOverrideExisting.Visible = True
            dgvProductsAndQnt.AllowUserToDeleteRows = True
            dgvProductsAndQnt.EditMode =
            DataGridViewEditMode.EditOnF2
            txtBoxTotalAmt.ReadOnly = False
            txtBoxExtraDiscount.ReadOnly = False
            txtBoxBarcode.ReadOnly = False
            dgvProductsAndQnt.ReadOnly = False
        Else
            chkBoxTmpSave.Checked = False
            chkBoxTmpSave.Enabled = False
            btnOverrideExisting.Visible = False
            txtBoxTotalAmt.ReadOnly = True
            txtBoxExtraDiscount.ReadOnly = True
            txtBoxBarcode.ReadOnly = True
            dgvProductsAndQnt.ReadOnly = True
            dgvProductsAndQnt.AllowUserToDeleteRows = False
        End If

        Dim sql As String = ""
        Try
            If SqlLite Then

                sql =
                    "
                    SELECT
                        IFNULL(d.SERNO,0) SERNO,
                        IFNULL(d.INV_PR_DESCR,' ') INV_PR_DESCR,
                        IFNULL(d.INV_PR_BUY_AMT,0) INV_PR_BUY_AMT,
                        IFNULL(d.INV_PR_DISC,0) INV_PR_DISC,
                        IFNULL(d.INV_PR_SELL_AMT,0) INV_PR_SELL_AMT,
                        IFNULL(d.INV_PR_PROFIT,0) INV_PR_PROFIT,
                        IFNULL(d.INV_PR_CUR_QNT,0) INV_PR_CUR_QNT,
                        IFNULL(d.INV_PR_QNT,0) INV_PR_QNT,
                        d.PRODUCT_UUID,
                        IFNULL(v.vat,0) VAT
                    FROM INVOICES_DET d
                    LEFT JOIN PRODUCTS p ON p.UUID = d.PRODUCT_UUID
                    LEFT JOIN VAT_TYPES v ON v.UUID = p.VATTYPE_ID
                    WHERE d.INV_UUID=@INV_UUID
                    ORDER BY d.SERNO
                    "

                Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                    sqliteConn.Open()
                    Using cmd As New SQLiteCommand(sql, sqliteConn)
                        cmd.Parameters.AddWithValue("@INV_UUID", tmpInvoice.uuid)
                        Using dr As SQLiteDataReader = cmd.ExecuteReader()

                            While dr.Read()
                                Dim row() As String = New String() {
                                dr("PRODUCT_UUID").ToString(),
                                dr("SERNO").ToString(),
                                dr("INV_PR_DESCR").ToString(),
                                dr("INV_PR_BUY_AMT").ToString(),
                                dr("INV_PR_DISC").ToString(),
                                dr("INV_PR_SELL_AMT").ToString(),
                                dr("INV_PR_PROFIT").ToString(),
                                dr("INV_PR_CUR_QNT").ToString(),
                                dr("INV_PR_QNT").ToString(),
                                dr("VAT").ToString()
                                }
                                dgvProductsAndQnt.Rows.Add(row)
                            End While
                        End Using
                    End Using
                End Using
            Else
                sql =
                    "select NVL(serno,0), " &
                    "NVL(INV_PR_DESCR,' '), " &
                    "NVL(INV_PR_BUY_AMT,0), " &
                    "NVL(INV_PR_DISC,0), " &
                    "NVL(INV_PR_SELL_AMT,0), " &
                    "NVL(INV_PR_PROFIT,0), " &
                    "NVL(INV_PR_CUR_QNT,0), " &
                    "NVL(INV_PR_QNT,0), " &
                    "INV_PR_SERNO, " &
                    "(select vat from vat_types " &
                    " where uuid = " &
                    "(select vattype_id from products " &
                    " where serno = INV_PR_SERNO)) " &
                    "from invoices_det " &
                    "where INV_SERNO = :INV_SERNO " &
                    "order by serno"

                Using cmd As New OracleCommand(sql, conn)
                    cmd.Parameters.Add("INV_SERNO", OracleDbType.Int32).Value = tmpInvoice.serno
                    Using dr As OracleDataReader = cmd.ExecuteReader()

                        While dr.Read()
                            Dim row() As String = New String() {
                            dr(8).ToString(),
                            dr(0).ToString(),
                            dr(1).ToString(),
                            dr(2).ToString(),
                            dr(3).ToString(),
                            dr(4).ToString(),
                            dr(5).ToString(),
                            dr(6).ToString(),
                            dr(7).ToString(),
                            dr(9).ToString()
                        }

                            dgvProductsAndQnt.Rows.Add(row)
                        End While
                    End Using
                End Using
            End If
        Catch ex As Exception
            CreateExceptionFile(ex.Message, sql)
            MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BtnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim sql As String = ""

        If Not ChecksOnSave() Then
            Exit Sub
        End If

        Dim tmpSave As Boolean = False
        If chkBoxTmpSave.Checked Then
            If MessageBox.Show("Να γίνει προσωρινή αποθήκευση;", "Προσωρινή Αποθήκευση", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                Exit Sub
            End If
            tmpSave = True
        End If

        Dim closed As Integer = If(chkBoxTmpSave.Checked, 0, 1)
        Try
            If SqlLite Then
                Dim invUUID As String = Guid.NewGuid().ToString("N").ToUpper()
                Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                    sqliteConn.Open()
                    Using tr = sqliteConn.BeginTransaction()

                        sql =
                                "
                                INSERT INTO INVOICES
                                (
                                    UUID,
                                    I_NUMBER,
                                    SUPPLIER_ID,
                                    TOTAL_AMT,
                                    CLOSED,
                                    INV_DATE,
                                    EXTRA_DISC,
                                    KIOSKID,
                                    UPDATED_AT,
                                    SYNC_STATUS
                                )
                                VALUES
                                (
                                    @UUID,
                                    @I_NUMBER,
                                    @SUPPLIER_ID,
                                    @TOTAL_AMT,
                                    @CLOSED,
                                    @INV_DATE,
                                    @EXTRA_DISC,
                                    @KIOSKID,
                                    CURRENT_TIMESTAMP,
                                    1
                                )
                                "

                        Using cmd As New SQLiteCommand(sql, sqliteConn)
                            cmd.Transaction = tr
                            cmd.Parameters.AddWithValue("@UUID", invUUID)
                            cmd.Parameters.AddWithValue("@I_NUMBER", txtInvNumber.Text.Replace("'", "`"))
                            cmd.Parameters.AddWithValue("@SUPPLIER_ID", supplierUUIDs(cmbSuppliers.SelectedIndex))
                            cmd.Parameters.AddWithValue("@TOTAL_AMT", CDbl(txtBoxTotalAmt.Text))
                            cmd.Parameters.AddWithValue("@CLOSED", closed)
                            cmd.Parameters.AddWithValue("@INV_DATE", DateTimePicker1.Value)
                            cmd.Parameters.AddWithValue("@EXTRA_DISC", CDbl(txtBoxExtraDiscount.Text))
                            cmd.Parameters.AddWithValue("@KIOSKID", kioskId)
                            cmd.ExecuteNonQuery()
                        End Using

                        For i As Integer = 0 To dgvProductsAndQnt.Rows.Count - 1
                            If dgvProductsAndQnt.Rows(i).IsNewRow Then
                                Continue For
                            End If

                            generateInvoicesDetLines(
                            i,
                            invUUID,
                            dgvProductsAndQnt.Rows(i).Cells("productserno").Value,
                            dgvProductsAndQnt.Rows(i).Cells("newQnt").Value,
                            dgvProductsAndQnt.Rows(i).Cells("description").Value,
                            dgvProductsAndQnt.Rows(i).Cells("buyamt").Value,
                            dgvProductsAndQnt.Rows(i).Cells("sellamt").Value,
                            dgvProductsAndQnt.Rows(i).Cells("currentQnt").Value,
                            dgvProductsAndQnt.Rows(i).Cells("invPrDiscount").Value,
                            dgvProductsAndQnt.Rows(i).Cells("profit").Value)


                            If Not tmpSave Then
                                UpdateProducts(
                                dgvProductsAndQnt.Rows(i).Cells("description").Value,
                                dgvProductsAndQnt.Rows(i).Cells("buyamt").Value.ToString,
                                dgvProductsAndQnt.Rows(i).Cells("sellamt").Value.ToString,
                                dgvProductsAndQnt.Rows(i).Cells("newQnt").Value,
                                dgvProductsAndQnt.Rows(i).Cells("productserno").Value,
                                dgvProductsAndQnt.Rows(i).Cells("profit").Value)
                            End If
                        Next
                        tr.Commit()
                    End Using
                End Using
            Else
                Dim invSerno As Integer = 0
                sql = "select invoicesSeq.nextVal from dual"
                Using cmdSeq As New OracleCommand(sql, conn)
                    invSerno = CInt(cmdSeq.ExecuteScalar())
                End Using

                sql =
                    "
                    INSERT INTO INVOICES
                    (
                        SERNO,
                        I_NUMBER,
                        SUPPLIER_ID,
                        TOTAL_AMT,
                        CLOSED,
                        INV_DATE,
                        EXTRA_DISC
                    )
                    VALUES
                    (
                        :SERNO,
                        :I_NUMBER,
                        :SUPPLIER_ID,
                        :TOTAL_AMT,
                        :CLOSED,
                        SYSTIMESTAMP,
                        :EXTRA_DISC
                    )
                    "
                Dim invUUID As String = Guid.NewGuid().ToString("N").ToUpper()
                Using cmd As New OracleCommand(sql, conn)
                    cmd.Parameters.Add("SERNO", invSerno)
                    cmd.Parameters.Add("I_NUMBER", txtInvNumber.Text)
                    cmd.Parameters.Add("SUPPLIER_ID", supplierUUIDs(cmbSuppliers.SelectedIndex))
                    cmd.Parameters.Add("TOTAL_AMT", txtBoxTotalAmt.Text.Replace(",", "."))
                    cmd.Parameters.Add("CLOSED", closed)
                    cmd.Parameters.Add("EXTRA_DISC", txtBoxExtraDiscount.Text.Replace(",", "."))
                    cmd.ExecuteNonQuery()
                End Using

                For i As Integer = 0 To dgvProductsAndQnt.Rows.Count - 1
                    generateInvoicesDetLines(
                    i,
                    invSerno,
                    dgvProductsAndQnt.Rows(i).Cells("productserno").Value,
                    dgvProductsAndQnt.Rows(i).Cells("newQnt").Value,
                    dgvProductsAndQnt.Rows(i).Cells("description").Value,
                    dgvProductsAndQnt.Rows(i).Cells("buyamt").Value,
                    dgvProductsAndQnt.Rows(i).Cells("sellamt").Value,
                    dgvProductsAndQnt.Rows(i).Cells("currentQnt").Value,
                    dgvProductsAndQnt.Rows(i).Cells("invPrDiscount").Value,
                    dgvProductsAndQnt.Rows(i).Cells("profit").Value)

                    If Not tmpSave Then
                        UpdateProducts(
                        dgvProductsAndQnt.Rows(i).Cells("description").Value,
                        dgvProductsAndQnt.Rows(i).Cells("buyamt").Value.ToString,
                        dgvProductsAndQnt.Rows(i).Cells("sellamt").Value.ToString,
                        dgvProductsAndQnt.Rows(i).Cells("newQnt").Value,
                        dgvProductsAndQnt.Rows(i).Cells("productserno").Value,
                        dgvProductsAndQnt.Rows(i).Cells("profit").Value)
                    End If
                Next
            End If

            MessageBox.Show("Η εντολή εκτελέστηκε επιτυχώς", "Πληροφορία", MessageBoxButtons.OK, MessageBoxIcon.Information)

            If Not tmpSave Then
                If MessageBox.Show("Έκδοση πληρωμής;", "Έκδοση Πληρωμής", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    'TODO
                    GenerateAndPrintPayment(supplierUUIDs.Item(cmbSuppliers.SelectedIndex), txtInvNumber.Text)
                End If
            End If

            ResetFields()
            FillInvoicesList()
            rdbNewInvoice.Checked = True
        Catch ex As Exception
            CreateExceptionFile(ex.Message, sql)
            MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function getTotalAmt(ByVal vat As Integer)
        Dim total As Double = 0
        For i = 0 To dgvProductsAndQnt.Rows.Count - 1
            If dgvProductsAndQnt.Rows(i).Cells("vat").Value.ToString().Equals(vat.ToString) Then
                Dim tmpTotal = CDbl(dgvProductsAndQnt.Rows(i).Cells("buyamt").Value) * CInt(dgvProductsAndQnt.Rows(i).Cells("newQnt").Value)
                If IsNumeric(CDbl(dgvProductsAndQnt.Rows(i).Cells("invPrDiscount").Value)) And
                   CDbl(dgvProductsAndQnt.Rows(i).Cells("invPrDiscount").Value) > 0 Then
                    tmpTotal = tmpTotal * ((100 - CDbl(dgvProductsAndQnt.Rows(i).Cells("invPrDiscount").Value)) / 100)
                End If
                total += tmpTotal
            End If
        Next
        Return total
    End Function

    Private Sub PrintPayment(ByVal totalAmt As Double, ByVal vattype As String)
        If MessageBox.Show("Εκτύπωση Πληρωμής;", "Εκτύπωση Πληρωμής", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            printAmt = totalAmt.ToString
            printVatType = vattype
            If rdbNewInvoice.Checked Then
                printSupplierName = cmbSuppliers.SelectedItem
            Else
                printSupplierName = txtBoxSNameRO.Text
            End If
            PrintDocument1.PrinterSettings.Copies = 1
            PrintDocument1.Print()
        End If
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        txtInvNumber.Clear()
        txtInvNumber.Clear()
        txtBoxTotalAmt.Clear()
        dgvProductsAndQnt.Rows.Clear()
    End Sub

    Private Sub txtBoxBarcode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBoxBarcode.TextChanged
        
    End Sub

    Private Sub dgvProductsAndQnt_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvProductsAndQnt.CellClick
        If rdbExisting.Checked And Not chkBoxTmpSave.Checked Then
            Exit Sub
        End If

        Dim index As Integer
        Try
            index = dgvProductsAndQnt.SelectedRows.Item(0).Index
        Catch ex As Exception
            index = -1
            Exit Sub
        End Try

        If MessageBox.Show(DELETE_SELECTED_LINE, DELETE_LINE, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            dgvProductsAndQnt.AllowUserToDeleteRows = True
            dgvProductsAndQnt.Rows.RemoveAt(index)
            dgvProductsAndQnt.Refresh()
            formatDataGrid()
            txtBoxBarcode.Focus()
        End If
    End Sub

    Private Function calculateAmounts() As Double
        Dim currentInvAmount As Double = 0
        If dgvProductsAndQnt.Rows.Count > 0 Then
            For i = 0 To dgvProductsAndQnt.Rows.Count - 1
                'Total amount without VAT
                Dim tmpTotal = TruncateDecimal(CDbl(dgvProductsAndQnt.Rows(i).Cells("buyamt").Value) * CInt(dgvProductsAndQnt.Rows(i).Cells("newQnt").Value), 3)
                Dim tmpInvPrDiscount As String = dgvProductsAndQnt.Rows(i).Cells("invPrDiscount").Value.ToString
                If IsNumeric(tmpInvPrDiscount) Then
                    If CDbl(tmpInvPrDiscount) > 0 Then
                        'Total amount without VAT - discount
                        tmpTotal = tmpTotal * ((100 - CDbl(tmpInvPrDiscount)) / 100)
                    End If
                End If

                'Total amount after discount + vat
                tmpTotal = ((100 + CInt(dgvProductsAndQnt.Rows(i).Cells("vat").Value)) * tmpTotal) / 100

                currentInvAmount += tmpTotal
            Next
            Return Math.Round(currentInvAmount - CDbl(txtBoxExtraDiscount.Text), 2)
        End If
    End Function

    Private Sub formatDataGrid()
        If dgvProductsAndQnt.Rows.Count > 0 Then
            For i = 0 To dgvProductsAndQnt.Rows.Count - 1
                dgvProductsAndQnt.Rows(i).Cells("serno").Value = i + 1
            Next
            dgvProductsAndQnt.FirstDisplayedScrollingRowIndex = dgvProductsAndQnt.Rows.Count - 1
        End If
    End Sub

    Private Function ChecksOnSave() As Boolean
        If txtInvNumber.Text = String.Empty Then
            MessageBox.Show("Το πεδίο αριθμός τιμολογίου είναι υποχρεωτικό", "Πληροφορία", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Function
        End If

        If Not txtBoxSNameRO.Text.Length > 0 And cmbSuppliers.SelectedIndex = -1 Then
            MessageBox.Show("Το πεδίο προμηθευτής είναι υποχρεωτικό", "Πληροφορία", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Function
        End If

        If Not IsNumeric(txtBoxTotalAmt.Text) Then
            MessageBox.Show("Το πεδίο ολικό ποσό πρέπει να αποτελείται μόνο από αριθμούς", "Πληροφορία", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        If txtBoxExtraDiscount.Text.Length > 0 And Not IsNumeric(txtBoxExtraDiscount.Text) Then
            MessageBox.Show("Το πεδίο επιπλέον έκπτωση πρέπει να αποτελείται μόνο από αριθμούς", "Πληροφορία", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        If txtBoxExtraDiscount.Text.Length = 0 Then
            txtBoxExtraDiscount.Text = "0"
        End If

        If dgvProductsAndQnt.Rows.Count = 0 Then
            MessageBox.Show("Δεν μπορεί να γίνει η αποθήκευση αν δεν καταχωρήσετε προϊόντα", "Πληροφορία", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        Dim tmpInvoiceAmount As Double = calculateAmounts()
        If (CStr(tmpInvoiceAmount) <> txtBoxTotalAmt.Text) And Not chkBoxTmpSave.Checked Then
            MessageBox.Show("Το ολικό ποσό τιμολογίου (" + txtBoxTotalAmt.Text + ") δεν συμφωνεί με τις τιμές αγοράς (" + CStr(tmpInvoiceAmount) + ")", "Πληροφορία", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        For i = 0 To dgvProductsAndQnt.Rows.Count - 1
            Dim tmpValue As String = ""
            tmpValue = dgvProductsAndQnt.Rows(i).Cells("newQnt").Value
            If Not IsNumeric(tmpValue) Then
                MessageBox.Show("Το πεδίο ποσότητα παραλαβής πρέπει αποτελείται μόνο από αριθμούς", "Πληροφορία", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If

            tmpValue = dgvProductsAndQnt.Rows(i).Cells("buyamt").Value
            If Not IsNumeric(tmpValue) Then
                MessageBox.Show("Το πεδίο ποσό αγοράς πρέπει αποτελείται μόνο από αριθμούς", "Πληροφορία", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If

            tmpValue = dgvProductsAndQnt.Rows(i).Cells("sellamt").Value
            If Not IsNumeric(tmpValue) Then
                MessageBox.Show("Το πεδίο ποσό πώλησης πρέπει αποτελείται μόνο από αριθμούς", "Πληροφορία", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If

            tmpValue = dgvProductsAndQnt.Rows(i).Cells("currentQnt").Value
            If Not IsNumeric(tmpValue) Then
                MessageBox.Show("Το πεδίο υφιστάμενη ποσότητα πρέπει αποτελείται μόνο από αριθμούς", "Πληροφορία", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If

            tmpValue = dgvProductsAndQnt.Rows(i).Cells("invPrDiscount").Value
            If Not IsNumeric(tmpValue) Then
                MessageBox.Show("Το πεδίο έκπτωση πρέπει αποτελείται μόνο από αριθμούς", "Πληροφορία", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If
        Next
        Return True
    End Function

    Private Sub BtnOverrideExisting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOverrideExisting.Click
        If Not ChecksOnSave() Then
            Exit Sub
        End If
        Dim closed As Integer = 1
        If chkBoxTmpSave.Checked Then
            Dim result As DialogResult = MessageBox.Show("Να γίνει προσωρινή αποθήκευση;", "Προσωρινή Αποθήκευση", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If result = Windows.Forms.DialogResult.No Then
                Exit Sub
            Else
                closed = 0
            End If
        End If

        Dim sql As String = ""
        Dim tmpInvoice As InvoiceObj = invoices.Item(lstBoxInvNumber.SelectedIndex)

        Try
            If SqlLite Then
                Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                    sqliteConn.Open()

                    Using tr = sqliteConn.BeginTransaction()

                        '1. Update invoice
                        sql = "UPDATE invoices
                               SET total_amt=@TOTAL_AMT,
                                   extra_disc=@EXTRA_DISC,
                                   closed=@CLOSED,
                                   updated_at=CURRENT_TIMESTAMP,
                                   sync_status=1
                               WHERE uuid=@UUID"

                        Using cmd As New SQLiteCommand(sql, sqliteConn)
                            cmd.Transaction = tr
                            cmd.Parameters.AddWithValue("@TOTAL_AMT", CDbl(txtBoxTotalAmt.Text))
                            cmd.Parameters.AddWithValue("@EXTRA_DISC", CDbl(txtBoxExtraDiscount.Text))
                            cmd.Parameters.AddWithValue("@CLOSED", closed)
                            cmd.Parameters.AddWithValue("@UUID", tmpInvoice.uuid)
                            cmd.ExecuteNonQuery()
                        End Using

                        '2. Delete existing details
                        sql = "DELETE FROM invoices_det WHERE inv_uuid=@INV_UUID"
                        Using cmd As New SQLiteCommand(sql, sqliteConn)
                            cmd.Transaction = tr
                            cmd.Parameters.AddWithValue("@INV_UUID", tmpInvoice.uuid)
                            cmd.ExecuteNonQuery()
                        End Using
                        tr.Commit()
                    End Using
                End Using

                '3. Recreate details
                For i = 0 To dgvProductsAndQnt.Rows.Count - 1
                    If dgvProductsAndQnt.Rows(i).IsNewRow Then Continue For
                    generateInvoicesDetLines(
                                            i,
                                            tmpInvoice.uuid,
                                            dgvProductsAndQnt.Rows(i).Cells("productserno").Value.ToString(),
                                            CInt(dgvProductsAndQnt.Rows(i).Cells("newQnt").Value),
                                            dgvProductsAndQnt.Rows(i).Cells("description").Value.ToString(),
                                            CDbl(dgvProductsAndQnt.Rows(i).Cells("buyamt").Value),
                                            CDbl(dgvProductsAndQnt.Rows(i).Cells("sellamt").Value),
                                            CInt(dgvProductsAndQnt.Rows(i).Cells("currentQnt").Value),
                                            CDbl(dgvProductsAndQnt.Rows(i).Cells("invPrDiscount").Value),
                                            CDbl(dgvProductsAndQnt.Rows(i).Cells("profit").Value)
                                        )

                    If closed = 1 Then
                        UpdateProducts(
                                        dgvProductsAndQnt.Rows(i).Cells("description").Value.ToString(),
                                        dgvProductsAndQnt.Rows(i).Cells("buyamt").Value.ToString(),
                                        dgvProductsAndQnt.Rows(i).Cells("sellamt").Value.ToString(),
                                        CInt(dgvProductsAndQnt.Rows(i).Cells("newQnt").Value),
                                        dgvProductsAndQnt.Rows(i).Cells("productserno").Value.ToString(),
                                        CDbl(dgvProductsAndQnt.Rows(i).Cells("profit").Value)
                )
                    End If
                Next
            Else
                '1. Update invoice
                sql = "update invoices set total_amt = " & txtBoxTotalAmt.Text.Replace(",", ".") & ", " &
                      "extra_disc = " & txtBoxExtraDiscount.Text.Replace(",", ".") & ", " &
                      "closed = " & closed & " " &
                      "where serno = " & tmpInvoice.serno & ""
                Dim cmd = New OracleCommand(sql, conn)
                cmd.CommandType = CommandType.Text

                Try
                    cmd = New OracleCommand(sql, conn)
                    cmd.CommandType = CommandType.Text
                    cmd.ExecuteNonQuery()

                    '2. Clear invoice details
                    sql = "delete from invoices_det where inv_serno = " & tmpInvoice.serno & ""
                    cmd = New OracleCommand(sql, conn)
                    cmd.CommandType = CommandType.Text
                    cmd.ExecuteNonQuery()

                    '3. Populate invoice details
                    For i = 0 To dgvProductsAndQnt.Rows.Count - 1
                        generateInvoicesDetLines(i, tmpInvoice.serno, dgvProductsAndQnt.Rows(i).Cells("productserno").Value,
                                                 dgvProductsAndQnt.Rows(i).Cells("newQnt").Value,
                                                 dgvProductsAndQnt.Rows(i).Cells("description").Value,
                                                 dgvProductsAndQnt.Rows(i).Cells("buyamt").Value,
                                                 dgvProductsAndQnt.Rows(i).Cells("sellamt").Value,
                                                 dgvProductsAndQnt.Rows(i).Cells("currentQnt").Value,
                                                 dgvProductsAndQnt.Rows(i).Cells("invPrDiscount").Value,
                                                 dgvProductsAndQnt.Rows(i).Cells("profit").Value)

                        If closed = 1 Then
                            UpdateProducts(dgvProductsAndQnt.Rows(i).Cells("description").Value,
                                           dgvProductsAndQnt.Rows(i).Cells("buyamt").Value.ToString,
                                           dgvProductsAndQnt.Rows(i).Cells("sellamt").Value.ToString,
                                           dgvProductsAndQnt.Rows(i).Cells("newQnt").Value,
                                           dgvProductsAndQnt.Rows(i).Cells("productserno").Value,
                                           dgvProductsAndQnt.Rows(i).Cells("profit").Value)
                        End If
                    Next
                Catch ex As Exception
                    CreateExceptionFile(ex.Message, sql)
                    MessageBox.Show(ex.Message, "Applicaton Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Finally
                    cmd.Dispose()
                End Try
            End If
        Catch ex As Exception
            CreateExceptionFile(ex.Message, sql)
            MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        MessageBox.Show("Η εντολή εκτελέστηκε επιτυχώς", "Πληροφορία", MessageBoxButtons.OK, MessageBoxIcon.Information)

        If closed = 1 Then
            Dim result As DialogResult = MessageBox.Show("Έκδοση πληρωμής;", "Έκδοση Πληρωμής", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If result = Windows.Forms.DialogResult.Yes Then
                GenerateAndPrintPayment(tmpInvoice.supplierID, tmpInvoice.invNumber)
            End If
        End If

        ResetFields()
        FillInvoicesList()
        txtBoxInvDateRO.Visible = True
        btnOverrideExisting.Visible = False
        txtBoxTotalAmt.ReadOnly = True
        chkBoxTmpSave.Enabled = False
    End Sub

    Private Sub UpdateProducts(ByVal prDescription As String, ByVal prBuyAmt As String,
                               ByVal prSellAmt As String, ByVal prNewQnt As Integer, ByVal prSerno As Integer, ByVal prNewProfit As Double)
        Dim sql As String = ""
        Dim cmd = New OracleCommand("", conn)
        Try
            sql = "update products set " &
                  "description = '" & prDescription.Replace("'", "`") & "', " &
                  "buy_amt_no_vat = " & prBuyAmt.Replace(",", ".") & ", " &
                  "sell_amt = " & prSellAmt.Replace(",", ".") & ", " &
                  "avail_quantity = avail_quantity + " & prNewQnt & ", " &
                  "profit_percent = " & prNewProfit & " " &
                  "where serno = " & prSerno & " "
            cmd = New OracleCommand(sql, conn)
            cmd.CommandType = CommandType.Text
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show(ex.ToString + " " + sql, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            cmd.Dispose()
        End Try
    End Sub

    Private Sub UpdateProducts(ByVal prDescription As String,
                           ByVal prBuyAmt As String,
                           ByVal prSellAmt As String,
                           ByVal prNewQnt As Integer,
                           ByVal productUUID As String,
                           ByVal prNewProfit As Double)

        Dim WhoAmI As String = "UpdateProducts-Sqlite"
        Dim sql As String = ""
        Try

            sql =
                "UPDATE products
                 SET
                    description=@DESCRIPTION,
                    buy_amt_no_vat=@BUY_AMT,
                    sell_amt=@SELL_AMT,
                    avail_quantity=avail_quantity + @NEW_QNT,
                    profit_percent=@PROFIT,
                    updated_at=CURRENT_TIMESTAMP,
                    sync_status=1
                 WHERE uuid=@UUID
                   AND kioskid=@KIOSKID"

            Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                sqliteConn.Open()
                Using cmd As New SQLiteCommand(sql, sqliteConn)
                    cmd.Parameters.AddWithValue("@DESCRIPTION", prDescription.Replace("'", "`"))
                    cmd.Parameters.AddWithValue("@BUY_AMT", CDbl(prBuyAmt.Replace(",", ".")))
                    cmd.Parameters.AddWithValue("@SELL_AMT", CDbl(prSellAmt.Replace(",", ".")))
                    cmd.Parameters.AddWithValue("@NEW_QNT", prNewQnt)
                    cmd.Parameters.AddWithValue("@PROFIT", prNewProfit)
                    cmd.Parameters.AddWithValue("@UUID", productUUID)
                    cmd.Parameters.AddWithValue("@KIOSKID", kioskId)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            CreateExceptionFile(WhoAmI + " " + ex.ToString(), sql)
            MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub generateInvoicesDetLines(ByVal lineSerno As Integer,
                                     ByVal invoiceUUID As String,
                                     ByVal productUUID As String,
                                     ByVal newQnt As Integer,
                                     ByVal productDesc As String,
                                     ByVal buyAmt As Double,
                                     ByVal sellAmt As Double,
                                     ByVal currentQnt As Integer,
                                     ByVal discount As Double,
                                     ByVal profit As Double)

        Dim WhoAmI As String = "generateInvoicesDetLines-Sqlite"
        Dim sql As String = ""

        Try
            Dim detailUUID As String =
            Guid.NewGuid().ToString("N").ToUpper()
            sql =
                "INSERT INTO invoices_det
                (
                    uuid,
                    serno,
                    inv_uuid,
                    product_uuid,
                    inv_pr_qnt,
                    inv_pr_descr,
                    inv_pr_buy_amt,
                    inv_pr_sell_amt,
                    inv_pr_cur_qnt,
                    inv_pr_disc,
                    inv_pr_profit,
                    kioskid,
                    updated_at,
                    sync_status
                )
                VALUES
                (
                    @UUID,
                    @SERNO,
                    @INV_UUID,
                    @PRODUCT_UUID,
                    @INV_PR_QNT,
                    @INV_PR_DESCR,
                    @INV_PR_BUY_AMT,
                    @INV_PR_SELL_AMT,
                    @INV_PR_CUR_QNT,
                    @INV_PR_DISC,
                    @INV_PR_PROFIT,
                    @KIOSKID,
                    CURRENT_TIMESTAMP,
                    1
                )"

            Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                sqliteConn.Open()
                Using cmd As New SQLiteCommand(sql, sqliteConn)
                    cmd.Parameters.AddWithValue("@UUID", detailUUID)
                    cmd.Parameters.AddWithValue("@SERNO", lineSerno + 1)
                    cmd.Parameters.AddWithValue("@INV_UUID", invoiceUUID)
                    cmd.Parameters.AddWithValue("@PRODUCT_UUID", productUUID)
                    cmd.Parameters.AddWithValue("@INV_PR_QNT", newQnt)
                    cmd.Parameters.AddWithValue("@INV_PR_DESCR", productDesc.Replace("'", "`"))
                    cmd.Parameters.AddWithValue("@INV_PR_BUY_AMT", buyAmt)
                    cmd.Parameters.AddWithValue("@INV_PR_SELL_AMT", sellAmt)
                    cmd.Parameters.AddWithValue("@INV_PR_CUR_QNT", currentQnt)
                    cmd.Parameters.AddWithValue("@INV_PR_DISC", discount)
                    cmd.Parameters.AddWithValue("@INV_PR_PROFIT", profit)
                    cmd.Parameters.AddWithValue("@KIOSKID", kioskId)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            CreateExceptionFile(WhoAmI + " " + ex.ToString(), sql)
            MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub generateInvoicesDetLines(ByVal lineSerno As Integer, ByVal invoiceSerno As Integer, ByVal productSerno As Integer, _
                                         ByVal newQnt As Integer, ByVal productDesc As String, ByVal buyAmt As Double, _
                                         ByVal sellAmt As Double, ByVal currentQnt As Integer, ByVal discount As Double, ByVal profit As Double)
        Dim cmd = New OracleCommand("", conn)
        Dim sql As String = ""
        Try
            sql = "insert into invoices_det (serno,inv_serno, inv_pr_serno, inv_pr_qnt, inv_pr_descr, " & _
                  "                          inv_pr_buy_amt, inv_pr_sell_amt, inv_pr_cur_qnt, inv_pr_disc, inv_pr_profit) " & _
                  "values(" & lineSerno + 1 & ", " & invoiceSerno & ", " & productSerno & ", " & newQnt & ", " & _
                  "      '" & productDesc.Replace("'", "`") & "', " & buyAmt & ", " & sellAmt & ", " & currentQnt & ", " & discount & ", " & profit & ") "

            cmd = New OracleCommand(sql, conn)
            cmd.CommandType = CommandType.Text
            Using cmd                cmd.ExecuteNonQuery()            End Using
        Catch ex As Exception
            MessageBox.Show(ex.ToString + " " + sql, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            cmd.Dispose()
        End Try

    End Sub


    Private Sub GenerateAndPrintPayment(ByVal supplierId As String, ByVal invoiceNumber As String)
        For i = 0 To 2
            Dim amountVAT As Double = 0
            Dim totalPerVatType As Double = 0
            Dim factor As Double = 0
            Dim vatType As String = ""
            If i = 0 Then
                totalPerVatType = getTotalAmt(0)
                vatType = "0"
            ElseIf i = 1 Then
                totalPerVatType = getTotalAmt(5)
                factor += 0.05
                vatType = "5"
            Else
                totalPerVatType = getTotalAmt(19)
                factor += 0.19
                vatType = "19"
            End If
            If totalPerVatType > 0 Then
                Dim amountWithVAT = totalPerVatType * (1 + factor)
                amountVAT = amountWithVAT - totalPerVatType
                createPayment(Math.Round(totalPerVatType, 2), Math.Round(amountVAT, 2), vatType, supplierId, invoiceNumber)
                PrintPayment(Math.Round(amountWithVAT, 2), vatType)
            End If
        Next
    End Sub

    Private Sub CreatePayment(ByVal totalAmt As Double, ByVal totalVATamt As Double, ByVal vatType As String, ByVal supplierId As String, ByVal invNumber As String)
        Dim WhoAmI As String = "frmInvoices.CreatePayment"
        Dim sql As String = ""

        Try
            If SqlLite Then
                sql =
                    "INSERT INTO payments
                    (
                        created_by,
                        created_on,
                        amount,
                        vat,
                        amountvat,
                        supplier_id,
                        inv_number,
                        kioskid,
                        updated_at,
                        sync_status
                    )
                    VALUES
                    (
                        @CREATED_BY,
                        CURRENT_TIMESTAMP,
                        @AMOUNT,
                        @VAT,
                        @AMOUNTVAT,
                        @SUPPLIER_ID,
                        @INV_NUMBER,
                        @KIOSKID,
                        CURRENT_TIMESTAMP,
                        1
                    )"

                Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                    sqliteConn.Open()

                    Using cmd As New SQLiteCommand(sql, sqliteConn)
                        cmd.Parameters.AddWithValue("@CREATED_BY", whois)
                        cmd.Parameters.AddWithValue("@AMOUNT", totalAmt)
                        cmd.Parameters.AddWithValue("@VAT", vatType)
                        cmd.Parameters.AddWithValue("@AMOUNTVAT", totalVATamt)
                        cmd.Parameters.AddWithValue("@SUPPLIER_ID", supplierId)
                        cmd.Parameters.AddWithValue("@INV_NUMBER", invNumber)
                        cmd.Parameters.AddWithValue("@KIOSKID", kioskId)
                        cmd.ExecuteNonQuery()
                    End Using
                End Using
            Else
                sql = "insert into payments " &
                      "(created_by, created_on, amount, vat, amountVAT, supplier_id, inv_number) " &
                      "values(:created_by, SYSTIMESTAMP, :amount, :vat, :amountVAT, :supplier_id, :inv_number)"

                Using cmd As New OracleCommand(sql, conn)
                    cmd.Parameters.Add("created_by", OracleDbType.Varchar2).Value = whois
                    cmd.Parameters.Add("amount", OracleDbType.Decimal).Value = totalAmt
                    cmd.Parameters.Add("vat", OracleDbType.Varchar2).Value = vatType
                    cmd.Parameters.Add("amountVAT", OracleDbType.Decimal).Value = totalVATamt
                    cmd.Parameters.Add("supplier_id", OracleDbType.Varchar2).Value = supplierId
                    cmd.Parameters.Add("inv_number", OracleDbType.Varchar2).Value = invNumber
                    cmd.ExecuteNonQuery()
                End Using
            End If
        Catch ex As Exception
            CreateExceptionFile(WhoAmI + " " + ex.ToString(), sql)
            MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub PrintDocument1_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        If Not isConnOpen() Then
            MessageBox.Show("Cannot connect to database, please try again", APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        Dim headerFont As Font = New Drawing.Font(REPORT_FONT, 15, FontStyle.Bold)
        Dim reportFont As Font = New Drawing.Font(REPORT_FONT, 9)
        Dim reportFontSmall As Font = New Drawing.Font(REPORT_FONT, 9)
        Dim reportFontTiny As Font = New Drawing.Font(REPORT_FONT, 8)

        e.Graphics.DrawString(KIOSK_NAME, headerFont, Brushes.Black, 63, 0)
        e.Graphics.DrawString(COMPANY_NAME, reportFont, Brushes.Black, 95, 35)
        e.Graphics.DrawString(KIOSK_ADDRESS1, reportFont, Brushes.Black, 75, 50)
        e.Graphics.DrawString(KIOSK_ADDRESS2, reportFont, Brushes.Black, 95, 65)
        e.Graphics.DrawString(COMPANY_VAT, reportFont, Brushes.Black, 60, 80)

        e.Graphics.DrawString(SINGE_DASHED_LINE, reportFont, Brushes.Black, 0, 95)
        e.Graphics.DrawString("Date: " & DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), reportFont, Brushes.Black, 0, 110)

        e.Graphics.DrawString(SINGE_DASHED_LINE, reportFont, Brushes.Black, 0, 130)
        e.Graphics.DrawString("Ποσό πληρωμής: " & printAmt & " Ευρώ", reportFont, Brushes.Black, 0, 200)
        e.Graphics.DrawString("Είδος Φ.Π.Α: " & printVatType, reportFont, Brushes.Black, 0, 220)
        e.Graphics.DrawString("Χρήστης: " & getUser(whois), reportFont, Brushes.Black, 0, 240)
        e.Graphics.DrawString("Προμηθευτής" & printSupplierName, reportFont, Brushes.Black, 0, 260)

        e.Graphics.DrawString(SINGE_DASHED_LINE, reportFont, Brushes.Black, 0, 400)
    End Sub


    Private Sub BtnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim WhoAmI As String = "frmInvoices.BtnSearch_Click"
        tmpBarcodeNotFoundExit = False

        If txtBoxBarcode.Text.Length < minBarcode Then
            Exit Sub
        End If

        Dim sql As String = ""
        Try
            If SqlLite Then
                sql = "SELECT p.uuid,
                          p.description,
                          p.sell_amt,
                          IFNULL(p.profit_percent,0),
                          p.buy_amt_no_vat,
                          p.avail_quantity,
                          v.vat
                       FROM products p
                       INNER JOIN barcodes b
                            ON b.product_uuid = p.uuid
                       INNER JOIN vat_types v
                            ON v.uuid = p.vattype_id
                       WHERE UPPER(b.barcode)=@BARCODE
                         AND p.kioskid=@KIOSKID"

                Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                    sqliteConn.Open()
                    Using cmd As New SQLiteCommand(sql, sqliteConn)
                        cmd.Parameters.AddWithValue("@BARCODE", txtBoxBarcode.Text.ToUpper())
                        cmd.Parameters.AddWithValue("@KIOSKID", kioskId)

                        Using dr As SQLiteDataReader = cmd.ExecuteReader()
                            If dr.Read() Then
                                Dim sellAmt As Double = CDbl(dr(2))
                                Dim profit As Double = CDbl(dr(3))
                                Dim buyAmt As Double = CDbl(dr(4))
                                Dim currentQnt As Integer = CInt(dr(5))
                                Dim vat As Integer = CInt(dr(6))

                                Dim newQnt As String = InputBox("Νέα Ποσότητα για " & dr(1).ToString() & ":", "Ποσοτητα καταχώρησης")

                                If Not IsNumeric(newQnt) Then
                                    MessageBox.Show("Η ποσότητα πρέπει να ειναι αριθμός", "Καταχώρηση Ποσότητας", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                                    Exit Sub
                                End If

                                Dim row() As String = New String() {
                                dr("UUID").ToString(),
                                (dgvProductsAndQnt.Rows.Count + 1).ToString(),
                                dr("DESCRIPTION").ToString(),
                                TruncateDecimal(buyAmt, 3).ToString(),
                                "0",
                                TruncateDecimal(sellAmt, 3).ToString(),
                                profit.ToString(),
                                currentQnt.ToString(),
                                newQnt,
                                vat.ToString()
                            }
                                dgvProductsAndQnt.Rows.Add(row)
                                txtBoxBarcode.Clear()
                            Else
                                tmpBarcodeNotFound = txtBoxBarcode.Text.ToUpper()
                                frmProductsModal.ShowDialog()

                                If Not tmpBarcodeNotFoundExit Then
                                    txtBoxBarcode_TextChanged(sender, e)
                                Else
                                    txtBoxBarcode.Clear()
                                End If
                            End If
                        End Using
                    End Using
                End Using
            Else

                sql = "select p.serno,
                          p.description,
                          p.sell_amt,
                          nvl(p.profit_percent,0),
                          p.buy_amt_no_vat,
                          p.avail_quantity,
                          v.vat
                   from products p
                   inner join vat_types v
                        on v.uuid = p.VATTYPE_ID
                   where p.serno =
                        (select b.product_serno
                           from barcodes b
                          where upper(b.barcode)=:BARCODE)"

                Using cmd As New OracleCommand(sql, conn)
                    cmd.Parameters.Add("BARCODE", OracleDbType.Varchar2).Value =
                    txtBoxBarcode.Text.ToUpper()

                    Using dr As OracleDataReader = cmd.ExecuteReader()
                        If dr.Read() Then
                            Dim sellAmt As Double = CDbl(dr(2))
                            Dim profit As Double = CDbl(dr(3))
                            Dim buyAmt As Double = CDbl(dr(4))
                            Dim currentQnt As Integer = CInt(dr(5))
                            Dim vat As Integer = CInt(dr(6))

                            Dim newQnt As String =
                            InputBox("Νέα Ποσότητα για " & dr(1).ToString() & ":", "Ποσοτητα καταχώρησης")
                            If Not IsNumeric(newQnt) Then
                                MessageBox.Show("Η ποσότητα πρέπει να ειναι αριθμός", "Καταχώρηση Ποσότητας", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                                Exit Sub
                            End If

                            Dim row() As String = New String() {
                            dr(0).ToString(),
                            (dgvProductsAndQnt.Rows.Count + 1).ToString(),
                            dr(1).ToString(),
                            TruncateDecimal(buyAmt, 3).ToString(),
                            "0",
                            TruncateDecimal(sellAmt, 3).ToString(),
                            profit.ToString(),
                            currentQnt.ToString(),
                            newQnt,
                            vat.ToString()
                        }

                            dgvProductsAndQnt.Rows.Add(row)
                            txtBoxBarcode.Clear()
                        Else
                            tmpBarcodeNotFound = txtBoxBarcode.Text.ToUpper()
                            frmProductsModal.ShowDialog()

                            If Not tmpBarcodeNotFoundExit Then
                                txtBoxBarcode_TextChanged(sender, e)
                            Else
                                txtBoxBarcode.Clear()
                            End If
                        End If
                    End Using
                End Using
            End If
            txtBoxBarcode.Focus()
        Catch ex As Exception
            CreateExceptionFile(WhoAmI + " " + ex.Message, sql)
            MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub FilterInvoices(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbAllInvoices.CheckedChanged, _
                                                                                    rdbClosedInvoices.CheckedChanged, rdbOpenInvoices.CheckedChanged, _
                                                                                    cmbBoxSupFilter.SelectedIndexChanged, chkBoxDate.CheckedChanged, dtpDate.ValueChanged
        resetFields()
        fillInvoicesList()
    End Sub    

    Private Sub dgvProductsAndQnt_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles dgvProductsAndQnt.CellValueChanged        
        If dgvProductsAndQnt.Rows.Count > 0 Then
            If e.ColumnIndex = 3 Or e.ColumnIndex = 5 Then
                Dim buyAmtNoVat = CDbl(dgvProductsAndQnt.Rows(e.RowIndex).Cells("buyAmt").Value)
                Dim buyAmtWithVat = buyAmtNoVat * (1 + (CDbl(dgvProductsAndQnt.Rows(e.RowIndex).Cells("vat").Value) / 100))
                Dim sellAmt = CDbl(dgvProductsAndQnt.Rows(e.RowIndex).Cells("sellamt").Value)
                Dim difference As Double = 0
                Dim diffPercentage As Double = 0
                difference = (sellAmt / buyAmtWithVat) - buyAmtNoVat
                diffPercentage = ((sellAmt * 100) / buyAmtWithVat) - 100
                dgvProductsAndQnt.Rows(e.RowIndex).Cells("profit").Value = TruncateDecimal(diffPercentage, 2).ToString
            End If
        End If
    End Sub
End Class

