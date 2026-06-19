Imports System.Data.SQLite
Imports Oracle.DataAccess.Client
Public Class frmLottery
    Dim tableIndex As String = ""

    Private Sub BtnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Dispose()
    End Sub

    Private Sub FrmNewProduct_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        frmMain.Show()
    End Sub

    Private Sub FrmNewProduct_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub BtnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim WhoAmI As String = "BtnSave_Click"
        If dgvLinkedProducts.Rows.Count = 0 Then
            MessageBox.Show("Δεν έχετε συνδέσει προιόν(τα) με τα λαχεία",
                        "Σφάλμα",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim sql As String = ""
        Try
            If SqlLite Then
                Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                    sqliteConn.Open()
                    Using trans = sqliteConn.BeginTransaction()
                        'Remove existing lottery mappings for this kiosk
                        sql = "DELETE FROM LOTTERY WHERE KIOSKID=@KIOSKID"

                        Using cmd As New SQLiteCommand(sql, sqliteConn, trans)
                            cmd.Parameters.AddWithValue("@KIOSKID", kioskId)
                            cmd.ExecuteNonQuery()
                        End Using

                        'Insert new mappings
                        sql =
                            "
                            INSERT INTO LOTTERY
                            (
                                BARCODE,
                                KIOSKID,
                                UPDATED_AT,
                                SYNC_STATUS
                            )
                            VALUES
                            (
                                @BARCODE,
                                @KIOSKID,
                                CURRENT_TIMESTAMP,
                                1
                            )
                            "

                        Using cmd As New SQLiteCommand(sql, sqliteConn, trans)
                            cmd.Parameters.Add("@BARCODE", DbType.String)
                            cmd.Parameters.Add("@KIOSKID", DbType.String)
                            For Each row As DataGridViewRow In dgvLinkedProducts.Rows
                                If row.IsNewRow Then Continue For
                                cmd.Parameters("@BARCODE").Value =
                                row.Cells("barcode").Value.ToString()
                                cmd.Parameters("@KIOSKID").Value = kioskId
                                cmd.ExecuteNonQuery()
                            Next
                        End Using
                        trans.Commit()
                    End Using
                End Using
            Else
                sql = "DELETE FROM LOTTERY"

                Using cmd As New OracleCommand(sql, conn)
                    cmd.ExecuteNonQuery()
                End Using

                sql =
                    "
                    INSERT INTO LOTTERY
                    (
                        BARCODE
                    )
                    VALUES
                    (
                        :BARCODE
                    )
                    "

                Using cmd As New OracleCommand(sql, conn)
                    cmd.BindByName = True
                    cmd.Parameters.Add("BARCODE", OracleDbType.Varchar2)
                    For Each row As DataGridViewRow In dgvLinkedProducts.Rows
                        If row.IsNewRow Then Continue For
                        cmd.Parameters("BARCODE").Value =
                        row.Cells("barcode").Value.ToString()
                        cmd.ExecuteNonQuery()
                    Next
                End Using
            End If

            CalculateAmount()

            MessageBox.Show("Η εντολή εκτελέστηκε επιτυχώς", "Πληροφορία", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Close()
        Catch ex As Exception
            CreateExceptionFile(WhoAmI + " " + ex.Message, sql)
            MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CalculateAmount()
        Dim totalAmt As Integer = 0
        For i = 0 To dgvLinkedProducts.Rows.Count - 1
            totalAmt += (dgvLinkedProducts.Rows(i).Cells("sellAmt").Value) * (dgvLinkedProducts.Rows(i).Cells("availQnt").Value)
        Next
        txtBoxLotteryAmt.Text = totalAmt.ToString + " ευρώ"
    End Sub

    Private Sub LnkLblBarcodes_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkLblBarcodes.LinkClicked
        Dim newBarcode As Object
        newBarcode = InputBox("Εισαγωγή νέου Barcode", "Νέο Barcode", "")
        If newBarcode Is "" Then
            Exit Sub
        Else
            addInGrid(newBarcode)
        End If
        txtBoxLotteryAmt.Focus()
    End Sub

    Private Sub AddInGrid(ByVal newBarcode As String)
        Dim WhoAmI As String = "AddInGrid"
        Dim sql As String = ""

        Try
            If SqlLite Then
                sql =
                    "SELECT
                        p.UUID,
                        b.BARCODE,
                        p.DESCRIPTION,
                        p.SELL_AMT,
                        p.AVAIL_QUANTITY
                     FROM BARCODES b
                     INNER JOIN PRODUCTS p
                         ON p.UUID = b.PRODUCT_UUID
                     WHERE UPPER(b.BARCODE)=@BARCODE
                       AND b.KIOSKID=@KIOSKID"

                Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                    sqliteConn.Open()

                    Using cmd As New SQLiteCommand(sql, sqliteConn)
                        cmd.Parameters.AddWithValue("@BARCODE", newBarcode.ToUpper())
                        cmd.Parameters.AddWithValue("@KIOSKID", kioskId)

                        Using dr As SQLiteDataReader = cmd.ExecuteReader()
                            If dr.Read() Then
                                For Each row As DataGridViewRow In dgvLinkedProducts.Rows
                                    If row.IsNewRow Then Continue For
                                    If row.Cells("productSerno").Value.ToString() = dr("UUID").ToString() Then
                                        MessageBox.Show("Το προϊόν είναι ήδη συνδεμένο με του κουμπί", "Καταχώρηση Barcode/Προϊόντος", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                                        Exit Sub
                                    End If
                                Next

                                dgvLinkedProducts.Rows.Add(
                                dr("UUID").ToString(),
                                dr("BARCODE").ToString(),
                                dr("DESCRIPTION").ToString(),
                                Convert.ToDouble(dr("SELL_AMT")),
                                Convert.ToInt32(dr("AVAIL_QUANTITY")))

                                CalculateAmount()

                            Else
                                MessageBox.Show("Το barcode δεν είναι καταχωρημένο στην διαχείρηση προϊόντων", "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            End If
                        End Using
                    End Using
                End Using
            Else
                sql =
                    "SELECT
                        b.PRODUCT_SERNO,
                        b.BARCODE,
                        p.DESCRIPTION,
                        p.SELL_AMT,
                        p.AVAIL_QUANTITY
                     FROM BARCODES b
                     INNER JOIN PRODUCTS p
                         ON p.SERNO=b.PRODUCT_SERNO
                     WHERE UPPER(b.BARCODE)=:BARCODE"

                Using cmd As New OracleCommand(sql, conn)

                    cmd.BindByName = True
                    cmd.Parameters.Add("BARCODE", OracleDbType.Varchar2).Value = newBarcode.ToUpper()

                    Using dr As OracleDataReader = cmd.ExecuteReader()
                        If dr.Read() Then
                            For Each row As DataGridViewRow In dgvLinkedProducts.Rows
                                If row.IsNewRow Then Continue For
                                If row.Cells("productSerno").Value.ToString() = dr("PRODUCT_SERNO").ToString() Then
                                    MessageBox.Show("Το προϊόν είναι ήδη συνδεμένο με του κουμπί", "Καταχώρηση Barcode/Προϊόντος", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                                    Exit Sub
                                End If
                            Next

                            dgvLinkedProducts.Rows.Add(
                            dr("PRODUCT_SERNO"),
                            dr("BARCODE"),
                            dr("DESCRIPTION"),
                            Convert.ToDouble(dr("SELL_AMT")),
                            Convert.ToInt32(dr("AVAIL_QUANTITY")))

                            CalculateAmount()

                        Else
                            MessageBox.Show("Το barcode δεν είναι καταχωρημένο στην διαχείρηση προϊόντων", "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End If
                    End Using
                End Using
            End If
        Catch ex As Exception
            CreateExceptionFile(WhoAmI + " " + ex.Message, sql)
            MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub TxtBoxSearchBox_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBoxLotteryAmt.MouseEnter
        txtBoxLotteryAmt.BackColor = Color.Bisque
    End Sub

    Private Sub TxtBoxSearchBox_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBoxLotteryAmt.MouseLeave
        txtBoxLotteryAmt.BackColor = Color.LemonChiffon
    End Sub

    Private Sub FrmLottery_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadLotteryDetails()
        CalculateAmount()
    End Sub

    Private Sub LoadLotteryDetails()
        Dim WhoAmI As String = "LoadLotteryDetails"
        Dim sql As String = ""
        dgvLinkedProducts.Rows.Clear()
        txtBoxLotteryAmt.Clear()

        Try
            If SqlLite Then
                sql =
                    "
                    SELECT
                        p.UUID,
                        l.BARCODE,
                        p.DESCRIPTION,
                        p.SELL_AMT,
                        p.AVAIL_QUANTITY
                    FROM LOTTERY l
                    INNER JOIN BARCODES b
                        ON b.BARCODE = l.BARCODE
                    INNER JOIN PRODUCTS p
                        ON p.UUID = b.PRODUCT_UUID
                    WHERE l.KIOSKID = @KIOSKID
                    ORDER BY p.DESCRIPTION
                    "

                Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                    sqliteConn.Open()
                    Using cmd As New SQLiteCommand(sql, sqliteConn)
                        cmd.Parameters.AddWithValue("@KIOSKID", kioskId)
                        Using dr As SQLiteDataReader = cmd.ExecuteReader()
                            While dr.Read()
                                dgvLinkedProducts.Rows.Add(
                                dr("UUID").ToString(),
                                dr("BARCODE").ToString(),
                                dr("DESCRIPTION").ToString(),
                                dr("SELL_AMT"),
                                dr("AVAIL_QUANTITY"))
                            End While
                        End Using
                    End Using
                End Using
            Else
                sql =
                    "
                    SELECT
                        b.PRODUCT_SERNO,
                        l.BARCODE,
                        p.DESCRIPTION,
                        p.SELL_AMT,
                        p.AVAIL_QUANTITY
                    FROM LOTTERY l
                    INNER JOIN BARCODES b
                        ON b.BARCODE = l.BARCODE
                    INNER JOIN PRODUCTS p
                        ON p.SERNO = b.PRODUCT_SERNO
                    "

                Using cmd As New OracleCommand(sql, conn)
                    cmd.CommandType = CommandType.Text
                    Using dr As OracleDataReader = cmd.ExecuteReader()
                        While dr.Read()
                            dgvLinkedProducts.Rows.Add(
                            dr("PRODUCT_SERNO"),
                            dr("BARCODE"),
                            dr("DESCRIPTION"),
                            dr("SELL_AMT"),
                            dr("AVAIL_QUANTITY"))
                        End While
                    End Using
                End Using
            End If
        Catch ex As Exception
            CreateExceptionFile(WhoAmI + " " + ex.Message, sql)
            MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DgvLinkedProducts_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvLinkedProducts.CellClick
        Dim index As Integer
        Try
            index = dgvLinkedProducts.SelectedRows.Item(0).Index
        Catch ex As Exception
            Exit Sub
        End Try

        If MessageBox.Show(DELETE_SELECTED_LINE, DELETE_LINE, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            dgvLinkedProducts.Rows.RemoveAt(index)
            dgvLinkedProducts.Refresh()
            CalculateAmount()
        End If
    End Sub
End Class