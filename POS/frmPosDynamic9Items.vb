﻿Public Class frmPosDynamic9Items
    Dim tmpBtnItem As BtnItem

    Private Sub frmDynamic9Items_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        tmpBtnItem = btnItemsMap.Item(currentBtnPosEdit)
        Me.Text = tmpBtnItem.Name
        For i As Integer = 1 To 9
            CType(Me.grbProducts.Controls("btnItem" & i), Button).Text = tmpBtnItem.LinkedItemsDetails.Item(i - 1).DisplayDesc()
        Next
    End Sub

    Private Sub btnItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnItem1.Click, btnItem2.Click, btnItem3.Click, _
                                                                                              btnItem4.Click, btnItem5.Click, btnItem6.Click, _
                                                                                              btnItem7.Click, btnItem8.Click, btnItem9.Click
        setDetails(sender.name, tmpBtnItem)
        Me.Dispose()
    End Sub
End Class