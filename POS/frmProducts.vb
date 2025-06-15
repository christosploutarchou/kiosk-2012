Imports Oracle.DataAccess.Client
Public Class frmProducts

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
        Me.Dispose()
    End Sub

    Private Sub frmNewProduct_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        frmMain.Show()
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
        chkBoxOffer.Enabled = True
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
        calculateAmounts()
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
        dtpOfferFrom.Value = Today
        dtpOfferTo.Value = Today
        txtBoxStockQuantity.Clear()
        chkBoxAlertExpiry.Checked = False
        txtBoxProfit.Clear()
        txtBoxProfitPercentage.Clear()
        chkBoxOffer.Checked = False
        txtBoxOfferX.Clear()
        txtBoxOfferDisc.Clear()
        txtBoxOfferDiscAt.Clear()
        txtBoxOfferX.Visible = False
        txtBoxOfferDisc.Visible = False
        txtBoxOfferDiscAt.Visible = False
        lblOfferDisc.Visible = False
        chkboxBox.Checked = False
        txtBoxBoxUnits.Clear()
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

        Dim isbox As Integer = 0
        Dim boxQnt As Integer = 0
        If Not newProduct And chkboxBox.Checked Then
            isbox = 1
            If txtBoxBoxUnits.Text = String.Empty Then
                MessageBox.Show("Το πεδίο Μονάδες Ανά Συσκευασία πρέπει να είναι μεγαλύτερο από το μηδέν", "Αποθήκευση Προϊόντως", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            Else
                If Not IsNumeric(txtBoxBoxUnits.Text) Then
                    MessageBox.Show("Το πεδίο Μονάδες Ανά Συσκευασία πρέπει να είναι αριθμός", "Αποθήκευση Προϊόντως", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If
            End If
            boxQnt = txtBoxBoxUnits.Text

            If Not checkBoxLinkedProducts() Then
                MessageBox.Show("Δεν έχετε συνδέσει barcode(s) με την συσκευασία", "Αποθήκευση Προϊόντως", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
        End If

        Dim alertOnMin As Integer = 0
        If ckboxAlert.Checked Then
            alertOnMin = 1
        End If

        Dim alertOnExpiry As Integer = 0
        If chkBoxAlertExpiry.Checked Then
            alertOnExpiry = 1
        End If

        Dim offer As Integer = 0
        Dim offerType As Integer = -1
        Dim offerX As Integer = 0
        Dim offerY As Integer = 0
        Dim offerDisc As Double = 0
        Dim offerAt As Integer = 0

        Dim offerFromDate As String = CStr(dtpOfferFrom.Value.Day) & "-" & findMonth(CStr(dtpOfferFrom.Value.Month)) & "-" & CStr(dtpOfferFrom.Value.Year).Substring(2, 2)
        Dim offerToDate As String = CStr(dtpOfferTo.Value.Day) & "-" & findMonth(CStr(dtpOfferTo.Value.Month)) & "-" & CStr(dtpOfferTo.Value.Year).Substring(2, 2)

        If chkBoxOffer.Checked Then
            offer = 1
            offerType = 1
            offerX = 1
            If txtBoxOfferDisc.Text = String.Empty Or txtBoxOfferDisc.Text = "" Or txtBoxOfferDiscAt.Text = String.Empty Or txtBoxOfferDiscAt.Text = "" Then
                MessageBox.Show("Δεν έχετε συμπληρώσει όλα τα πεδία προσφοράς", ERROR_MSG, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            If Not IsNumeric(txtBoxOfferDisc.Text) Or Not IsNumeric(txtBoxOfferDiscAt.Text) Then
                MessageBox.Show("Τα πεδία προσφοράς πρέπει να είναι αριθμοί", ERROR_MSG, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            If Not (Math.Floor(CDbl(txtBoxOfferDiscAt.Text)) = Math.Ceiling(CDbl(txtBoxOfferDiscAt.Text))) Then
                MessageBox.Show("Το πεδίο 'έκπτωση στα' πρέπει να είναι ακέραιος αριθμός", ERROR_MSG, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            If CDbl(txtBoxOfferDisc.Text) <= 0 Or CDbl(txtBoxOfferDiscAt.Text) <= 0 Then
                MessageBox.Show("Ελέξετε τα πεδία προσφοράς", ERROR_MSG, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            offerDisc = Math.Round(CDbl(txtBoxOfferDisc.Text), 2)
            offerAt = CDbl(txtBoxOfferDiscAt.Text)

            If DateDiff(DateInterval.Day, Date.Now, dtpOfferTo.Value) < 0 Then
                MessageBox.Show(Me, "Η ημερομηνία λήξης προσφοράς δεν μπορεί να είναι μικρότερη από την σημερινή", "Ημερομηνία Προσφοράς", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            If DateDiff(DateInterval.Day, dtpOfferFrom.Value, dtpOfferTo.Value) < 0 Then
                MessageBox.Show(Me, "Η ημερομηνία έναρξης προσφοράς δεν μπορεί να είναι μεγαλύτερη από την ημερομηνία λήξης", "Ημερομηνία Προσφοράς", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If
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
            If newProduct Then
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
                      "                      offer, offer_type, offer_x, offer_y, offer_disc, offer_at, lastmodifiedby, isbox, BOX_QNT, offerfromdate, offertodate)" & _
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
                      "                      " & offer & ", " & _
                      "                      " & offerType & ", " & _
                      "                      " & offerX & ", " & _
                      "                      " & offerY & ", " & _
                      "                      " & offerDisc & ", " & _
                      "                      " & offerAt & ", " & _
                      "                      '" & whois & "', " & _
                      "                      " & isbox & ", " & _
                      "                      " & boxQnt & ", " & _
                      "                      to_date('" & offerFromDate & "','DD-MM-YY'), " & _
                      "                      to_date('" & offerToDate & "','DD-MM-YY'))"

                cmd = New OracleCommand(sql, conn)
                cmd.CommandType = CommandType.Text
                cmd.ExecuteNonQuery()
                txtBoxAvailQuantity.Text = 0
            Else
                Dim minQuantity As Integer = 0
                If txtBoxMinQuantity.Text <> String.Empty And txtBoxMinQuantity.Text.Length > 0 Then
                    minQuantity = CInt(txtBoxMinQuantity.Text)
                End If

                sql = "update products set description = '" & txtBoxDescription.Text.Replace("'", "`") & "'," & _
                                         " buy_amt = " & txtBoxBuyAmt.Text.Replace(",", ".") & "," & _
                                         " buy_amt_no_vat = " & txtBoxBuyAmtNoVAT.Text.Replace(",", ".") & "," & _
                                         " sell_amt = " & txtBoxSellAmt.Text.Replace(",", ".") & "," & _
                                         " category_id = '" & categoryId & "'," & _
                                         " vattype_id = '" & vatTypeId & "'," & _
                                         " supplier_id = '" & supplierId & "'," & _
                                         " avail_quantity = '" & availQuantity & "'," & _
                                         " min_quantity = " & txtBoxMinQuantity.Text & ", " & _
                                         " alert_on_min = " & alertOnMin & ",  " & _
                                         " alert_on_expiry = " & alertOnExpiry & ",  " & _
                                         " expiry_date = to_date('" & expiryDate & "','DD-MM-YY'),  " & _
                                         " alert_date = to_date('" & alertDate & "','DD-MM-YY'),  " & _
                                         " notes = '" & notes & "', " & _
                                         " stock_quantity = '" & txtBoxStockQuantity.Text & "', " & _
                                         " amt_profit = " & CDbl(txtBoxProfit.Text) & ", " & _
                                         " profit_percent = " & CDbl(txtBoxProfitPercentage.Text) & ", " & _
                                         " offer = " & offer & ", " & _
                                         " offer_type = " & offerType & ", " & _
                                         " offer_x = " & offerX & ", " & _
                                         " offer_y = " & offerY & ", " & _
                                         " offer_disc = " & offerDisc & ", " & _
                                         " offer_at = " & offerAt & ", " & _
                                         " lastmodifiedby = '" & whois & "', " & _
                                         " lastmodifiedscreen = 1 ," & _
                                         " isbox = " & isbox & ", " & _
                                         " box_qnt = " & boxQnt & ", " & _
                                         " offerFromDate = to_date('" & offerFromDate & "','DD-MM-YY'),  " & _
                                         " offerToDate = to_date('" & offerToDate & "','DD-MM-YY')  " & _
                    "where serno =      " & serno & ""

                cmd = New OracleCommand(sql, conn)
                cmd.CommandType = CommandType.Text
                cmd.ExecuteNonQuery()
            End If

            sql = "delete from barcodes where product_serno = " & serno & ""
            cmd = New OracleCommand(sql, conn)
            cmd.CommandType = CommandType.Text
            cmd.ExecuteNonQuery()

            For i As Integer = 0 To lstBoxBarcodes.Items.Count - 1
                sql = "insert into barcodes (product_serno, barcode) values (" & serno & ", '" & lstBoxBarcodes.Items.Item(i) & "')"
                cmd = New OracleCommand(sql, conn)
                cmd.CommandType = CommandType.Text
                cmd.ExecuteNonQuery()
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
        Return False
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
        txtBoxSearchBox.Focus()
    End Sub

    Private Sub lnkLblAddQuantity_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkLblAddQuantity.LinkClicked
        'If serno = -1 Then
        'MessageBox.Show("Δεν μπορεί να γίνει προσθήκη ποσότητας αν δεν έχει αποθηκευτεί το προϊόν", "Καταχώρηση Ποσότητας", MessageBoxButtons.OK, MessageBoxIcon.Error)
        'Exit Sub
        'Else
        Dim newQuantity As Object
        newQuantity = InputBox("Προσθήκη Νέας Ποσότητας", "Νέα Ποσότητα", "")
        If Not IsNumeric(newQuantity) Then
            MessageBox.Show("Η ποσότητα πρέπει να είναι αριθμός", "Καταχώρηση Ποσότητας", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        Else
            'Dim sql As String = "update products set avail_quantity = (" & CInt(newQuantity) & " + avail_quantity) " & _
            '                    "where serno = " & serno & " "
            'Dim cmd As New OracleCommand(sql, conn)
            'cmd.CommandType = CommandType.Text
            'cmd.ExecuteReader()
            'cmd.Dispose()
            Dim currentQuantity As Integer = 0
            If txtBoxAvailQuantity.Text.Length > 0 Then
                Try
                    currentQuantity = CInt(txtBoxAvailQuantity.Text)
                Catch ex As Exception
                End Try
            End If
            txtBoxAvailQuantity.Text = newQuantity + currentQuantity
        End If
        'End If
        txtBoxSearchBox.Focus()
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        lstBoxBarcodes.Items.Clear()
        newProduct = True
        Dim categoryIdtemp As String = ""
        Dim vattypeIdtemp As String = ""
        Dim supplierIdtemp As String = ""
        Dim cmd As New OracleCommand("", conn)
        Dim dr As OracleDataReader
        Dim sql As String = ""
        resetFieldsNoSearchBox()

        Try
            sql = "select p.serno, p.description, buy_amt, buy_amt_no_vat, sell_amt, " & _
                  "       NVL(category_id, ' ') cat_id, supplier_id, nvl(avail_quantity,0) avq, min_quantity, alert_on_min, " & _
                  "       expiry_date, alert_date, notes, alert_on_expiry, nvl(stock_quantity,0) stq, amt_profit, profit_percent, " & _
                  "       nvl(offer,0) offer, nvl(offer_type,-1) offerType, nvl(offer_x,0) offerx, nvl(offer_y,0) offery, " & _
                  "       nvl(offer_disc,0) offerdisc, nvl(offer_at,0) offerat, vattype_id, v.vat vat, NVL(p.isbox,0) isbox, NVL(p.box_qnt,0) box_qnt, " & _
                  "       nvl(offerfromdate, sysdate) offerfromdate, nvl(offertodate, sysdate) offertodate " & _
                  "from products p " & _
                  "inner join vat_types v on p.vattype_id = v.uuid " & _
                  "where serno = (select product_serno from barcodes where Upper(barcode) = '" & txtBoxSearchBox.Text.ToUpper & "')"

            cmd = New OracleCommand(sql, conn)
            Dim counter As Integer = 0
            Dim result
            cmd.CommandType = CommandType.Text
            dr = cmd.ExecuteReader()

            If dr.Read() Then
                newProduct = False
                chkboxBox.Enabled = True
                serno = CInt(dr("serno"))
                tmpGlobalProductSerno = serno
                txtBoxDescription.Text = dr("description")
                txtBoxBuyAmt.Text = dr("buy_amt")
                txtBoxVAT.Text = dr("vat")

                Dim buyAmtNoVat As Double = CDbl(dr("buy_amt_no_vat"))

                txtBoxBuyAmtNoVAT.Text = buyAmtNoVat.ToString("N3")
                txtBoxSellAmt.Text = dr("sell_amt")

                txtBoxNotes.Text = dr("notes")

                dtpExpiry.Value = CDate(dr("expiry_date"))
                dtpAlert.Value = CDate(dr("alert_date"))

                dtpOfferFrom.Value = CDate(dr("offerfromdate"))
                dtpOfferTo.Value = CDate(dr("offertodate"))

                categoryIdtemp = dr("cat_id")
                vattypeIdtemp = dr("vattype_id")
                supplierIdtemp = dr("supplier_id")

                txtBoxAvailQuantity.Text = dr("avq")
                txtBoxStockQuantity.Text = dr("stq")
                txtBoxMinQuantity.Text = dr("min_quantity")
                txtBoxProfit.Text = dr("amt_profit")
                txtBoxProfitPercentage.Text = dr("profit_percent")

                If CInt(dr("alert_on_min")) = 1 Then
                    ckboxAlert.Checked = True
                    txtBoxMinQuantity.ReadOnly = False
                Else
                    txtBoxMinQuantity.ReadOnly = True
                End If

                If CInt(dr("alert_on_expiry")) = 1 Then
                    chkBoxAlertExpiry.Checked = True
                Else
                    chkBoxAlertExpiry.Checked = False
                End If

                If CInt(dr("offer")) = 1 Then
                    chkBoxOffer.Checked = True
                    If CInt(dr("offerType")) = 1 Then
                        txtBoxOfferDisc.Text = CDbl(dr("offerdisc"))
                        txtBoxOfferDiscAt.Text = CInt(dr("offerat"))
                    End If
                    setDiscountComponents()
                Else
                    chkBoxOffer.Checked = False
                End If

                If CInt(dr("isbox")) = 1 Then
                    chkboxBox.Checked = True
                    txtBoxBoxUnits.Text = CInt(dr("box_qnt"))
                End If

                sql = "select barcode " & _
                      "from barcodes " & _
                      "where product_serno = " & serno & " "

                cmd = New OracleCommand(sql, conn)
                cmd.CommandType = CommandType.Text
                dr = cmd.ExecuteReader()
                While dr.Read
                    lstBoxBarcodes.Items.Add(dr("barcode"))
                End While
            Else
                chkboxBox.Enabled = False
                result = MessageBox.Show("Το προϊόν δεν βρέθηκε, προσθήκη;", "Νέο Προϊόν", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
                If result = DialogResult.No Or result = DialogResult.Cancel Then
                    resetFields()
                Else
                    If checkIfExistsInDB(txtBoxSearchBox.Text) Then
                        MessageBox.Show("Ο κωδικός Barcode είναι ήδη καταχωρημένος σε άλλο προϊόν", "Καταχώρηση Barcode", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                        Exit Sub
                    End If
                    If Not checkIfExist(txtBoxSearchBox.Text) Then
                        lstBoxBarcodes.Items.Add(txtBoxSearchBox.Text)
                    End If
                    txtBoxSearchBox.Clear()
                End If
            End If
            dr.Close()
            If Not newProduct Or result = DialogResult.Yes Then
                enableComponents()
                fillCategories()
                fillVatTypes()
                fillSuppliers()
            End If

            If Not newProduct Then
                categoryId = categoryIdtemp
                vatTypeId = vattypeIdtemp
                supplierId = supplierIdtemp

                If categoryUUIDs.IndexOf(categoryId) <> -1 Then
                    cmdBoxCategoy.SelectedIndex = categoryUUIDs.IndexOf(categoryId)
                End If

                If supplierUUIDs.IndexOf(supplierId) <> -1 Then
                    cmbSupplier.SelectedIndex = supplierUUIDs.IndexOf(supplierId)
                End If

                If vatTypeUUIDs.IndexOf(vatTypeId) <> -1 Then
                    cmdBoxVatType.SelectedIndex = vatTypeUUIDs.IndexOf(vatTypeId)
                End If
            End If
            calculateWithVat()
            txtBoxDescription.Focus()
        Catch ex As Exception
            createExceptionFile(ex.Message, sql)
            MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            cmd.Dispose()
        End Try
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        resetFields()
    End Sub

    Private Sub btnCalculateWithoutVAT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCalculateWithoutVAT.Click
        calculateAmounts()        
    End Sub

    Private Sub calculateAmounts()
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
            txtBoxBuyAmt.Text = TruncateDecimal(amtWithVAT, 3)
        Catch ex As Exception
            Exit Sub
        End Try
    End Sub

    Private Sub calculateWithVat()
        Try
            Dim vat As Integer = CInt(txtBoxVAT.Text)
            Dim amtWithNoVAT As Double = 0
            Dim temp = txtBoxBuyAmt.Text.Replace(",", ".")
            amtWithNoVAT = CDbl(temp)
            If vat <> 0 Then
                amtWithNoVAT = CDbl(temp) / (1 + (CInt(txtBoxVAT.Text) / 100))
            End If
            txtBoxBuyAmtNoVAT.Text = TruncateDecimal(amtWithNoVAT, 3)
        Catch ex As Exception
            Exit Sub
        End Try
    End Sub

    Private Sub btnCalculateWithVAT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCalculateWithVAT.Click
        calculateWithVat()
    End Sub

    Private Sub calculateProfitPercent()
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
            txtBoxProfit.Text = difference.ToString("N3")
            diffPercentage = ((sellAmt * 100) / buyAmtWithVAT) - 100
            txtBoxProfitPercentage.Text = diffPercentage.ToString("N3")
        Catch ex As Exception
            Exit Sub
        End Try
    End Sub

    Private Sub txtBoxSellAmt_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBoxSellAmt.TextChanged
        calculateProfitPercent()
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

    Private Sub chkBoxOffer_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkBoxOffer.CheckedChanged
        If chkBoxOffer.Checked Then
            txtBoxOfferDisc.Visible = True
            lblOfferDisc.Visible = True
            txtBoxOfferDiscAt.Visible = True
            lblOfferFrom.Visible = True
            lblOfferTo.Visible = True
            dtpOfferFrom.Visible = True
            dtpOfferTo.Visible = True
            dtpOfferFrom.Value = Today
            dtpOfferTo.Value = Today
        Else
            txtBoxOfferDisc.Visible = False
            lblOfferDisc.Visible = False
            txtBoxOfferDiscAt.Visible = False
            lblOfferFrom.Visible = False
            lblOfferTo.Visible = False
            dtpOfferFrom.Visible = False
            dtpOfferTo.Visible = False
        End If
        setDiscountComponents()
    End Sub

    Private Sub setDiscountComponents()
        If chkBoxOffer.Checked Then
            txtBoxOfferDisc.Visible = True
            txtBoxOfferDiscAt.Visible = True
            lblOfferDisc.Visible = True
        Else
            txtBoxOfferDisc.Visible = False
            txtBoxOfferDiscAt.Visible = False
            lblOfferDisc.Visible = False
        End If
        txtBoxOfferX.Visible = False
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
        If isAdmin Or canEditProductsFull Then
            txtBoxBuyAmtNoVAT.Visible = True
            btnCalculateWithoutVAT.Visible = True
            Label1.Visible = True
            lblBuyAmt.Visible = True
            txtBoxProfit.Visible = True
            txtBoxProfitPercentage.Visible = True
            Label2.Visible = True
            btnCalculateWithVAT.Visible = True
            txtBoxBuyAmt.Visible = True
        Else
            txtBoxBuyAmtNoVAT.Visible = False
            btnCalculateWithoutVAT.Visible = False
            Label1.Visible = False
            lblBuyAmt.Visible = False
            txtBoxProfit.Visible = False
            txtBoxProfitPercentage.Visible = False
            Label2.Visible = False
            btnCalculateWithVAT.Visible = False
            txtBoxBuyAmt.Visible = False
        End If
    End Sub

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkLblBoxBarcodes.LinkClicked
        frmBoxBarcodesModal.ShowDialog()

    End Sub

    Private Sub chkboxBox_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkboxBox.CheckedChanged
        If chkboxBox.Checked Then
            txtBoxBoxUnits.ReadOnly = False
            lnkLblBoxBarcodes.Enabled = True
        Else
            txtBoxBoxUnits.ReadOnly = True
            lnkLblBoxBarcodes.Enabled = False
        End If
    End Sub

    Private Function checkBoxLinkedProducts() As Boolean
        Dim cmd As New OracleCommand("", conn)
        Dim dr As OracleDataReader
        Try
            Dim found As Integer = 0
            Dim sql As String = "select count(*) from boxbarcodes where product_serno = " & tmpGlobalProductSerno & ""
            cmd = New OracleCommand(sql, conn)
            cmd.CommandType = CommandType.Text
            dr = cmd.ExecuteReader()
            If dr.Read() Then
                found = CInt(dr(0))
            End If
            cmd.Dispose()
            dr.Close()

            If found > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            createExceptionFile(ex.Message, "")
            MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
        End Try
    End Function

    Private Sub txtBoxBuyAmtNoVAT_TextChanged(sender As Object, e As EventArgs) Handles txtBoxBuyAmtNoVAT.TextChanged
        calculateAmounts()
        calculateProfitPercent()
    End Sub
End Class