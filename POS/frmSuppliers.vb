Imports System.Data.SQLite
Imports Oracle.DataAccess.Client

Public Class frmSuppliers
    Private Sub FrmSuppliers_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        frmMain.Show()
    End Sub

    Private Sub FrmSuppliers_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub FrmSuppliers_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        FillSuppliersList()
        rdbNewSupplier.Checked = True
    End Sub

    Private Sub FillSuppliersList()
        Dim WhoAmI As String = "FillSuppliersList"
        Dim sql As String = ""

        Try
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
            lstBoxIsDefault.Items.Clear()

            If SqlLite Then
                sql =
                    "SELECT
                        UUID,
                        S_NAME,
                        IFNULL(PHONE_1,' ') PHONE_1,
                        IFNULL(PHONE_2,' ') PHONE_2,
                        IFNULL(EMAIL,' ') EMAIL,
                        IFNULL(CONTACT_NAME,' ') CONTACT_NAME,
                        IFNULL(MON,0) MON,
                        IFNULL(TUE,0) TUE,
                        IFNULL(WED,0) WED,
                        IFNULL(THU,0) THU,
                        IFNULL(FRI,0) FRI,
                        IFNULL(NOTES,' ') NOTES,
                        IFNULL(ISDEFAULT,0) ISDEFAULT
                    FROM SUPPLIERS
                    WHERE KIOSKID=@KIOSKID
                    ORDER BY S_NAME"

                Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                    sqliteConn.Open()
                    Using cmd As New SQLiteCommand(sql, sqliteConn)
                        cmd.Parameters.AddWithValue("@KIOSKID", kioskId)
                        Using dr As SQLiteDataReader = cmd.ExecuteReader()
                            While dr.Read()
                                lstBoxUUID.Items.Add(dr("UUID").ToString())
                                lstBoxName.Items.Add(dr("S_NAME").ToString())
                                lstBoxPhone1.Items.Add(dr("PHONE_1").ToString())
                                lstBoxPhone2.Items.Add(dr("PHONE_2").ToString())
                                lstBoxEmail.Items.Add(dr("EMAIL").ToString())
                                lstBoxContactName.Items.Add(dr("CONTACT_NAME").ToString())
                                lstBoxMon.Items.Add(Convert.ToInt32(dr("MON")))
                                lstBoxTue.Items.Add(Convert.ToInt32(dr("TUE")))
                                lstBoxWed.Items.Add(Convert.ToInt32(dr("WED")))
                                lstBoxThu.Items.Add(Convert.ToInt32(dr("THU")))
                                lstBoxFri.Items.Add(Convert.ToInt32(dr("FRI")))
                                lstBoxNotes.Items.Add(dr("NOTES").ToString())
                                lstBoxIsDefault.Items.Add(Convert.ToInt32(dr("ISDEFAULT")))
                            End While
                        End Using
                    End Using
                End Using
            Else
                sql =
                    "SELECT
                        UUID,
                        S_NAME,
                        NVL(PHONE_1,' ') PHONE_1,
                        NVL(PHONE_2,' ') PHONE_2,
                        NVL(EMAIL,' ') EMAIL,
                        NVL(CONTACT_NAME,' ') CONTACT_NAME,
                        NVL(MON,0) MON,
                        NVL(TUE,0) TUE,
                        NVL(WED,0) WED,
                        NVL(THU,0) THU,
                        NVL(FRI,0) FRI,
                        NVL(NOTES,' ') NOTES,
                        NVL(ISDEFAULT,0) ISDEFAULT
                    FROM SUPPLIERS
                    ORDER BY S_NAME"

                Using cmd As New OracleCommand(sql, conn)
                    cmd.CommandType = CommandType.Text
                    Using dr As OracleDataReader = cmd.ExecuteReader()
                        While dr.Read()
                            lstBoxUUID.Items.Add(dr("UUID").ToString())
                            lstBoxName.Items.Add(dr("S_NAME").ToString())
                            lstBoxPhone1.Items.Add(dr("PHONE_1").ToString())
                            lstBoxPhone2.Items.Add(dr("PHONE_2").ToString())
                            lstBoxEmail.Items.Add(dr("EMAIL").ToString())
                            lstBoxContactName.Items.Add(dr("CONTACT_NAME").ToString())
                            lstBoxMon.Items.Add(Convert.ToInt32(dr("MON")))
                            lstBoxTue.Items.Add(Convert.ToInt32(dr("TUE")))
                            lstBoxWed.Items.Add(Convert.ToInt32(dr("WED")))
                            lstBoxThu.Items.Add(Convert.ToInt32(dr("THU")))
                            lstBoxFri.Items.Add(Convert.ToInt32(dr("FRI")))
                            lstBoxNotes.Items.Add(dr("NOTES").ToString())
                            lstBoxIsDefault.Items.Add(Convert.ToInt32(dr("ISDEFAULT")))
                        End While
                    End Using
                End Using
            End If
        Catch ex As Exception
            CreateExceptionFile(WhoAmI + " " + ex.Message, sql)
            MessageBox.Show(ex.Message, "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BtnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Dispose()
    End Sub

    Private Sub RdbNewSupplier_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbNewSupplier.CheckedChanged
        ResetFields()
        btnClear.Visible = True
        FillSuppliersList()
    End Sub

    Private Sub rdbExisting_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbExisting.CheckedChanged
        resetFields()
        fillSuppliersList()
        btnClear.Visible = False
        btnDeleteSupplier.Visible = True
    End Sub

    Private Sub ResetFields()
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

    Private Sub LstBoxName_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstBoxName.SelectedIndexChanged
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
            txtBoxNotes.Text = lstBoxNotes.Items.Item(index)

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

    Private Sub BtnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim WhoAmI As String = "BtnSave_Click"
        Dim sql As String = ""

        Dim mon As Integer = If(ckbMon.Checked, 1, 0)
        Dim tue As Integer = If(ckbTue.Checked, 1, 0)
        Dim wed As Integer = If(ckbWed.Checked, 1, 0)
        Dim thu As Integer = If(ckbThu.Checked, 1, 0)
        Dim fri As Integer = If(ckbFri.Checked, 1, 0)

        If Not IsNumeric(txtBoxPhone1.Text) Then
            MessageBox.Show("Το πεδίο τηλέφωνο (1) πρέπει να αποτελείται μόνο από αριθμούς")
            Exit Sub
        End If

        If txtBoxPhone2.Text.Length > 0 AndAlso
           txtBoxPhone2.Text <> " " AndAlso
           Not IsNumeric(txtBoxPhone2.Text) Then
            MessageBox.Show("Το πεδίο τηλέφωνο (2) πρέπει να αποτελείται μόνο από αριθμούς")
            Exit Sub
        End If

        If txtBoxNotes.Text = "" Then txtBoxNotes.Text = " "
        If txtBoxPhone2.Text = "" Then txtBoxPhone2.Text = " "
        If txtBoxEmail.Text = "" Then txtBoxEmail.Text = " "

        If txtBoxContactName.Text = "" OrElse txtBoxName.Text = "" OrElse txtBoxPhone1.Text = "" Then
            MessageBox.Show("Υπάρχουν κενά πεδία")
            Exit Sub
        End If

        If supplierExists(txtBoxName.Text.Replace("'", "`")) Then
            If rdbNewSupplier.Checked Then
                MessageBox.Show("Υπάρχει ήδη καταχωρημένος Προμηθευτής με αυτό το όνομα")
                Exit Sub
            End If
        End If

        Try
            If SqlLite Then
                Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                    sqliteConn.Open()
                    If rdbNewSupplier.Checked Then
                        sql =
                            "INSERT INTO SUPPLIERS
                            (
                                UUID,
                                S_NAME,
                                PHONE_1,
                                PHONE_2,
                                EMAIL,
                                CONTACT_NAME,
                                MON,
                                TUE,
                                WED,
                                THU,
                                FRI,
                                NOTES,
                                KIOSKID,
                                UPDATED_AT,
                                SYNC_STATUS
                            )
                            VALUES
                            (
                                @UUID,
                                @S_NAME,
                                @PHONE_1,
                                @PHONE_2,
                                @EMAIL,
                                @CONTACT_NAME,
                                @MON,
                                @TUE,
                                @WED,
                                @THU,
                                @FRI,
                                @NOTES,
                                @KIOSKID,
                                CURRENT_TIMESTAMP,
                                1
                            )"

                    Else
                        If lstBoxName.SelectedIndex = -1 Then
                            MessageBox.Show("Δεν έχετε επιλέξει προμηθευτή")
                            Exit Sub
                        End If

                        sql =
                            "UPDATE SUPPLIERS
                                SET
                                    S_NAME=@S_NAME,
                                    PHONE_1=@PHONE_1,
                                    PHONE_2=@PHONE_2,
                                    EMAIL=@EMAIL,
                                    CONTACT_NAME=@CONTACT_NAME,
                                    MON=@MON,
                                    TUE=@TUE,
                                    WED=@WED,
                                    THU=@THU,
                                    FRI=@FRI,
                                    NOTES=@NOTES,
                                    UPDATED_AT=CURRENT_TIMESTAMP,
                                    SYNC_STATUS=1
                             WHERE UUID=@UUID
                             AND KIOSKID=@KIOSKID"
                    End If

                    Using cmd As New SQLiteCommand(sql, sqliteConn)
                        Dim uuid As String = If(rdbNewSupplier.Checked, Guid.NewGuid().ToString("N").ToUpper(), lstBoxUUID.Items(lstBoxName.SelectedIndex).ToString())

                        cmd.Parameters.AddWithValue("@UUID", uuid)
                        cmd.Parameters.AddWithValue("@S_NAME", txtBoxName.Text.Replace("'", "`"))
                        cmd.Parameters.AddWithValue("@PHONE_1", txtBoxPhone1.Text)
                        cmd.Parameters.AddWithValue("@PHONE_2", txtBoxPhone2.Text)
                        cmd.Parameters.AddWithValue("@EMAIL", txtBoxEmail.Text.Replace("'", "`"))
                        cmd.Parameters.AddWithValue("@CONTACT_NAME", txtBoxContactName.Text.Replace("'", "`"))
                        cmd.Parameters.AddWithValue("@MON", mon)
                        cmd.Parameters.AddWithValue("@TUE", tue)
                        cmd.Parameters.AddWithValue("@WED", wed)
                        cmd.Parameters.AddWithValue("@THU", thu)
                        cmd.Parameters.AddWithValue("@FRI", fri)
                        cmd.Parameters.AddWithValue("@NOTES", txtBoxNotes.Text.Replace("'", "`"))
                        cmd.Parameters.AddWithValue("@KIOSKID", kioskId)

                        cmd.ExecuteNonQuery()
                    End Using
                End Using
            Else
                Dim cmd As New OracleCommand("", conn)

                If rdbNewSupplier.Checked Then
                    sql =
                        "INSERT INTO suppliers
                        (UUID,S_NAME,PHONE_1,PHONE_2,EMAIL,CONTACT_NAME,MON,TUE,WED,THU,FRI,NOTES)
                        VALUES
                        (
                            sys_guid(),
                            '" & txtBoxName.Text.Replace("'", "`") & "',
                            '" & txtBoxPhone1.Text & "',
                            '" & txtBoxPhone2.Text & "',
                            '" & txtBoxEmail.Text.Replace("'", "`") & "',
                            '" & txtBoxContactName.Text.Replace("'", "`") & "',
                            " & mon & ",
                            " & tue & ",
                            " & wed & ",
                            " & thu & ",
                            " & fri & ",
                            '" & txtBoxNotes.Text.Replace("'", "`") & "'
                        )"
                Else
                    sql =
                        "UPDATE suppliers SET
                            S_NAME='" & txtBoxName.Text.Replace("'", "`") & "',
                            PHONE_1='" & txtBoxPhone1.Text & "',
                            PHONE_2='" & txtBoxPhone2.Text & "',
                            EMAIL='" & txtBoxEmail.Text.Replace("'", "`") & "',
                            CONTACT_NAME='" & txtBoxContactName.Text.Replace("'", "`") & "',
                            MON=" & mon & ",
                            TUE=" & tue & ",
                            WED=" & wed & ",
                            THU=" & thu & ",
                            FRI=" & fri & ",
                            NOTES='" & txtBoxNotes.Text.Replace("'", "`") & "'
                         WHERE UUID='" & lstBoxUUID.Items(lstBoxName.SelectedIndex) & "'"
                End If
                cmd = New OracleCommand(sql, conn)
                cmd.ExecuteNonQuery()
            End If

            MessageBox.Show("Η εντολή εκτελέστηκε επιτυχώς", "Πληροφορία", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ResetFields()
            FillSuppliersList()
            rdbNewSupplier.Checked = True
        Catch ex As Exception
            CreateExceptionFile(WhoAmI + " " + ex.Message, sql)
            MessageBox.Show(ex.Message, "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BtnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        txtBoxName.Clear()
        txtBoxContactName.Clear()
        txtBoxEmail.Clear()
        txtBoxName.Clear()
        txtBoxPhone1.Clear()
        txtBoxPhone2.Clear()
    End Sub

    Private Function SupplierExists(ByVal name As String) As Boolean
        Dim WhoAmI As String = "SupplierExists"
        Dim sql As String = ""
        Dim found As Boolean = False

        Try
            If SqlLite Then
                sql =
                    "SELECT COUNT(*)
                     FROM SUPPLIERS
                     WHERE UPPER(S_NAME)=@NAME
                     AND KIOSKID=@KIOSKID"

                Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                    sqliteConn.Open()
                    Using cmd As New SQLiteCommand(sql, sqliteConn)
                        cmd.Parameters.AddWithValue("@NAME", name.ToUpper())
                        cmd.Parameters.AddWithValue("@KIOSKID", kioskId)
                        found = Convert.ToInt32(cmd.ExecuteScalar()) > 0
                    End Using
                End Using
            Else
                sql =
                    "SELECT COUNT(*)
                        FROM SUPPLIERS
                        WHERE UPPER(S_NAME)=:NAME"

                Using cmd As New OracleCommand(sql, conn)
                    cmd.BindByName = True
                    cmd.Parameters.Add("NAME", OracleDbType.Varchar2).Value = name.ToUpper()
                    found = Convert.ToInt32(cmd.ExecuteScalar()) > 0
                End Using
            End If
        Catch ex As Exception
            CreateExceptionFile(WhoAmI + " " + ex.Message, sql)
            MessageBox.Show(ex.Message, "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return found
    End Function

    Private Sub BtnDeleteSupplier_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteSupplier.Click
        Dim WhoAmI As String = "BtnDeleteSupplier_Click"
        If lstBoxName.SelectedIndex = -1 Then
            MessageBox.Show("Δεν έχετε επιλέξει προμηθευτή", "Διαγραφή Προμηθευτή", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If lstBoxIsDefault.Items(lstBoxName.SelectedIndex).ToString() = "1" Then
            MessageBox.Show("Δεν μπορείτε να διαγράψετε αυτόν τον προμηθευτή", "Διαγραφή Προμηθευτή", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim supplierUUID As String = lstBoxUUID.Items(lstBoxName.SelectedIndex).ToString()

        Try
            If SqlLite Then
                Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                    sqliteConn.Open()
                    Using trans = sqliteConn.BeginTransaction()
                        'Find default supplier
                        Dim defaultSupplier As String = Nothing

                        Using cmd As New SQLiteCommand("SELECT UUID FROM SUPPLIERS WHERE ISDEFAULT=1 AND KIOSKID=@KIOSKID", sqliteConn, trans)
                            cmd.Parameters.AddWithValue("@KIOSKID", kioskId)
                            defaultSupplier = Convert.ToString(cmd.ExecuteScalar())
                        End Using

                        'Update products
                        Using cmd As New SQLiteCommand(
                                                    "UPDATE PRODUCTS
                                                     SET SUPPLIER_ID=@DEFAULTSUPPLIER,
                                                         UPDATED_AT=CURRENT_TIMESTAMP,
                                                         SYNC_STATUS=1
                                                     WHERE SUPPLIER_ID=@SUPPLIERUUID
                                                     AND KIOSKID=@KIOSKID",
                                                    sqliteConn, trans)

                            cmd.Parameters.AddWithValue("@DEFAULTSUPPLIER", defaultSupplier)
                            cmd.Parameters.AddWithValue("@SUPPLIERUUID", supplierUUID)
                            cmd.Parameters.AddWithValue("@KIOSKID", kioskId)
                            cmd.ExecuteNonQuery()
                        End Using

                        'Delete supplier
                        Using cmd As New SQLiteCommand(
                                                    "DELETE FROM SUPPLIERS
                                                     WHERE UUID=@UUID
                                                     AND KIOSKID=@KIOSKID", sqliteConn, trans)
                            cmd.Parameters.AddWithValue("@UUID", supplierUUID)
                            cmd.Parameters.AddWithValue("@KIOSKID", kioskId)
                            cmd.ExecuteNonQuery()
                            DeletedSuppliers.Add(supplierUUID)
                        End Using
                        trans.Commit()
                    End Using
                End Using
            Else
                Using trans = conn.BeginTransaction()

                    Dim defaultSupplier As String

                    Using cmd As New OracleCommand("SELECT UUID FROM SUPPLIERS WHERE ISDEFAULT=1", conn)
                        cmd.Transaction = trans
                        defaultSupplier = Convert.ToString(cmd.ExecuteScalar())
                    End Using

                    Using cmd As New OracleCommand(
                                                "UPDATE PRODUCTS
                                                 SET SUPPLIER_ID=:DEFAULTSUPPLIER
                                                 WHERE SUPPLIER_ID=:SUPPLIERUUID",
                    conn)

                        cmd.Transaction = trans
                        cmd.BindByName = True

                        cmd.Parameters.Add("DEFAULTSUPPLIER", OracleDbType.Varchar2).Value = defaultSupplier
                        cmd.Parameters.Add("SUPPLIERUUID", OracleDbType.Varchar2).Value = supplierUUID
                        cmd.ExecuteNonQuery()
                    End Using

                    Using cmd As New OracleCommand("DELETE FROM SUPPLIERS WHERE UUID=:UUID", conn)
                        cmd.Transaction = trans
                        cmd.BindByName = True
                        cmd.Parameters.Add("UUID", OracleDbType.Varchar2).Value = supplierUUID
                        cmd.ExecuteNonQuery()
                    End Using
                    trans.Commit()
                End Using
            End If

            MessageBox.Show("Η εντολή εκτελέστηκε επιτυχώς", "Πληροφορία", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ResetFields()
            FillSuppliersList()
            rdbNewSupplier.Checked = True
        Catch ex As Exception
            CreateExceptionFile(WhoAmI + " " + ex.Message, "Delete Supplier")
            MessageBox.Show(ex.Message, "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class