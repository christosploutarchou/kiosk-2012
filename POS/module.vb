Imports System.Data.SQLite
Imports System.Globalization
Imports System.IO
Imports System.Net
Imports System.Net.Http
Imports System.Net.Sockets
Imports System.Text
Imports System.Threading.Tasks
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types
Imports POS.CIActivateModels
Imports SQLitePCL

Module connectionModule

    Public WithEvents SyncTimer As New System.Windows.Forms.Timer()
    Public SyncRunning As Boolean = False

    Public DeletedSuppliers As New ArrayList

    Public oradb As String

    'Button selected to be edited
    Public currentBtnPosEdit As String
    Public currentBtnPos As String

    'Product serno for boxes
    Public tmpGlobalProductSerno As Integer

    'Product Selected on dynamic form to be added in the DGV
    Public dynamicProductSerno As Integer = -1
    Public dynamicProductDesc As String
    Public dynamicProductVAT As Integer
    Public dynamicProductPrice As Double
    Public dynamicProductIsBox As Integer
    Public dynamicProductBoxQnt As Integer

    Public username As String
    Public whois As String
    Public isAdmin As Boolean
    Public canViewReports As Boolean
    Public canEditProducts As Boolean
    Public canEditProductsFull As Boolean
    Public computerName As String
    Public divideFactor0 As Double = 1
    Public divideFactor3 As Double = 1
    Public divideFactor5 As Double = 1
    Public divideFactor19 As Double = 1
    Public dualMonitor As Boolean = False
    Public minBarcode As Integer
    Public tmpPaymentVAT As Integer = -1

    Public startDate As Date
    Public tmpTrxn As New tmpTransaction
    Public conn As New OracleConnection()

    Public tmpBarcodeNotFound As String
    Public tmpBarcodeNotFoundExit As Boolean

    Public Sub StartSyncService()
        SyncTimer.Interval = 300000   '5 minutes
        SyncTimer.Start()
    End Sub

    Public Sub StopSyncService()
        SyncTimer.Stop()
    End Sub

    Private Sub SyncTimer_Tick(sender As Object, e As EventArgs) Handles SyncTimer.Tick
        Dim WhoAmI As String = "SyncTimer_Tick"
        If SyncRunning Then
            Exit Sub
        End If
        SyncRunning = True
        Try
            If Not isConnOpen() Then
                Exit Sub
            End If
            Dim sync As New SyncTables()

            sync.UploadGlobalParams()
            sync.UploadCategories()
            sync.UploadSuppliers()
            sync.UploadLottery()
            sync.UploadSessions()
            sync.UploadPayments()
            sync.UploadReceipts()
            sync.UploadReceiptsDet()
            sync.UploadXReport()
            sync.UploadZReport()
            sync.UploadProducts()
            sync.UploadUsers()
            sync.UploadInvoices()
            sync.UploadInvoicesDet()
        Catch ex As Exception
            CreateExceptionFile(WhoAmI + " " + ex.ToString(), "SyncTimer")
        Finally
            SyncRunning = False
        End Try
    End Sub

    Public Sub GetVat3PercentColumns()
        Dim cmd As New OracleCommand("", conn)
        Dim dr As OracleDataReader
        Dim sql As String = ""
        Try
            'VAT_TYPES
            sql = "select count(*) from vat_types where vat=3 "
            If SqlLite Then
                sql += " AND KIOSKID = :KIOSKID"
            End If
            cmd = New OracleCommand(sql, conn)
            cmd.CommandType = CommandType.Text
            dr = cmd.ExecuteReader()
            If dr.Read Then
                If (CInt(dr(0)) = 0) Then
                    sql = "insert into vat_types (uuid, description, vat) values (sys_guid(), 'V.A.T 3%', 3)"
                    cmd = New OracleCommand(sql, conn)
                    Using cmd
                        cmd.ExecuteNonQuery()
                    End Using
                End If
            End If

            'RECEIPTS
            sql = "select COUNT(*) from ALL_TAB_COLUMNS " &
                  "where TABLE_NAME = 'RECEIPTS' " &
                  "AND COLUMN_NAME = 'TOTAL_VAT3'"

            cmd = New OracleCommand(sql, conn)
            cmd.CommandType = CommandType.Text
            dr = cmd.ExecuteReader()
            If dr.Read Then
                If (CInt(dr(0)) = 0) Then
                    sql = "alter table RECEIPTS add TOTAL_VAT3 Number(10,2)"
                    cmd = New OracleCommand(sql, conn)
                    cmd.ExecuteReader()
                End If
            End If

            'X_REPORT
            sql = "select COUNT(*) from ALL_TAB_COLUMNS " &
                  "where TABLE_NAME = 'X_REPORT' " &
                  "AND COLUMN_NAME = 'TOTAL3PERCENT'"
            cmd = New OracleCommand(sql, conn)
            cmd.CommandType = CommandType.Text
            dr = cmd.ExecuteReader()
            If dr.Read Then
                If (CInt(dr(0)) = 0) Then
                    sql = "alter table X_REPORT add TOTAL3PERCENT Number(10,2)"
                    cmd = New OracleCommand(sql, conn)
                    cmd.ExecuteReader()
                End If
            End If

            'Z_REPORT
            sql = "select COUNT(*) from ALL_TAB_COLUMNS " &
                  "where TABLE_NAME = 'Z_REPORT' " &
                  "AND COLUMN_NAME = 'TOTAL_AMOUNT3'"
            cmd = New OracleCommand(sql, conn)
            cmd.CommandType = CommandType.Text
            dr = cmd.ExecuteReader()
            If dr.Read Then
                If (CInt(dr(0)) = 0) Then
                    sql = "alter table Z_REPORT add TOTAL_AMOUNT3 Number(10,2)"
                    cmd = New OracleCommand(sql, conn)
                    cmd.ExecuteReader()
                End If
            End If
            dr.Close()
        Catch ex As Exception
            CreateExceptionFile(ex.Message, " " & sql)
            MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            cmd.Dispose()
        End Try
    End Sub
    Public Function TruncateDecimal(value As Decimal, precision As Integer) As Decimal
        If precision < 0 Then
            Throw New ArgumentOutOfRangeException(NameOf(precision), "Precision must be zero or positive.")
        End If

        Dim factor As Decimal = CDec(Math.Pow(10, precision))
        Return Math.Truncate(value * factor) / factor
    End Function

    'Public Function CheckHhdSerial() As Boolean
    'Dim DriveSerial As Integer
    'Dim fso As Object = CreateObject("Scripting.FileSystemObject")
    'Dim Drv As Object = fso.GetDrive(fso.GetDriveName(Application.StartupPath))
    'With Drv
    'If .IsReady Then
    '           DriveSerial = .SerialNumber
    'Else    '"Drive Not Ready!"
    '           DriveSerial = -1
    'End If
    'End With

    'Dim FILE_NAME As String = "C:\Windows\sid.txt"
    'Dim serialFromFile As String
    'If System.IO.File.Exists(FILE_NAME) = True Then
    'Dim objReader As New System.IO.StreamReader(FILE_NAME)
    '       serialFromFile = objReader.ReadToEnd
    '      objReader.Close()
    'If Not DriveSerial.ToString("X2").Equals(serialFromFile) Then
    '           MessageBox.Show("You are not allowed to use this program", "Invalid License", MessageBoxButtons.OK, MessageBoxIcon.Error)
    'Return False
    'End If
    'Else
    'Dim fs As FileStream = File.Create(FILE_NAME)
    'Dim info As Byte() = New UTF8Encoding(True).GetBytes(DriveSerial.ToString("X2"))
    '       fs.Write(info, 0, info.Length)
    '      File.SetAttributes(FILE_NAME, FileAttributes.Hidden)
    '     fs.Close()
    'End If
    'Return True
    'End Function

    Public Function OpenConn() As Boolean
        Dim dbHostName As String
        dbHostName = GetDbHostName()
        If dbHostName = "" Then
            MessageBox.Show(DB_NOT_ACCESSIBLE, ERROR_MSG, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Function
        End If
        oradb = "Data Source=(DESCRIPTION=" _
           + "(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=" + dbHostName + ")(PORT=1521)))" _
           + "(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=ORCL)));" _
           + "User Id=KIOSK;Password=oracle;"
        Try
            conn.ConnectionString = oradb
            conn.Open()
            'GetMinBarcodeLength()
            'GetStartDate()
            Return True
        Catch ex As Exception
            CreateExceptionFile(ex.Message, "")
            If ex.Message.Substring(0, 9).Equals("ORA-12541") Then
                If dbHostName.Equals("localhost") Then
                    MessageBox.Show("Σφάλμα με την βάση δεδομένων. Πατήστε OK και περιμένετε 45 δευτερόλεπτα", APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Shell("lsnrctl start", AppWinStyle.NormalFocus, True)
                    Threading.Thread.Sleep(45000)
                    OpenConn()
                    Return True
                Else
                    MessageBox.Show("Σφάλμα με την βάση δεδομένων", APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return False
                End If
            Else
                MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            Return False
        End Try
    End Function

    Public Sub CloseConn()
        If Not SqlLite Or (conn IsNot Nothing AndAlso conn.State = ConnectionState.Open) Then
            conn.Close()
        End If
    End Sub

    Private Function GetDbHostName() As String
        Const FILE_NAME As String = "C:\dbparams.txt"

        If Not IO.File.Exists(FILE_NAME) Then
            MessageBox.Show(READ_DB_PARAMS, ERROR_MSG, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return String.Empty
        End If

        Try
            Using reader As New IO.StreamReader(FILE_NAME)
                ' Read the first non-empty line and trim spaces
                Dim line As String
                Do
                    line = reader.ReadLine()
                    If line Is Nothing Then Exit Do
                    line = line.Trim()
                Loop While String.IsNullOrEmpty(line)

                Return If(String.IsNullOrEmpty(line), String.Empty, line)
            End Using
        Catch ex As Exception
            MessageBox.Show("Error reading DB parameters: " & ex.Message, ERROR_MSG, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return String.Empty
        End Try
    End Function

    Public Sub GetParams()
        Const FILE_NAME As String = "C:\params.txt"

        If Not IO.File.Exists(FILE_NAME) Then
            MessageBox.Show(READ_PARAMS, ERROR_MSG, MessageBoxButtons.OK, MessageBoxIcon.Error)
            computerName = "-1"
            Return
        End If

        Try
            Using reader As New IO.StreamReader(FILE_NAME)
                Dim content As String = reader.ReadToEnd()
                Dim params() As String = content.Split("~"c)

                If params.Length < 5 Then
                    MessageBox.Show("Το αρχείο παραμέτρων είναι κατεστραμμένο ή ελλιπές.", ERROR_MSG, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    computerName = "-1"
                    Return
                End If

                computerName = params(0).Trim()

                ' Parse numeric divide factors safely
                divideFactor0 = ParseDecimal(params(1))
                divideFactor5 = ParseDecimal(params(2))
                divideFactor19 = ParseDecimal(params(3))

                ' Parse boolean
                dualMonitor = params(4).Trim() = "1"

            End Using

        Catch ex As Exception
            MessageBox.Show("Σφάλμα κατά την ανάγνωση του αρχείου παραμέτρων: " & ex.Message, ERROR_MSG, MessageBoxButtons.OK, MessageBoxIcon.Error)
            computerName = "-1"
        End Try
    End Sub

    Private Function ParseDecimal(value As String) As Decimal
        value = value.Trim().Replace(",", ".")
        Dim result As Decimal
        If Decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, result) Then
            Return result
        Else
            Return 0D ' fallback default if parsing fails
        End If
    End Function
    Public Sub GetKIOSKParams()
        Dim WhoAmI As String = "GetKIOSKParams"
        Dim sql As String =
            "SELECT paramkey, paramvalue " &
            "FROM global_params " &
            "WHERE paramkey IN ('login.title1', 'login.title2', 'kiosk.name', 'company.name', " &
            "'kiosk.address1', 'kiosk.address2', 'company.vat', " &
            "'glory.enabled','glory.ip','glory.device.name','glory.admin.username'," &
            "'glory.admin.password','glory.local.ip') "

        If SqlLite Then
            sql &= " AND kioskid = @KIOSKID"
        End If

        Dim paramMap As New Dictionary(Of String, Action(Of String)) From {
            {"login.title1", Sub(val) LOGIN_TITLE1 = val},
            {"login.title2", Sub(val) LOGIN_TITLE2 = val},
            {"kiosk.name", Sub(val) KIOSK_NAME = val},
            {"company.name", Sub(val) COMPANY_NAME = val},
            {"kiosk.address1", Sub(val) KIOSK_ADDRESS1 = val},
            {"kiosk.address2", Sub(val) KIOSK_ADDRESS2 = val},
            {"company.vat", Sub(val) COMPANY_VAT = val},
            {"glory.enabled", Sub(val) GLORY_ENABLED = val},
            {"glory.ip", Sub(val) GLORY_IP = val},
            {"glory.device.name", Sub(val) GLORY_DEVICE_NAME = val},
            {"glory.admin.username", Sub(val) GLORY_ADMIN_USERNAME = val},
            {"glory.admin.password", Sub(val) GLORY_ADMIN_PASSWORD = val},
            {"glory.local.ip", Sub(val) GLORY_LOCAL_IP = val}
        }

        Try
            If SqlLite Then
                Using conn As New SQLiteConnection("Data Source=kiosk.db")
                    conn.Open()
                    Using cmd As New SQLiteCommand(sql, conn)
                        cmd.Parameters.AddWithValue("@KIOSKID", kioskId)
                        Using dr As SQLiteDataReader = cmd.ExecuteReader()
                            While dr.Read()
                                Dim key As String = dr("paramkey").ToString()
                                Dim value As String = If(IsDBNull(dr("paramvalue")), "", dr("paramvalue").ToString())
                                If paramMap.ContainsKey(key) Then
                                    paramMap(key)(value)
                                End If
                            End While
                        End Using
                    End Using
                End Using
            Else
                Using cmd As New OracleCommand(sql, conn)
                    cmd.CommandType = CommandType.Text
                    Using dr As OracleDataReader = cmd.ExecuteReader()
                        While dr.Read()
                            Dim key As String = dr("paramkey").ToString()
                            Dim value As String = If(IsDBNull(dr("paramvalue")), "", dr("paramvalue").ToString())
                            If paramMap.ContainsKey(key) Then
                                paramMap(key)(value)
                            End If
                        End While
                    End Using
                End Using
            End If
        Catch ex As Exception
            CreateExceptionFile(ex.ToString(), sql)
            MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private ReadOnly MonthMap As New Dictionary(Of Integer, String) From {
    {1, "JAN"}, {2, "FEB"}, {3, "MAR"}, {4, "APR"},
    {5, "MAY"}, {6, "JUN"}, {7, "JUL"}, {8, "AUG"},
    {9, "SEP"}, {10, "OCT"}, {11, "NOV"}, {12, "DEC"}
}

    Public Function FindMonth(ByVal month As String) As String
        Dim m As Integer
        If Integer.TryParse(month, m) AndAlso MonthMap.ContainsKey(m) Then
            Return MonthMap(m)
        End If
        Return ""
    End Function

    Public Structure ReceiptTotals
        Public TotalReceipts As Integer
        Public TotalAmt As Double
        Public TotalVat0 As Double
        Public TotalVat3 As Double
        Public TotalVat5 As Double
        Public TotalVat19 As Double
    End Structure

    Private Function GetReceiptTotals(userid As String) As ReceiptTotals
        Dim WhoAmI As String = "GetReceiptTotals"
        Dim totals As New ReceiptTotals
        Dim sql As String = ""

        Try
            If SqlLite Then
                sql =
                    "SELECT COUNT(*), " &
                    "       IFNULL(SUM(TOTAL_AMT_WITH_DISC),0), " &
                    "       IFNULL(SUM(TOTAL_VAT5),0), " &
                    "       IFNULL(SUM(TOTAL_VAT19),0), " &
                    "       IFNULL(SUM(TOTAL_VAT0),0), " &
                    "       IFNULL(SUM(TOTAL_VAT3),0) " &
                    "FROM RECEIPTS " &
                    "WHERE CREATED_BY = @USERID " &
                    "AND KIOSKID = @KIOSKID " &
                    "AND CREATED_ON BETWEEN " &
                    "(SELECT MAX(LOGIN_WHEN) " &
                    " FROM SESSIONS " &
                    " WHERE USER_ID = @USERID " &
                    " AND KIOSKID = @KIOSKID) " &
                    "AND CURRENT_TIMESTAMP"

                Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")

                    sqliteConn.Open()

                    Using cmd As New SQLiteCommand(sql, sqliteConn)
                        cmd.Parameters.AddWithValue("@USERID", userid)
                        cmd.Parameters.AddWithValue("@KIOSKID", kioskId)

                        Using dr As SQLiteDataReader = cmd.ExecuteReader()
                            If dr.Read() Then
                                totals.TotalReceipts = Convert.ToInt32(dr(0))
                                totals.TotalAmt = Convert.ToDouble(dr(1))
                                totals.TotalVat5 = Convert.ToDouble(dr(2))
                                totals.TotalVat19 = Convert.ToDouble(dr(3))
                                totals.TotalVat0 = Convert.ToDouble(dr(4))
                                totals.TotalVat3 = Convert.ToDouble(dr(5))
                            End If
                        End Using
                    End Using
                End Using
            Else
                sql =
                        "select count(*), " &
                        "       NVL(sum(total_amt_with_disc),0), " &
                        "       NVL(sum(total_vat5),0), " &
                        "       NVL(sum(total_vat19),0), " &
                        "       NVL(sum(total_vat0),0), " &
                        "       NVL(sum(total_vat3),0) " &
                        "from receipts " &
                        "where created_by = :userid " &
                        "and created_on between " &
                        "(select max(login_when) " &
                        " from sessions " &
                        " where user_id = :userid) " &
                        "and systimestamp"

                Using cmd As New OracleCommand(sql, conn)
                    cmd.Parameters.Add("userid", OracleDbType.Varchar2).Value = userid

                    Using dr = cmd.ExecuteReader()
                        If dr.Read() Then
                            totals.TotalReceipts = Convert.ToInt32(dr(0))
                            totals.TotalAmt = Convert.ToDouble(dr(1))
                            totals.TotalVat5 = Convert.ToDouble(dr(2))
                            totals.TotalVat19 = Convert.ToDouble(dr(3))
                            totals.TotalVat0 = Convert.ToDouble(dr(4))
                            totals.TotalVat3 = Convert.ToDouble(dr(5))
                        End If
                    End Using
                End Using
            End If
        Catch ex As Exception
            CreateExceptionFile(WhoAmI & ": " & ex.Message, sql)
            MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return totals
    End Function

    Private Function GetTotalPayments(userid As String) As Double
        Dim WhoAmI As String = "GetTotalPayments"
        Dim sql As String = ""

        Try
            If SqlLite Then
                sql =
                    "SELECT IFNULL(SUM(AMOUNT),0) " &
                    "FROM PAYMENTS " &
                    "WHERE CREATED_BY = @USERID " &
                    "AND KIOSKID = @KIOSKID " &
                    "AND CREATED_ON BETWEEN " &
                    "(SELECT MAX(LOGIN_WHEN) " &
                    " FROM SESSIONS " &
                    " WHERE USER_ID = @USERID " &
                    " AND KIOSKID = @KIOSKID) " &
                    "AND CURRENT_TIMESTAMP"

                Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                    sqliteConn.Open()

                    Using cmd As New SQLiteCommand(sql, sqliteConn)
                        cmd.Parameters.AddWithValue("@USERID", userid)
                        cmd.Parameters.AddWithValue("@KIOSKID", kioskId)

                        Dim result = cmd.ExecuteScalar()

                        If result IsNot Nothing AndAlso result IsNot DBNull.Value Then
                            Return Convert.ToDouble(result)
                        End If
                    End Using
                End Using
            Else
                sql =
                    "select NVL(sum(amount),0) " &
                    "from payments " &
                    "where created_by = :userid " &
                    "and created_on between " &
                    "(select max(login_when) " &
                    " from sessions " &
                    " where user_id = :userid) " &
                    "and systimestamp"

                Using cmd As New OracleCommand(sql, conn)
                    cmd.Parameters.Add("userid", OracleDbType.Varchar2).Value = userid

                    Dim result = cmd.ExecuteScalar()

                    If result IsNot Nothing AndAlso result IsNot DBNull.Value Then
                        Return Convert.ToDouble(result)
                    End If
                End Using
            End If
        Catch ex As Exception
            CreateExceptionFile(WhoAmI & ": " & ex.Message, sql)
            MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return 0
    End Function

    Private Function GetSalesDescription(userid As String) As String
        Dim WhoAmI As String = "GetSalesDescription"
        Dim sql As String = ""
        Dim result As String = ""

        Try
            If SqlLite Then
                sql =
                    "SELECT " &
                    "   p.UUID PRODUCT_ID, " &
                    "   p.DESCRIPTION DESCRIPTION, " &
                    "   COUNT(rd.PRODUCT_UUID) TOTAL_LINES, " &
                    "   IFNULL(SUM(rd.QUANTITY),0) TOTAL_QTY " &
                    "FROM RECEIPTS_DET rd " &
                    "INNER JOIN PRODUCTS p " &
                    "   ON p.UUID = rd.PRODUCT_UUID " &
                    "WHERE rd.RECEIPT_UUID IN " &
                    "( " &
                    "   SELECT UUID " &
                    "   FROM RECEIPTS " &
                    "   WHERE CREATED_BY = @USERID " &
                    "   AND KIOSKID = @KIOSKID " &
                    "   AND CREATED_ON BETWEEN " &
                    "       (SELECT MAX(LOGIN_WHEN) " &
                    "          FROM SESSIONS " &
                    "         WHERE USER_ID = @USERID " &
                    "           AND KIOSKID = @KIOSKID) " &
                    "       AND CURRENT_TIMESTAMP " &
                    ") " &
                    "GROUP BY p.UUID, p.DESCRIPTION " &
                    "ORDER BY p.DESCRIPTION"


                Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                    sqliteConn.Open()

                    Using cmd As New SQLiteCommand(sql, sqliteConn)
                        cmd.Parameters.AddWithValue("@USERID", userid)
                        cmd.Parameters.AddWithValue("@KIOSKID", kioskId)

                        Using dr As SQLiteDataReader = cmd.ExecuteReader()
                            While dr.Read()
                                result &= vbCrLf & dr("DESCRIPTION").ToString() & ": " & Convert.ToInt32(dr("TOTAL_QTY")).ToString()
                            End While
                        End Using
                    End Using
                End Using
            Else
                sql =
                    "SELECT " &
                    "   p.SERNO PRODUCT_ID, " &
                    "   p.DESCRIPTION DESCRIPTION, " &
                    "   COUNT(rd.PRODUCT_SERNO) TOTAL_LINES, " &
                    "   NVL(SUM(rd.QUANTITY),0) TOTAL_QTY " &
                    "FROM RECEIPTS_DET rd " &
                    "INNER JOIN PRODUCTS p " &
                    "   ON p.SERNO = rd.PRODUCT_SERNO " &
                    "WHERE rd.RECEIPT_SERNO IN " &
                    "( " &
                    "   SELECT SERNO " &
                    "   FROM RECEIPTS " &
                    "   WHERE CREATED_BY = :USERID " &
                    "   AND CREATED_ON BETWEEN " &
                    "       (SELECT MAX(LOGIN_WHEN) " &
                    "          FROM SESSIONS " &
                    "         WHERE USER_ID = :USERID) " &
                    "       AND SYSTIMESTAMP " &
                    ") " &
                    "GROUP BY p.SERNO, p.DESCRIPTION " &
                    "ORDER BY p.DESCRIPTION"


                Using cmd As New OracleCommand(sql, conn)
                    cmd.BindByName = True
                    cmd.Parameters.Add("USERID", OracleDbType.Varchar2).Value = userid

                    Using dr As OracleDataReader = cmd.ExecuteReader()
                        While dr.Read()
                            result &= vbCrLf & dr("DESCRIPTION").ToString() & ": " & Convert.ToInt32(dr("TOTAL_QTY")).ToString()
                        End While
                    End Using
                End Using
            End If

        Catch ex As Exception
            CreateExceptionFile(WhoAmI & ": " & ex.Message, sql)
            MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return result
    End Function

    Private Function GetAmountLaxeia(userid As String) As Double
        Dim WhoAmI As String = "GetAmountLaxeia"
        Dim sql As String = ""

        Try
            If SqlLite Then
                sql =
                    "SELECT IFNULL(SUM(AMOUNT),0) " &
                    "FROM RECEIPTS_DET " &
                    "WHERE VAT = 0 " &
                    "AND RECEIPT_UUID IN " &
                    "( " &
                    "   SELECT UUID " &
                    "   FROM RECEIPTS " &
                    "   WHERE CREATED_BY = @USERID " &
                    "   AND KIOSKID = @KIOSKID " &
                    "   AND CREATED_ON BETWEEN " &
                    "       (SELECT MAX(LOGIN_WHEN) " &
                    "        FROM SESSIONS " &
                    "        WHERE USER_ID = @USERID " &
                    "        AND KIOSKID = @KIOSKID) " &
                    "       AND CURRENT_TIMESTAMP " &
                    ")"

                Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                    sqliteConn.Open()

                    Using cmd As New SQLiteCommand(sql, sqliteConn)
                        cmd.Parameters.AddWithValue("@USERID", userid)
                        cmd.Parameters.AddWithValue("@KIOSKID", kioskId)

                        Dim value = cmd.ExecuteScalar()
                        If value IsNot Nothing AndAlso value IsNot DBNull.Value Then
                            Return Convert.ToDouble(value)
                        End If
                    End Using
                End Using
            Else
                sql =
                    "SELECT NVL(SUM(AMOUNT),0) " &
                    "FROM RECEIPTS_DET " &
                    "WHERE VAT = 0 " &
                    "AND RECEIPT_SERNO IN " &
                    "( " &
                    "   SELECT SERNO " &
                    "   FROM RECEIPTS " &
                    "   WHERE CREATED_BY = :USERID " &
                    "   AND CREATED_ON BETWEEN " &
                    "       (SELECT MAX(LOGIN_WHEN) " &
                    "        FROM SESSIONS " &
                    "        WHERE USER_ID = :USERID) " &
                    "       AND SYSTIMESTAMP " &
                    ")"

                Using cmd As New OracleCommand(sql, conn)
                    cmd.BindByName = True
                    cmd.Parameters.Add("USERID", OracleDbType.Varchar2).Value = userid

                    Dim value = cmd.ExecuteScalar()
                    If value IsNot Nothing AndAlso value IsNot DBNull.Value Then
                        Return Convert.ToDouble(value)
                    End If
                End Using
            End If
        Catch ex As Exception
            CreateExceptionFile(WhoAmI & ": " & ex.Message, sql)
            MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return 0
    End Function

    Private Function GetAmountVisa(userid As String) As Double
        Dim WhoAmI As String = "GetAmountVisa"
        Dim sql As String = ""

        Try
            If SqlLite Then
                sql =
                    "SELECT IFNULL(SUM(TOTAL_AMT_WITH_DISC),0) " &
                    "FROM RECEIPTS " &
                    "WHERE PAYMENT_TYPE = 'V' " &
                    "AND CREATED_BY = @USERID " &
                    "AND KIOSKID = @KIOSKID " &
                    "AND CREATED_ON BETWEEN " &
                    "   (SELECT MAX(LOGIN_WHEN) " &
                    "    FROM SESSIONS " &
                    "    WHERE USER_ID = @USERID " &
                    "    AND KIOSKID = @KIOSKID) " &
                    "AND CURRENT_TIMESTAMP"


                Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                    sqliteConn.Open()
                    Using cmd As New SQLiteCommand(sql, sqliteConn)
                        cmd.Parameters.AddWithValue("@USERID", userid)
                        cmd.Parameters.AddWithValue("@KIOSKID", kioskId)

                        Dim value = cmd.ExecuteScalar()
                        If value IsNot Nothing AndAlso value IsNot DBNull.Value Then
                            Return Convert.ToDouble(value)
                        End If
                    End Using
                End Using
            Else
                sql =
                    "SELECT NVL(SUM(TOTAL_AMT_WITH_DISC),0) " &
                    "FROM RECEIPTS " &
                    "WHERE PAYMENT_TYPE = 'V' " &
                    "AND CREATED_BY = :USERID " &
                    "AND CREATED_ON BETWEEN " &
                    "   (SELECT MAX(LOGIN_WHEN) " &
                    "    FROM SESSIONS " &
                    "    WHERE USER_ID = :USERID) " &
                    "AND SYSTIMESTAMP"

                Using cmd As New OracleCommand(sql, conn)
                    cmd.BindByName = True
                    cmd.Parameters.Add("USERID", OracleDbType.Varchar2).Value = userid

                    Dim value = cmd.ExecuteScalar()
                    If value IsNot Nothing AndAlso value IsNot DBNull.Value Then
                        Return Convert.ToDouble(value)
                    End If
                End Using
            End If
        Catch ex As Exception
            CreateExceptionFile(WhoAmI & ": " & ex.Message, sql)
            MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return 0
    End Function

    Private Sub InsertXReport(userid As String, totals As ReceiptTotals, totalPayments As Decimal, amountLaxeia As Decimal, amountVisa As Decimal, finalLaxeia As Decimal)
        Dim WhoAmI As String = "InsertXReport"
        Dim sql As String = ""
        Try

            If SqlLite Then
                Dim xreport As New XReport
                xreport.InsertXReport(userid, totals, totalPayments, amountLaxeia, amountVisa, finalLaxeia)
            Else

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
                FINALAMTLAXEIA
            )
            VALUES
            (
                :USER_ID,
                (SELECT MAX(LOGIN_WHEN)
                   FROM SESSIONS
                  WHERE USER_ID=:USER_ID),
                SYSTIMESTAMP,
                :TOTAL_RECEIPTS,
                :TOTAL_AMT,
                :TOTAL0,
                :TOTAL3,
                :TOTAL5,
                :TOTAL19,
                (SELECT PARAMVALUE
                   FROM GLOBAL_PARAMS
                  WHERE PARAMKEY='init.fiscal.amt'),
                :FINAL_AMT,
                :PAYMENTS,
                SYSTIMESTAMP,
                '',
                :AMOUNT_LAXEIA,
                (SELECT AMOUNTLAXEIAONLOGIN
                   FROM SESSIONS
                  WHERE USER_ID=:USER_ID
                    AND LOGIN_WHEN =
                    (SELECT MAX(LOGIN_WHEN)
                       FROM SESSIONS
                      WHERE USER_ID=:USER_ID)),
                :AMOUNT_VISA,
                :FINAL_LAXEIA
            )"


                Using cmd As New OracleCommand(sql, conn)

                    cmd.BindByName = True

                    cmd.Parameters.Add("USER_ID", OracleDbType.Varchar2).Value = userid
                    cmd.Parameters.Add("TOTAL_RECEIPTS", OracleDbType.Int32).Value = totals.TotalReceipts
                    cmd.Parameters.Add("TOTAL_AMT", OracleDbType.Decimal).Value = totals.TotalAmt
                    cmd.Parameters.Add("TOTAL0", OracleDbType.Decimal).Value = totals.TotalVat0
                    cmd.Parameters.Add("TOTAL3", OracleDbType.Decimal).Value = totals.TotalVat3
                    cmd.Parameters.Add("TOTAL5", OracleDbType.Decimal).Value = totals.TotalVat5
                    cmd.Parameters.Add("TOTAL19", OracleDbType.Decimal).Value = totals.TotalVat19
                    cmd.Parameters.Add("FINAL_AMT", OracleDbType.Decimal).Value = totals.TotalAmt
                    cmd.Parameters.Add("PAYMENTS", OracleDbType.Decimal).Value = totalPayments
                    cmd.Parameters.Add("AMOUNT_LAXEIA", OracleDbType.Decimal).Value = amountLaxeia
                    cmd.Parameters.Add("AMOUNT_VISA", OracleDbType.Decimal).Value = amountVisa
                    cmd.Parameters.Add("FINAL_LAXEIA", OracleDbType.Decimal).Value = finalLaxeia

                    cmd.ExecuteNonQuery()

                End Using

            End If


        Catch ex As Exception
            CreateExceptionFile(WhoAmI & ": " & ex.Message, sql)
            MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Function GenerateXreport(ByVal userid As String) As Boolean
        Dim WhoAmI As String = "GenerateXreport"
        Try
            Dim totals = GetReceiptTotals(userid)
            Dim totalPayments = GetTotalPayments(userid)
            totals.TotalAmt -= totalPayments
            Dim salesDescription = GetSalesDescription(userid)
            Dim amountLaxeia = GetAmountLaxeia(userid)
            Dim amountVisa = GetAmountVisa(userid)
            Dim finalAmountLaxeia = getAmountLaxeia()

            InsertXReport(userid, totals, totalPayments, amountLaxeia, amountVisa, finalAmountLaxeia)

            Return True
        Catch ex As Exception
            CreateExceptionFile(WhoAmI + ": " + ex.Message, "")
            MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    Public Function GetUser(ByVal userId As String) As String
        Dim WhoAmI As String = "GetUser"
        Dim result As String = ""
        Dim sql As String = ""

        Try
            If SqlLite Then
                sql =
                    "SELECT USERNAME " &
                    "FROM USERS " &
                    "WHERE UUID = @USERID " &
                    "AND KIOSKID = @KIOSKID"

                Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                    sqliteConn.Open()

                    Using cmd As New SQLiteCommand(sql, sqliteConn)
                        cmd.Parameters.AddWithValue("@USERID", userId)
                        cmd.Parameters.AddWithValue("@KIOSKID", kioskId)

                        Dim value = cmd.ExecuteScalar()
                        If value IsNot Nothing AndAlso value IsNot DBNull.Value Then
                            result = value.ToString()
                        End If
                    End Using
                End Using
            Else
                sql =
                    "SELECT USERNAME " &
                    "FROM USERS " &
                    "WHERE UUID = :USERID"

                If conn.State = ConnectionState.Closed Then
                    OpenConn()
                End If

                Using cmd As New OracleCommand(sql, conn)
                    cmd.BindByName = True
                    cmd.CommandType = CommandType.Text
                    cmd.Parameters.Add("USERID", OracleDbType.Varchar2).Value = userId

                    Dim value = cmd.ExecuteScalar()
                    If value IsNot Nothing AndAlso value IsNot DBNull.Value Then
                        result = value.ToString()
                    End If
                End Using
            End If
        Catch ex As Exception
            CreateExceptionFile(WhoAmI + " " + ex.Message, sql)
            MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return result
    End Function

    Public Function GetUserByUsername(ByVal username As String) As String
        Dim WhoAmI As String = "GetUserByUsername"
        Dim result As String = ""
        Dim sql As String = ""

        Try
            If SqlLite Then
                sql =
                    "SELECT USERNAME
                     FROM USERS
                     WHERE USERNAME = @USERNAME
                     AND KIOSKID = @KIOSKID"

                Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                    sqliteConn.Open()
                    Using cmd As New SQLiteCommand(sql, sqliteConn)
                        cmd.Parameters.AddWithValue("@USERNAME", username)
                        cmd.Parameters.AddWithValue("@KIOSKID", kioskId)
                        Dim value = cmd.ExecuteScalar()
                        If value IsNot Nothing AndAlso value IsNot DBNull.Value Then
                            result = value.ToString()
                        End If
                    End Using
                End Using
            Else
                sql =
                    "SELECT USERNAME
                     FROM USERS
                     WHERE USERNAME = :USERNAME"

                Using cmd As New OracleCommand(sql, conn)
                    cmd.BindByName = True
                    cmd.Parameters.Add("USERNAME", OracleDbType.Varchar2).Value = username
                    Dim value = cmd.ExecuteScalar()

                    If value IsNot Nothing AndAlso value IsNot DBNull.Value Then
                        result = value.ToString()
                    End If
                End Using
            End If
        Catch ex As Exception
            CreateExceptionFile(WhoAmI + " " + ex.Message, sql)
            MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return result
    End Function

    Public Sub LogoutUser(ByVal username As String)
        Dim WhoAmI As String = "LogoutUser"
        Dim sql As String = ""
        Try
            If SqlLite Then
                sql =
                    "UPDATE SESSIONS
                     SET IS_ACTIVE = 0,
                         LOGOUT_WHEN = CURRENT_TIMESTAMP,
                         SYNC_STATUS = 1
                     WHERE IS_ACTIVE = 1
                     AND KIOSKID = @KIOSKID
                     AND USER_ID = (
                            SELECT UUID
                            FROM USERS
                            WHERE USERNAME = @USERNAME
                            AND KIOSKID = @KIOSKID
                     )"

                Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                    sqliteConn.Open()
                    Using cmd As New SQLiteCommand(sql, sqliteConn)
                        cmd.Parameters.AddWithValue("@USERNAME", username)
                        cmd.Parameters.AddWithValue("@KIOSKID", kioskId)
                        cmd.ExecuteNonQuery()
                    End Using
                End Using

                Try
                    Dim sync As New SyncTables()
                    sync.SyncSessions()
                Catch ex As Exception
                End Try
            Else
                sql =
                    "UPDATE sessions s
                     SET s.is_active = 0,
                         s.logout_when = SYSTIMESTAMP
                     WHERE s.is_active = 1
                     AND EXISTS
                     (
                         SELECT 1
                         FROM users u
                         WHERE u.uuid = s.user_id
                         AND u.username = :username
                     )"

                If conn.State <> ConnectionState.Open Then
                    OpenConn()
                End If

                Using cmd As New OracleCommand(sql, conn)
                    cmd.CommandType = CommandType.Text
                    cmd.BindByName = True
                    cmd.Parameters.Add("username", OracleDbType.Varchar2).Value = username
                    cmd.ExecuteNonQuery()
                End Using
            End If
        Catch ex As Exception
            CreateExceptionFile(ex.ToString(), sql)
            MessageBox.Show(WhoAmI + " " + ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Function IsLoggedIn(username As String) As Boolean
        Dim WhoAmI As String = "IsLoggedIn"

        Dim sql As String = "SELECT COUNT(*) 
                            FROM sessions s 
                            WHERE 
                            s.is_active = 1 AND s.user_id = (SELECT u.uuid FROM users u WHERE u.username = :username) "
        Try
            If SqlLite Then
                Dim session As New Session
                Return session.CheckIfUserAlreadyLoggedIn(username)
            Else
                Using cmd As New OracleCommand(sql, conn)
                    If conn.State <> ConnectionState.Open Then OpenConn()

                    cmd.CommandType = CommandType.Text
                    cmd.Parameters.Add("username", OracleDbType.Varchar2).Value = username

                    Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())

                    Return (count > 0)
                End Using
            End If
        Catch ex As Exception
            CreateExceptionFile(WhoAmI + " " + ex.ToString(), " " + sql)
            MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    Public Sub LogoutUserUUID(ByVal uuid As String)
        Dim WhoAmI As String = "LogoutUserUUID"
        Dim sql As String = "UPDATE SESSIONS
                             SET IS_ACTIVE = 0,
                                 LOGOUT_WHEN = SYSTIMESTAMP
                             WHERE USER_ID = :USER_ID
                             AND IS_ACTIVE = 1"
        Try
            If SqlLite Then
                Dim session As New Session
                session.LogoutUserByUUID(uuid)
            Else
                While conn.State = ConnectionState.Closed
                    OpenConn()
                End While

                Using cmd As New OracleCommand(sql, conn)
                    cmd.BindByName = True
                    cmd.Parameters.Add("USER_ID", OracleDbType.Varchar2).Value = uuid
                    cmd.ExecuteNonQuery()
                End Using
            End If
        Catch ex As Exception
            CreateExceptionFile(WhoAmI & ": " & ex.Message, sql)
            MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Function GetAmountLaxeia() As Double
        Dim WhoAmI As String = "GetAmountLaxeia"
        Dim sql As String
        Dim amountLaxeia As Double = 0
        If SqlLite Then
            sql = "SELECT IFNULL(SUM(p.sell_amt * p.avail_quantity),0)
                     FROM lottery l
                     INNER JOIN barcodes b
                         ON b.barcode = l.barcode
                     INNER JOIN products p
                         ON p.uuid = b.product_uuid
                     WHERE l.kioskid = @KIOSKID"
        Else
            sql = "SELECT NVL(SUM(sell_amt * avail_quantity),0) " &
                        "FROM lottery l " &
                        "INNER JOIN barcodes b ON b.barcode = l.barcode " &
                        "INNER JOIN products p ON p.serno = b.product_serno"
        End If

        Try
            If SqlLite Then
                Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                    sqliteConn.Open()
                    Using cmd As New SQLiteCommand(sql, sqliteConn)
                        cmd.Parameters.AddWithValue("@KIOSKID", kioskId)
                        Dim result = cmd.ExecuteScalar()
                        If result IsNot Nothing AndAlso result IsNot DBNull.Value Then
                            amountLaxeia = Convert.ToDouble(result)
                        End If
                    End Using
                End Using
            Else
                Using cmd As New OracleCommand(sql, conn)
                    cmd.CommandType = CommandType.Text

                    Using dr As OracleDataReader = cmd.ExecuteReader()
                        If dr.Read() Then
                            ' Safe conversion even if Oracle returns Decimal
                            amountLaxeia = Convert.ToDouble(dr(0))
                        End If
                    End Using
                End Using
            End If
        Catch ex As Exception
            CreateExceptionFile(WhoAmI + " " + ex.Message, sql)
            MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return amountLaxeia
    End Function

    Public Function isConnOpen() As Boolean
        Dim connectionRetries As Integer = 0
        While conn.State = ConnectionState.Closed And connectionRetries < 200
            OpenConn()
            connectionRetries += 1
        End While
        If conn.State = ConnectionState.Closed Then
            Return False
        End If
        Return True
    End Function

    Public Structure BtnItemDetails
        Public ProductSerno As Integer
        Public DisplayDesc As String
        Public Description As String
        Public Vat As Integer
        Public ProductPrice As Double
        Public IsBox As Integer
        Public BoxQnt As Integer
        Public ProductUUID As String
    End Structure

    Public Structure BtnItem
        Public Id As String
        Public Name As String
        Public LinkedItems As Integer
        Public LinkedItemsDetails As ArrayList
    End Structure

    Public btnItemsMap As New Dictionary(Of String, BtnItem)

    Public Structure OfferTypeXY
        Public productSerno As Integer
        Public currentQuantity As Integer
        Public xquantity As Integer
        Public yquantity As Integer
        Public currentDiscount As Double
    End Structure

    Public Structure OfferTypeDiscAt
        'Oracle version
        Public productSerno As Integer
        'Sqlite version
        Public Property ProductUUID As String

        Public currentQuantity As Integer
        Public discountAmt As Double
        Public discountAt As Integer
        Public currentDiscount As Double
    End Structure

    Public Sub GetMinBarcodeLength()
        Dim WhoAmI As String = "GetMinBarcodeLength"
        Dim sql As String = ""

        Try
            If SqlLite Then
                sql = "SELECT IFNULL(MIN(LENGTH(BARCODE)),9999999)
                       FROM BARCODES
                       WHERE KIOSKID=@KIOSKID"

                Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                    sqliteConn.Open()
                    Using cmd As New SQLiteCommand(sql, sqliteConn)
                        cmd.Parameters.AddWithValue("@KIOSKID", kioskId)
                        Dim result = cmd.ExecuteScalar()

                        If result IsNot Nothing AndAlso Not Convert.IsDBNull(result) Then
                            minBarcode = Convert.ToInt32(result)
                        End If
                    End Using
                End Using
            Else
                sql = "SELECT NVL(MIN(LENGTH(BARCODE)),9999999) FROM BARCODES"

                Using cmd As New OracleCommand(sql, conn)
                    cmd.CommandType = CommandType.Text
                    Dim result = cmd.ExecuteScalar()

                    If result IsNot Nothing AndAlso Not Convert.IsDBNull(result) Then
                        minBarcode = Convert.ToInt32(result)
                    End If
                End Using
            End If
        Catch ex As Exception
            CreateExceptionFile(WhoAmI + " " + ex.Message, sql)
            MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub CreateExceptionFile(exception As String, sql As String)
        Try
            Dim folder As String = "C:\exceptions"
            If Not IO.Directory.Exists(folder) Then
                IO.Directory.CreateDirectory(folder)
            End If

            Dim fileName As String = DateTime.Now.ToString("yyyyMMdd_HHmmss_fff") & ".txt"
            Dim filePath As String = IO.Path.Combine(folder, "exception_" & fileName)

            Using writer As New IO.StreamWriter(filePath, False)
                writer.WriteLine("Timestamp: " & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"))
                writer.WriteLine()
                writer.WriteLine("Exception:")
                writer.WriteLine(exception)
                writer.WriteLine()
                writer.WriteLine("SQL:")
                writer.WriteLine(sql)
            End Using

        Catch ex As Exception
            ' As last fallback, write to Event Viewer
            Diagnostics.EventLog.WriteEntry("Application", ex.Message, Diagnostics.EventLogEntryType.Error)
        End Try
    End Sub

    Public Sub IsPasswordEncrypted()
        If SqlLite Then
            Dim sql As String = "
                            SELECT PARAMVALUE 
                            FROM GLOBAL_PARAMS
                            WHERE PARAMKEY='all.pass.encrypted' 
                            AND KIOSKID=:KIOSKID "

            Using conn As New SQLiteConnection("Data Source=kiosk.db")
                conn.Open()
                Using cmd As New SQLiteCommand(sql, conn)
                    cmd.Parameters.Add("KIOSKID", OracleDbType.Varchar2).Value = kioskId
                    Using reader = cmd.ExecuteReader()
                        If reader.Read() AndAlso Convert.ToInt32(reader(0)) = 0 Then
                            EncryptPassword("all")
                        End If
                    End Using
                End Using
            End Using
        Else
            If Not isConnOpen() Then
                MessageBox.Show(CANNOT_ACCESS_DB, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            Try
                Using cmd As New OracleCommand("select paramvalue from global_params where paramkey='all.pass.encrypted'", conn)
                    cmd.CommandType = CommandType.Text
                    Using dr As OracleDataReader = cmd.ExecuteReader()
                        If dr.Read() AndAlso Convert.ToInt32(dr(0)) = 0 Then
                            encryptPassword("all")
                        End If
                    End Using
                End Using
            Catch ex As Exception
                CreateExceptionFile(ex.Message, " " & "select paramvalue from global_params where paramkey='all.pass.encrypted'")
                MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Public Sub EncryptPassword(ByVal username As String)
        'TODO: 15/06 Revisit (might not needed - all passwords are already encrypted in the DB)
        If Not isConnOpen() Then
            MessageBox.Show(CANNOT_ACCESS_DB, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If Not username.Equals("all", StringComparison.OrdinalIgnoreCase) Then Exit Sub

        Try
            Dim userNameAndPass As New Dictionary(Of String, String)

            ' Get all usernames and passwords
            Using cmd As New OracleCommand(GET_ALL_USERS_AND_PASS, conn)
                cmd.CommandType = CommandType.Text
                Using dr As OracleDataReader = cmd.ExecuteReader()
                    While dr.Read()
                        Dim user As String = dr.GetString(0)
                        Dim pass As String = dr.GetString(1)
                        userNameAndPass(user) = pass
                    End While
                End Using
            End Using

            ' Update each user with encrypted password
            For Each entry In userNameAndPass
                Dim tmpUsername = entry.Key
                Dim tmpEncryptedPass = getEncryptedValue(entry.Value)

                Dim updateSql As String = "UPDATE users SET pass = :pass WHERE username = :username"
                Using updateCmd As New OracleCommand(updateSql, conn)
                    updateCmd.Parameters.Add(New OracleParameter("pass", tmpEncryptedPass))
                    updateCmd.Parameters.Add(New OracleParameter("username", tmpUsername))
                    updateCmd.ExecuteNonQuery()
                End Using
            Next

            ' Set global param
            Dim globalUpdateSql As String = "UPDATE global_params SET paramkey = 1 WHERE paramvalue = 'all.pass.encrypted'"
            Using globalCmd As New OracleCommand(globalUpdateSql, conn)
                globalCmd.ExecuteNonQuery()
            End Using

        Catch ex As Exception
            CreateExceptionFile(ex.Message, GET_ALL_USERS_AND_PASS)
            MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Public Function getEncryptedValue(ByVal value As String) As String
        Dim bArray() As Byte = System.Text.Encoding.UTF8.GetBytes(value)
        Dim sb64 As String = System.Convert.ToBase64String(bArray)
        Return sb64
    End Function

    Public Sub GetStartDate()
        Dim WhoAmI As String = "GetStartDate"
        Dim sql As String = ""

        Try
            If SqlLite Then
                sql = "SELECT PARAMVALUE
                       FROM GLOBAL_PARAMS
                       WHERE PARAMKEY = 'start.date'
                         AND KIOSKID = @KIOSKID"

                Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                    sqliteConn.Open()
                    Using cmd As New SQLiteCommand(sql, sqliteConn)
                        cmd.Parameters.AddWithValue("@KIOSKID", kioskId)
                        Dim result = cmd.ExecuteScalar()

                        If result IsNot Nothing AndAlso Not Convert.IsDBNull(result) Then
                            startDate = Date.ParseExact(result.ToString(), "dd/MM/yy", Globalization.CultureInfo.InvariantCulture)
                        End If
                    End Using
                End Using
            Else

                sql = "SELECT TO_DATE(paramvalue, 'DD/MM/YY')
                       FROM global_params
                       WHERE paramkey = 'start.date'"

                Using cmd As New OracleCommand(sql, conn)
                    cmd.CommandType = CommandType.Text
                    Using dr As OracleDataReader = cmd.ExecuteReader()
                        If dr.Read() AndAlso Not dr.IsDBNull(0) Then
                            startDate = dr.GetDateTime(0)
                        End If
                    End Using
                End Using
            End If
        Catch ex As Exception
            CreateExceptionFile(WhoAmI + "" + ex.Message, sql)
            MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Structure tmpTransaction
        Public totalWithDiscount As String
        Public totalAmt As String
        Public discount As String
        Public paymentAmt As String
        Public returnAmt As String
        Public totalAmt19 As Double
        Public totalAmt5 As Double
        Public totalAmt0 As Double
        Public totalAmt3 As Double
        Public totalItems As Integer
        Public dTotalAmt As Double
        Public dTotalWithDiscount As Double
        Public offerXYItems As ArrayList
        Public offerDiscAtItems As ArrayList
        Public productsAndQuantity As Dictionary(Of String, Integer)
        Public dgvReceipt As DataGridView
    End Structure

    Public Sub createDeleteLineFile(ByVal lineInfo As String)
        Dim fileName As String = Date.Now.Day.ToString & Date.Now.Month.ToString & Date.Now.Year.ToString & Date.Now.Hour.ToString & Date.Now.Minute.ToString & Date.Now.Millisecond.ToString
        Dim filepath As String = "C:\exceptions\DL" & fileName & ".txt"
        If Not System.IO.File.Exists(filepath) Then
            System.IO.File.Create(filepath).Dispose()
            Dim objWriter As New System.IO.StreamWriter(filepath)
            objWriter.Write(lineInfo)
            objWriter.Close()
        End If
    End Sub

    Public Sub setDetails(ByVal buttonName As String, ByVal tmpBtnItem As BtnItem)
        Dim index As Integer
        If buttonName.Length = 8 Then
            index = CInt(buttonName.Substring(7, 1)) - 1
        Else
            index = CInt(buttonName.Substring(7, 2)) - 1
        End If
        dynamicProductSerno = tmpBtnItem.LinkedItemsDetails.Item(index).ProductSerno
        dynamicProductDesc = tmpBtnItem.LinkedItemsDetails.Item(index).Description()
        dynamicProductVAT = tmpBtnItem.LinkedItemsDetails.Item(index).Vat
        dynamicProductPrice = tmpBtnItem.LinkedItemsDetails.Item(index).ProductPrice
        dynamicProductIsBox = tmpBtnItem.LinkedItemsDetails.Item(index).IsBox
        dynamicProductBoxQnt = tmpBtnItem.LinkedItemsDetails.Item(index).BoxQnt
    End Sub

    Public Async Function ExecuteSale(totalAmount As Double) As Task
        Try
            Dim baseUrl As String = "http://127.0.0.1:8080/api"
            Dim client As New CIActivateClient("http://127.0.0.1:8080/api", "192.168.0.25")
            ' 0️⃣ Login
            Dim loggedIn As Boolean = Await client.Login("admin", "Admin123!")
            If Not loggedIn Then
                MessageBox.Show("Login failed")
                Return
            End If

            Try
                ' Inside your button click or workflow:
                Dim statusJson As String = Await client.GetStatus()
                Dim status As StatusResponse = JsonConvert.DeserializeObject(Of StatusResponse)(statusJson)

                If status.statusTxt <> "Idle" Then

                    Return
                End If
            Catch ex As Exception
                MessageBox.Show($"Error: {ex.Message}")
            End Try

            ' 3️⃣ Start transaction (async)
            Dim txn As New CIActivateTransactionRequest With {
            .seqNo = Guid.NewGuid().ToString().Substring(0, 11),
            .amount = totalAmount,
            .async = True
        }

            Dim log As CIActivateTransactionLog = Await client.StartTransaction(txn)

            ' 4️⃣ Poll / wait for cash in
            Dim finalLog As CIActivateTransactionLog
            Do
                finalLog = Await client.WaitTransaction(log.seqNo)
                Await Task.Delay(500) ' avoid spamming device
            Loop Until finalLog.status = 0

            ' 5️⃣ Check for change
            Dim change As Double = finalLog.inTotal - totalAmount
            If change > 0 Then

                Dim changeDict As New Dictionary(Of String, Integer)
                changeDict.Add("EUR_1_0_2", CInt(change)) ' example
                'Dim changeLog As CIActivateTransactionLog = Await client.CashOut(deviceName, changeDict)
                Dim changeLog As CIActivateTransactionLog = Await client.CashOut(changeDict)

            Else

            End If
        Catch ex As Exception

        End Try
    End Function

    Public Function GetLocalIPAddress() As String
        Dim host = Dns.GetHostEntry(Dns.GetHostName())
        For Each ip In host.AddressList
            If ip.AddressFamily = AddressFamily.InterNetwork Then
                Return ip.ToString()
            End If
        Next
        Return ""
    End Function

    Public Function BuildPayoutDictionary(amount As Double) As Dictionary(Of String, Integer)

        Dim payout As New Dictionary(Of String, Integer)

        Dim denominations As New Dictionary(Of String, Double) From {
            {"EUR_200_0_2", 200},
            {"EUR_100_0_2", 100},
            {"EUR_50_0_2", 50},
            {"EUR_20_0_2", 20},
            {"EUR_10_0_2", 10},
            {"EUR_5_0_2", 5},
            {"EUR_2_0_2", 2},
            {"EUR_1_0_2", 1},
            {"EUR_0_50_2", 0.5},
            {"EUR_0_20_2", 0.2},
            {"EUR_0_10_2", 0.1}
        }

        Dim remaining As Double = amount

        For Each denom In denominations
            Dim count As Integer = CInt(Math.Floor(remaining / denom.Value))
            If count > 0 Then
                payout.Add(denom.Key, count)
                remaining -= count * denom.Value
                remaining = Math.Round(remaining, 2)
            End If
        Next

        Return payout
    End Function

    Public Function DenominationValue(key As String) As Double
        Dim parts() As String = key.Split("_"c)
        Dim euros As Double = Double.Parse(parts(1))
        Dim cents As Double = Double.Parse(parts(2))
        Return euros + (cents / 100)
    End Function

    Public Function BuildPayoutFromInventory(amount As Double,
                                         inventory As Dictionary(Of String, Integer)) _
                                         As Dictionary(Of String, Integer)

        Dim payout As New Dictionary(Of String, Integer)
        Dim remaining As Double = amount

        ' Sort denominations from highest to lowest value
        Dim sortedDenoms = inventory _
            .OrderByDescending(Function(d) DenominationValue(d.Key))

        For Each denom In sortedDenoms
            Dim value As Double = DenominationValue(denom.Key)
            Dim availableQty As Integer = denom.Value

            Dim neededQty As Integer = CInt(Math.Floor(remaining / value))
            Dim qtyToUse As Integer = Math.Min(neededQty, availableQty)

            If qtyToUse > 0 Then
                payout.Add(denom.Key, qtyToUse)
                remaining -= qtyToUse * value
                remaining = Math.Round(remaining, 2)
            End If
        Next

        If remaining > 0 Then
            Throw New Exception("Not enough cash in machine to pay this amount.")
        End If

        Return payout
    End Function

    Public Function BuildPayout(amount As Decimal, inventory As Dictionary(Of String, Integer)) _
    As Dictionary(Of String, Integer)

        Dim payout As New Dictionary(Of String, Integer)

        ' Sort denominations by value descending
        Dim sorted = inventory.Keys _
            .Select(Function(k) New With {
                .Key = k,
                .Value = Decimal.Parse(k.Split("_"c)(1)) / 100
            }) _
            .OrderByDescending(Function(x) x.Value)

        Dim remaining = amount

        For Each d In sorted
            Dim denomValue = d.Value
            Dim available = inventory(d.Key)

            Dim needed = Math.Floor(remaining / denomValue)

            If needed > 0 Then
                Dim useCount = Math.Min(needed, available)
                payout.Add(d.Key, CInt(useCount))
                remaining -= useCount * denomValue
            End If
        Next

        Return payout
    End Function

    Public Async Function FindCIMachine() As Task(Of String)
        Dim baseNetwork As String = "192.168.0."
        Dim http As New HttpClient()
        http.Timeout = TimeSpan.FromMilliseconds(300)

        For i As Integer = 1 To 254
            Dim ip As String = baseNetwork & i.ToString()
            Try
                Dim response = Await http.GetAsync($"http://{ip}/api/ping")
                If response.IsSuccessStatusCode Then
                    Return ip
                End If
            Catch
                ' ignore timeouts
            End Try
        Next

        Return Nothing
    End Function
End Module
