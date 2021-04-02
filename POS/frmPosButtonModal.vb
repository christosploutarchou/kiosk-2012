Imports Oracle.DataAccess.Client
Public Class frmPosButtonModal
    Dim tableIndex As String = ""

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Dispose()
    End Sub

    Private Sub frmNewProduct_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        Me.Dispose()
    End Sub

    Private Sub frmNewProduct_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim sql As String = ""
        Dim cmd As New OracleCommand(sql, conn)
        Dim dr As OracleDataReader

        If txtBoxButtonName.Text = String.Empty Then
            MessageBox.Show("Το πεδίο Όνομα κουμπιού δεν μπορεί να είναι κενό", "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If dgvLinkedProducts.Rows.Count = 0 Then
            MessageBox.Show("Δεν έχετε συνδέσει προιόν(τα) με το κουμπί", "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        For i = 0 To dgvLinkedProducts.Rows.Count - 1
            If CStr(dgvLinkedProducts.Rows(i).Cells("posDescription").Value).Length > 15 Then
                MessageBox.Show("Η περιγραφή στο POS (γραμμή" & CStr(i + 1) & ") δεν πρέπει να ξεπερνά τους 15 χαρακτήρες", "Περιγραφή στο POS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
        Next

        Dim isVisible As Integer = 0
        If chkBoxVisibleOnPOS.Checked Then
            isVisible = 1
        End If
        Try
            sql = "select count(*) from BTN_POS" + tableIndex
            cmd = New OracleCommand(sql, conn)
            cmd.CommandType = CommandType.Text
            dr = cmd.ExecuteReader()
            If dr.Read() Then
                If CInt(dr(0)) = 0 Then
                    'First setup, insert
                    sql = "Insert into BTN_POS" + tableIndex + " (DISP_NAME, IS_VISIBLE) " & _
                          "Values ('" & txtBoxButtonName.Text.Replace("'", "`") & "'," & isVisible & ")"
                Else
                    sql = "Update BTN_POS" + tableIndex + " Set " & _
                          "DISP_NAME = '" & txtBoxButtonName.Text.Replace("'", "`") & "', " & _
                          "IS_VISIBLE = " & isVisible & " "
                End If
                cmd = New OracleCommand(sql, conn)
                cmd.CommandType = CommandType.Text
                cmd.ExecuteReader()
            End If

            sql = "delete from BTN_POS" + tableIndex + "_DET"
            cmd = New OracleCommand(sql, conn)
            cmd.CommandType = CommandType.Text
            cmd.ExecuteReader()

            For i = 0 To dgvLinkedProducts.Rows.Count - 1
                sql = "insert into BTN_POS" + tableIndex + "_DET (product_serno, DISPLAY_DESC) " & _
                      "values (" & dgvLinkedProducts.Rows(i).Cells("productSerno").Value & ", " & _
                      "'" & dgvLinkedProducts.Rows(i).Cells("posDescription").Value & "')"
                cmd = New OracleCommand(sql, conn)
                cmd.CommandType = CommandType.Text
                cmd.ExecuteReader()
            Next
            dr.Close()
        Catch ex As Exception
            createExceptionFile(ex.Message, sql)
            MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            cmd.Dispose()
        End Try
        Me.Dispose()
    End Sub

    Private Sub lnkLblBarcodes_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkLblBarcodes.LinkClicked
        If dgvLinkedProducts.Rows.Count = 10 Then
            MessageBox.Show("Δεν μπορείτε να συνδέσετε περισσότερα από 10 προϊόντα ανά κουμπί")
            Exit Sub
        End If
        Dim newBarcode As Object
        newBarcode = InputBox("Εισαγωγή νέου Barcode", "Νέο Barcode", "")
        If newBarcode Is "" Then
            Exit Sub
        Else
            addInGrid(newBarcode)
        End If
        txtBoxButtonName.Focus()
    End Sub

    Private Sub addInGrid(ByVal newBarcode As String)
        Dim sql As String = ""
        Dim cmd As New OracleCommand(sql, conn)
        Dim dr As OracleDataReader
        Try
            sql = "select product_serno, description " & _
                  "from barcodes b " & _
                  "inner join products p on p.serno = b.product_serno " & _
                  "where UPPER(barcode) = '" & newBarcode.ToUpper & "'"
            cmd = New OracleCommand(sql, conn)
            cmd.CommandType = CommandType.Text
            dr = cmd.ExecuteReader()
            If dr.Read() Then
                For i = 0 To dgvLinkedProducts.Rows.Count - 1
                    If CInt(dgvLinkedProducts.Rows(i).Cells("productSerno").Value) = CInt(dr(0)) Then
                        MessageBox.Show("Το προϊόν είναι ήδη συνδεμένο με του κουμπί", "Καταχώρηση Barcode/Προϊόντος", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                        Exit Sub
                    End If
                Next
                Dim row As String() = New String() {dr(0), dr(1), dr(1)}
                dgvLinkedProducts.Rows.Add(row)
            Else
                MessageBox.Show("Το barcode δεν είναι καταχωρημένο στην διαχείρηση προϊόντων")
            End If
            dr.Close()
        Catch ex As Exception
        Finally
            cmd.Dispose()
        End Try
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If MessageBox.Show("Να διαγραφεί το κουμπί;", "Διαγραφή Κουμπιού", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
            Dim sql As String
            Dim cmd = New OracleCommand("", conn)
            Try
                sql = "delete from BTN_POS" + tableIndex + "_DET"
                cmd = New OracleCommand(sql, conn)
                cmd.CommandType = CommandType.Text
                cmd.ExecuteReader()
                sql = "delete from BTN_POS" + tableIndex
                cmd = New OracleCommand(sql, conn)
                cmd.CommandType = CommandType.Text
                cmd.ExecuteReader()
                cmd.Dispose()
                Me.Dispose()
            Catch ex As Exception
                MessageBox.Show(ex.Message + sql, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub txtBoxSearchBox_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBoxButtonName.MouseEnter
        txtBoxButtonName.BackColor = Color.Bisque
    End Sub

    Private Sub txtBoxSearchBox_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBoxButtonName.MouseLeave
        txtBoxButtonName.BackColor = Color.LemonChiffon
    End Sub

    Private Sub frmProducts_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        setCurrentTableIndex()
        loadButtonDetails()
    End Sub

    Private Sub setCurrentTableIndex()
        If currentBtnPosEdit.Equals("btnPos1") Or currentBtnPosEdit.Equals("btnPos2") Or currentBtnPosEdit.Equals("btnPos3") Or _
           currentBtnPosEdit.Equals("btnPos4") Or currentBtnPosEdit.Equals("btnPos5") Or currentBtnPosEdit.Equals("btnPos6") Or _
           currentBtnPosEdit.Equals("btnPos7") Or currentBtnPosEdit.Equals("btnPos8") Or currentBtnPosEdit.Equals("btnPos9") Then
            tableIndex = currentBtnPosEdit.Substring(6, 1)
        Else
            tableIndex = currentBtnPosEdit.Substring(6, 2)
        End If
    End Sub

    Private Sub loadButtonDetails()
        Dim sql As String = ""
        Dim cmd As New OracleCommand("", conn)
        Dim dr As OracleDataReader
        dgvLinkedProducts.Rows.Clear()
        txtBoxButtonName.Clear()
        chkBoxVisibleOnPOS.Checked = False
        Try
            sql = "select * from BTN_POS" + tableIndex
            cmd = New OracleCommand(sql, conn)
            cmd.CommandType = CommandType.Text
            dr = cmd.ExecuteReader()
            If dr.Read() Then
                txtBoxButtonName.Text = CStr(dr(0))
                If CInt(dr(1)) = 1 Then
                    chkBoxVisibleOnPOS.Checked = True
                Else
                    chkBoxVisibleOnPOS.Checked = False
                End If

                sql = "select product_serno, description, display_desc  " & _
                      "from BTN_POS" + tableIndex + "_DET b " & _
                      "inner join products p on p.serno = b.product_serno"
                cmd = New OracleCommand(sql, conn)
                cmd.CommandType = CommandType.Text
                dr = cmd.ExecuteReader()
                While dr.Read()
                    Dim row As String() = New String() {dr(0), dr(1), dr(2)}
                    dgvLinkedProducts.Rows.Add(row)
                End While
            End If
            dr.Close()
        Catch ex As Exception
            createExceptionFile(ex.Message, sql)
            MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            cmd.Dispose()
        End Try
    End Sub

    Private Sub dgvLinkedProducts_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvLinkedProducts.CellClick
        Dim index As Integer
        Try
            index = dgvLinkedProducts.SelectedRows.Item(0).Index
        Catch ex As Exception
            index = -1
            Exit Sub
        End Try

        If MessageBox.Show(DELETE_SELECTED_LINE, DELETE_LINE, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            dgvLinkedProducts.Rows.RemoveAt(index)
            dgvLinkedProducts.Refresh()
        End If
    End Sub
End Class