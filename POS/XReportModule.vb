Imports System.Data.SQLite
Imports Oracle.DataAccess.Client

Module XReportModule
    Public Class XReport
        Public Sub InsertXReport(userid As String, totals As ReceiptTotals, totalPayments As Decimal, amountLaxeia As Decimal, amountVisa As Decimal, finalLaxeia As Decimal)
            Dim WhoAmI As String = "XReportModule.InsertXReport"
            Dim sql As String = ""
            Try
                sql =
                        "INSERT INTO X_REPORT
                    (
                        USER_ID,
                        FROM_DATE,
                        TO_DATE,
                        TOTAL_RECEIPTS,
                        TOTAL_AMT,
                        TOTAL0PERCENT,
                        TOTAL3PERCENT,
                        TOTAL5PERCENT,
                        TOTAL19PERCENT,
                        INITIAL_AMT,
                        FINAL_AMT,
                        PAYMENTS,
                        CREATED_ON,
                        DESCRIPTION,
                        AMOUNT_LAXEIA,
                        INITIALAMTLAXEIA,
                        AMOUNTVISA,
                        FINALAMTLAXEIA,
                        KIOSKID,
                        UPDATED_AT,
                        SYNC_STATUS
                    )
                    VALUES
                    (
                        @USER_ID,
                        (SELECT MAX(LOGIN_WHEN)
                           FROM SESSIONS
                          WHERE USER_ID=@USER_ID
                            AND KIOSKID=@KIOSKID),
                        CURRENT_TIMESTAMP,
                        @TOTAL_RECEIPTS,
                        @TOTAL_AMT,
                        @TOTAL0,
                        @TOTAL3,
                        @TOTAL5,
                        @TOTAL19,
                        (SELECT PARAMVALUE
                           FROM GLOBAL_PARAMS
                          WHERE PARAMKEY='init.fiscal.amt'),
                        @FINAL_AMT,
                        @PAYMENTS,
                        CURRENT_TIMESTAMP,
                        '',
                        @AMOUNT_LAXEIA,
                        (SELECT AMOUNTLAXEIAONLOGIN
                           FROM SESSIONS
                          WHERE USER_ID=@USER_ID
                            AND KIOSKID=@KIOSKID
                          ORDER BY LOGIN_WHEN DESC
                          LIMIT 1),
                        @AMOUNT_VISA,
                        @FINAL_LAXEIA,
                        @KIOSKID,
                        CURRENT_TIMESTAMP,
                        1
                    )"

                Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                    sqliteConn.Open()
                    Using cmd As New SQLiteCommand(sql, sqliteConn)
                        cmd.Parameters.AddWithValue("@USER_ID", userid)
                        cmd.Parameters.AddWithValue("@TOTAL_RECEIPTS", totals.TotalReceipts)
                        cmd.Parameters.AddWithValue("@TOTAL_AMT", totals.TotalAmt)
                        cmd.Parameters.AddWithValue("@TOTAL0", totals.TotalVat0)
                        cmd.Parameters.AddWithValue("@TOTAL3", totals.TotalVat3)
                        cmd.Parameters.AddWithValue("@TOTAL5", totals.TotalVat5)
                        cmd.Parameters.AddWithValue("@TOTAL19", totals.TotalVat19)
                        cmd.Parameters.AddWithValue("@FINAL_AMT", totals.TotalAmt)
                        cmd.Parameters.AddWithValue("@PAYMENTS", totalPayments)
                        cmd.Parameters.AddWithValue("@AMOUNT_LAXEIA", amountLaxeia)
                        cmd.Parameters.AddWithValue("@AMOUNT_VISA", amountVisa)
                        cmd.Parameters.AddWithValue("@FINAL_LAXEIA", finalLaxeia)
                        cmd.Parameters.AddWithValue("@KIOSKID", kioskId)
                        cmd.ExecuteNonQuery()
                    End Using
                End Using
            Catch ex As Exception
                CreateExceptionFile(WhoAmI & ": " & ex.Message, sql)
                MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub
    End Class
End Module
