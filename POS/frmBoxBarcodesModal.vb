Imports System.Data.SQLite
Imports Oracle.DataAccess.Client
Public Class frmBoxBarcodesModal
    Dim serno As Integer = -1


    Private Sub FrmBoxBarcodesModal_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        Me.Dispose()
    End Sub

    Private Sub FrmBoxBarcodesModal_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub BtnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim WhoAmI As String = "frmBoxBarcodesModal.BtnSearch_Click"
        tmpBarcodeNotFoundExit = False
        If txtBoxBarcode.Text.Length < minBarcode Then Exit Sub
        Dim sql As String = ""

        Try
            If SqlLite Then
                sql = "SELECT p.uuid,
                          p.description
                   FROM products p
                   WHERE p.uuid =
                        (
                            SELECT b.product_uuid
                            FROM barcodes b
                            WHERE UPPER(b.barcode)=@BARCODE
                            LIMIT 1
                        )
                     AND p.kioskid=@KIOSKID"

                Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                    sqliteConn.Open()
                    Using cmd As New SQLiteCommand(sql, sqliteConn)
                        cmd.Parameters.AddWithValue("@BARCODE", txtBoxBarcode.Text.ToUpper())
                        cmd.Parameters.AddWithValue("@KIOSKID", kioskId)

                        Using dr As SQLiteDataReader = cmd.ExecuteReader()
                            If dr.Read() Then
                                'Check if product already exists
                                For i As Integer = 0 To dgvProductsAndQnt.Rows.Count - 1
                                    If dgvProductsAndQnt.Rows(i).Cells("productUUID").Value.ToString() = dr("uuid").ToString() Then
                                        MessageBox.Show("Υπάρχει ήδη καταχωρηση για το: " & dr("description").ToString(), "Καταχώρηση", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                                        txtBoxBarcode.Clear()
                                        Exit Sub
                                    End If
                                Next
                                dgvProductsAndQnt.Rows.Add(dr("uuid").ToString(), dr("description").ToString(), txtBoxBarcode.Text)
                                txtBoxBarcode.Clear()
                            Else
                                tmpBarcodeNotFound = txtBoxBarcode.Text.ToUpper()
                                frmProductsModal.ShowDialog()
                                txtBoxBarcode.Clear()
                            End If
                        End Using
                    End Using
                End Using
            Else
                sql = "SELECT p.serno,
                          p.description
                   FROM products p
                   WHERE p.serno =
                        (
                            SELECT b.product_serno
                            FROM barcodes b
                            WHERE UPPER(b.barcode)='" & txtBoxBarcode.Text.ToUpper() & "'
                        )"

                Using cmd As New OracleCommand(sql, conn)
                    Using dr As OracleDataReader = cmd.ExecuteReader()
                        If dr.Read() Then
                            For i As Integer = 0 To dgvProductsAndQnt.Rows.Count - 1
                                If CInt(dgvProductsAndQnt.Rows(i).Cells("productSerno").Value) = CInt(dr(0)) Then
                                    MessageBox.Show("Υπάρχει ήδη καταχωρηση για το: " & dr(1).ToString(), "Καταχώρηση", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                                    txtBoxBarcode.Clear()
                                    Exit Sub
                                End If
                            Next
                            dgvProductsAndQnt.Rows.Add(dr(0), dr(1), txtBoxBarcode.Text)
                            txtBoxBarcode.Clear()
                        Else
                            tmpBarcodeNotFound = txtBoxBarcode.Text.ToUpper()
                            frmProductsModal.ShowDialog()
                            txtBoxBarcode.Clear()
                        End If
                    End Using
                End Using
            End If
        Catch ex As Exception
            CreateExceptionFile(WhoAmI + " " + ex.Message, sql)
            MessageBox.Show(WhoAmI + " " + ex.Message)
        End Try
    End Sub

    Private Sub BtnOverrideExisting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOverrideExisting.Click
        Dim WhoAmI As String = "frmBoxBarcodesModal.BtnOverrideExisting_Click"
        Dim sql As String = ""
        Try
            If SqlLite Then
                Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                    sqliteConn.Open()
                    'TODO: BOXBARCODES table??
                    sql = "DELETE FROM boxbarcodes
                           WHERE product_uuid=@PRODUCT_UUID
                           AND kioskid=@KIOSKID"

                    Using cmd As New SQLiteCommand(sql, sqliteConn)
                        cmd.Parameters.AddWithValue("@PRODUCT_UUID", tmpGlobalProductUUID)
                        cmd.Parameters.AddWithValue("@KIOSKID", kioskId)
                        cmd.ExecuteNonQuery()
                    End Using

                    For i As Integer = 0 To dgvProductsAndQnt.Rows.Count - 1
                        sql = "INSERT INTO boxbarcodes
                               (product_uuid, barcode, kioskid)
                               VALUES
                               (@PRODUCT_UUID, @BARCODE, @KIOSKID)"

                        Using cmd As New SQLiteCommand(sql, sqliteConn)
                            cmd.Parameters.AddWithValue("@PRODUCT_UUID", tmpGlobalProductUUID)
                            cmd.Parameters.AddWithValue("@BARCODE", dgvProductsAndQnt.Rows(i).Cells("barcode").Value.ToString())
                            cmd.Parameters.AddWithValue("@KIOSKID", kioskId)
                            cmd.ExecuteNonQuery()
                        End Using
                    Next
                End Using
            Else
                sql = "DELETE FROM boxbarcodes WHERE product_serno=" & tmpGlobalProductSerno
                Using cmd As New OracleCommand(sql, conn)
                    cmd.ExecuteNonQuery()
                End Using

                For i As Integer = 0 To dgvProductsAndQnt.Rows.Count - 1
                    sql = "INSERT INTO boxbarcodes (product_serno, barcode)
                           VALUES
                           (" & tmpGlobalProductSerno & ",
                           '" & dgvProductsAndQnt.Rows(i).Cells("barcode").Value & "')"

                    Using cmd As New OracleCommand(sql, conn)
                        cmd.ExecuteNonQuery()
                    End Using
                Next
            End If

            MessageBox.Show("Η εντολή εκτελέστηκε επιτυχώς", "Πληροφορία", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            CreateExceptionFile(WhoAmI + " " + ex.Message, sql)
            MessageBox.Show(ex.Message, "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DgvProductsAndQnt_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvProductsAndQnt.CellClick
        Dim index As Integer
        Try
            index = dgvProductsAndQnt.SelectedRows.Item(0).Index
        Catch ex As Exception
            Exit Sub
        End Try

        If MessageBox.Show(DELETE_SELECTED_LINE, DELETE_LINE, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            dgvProductsAndQnt.AllowUserToDeleteRows = True
            dgvProductsAndQnt.Rows.RemoveAt(index)
            dgvProductsAndQnt.Refresh()
            formatDataGrid()
            txtBoxBarcode.Focus()
        End If
    End Sub

    Private Sub BtnExit_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Dispose()
    End Sub

    Private Sub FormatDataGrid()
        If dgvProductsAndQnt.Rows.Count > 0 Then
            For i = 0 To dgvProductsAndQnt.Rows.Count - 1
                dgvProductsAndQnt.Rows(i).Cells("serno").Value = i + 1
            Next
            dgvProductsAndQnt.FirstDisplayedScrollingRowIndex = dgvProductsAndQnt.Rows.Count - 1
        End If
    End Sub

    Private Sub FrmBoxBarcodesModal_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim WhoAmI As String = "frmBoxBarcodesModal.FrmBoxBarcodesModal_Load"
        Dim sql As String = ""
        Try
            If SqlLite Then
                Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                    sqliteConn.Open()

                    sql =
                        "SELECT barcode
                            FROM boxbarcodes
                            WHERE product_uuid=@PRODUCT_UUID
                            AND kioskid=@KIOSKID"

                    Using cmd As New SQLiteCommand(sql, sqliteConn)
                        cmd.Parameters.AddWithValue("@PRODUCT_UUID", tmpGlobalProductUUID)
                        cmd.Parameters.AddWithValue("@KIOSKID", kioskId)

                        Using dr As SQLiteDataReader = cmd.ExecuteReader()
                            While dr.Read()
                                Dim tmpDesc As String = ""
                                sql =
                                        "SELECT description
                                            FROM products
                                            WHERE uuid =
                                            (
                                                SELECT product_uuid
                                                FROM barcodes
                                                WHERE UPPER(barcode)=@BARCODE
                                                LIMIT 1
                                            )
                                            AND kioskid=@KIOSKID"

                                Using cmd1 As New SQLiteCommand(sql, sqliteConn)
                                    cmd1.Parameters.AddWithValue("@BARCODE", dr("barcode").ToString().ToUpper())
                                    cmd1.Parameters.AddWithValue("@KIOSKID", kioskId)
                                    Using dr1 As SQLiteDataReader = cmd1.ExecuteReader()
                                        If dr1.Read() Then
                                            tmpDesc = dr1("description").ToString()
                                        End If
                                    End Using
                                End Using

                                dgvProductsAndQnt.Rows.Add(tmpGlobalProductUUID, tmpDesc, dr("barcode").ToString())
                            End While
                        End Using
                    End Using
                End Using
            Else
                sql = "SELECT barcode
                    FROM boxbarcodes
                    WHERE product_serno=" & tmpGlobalProductSerno

                Using cmd As New OracleCommand(sql, conn)
                    Using dr As OracleDataReader = cmd.ExecuteReader()
                        While dr.Read()
                            Dim tmpDesc As String = ""
                            sql =
                                "SELECT description
                                    FROM products
                                    WHERE serno =
                                    (
                                        SELECT product_serno
                                        FROM barcodes
                                        WHERE UPPER(barcode)='" & CStr(dr(0)).ToUpper() & "'
                                    )"

                            Using cmd1 As New OracleCommand(sql, conn)
                                Using dr1 As OracleDataReader = cmd1.ExecuteReader()
                                    If dr1.Read() Then
                                        tmpDesc = dr1(0).ToString()
                                    End If
                                End Using
                            End Using
                            dgvProductsAndQnt.Rows.Add(tmpGlobalProductSerno, tmpDesc, dr(0).ToString())
                        End While
                    End Using
                End Using
            End If
        Catch ex As Exception
            CreateExceptionFile(WhoAmI + " " + ex.Message, sql)
            MessageBox.Show(WhoAmI + " " + ex.Message)
        End Try
    End Sub
End Class