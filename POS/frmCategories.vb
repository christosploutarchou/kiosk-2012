Imports System.Data.SQLite
Imports Oracle.DataAccess.Client

Public Class frmCategories

    Private Sub FrmCategories_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        frmMain.Show()
    End Sub

    Private Sub FrmCategories_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub FrmCategories_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        FillCategoriesList()
        rdbNewCategory.Checked = True
    End Sub

    Private Sub FillCategoriesList()
        Dim WhoAmI As String = "frmCategories.FillCategoriesList"
        Dim sql As String = ""

        Try
            lstBoxUUIDs.Items.Clear()
            lstBoxDescription.Items.Clear()
            lstBoxVAT.Items.Clear()

            If SqlLite Then
                sql = "SELECT UUID,
                          DESCRIPTION,
                          VAT
                   FROM CATEGORIES
                   WHERE KIOSKID = @KIOSKID
                   ORDER BY DESCRIPTION"

                Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                    sqliteConn.Open()
                    Using cmd As New SQLiteCommand(sql, sqliteConn)
                        cmd.Parameters.AddWithValue("@KIOSKID", kioskId)
                        Using dr As SQLiteDataReader = cmd.ExecuteReader()
                            While dr.Read()
                                lstBoxUUIDs.Items.Add(dr("UUID").ToString())
                                lstBoxDescription.Items.Add(dr("DESCRIPTION").ToString())
                                lstBoxVAT.Items.Add(If(IsDBNull(dr("VAT")), "", dr("VAT").ToString()))
                            End While
                        End Using
                    End Using
                End Using
            Else
                sql = "SELECT UUID,
                          DESCRIPTION,
                          VAT
                   FROM CATEGORIES
                   ORDER BY DESCRIPTION"

                Using cmd As New OracleCommand(sql, conn)
                    cmd.CommandType = CommandType.Text
                    Using dr As OracleDataReader = cmd.ExecuteReader()
                        While dr.Read()
                            lstBoxUUIDs.Items.Add(dr("UUID").ToString())
                            lstBoxDescription.Items.Add(dr("DESCRIPTION").ToString())
                            lstBoxVAT.Items.Add(If(IsDBNull(dr("VAT")), "", dr("VAT").ToString()))
                        End While
                    End Using
                End Using
            End If
        Catch ex As Exception
            CreateExceptionFile(WhoAmI + " " + ex.Message, sql)
            MessageBox.Show(ex.Message, "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub RdbExisting_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbExisting.CheckedChanged
        txtBoxVAT.Clear()
        txtBoxDescription.Clear()
    End Sub

    Private Sub RdbNewCategory_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbNewCategory.CheckedChanged
        txtBoxVAT.Clear()
        txtBoxDescription.Clear()
        FillCategoriesList()
    End Sub

    Private Sub LstBoxDescription_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstBoxDescription.SelectedIndexChanged
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

    Private Sub CmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Dispose()
    End Sub

    Private Sub BtnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim WhoAmI As String = "frmCategories.BtnSave_Click"

        If txtBoxDescription.Text = String.Empty OrElse txtBoxVAT.Text = String.Empty Then
            MessageBox.Show("Υπάρχουν κενά πεδία", "Πληροφορία", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If Not IsNumeric(txtBoxVAT.Text) Then
            MessageBox.Show("Ο Φ.Π.Α. πρέπει να αποτελείται μόνο από αριθμούς", "Πληροφορία", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If CInt(txtBoxVAT.Text) <> 0 AndAlso
           CInt(txtBoxVAT.Text) <> 3 AndAlso
           CInt(txtBoxVAT.Text) <> 5 AndAlso
           CInt(txtBoxVAT.Text) <> 19 Then
            MessageBox.Show("Ο Φ.Π.Α. πρέπει να είναι 0% ή 3% ή 5% ή 19%", "Πληροφορία", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim sql As String = ""

        Try
            If SqlLite Then
                Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                    sqliteConn.Open()

                    If rdbNewCategory.Checked Then
                        sql =
                            "INSERT INTO CATEGORIES
                            (
                                UUID,
                                DESCRIPTION,
                                VAT,
                                KIOSKID,
                                UPDATED_AT,
                                SYNC_STATUS
                            )
                            VALUES
                            (
                                @UUID,
                                @DESCRIPTION,
                                @VAT,
                                @KIOSKID,
                                CURRENT_TIMESTAMP,
                                1
                            )"

                        Using cmd As New SQLiteCommand(sql, sqliteConn)
                            cmd.Parameters.AddWithValue("@UUID", Guid.NewGuid().ToString("N").ToUpper())
                            cmd.Parameters.AddWithValue("@DESCRIPTION", txtBoxDescription.Text.Trim())
                            cmd.Parameters.AddWithValue("@VAT", CInt(txtBoxVAT.Text))
                            cmd.Parameters.AddWithValue("@KIOSKID", kioskId)
                            cmd.ExecuteNonQuery()
                        End Using
                    Else
                        If lstBoxDescription.SelectedIndex = -1 Then
                            MessageBox.Show("Δεν μπορεί να γίνει επεξεργασία αν δεν επιλέξετε κατηγορία", "Επεξεργασία Κατηγορίας", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End If

                        sql =
                            "UPDATE CATEGORIES
                             SET DESCRIPTION=@DESCRIPTION,
                                 VAT=@VAT,
                                 UPDATED_AT=CURRENT_TIMESTAMP,
                                 SYNC_STATUS=1
                             WHERE UUID=@UUID
                             AND KIOSKID=@KIOSKID"

                        Using cmd As New SQLiteCommand(sql, sqliteConn)
                            cmd.Parameters.AddWithValue("@DESCRIPTION", txtBoxDescription.Text.Trim())
                            cmd.Parameters.AddWithValue("@VAT", CInt(txtBoxVAT.Text))
                            cmd.Parameters.AddWithValue("@UUID", lstBoxUUIDs.Items(lstBoxDescription.SelectedIndex).ToString())
                            cmd.Parameters.AddWithValue("@KIOSKID", kioskId)
                            cmd.ExecuteNonQuery()
                        End Using
                    End If
                End Using
            Else
                Using cmd As New OracleCommand()
                    cmd.Connection = conn
                    cmd.BindByName = True
                    If rdbNewCategory.Checked Then
                        sql =
                            "INSERT INTO CATEGORIES
                            (
                                UUID,
                                DESCRIPTION,
                                VAT,
                                UPDATED_AT
                            )
                            VALUES
                            (
                                SYS_GUID(),
                                :DESCRIPTION,
                                :VAT,
                                SYSTIMESTAMP
                            )"

                        cmd.CommandText = sql
                        cmd.Parameters.Add("DESCRIPTION", OracleDbType.Varchar2).Value = txtBoxDescription.Text.Trim()
                        cmd.Parameters.Add("VAT", OracleDbType.Int32).Value = CInt(txtBoxVAT.Text)
                    Else
                        If lstBoxDescription.SelectedIndex = -1 Then
                            MessageBox.Show("Δεν μπορεί να γίνει επεξεργασία αν δεν επιλέξετε κατηγορία", "Επεξεργασία Κατηγορίας", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End If

                        sql =
                            "UPDATE CATEGORIES
                             SET DESCRIPTION=:DESCRIPTION,
                                 VAT=:VAT,
                                 UPDATED_AT=SYSTIMESTAMP
                             WHERE UUID=:UUID"

                        cmd.CommandText = sql
                        cmd.Parameters.Add("DESCRIPTION", OracleDbType.Varchar2).Value = txtBoxDescription.Text.Trim()
                        cmd.Parameters.Add("VAT", OracleDbType.Int32).Value = CInt(txtBoxVAT.Text)
                        cmd.Parameters.Add("UUID", OracleDbType.Varchar2).Value =
                        lstBoxUUIDs.Items(lstBoxDescription.SelectedIndex).ToString()
                    End If
                    cmd.ExecuteNonQuery()
                End Using
            End If

            MessageBox.Show("Η εντολή εκτελέστηκε επιτυχώς", "Πληροφορία", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtBoxDescription.Clear()
            txtBoxVAT.Clear()

            FillCategoriesList()
            rdbNewCategory.Checked = True
        Catch ex As Exception
            CreateExceptionFile(WhoAmI + " " + ex.Message, sql)
            MessageBox.Show(ex.Message, "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class