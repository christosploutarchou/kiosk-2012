Imports System.Globalization
Imports System.IO
Imports System.Text
Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types

Module connectionModule
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

    'Public Sub isBoxReport()
    'Dim cmd As New OracleCommand("", conn)
    'Dim dr As OracleDataReader
    'Dim sql As String = ""
    'Try
    'ISBOX_LOG
    '       sql = "select COUNT(*) from ALL_TABLES " &
    '            "where TABLE_NAME = 'ISBOX_LOG' "
    '
    'cmd = New OracleCommand(sql, conn)
    'cmd.CommandType = CommandType.Text
    'dr = cmd.ExecuteReader()
    'If dr.Read Then
    'If (CInt(dr(0)) = 0) Then
    '               sql = "create table ISBOX_LOG (LOGMSG Varchar2(256))"
    '              cmd = New OracleCommand(sql, conn)
    '             cmd.ExecuteReader()
    'End If
    'End If
    '       dr.Close()
    'Catch ex As Exception
    '       createExceptionFile(ex.Message, " " & sql)
    '      MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
    'Finally
    '       cmd.Dispose()
    'End Try
    'End Sub

    Public Sub getVat3PercentColumns()
        Dim cmd As New OracleCommand("", conn)
        Dim dr As OracleDataReader
        Dim sql As String = ""
        Try
            'VAT_TYPES
            sql = "select count(*) from vat_types where vat=3"
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
            GetParams()
            GetMinBarcodeLength()
            getStartDate()
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
        conn.Close()
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



    Public Sub getKIOSKParams()
        Dim sql As String = "SELECT paramkey, paramvalue " &
                        "FROM global_params " &
                        "WHERE paramkey IN ('login.title1', 'login.title2', 'kiosk.name', 'company.name', " &
                        "'kiosk.address1', 'kiosk.address2', 'company.vat')"

        Try
            Using cmd As New OracleCommand(sql, conn)
                cmd.CommandType = CommandType.Text
                Using dr As OracleDataReader = cmd.ExecuteReader()
                    ' Map keys to variables using a dictionary
                    Dim paramMap As New Dictionary(Of String, Action(Of String)) From {
                    {"login.title1", Sub(val) LOGIN_TITLE1 = val},
                    {"login.title2", Sub(val) LOGIN_TITLE2 = val},
                    {"kiosk.name", Sub(val) KIOSK_NAME = val},
                    {"company.name", Sub(val) COMPANY_NAME = val},
                    {"kiosk.address1", Sub(val) KIOSK_ADDRESS1 = val},
                    {"kiosk.address2", Sub(val) KIOSK_ADDRESS2 = val},
                    {"company.vat", Sub(val) COMPANY_VAT = val}
                }

                    While dr.Read()
                        Dim key As String = dr("paramkey").ToString()
                        Dim value As String = dr("paramvalue").ToString()
                        If paramMap.ContainsKey(key) Then
                            paramMap(key)(value)
                        End If
                    End While
                End Using
            End Using
        Catch ex As Exception
            createExceptionFile(ex.Message, " " & sql)
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

    Public Function generateXreport(ByVal userid As String) As Boolean
        Dim cmd = New OracleCommand("", conn)
        Dim dr As OracleDataReader
        Dim sql As String = ""
        Try
            '1. Total receipts with their amounts
            cmd = New OracleCommand(GET_TOTALS, conn)
            Dim userIdParam As New OracleParameter
            userIdParam.OracleDbType = OracleDbType.Varchar2
            userIdParam.Value = userid
            cmd.Parameters.Add(userIdParam)
            sql = GET_TOTALS

            dr = cmd.ExecuteReader()

            Dim totalReceipts As Integer = 0
            Dim totalAmt As Double = 0
            Dim totalVat0 As Double = 0
            Dim totalVat3 As Double = 0
            Dim totalVat5 As Double = 0
            Dim totalVat19 As Double = 0
            Dim totalPayments As Double = 0

            If dr.Read() Then
                totalReceipts = CInt(dr(0))
                totalAmt = CDbl(dr(1))
                totalVat5 = CDbl(dr(2))
                totalVat19 = CDbl(dr(3))
                totalVat0 = CDbl(dr(4))
                totalVat3 = CDbl(dr(5))
            End If

            '2. Total Payments
            sql = "select NVL(sum(amount),0) from payments " &
                  "where created_by = '" & userid & "' " &
                  "and created_on between (select max(login_when) from sessions " &
                                          "where user_id = '" & userid & "') " &
                  "and (select systimestamp from dual)"

            cmd = New OracleCommand(sql, conn)
            dr = cmd.ExecuteReader()
            If dr.Read Then
                totalPayments = CDbl(dr(0))
                totalAmt -= totalPayments
            End If

            'Description
            sql = "select product_serno, description , count(product_serno), sum(quantity) " &
                  "from receipts_det " &
                  "inner join products on products.serno = receipts_det.product_serno " &
                  "where receipt_serno in (select serno from receipts  " &
                                          "where created_by = '" & userid & "' " &
                                          "and created_on between (select max(login_when) from sessions " &
                                          "where user_id = '" & userid & "') " &
                                          "and (select systimestamp from dual)) " &
                  "group by product_serno, description "

            cmd = New OracleCommand(sql, conn)
            dr = cmd.ExecuteReader()
            Dim salesDescription As String = ""
            While dr.Read
                salesDescription += vbNewLine & dr(1) & ": " & dr(3)
            End While

            'Total amount laxeia
            sql = "select NVL(sum(amount),0) " &
                  "from receipts_det " &
                  "where vat=0 and " &
                  "receipt_serno in (select serno from receipts  " &
                                    "where created_by = '" & userid & "' " &
                                    "and created_on between (select max(login_when) from sessions " &
                                                            "where user_id = '" & userid & "') " &
                                                            "and (select systimestamp from dual)) "

            cmd = New OracleCommand(sql, conn)
            dr = cmd.ExecuteReader()
            Dim amountLaxeia As Double = 0
            If dr.Read Then
                amountLaxeia = CDbl(dr(0))
            End If

            sql = "select NVL(sum(total_amt_with_disc),0) " &
                  "from receipts " &
                  "where payment_type='V' and " &
                  "created_by = '" & userid & "' and " &
                  "created_on between (select max(login_when) from sessions " &
                  "                    where user_id = '" & userid & "') " &
                  "                    and (select systimestamp from dual) "

            cmd = New OracleCommand(sql, conn)
            dr = cmd.ExecuteReader()
            Dim amountVisa As Double = 0
            If dr.Read Then
                amountVisa = CDbl(dr(0))
            End If

            Dim finalAmountLaxeia As Double = getAmountLaxeia()
            dr.Close()

            sql = "insert into x_report (user_id, from_date, to_date, total_receipts, total_amt, total0percent, total3percent, total5percent, total19percent, " &
                  "                      initial_amt, final_amt, payments, created_on, description, amount_laxeia, initialAmtLaxeia, amountVisa, finalAmtLaxeia) " &
                  "values               ('" & userid & "', " &
                  "                     (select max(login_when) from sessions where user_id = '" & userid & "'), " &
                  "                     (select systimestamp from dual), " &
                  "                       " & totalReceipts & ", " &
                  "                       " & totalAmt & ", " &
                  "                       " & totalVat0 & ", " &
                  "                       " & totalVat3 & ", " &
                  "                       " & totalVat5 & ", " &
                  "                       " & totalVat19 & ", " &
                  "                       (select paramvalue from global_params where paramkey = 'init.fiscal.amt'), " &
                  "                       (select sum(" & totalAmt & " ) from dual), " &
                  "                       " & totalPayments & ", (select systimestamp from dual), " &
                  "                      ''," &
                  "                       " & amountLaxeia & ", " &
                  "                      (select AMOUNTLAXEIAONLOGIN from sessions where user_id = '" & userid & "' " &
                  "                       and login_when =(select max(login_when) from sessions " &
                  "                                        where user_id = '" & userid & "')), " &
                  "                       " & amountVisa & ", " & finalAmountLaxeia & ")"

            cmd = New OracleCommand(sql, conn)
            Using cmd
                cmd.ExecuteNonQuery()
            End Using
            Return True
        Catch ex As Exception
            createExceptionFile(ex.Message, sql)
            MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            cmd.Dispose()
        End Try
    End Function

    Public Function GetUser(ByVal userId As String) As String
        Dim result As String = ""
        Dim sql As String = GET_USER_BY_ID

        Try
            If conn.State = ConnectionState.Closed Then
                openConn()
            End If

            Using cmd As New OracleCommand(sql, conn)
                cmd.CommandType = CommandType.Text
                cmd.Parameters.Add(New OracleParameter("userid", OracleDbType.Varchar2)).Value = userId

                Using dr As OracleDataReader = cmd.ExecuteReader()
                    If dr.Read() Then
                        result = dr.GetString(0)
                    End If
                End Using
            End Using

        Catch ex As Exception
            createExceptionFile(ex.Message, sql)
            MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return result
    End Function


    Public Function getUserByUsername(ByVal username As String) As String
        Dim result = ""
        Dim cmd As New OracleCommand("", conn)
        Dim sql As String = ""
        Try
            sql = "select username from users " & _
                  "where uuid = (select uuid from users where username = '" & username & "')"

            cmd = New OracleCommand(sql, conn)
            Using dr = cmd.ExecuteReader()
                If dr.Read Then
                    result = CStr(dr(0))
                End If
            End Using

        Catch ex As Exception
            createExceptionFile(ex.Message, sql)
            MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            cmd.Dispose()
        End Try
        Return result
    End Function

    Public Sub LogoutUser(ByVal username As String)
        Const sql As String =
        "UPDATE sessions s " &
        "SET s.is_active = 0, s.logout_when = SYSTIMESTAMP " &
        "WHERE s.is_active = 1 " &
        "  AND EXISTS (SELECT 1 FROM users u WHERE u.uuid = s.user_id AND u.username = :username)"

        Try
            If conn.State <> ConnectionState.Open Then
                openConn()
            End If

            Using cmd As New OracleCommand(sql, conn)
                cmd.CommandType = CommandType.Text
                cmd.Parameters.Add("username", OracleDbType.Varchar2).Value = username
                Dim rows As Integer = cmd.ExecuteNonQuery()
            End Using

        Catch ex As Exception
            CreateExceptionFile(ex.ToString(), sql)
            MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Public Function IsLoggedIn(username As String) As Boolean
        Const q As String =
        "SELECT COUNT(*) " &
        "FROM sessions s " &
        "WHERE s.is_active = 1 " &
        "  AND s.user_id = (SELECT u.uuid FROM users u WHERE u.username = :username)"

        Try
            Using cmd As New OracleCommand(q, conn)
                If conn.State <> ConnectionState.Open Then openConn()

                cmd.CommandType = CommandType.Text
                cmd.Parameters.Add("username", OracleDbType.Varchar2).Value = username

                Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())

                Return (count > 0)
            End Using

        Catch ex As Exception
            CreateExceptionFile(ex.ToString(), q)
            MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function



    Public Sub logoutUserUUID(ByVal uuid As String)
        Dim connectionRetries As Integer = 0
        While conn.State = ConnectionState.Closed And connectionRetries < 10
            openConn()
            connectionRetries += 1
        End While
        If conn.State = ConnectionState.Closed Then
            MessageBox.Show(CANNOT_ACCESS_DB, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim sql As String = "update sessions set is_active = 0, logout_when = (select systimestamp from dual) " & _
                            "where user_id = '" & uuid & "' and is_active = 1"
        Dim cmd As New OracleCommand(sql, conn)
        Try
            While conn.State = ConnectionState.Closed
                openConn()
            End While
            cmd.CommandType = CommandType.Text
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            createExceptionFile(ex.Message, sql)
            MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            cmd.Dispose()
        End Try
    End Sub

    Public Function getAmountLaxeia() As Double
        Dim sql As String = "select NVL(sum(sell_amt * avail_quantity),0) " & _
                            "from lottery l " & _
                            "inner join barcodes b on b.barcode = l.barcode " & _
                            "inner join products p on p.serno = b.product_serno"
        Dim cmd As New OracleCommand("", conn)
        Dim dr As OracleDataReader
        Dim amountLaxeia As Double = 0
        Try
            cmd = New OracleCommand(sql, conn)
            cmd.CommandType = CommandType.Text
            dr = cmd.ExecuteReader()
            If dr.Read Then
                amountLaxeia = CDbl(dr(0))
            End If
            dr.Close()
        Catch ex As Exception
            createExceptionFile(ex.Message, " " & sql)
            MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            cmd.Dispose()
        End Try
        Return amountLaxeia
    End Function

    Public Function isConnOpen() As Boolean
        Dim connectionRetries As Integer = 0
        While conn.State = ConnectionState.Closed And connectionRetries < 200
            openConn()
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
        Public productSerno As Integer
        Public currentQuantity As Integer
        Public discountAmt As Double
        Public discountAt As Integer
        Public currentDiscount As Double
    End Structure

    Public Sub GetMinBarcodeLength()
        Const q As String = "SELECT NVL(MIN(LENGTH(barcode)), 9999999) FROM barcodes"
        Try
            Using cmd As New OracleCommand(q, conn)
                cmd.CommandType = CommandType.Text
                Dim result = cmd.ExecuteScalar()

                If result IsNot Nothing AndAlso Not Convert.IsDBNull(result) Then
                    minBarcode = Convert.ToInt32(result)
                End If
            End Using

        Catch ex As Exception
            CreateExceptionFile(ex.Message, q)
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

    Public Sub isPasswordEncrypted()
        If Not isConnOpen() Then
            MessageBox.Show(CANNOT_ACCESS_DB, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Try
            Using cmd As New OracleCommand(GET_ALL_PASS_ENCRYPTED, conn)
                cmd.CommandType = CommandType.Text
                Using dr As OracleDataReader = cmd.ExecuteReader()
                    If dr.Read() AndAlso Convert.ToInt32(dr(0)) = 0 Then
                        encryptPassword("all")
                    End If
                End Using
            End Using
        Catch ex As Exception
            createExceptionFile(ex.Message, " " & GET_ALL_PASS_ENCRYPTED)
            MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub encryptPassword(ByVal username As String)
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
            createExceptionFile(ex.Message, GET_ALL_USERS_AND_PASS)
            MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Public Function getEncryptedValue(ByVal value As String) As String
        Dim bArray() As Byte = System.Text.Encoding.UTF8.GetBytes(value)
        Dim sb64 As String = System.Convert.ToBase64String(bArray)
        Return sb64
    End Function

    Public Sub getStartDate()
        If Not isConnOpen() Then
            MessageBox.Show(CANNOT_ACCESS_DB, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Const sql As String = "SELECT TO_DATE(paramvalue, 'DD/MM/YY') FROM global_params WHERE paramkey = 'start.date'"

        Try
            Using cmd As New OracleCommand(sql, conn)
                cmd.CommandType = CommandType.Text
                Using dr As OracleDataReader = cmd.ExecuteReader()
                    If dr.Read() AndAlso Not dr.IsDBNull(0) Then
                        startDate = dr.GetDateTime(0)
                    End If
                End Using
            End Using

        Catch ex As Exception
            createExceptionFile(ex.Message, sql)
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
End Module
