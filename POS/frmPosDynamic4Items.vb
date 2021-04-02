Public Class frmPosDynamic4Items
    Dim tmpBtnItem As BtnItem

    Private Sub frmPosDynamic4Items_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        tmpBtnItem = btnItemsMap.Item(currentBtnPosEdit)
        Me.Text = tmpBtnItem.Name
        For i As Integer = 1 To 4
            CType(Me.grbProducts.Controls("btnItem" & i), Button).Text = tmpBtnItem.LinkedItemsDetails.Item(i - 1).DisplayDesc()
        Next
    End Sub

    Private Sub btnItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnItem1.Click, btnItem2.Click, btnItem3.Click, _
                                                                                                  btnItem4.Click
        setDetails(sender.name, tmpBtnItem)
        Me.Dispose()
    End Sub
End Class