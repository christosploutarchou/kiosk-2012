Imports Oracle.DataAccess.Client
Public Class frmLottery
    Dim tableIndex As String = ""

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Dispose()
    End Sub

    Private Sub frmNewProduct_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        frmMain.Show()
    End Sub

    Private Sub frmNewProduct_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim sql As String = ""
        Dim cmd As New OracleCommand(sql, conn)
        If dgvLinkedProducts.Rows.Count = 0 Then
            MessageBox.Show("Δεν έχετε συνδέσει προιόν(τα) με τα λαχεία", "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Try
            sql = "delete from LOTTERY"
            cmd = New OracleCommand(sql, conn)
            cmd.CommandType = CommandType.Text
            cmd.ExecuteReader()

            For i = 0 To dgvLinkedProducts.Rows.Count - 1
                sql = "insert into LOTTERY (barcode) " & _
                      "values (" & "'" & dgvLinkedProducts.Rows(i).Cells("barcode").Value & "')"
                cmd = New OracleCommand(sql, conn)
                cmd.CommandType = CommandType.Text
                cmd.ExecuteNonQuery()
            Next
            calculateAmount()
        Catch ex As Exception
            createExceptionFile(ex.Message, sql)
            MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            cmd.Dispose()
        End Try
        Me.Dispose()
    End Sub

    Private Sub calculateAmount()
        Dim totalAmt As Integer = 0
        For i = 0 To dgvLinkedProducts.Rows.Count - 1
            totalAmt += (dgvLinkedProducts.Rows(i).Cells("sellAmt").Value) * (dgvLinkedProducts.Rows(i).Cells("availQnt").Value)
        Next
        txtBoxLotteryAmt.Text = totalAmt.ToString + " ευρώ"
    End Sub

    Private Sub lnkLblBarcodes_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkLblBarcodes.LinkClicked
        Dim newBarcode As Object
        newBarcode = InputBox("Εισαγωγή νέου Barcode", "Νέο Barcode", "")
        If newBarcode Is "" Then
            Exit Sub
        Else
            addInGrid(newBarcode)
        End If
        txtBoxLotteryAmt.Focus()
    End Sub

    Private Sub addInGrid(ByVal newBarcode As String)
        Dim sql As String = ""
        Dim cmd As New OracleCommand(sql, conn)
        Dim dr As OracleDataReader
        Try
            sql = "select product_serno, barcode, description, sell_amt, avail_quantity " & _
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
                Dim row As String() = New String() {dr(0), dr(1), dr(2), CDbl(dr(3)), dr(4)}
                dgvLinkedProducts.Rows.Add(row)
                calculateAmount()
            Else
                MessageBox.Show("Το barcode δεν είναι καταχωρημένο στην διαχείρηση προϊόντων", "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            dr.Close()
        Catch ex As Exception
        Finally
            cmd.Dispose()
        End Try
    End Sub

    Private Sub txtBoxSearchBox_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBoxLotteryAmt.MouseEnter
        txtBoxLotteryAmt.BackColor = Color.Bisque
    End Sub

    Private Sub txtBoxSearchBox_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBoxLotteryAmt.MouseLeave
        txtBoxLotteryAmt.BackColor = Color.LemonChiffon
    End Sub

    Private Sub frmLottery_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        loadLotteryDetails()
        calculateAmount()
    End Sub

    Private Sub loadLotteryDetails()
        Dim sql As String = ""
        Dim cmd As New OracleCommand("", conn)
        Dim dr As OracleDataReader
        dgvLinkedProducts.Rows.Clear()
        txtBoxLotteryAmt.Clear()
        Try
            sql = "select b.product_serno, l.barcode, p.description, p.sell_amt, p.avail_quantity " & _
                  "from lottery l " & _
                  "inner join barcodes b on b.barcode = l.barcode " & _
                  "inner join products p on p.serno = b.product_serno "
            cmd = New OracleCommand(sql, conn)
            cmd.CommandType = CommandType.Text
            dr = cmd.ExecuteReader()
            While dr.Read()
                Dim row As String() = New String() {dr(0), dr(1), dr(2), dr(3), dr(4)}
                dgvLinkedProducts.Rows.Add(row)
            End While
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
            calculateAmount()
        End If
    End Sub
End Class