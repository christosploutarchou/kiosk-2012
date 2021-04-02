Imports Oracle.DataAccess.Client

Public Class frmKRONOSadmin
    Dim kronosItems As New ArrayList

    Private Sub frmKRONOSadmin_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        frmMain.Show()
    End Sub

    Private Sub frmKRONOSadmin_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub frmKRONOSadmin_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        rdbJobs.Checked = True       
    End Sub

    'Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
    '    Dim cmd As New OracleCommand("", conn)
    '    Dim dr As OracleDataReader
    '    Dim sql As String = ""
    '    dgvReports.Rows.Clear()
    '    dgvReports.Columns.Clear()
    '    Try
    '        If rdbJobs.Checked Then
    '            cmd = New OracleCommand(Q_GET_ALL_JOBS, conn)
    '            sql = Q_GET_ALL_JOBS
    '            dr = cmd.ExecuteReader()

    '            dgvReports.ColumnCount = 8

    '            dgvReports.Columns(0).Name = "LAST DATE"
    '            dgvReports.Columns(0).Width = 100

    '            dgvReports.Columns(1).Name = "LAST SEC"
    '            dgvReports.Columns(1).Width = 100

    '            dgvReports.Columns(2).Name = "NEXT DATE"
    '            dgvReports.Columns(2).Width = 100

    '            dgvReports.Columns(3).Name = "NEXT SEC"
    '            dgvReports.Columns(3).Width = 100

    '            dgvReports.Columns(4).Name = "BROKEN"
    '            dgvReports.Columns(4).Width = 100

    '            dgvReports.Columns(5).Name = "INTERVAL"
    '            dgvReports.Columns(5).Width = 100

    '            dgvReports.Columns(6).Name = "FAILURES"
    '            dgvReports.Columns(6).Width = 100

    '            dgvReports.Columns(7).Name = "WHAT"
    '            dgvReports.Columns(7).Width = 150

    '            While dr.Read()
    '                Dim row As String() = New String() {dr(0), dr(1), dr(2), dr(3), dr(4), dr(5), dr(6), dr(7)}
    '                dgvReports.Rows.Add(row)
    '            End While
    '            dr.Close()

    '        ElseIf rdbProcessed.Checked Then
    '            cmd = New OracleCommand(Q_GET_ALL_PROCESSED, conn)
    '            sql = Q_GET_ALL_PROCESSED
    '            dr = cmd.ExecuteReader()

    '            dgvReports.ColumnCount = 2

    '            dgvReports.Columns(0).Name = "FILE NAME"
    '            dgvReports.Columns(0).Width = 200

    '            dgvReports.Columns(1).Name = "PROCESSED WHEN"
    '            dgvReports.Columns(1).Width = 200

    '            While dr.Read()
    '                Dim row As String() = New String() {dr(0), dr(1)}
    '                dgvReports.Rows.Add(row)
    '            End While
    '            dr.Close()

    '        ElseIf rdbGenerated.Checked Then
    '            cmd = New OracleCommand(Q_GET_ALL_REPORTS, conn)
    '            sql = Q_GET_ALL_REPORTS
    '            dr = cmd.ExecuteReader()

    '            dgvReports.ColumnCount = 2

    '            dgvReports.Columns(0).Name = "FILE NAME"
    '            dgvReports.Columns(0).Width = 200

    '            dgvReports.Columns(1).Name = "PROCESSED WHEN"
    '            dgvReports.Columns(1).Width = 200

    '            While dr.Read()
    '                Dim row As String() = New String() {dr(0), dr(1)}
    '                dgvReports.Rows.Add(row)
    '            End While
    '            dr.Close()

    '        ElseIf rdbBarcodes.Checked Then
    '            kronosItems.Clear()
    '            cmd = New OracleCommand(Q_GET_ALL_KRONOS, conn)
    '            sql = Q_GET_ALL_KRONOS
    '            dr = cmd.ExecuteReader()

    '            dgvReports.ColumnCount = 14

    '            dgvReports.Columns(0).Name = "BARCODE"
    '            dgvReports.Columns(0).Width = 70
    '            dgvReports.Columns(1).Name = "ITEM_CODE"
    '            dgvReports.Columns(1).Width = 60
    '            dgvReports.Columns(2).Name = "ITEM_NAME"
    '            dgvReports.Columns(2).Width = 60
    '            dgvReports.Columns(3).Name = "EXTENDED_ITEM_NAME"
    '            dgvReports.Columns(3).Width = 60
    '            dgvReports.Columns(4).Name = "ISSUE_NUMBER"
    '            dgvReports.Columns(4).Width = 60
    '            dgvReports.Columns(5).Name = "DELIVERY_DATE"
    '            dgvReports.Columns(5).Width = 60
    '            dgvReports.Columns(6).Name = "PRICE"
    '            dgvReports.Columns(6).Width = 60
    '            dgvReports.Columns(7).Name = "VAT"
    '            dgvReports.Columns(7).Width = 60
    '            dgvReports.Columns(8).Name = "PRICE_1"
    '            dgvReports.Columns(8).Width = 60
    '            dgvReports.Columns(9).Name = "PRICE_2"
    '            dgvReports.Columns(9).Width = 60
    '            dgvReports.Columns(10).Name = "QUANTITY_SENT"
    '            dgvReports.Columns(10).Width = 60
    '            dgvReports.Columns(11).Name = "RETURN_DATE"
    '            dgvReports.Columns(11).Width = 60
    '            dgvReports.Columns(12).Name = "DAILY_FLAG"
    '            dgvReports.Columns(12).Width = 60
    '            dgvReports.Columns(13).Name = "SORTING_CODE"
    '            dgvReports.Columns(13).Width = 60

    '            While dr.Read()
    '                Dim objKronosItem As New KronosItem
    '                objKronosItem.barcode = dr(0)
    '                objKronosItem.itemCode = dr(1)
    '                objKronosItem.itemName = dr(2)
    '                objKronosItem.itemExtendedName = dr(3)
    '                objKronosItem.issueNumber = dr(4)
    '                objKronosItem.deliveryDate = dr(5)
    '                objKronosItem.price = dr(6)
    '                objKronosItem.vat = dr(7)
    '                objKronosItem.price1 = dr(8)
    '                objKronosItem.price2 = dr(9)
    '                objKronosItem.quantitySent = dr(10)
    '                objKronosItem.returnedDate = dr(11)
    '                objKronosItem.dailyFalg = dr(12)
    '                objKronosItem.sortingCode = dr(13)
    '                kronosItems.Add(objKronosItem)

    '                Dim row As String() = New String() {dr(0), dr(1), dr(2), dr(3), dr(4), dr(5), dr(6), dr(7), dr(8), dr(9), dr(10), dr(11), dr(12), dr(13)}
    '                dgvReports.Rows.Add(row)
    '            End While

    '            lblDummy.Text = TOTAL_BARCODES & dgvReports.Rows.Count
    '            dr.Close()
    '            txtBoxBarcode.Visible = True

    '        ElseIf rdbKronosSales.Checked Then
    '            getKronosSales()
    '            btnPrint.Visible = True
    '        End If
    '    Catch ex As Exception
    '        createExceptionFile(ex.Message, " " & sql)
    '        MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    Finally
    '        cmd.Dispose()
    '        formatDataGrid()
    '    End Try
    'End Sub

    Private Sub getKronosSales()
        Dim cmd As New OracleCommand("", conn)
        Dim dr As OracleDataReader
        Dim sql As String = ""

        Try
            Dim dateFrom As String = CStr(dtpFrom.Value.Day) & "-" & findMonth(CStr(dtpFrom.Value.Month)) & "-" & CStr(dtpFrom.Value.Year).Substring(2, 2)
            Dim dateTo As String = CStr(dtpTo.Value.Day) & "-" & findMonth(CStr(dtpTo.Value.Month)) & "-" & CStr(dtpTo.Value.Year).Substring(2, 2)

            sql = "select K_ITEM_NAME, COUNT(K_ITEM_NAME), AMOUNT, VAT from receipts_det " & _
                  "where K_ITEM_NAME is not null and created_on BETWEEN " & _
                  "to_timestamp('" & dateFrom & " 00:00:00', 'DD-MON-YY HH24:MI:SS') AND " & _
                  "to_timestamp('" & dateTo & " 23:59:59', 'DD-MON-YY HH24:MI:SS') " & _
                  "group by K_ITEM_NAME, AMOUNT, VAT"

            dgvReports.ColumnCount = 4

            dgvReports.Columns(0).Name = "Προϊόν"
            dgvReports.Columns(0).Width = 250
            dgvReports.Columns(1).Name = "Αριθμός Πωλήσεων"
            dgvReports.Columns(1).Width = 100
            dgvReports.Columns(2).Name = "Τιμή (€)"
            dgvReports.Columns(2).Width = 100
            dgvReports.Columns(3).Name = "Φ.Π.Α (%)"
            dgvReports.Columns(3).Width = 100
            cmd = New OracleCommand(sql, conn)
            dr = cmd.ExecuteReader()
            While dr.Read()
                Dim row As String() = New String() {dr(0), dr(1), dr(2), dr(3)}
                dgvReports.Rows.Add(row)
            End While
        Catch ex As Exception
            createExceptionFile(ex.Message, " " & sql)
            MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            cmd.Dispose()
            formatDataGrid()
        End Try        
    End Sub

    Private Sub dgvReports_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvReports.CellClick
        Dim index As Integer
        Try
            index = dgvReports.SelectedRows.Item(0).Index
        Catch ex As Exception
            index = -1
            Exit Sub
        End Try

        If MessageBox.Show(DELETE_SELECTED_LINE, DELETE_LINE, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            dgvReports.AllowUserToDeleteRows = True
            dgvReports.Rows.RemoveAt(index)
            dgvReports.Refresh()
        End If
        formatDataGrid()
    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Dispose()
    End Sub

    Private Sub formatDataGrid()
        If dgvReports.Rows.Count > 0 Then
            dgvReports.FirstDisplayedScrollingRowIndex = dgvReports.Rows.Count - 1
        End If
    End Sub

    Private Sub rdbJobs_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbJobs.CheckedChanged
        clearGridAndLbl()
    End Sub

    Private Sub clearGridAndLbl()
        lblDummy.Text = ""
        dgvReports.Rows.Clear()
        dgvReports.Columns.Clear()
        txtBoxBarcode.Visible = False        
    End Sub

    Private Sub rdbProcessed_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbProcessed.CheckedChanged
        clearGridAndLbl()
    End Sub

    Private Sub rdbGenerated_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbGenerated.CheckedChanged
        clearGridAndLbl()
    End Sub

    Private Sub rdbBarcodes_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbBarcodes.CheckedChanged
        clearGridAndLbl()
    End Sub

    Protected Overrides ReadOnly Property CreateParams() As CreateParams
        'Disables X button
        Get
            Dim param As CreateParams = MyBase.CreateParams
            param.ClassStyle = param.ClassStyle Or &H200
            Return param
        End Get
    End Property

    'Private Sub txtBoxBarcode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBoxBarcode.TextChanged
    '    dgvReports.Rows.Clear()
    '    If txtBoxBarcode.Text = "" Or txtBoxBarcode.Text = String.Empty Then
    '        For Each tmpKronosItem In kronosItems
    '            addKronosItem(tmpKronosItem)
    '        Next
    '    Else
    '        For Each tmpKronosItem In kronosItems
    '            If tmpKronosItem.barcode.ToString.StartsWith(txtBoxBarcode.Text) Then
    '                addKronosItem(tmpKronosItem)
    '            End If
    '        Next
    '    End If
    '    lblDummy.Text = TOTAL_BARCODES & dgvReports.Rows.Count
    'End Sub

    'Private Sub addKronosItem(ByVal tmpKronosItem As KronosItem)
    '    Dim row As String() = New String() {tmpKronosItem.barcode, tmpKronosItem.itemCode, tmpKronosItem.itemName, _
    '                                        tmpKronosItem.itemExtendedName, tmpKronosItem.issueNumber, tmpKronosItem.deliveryDate, _
    '                                        tmpKronosItem.price, tmpKronosItem.vat, tmpKronosItem.price1, _
    '                                        tmpKronosItem.price2, tmpKronosItem.quantitySent, tmpKronosItem.returnedDate, _
    '                                        tmpKronosItem.dailyFalg, tmpKronosItem.sortingCode}
    '    dgvReports.Rows.Add(row)
    'End Sub

    Private Sub rdbKronosSales_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbKronosSales.CheckedChanged
        If rdbKronosSales.Checked Then
            lblFromDate.Visible = True
            lblToDate.Visible = True
            dtpFrom.Visible = True
            dtpTo.Visible = True
        Else
            lblFromDate.Visible = False
            lblToDate.Visible = False
            dtpFrom.Visible = False
            dtpTo.Visible = False
            btnPrint.Visible = False
        End If
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        PrintDocument1.PrinterSettings.Copies = 1
        PrintDocument1.Print()
    End Sub

    Private Sub PrintDocument1_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        Dim headerFont As Font = New Drawing.Font(REPORT_FONT, 15, FontStyle.Bold)
        Dim reportFont As Font = New Drawing.Font(REPORT_FONT, 9)

        e.Graphics.DrawString(KIOSK_NAME, headerFont, Brushes.Black, 65, 0)
        e.Graphics.DrawString(COMPANY_NAME, reportFont, Brushes.Black, 95, 35)
        e.Graphics.DrawString(KIOSK_ADDRESS1, reportFont, Brushes.Black, 75, 50)
        e.Graphics.DrawString(KIOSK_ADDRESS2, reportFont, Brushes.Black, 95, 65)
        e.Graphics.DrawString(COMPANY_VAT, reportFont, Brushes.Black, 60, 80)
        e.Graphics.DrawString(SINGE_DASHED_LINE, reportFont, Brushes.Black, 0, 95)

        Dim xMargin As Integer = 120

        Dim headerFontBold As Font = New Drawing.Font(REPORT_FONT, 12, FontStyle.Bold)
        e.Graphics.DrawString("Πωλήσεις ΚΡΟΝΟΥ", headerFontBold, Brushes.Black, 0, xMargin)
        xMargin += 20
        e.Graphics.DrawString("Από: " & dtpFrom.Value.ToString, reportFont, Brushes.Black, 0, xMargin)
        xMargin += 20
        e.Graphics.DrawString("Έως: " & dtpTo.Value.ToString, reportFont, Brushes.Black, 0, xMargin)
        xMargin += 20
        e.Graphics.DrawString(SINGE_DASHED_LINE, reportFont, Brushes.Black, 0, xMargin)

        For i = 0 To dgvReports.Rows.Count - 1
            xMargin += 20
            e.Graphics.DrawString("Προϊόν: " & dgvReports.Rows(i).Cells("Προϊόν").Value, reportFont, Brushes.Black, 0, xMargin)
            xMargin += 20
            e.Graphics.DrawString("Αριθμός Πωλήσεων: " & dgvReports.Rows(i).Cells("Αριθμός Πωλήσεων").Value, reportFont, Brushes.Black, 0, xMargin)
            xMargin += 20
            e.Graphics.DrawString("Τιμή (€): " & dgvReports.Rows(i).Cells("Τιμή (€)").Value, reportFont, Brushes.Black, 0, xMargin)
            xMargin += 20
            e.Graphics.DrawString("Φ.Π.Α (%): " & dgvReports.Rows(i).Cells("Φ.Π.Α (%)").Value, reportFont, Brushes.Black, 0, xMargin)
            xMargin += 20
            e.Graphics.DrawString(SINGE_DASHED_LINE, reportFont, Brushes.Black, 0, xMargin)
            xMargin += 20
        Next
    End Sub
End Class