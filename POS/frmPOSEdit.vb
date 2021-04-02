Imports Oracle.DataAccess.Client
Imports System.ComponentModel

Public Class frmPOSEdit
    Private Sub frmPOSEdit_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        frmMain.Show()
    End Sub

    Private Sub frmPOSEdit_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub frmPOSEdit_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        loadButtonParams(1)
    End Sub

    Private Sub loadButtonParams(ByVal all As Integer)
        Dim sql As String = ""
        Dim cmd As New OracleCommand("", conn)
        Dim dr As OracleDataReader
        Try
            If all = 1 Then
                For i As Integer = 1 To 23
                    sql = "select disp_name, is_visible from BTN_POS" + i.ToString
                    cmd = New OracleCommand(sql, conn)
                    dr = cmd.ExecuteReader
                    If dr.Read() Then
                        CType(Me.Controls("btnPos" & i), Button).Text = dr(0)
                        If CInt(dr(1)) = 0 Then
                            CType(Me.Controls("btnPos" & i), Button).BackColor = Color.Red
                        Else
                            CType(Me.Controls("btnPos" & i), Button).BackColor = Color.Blue
                        End If
                    Else
                        CType(Me.Controls("btnPos" & i), Button).BackColor = Color.Red
                        CType(Me.Controls("btnPos" & i), Button).Text = ""
                    End If
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
            End If
            
        Catch ex As Exception
            MessageBox.Show(ex.Message + sql, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            cmd.Dispose()
        End Try
    End Sub

    Private Function setCurrentTableIndex() As String
        If currentBtnPosEdit.Equals("btnPos1") Or currentBtnPosEdit.Equals("btnPos2") Or currentBtnPosEdit.Equals("btnPos3") Or _
           currentBtnPosEdit.Equals("btnPos4") Or currentBtnPosEdit.Equals("btnPos5") Or currentBtnPosEdit.Equals("btnPos6") Or _
           currentBtnPosEdit.Equals("btnPos7") Or currentBtnPosEdit.Equals("btnPos8") Or currentBtnPosEdit.Equals("btnPos9") Then
            Return currentBtnPosEdit.Substring(6, 1)
        Else
            Return currentBtnPosEdit.Substring(6, 2)
        End If
    End Function

    Private Sub btnPos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _
                                                                btnPos1.Click, btnPos2.Click, btnPos3.Click, btnPos4.Click, _
                                                                btnPos5.Click, btnPos6.Click, btnPos7.Click, btnPos8.Click, _
                                                                btnPos9.Click, btnPos10.Click, btnPos11.Click, btnPos12.Click, _
                                                                btnPos13.Click, btnPos14.Click, btnPos15.Click, btnPos16.Click, _
                                                                btnPos17.Click, btnPos18.Click, btnPos19.Click, btnPos20.Click, _
                                                                btnPos21.Click, btnPos22.Click, btnPos23.Click
        updateButton(sender)
    End Sub

    Private Sub updateButton(ByVal sender As Object)
        currentBtnPosEdit = sender.name
        frmPosButtonModal.ShowDialog()
        loadButtonParams(0)
    End Sub

End Class
