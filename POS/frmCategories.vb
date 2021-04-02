Imports Oracle.DataAccess.Client

Public Class frmCategories

    Private Sub frmCategories_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        frmMain.Show()
    End Sub

    Private Sub frmCategories_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub frmCategories_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fillCategoriesList()
        rdbNewCategory.Checked = True
    End Sub

    Private Sub fillCategoriesList()
        Dim cmd As New OracleCommand("", conn)
        Dim dr As OracleDataReader
        Dim sql As String = ""
        Try
            sql = "select uuid, description, vat from categories"
            cmd = New OracleCommand(sql, conn)
            Dim counter As Integer = 0
            cmd.CommandType = CommandType.Text
            dr = cmd.ExecuteReader()
            lstBoxUUIDs.Items.Clear()
            lstBoxDescription.Items.Clear()
            lstBoxVAT.Items.Clear()
            While dr.Read()
                lstBoxUUIDs.Items.Add(dr("uuid"))
                lstBoxDescription.Items.Add(dr("description"))
                lstBoxVAT.Items.Add(dr("vat"))
            End While
            dr.Close()
        Catch ex As Exception
            createExceptionFile(ex.Message, sql)
            MessageBox.Show(ex.Message, "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            cmd.Dispose()
        End Try
    End Sub

    Private Sub rdbExisting_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbExisting.CheckedChanged
        txtBoxVAT.Clear()
        txtBoxDescription.clear()
    End Sub

    Private Sub rdbNewCategory_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbNewCategory.CheckedChanged
        txtBoxVAT.Clear()
        txtBoxDescription.Clear()
        fillCategoriesList()
    End Sub

    Private Sub lstBoxDescription_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstBoxDescription.SelectedIndexChanged
        If rdbNewCategory.Checked Then
            Exit Sub
        Else
            Dim index As Integer = lstBoxDescription.SelectedIndex
            If index < 0 Then
                Exit Sub
            End If
            txtBoxDescription.Text = lstBoxDescription.Text
            txtBoxVAT.Text = lstBoxVAT.Items.Item(index)
        End If
    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Dispose()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim sql As String = ""
        Dim cmd As New OracleCommand("", conn)

        If txtBoxDescription.Text = String.Empty Or txtBoxVAT.Text = String.Empty Then
            MessageBox.Show("Υπάρχουν κενά πεδία", "Πληροφορία", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If Not IsNumeric(txtBoxVAT.Text) Then
            MessageBox.Show("Ο Φ.Π.Α. πρέπει να αποτελείται μόνο από αριθμούς", "Πληροφορία", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If CInt(txtBoxVAT.Text) <> 19 And CInt(txtBoxVAT.Text) <> 5 Then
            MessageBox.Show("Ο Φ.Π.Α. πρέπει να είναι είτε 19% είτε 5%", "Πληροφορία", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Try
            If rdbNewCategory.Checked Then
                sql = "insert into categories (UUID, DESCRIPTION, VAT) " & _
                      "values (sys_guid(), '" & txtBoxDescription.Text.Replace("\'", "\`") & "'," & _
                      "                     " & CInt(txtBoxVAT.Text) & ") "
            Else
                If lstBoxDescription.SelectedIndex = -1 Then
                    MessageBox.Show("Δεν μπορεί να γίνει επεξεργασία αν δεν επιλέξετε κατηγορία", "Επεξεργασία Κατηγορίας", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    cmd.Dispose()
                    Exit Sub
                End If
                sql = "update categories set description = '" & txtBoxDescription.Text.Replace("\'", "\`") & "'," & _
                      "                      vat = " & CInt(txtBoxVAT.Text) & " " & _
                      " where uuid = '" & lstBoxUUIDs.Items.Item(lstBoxDescription.SelectedIndex) & "'"

            End If
            cmd = New OracleCommand(sql, conn)
            cmd.CommandType = CommandType.Text
            cmd.ExecuteReader()

            MessageBox.Show("Η εντολή εκτελέστηκε επιτυχώς", "Πληροφορία", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtBoxDescription.ResetText()
            txtBoxVAT.ResetText()
            fillCategoriesList()
            rdbNewCategory.Checked = True
        Catch ex As Exception
            createExceptionFile(ex.Message, sql)
            MessageBox.Show(ex.Message, "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            cmd.Dispose()
        End Try
    End Sub
End Class