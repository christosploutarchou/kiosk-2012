Imports Oracle.DataAccess.Client

Public Class frmDual
    Private Sub frmDual_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        With Me.dgvReceiptDual.DefaultCellStyle
            .Font = New Font(REPORT_FONT, 15)
            .ForeColor = Color.Black
            .BackColor = Color.Beige
        End With
    End Sub

    Protected Overrides ReadOnly Property CreateParams() As CreateParams
        'Disables X button
        Get
            Dim param As CreateParams = MyBase.CreateParams
            param.ClassStyle = param.ClassStyle Or &H200
            Return param
        End Get
    End Property

    Private Sub lblTotal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblTotal.Click

    End Sub
End Class