Imports Oracle.DataAccess.Client
Public Class frmProductsModal

    Dim newProduct As Boolean

    Dim categoryId As String
    Dim categoryUUIDs As ArrayList = New ArrayList()
    Dim categoryVat As ArrayList = New ArrayList()

    Dim vatTypeId As String
    Dim vatTypeUUIDs As ArrayList = New ArrayList()
    Dim vatTypeVat As ArrayList = New ArrayList()

    Dim supplierId As String
    Dim supplierUUIDs As ArrayList = New ArrayList()
    Dim supplierPhone As ArrayList = New ArrayList()

    Dim serno As Integer = -1

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        tmpBarcodeNotFoundExit = True
        Me.Dispose()
    End Sub

    Private Sub frmNewProduct_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        Me.Dispose()
    End Sub

    Private Sub frmNewProduct_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub enableComponents()
        txtBoxDescription.ReadOnly = False
        txtBoxBuyAmt.ReadOnly = False
        txtBoxSellAmt.ReadOnly = False
        cmdBoxCategoy.Enabled = True
        cmdBoxVatType.Enabled = True
        cmbSupplier.Enabled = True
        ckboxAlert.Enabled = True
        chkBoxAlertExpiry.Enabled = True
        btnSave.Enabled = True
        lnkLblBarcodes.Enabled = True
        lnkLblAddQuantity.Enabled = True
        lnkLblAddStock.Enabled = True
        txtBoxNotes.ReadOnly = False
        txtBoxBuyAmtNoVAT.ReadOnly = False
        txtBoxSearchBox.Focus()
    End Sub

    Private Sub fillCategories()
        Dim sql As String = "select uuid, description, vat " & _
                            "from categories "

        Dim cmd As New OracleCommand(sql, conn)
        cmd.CommandType = CommandType.Text
        Dim dr As OracleDataReader = cmd.ExecuteReader()
        categoryId = ""
        cmdBoxCategoy.Items.Clear()
        categoryUUIDs.Clear()
        categoryVat.Clear()
        While dr.Read()
            categoryUUIDs.Add(dr("uuid"))
            cmdBoxCategoy.Items.Add(dr("description"))
            categoryVat.Add(dr("vat"))
        End While
        dr.Close()
        cmd.Dispose()
    End Sub

    Private Sub fillVatTypes()
        Dim sql As String = "select uuid, description, vat " & _
                            "from vat_types "

        Dim cmd As New OracleCommand(sql, conn)
        cmd.CommandType = CommandType.Text
        Dim dr As OracleDataReader = cmd.ExecuteReader()
        vatTypeId = ""
        cmdBoxVatType.Items.Clear()
        vatTypeUUIDs.Clear()
        vatTypeVat.Clear()
        While dr.Read()
            vatTypeUUIDs.Add(dr("uuid"))
            cmdBoxVatType.Items.Add(dr("description"))
            vatTypeVat.Add(dr("vat"))
        End While
        dr.Close()
        cmd.Dispose()
    End Sub

    Private Sub fillSuppliers()
        Dim cmd As New OracleCommand("", conn)
        Dim sql As String = ""
        Try
            sql = "select uuid, NVL(s_name, ' ') s_name, NVL(phone_1, ' ') phone_1, NVL(phone_2, ' ') phone_2 " & _
                  "from suppliers order by s_name asc"

            cmd = New OracleCommand(sql, conn)
            cmd.CommandType = CommandType.Text
            Dim dr As OracleDataReader = cmd.ExecuteReader()
            supplierId = ""
            supplierUUIDs.Clear()
            cmbSupplier.Items.Clear()
            supplierPhone.Clear()
            While dr.Read()
                supplierUUIDs.Add(dr("uuid"))
                cmbSupplier.Items.Add(dr("s_name"))
                supplierPhone.Add(dr("phone_1") & " " & dr("phone_2"))
            End While
            dr.Close()
        Catch ex As Exception
            createExceptionFile(ex.Message, sql)
            MessageBox.Show(ex.Message, "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            cmd.Dispose()
        End Try
    End Sub

    Private Sub cmdBoxCategoy_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBoxCategoy.TextChanged
        categoryId = categoryUUIDs(cmdBoxCategoy.SelectedIndex)
        'txtBoxVAT.Text = categoryVat(cmdBoxCategoy.SelectedIndex)
    End Sub

    Private Sub cmdBoxVatType_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBoxVatType.TextChanged
        vatTypeId = vatTypeUUIDs(cmdBoxVatType.SelectedIndex)
        txtBoxVAT.Text = vatTypeVat(cmdBoxVatType.SelectedIndex)
    End Sub

    Private Sub cmbSupplier_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSupplier.TextChanged
        supplierId = supplierUUIDs(cmbSupplier.SelectedIndex)
        txtBoxSupplierPhone.Text = supplierPhone(cmbSupplier.SelectedIndex)
    End Sub

    Private Sub resetFields()
        txtBoxSearchBox.Clear()
        txtBoxDescription.Clear()
        txtBoxBuyAmt.Clear()
        txtBoxSellAmt.Clear()
        cmdBoxCategoy.Items.Clear()
        cmbSupplier.Items.Clear()
        ckboxAlert.Checked = False
        txtBoxVAT.Clear()
        txtBoxSupplierPhone.Clear()
        txtBoxAvailQuantity.Clear()
        txtBoxMinQuantity.Clear()
        lstBoxBarcodes.Items.Clear()
        txtBoxBuyAmtNoVAT.Clear()
        serno = -1
        categoryId = ""
        supplierId = ""
        vatTypeId = ""
        txtBoxSearchBox.Focus()
        txtBoxNotes.Clear()
        dtpAlert.Value = Today
        dtpExpiry.Value = Today
        txtBoxStockQuantity.Clear()
        chkBoxAlertExpiry.Checked = False
        txtBoxProfit.Clear()
        txtBoxProfitPercentage.Clear()        
    End Sub

    Private Sub resetFieldsNoSearchBox()
        txtBoxDescription.Clear()
        txtBoxBuyAmt.Clear()
        txtBoxSellAmt.Clear()
        cmdBoxCategoy.Items.Clear()
        cmbSupplier.Items.Clear()
        ckboxAlert.Checked = False
        txtBoxVAT.Clear()
        txtBoxSupplierPhone.Clear()
        txtBoxAvailQuantity.Clear()
        txtBoxMinQuantity.Clear()
        lstBoxBarcodes.Items.Clear()
        txtBoxBuyAmtNoVAT.Clear()
        serno = -1
        categoryId = ""
        supplierId = ""
        vatTypeId = ""
        txtBoxSearchBox.Focus()
        txtBoxNotes.Clear()
        dtpAlert.Value = Today
        dtpExpiry.Value = Today
        txtBoxStockQuantity.Clear()
        chkBoxAlertExpiry.Checked = False
        txtBoxProfit.Clear()
        txtBoxProfitPercentage.Clear()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim sql As String = ""
        Dim cmd As New OracleCommand(sql, conn)
        Dim dr As OracleDataReader

        If txtBoxDescription.Text = String.Empty Then
            MessageBox.Show("Το πεδίο Περιγραφή δεν μπορεί να είναι κενό", "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If txtBoxBuyAmt.Text = String.Empty Or Not IsNumeric(txtBoxBuyAmt.Text) Or txtBoxBuyAmtNoVAT.Text = String.Empty Or Not IsNumeric(txtBoxBuyAmtNoVAT.Text) Or txtBoxSellAmt.Text = String.Empty Or Not IsNumeric(txtBoxSellAmt.Text) Then
            MessageBox.Show("Τα πεδία Τιμή Αγοράς/Πώλησης δεν μπορούν να είναι κενά και αναποτελούνται από αριθμούς", "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If vatTypeId = "" Then
            MessageBox.Show("Το πεδίο Φ.Π.Α δεν μπορεί να είναι κενό", "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If categoryId = "" Or categoryId = " " Then
            MessageBox.Show("Το πεδίο Κατηγόρία δεν μπορεί να είναι κενό", "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If supplierId = "" Then
            MessageBox.Show("Το πεδίο Προμηθευτής δεν μπορεί να είναι κενό", "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If ckboxAlert.Checked And txtBoxMinQuantity.Text = String.Empty Then
            MessageBox.Show("Το πεδίο ελάχιστη ποσότητα δεν μπορεί να είναι κενό", "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If ckboxAlert.Checked And Not IsNumeric(txtBoxMinQuantity.Text) Then
            MessageBox.Show("Το πεδίο ελάχιστη ποσότητα πρέπει να είναι αριθμός", "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If lstBoxBarcodes.Items.Count = 0 Then
            MessageBox.Show("Δεν έχετε καταχωρήσει κωδικό Barcode για το προϊόν", "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim alertOnMin As Integer = 0
        If ckboxAlert.Checked Then
            alertOnMin = 1
        End If

        Dim alertOnExpiry As Integer = 0
        If chkBoxAlertExpiry.Checked Then
            alertOnExpiry = 1
        End If

        Dim expiryDate As String = CStr(dtpExpiry.Value.Day) & "-" & findMonth(CStr(dtpExpiry.Value.Month)) & "-" & CStr(dtpExpiry.Value.Year).Substring(2, 2)
        Dim alertDate As String = CStr(dtpAlert.Value.Day) & "-" & findMonth(CStr(dtpAlert.Value.Month)) & "-" & CStr(dtpAlert.Value.Year).Substring(2, 2)
        Dim notes = " "
        If txtBoxNotes.Text.Length > 0 Then
            notes = txtBoxNotes.Text.Replace("'", "`")
        End If

        Dim availQuantity As Integer = 0
        If txtBoxAvailQuantity.Text.Length > 0 Then
            availQuantity = CInt(txtBoxAvailQuantity.Text)
        End If

        Dim stockQuantity As Integer = 0
        If txtBoxStockQuantity.Text.Length > 0 Then
            stockQuantity = CInt(txtBoxStockQuantity.Text)
        End If

        If availQuantity < 0 Then
            MessageBox.Show("Το πεδίο Ποσότητα πρέπει να είναι μεγαλύτερο ή ίσο με το μηδέν", "Αποθήκευση Προϊόντως", MessageBoxButtons.OK, MessageBoxIcon.Information)
            cmd.Dispose()
            Exit Sub
        End If

        Try

            sql = "select productsSeq.nextVal from dual"

            cmd = New OracleCommand(sql, conn)
            cmd.CommandType = CommandType.Text
            dr = cmd.ExecuteReader()
            If dr.Read() Then
                serno = CInt(dr(0))
            End If
            dr.Close()

            Dim minQuantity As Integer = 0
            If txtBoxMinQuantity.Text.Length > 0 Then
                minQuantity = CInt(txtBoxMinQuantity.Text)
            End If

            sql = "insert into products (serno, description, buy_amt, sell_amt, category_id, vattype_id, supplier_id, avail_quantity, min_quantity, alert_on_min, " & _
                  "                      expiry_date, alert_date, notes, buy_amt_no_vat, alert_on_expiry, stock_quantity, amt_profit, profit_percent, " & _
                  "                      lastmodifiedby)" & _
                  "values               (" & serno & "," & _
                  "                     '" & txtBoxDescription.Text.Replace("'", "`") & "'," & _
                  "                      " & txtBoxBuyAmt.Text.Replace(",", ".") & "," & _
                  "                      " & txtBoxSellAmt.Text.Replace(",", ".") & "," & _
                  "                     '" & categoryId & "'," & _
                  "                     '" & vatTypeId & "'," & _
                  "                     '" & supplierId & "'," & _
                  "                      " & availQuantity & ", " & _
                  "                      " & minQuantity & ", " & _
                  "                      " & alertOnMin & ", " & _
                  "                      to_date('" & expiryDate & "','DD-MM-YY'), " & _
                  "                      to_date('" & alertDate & "','DD-MM-YY'), " & _
                  "                      '" & notes & "'," & _
                  "                      " & txtBoxBuyAmtNoVAT.Text.Replace(",", ".") & ", " & _
                  "                      " & alertOnExpiry & " " & ", " & _
                  "                      " & stockQuantity & ", " & _
                  "                      " & CDbl(txtBoxProfit.Text) & ", " & _
                  "                      " & CDbl(txtBoxProfitPercentage.Text) & ", " & _
                  "                      '" & whois & "') "

            cmd = New OracleCommand(sql, conn)
            cmd.CommandType = CommandType.Text
            Using cmd                cmd.ExecuteNonQuery()            End Using
            txtBoxAvailQuantity.Text = 0

            sql = "delete from barcodes where product_serno = " & serno & ""
            cmd = New OracleCommand(sql, conn)
            cmd.CommandType = CommandType.Text
            Using cmd                cmd.ExecuteNonQuery()            End Using

            For i As Integer = 0 To lstBoxBarcodes.Items.Count - 1
                sql = "insert into barcodes (product_serno, barcode) values (" & serno & ", '" & lstBoxBarcodes.Items.Item(i) & "')"
                cmd = New OracleCommand(sql, conn)
                cmd.CommandType = CommandType.Text
                Using cmd                    cmd.ExecuteNonQuery()                End Using
            Next
            MessageBox.Show("Το προϊόν έχει αποθηκευτεί επιτυχώς", "Αποθήκευση Νέου Προϊόντως", MessageBoxButtons.OK, MessageBoxIcon.Information)
            newProduct = False
            txtBoxSearchBox.Focus()
            btnClear_Click(sender, e)
        Catch ex As Exception
            createExceptionFile(ex.Message, sql)
            MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            cmd.Dispose()
        End Try
        Me.Dispose()
    End Sub

    Private Sub ckboxAlert_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckboxAlert.CheckedChanged
        If ckboxAlert.Checked Then
            txtBoxMinQuantity.ReadOnly = False
        Else
            txtBoxMinQuantity.ReadOnly = True
        End If
    End Sub

    Private Sub lnkLblBarcodes_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkLblBarcodes.LinkClicked
        Dim newBarcode As Object
        newBarcode = InputBox("Εισαγωγή νέου Barcode", "Νέο Barcode", "")
        If newBarcode Is "" Then
            Exit Sub
        Else
            If checkIfExistsInDB(newBarcode) Then
                MessageBox.Show("Ο κωδικός Barcode είναι ήδη καταχωρημένος σε άλλο προϊόν", "Καταχώρηση Barcode", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                Exit Sub
            End If
            If Not checkIfExist(newBarcode) Then
                lstBoxBarcodes.Items.Add(newBarcode)
            End If
        End If
        txtBoxDescription.Focus()
    End Sub

    Private Function checkIfExistsInDB(ByVal newBarcode As String) As Boolean
        Dim sql As String = "select count(*) from barcodes where UPPER(barcode) = '" & newBarcode.ToUpper & "'"

        Dim cmd As New OracleCommand(sql, conn)
        Dim counter As Integer = 0
        cmd.CommandType = CommandType.Text
        Dim dr As OracleDataReader = cmd.ExecuteReader()

        If dr.Read() And CInt(dr(0)) > 0 Then
            dr.Close()
            cmd.Dispose()
            Return True
        End If
        dr.Close()
        cmd.Dispose()
    End Function

    Private Function checkIfExist(ByVal newBarcode As String) As Boolean
        If lstBoxBarcodes.Items.IndexOf(newBarcode) <> -1 Then
            Return True
        End If
        Return False
    End Function

    Private Sub lstBoxBarcodes_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstBoxBarcodes.DoubleClick
        Dim result = MessageBox.Show("Να διαγραφεί ο κωδικός Barcode;", "Διαγραφή Barcode", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
        If result = DialogResult.Yes Then
            If lstBoxBarcodes.SelectedIndex <> -1 Then
                lstBoxBarcodes.Items.RemoveAt(lstBoxBarcodes.SelectedIndex)
                Dim sql As String = "delete from barcodes where UPPER(barcode) = '" & lstBoxBarcodes.Text.ToUpper & "'"
                Dim cmd As New OracleCommand(sql, conn)
                cmd.CommandType = CommandType.Text
                cmd.ExecuteNonQuery()
                cmd.Dispose()
            End If
        End If
        'txtBoxSearchBox.Focus()
    End Sub

    Private Sub lnkLblAddQuantity_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkLblAddQuantity.LinkClicked
        Dim newQuantity As Object
        newQuantity = InputBox("Προσθήκη Νέας Ποσότητας", "Νέα Ποσότητα", "")
        If Not IsNumeric(newQuantity) Then
            MessageBox.Show("Η ποσότητα πρέπει να είναι αριθμός", "Καταχώρηση Ποσότητας", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        Else
            Dim currentQuantity As Integer = 0
            If txtBoxAvailQuantity.Text.Length > 0 Then
                Try
                    currentQuantity = CInt(txtBoxAvailQuantity.Text)
                Catch ex As Exception
                End Try
            End If
            txtBoxAvailQuantity.Text = newQuantity + currentQuantity
        End If
        txtBoxSearchBox.Focus()
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        lstBoxBarcodes.Items.Clear()
        Dim sql As String = ""
        resetFieldsNoSearchBox()
        lstBoxBarcodes.Items.Add(txtBoxSearchBox.Text)
        txtBoxSearchBox.Clear()
        enableComponents()
        fillCategories()
        fillVatTypes()
        fillSuppliers()
        txtBoxDescription.Focus()
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        resetFields()
    End Sub

    Private Sub btnCalculateWithoutVAT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCalculateWithoutVAT.Click
        Try
            Dim vat As Integer = CInt(txtBoxVAT.Text)
            Dim amtWithVAT As Double = 0
            Dim temp = txtBoxBuyAmtNoVAT.Text.Replace(",", ".")
            amtWithVAT = CDbl(temp)
            If vat = 19 Then
                amtWithVAT = CDbl(temp) * 1.19
            ElseIf vat = 5 Then
                amtWithVAT = CDbl(temp) * 1.05
            ElseIf vat = 8 Then
                amtWithVAT = CDbl(temp) * 1.08
            End If
            txtBoxBuyAmt.Text = amtWithVAT.ToString("N2")
        Catch ex As Exception
            Exit Sub
        End Try
    End Sub

    Private Sub btnCalculateWithVAT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCalculateWithVAT.Click
        Try
            Dim vat As Integer = CInt(txtBoxVAT.Text)
            Dim amtWithNoVAT As Double = 0
            Dim temp = txtBoxBuyAmt.Text.Replace(",", ".")
            amtWithNoVAT = CDbl(temp)
            If vat <> 0 Then
                amtWithNoVAT = CDbl(temp) / (1 + (CInt(txtBoxVAT.Text) / 100))
            End If
            txtBoxBuyAmtNoVAT.Text = amtWithNoVAT.ToString("N2")
        Catch ex As Exception
            Exit Sub
        End Try
    End Sub

    Private Sub txtBoxSellAmt_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBoxSellAmt.TextChanged
        Try
            Dim sellAmt = CDbl(txtBoxSellAmt.Text.Replace(",", "."))
            Dim buyAmt = CDbl(txtBoxBuyAmtNoVAT.Text.Replace(",", "."))
            Dim buyAmtWithVAT = CDbl(txtBoxBuyAmt.Text.Replace(",", "."))
            Dim difference As Double = 0
            Dim diffPercentage As Double = 0

            If Not txtBoxVAT.Text = String.Empty Then
                difference = (sellAmt / (1 + (CInt(txtBoxVAT.Text) / 100))) - buyAmt
            Else
                difference = sellAmt - buyAmt
            End If
            txtBoxProfit.Text = difference.ToString("N2")
            diffPercentage = ((sellAmt * 100) / buyAmtWithVAT) - 100
            txtBoxProfitPercentage.Text = diffPercentage.ToString("N2")
        Catch ex As Exception
            Exit Sub
        End Try
    End Sub

    Private Sub lnkLblAddStock_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkLblAddStock.LinkClicked
        Try
            Dim newQuantity As Object
            newQuantity = InputBox("Προσθήκη Νέας Ποσότητας Αποθήκης", "Νέα Ποσότητα Αποθήκης", "")
            If Not IsNumeric(newQuantity) Then
                MessageBox.Show("Η ποσότητα πρέπει να είναι αριθμός", "Καταχώρηση Ποσότητας Αποθήκης", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            Else
                Dim currentStock As Integer = 0
                Try
                    currentStock = CInt(txtBoxStockQuantity.Text)
                Catch ex As Exception
                End Try

                Dim newTotal As Integer = newQuantity + currentStock
                txtBoxStockQuantity.Text = newTotal
            End If
            txtBoxSearchBox.Focus()
        Catch ex As Exception
            createExceptionFile(ex.Message, "")
            MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            'cmd.Dispose()
        End Try
    End Sub

    Private Sub txtBoxSearchBox_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBoxSearchBox.MouseEnter
        txtBoxSearchBox.BackColor = Color.Bisque
    End Sub

    Private Sub txtBoxSearchBox_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBoxSearchBox.MouseLeave
        txtBoxSearchBox.BackColor = Color.LemonChiffon
    End Sub

    Private Sub txtBoxDescription_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBoxDescription.MouseEnter
        txtBoxDescription.BackColor = Color.Bisque
    End Sub

    Private Sub txtBoxDescription_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBoxDescription.MouseLeave
        txtBoxDescription.BackColor = Color.LemonChiffon
    End Sub

    Private Sub txtBoxAvailQuantity_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBoxAvailQuantity.MouseEnter
        txtBoxAvailQuantity.BackColor = Color.Bisque
    End Sub

    Private Sub txtBoxAvailQuantity_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBoxAvailQuantity.MouseLeave
        txtBoxAvailQuantity.BackColor = Color.LemonChiffon
    End Sub

    Private Sub frmProducts_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtBoxSearchBox.Text = tmpBarcodeNotFound
        btnSearch_Click(sender, e)
    End Sub
End Class