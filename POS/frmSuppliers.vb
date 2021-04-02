Imports Oracle.DataAccess.Client

Public Class frmSuppliers

    Private Sub frmSuppliers_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        frmMain.Show()
    End Sub

    Private Sub frmSuppliers_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub frmSuppliers_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fillSuppliersList()
        rdbNewSupplier.Checked = True
    End Sub

    Private Sub fillSuppliersList()
        Dim cmd As New OracleCommand("", conn)
        Dim dr As OracleDataReader
        Dim sql As String = ""
        Try
            sql = "select uuid, s_name, NVL(phone_1,' ') phone_1, NVL(phone_2,' ') phone_2, NVL(email,' ') email, " & _
                  "NVL(contact_name,' ') contact_name, " & _
                  "NVL(mon,0) mon, NVL(tue,0) tue, NVL(wed,0) wed, NVL(thu,0) thu, NVL(fri,0) fri, NVL(notes,' ') notes, NVL(isdefault,0) isdefault " & _
                  "from suppliers order by s_name asc"
            Dim counter As Integer = 0
            cmd = New OracleCommand(sql, conn)
            cmd.CommandType = CommandType.Text

            dr = cmd.ExecuteReader()
            lstBoxUUID.Items.Clear()
            lstBoxName.Items.Clear()
            lstBoxPhone1.Items.Clear()
            lstBoxPhone2.Items.Clear()
            lstBoxEmail.Items.Clear()
            lstBoxContactName.Items.Clear()
            lstBoxMon.Items.Clear()
            lstBoxTue.Items.Clear()
            lstBoxWed.Items.Clear()
            lstBoxThu.Items.Clear()
            lstBoxFri.Items.Clear()
            lstBoxNotes.Items.Clear()
            lstBoxIsDefault.items.clear()

            While dr.Read()
                lstBoxUUID.Items.Add(dr("uuid"))
                lstBoxName.Items.Add(dr("s_name"))
                lstBoxPhone1.Items.Add(dr("phone_1"))
                lstBoxPhone2.Items.Add(dr("phone_2"))
                lstBoxEmail.Items.Add(dr("email"))
                lstBoxContactName.Items.Add(dr("contact_name"))
                lstBoxMon.Items.Add(dr("mon"))
                lstBoxTue.Items.Add(dr("tue"))
                lstBoxWed.Items.Add(dr("wed"))
                lstBoxThu.Items.Add(dr("thu"))
                lstBoxFri.Items.Add(dr("fri"))
                lstBoxNotes.Items.Add(dr("notes"))
                lstBoxIsDefault.Items.Add(dr("isdefault"))
            End While
            dr.Close()
        Catch ex As Exception
            createExceptionFile(ex.Message, sql)
            MessageBox.Show(ex.Message, "Applicaton Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            cmd.Dispose()
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Dispose()
    End Sub

    Private Sub rdbNewSupplier_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbNewSupplier.CheckedChanged
        resetFields()
        btnClear.Visible = True
        fillSuppliersList()
    End Sub

    Private Sub rdbExisting_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbExisting.CheckedChanged
        resetFields()
        fillSuppliersList()
        btnClear.Visible = False
        btnDeleteSupplier.Visible = True
    End Sub

    Private Sub resetFields()
        txtBoxContactName.Clear()
        txtBoxEmail.Clear()
        txtBoxName.Clear()
        txtBoxPhone1.Clear()
        txtBoxPhone2.Clear()
        txtBoxNotes.Clear()
        ckbMon.Checked = False
        ckbTue.Checked = False
        ckbWed.Checked = False
        ckbThu.Checked = False
        ckbFri.Checked = False
        btnDeleteSupplier.Visible = False
    End Sub

    Private Sub lstBoxName_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstBoxName.SelectedIndexChanged
        If rdbNewSupplier.Checked Then
            Exit Sub
        Else
            Dim index As Integer = lstBoxName.SelectedIndex
            If index < 0 Then
                Exit Sub
            End If
            txtBoxName.Text = lstBoxName.Text
            txtBoxContactName.Text = lstBoxContactName.Items.Item(index)
            txtBoxEmail.Text = lstBoxEmail.Items.Item(index)
            txtBoxPhone1.Text = lstBoxPhone1.Items.Item(index)
            txtBoxPhone2.Text = lstBoxPhone2.Items.Item(index)
            txtBoxNotes.Text = lstBoxNotes.items.item(index)

            If lstBoxMon.Items.Item(index) = 1 Then
                ckbMon.Checked = True
            Else
                ckbMon.Checked = False
            End If

            If lstBoxTue.Items.Item(index) = 1 Then
                ckbTue.Checked = True
            Else
                ckbTue.Checked = False
            End If

            If lstBoxWed.Items.Item(index) = 1 Then
                ckbWed.Checked = True
            Else
                ckbWed.Checked = False
            End If

            If lstBoxThu.Items.Item(index) = 1 Then
                ckbThu.Checked = True
            Else
                ckbThu.Checked = False
            End If

            If lstBoxFri.Items.Item(index) = 1 Then
                ckbFri.Checked = True
            Else
                ckbFri.Checked = False
            End If
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim sql As String = ""

        Dim mon, tue, wed, thu, fri As Integer

        If ckbMon.Checked = True Then
            mon = 1
        Else
            mon = 0
        End If

        If ckbTue.Checked = True Then
            tue = 1
        Else
            tue = 0
        End If

        If ckbWed.Checked = True Then
            wed = 1
        Else
            wed = 0
        End If

        If ckbThu.Checked = True Then
            thu = 1
        Else
            thu = 0
        End If

        If ckbFri.Checked = True Then
            fri = 1
        Else
            fri = 0
        End If

        If Not IsNumeric(txtBoxPhone1.Text) Then
            MessageBox.Show("Το πεδίο τηλέφωνο (1) πρέπει να αποτελείται μόνο από αριθμούς", "Πληροφορία", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If txtBoxPhone2.Text.Length > 0 And Not txtBoxPhone2.Text.Equals(" ") And Not IsNumeric(txtBoxPhone2.Text) Then
            MessageBox.Show("Το πεδίο τηλέφωνο (2) πρέπει να αποτελείται μόνο από αριθμούς", "Πληροφορία", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If txtBoxNotes.Text = String.Empty Then
            txtBoxNotes.Text = " "
        End If

        If txtBoxPhone2.Text = String.Empty Then
            txtBoxPhone2.Text = " "
        End If

        If txtBoxEmail.Text = String.Empty Then
            txtBoxEmail.Text = " "
        End If

        If rdbNewSupplier.Checked Then
            If txtBoxContactName.Text = String.Empty Or txtBoxName.Text = String.Empty Or txtBoxPhone1.Text = String.Empty Then
                MessageBox.Show("Υπάρχουν κενά πεδία", "Πληροφορία", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            If supplierExists(txtBoxName.Text.Replace("'", "`")) Then
                MessageBox.Show("Υπάρχει ήδη καταχωρημένος Προμηθευτής με αυτό το όνομα", "Πληροφορία", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            sql = "insert into suppliers (UUID, S_NAME, PHONE_1, PHONE_2, EMAIL, CONTACT_NAME, MON, TUE, WED, THU, FRI, notes) " & _
                  "values (sys_guid(), '" & txtBoxName.Text.Replace("'", "`") & "'," & _
                  "                    '" & txtBoxPhone1.Text & "', " & _
                  "                    '" & txtBoxPhone2.Text & "', " & _
                  "                    '" & txtBoxEmail.Text.Replace("'", "`") & "'," & _
                  "                    '" & txtBoxContactName.Text.Replace("'", "`") & "'," & _
                  "                     " & mon & ", " & _
                  "                     " & tue & ", " & _
                  "                     " & wed & ", " & _
                  "                     " & thu & ", " & _
                  "                     " & fri & ", " & _
                  "                    '" & txtBoxNotes.Text.Replace("'", "`") & "') "
        Else
            If lstBoxName.SelectedIndex = -1 Then
                MessageBox.Show("Δεν έχετε επιλέξει προμηθευτή για επεξεργασία", "Επεξεργασία Προμηθευτή", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            sql = "update suppliers set " & _
                  "                      S_NAME = '" & txtBoxName.Text.Replace("'", "`") & "'," & _
                  "                      PHONE_1 = '" & txtBoxPhone1.Text & "', " & _
                  "                      PHONE_2 = '" & txtBoxPhone2.Text & "', " & _
                  "                      EMAIL = '" & txtBoxEmail.Text.Replace("'", "`") & "'," & _
                  "                      CONTACT_NAME = '" & txtBoxContactName.Text.Replace("'", "`") & "'," & _
                  "                      MON = " & mon & ", " & _
                  "                      TUE = " & tue & ", " & _
                  "                      WED = " & wed & ", " & _
                  "                      THU = " & thu & ", " & _
                  "                      FRI = " & fri & ", " & _
                  "                      notes = '" & txtBoxNotes.Text.Replace("'", "`") & "' " & _
                  " where uuid = '" & lstBoxUUID.Items.Item(lstBoxName.SelectedIndex) & "'"
        End If

        Dim cmd As New OracleCommand("", conn)
        Try
            cmd = New OracleCommand(sql, conn)
            cmd.CommandType = CommandType.Text
            cmd.ExecuteReader()
        Catch ex As Exception
            createExceptionFile(ex.Message, sql)
            MessageBox.Show(ex.Message, "Applicaton Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            cmd.Dispose()
        End Try

        MessageBox.Show("Η εντολή εκτελέστηκε επιτυχώς", "Πληροφορία", MessageBoxButtons.OK, MessageBoxIcon.Information)
        resetFields()
        fillSuppliersList()
        rdbNewSupplier.Checked = True
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        txtBoxName.Clear()
        txtBoxContactName.Clear()
        txtBoxEmail.Clear()
        txtBoxName.Clear()
        txtBoxPhone1.Clear()
        txtBoxPhone2.Clear()
    End Sub

    Private Function supplierExists(ByVal name As String) As Boolean
        Dim sql As String = "select count(*) from suppliers where upper(s_name) = '" & name.ToUpper & "' "
        Dim cmd As New OracleCommand(sql, conn)
        Dim dr As OracleDataReader
        Dim found As Boolean = False
        Try
            cmd = New OracleCommand(sql, conn)
            cmd.CommandType = CommandType.Text
            dr = cmd.ExecuteReader()
            If dr.Read Then
                If CInt(dr(0)) > 0 Then
                    found = True
                End If
            End If
            dr.Close()
        Catch ex As Exception
            createExceptionFile(ex.Message, sql)
            MessageBox.Show(ex.Message, "Applicaton Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            cmd.Dispose()
        End Try
        Return found
    End Function

    Private Sub btnDeleteSupplier_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteSupplier.Click
        Dim sql As String = ""

        If lstBoxName.SelectedIndex = -1 Then
            MessageBox.Show("Δεν έχετε επιλέξει προμηθευτή", "Διαγραφή Προμηθευτή", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        'If default you cannot delete
        If lstBoxIsDefault.Items.Item(lstBoxName.SelectedIndex).ToString.Equals("1") Then
            MessageBox.Show("Δεν μπορειτε να διαγραψετε αυτόν τον προμηθευτή", "Διαγραφή Προμηθευτή", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        '1. Update linked products with default supplier
        sql = "update products set supplier_id =(select uuid from suppliers where isdefault=1) " & _
              "where supplier_id = '" & lstBoxUUID.Items.Item(lstBoxName.SelectedIndex) & "'"

        Dim cmd As New OracleCommand("", conn)
        Try
            cmd = New OracleCommand(sql, conn)
            cmd.CommandType = CommandType.Text
            cmd.ExecuteReader()

            '2. Delete supplier
            sql = "delete from suppliers " & _
                  " where uuid = '" & lstBoxUUID.Items.Item(lstBoxName.SelectedIndex) & "'"

            cmd = New OracleCommand(sql, conn)
            cmd.CommandType = CommandType.Text
            cmd.ExecuteReader()
        Catch ex As Exception
            createExceptionFile(ex.Message, sql)
            MessageBox.Show(ex.Message, "Applicaton Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            cmd.Dispose()
        End Try

        MessageBox.Show("Η εντολή εκτελέστηκε επιτυχώς", "Πληροφορία", MessageBoxButtons.OK, MessageBoxIcon.Information)
        resetFields()
        fillSuppliersList()
        rdbNewSupplier.Checked = True
    End Sub
End Class