Imports Oracle.DataAccess.Client
Public Class frmBoxBarcodesModal
    Dim serno As Integer = -1


    Private Sub frmBoxBarcodesModal_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        Me.Dispose()
    End Sub

    Private Sub frmBoxBarcodesModal_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        tmpBarcodeNotFoundExit = False
        Dim cmd = New OracleCommand("", conn)
        If txtBoxBarcode.Text.Length >= minBarcode Then
            Try
                Dim sql As String = "select p.serno, p.description " & _
                  "from products p " & _
                  "where p.serno = (select b.product_serno from BARCODES b where UPPER(b.barcode) =  '" & txtBoxBarcode.Text.ToUpper & "')"

                cmd = New OracleCommand(sql, conn)
                cmd.CommandType = CommandType.Text

                Dim dr = cmd.ExecuteReader()

                If dr.Read() Then
                    'Check if product already in the grid
                    For i = 0 To dgvProductsAndQnt.Rows.Count - 1
                        If CInt(dgvProductsAndQnt.Rows(i).Cells("productSerno").Value) = CInt(dr(0)) Then
                            MessageBox.Show("Υπάρχει ήδη καταχωρηση για το: " + CStr(dr(1)), "Καταχώρηση", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                            txtBoxBarcode.Clear()
                            dr.Close()
                            cmd.Dispose()
                            Exit Sub
                        End If
                    Next
                    Dim row As String() = New String() {dr(0), dr(1), txtBoxBarcode.Text}
                    dgvProductsAndQnt.Rows.Add(row)
                    txtBoxBarcode.Clear()
                Else
                    tmpBarcodeNotFound = txtBoxBarcode.Text.ToUpper
                    frmProductsModal.ShowDialog()
                    'If user cancelled new product addition don't do anything
                    'Else
                    'Search for the new product added
                    'If Not tmpBarcodeNotFoundExit Then
                    'txtBoxBarcode_TextChanged(sender, e)
                    'Else
                    txtBoxBarcode.Clear()
                    'End If
                End If
        dr.Close()

            Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            cmd.Dispose()
        End Try
        End If
    End Sub

    Private Sub btnOverrideExisting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOverrideExisting.Click
        Dim sql As String = "delete from boxbarcodes where PRODUCT_SERNO = " & tmpGlobalProductSerno & ""
        Dim cmd = New OracleCommand(sql, conn)
        cmd.CommandType = CommandType.Text
        Try
            cmd = New OracleCommand(sql, conn)
            cmd.CommandType = CommandType.Text
            cmd.ExecuteReader()

            For i = 0 To dgvProductsAndQnt.Rows.Count - 1
                sql = "insert into boxbarcodes (product_serno, barcode) values (" & tmpGlobalProductSerno & ", '" & dgvProductsAndQnt.Rows(i).Cells("barcode").Value & "')"
                cmd = New OracleCommand(sql, conn)
                cmd.CommandType = CommandType.Text
                Using cmd                    cmd.ExecuteNonQuery()                End Using
            Next
        Catch ex As Exception
            createExceptionFile(ex.Message, sql)
            MessageBox.Show(ex.Message, "Applicaton Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            cmd.Dispose()
        End Try
        MessageBox.Show("Η εντολή εκτελέστηκε επιτυχώς", "Πληροφορία", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub


    Private Sub dgvProductsAndQnt_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvProductsAndQnt.CellClick
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

    Private Sub btnExit_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Dispose()
    End Sub

    Private Sub formatDataGrid()
        If dgvProductsAndQnt.Rows.Count > 0 Then
            For i = 0 To dgvProductsAndQnt.Rows.Count - 1
                dgvProductsAndQnt.Rows(i).Cells("serno").Value = i + 1
            Next
            dgvProductsAndQnt.FirstDisplayedScrollingRowIndex = dgvProductsAndQnt.Rows.Count - 1
        End If
    End Sub

    Private Sub frmBoxBarcodesModal_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim cmd As New OracleCommand("", conn)
        Try
            Dim sql As String = "select barcode from boxbarcodes where product_serno = " & tmpGlobalProductSerno & " "
            cmd = New OracleCommand(sql, conn)
            Dim dr = cmd.ExecuteReader()
            While dr.Read()
                Dim tmpDesc As String = ""
                Dim sqlInner = "select description from products where serno = (select product_serno from barcodes where UPPER(barcode) = '" & CStr(dr(0)).ToUpper & "')"
                Dim cmd1 = New OracleCommand(sqlInner, conn)
                Dim dr1 = cmd1.ExecuteReader()
                While dr1.Read()
                    tmpDesc = CStr(dr1(0))
                End While
                Dim row As String() = New String() {tmpGlobalProductSerno, tmpDesc, dr(0)}
                dgvProductsAndQnt.Rows.Add(row)
            End While
            dr.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            cmd.Dispose()
        End Try


    End Sub
End Class