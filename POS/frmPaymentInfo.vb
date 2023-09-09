Public Class frmPaymentInfo

    Private Sub btnBackspace_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelect.Click
        If Not rdb0Percent.Checked _
            And Not rdb3Percent.Checked _
            And Not rdb5Percent.Checked _
            And Not rdb19Percent.Checked Then
            MessageBox.Show("Δεν έχετε επιλέξει Φ.Π.Α.", "Επιλογή Προϊόντος", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        Me.Dispose()
    End Sub

    Private Sub frmPaymentInfo_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        btnBackspace_Click(sender, e)
    End Sub

    Private Sub frmPaymentInfo_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        btnBackspace_Click(sender, e)
    End Sub

    Private Sub rdb0Percent_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdb0Percent.CheckedChanged
        tmpPaymentVAT = 0
    End Sub

    Private Sub rdb5Percent_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdb5Percent.CheckedChanged
        tmpPaymentVAT = 5
    End Sub

    Private Sub rdb19Percent_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdb19Percent.CheckedChanged
        tmpPaymentVAT = 19
    End Sub

    Private Sub rdb3Percent_CheckedChanged(sender As Object, e As EventArgs) Handles rdb3Percent.CheckedChanged
        tmpPaymentVAT = 3
    End Sub
End Class