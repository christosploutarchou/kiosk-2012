Imports Oracle.DataAccess.Client
Imports System.ComponentModel

Public Class frmPOS

    Dim totalAmt As New Double
    Dim totalAmt0 As New Double
    Dim totalAmt5 As New Double
    Dim totalAmt19 As New Double
    Dim totalAmt3 As New Double
    Dim totalItems As New Double
    Dim totalDiscount As New Double
    Dim totalWithDiscount As New Double
    Dim payment As New Double
    Dim returnAmount As New Double
    Dim returnProduct As New Double
    Dim receiptSerno As Integer
    Dim componentsEnabled As Boolean = True

    Dim paymentMethod As String
    Dim productsAndQuantity As New Dictionary(Of String, Integer)

    Dim kronosItems As New ArrayList
    Dim tmpkronosItems As New ArrayList
    Public tmpKronosSelectedIndex As Integer = -1

    Dim offerXYItems As New ArrayList
    Dim offerDiscAtItems As New ArrayList

    Dim printType As String

    Private Sub setAmount(ByVal value As String)
        txtBoxManualAmt.Text += value
        If String.Compare(".", value) = 0 Or String.Compare("0.", value) = 0 Then
            Exit Sub
        End If
        If validateAmount(txtBoxManualAmt.Text) Then
            Exit Sub
        End If
        Dim tmpAmount = Math.Round(CDbl(txtBoxManualAmt.Text), 2)
        txtBoxManualAmt.Text = tmpAmount
    End Sub

    Private Function validateAmount(ByVal amt As String) As Boolean
        For i As Integer = 0 To 9999
            If amt.Equals(i.ToString & ".0") Then
                Return True
            End If
        Next
        Return False
    End Function

    Private Sub btn7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn7.Click
        setAmount("7")
    End Sub

    Private Sub btn8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn8.Click
        setAmount("8")
    End Sub

    Private Sub btn9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn9.Click
        setAmount("9")
    End Sub

    Private Sub btn4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn4.Click
        setAmount("4")
    End Sub

    Private Sub btn5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn5.Click
        setAmount("5")
    End Sub

    Private Sub btn6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn6.Click
        setAmount("6")
    End Sub

    Private Sub btn1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn1.Click
        setAmount("1")
    End Sub

    Private Sub btn2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn2.Click
        setAmount("2")
    End Sub

    Private Sub btn3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn3.Click
        setAmount("3")
    End Sub

    Private Sub btn0_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn0.Click
        setAmount("0")
    End Sub

    Private Sub btnDot_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDot.Click
        If txtBoxManualAmt.Text.Contains(".") Then
            Exit Sub
        End If

        If txtBoxManualAmt.Text.Length = 0 Or txtBoxManualAmt.Text = String.Empty Then
            setAmount("0.")
            Exit Sub
        End If
        setAmount(".")
    End Sub

    Private Sub txtBoxBarcode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBoxBarcode.TextChanged
        If Not isLoggedIn(username) Then
            MessageBox.Show("Ο χρήστης δεν ειναι συνδεμένος", "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If Not isConnOpen() Then
            MessageBox.Show(CANNOT_ACCESS_DB, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        Dim productFound As Boolean = False

        Dim cmd As New OracleCommand("", conn)
        Dim sql As String = ""
        Try
            Dim dr As OracleDataReader
            If txtBoxBarcode.Text.Length >= minBarcode Then
                sql = "select p.serno, p.description, p.sell_amt, v.vat, " & _
                      "nvl(offer,-1), nvl(offer_type,-1), nvl(offer_x,0), nvl(offer_y,0), nvl(offer_disc,0), nvl(offer_at,0), " & _
                      "nvl(isbox,0) isbox,  nvl(box_qnt,0) box_qnt, " & _
                      "nvl(offerfromdate, sysdate) offerfromdate , nvl(offertodate, sysdate) offertodate " & _
                      "from products p " & _
                      "inner join vat_types v on p.vattype_id = v.uuid " & _
                      "where p.serno = (select b.product_serno from BARCODES b where UPPER(b.barcode) =  '" & txtBoxBarcode.Text.ToUpper & "')"

                cmd = New OracleCommand(sql, conn)
                cmd.CommandType = CommandType.Text

                dr = cmd.ExecuteReader()

                If dr.Read() Then
                    totalItems += 1
                    productFound = True
                    Dim sellAmt As Double = CDbl(dr(2))
                    Dim currentAmt As Double = sellAmt * CInt(txtBoxQuantity.Text)
                    currentAmt *= returnProduct

                    Dim row As String() = New String() {dr(0), totalItems, dr(1), txtBoxQuantity.Text, sellAmt.ToString("N2"), currentAmt.ToString("N2"), dr(3), "0", CInt(dr("isbox")), CInt(dr("box_qnt"))}
                    dgvReceipt.Rows.Add(row)
                    totalAmt += Math.Round(currentAmt, 2)
                    totalWithDiscount += Math.Round(currentAmt, 2)
                    txtBoxTotalAmt.Text = totalAmt.ToString("N2")

                    Dim tmpVat = CDbl(dr(2)) * returnProduct * CInt(txtBoxQuantity.Text)

                    If CInt(dr(3)) = 0 Then
                        totalAmt0 += tmpVat
                    ElseIf CInt(dr(3)) = 3 Then
                        totalAmt3 += tmpVat
                    ElseIf CInt(dr(3)) = 5 Then
                        totalAmt5 += tmpVat
                    ElseIf CInt(dr(3)) = 19 Then
                        totalAmt19 += tmpVat
                    End If
                    fillProductsAndQuantity(CStr(dr(0)))

                    Dim offer As Integer = CInt(dr(4))
                    Dim dateOfferFrom As Date = CDate(dr("offerfromdate"))
                    Dim dateOfferTo As Date = CDate(dr("offertodate"))
                    If offer <> -1 Then
                        Dim foundProduct As Boolean = False
                        Dim productSerno As Integer = dr(0)
                        Dim offerType As Integer = CInt(dr(5))
                        If offerType = 1 And _
                        DateTime.Compare(Date.Now, dateOfferFrom) > 0 And _
                        DateTime.Compare(Date.Now, dateOfferTo) < 0 Then
                            setDiscountOfferType1(productSerno, CInt(dr(9)), CDbl(dr(8)))
                        End If
                    End If
                    dr.Close()
                End If
            End If

            If productFound Then
                txtBoxTotalWithDiscount.Text = totalWithDiscount.ToString("N2")
                txtBoxQuantity.Text = "1"
                txtBoxBarcode.Clear()
                txtBoxBarcode.Focus()
                chkBoxReturnProduct.Checked = False
            End If
        Catch ex As Exception
            createExceptionFile(ex.Message, " " & sql)
            MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            cmd.Dispose()
            formatDataGrid()
        End Try
    End Sub

    Private Sub setDiscountOfferType1(ByVal productSerno As Integer, ByVal discountAt As Integer, ByVal discountAmt As Double)
        Dim tmpOfferDiscAtItem As OfferTypeDiscAt
        Dim found As Boolean = False
        Dim index As Integer = 0
        For Each tmpOfferDiscAtItem In offerDiscAtItems
            If tmpOfferDiscAtItem.productSerno = productSerno Then
                found = True
                tmpOfferDiscAtItem.productSerno = productSerno
                tmpOfferDiscAtItem.currentQuantity = tmpOfferDiscAtItem.currentQuantity + txtBoxQuantity.Text
                tmpOfferDiscAtItem.discountAmt = discountAmt
                tmpOfferDiscAtItem.discountAt = discountAt
                If tmpOfferDiscAtItem.currentDiscount > 0 Then
                    totalDiscount -= tmpOfferDiscAtItem.currentDiscount
                End If

                If CInt(tmpOfferDiscAtItem.currentQuantity) = discountAt Then
                    tmpOfferDiscAtItem.currentDiscount = discountAmt
                ElseIf (CInt(tmpOfferDiscAtItem.currentQuantity) > discountAt) Then
                    Dim tmp As Double = tmpOfferDiscAtItem.currentQuantity / discountAt
                    If Not (Math.Floor(CDbl(tmp)) = Math.Ceiling(CDbl(tmp))) Then
                        tmp = Math.Truncate(tmp)
                    End If
                    tmpOfferDiscAtItem.currentDiscount = discountAmt * tmp
                End If
                Exit For
            End If
        Next

        If Not found Then
            tmpOfferDiscAtItem.productSerno = productSerno
            tmpOfferDiscAtItem.currentQuantity = txtBoxQuantity.Text
            tmpOfferDiscAtItem.discountAmt = discountAmt
            tmpOfferDiscAtItem.discountAt = discountAt
            tmpOfferDiscAtItem.currentDiscount = 0

            If CInt(txtBoxQuantity.Text) = discountAt Then
                tmpOfferDiscAtItem.currentDiscount = discountAmt
            ElseIf (CInt(txtBoxQuantity.Text) > discountAt) Then
                Dim tmp As Double = tmpOfferDiscAtItem.currentQuantity / discountAt
                If Not (Math.Floor(CDbl(tmp)) = Math.Ceiling(CDbl(tmp))) Then
                    tmp = Math.Truncate(tmp)
                End If
                tmpOfferDiscAtItem.currentDiscount = discountAmt * tmp
            End If
            offerDiscAtItems.Add(tmpOfferDiscAtItem)
        Else
            offerDiscAtItems.RemoveAt(index)
            offerDiscAtItems.Add(tmpOfferDiscAtItem)
        End If

        totalDiscount += tmpOfferDiscAtItem.currentDiscount
        txtBoxDiscount.Text = totalDiscount.ToString("N2")
        If chkBoxReturnProduct.Checked Then
            totalWithDiscount = CStr(totalAmt + totalDiscount)
        Else
            totalWithDiscount = CStr(totalAmt - totalDiscount)
        End If

        txtBoxTotalWithDiscount.Text = totalWithDiscount.ToString("N2")

        If txtBoxPaymentAmt.Text.Length > 0 And CDbl(txtBoxPaymentAmt.Text) > 0 Then
            Dim tmp As Double = CDbl(txtBoxTotalWithDiscount.Text)
            Dim tmpPayment As Double = CDbl(txtBoxPaymentAmt.Text)
            tmp = tmp - tmpPayment
            txtBoxReturnAmt.Text = tmp.ToString("N2")
        End If
    End Sub

    Private Sub fillProductsAndQuantity(ByVal productSerno As String)
        If productsAndQuantity.ContainsKey(productSerno) Then
            productsAndQuantity(productSerno) = CInt(txtBoxQuantity.Text) + productsAndQuantity(productSerno)
        Else
            productsAndQuantity.Add(productSerno, CInt(txtBoxQuantity.Text))
        End If
    End Sub

    Private Sub frmPOS_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        If isAdmin Or canEditProducts Or canEditProductsFull Then
            frmMain.Show()
        Else
            frmLogin.Dispose()
            'frmLogin.Show()        
        End If
    End Sub

    Private Sub frmPOS_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        e.Cancel = True
    End Sub

    Private Sub frmPOS_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtBoxQuantity.Text = "1"
        returnProduct = 1
        setAmounts()
        setPosButtons(1)
        txtBoxBarcode.Focus()
        lblCurrentUser.Text = "User: " & username
    End Sub

    Private Sub setPosButtons(ByVal all As Integer)
        Dim sql As String = ""
        Dim cmd As New OracleCommand("", conn)
        Dim dr As OracleDataReader
        btnItemsMap.Clear()
        Try
            If all = 1 Then
                For i As Integer = 1 To 23
                    sql = "select disp_name, NVL(is_visible,0) from BTN_POS" + i.ToString
                    cmd = New OracleCommand(sql, conn)
                    dr = cmd.ExecuteReader
                    If dr.Read() Then
                        If CInt(dr(1)) = 1 Then
                            CType(Me.Controls("btnPos" & i), Button).Visible = True
                            CType(Me.Controls("btnPos" & i), Button).Text = dr(0)
                        End If

                        Dim tmpBtnItem = New BtnItem
                        tmpBtnItem.Id = "btnPos" & i
                        tmpBtnItem.Name = dr(0)
                        'TODO: DISCOUNT ON POS BUTTONS
                        sql = "select DISPLAY_DESC, product_serno, p.DESCRIPTION, " & _
                              "(select vat from vat_types where uuid=p.VATTYPE_ID), p.SELL_AMT, NVL(p.isbox,0), NVL(p.box_qnt,0), " & _
                              "nvl(p.offer,-1), nvl(p.offer_type,-1), nvl(p.offer_x,0), nvl(p.offer_y,0), nvl(p.offer_disc,0), nvl(p.offer_at,0), " & _
                              "nvl(p.offerfromdate, sysdate) offerfromdate , nvl(p.offertodate, sysdate) offertodate " & _
                              "from BTN_POS" + i.ToString + "_DET d " & _
                              "inner join PRODUCTS p on p.serno = d.PRODUCT_SERNO " & _
                              "order by nvl(seqno, -1)"
                        cmd = New OracleCommand(sql, conn)
                        dr = cmd.ExecuteReader
                        Dim tmpBtnItemDetails As BtnItemDetails
                        Dim tmpDetailsArrayList As New ArrayList
                        While dr.Read
                            tmpBtnItemDetails = New BtnItemDetails
                            tmpBtnItemDetails.DisplayDesc = dr(0)
                            tmpBtnItemDetails.ProductSerno = CInt(dr(1))
                            tmpBtnItemDetails.Description = dr(2)
                            tmpBtnItemDetails.Vat = CInt(dr(3))
                            tmpBtnItemDetails.ProductPrice = CDbl(dr(4))
                            tmpBtnItemDetails.IsBox = CInt(dr(5))
                            tmpBtnItemDetails.BoxQnt = CInt(dr(6))
                            tmpDetailsArrayList.Add(tmpBtnItemDetails)
                        End While
                        tmpBtnItem.LinkedItemsDetails = tmpDetailsArrayList
                        tmpBtnItem.LinkedItems = tmpBtnItem.LinkedItemsDetails.Count
                        btnItemsMap.Add("btnPos" & i, tmpBtnItem)
                    End If
                    dr.Close()
                Next
            Else
                sql = "select disp_name, is_visible from BTN_POS" + setCurrentTableIndex()
                cmd = New OracleCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read() Then
                    CType(Me.Controls("btnPos" & setCurrentTableIndex()), Button).Text = dr(0)
                    If CInt(dr(1)) = 0 Then
                        CType(Me.Controls("btnPos" & setCurrentTableIndex()), Button).BackColor = Color.Red
                    Else
                        CType(Me.Controls("btnPos" & setCurrentTableIndex()), Button).BackColor = Color.Blue
                    End If
                Else
                    CType(Me.Controls("btnPos" & setCurrentTableIndex()), Button).BackColor = Color.Red
                    CType(Me.Controls("btnPos" & setCurrentTableIndex()), Button).Text = ""
                End If
                dr.Close()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message + sql, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            cmd.Dispose()
        End Try
    End Sub

    Private Function setCurrentTableIndex() As String
        If currentBtnPos.Equals("btnPos1") Or currentBtnPos.Equals("btnPos2") Or currentBtnPos.Equals("btnPos3") Or _
           currentBtnPos.Equals("btnPos4") Or currentBtnPos.Equals("btnPos5") Or currentBtnPos.Equals("btnPos6") Or _
           currentBtnPos.Equals("btnPos7") Or currentBtnPos.Equals("btnPos8") Or currentBtnPos.Equals("btnPos9") Then
            Return currentBtnPos.Substring(6, 1)
        Else
            Return currentBtnPos.Substring(6, 2)
        End If
    End Function

    Private Sub setAmounts()
        totalWithDiscount = 0.0
        totalAmt = 0.0
        totalDiscount = 0.0
        payment = 0.0
        returnAmount = 0.0

        totalAmt0 = 0.0
        totalAmt3 = 0.0
        totalAmt5 = 0.0
        totalAmt19 = 0.0
        totalItems = 0

        txtBoxTotalWithDiscount.Text = totalWithDiscount.ToString("N2")
        txtBoxTotalAmt.Text = totalAmt.ToString("N2")
        txtBoxDiscount.Text = totalDiscount.ToString("N2")
        txtBoxPaymentAmt.Text = payment.ToString("N2")
        txtBoxReturnAmt.Text = returnAmount.ToString("N2")
    End Sub

    Private Sub btnAddQuantity_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddQuantity.Click
        txtBoxQuantity.Text = CStr((CInt(txtBoxQuantity.Text) + 1))
        txtBoxBarcode.Focus()
    End Sub

    Private Sub btnRemoveQuantity_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveQuantity.Click
        Dim temp As Integer = CInt(txtBoxQuantity.Text) - 1
        If temp > 0 Then
            txtBoxQuantity.Text = temp
        Else
            txtBoxQuantity.Text = "1"
        End If
        txtBoxBarcode.Focus()
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        If dgvReceipt.RowCount > 0 Then
            MessageBox.Show("Δεν μπορεί να γίνει έξοδος όταν υπάρχουν γραμμές προϊόντων", "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If MessageBox.Show("Έξοδος;", "Έξοδος", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            If Not isAdmin And Not canEditProducts And Not canEditProductsFull Then
                If generateXreport(whois) Then
                    If MessageBox.Show("Εκτύπωση Αναφοράς Βάρδιας;", "Εκτύπωση", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        printType = "X"
                        PrintDocument1.PrinterSettings.Copies = 1
                        PrintDocument1.Print()
                    End If
                End If
            End If

            If Not isAdmin And Not canEditProducts And Not canEditProductsFull Then
                logoutUserUUID(whois)
            End If

            If dualMonitor Then
                frmDual.dgvReceiptDual.Rows.Clear()
                frmDual.txtBoxTotalDual.Text = "0.00"
                frmDual.txtBoxDualDisc.Text = "0.00"
                frmDual.txtBoxDualFinal.Text = "0.00"
            End If

            Me.Dispose()
        End If
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        txtBoxManualAmt.Clear()
        txtBoxBarcode.Focus()
    End Sub

    Private Sub btnBackspace_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackspace.Click
        If txtBoxManualAmt.Text.Length > 0 Then
            If txtBoxManualAmt.Text.Length = 2 Then
                If txtBoxManualAmt.Text.Contains(".") Then
                    txtBoxManualAmt.Clear()
                Else
                    txtBoxManualAmt.Text = txtBoxManualAmt.Text.Substring(0, txtBoxManualAmt.Text.Length - 1)
                End If
            Else
                txtBoxManualAmt.Text = txtBoxManualAmt.Text.Substring(0, txtBoxManualAmt.Text.Length - 1)
            End If
        End If
        txtBoxBarcode.Focus()
    End Sub

    Private Sub addNewRow(ByVal serno As String, ByVal item As String, ByVal vat As Integer, ByVal unitPrice As Double, ByVal isBox As Integer, ByVal boxQnt As Integer)
        If dualMonitor Then
            BackgroundWorker1.Dispose()
            frmDual.dgvReceiptDual.Rows.Clear()
            frmDual.txtBoxTotalDual.Text = "0.00"
            frmDual.txtBoxDualDisc.Text = "0.00"
            frmDual.txtBoxDualFinal.Text = "0.00"
        End If

        Dim tmpAmt As Double = 0
        tmpAmt = Math.Round(unitPrice * CInt(txtBoxQuantity.Text), 2)

        tmpAmt *= returnProduct
        If vat = 0 Then
            totalAmt0 += tmpAmt
        ElseIf vat = 3 Then
            totalAmt3 += tmpAmt
        ElseIf vat = 5 Then
            totalAmt5 += tmpAmt
        ElseIf vat = 19 Then
            totalAmt19 += tmpAmt
        End If

        totalItems = dgvReceipt.Rows.Count + 1
        Dim row As String() = New String() {serno, totalItems, item, txtBoxQuantity.Text, unitPrice.ToString("N2"), tmpAmt.ToString("N2"), vat, "0", isBox, boxQnt}
        dgvReceipt.Rows.Add(row)
        totalAmt += Math.Round(tmpAmt, 2)
        totalWithDiscount += Math.Round(tmpAmt, 2)

        txtBoxTotalAmt.Text = totalAmt.ToString("N2")
        txtBoxTotalWithDiscount.Text = totalWithDiscount.ToString("N2")

        txtBoxQuantity.Text = "1"
        txtBoxBarcode.Clear()
        txtBoxManualAmt.Clear()
        chkBoxReturnProduct.Checked = False
        returnProduct = 1

        txtBoxBarcode.Focus()
        formatDataGrid()
    End Sub

    Private Sub formatDataGrid()
        If dgvReceipt.Rows.Count > 0 Then
            dgvReceipt.FirstDisplayedScrollingRowIndex = dgvReceipt.Rows.Count - 1
        End If

        For i As Integer = 0 To dgvReceipt.Rows.Count - 1
            If dgvReceipt.Rows(i).Cells("amount").Value() < 0 Then
                dgvReceipt.Rows(i).DefaultCellStyle.ForeColor = Color.Red
            End If
        Next

        If dualMonitor Then
            setDualMonitorContents()
        End If
    End Sub

    Private Sub setDualMonitorContents()
        frmDual.dgvReceiptDual.Rows.Clear()

        If dgvReceipt.Rows.Count > 0 Then
            For i As Integer = 0 To dgvReceipt.Rows.Count - 1
                Dim dualRow = New String() {dgvReceipt.Rows(i).Cells("serno").Value, dgvReceipt.Rows(i).Cells("description").Value, dgvReceipt.Rows(i).Cells("quantity").Value, dgvReceipt.Rows(i).Cells("unitprice").Value, dgvReceipt.Rows(i).Cells("amount").Value, dgvReceipt.Rows(i).Cells("vat").Value}
                frmDual.dgvReceiptDual.Rows.Add(dualRow)
            Next
            frmDual.dgvReceiptDual.FirstDisplayedScrollingRowIndex = frmDual.dgvReceiptDual.Rows.Count - 1
        End If
        frmDual.txtBoxTotalDual.Text = txtBoxTotalAmt.Text
        frmDual.txtBoxDualDisc.Text = txtBoxDiscount.Text
        frmDual.txtBoxDualFinal.Text = txtBoxTotalWithDiscount.Text
    End Sub

    Private Sub dgvReceipt_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvReceipt.CellClick
        Dim index As Integer
        Try
            index = dgvReceipt.SelectedRows.Item(0).Index
        Catch ex As Exception
            index = -1
            Exit Sub
        End Try

        If Not componentsEnabled Then
            Exit Sub
        End If

        If MessageBox.Show(DELETE_SELECTED_LINE, DELETE_LINE, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            dgvReceipt.AllowUserToDeleteRows = True

            Dim tmpDesc As String
            Dim tmpQuantity As String
            Dim tmpIsKronos As String
            Dim tmpProductSerno As String

            tmpDesc = dgvReceipt.Rows(index).Cells("description").Value
            tmpQuantity = dgvReceipt.Rows(index).Cells("quantity").Value
            tmpIsKronos = "0" 'dgvReceipt.Rows(index).Cells("isKronos").Value
            tmpProductSerno = dgvReceipt.Rows(index).Cells("productSerno").Value

            updateProductsAndQuantity(tmpDesc, CInt(tmpQuantity))
            'If tmpIsKronos.Equals("1") Then
            'updateKronosProductsAndQuantity(dgvReceipt.Rows(index).Cells("productSerno").Value, dgvReceipt.Rows(index).Cells("itemCode").Value, dgvReceipt.Rows(index).Cells("issueNumber").Value, dgvReceipt.Rows(index).Cells("deliveryDate").Value)
            'End If

            Try
                recalculateDiscountForOffers(tmpProductSerno, tmpQuantity)
            Catch ex As Exception
                createExceptionFile(ex.Message, " File frmPOS.vb, Line 678 - Method dgvReceipt_CellClick")
            End Try

            createDeleteLineFile("User ID: " & whois & ", Description: " & tmpDesc & ", Quantity: " & tmpQuantity & ", Amount: " & dgvReceipt.Rows(index).Cells("amount").Value)

            dgvReceipt.Rows.RemoveAt(index)
            dgvReceipt.Refresh()

            recalculateAmounts()
            txtBoxBarcode.Focus()
            formatDataGrid()
        End If
    End Sub

    Private Sub recalculateDiscountForOffers(ByVal productSerno As String, ByVal quantity As String)
        Dim tmpOfferDiscAtItem As OfferTypeDiscAt
        Dim index As Integer = -1

        For Each tmpOfferDiscAtItem In offerDiscAtItems
            If tmpOfferDiscAtItem.productSerno = productSerno Then
                index += 1
                tmpOfferDiscAtItem.currentQuantity = tmpOfferDiscAtItem.currentQuantity - txtBoxQuantity.Text
                If tmpOfferDiscAtItem.currentDiscount > 0 Then
                    totalDiscount -= tmpOfferDiscAtItem.currentDiscount

                    Dim tmp As Double = tmpOfferDiscAtItem.currentQuantity / tmpOfferDiscAtItem.discountAt
                    If Not (Math.Floor(CDbl(tmp)) = Math.Ceiling(CDbl(tmp))) Then
                        tmp = Math.Truncate(tmp)
                    End If
                    tmpOfferDiscAtItem.currentDiscount = tmpOfferDiscAtItem.discountAmt * tmp
                End If
            Else
                Exit Sub
            End If
        Next

        If offerDiscAtItems(index).currentQuantity > 1 Then
            offerDiscAtItems.RemoveAt(index)
            offerDiscAtItems.Add(tmpOfferDiscAtItem)
        Else
            offerDiscAtItems.RemoveAt(index)
        End If

        totalDiscount += tmpOfferDiscAtItem.currentDiscount
        txtBoxDiscount.Text = totalDiscount.ToString("N2")
        totalWithDiscount = CStr(totalAmt - totalDiscount)
        txtBoxTotalWithDiscount.Text = totalWithDiscount.ToString("N2")

        If txtBoxPaymentAmt.Text.Length > 0 And CDbl(txtBoxPaymentAmt.Text) > 0 Then
            Dim tmp As Double = CDbl(txtBoxTotalWithDiscount.Text)
            Dim tmpPayment As Double = CDbl(txtBoxPaymentAmt.Text)
            tmp = tmp - tmpPayment
            txtBoxReturnAmt.Text = tmp.ToString("N2")
        End If
    End Sub

    'Private Sub updateKronosProductsAndQuantity(ByVal barcode As String, ByVal itemCode As String, ByVal issueNumber As String, ByVal deliveryDate As String)
    '    Dim tmpKronosItem As KronosItem
    '    Dim index As Integer = 0
    '    Dim found As Boolean = False
    '    For Each tmpKronosItem In kronosItems
    '        If tmpKronosItem.barcode.Equals(barcode) And tmpKronosItem.itemCode.Equals(itemCode) And tmpKronosItem.issueNumber.Equals(issueNumber) And tmpKronosItem.deliveryDate.Equals(deliveryDate) Then
    '            found = True
    '            kronosItems.RemoveAt(index)
    '            Exit For
    '        End If
    '        index += 1
    '    Next
    'End Sub

    Private Sub updateProductsAndQuantity(ByVal productDesc As String, ByVal quantity As Integer)
        If Not isConnOpen() Then
            MessageBox.Show("Cannot connect to database, please try again", APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim sql As String = "select serno from products where description = '" & productDesc & "'"
        Dim cmd = New OracleCommand(sql, conn)
        Dim dr As OracleDataReader = cmd.ExecuteReader()

        Dim serno As Integer
        If dr.Read() Then
            serno = CInt(dr(0))
        End If
        dr.Close()
        cmd.Dispose()

        Dim strSerno = CStr(serno)

        If productsAndQuantity.ContainsKey(strSerno) Then
            Dim tmpQuantity As Integer = productsAndQuantity(strSerno)
            If tmpQuantity = quantity Then
                productsAndQuantity.Remove(strSerno)
            Else
                productsAndQuantity(strSerno) = CInt(productsAndQuantity(strSerno)) - quantity
            End If
        End If
    End Sub

    Private Sub recalculateAmounts()
        If dgvReceipt.Rows.Count > 0 Then
            totalItems = 1
            ' 04/08 - Changes to support VAT 3%
            ' Supported VAT types = 0, 3, 5, 19
            '
            totalAmt0 = 0
            totalAmt3 = 0
            totalAmt5 = 0
            totalAmt19 = 0
            totalAmt = 0
            totalWithDiscount = 0

            For i = 0 To dgvReceipt.Rows.Count - 1
                dgvReceipt.Rows(i).Cells("serno").Value = totalItems
                totalItems += 1
                totalAmt += dgvReceipt.Rows(i).Cells("amount").Value
                Dim tmpAmt As Double = 0
                tmpAmt = dgvReceipt.Rows(i).Cells("amount").Value
                If dgvReceipt.Rows(i).Cells("vat").Value = 0 Then
                    totalAmt0 += tmpAmt
                ElseIf dgvReceipt.Rows(i).Cells("vat").Value = 3 Then
                    totalAmt3 += tmpAmt
                ElseIf dgvReceipt.Rows(i).Cells("vat").Value = 5 Then
                    totalAmt5 += tmpAmt
                ElseIf dgvReceipt.Rows(i).Cells("vat").Value = 19 Then
                    totalAmt19 += tmpAmt
                End If
            Next
            totalWithDiscount = totalAmt - totalDiscount
            txtBoxTotalWithDiscount.Text = totalWithDiscount.ToString("N2")
            txtBoxTotalAmt.Text = totalAmt.ToString("N2")
        Else
            setAmounts()
            txtBoxBarcode.Focus()
            txtBoxManualAmt.Clear()
        End If
    End Sub

    Private Sub btn5percent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn5percent.Click
        If Not isLoggedIn(username) Then
            MessageBox.Show("Ο χρήστης δεν ειναι συνδεμένος", "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        If txtBoxManualAmt.Text = String.Empty Or txtBoxManualAmt.Text.Length = 0 Then
            MessageBox.Show("Δεν έχετε καταχωρήσει τιμή για ΔΙΑΦΟΡΑ 5%", "Καταχώρηση Τιμής", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        fillProductsAndQuantity("-308")
        addNewRow("-308", "ΔΙΑΦΟΡΑ 5%", 5, CDbl(txtBoxManualAmt.Text), 0, 0)
    End Sub

    Private Sub btn19percent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn19percent.Click
        If Not isLoggedIn(username) Then
            MessageBox.Show("Ο χρήστης δεν ειναι συνδεμένος", "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        If txtBoxManualAmt.Text = String.Empty Or txtBoxManualAmt.Text.Length = 0 Then
            MessageBox.Show("Δεν έχετε καταχωρήσει τιμή για ΔΙΑΦΟΡΑ 19%", "Καταχώρηση Τιμής", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        fillProductsAndQuantity("-309")
        addNewRow("-309", "ΔΙΑΦΟΡΑ 19%", 19, CDbl(txtBoxManualAmt.Text), 0, 0)
    End Sub

    Private Sub btnDiscount_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDiscount.Click
        Dim tmpDiscount As Double
        If txtBoxManualAmt.Text = String.Empty Or txtBoxManualAmt.Text.Length = 0 Then
            MessageBox.Show("Δεν έχετε καταχωρήσει τιμή για Έκπτωση", "Καταχώρηση Έκπτωσης", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        Else
            tmpDiscount = CDbl(txtBoxManualAmt.Text)
            If txtBoxDiscount.Text.Length > 0 Then
                totalDiscount = tmpDiscount + CDbl(txtBoxDiscount.Text)
            Else
                totalDiscount = CDbl(txtBoxManualAmt.Text)
            End If
        End If
        txtBoxDiscount.Text = totalDiscount.ToString("N2")

        totalWithDiscount = CStr(totalAmt - totalDiscount)
        txtBoxTotalWithDiscount.Text = totalWithDiscount.ToString("N2")

        If txtBoxPaymentAmt.Text.Length > 0 And CDbl(txtBoxPaymentAmt.Text) > 0 Then
            Dim tmp As Double = CDbl(txtBoxTotalWithDiscount.Text)
            Dim tmpPayment As Double = CDbl(txtBoxPaymentAmt.Text)
            tmp = tmp - tmpPayment
            txtBoxReturnAmt.Text = tmp.ToString("N2")
        End If

        txtBoxQuantity.Text = "1"
        txtBoxBarcode.Clear()
        txtBoxBarcode.Focus()
        txtBoxManualAmt.Clear()
        If dualMonitor Then
            setDualMonitorContents()
        End If
    End Sub

    Private Sub btnDiscountPercent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDiscountPercent.Click
        If txtBoxManualAmt.Text = String.Empty Or txtBoxManualAmt.Text.Length = 0 Then
            MessageBox.Show("Δεν έχετε καταχωρήσει τιμή για Ποσοστό Έκπτωσης", "Καταχώρηση Ποσοστού Έκπτωσης", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        Else
            Dim discountPercent As Double = CDbl(txtBoxManualAmt.Text)
            totalDiscount = (totalAmt * (discountPercent / 100))
        End If
        txtBoxDiscount.Text = totalDiscount.ToString("N2")
        txtBoxTotalWithDiscount.Text = (totalAmt - totalDiscount).ToString("N2")

        If txtBoxPaymentAmt.Text.Length > 0 And CDbl(txtBoxPaymentAmt.Text.Length) > 0 Then
            Dim tmp As Double = CDbl(txtBoxTotalWithDiscount.Text)
            Dim tmpPayment As Double = CDbl(txtBoxPaymentAmt.Text)
            tmp = tmp - tmpPayment
            txtBoxReturnAmt.Text = tmp.ToString("N2")
        End If

        txtBoxQuantity.Text = "1"
        txtBoxBarcode.Clear()
        txtBoxBarcode.Focus()
        txtBoxManualAmt.Clear()
    End Sub

    Private Sub btnPayment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPayment.Click
        If txtBoxManualAmt.Text = String.Empty Or txtBoxManualAmt.Text.Length = 0 Then
            MessageBox.Show("Δεν έχετε καταχωρήσει ποσό Πληρωμής", "Καταχώρηση Ποσού Πληρωμής", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        Else
            payment = CDbl(txtBoxManualAmt.Text)
            txtBoxPaymentAmt.Text = payment.ToString("N2")

            returnAmount = Math.Round(totalWithDiscount - payment, 2)
            txtBoxReturnAmt.Text = returnAmount.ToString("N2")
            txtBoxManualAmt.Clear()
        End If
    End Sub

    Private Sub btnVisa_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVisa.Click
        If dgvReceipt.Rows.Count = 0 Then
            MessageBox.Show("Δεν έχετε επιλέξει είδος αγοράς", "Επιλογή είδους αγοράς", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        paymentMethod = "V"
        generateReceipt()
    End Sub

    Private Sub btnCash_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCash.Click
        If dgvReceipt.Rows.Count = 0 Then
            MessageBox.Show("Δεν έχετε επιλέξει είδος αγοράς", "Επιλογή είδους αγοράς", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        paymentMethod = "C"
        generateReceipt()
    End Sub

    Private Sub clearScreen()
        dgvReceipt.Rows.Clear()
        setAmounts()
        txtBoxBarcode.Clear()
        txtBoxManualAmt.Clear()
        txtBoxQuantity.Text = "1"
        kronosItems.Clear()
        tmpkronosItems.Clear()
        offerXYItems.Clear()
        offerDiscAtItems.Clear()
        If dualMonitor Then
            Try
                If BackgroundWorker1.IsBusy Then
                    BackgroundWorker1.Dispose()
                End If
                BackgroundWorker1.RunWorkerAsync()
            Catch ex As Exception

            End Try

        End If
        txtBoxBarcode.Focus()
        Me.Focus()
    End Sub

    Private Sub countBackground_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Dim progresscount As Integer = 0
        Do
            progresscount += 50
            BackgroundWorker1.ReportProgress(progresscount)
            Threading.Thread.Sleep(5000)
            If progresscount >= 100 Then
                BackgroundWorker1.CancelAsync()
                frmDual.dgvReceiptDual.Rows.Clear()
                frmDual.txtBoxTotalDual.Text = "0.00"
                frmDual.txtBoxDualDisc.Text = "0.00"
                frmDual.txtBoxDualFinal.Text = "0.00"
            End If
            If BackgroundWorker1.CancellationPending = True Then Exit Do
        Loop
    End Sub

    Private Sub countBackground_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        frmDual.dgvReceiptDual.Rows.Clear()
        frmDual.txtBoxTotalDual.Text = "0.00"
        frmDual.txtBoxDualDisc.Text = "0.00"
        frmDual.txtBoxDualFinal.Text = "0.00"
        setDualMonitorContents()
    End Sub

    Private Sub generateReceipt()
        If Not isConnOpen() Then
            MessageBox.Show("Cannot connect to database, please try again", APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim description As String = ""
        Dim cmd = New OracleCommand("", conn)
        Dim dr As OracleDataReader
        receiptSerno = -1

        Dim sql As String = ""
        Try
            sql = "select receiptsSeq.nextVal from dual"
            cmd = New OracleCommand(sql, conn)
            dr = cmd.ExecuteReader()

            If dr.Read() Then
                receiptSerno = CInt(dr(0))
            End If
            dr.Close()

            If totalDiscount > 0 Then
                If totalAmt19 >= totalDiscount Then
                    totalAmt19 -= totalDiscount
                Else
                    Dim diff As Double = totalDiscount - totalAmt19
                    totalAmt19 -= (totalDiscount - diff)

                    If totalAmt5 > 0 Then
                        totalAmt5 -= diff
                    End If

                    If totalAmt3 > 0 Then
                        totalAmt3 -= diff
                    End If
                End If
            End If

            sql = "insert into receipts (serno, payment_type, total_discount, total_vat19, total_vat5, total_vat0, return_amt, total_amt_with_disc, " & _
                  "                      total_amt, payment_amt, created_on, total_vat3, created_by) " & _
                  "values               (" & receiptSerno & "," & _
                  "                     '" & paymentMethod & "'," & _
                  "                      " & totalDiscount & "," & _
                  "                      " & totalAmt19 & "," & _
                  "                      " & totalAmt5 & "," & _
                  "                      " & totalAmt0 & "," & _
                  "                      " & returnAmount & "," & _
                  "                      " & totalWithDiscount & ", " & _
                  "                      " & totalAmt & ", " & _
                  "                      " & payment & ", " & _
                  "                  (select systimestamp from dual), " & _
                  "                      " & totalAmt3 & "," & _
                  "                     '" & whois & "' ) "
            cmd = New OracleCommand(sql, conn)
            cmd.ExecuteReader()

            Dim productsNotUpdateQuantity As New ArrayList()
            For i As Integer = -313 To -300
                productsNotUpdateQuantity.Add(i.ToString)
            Next

            For i = 0 To dgvReceipt.Rows.Count - 1
                Dim tmpAmount As Double = dgvReceipt.Rows(i).Cells("amount").Value
                Dim tmpQuantity As Integer = dgvReceipt.Rows(i).Cells("quantity").Value
                Dim tmpSerno As String = ""
                Dim tmpVat As String = dgvReceipt.Rows(i).Cells("vat").Value
                tmpSerno = dgvReceipt.Rows(i).Cells("productSerno").Value

                Dim tmpIsBox As Integer = CInt(dgvReceipt.Rows(i).Cells("isbox").Value)
                Dim tmpBoxQnt As Integer = CInt(dgvReceipt.Rows(i).Cells("box_qnt").Value)

                If tmpAmount < 0 Then
                    tmpQuantity *= -1
                End If

                Dim currentAmt As Double = dgvReceipt.Rows(i).Cells("amount").Value

                sql = "insert into receipts_det (receipt_serno, product_serno, quantity, amount, vat, created_on) " & _
                      "values                   (" & receiptSerno & "," & _
                      "                          " & tmpSerno & "," & _
                      "                          " & tmpQuantity & ", " & _
                      "                          " & currentAmt & ", " & _
                      "                          " & tmpVat & ", (select systimestamp from dual))"

                cmd = New OracleCommand(sql, conn)
                cmd.ExecuteReader()

                If Not productsNotUpdateQuantity.Contains(tmpSerno) Then

                    Dim isBox As Boolean = False

                    If tmpIsBox.Equals(0) Then

                        'Double check if product is a box to avoid wrong quantity updates
                        sql = "select nvl(isbox,0) from products where serno = " & CInt(tmpSerno) & ""
                        cmd = New OracleCommand(sql, conn)
                        dr = cmd.ExecuteReader()

                        If dr.Read Then
                            Dim tmp As Integer = CInt(dr(0))
                            If tmp > 0 Then
                                isBox = True
                            End If
                        End If
                        dr.Close()

                        If Not isBox Then
                            sql = "update products set avail_quantity = (avail_quantity"
                            If tmpAmount >= 0 Then
                                sql += " - " & tmpQuantity & ")"
                            Else
                                sql += " + " & (tmpQuantity * -1) & ")"
                            End If
                            sql += ", lastmodifiedscreen = 0 where serno = " & CInt(tmpSerno) & " "
                            cmd = New OracleCommand(sql, conn)
                            cmd.ExecuteReader()
                        End If
                    End If

                    If tmpIsBox.Equals(1) Or isBox Then
                        '
                        Dim logMsg As String = ""
                        Dim currentqnt As Integer = 0
                        Dim q As String = "select avail_quantity from products where serno in (" & _
                                                "select product_serno from barcodes where UPPER(barcode) in " & _
                                                "(select UPPER(barcode) from boxbarcodes where product_serno = " & CInt(tmpSerno) & ")" & _
                                                ")"

                        cmd = New OracleCommand(q, conn)
                        dr = cmd.ExecuteReader()

                        If dr.Read Then
                            currentqnt = CInt(dr(0))
                            logMsg = Date.Now + " Current:" + currentqnt.ToString

                        End If
                        dr.Close()
                        '

                        sql = "update products set avail_quantity = (avail_quantity"
                        If tmpAmount >= 0 Then
                            sql += " - " & (tmpBoxQnt * tmpQuantity) & ")"
                        Else
                            sql += " + " & (tmpBoxQnt * tmpQuantity) & ")"
                        End If
                        sql += ", lastmodifiedscreen = 0  " & _
                               "where serno in (" & _
                                                "select product_serno from barcodes where UPPER(barcode) in " & _
                                                "(select UPPER(barcode) from boxbarcodes where product_serno = " & CInt(tmpSerno) & ")" & _
                                                ")"

                        cmd = New OracleCommand(sql, conn)
                        cmd.ExecuteReader()
                        If tmpAmount < 0 Then
                            tmpBoxQnt *= -1
                        End If
                        logMsg += ", PrSerno:" + tmpSerno + ", Amt:" + tmpAmount.ToString + ", BoxQnt:" + tmpBoxQnt.ToString + ", NewQnt:" + (currentqnt - tmpBoxQnt).ToString

                        q = "insert into isbox_log (logmsg) values ('" & logMsg & "')"
                        cmd = New OracleCommand(q, conn)
                        cmd.ExecuteReader()
                    End If
                End If
            Next

            printType = "R"
            PrintDocument1.PrinterSettings.Copies = 1
            PrintDocument1.Print()

            If btnHold.Text = RETRIEVE Then
                btnExit.Enabled = True
                txtBoxTotalWithDiscount.Text = tmpTrxn.totalWithDiscount
                txtBoxTotalAmt.Text = tmpTrxn.totalAmt
                txtBoxDiscount.Text = tmpTrxn.discount
                txtBoxPaymentAmt.Text = tmpTrxn.paymentAmt
                txtBoxReturnAmt.Text = tmpTrxn.returnAmt
                totalAmt19 = tmpTrxn.totalAmt19
                totalAmt5 = tmpTrxn.totalAmt5
                totalAmt0 = tmpTrxn.totalAmt0
                totalAmt3 = tmpTrxn.totalAmt3
                totalItems = tmpTrxn.totalItems
                totalAmt = tmpTrxn.dTotalAmt
                totalWithDiscount = tmpTrxn.dTotalWithDiscount
                offerXYItems = tmpTrxn.offerXYItems
                offerDiscAtItems = tmpTrxn.offerDiscAtItems
                productsAndQuantity = tmpTrxn.productsAndQuantity

                dgvReceipt.Rows.Clear()

                Try
                    For i As Integer = 0 To tmpTrxn.dgvReceipt.Rows.Count - 1
                        If tmpTrxn.dgvReceipt.Rows(i).Cells("description").Value() <> String.Empty Then
                            dgvReceipt.Rows.Add(tmpTrxn.dgvReceipt.Rows(i).Cells("productSerno").Value(), _
                                        tmpTrxn.dgvReceipt.Rows(i).Cells("serno").Value(), _
                                        tmpTrxn.dgvReceipt.Rows(i).Cells("description").Value(), _
                                        tmpTrxn.dgvReceipt.Rows(i).Cells("quantity").Value(), _
                                        tmpTrxn.dgvReceipt.Rows(i).Cells("unitprice").Value(), _
                                        tmpTrxn.dgvReceipt.Rows(i).Cells("amount").Value(), _
                                        tmpTrxn.dgvReceipt.Rows(i).Cells("vat").Value(), _
                                        tmpTrxn.dgvReceipt.Rows(i).Cells("isKronos").Value(), _
                                        tmpTrxn.dgvReceipt.Rows(i).Cells("itemCode").Value(), _
                                        tmpTrxn.dgvReceipt.Rows(i).Cells("issueNumber").Value(), _
                                        tmpTrxn.dgvReceipt.Rows(i).Cells("deliveryDate").Value() _
                                        )
                        End If
                    Next
                Catch ex As Exception
                    createExceptionFile(ex.Message, " ")
                    MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try

                tmpTrxn = New tmpTransaction
                btnHold.Text = HOLD
                btnHold.BackColor = Color.LightGray
                txtBoxBarcode.Focus()
            Else
                clearScreen()
            End If
        Catch ex As Exception
            createExceptionFile(ex.Message, " " & sql)
            MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            cmd.Dispose()
            getMinBarcodeLength()
        End Try
    End Sub

    Private Function amountFormat(ByVal amount As String) As String
        Dim length As Integer = amount.Length
        Dim spacesToAdd As Integer = 6 - length
        Dim tmp As String = " "
        For i As Integer = 0 To spacesToAdd - 1
            tmp += " "
        Next
        amount = tmp & amount
        Return amount
    End Function

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

        If printType = "R" Then
            If paymentMethod = "C" Then
                paymentMethod = "Cash"
            Else
                paymentMethod = "Visa"
            End If

            e.Graphics.DrawString("Payment Method: " & paymentMethod, reportFont, Brushes.Black, 0, 125)
            'e.Graphics.DrawString("Invoice Number: " & paymentMethod, reportFont, Brushes.Black, 100, 125)
            e.Graphics.DrawString("Cashier: " & getUser(whois), reportFont, Brushes.Black, 0, 140)
            e.Graphics.DrawString(SINGE_DASHED_LINE, reportFont, Brushes.Black, 0, 150)

            Dim tmpDescription As String
            Dim tmpQuantity As String
            Dim tmpUnitPrice As String
            Dim tmpAmount As String
            Dim tmpVAT As String

            Dim xMargin As Integer = 170
            For i = 0 To dgvReceipt.Rows.Count - 1
                tmpDescription = dgvReceipt.Rows(i).Cells("description").Value
                tmpQuantity = dgvReceipt.Rows(i).Cells("quantity").Value
                tmpUnitPrice = dgvReceipt.Rows(i).Cells("unitprice").Value
                tmpAmount = dgvReceipt.Rows(i).Cells("amount").Value
                tmpQuantity = dgvReceipt.Rows(i).Cells("quantity").Value
                tmpVAT = dgvReceipt.Rows(i).Cells("vat").Value

                e.Graphics.DrawString(tmpDescription, reportFont, Brushes.Black, 0, xMargin)
                xMargin += 12

                If tmpVAT = "5" Then
                    tmpVAT = " 5"
                ElseIf tmpVAT = "0" Then
                    tmpVAT = " 0"
                ElseIf tmpVAT = "3" Then
                    tmpVAT = " 3"
                End If

                tmpUnitPrice = amountFormat(tmpUnitPrice)
                tmpAmount = amountFormat(tmpAmount)

                e.Graphics.DrawString(" " & " " & " " & " " & " " & " " & " " & " " & tmpUnitPrice & " x " & tmpQuantity & " " & " ( " & tmpVAT & "% VAT )" & vbTab & " " & " " & " " & " " & " " & " " & tmpAmount & vbCrLf, reportFontSmall, Brushes.Black, 0, xMargin)
                xMargin += 12
            Next
            e.Graphics.DrawString(SINGE_DASHED_LINE, reportFont, Brushes.Black, 0, xMargin)

            xMargin += 20
            Dim totalFont As Font = New Drawing.Font(REPORT_FONT, 13, FontStyle.Bold)

            e.Graphics.DrawString("Discount: ", reportFont, Brushes.Black, 0, xMargin)
            e.Graphics.DrawString(txtBoxDiscount.Text, reportFont, Brushes.Black, 242, xMargin)

            xMargin += 20
            e.Graphics.DrawString("TOTAL: ", totalFont, Brushes.Black, 0, xMargin)
            e.Graphics.DrawString(txtBoxTotalWithDiscount.Text, totalFont, Brushes.Black, 233, xMargin)

            xMargin += 17
            e.Graphics.DrawString("======", reportFont, Brushes.Black, 235, xMargin)

            xMargin += 20
            e.Graphics.DrawString("Cash(cash EURO): ", reportFont, Brushes.Black, 0, xMargin)
            e.Graphics.DrawString(txtBoxPaymentAmt.Text, reportFont, Brushes.Black, 245, xMargin)

            xMargin += 17
            e.Graphics.DrawString("Change Back    : ", reportFont, Brushes.Black, 0, xMargin)
            e.Graphics.DrawString(txtBoxReturnAmt.Text, reportFont, Brushes.Black, 242, xMargin)

            xMargin += 12
            e.Graphics.DrawString(SINGE_DASHED_LINE, reportFont, Brushes.Black, 0, xMargin)

            xMargin += 20
            e.Graphics.DrawString("VAT ANALYSIS", reportFont, Brushes.Black, 0, xMargin)

            xMargin += 12
            e.Graphics.DrawString(SINGE_DASHED_LINE, reportFont, Brushes.Black, 0, xMargin)

            xMargin += 15
            e.Graphics.DrawString("VAT 00%: " & totalAmt0.ToString("N2") & vbTab & vbTab & "0.00", reportFont, Brushes.Black, 0, xMargin)

            xMargin += 15
            e.Graphics.DrawString("VAT 03%: " & totalAmt3.ToString("N2") & vbTab & vbTab & "0.00", reportFont, Brushes.Black, 0, xMargin)

            xMargin += 15
            e.Graphics.DrawString("VAT 05%: " & totalAmt5.ToString("N2") & vbTab & vbTab & (totalAmt5 - (totalAmt5 / 1.05)).ToString("N2"), reportFont, Brushes.Black, 0, xMargin)
            xMargin += 12
            e.Graphics.DrawString("VAT 19%: " & totalAmt19.ToString("N2") & vbTab & vbTab & (totalAmt19 - (totalAmt19 / 1.19)).ToString("N2"), reportFont, Brushes.Black, 0, xMargin)
            xMargin += 12
            e.Graphics.DrawString(SINGE_DASHED_LINE, reportFont, Brushes.Black, 0, xMargin)

            xMargin += 30
            e.Graphics.DrawString("Thank you!", reportFont, Brushes.Black, 100, xMargin)
            xMargin += 15
            e.Graphics.DrawString("Please come again!", reportFont, Brushes.Black, 80, xMargin)

        ElseIf printType = "P" Then
            e.Graphics.DrawString(SINGE_DASHED_LINE, reportFont, Brushes.Black, 0, 130)
            e.Graphics.DrawString("Ποσό πληρωμής: " & txtBoxManualAmt.Text & " Ευρώ", reportFont, Brushes.Black, 0, 200)
            e.Graphics.DrawString("Χρήστης: " & getUser(whois), reportFont, Brushes.Black, 0, 220)
            e.Graphics.DrawString("Προμηθευτής:......................... ", reportFont, Brushes.Black, 0, 240)
            e.Graphics.DrawString(SINGE_DASHED_LINE, reportFont, Brushes.Black, 0, 400)

        ElseIf printType = "X" Then
            If Not isConnOpen() Then
                MessageBox.Show("Cannot connect to database, please try again", "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            e.Graphics.DrawString(SINGE_DASHED_LINE, reportFont, Brushes.Black, 0, 130)
            e.Graphics.DrawString("Χρήστης: " & getUser(whois), reportFont, Brushes.Black, 0, 160)

            Dim cmd As New OracleCommand("", conn)
            Dim dr As OracleDataReader
            Dim sql As String = ""
            Try
                sql = "select from_date, to_date, total_receipts, total5percent, total19percent, payments, " & _
                      "initial_amt, final_amt, description, total0percent, amount_laxeia, initialAmtLaxeia, amountvisa, total3percent  " & _
                      "from x_report " & _
                      "where user_id = '" & whois & "' and created_on = (select max(created_on) from x_report)"

                cmd = New OracleCommand(sql, conn)
                dr = cmd.ExecuteReader()

                Dim xMargin As Integer = 180
                If dr.Read Then
                    e.Graphics.DrawString("Από: " & CStr(dr(0)), reportFont, Brushes.Black, 0, xMargin)
                    xMargin += 20
                    e.Graphics.DrawString("Έως: " & CStr(dr(1)), reportFont, Brushes.Black, 0, xMargin)
                    Dim initial As Double = CDbl(dr(6))
                    Dim amountLaxeia As Double = CDbl(dr(10))
                    Dim initialAmountLaxeia As Double = CDbl(dr(11))

                    xMargin += 20
                    e.Graphics.DrawString("Αρχικό Ποσό: " & initial.ToString("N2"), reportFont, Brushes.Black, 0, xMargin)
                    xMargin += 20

                    amountLaxeia = getAmountLaxeia()
                    e.Graphics.DrawString("Ποσο λαχείων για Παράδωση: " & (amountLaxeia).ToString("N2"), reportFont, Brushes.Black, 0, xMargin)
                End If
                dr.Close()
            Catch ex As Exception
                createExceptionFile(ex.Message, " " & sql)
                MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                cmd.Dispose()
            End Try
        End If
    End Sub

    Private Sub chkBoxReturnProduct_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkBoxReturnProduct.CheckedChanged
        If chkBoxReturnProduct.Checked Then
            returnProduct = -1
        Else
            returnProduct = 1
        End If
        txtBoxBarcode.Focus()
    End Sub

    Private Sub btnClearBarcode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearBarcode.Click
        txtBoxBarcode.Clear()
        txtBoxBarcode.Focus()
    End Sub

    Private Sub btnPayments_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPayments.Click
        If Not isConnOpen() Then
            MessageBox.Show("Cannot connect to database, please try again", APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If Not isLoggedIn(username) Then
            MessageBox.Show("Ο χρήστης δεν ειναι συνδεμένος", "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If txtBoxManualAmt.Text = String.Empty Or txtBoxManualAmt.Text.Length = 0 Then
            MessageBox.Show("Δεν έχετε καταχωρήσει ποσό για έκδωση ΠΛΗΡΩΜΗΣ", "Καταχώρηση Τιμής", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If MessageBox.Show("Έκδωση Πληρωμής " & txtBoxManualAmt.Text & " ευρώ;", "Πληρωμή", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

            frmPaymentInfo.ShowDialog()

            If tmpPaymentVAT = -1 Then
                MessageBox.Show("Δεν έχετε επιλέξει είδος Φ.Π.Α", "Επιλογή Φ.Π.Α", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            Dim sql As String = ""
            Dim cmd = New OracleCommand("", conn)
            Dim vat As String = 0
            Dim amountVAT As Double = 0

            If tmpPaymentVAT = 5 Then
                vat = 5
                amountVAT = CDbl(txtBoxManualAmt.Text) - (CDbl(txtBoxManualAmt.Text) / 1.05)
            ElseIf tmpPaymentVAT = 19 Then
                vat = 19
                amountVAT = CDbl(txtBoxManualAmt.Text) - (CDbl(txtBoxManualAmt.Text) / 1.19)
            End If

            Try
                sql = "insert into payments (created_by, created_on, amount,vat, amountVAT) " & _
                      "values('" & whois & "', (select systimestamp from dual), " & txtBoxManualAmt.Text & ", '" & vat & "', " & amountVAT & ")"
                cmd = New OracleCommand(sql, conn)
                cmd.ExecuteReader()

                If MessageBox.Show("Εκτύπωση Πληρωμής;", "Εκτύπωση Πληρωμής", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    printType = "P"
                    PrintDocument1.PrinterSettings.Copies = 1
                    PrintDocument1.Print()
                End If

                txtBoxManualAmt.Clear()
                setAmounts()
                txtBoxBarcode.Focus()

            Catch ex As Exception
                createExceptionFile(ex.Message, " " & sql)
                MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                cmd.Dispose()
                tmpPaymentVAT = -1
            End Try
        End If
    End Sub

    Private Sub btnCancelDiscount_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelDiscount.Click
        txtBoxDiscount.Text = "0.00"
        totalDiscount = 0
        recalculateAmounts()
    End Sub

    Private Sub btnCancelPayment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelPayment.Click
        txtBoxPaymentAmt.Text = "0.00"
        txtBoxReturnAmt.Text = "0.00"
        payment = 0
        returnAmount = 0
        recalculateAmounts()
    End Sub

    Private Sub btnKronos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKronos.Click
        If MessageBox.Show("Εκτύπωση Αναφοράς ΚΡΟΝΟΥ;", "ΚΡΟΝΟΣ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            printType = "K"
            PrintDocument1.PrinterSettings.Copies = 1
            PrintDocument1.Print()
        End If
    End Sub

    'Private Sub btnKronosSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKronosSearch.Click
    '    If Not isConnOpen() Then
    '        MessageBox.Show(CANNOT_ACCESS_DB, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        Exit Sub
    '    End If

    '    Dim productFound As Boolean = False

    '    Dim cmd As New OracleCommand("", conn)
    '    Dim sql As String = ""
    '    Try
    '        Dim dr As OracleDataReader
    '        sql = "select count(*) from kronos_products where trim(barcode) like  '" & txtBoxBarcode.Text & "%' and is_deleted =0"

    '        cmd = New OracleCommand(sql, conn)
    '        cmd.CommandType = CommandType.Text

    '        dr = cmd.ExecuteReader()

    '        Dim totalFound As Integer = 0
    '        If dr.Read() Then
    '            totalFound = CInt(dr(0))
    '            If totalFound >= 1 Then

    '                productFound = True

    '                sql = "select trim(item_name), trim(extended_item_name), trim(issue_number), trim(delivery_date), trim(price), trim(vat), trim(item_code), " & _
    '                      "trim(price_1), trim(price_2) " & _
    '                      "from kronos_products " & _
    '                      "where trim(barcode) like  '" & txtBoxBarcode.Text & "%' and is_deleted =0"

    '                cmd = New OracleCommand(sql, conn)
    '                cmd.CommandType = CommandType.Text
    '                dr = cmd.ExecuteReader()
    '                dr.Read()
    '                If totalFound = 1 Then
    '                    Dim objKronosItem As New KronosItem
    '                    objKronosItem.barcode = txtBoxBarcode.Text
    '                    objKronosItem.itemCode = CStr(dr(6))
    '                    objKronosItem.issueNumber = CStr(dr(2))
    '                    objKronosItem.deliveryDate = CStr(dr(3))
    '                    objKronosItem.itemName = CStr(dr(0))
    '                    objKronosItem.vat = CStr(dr(5))
    '                    If Not IsDBNull(dr(1)) Then
    '                        objKronosItem.itemExtendedName = CStr(dr(1))
    '                    End If
    '                    objKronosItem.price = CDbl(dr(4))
    '                    objKronosItem.price1 = CDbl(dr(7))
    '                    objKronosItem.price2 = CDbl(dr(8))

    '                    kronosItems.Add(objKronosItem)

    '                    totalItems += 1
    '                    productFound = True
    '                    Dim sellAmt As Double = CDbl(dr(4))
    '                    Dim currentAmt As Double = sellAmt * CInt(txtBoxQuantity.Text)
    '                    currentAmt *= returnProduct

    '                    Dim vatValue As String = dr(5)
    '                    If vatValue.Equals("05") Then
    '                        vatValue = "5"
    '                    ElseIf vatValue.Equals("00") Then
    '                        vatValue = "0"
    '                    ElseIf vatValue.Equals("M") Or vatValue.Equals("Μ") Then
    '                        vatValue = "M"
    '                    End If

    '                    Dim row As String()
    '                    Dim price1 As New Double
    '                    Dim price2 As New Double
    '                    If vatValue = "M" Then
    '                        price1 = CDbl(dr(7)) * CInt(txtBoxQuantity.Text)
    '                        row = New String() {txtBoxBarcode.Text, totalItems, dr(0), txtBoxQuantity.Text, price1.ToString("N2"), price1.ToString("N2"), "5", "1", objKronosItem.itemCode, objKronosItem.issueNumber, objKronosItem.deliveryDate}
    '                        dgvReceipt.Rows.Add(row)

    '                        totalItems += 1
    '                        price2 = CDbl(dr(8)) * CInt(txtBoxQuantity.Text)
    '                        row = New String() {txtBoxBarcode.Text, totalItems, dr(0), txtBoxQuantity.Text, price2.ToString("N2"), price2.ToString("N2"), "19", "1", objKronosItem.itemCode, objKronosItem.issueNumber, objKronosItem.deliveryDate}

    '                        dgvReceipt.Rows.Add(row)
    '                    Else
    '                        row = New String() {txtBoxBarcode.Text, totalItems, dr(0), txtBoxQuantity.Text, sellAmt.ToString("N2"), currentAmt.ToString("N2"), vatValue, "1", objKronosItem.itemCode, objKronosItem.issueNumber, objKronosItem.deliveryDate}
    '                        dgvReceipt.Rows.Add(row)
    '                    End If

    '                    totalAmt += Math.Round(currentAmt, 2)
    '                    totalWithDiscount += Math.Round(currentAmt, 2)
    '                    txtBoxTotalAmt.Text = totalAmt.ToString("N2")

    '                    Dim tmpVat As Double

    '                    If vatValue = "M" Then
    '                        tmpVat = price1 * returnProduct * CInt(txtBoxQuantity.Text)
    '                        totalAmt5 += tmpVat

    '                        tmpVat = price2 * returnProduct * CInt(txtBoxQuantity.Text)
    '                        totalAmt19 += tmpVat
    '                    Else
    '                        tmpVat = (CDbl(dr(4)) * returnProduct * CInt(txtBoxQuantity.Text))
    '                        If CInt(dr(5)) = 19 Then
    '                            totalAmt19 += tmpVat
    '                        ElseIf CInt(dr(5)) = 5 Then
    '                            totalAmt5 += tmpVat
    '                        ElseIf CInt(dr(5)) = 0 Then
    '                            totalAmt0 += tmpVat
    '                        End If
    '                    End If
    '                Else
    '                    frmSelectKronos.dgvKronosProducts.Rows.Clear()
    '                    tmpkronosItems.Clear()

    '                    Dim objKronosItem As New KronosItem
    '                    objKronosItem.barcode = txtBoxBarcode.Text
    '                    objKronosItem.itemCode = CStr(dr(6))
    '                    objKronosItem.issueNumber = CStr(dr(2))
    '                    objKronosItem.deliveryDate = CStr(dr(3))
    '                    objKronosItem.itemName = CStr(dr(0))
    '                    If Not IsDBNull(dr(1)) Then
    '                        objKronosItem.itemExtendedName = CStr(dr(1))
    '                    End If
    '                    objKronosItem.price = CDbl(dr(4))
    '                    objKronosItem.vat = CStr(dr(5))
    '                    objKronosItem.price1 = CDbl(dr(7))
    '                    objKronosItem.price2 = CDbl(dr(8))

    '                    tmpkronosItems.Add(objKronosItem)

    '                    frmSelectKronos.dgvKronosProducts.Rows.Add(objKronosItem.itemName, objKronosItem.itemExtendedName, objKronosItem.issueNumber, objKronosItem.deliveryDate, objKronosItem.price)

    '                    While dr.Read
    '                        objKronosItem = New KronosItem
    '                        objKronosItem.barcode = txtBoxBarcode.Text
    '                        objKronosItem.itemCode = CStr(dr(6))
    '                        objKronosItem.issueNumber = CStr(dr(2))
    '                        objKronosItem.deliveryDate = CStr(dr(3))
    '                        objKronosItem.itemName = CStr(dr(0))
    '                        If Not IsDBNull(dr(1)) Then
    '                            objKronosItem.itemExtendedName = CStr(dr(1))
    '                        End If
    '                        objKronosItem.price = CDbl(dr(4))
    '                        objKronosItem.vat = CStr(dr(5))
    '                        objKronosItem.price1 = CDbl(dr(7))
    '                        objKronosItem.price2 = CDbl(dr(8))

    '                        tmpkronosItems.Add(objKronosItem)

    '                        frmSelectKronos.dgvKronosProducts.Rows.Add(objKronosItem.itemName, objKronosItem.itemExtendedName, objKronosItem.issueNumber, objKronosItem.deliveryDate, objKronosItem.price)
    '                    End While
    '                    dr.Close()
    '                    frmSelectKronos.ShowDialog()

    '                    If tmpKronosSelectedIndex <> -1 Then
    '                        kronosItems.Add(tmpkronosItems.Item(tmpKronosSelectedIndex))
    '                        totalItems += 1
    '                        productFound = True

    '                        Dim tmpKronosItem As KronosItem = tmpkronosItems.Item(tmpKronosSelectedIndex)
    '                        Dim tmpDescription As String = tmpKronosItem.itemName
    '                        Dim sellAmt As Double = CDbl(tmpKronosItem.price)
    '                        Dim currentAmt As Double = sellAmt * CInt(txtBoxQuantity.Text)
    '                        currentAmt *= returnProduct

    '                        Dim vatValue As String = tmpKronosItem.vat
    '                        If vatValue.Equals("05") Then
    '                            vatValue = "5"
    '                        ElseIf vatValue.Equals("00") Then
    '                            vatValue = "0"
    '                        ElseIf vatValue.Equals("M") Or vatValue.Equals("Μ") Then
    '                            vatValue = "M"
    '                        End If

    '                        Dim row As String()
    '                        Dim price1 As New Double
    '                        Dim price2 As New Double
    '                        If vatValue = "M" Then
    '                            price1 = CDbl(tmpKronosItem.price1) * CInt(txtBoxQuantity.Text)
    '                            row = New String() {txtBoxBarcode.Text, totalItems, tmpKronosItem.itemName, txtBoxQuantity.Text, CDbl(tmpKronosItem.price1).ToString("N2"), price1.ToString("N2"), "5", "1", tmpKronosItem.itemCode, tmpKronosItem.issueNumber, tmpKronosItem.deliveryDate}
    '                            dgvReceipt.Rows.Add(row)

    '                            totalItems += 1
    '                            price2 = CDbl(tmpKronosItem.price2) * CInt(txtBoxQuantity.Text)
    '                            row = New String() {txtBoxBarcode.Text, totalItems, tmpKronosItem.itemName, txtBoxQuantity.Text, CDbl(tmpKronosItem.price2).ToString("N2"), price2.ToString("N2"), "19", "1", tmpKronosItem.itemCode, tmpKronosItem.issueNumber, tmpKronosItem.deliveryDate}

    '                            dgvReceipt.Rows.Add(row)
    '                        Else
    '                            row = New String() {txtBoxBarcode.Text, totalItems, tmpKronosItem.itemName, txtBoxQuantity.Text, sellAmt.ToString("N2"), currentAmt.ToString("N2"), vatValue, "1", tmpKronosItem.itemCode, tmpKronosItem.issueNumber, tmpKronosItem.deliveryDate}
    '                            dgvReceipt.Rows.Add(row)
    '                        End If

    '                        totalAmt += Math.Round(currentAmt, 2)
    '                        totalWithDiscount += Math.Round(currentAmt, 2)
    '                        txtBoxTotalAmt.Text = totalAmt.ToString("N2")

    '                        Dim tmpVat As Double

    '                        If vatValue = "M" Then
    '                            tmpVat = price1 * returnProduct * CInt(txtBoxQuantity.Text)
    '                            totalAmt5 += tmpVat

    '                            tmpVat = price2 * returnProduct * CInt(txtBoxQuantity.Text)
    '                            totalAmt19 += tmpVat
    '                        Else
    '                            tmpVat = (sellAmt * returnProduct * CInt(txtBoxQuantity.Text))
    '                            If CInt(tmpKronosItem.vat) = 19 Then
    '                                totalAmt19 += tmpVat
    '                            ElseIf CInt(tmpKronosItem.vat) = 5 Then
    '                                totalAmt5 += tmpVat
    '                            ElseIf CInt(tmpKronosItem.vat) = 0 Then
    '                                totalAmt0 += tmpVat
    '                            End If
    '                        End If
    '                        tmpkronosItems.Clear()
    '                    End If
    '                End If
    '            End If
    '        End If

    '        If productFound Then
    '            txtBoxTotalWithDiscount.Text = totalWithDiscount.ToString("N2")
    '            txtBoxQuantity.Text = "1"
    '            txtBoxBarcode.Clear()
    '            txtBoxBarcode.Focus()
    '            chkBoxReturnProduct.Checked = False
    '        End If
    '    Catch ex As Exception
    '        createExceptionFile(ex.Message, " " & sql)
    '        MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    Finally
    '        cmd.Dispose()
    '        formatDataGrid()
    '    End Try
    'End Sub

    Private Sub btnHold_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHold.Click
        If dgvReceipt.Rows.Count = 0 And btnHold.Text = HOLD Then
            MessageBox.Show("Δεν υπάρχει κάτι για κράτημα", "Κράτημα Συναλλαγής", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If dgvReceipt.Rows.Count > 0 And btnHold.Text = RETRIEVE Then
            MessageBox.Show("Δεν μπορεί να γίνει επαναφορά όταν υπάρχει ενεργή συναλλαγή", "Επαναφορά Συναλλαγής", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If btnHold.Text = HOLD Then
            btnExit.Enabled = False
            tmpTrxn.totalWithDiscount = txtBoxTotalWithDiscount.Text
            tmpTrxn.totalAmt = txtBoxTotalAmt.Text
            tmpTrxn.discount = txtBoxDiscount.Text
            tmpTrxn.paymentAmt = txtBoxPaymentAmt.Text
            tmpTrxn.returnAmt = txtBoxReturnAmt.Text
            tmpTrxn.totalAmt19 = totalAmt19
            tmpTrxn.totalAmt5 = totalAmt5
            tmpTrxn.totalAmt0 = totalAmt0
            tmpTrxn.totalItems = totalItems
            tmpTrxn.dTotalAmt = totalAmt
            tmpTrxn.dTotalWithDiscount = totalWithDiscount
            tmpTrxn.offerXYItems = offerXYItems
            Dim tmpOffersArrayList As New ArrayList()
            For Each tmp In offerDiscAtItems
                tmpOffersArrayList.Add(tmp)                
            Next
            tmpTrxn.offerDiscAtItems = tmpOffersArrayList
            tmpTrxn.productsAndQuantity = productsAndQuantity

            tmpTrxn.dgvReceipt = New DataGridView
            tmpTrxn.dgvReceipt.Columns.Add("productSerno", "productSerno")
            tmpTrxn.dgvReceipt.Columns.Add("serno", "serno")
            tmpTrxn.dgvReceipt.Columns.Add("description", "description")
            tmpTrxn.dgvReceipt.Columns.Add("quantity", "quantity")
            tmpTrxn.dgvReceipt.Columns.Add("unitprice", "unitprice")
            tmpTrxn.dgvReceipt.Columns.Add("amount", "amount")
            tmpTrxn.dgvReceipt.Columns.Add("vat", "vat")
            tmpTrxn.dgvReceipt.Columns.Add("isKronos", "isKronos")
            tmpTrxn.dgvReceipt.Columns.Add("itemCode", "itemCode")
            tmpTrxn.dgvReceipt.Columns.Add("issueNumber", "issueNumber")
            tmpTrxn.dgvReceipt.Columns.Add("deliveryDate", "deliveryDate")

            Try
                For i As Integer = 0 To dgvReceipt.Rows.Count - 1
                    tmpTrxn.dgvReceipt.Rows.Add(dgvReceipt.Rows(i).Cells("productSerno").Value(), _
                                                dgvReceipt.Rows(i).Cells("serno").Value(), _
                                                dgvReceipt.Rows(i).Cells("description").Value(), _
                                                dgvReceipt.Rows(i).Cells("quantity").Value(), _
                                                dgvReceipt.Rows(i).Cells("unitprice").Value(), _
                                                dgvReceipt.Rows(i).Cells("amount").Value(), _
                                                dgvReceipt.Rows(i).Cells("vat").Value(), _
                                                dgvReceipt.Rows(i).Cells("isKronos").Value(), _
                                                dgvReceipt.Rows(i).Cells("itemCode").Value(), _
                                                dgvReceipt.Rows(i).Cells("issueNumber").Value(), _
                                                dgvReceipt.Rows(i).Cells("deliveryDate").Value() _
                                                )
                Next
            Catch ex As Exception
                createExceptionFile(ex.Message, " ")
                MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
            clearScreen()
            btnHold.Text = RETRIEVE
            btnHold.BackColor = Color.Red
        Else
            btnExit.Enabled = True
            txtBoxTotalWithDiscount.Text = tmpTrxn.totalWithDiscount
            txtBoxTotalAmt.Text = tmpTrxn.totalAmt
            txtBoxDiscount.Text = tmpTrxn.discount
            totalDiscount = CDbl(txtBoxDiscount.Text)
            txtBoxPaymentAmt.Text = tmpTrxn.paymentAmt
            txtBoxReturnAmt.Text = tmpTrxn.returnAmt
            totalAmt19 = tmpTrxn.totalAmt19
            totalAmt5 = tmpTrxn.totalAmt5
            totalAmt0 = tmpTrxn.totalAmt0
            totalItems = tmpTrxn.totalItems
            totalAmt = tmpTrxn.dTotalAmt
            totalWithDiscount = tmpTrxn.dTotalWithDiscount
            'kronosItems = tmpTrxn.kronosItems
            offerXYItems = tmpTrxn.offerXYItems
            offerDiscAtItems = tmpTrxn.offerDiscAtItems
            productsAndQuantity = tmpTrxn.productsAndQuantity

            Try
                For i As Integer = 0 To tmpTrxn.dgvReceipt.Rows.Count - 1
                    If tmpTrxn.dgvReceipt.Rows(i).Cells("description").Value() <> String.Empty Then
                        dgvReceipt.Rows.Add(tmpTrxn.dgvReceipt.Rows(i).Cells("productSerno").Value(), _
                                    tmpTrxn.dgvReceipt.Rows(i).Cells("serno").Value(), _
                                    tmpTrxn.dgvReceipt.Rows(i).Cells("description").Value(), _
                                    tmpTrxn.dgvReceipt.Rows(i).Cells("quantity").Value(), _
                                    tmpTrxn.dgvReceipt.Rows(i).Cells("unitprice").Value(), _
                                    tmpTrxn.dgvReceipt.Rows(i).Cells("amount").Value(), _
                                    tmpTrxn.dgvReceipt.Rows(i).Cells("vat").Value(), _
                                    tmpTrxn.dgvReceipt.Rows(i).Cells("isKronos").Value(), _
                                    tmpTrxn.dgvReceipt.Rows(i).Cells("itemCode").Value(), _
                                    tmpTrxn.dgvReceipt.Rows(i).Cells("issueNumber").Value(), _
                                    tmpTrxn.dgvReceipt.Rows(i).Cells("deliveryDate").Value() _
                                    )
                    End If
                Next
            Catch ex As Exception
                createExceptionFile(ex.Message, " ")
                MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

            tmpTrxn = New tmpTransaction
            btnHold.Text = HOLD
            btnHold.BackColor = Color.LightGray
            txtBoxBarcode.Focus()
        End If
    End Sub

    Private Sub dgvReceipt_UserDeletingRow(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowCancelEventArgs) Handles dgvReceipt.UserDeletingRow
        formatDataGrid()
        setAmounts()
    End Sub

    Private Sub btnReceipts_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReceipts.Click
        frmReceiptsPOS.ShowDialog()
    End Sub

    Private Sub btnPosBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPos1.Click, btnPos2.Click, btnPos3.Click, _
                                        btnPos4.Click, btnPos5.Click, btnPos6.Click, btnPos7.Click, btnPos8.Click, btnPos9.Click, btnPos10.Click, _
                                        btnPos11.Click, btnPos12.Click, btnPos13.Click, btnPos14.Click, btnPos15.Click, btnPos16.Click, btnPos17.Click, _
                                        btnPos18.Click, btnPos19.Click, btnPos20.Click, btnPos21.Click, btnPos22.Click, btnPos23.Click
        If Not isLoggedIn(username) Then
            MessageBox.Show("Ο χρήστης δεν ειναι συνδεμένος", "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        currentBtnPosEdit = sender.name
        Dim tmpBtnItem As BtnItem = btnItemsMap.Item(sender.name)
        If tmpBtnItem.LinkedItems = 1 Then
            dynamicProductSerno = tmpBtnItem.LinkedItemsDetails.Item(0).ProductSerno
            dynamicProductDesc = tmpBtnItem.LinkedItemsDetails.Item(0).Description()
            dynamicProductVAT = tmpBtnItem.LinkedItemsDetails.Item(0).Vat
            dynamicProductPrice = tmpBtnItem.LinkedItemsDetails.Item(0).ProductPrice
            dynamicProductIsBox = tmpBtnItem.LinkedItemsDetails.Item(0).IsBox
            dynamicProductBoxQnt = tmpBtnItem.LinkedItemsDetails.Item(0).BoxQnt
            addNewRow(dynamicProductSerno, dynamicProductDesc, dynamicProductVAT, dynamicProductPrice, dynamicProductIsBox, dynamicProductBoxQnt)
            fillProductsAndQuantity(dynamicProductSerno)
            dynamicProductSerno = -1
            Exit Sub
        ElseIf tmpBtnItem.LinkedItems = 2 Then
            frmPosDynamic2Items.ShowDialog()
        ElseIf tmpBtnItem.LinkedItems = 3 Then
            frmPosDynamic3Items.ShowDialog()
        ElseIf tmpBtnItem.LinkedItems = 4 Then
            frmPosDynamic4Items.ShowDialog()
        ElseIf tmpBtnItem.LinkedItems = 5 Then
            frmPosDynamic5Items.ShowDialog()
        ElseIf tmpBtnItem.LinkedItems = 6 Then
            frmPosDynamic6Items.ShowDialog()
        ElseIf tmpBtnItem.LinkedItems = 7 Then
            frmPosDynamic7Items.ShowDialog()
        ElseIf tmpBtnItem.LinkedItems = 8 Then
            frmPosDynamic8Items.ShowDialog()
        ElseIf tmpBtnItem.LinkedItems = 9 Then
            frmPosDynamic9Items.ShowDialog()
        ElseIf tmpBtnItem.LinkedItems = 10 Then
            frmPosDynamic10Items.ShowDialog()
        End If
        If dynamicProductSerno <> -1 Then
            addNewRow(dynamicProductSerno, dynamicProductDesc, dynamicProductVAT, dynamicProductPrice, dynamicProductIsBox, dynamicProductBoxQnt)
            fillProductsAndQuantity(dynamicProductSerno)
            dynamicProductSerno = -1
        End If
    End Sub
End Class
