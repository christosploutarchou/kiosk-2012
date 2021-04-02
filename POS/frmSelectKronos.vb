Public Class frmSelectKronos

    Private Sub btnBackspace_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackspace.Click
        Dim index As Integer
        Try
            index = dgvKronosProducts.CurrentCell.RowIndex
        Catch ex As Exception
            index = -1            
        End Try
        If index = -1 Then
            MessageBox.Show("Δεν έχετε επιλέξει προϊόν", "Επιλογή Προϊόντος", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        Else
            frmPOS.tmpKronosSelectedIndex = index
        End If
        Me.Dispose()
    End Sub
End Class