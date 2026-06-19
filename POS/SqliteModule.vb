Imports System.Data.SQLite
Imports System.Web.UI.WebControls
Imports Microsoft.VisualBasic.ApplicationServices
Imports Oracle.DataAccess.Client

Module SqliteModule

    Public SqlLite As Boolean = False
    Public kioskId As String = ""

    Public Function SqlLiteEnabled() As Boolean
        Const FILE_NAME As String = "C:\sqlite.txt"

        If Not IO.File.Exists(FILE_NAME) Then
            Return False
        Else
            Try
                Using reader As New IO.StreamReader(FILE_NAME)
                    ' Read the first non-empty line and trim spaces
                    Dim line As String
                    Do
                        line = reader.ReadLine()
                        If line Is Nothing Then Exit Do
                        line = line.Trim()
                    Loop While String.IsNullOrEmpty(line)

                    kioskId = If(String.IsNullOrEmpty(line), String.Empty, line)
                    Using conn As New SQLiteConnection("Data Source=kiosk.db")
                        conn.Open()
                        Dim sql As String = "
                                CREATE TABLE IF NOT EXISTS KIOSK (
                                                        KIOSKID TEXT PRIMARY KEY,
                                                        DESCRIPTION TEXT
                                                    );

                                INSERT INTO KIOSK (KIOSKID) VALUES (@KIOSKID)
                                ON CONFLICT(KIOSKID)
                                DO UPDATE SET
                                KIOSKID = excluded.KIOSKID                                    
                                "

                        Using cmd As New SQLiteCommand(sql, conn)
                            cmd.Parameters.AddWithValue("@KIOSKID", If(kioskId, DBNull.Value))
                            cmd.ExecuteNonQuery()
                        End Using
                    End Using
                End Using
            Catch ex As Exception
                MessageBox.Show("Error reading kioskid: " + ex.Message, ERROR_MSG, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return String.Empty
            End Try
            Return True
        End If
    End Function
    Private Sub CreateSyncMetadataTable()
        Using conn As New SQLiteConnection("Data Source=kiosk.db")
            conn.Open()
            Dim sql As String = "
                    CREATE TABLE IF NOT EXISTS SYNC_METADATA (
                        KEY TEXT PRIMARY KEY,
                        KIOSKID TEXT,
                        VALUE TEXT
                    );
                "
            Using cmd As New SQLiteCommand(sql, conn)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Private Sub CreateGlobalParamsTable()
        Using conn As New SQLiteConnection("Data Source=kiosk.db")
            conn.Open()
            Dim sql As String = "
                    CREATE TABLE IF NOT EXISTS GLOBAL_PARAMS (
                        PARAMKEY TEXT PRIMARY KEY,
                        PARAMVALUE TEXT,
                        KIOSKID TEXT,
                        UPDATED_AT TEXT,
                        SYNC_STATUS INTEGER NOT NULL DEFAULT 0,
                        FOREIGN KEY (KIOSKID) REFERENCES KIOSK(KIOSKID)
                    );
                "
            Using cmd As New SQLiteCommand(sql, conn)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Private Sub CreateUsersTable()
        Using conn As New SQLiteConnection("Data Source=kiosk.db")
            conn.Open()
            Dim sql As String = "
                    CREATE TABLE IF NOT EXISTS USERS (
                        UUID TEXT PRIMARY KEY,
                        USERNAME TEXT,
                        DELETED INTEGER,
                        ACCESS_LEVEL INTEGER,
                        IS_UNLOCK INTEGER,
                        PASS TEXT,
                        FULLNAME TEXT,
                        PHONE TEXT,
                        ADDRESS TEXT,
                        ID_NUM TEXT,
                        DELETED_BY TEXT,
                        CREATED_BY TEXT,
                        VIEW_REPORTS INTEGER,
                        EDIT_PROD INTEGER,
                        EDIT_PROD_FULL INTEGER,
                        KIOSKID TEXT,
                        UPDATED_AT TEXT DEFAULT CURRENT_TIMESTAMP,
                        FOREIGN KEY (KIOSKID) REFERENCES KIOSK(KIOSKID)
                    );
                "
            Using cmd As New SQLiteCommand(sql, conn)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Private Sub CreateSessionsTable()
        Using conn As New SQLiteConnection("Data Source=kiosk.db")
            conn.Open()
            Dim sql As String = "
                    CREATE TABLE IF NOT EXISTS SESSIONS (
                        UUID TEXT PRIMARY KEY,
                        LOGIN_WHEN TEXT,
                        IS_ACTIVE INTEGER,
                        MACHINE_NAME TEXT,
                        USER_ID TEXT,
                        LOGOUT_WHEN TEXT,
                        AMOUNTLAXEIAONLOGIN REAL,
                        KIOSKID TEXT,
                        UPDATED_AT TEXT DEFAULT CURRENT_TIMESTAMP,
                        FOREIGN KEY (USER_ID) REFERENCES USERS(UUID),
                        FOREIGN KEY (KIOSKID) REFERENCES KIOSK(KIOSKID)
                    );

                    CREATE INDEX IF NOT EXISTS IDX_SESSIONS_USER_ID ON SESSIONS(USER_ID);
                    CREATE INDEX IF NOT EXISTS IDX_SESSIONS_UPDATED_AT ON SESSIONS(UPDATED_AT);
                    CREATE INDEX IF NOT EXISTS IDX_SESSIONS_KIOSKID ON SESSIONS(KIOSKID);
                "
            Using cmd As New SQLiteCommand(sql, conn)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Private Sub CreateLotteryTable()
        Using conn As New SQLiteConnection("Data Source=kiosk.db")
            conn.Open()
            Dim sql As String = "
                    CREATE TABLE IF NOT EXISTS LOTTERY (
                        BARCODE TEXT,
                        KIOSKID TEXT,
                        UPDATED_AT TEXT DEFAULT CURRENT_TIMESTAMP,
                        SYNC_STATUS INTEGER NOT NULL DEFAULT 0,
                        PRIMARY KEY (BARCODE, KIOSKID),                        
                        FOREIGN KEY (KIOSKID) REFERENCES KIOSK(KIOSKID)
                    );
                    CREATE INDEX IF NOT EXISTS IDX_LOTTERY_BARCODE ON LOTTERY(BARCODE, KIOSKID);
                "
            Using cmd As New SQLiteCommand(sql, conn)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Private Sub CreateProductsTable()
        Using conn As New SQLiteConnection("Data Source=kiosk.db")
            conn.Open()
            Dim sql As String = "
                    CREATE TABLE IF NOT EXISTS PRODUCTS (
                        DESCRIPTION TEXT,
                        MIN_QUANTITY INTEGER,
                        AVAIL_QUANTITY INTEGER NOT NULL,
                        ALERT_ON_MIN INTEGER,
                        EXPIRY_DATE TEXT,
                        ALERT_ON_EXPIRY INTEGER,
                        ALERT_DATE TEXT,
                        SERNO INTEGER,
                        BUY_AMT REAL,
                        SELL_AMT REAL,
                        CATEGORY_ID TEXT,
                        SUPPLIER_ID TEXT,
                        NOTES TEXT,
                        STOCK_QUANTITY INTEGER,
                        PROFIT_PERCENT REAL,
                        AMT_PROFIT REAL,
                        OFFER INTEGER,
                        OFFER_TYPE INTEGER,
                        OFFER_X INTEGER,
                        OFFER_Y INTEGER,
                        OFFER_DISC REAL,
                        OFFER_AT INTEGER,
                        LASTMODIFIEDBY TEXT,
                        LASTMODIFIEDSCREEN INTEGER,
                        VATTYPE_ID TEXT,
                        ISBOX INTEGER,
                        BOX_QNT INTEGER,
                        OFFERFROMDATE TEXT,
                        OFFERTODATE TEXT,
                        BUY_AMT_NO_VAT REAL,
                        BUY_AMT_NEW REAL,
                        KIOSKID TEXT,
                        UUID TEXT NOT NULL UNIQUE,
                        SYNC_STATUS INTEGER NOT NULL DEFAULT 0,
                        UPDATED_AT TEXT DEFAULT CURRENT_TIMESTAMP,                        
                        FOREIGN KEY (CATEGORY_ID) REFERENCES CATEGORIES(UUID),
                        FOREIGN KEY (SUPPLIER_ID) REFERENCES SUPPLIERS(UUID),
                        FOREIGN KEY (VATTYPE_ID) REFERENCES VAT_TYPES(UUID),
                        FOREIGN KEY (KIOSKID) REFERENCES KIOSK(KIOSKID)
                    );

                        CREATE INDEX IF NOT EXISTS IDX_PRODUCTS_SYNC ON PRODUCTS(KIOSKID, UPDATED_AT);
                        CREATE INDEX IF NOT EXISTS IDX_PRODUCTS_CATEGORY ON PRODUCTS(CATEGORY_ID);
                        CREATE INDEX IF NOT EXISTS IDX_PRODUCTS_SUPPLIER ON PRODUCTS(SUPPLIER_ID);
                        CREATE INDEX IF NOT EXISTS IDX_PRODUCTS_VATTYPE ON PRODUCTS(VATTYPE_ID);
                        CREATE INDEX IF NOT EXISTS IDX_PRODUCTS_DESCRIPTION ON PRODUCTS(DESCRIPTION);

                        CREATE TABLE IF NOT EXISTS PRODUCTS_AUDIT (
                            ID INTEGER PRIMARY KEY AUTOINCREMENT,
                            UUID TEXT,
                            PRODUCT_SERNO INTEGER,
                            PREV_QUANTITY INTEGER,
                            NEW_QUANTITY INTEGER,
                            MODIFIED_BY TEXT,
                            MODIFIED_WHEN TEXT,
                            PREV_ST_QNT INTEGER,
                            NEW_ST_QNT INTEGER,
                            OLD_PRICE REAL,
                            NEW_PRICE REAL,
                            SYNCED INTEGER DEFAULT 0
                        );

                        CREATE TRIGGER IF NOT EXISTS PRODUCTS_BEFORE_UPDATE
                        BEFORE UPDATE ON PRODUCTS
                        FOR EACH ROW
                        WHEN NEW.LASTMODIFIEDSCREEN = 1
                        BEGIN

                        INSERT INTO PRODUCTS_AUDIT
                        (
                            UUID,
                            PRODUCT_SERNO,
                            PREV_QUANTITY,
                            NEW_QUANTITY,
                            MODIFIED_BY,
                            MODIFIED_WHEN,
                            PREV_ST_QNT,
                            NEW_ST_QNT,
                            OLD_PRICE,
                            NEW_PRICE
                        )
                        VALUES
                        (
                            OLD.UUID,
                            OLD.SERNO,

                            OLD.AVAIL_QUANTITY,
                            NEW.AVAIL_QUANTITY,

                            NEW.LASTMODIFIEDBY,
                            CURRENT_TIMESTAMP,

                            OLD.STOCK_QUANTITY,
                            NEW.STOCK_QUANTITY,

                            OLD.SELL_AMT,
                            NEW.SELL_AMT
                        );

                        END;
                "
            Using cmd As New SQLiteCommand(sql, conn)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Private Sub CreateVatTypesTable()
        Using conn As New SQLiteConnection("Data Source=kiosk.db")
            conn.Open()
            Dim sql As String = "
                    CREATE TABLE IF NOT EXISTS VAT_TYPES (
                        UUID TEXT PRIMARY KEY,
                        DESCRIPTION TEXT,
                        VAT INTEGER,
                        KIOSKID TEXT,
                        UPDATED_AT TEXT DEFAULT CURRENT_TIMESTAMP,
                        FOREIGN KEY (KIOSKID) REFERENCES KIOSK(KIOSKID)
                    );
                    CREATE INDEX IF NOT EXISTS IDX_VAT_TYPES_SYNC ON VAT_TYPES(KIOSKID, UPDATED_AT);
                "
            Using cmd As New SQLiteCommand(sql, conn)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Private Sub CreateSuppliersTable()
        Using conn As New SQLiteConnection("Data Source=kiosk.db")
            conn.Open()
            Dim sql As String = "
                    CREATE TABLE IF NOT EXISTS SUPPLIERS (
                        UUID TEXT PRIMARY KEY,
                        S_NAME TEXT,
                        PHONE_1 TEXT,
                        PHONE_2 TEXT,
                        EMAIL TEXT,
                        CONTACT_NAME TEXT,
                        MON INTEGER,
                        TUE INTEGER,
                        WED INTEGER,
                        THU INTEGER,
                        FRI INTEGER,
                        NOTES TEXT,
                        WEB INTEGER,
                        ISDEFAULT INTEGER,
                        KIOSKID TEXT,
                        SYNC_STATUS INTEGER NOT NULL DEFAULT 0,
                        UPDATED_AT TEXT DEFAULT CURRENT_TIMESTAMP,
                        FOREIGN KEY (KIOSKID) REFERENCES KIOSK(KIOSKID)
                    );

                    CREATE INDEX IF NOT EXISTS IDX_SUPPLIERS_SYNC ON SUPPLIERS(KIOSKID, UPDATED_AT);
                    CREATE INDEX IF NOT EXISTS IDX_SUPPLIERS_NAME ON SUPPLIERS(S_NAME);
                "
            Using cmd As New SQLiteCommand(sql, conn)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Private Sub CreateCategoriesTable()
        Using conn As New SQLiteConnection("Data Source=kiosk.db")
            conn.Open()
            Dim sql As String = "
                    CREATE TABLE IF NOT EXISTS CATEGORIES (
                        UUID TEXT PRIMARY KEY,
                        DESCRIPTION TEXT,
                        VAT INTEGER,
                        KIOSKID TEXT,
                        UPDATED_AT TEXT DEFAULT CURRENT_TIMESTAMP,
                        SYNC_STATUS INTEGER NOT NULL DEFAULT 0,
                        FOREIGN KEY (KIOSKID) REFERENCES KIOSK(KIOSKID)
                    );
                    CREATE INDEX IF NOT EXISTS IDX_CATEGORIES_SYNC ON CATEGORIES(KIOSKID, UPDATED_AT);
                    CREATE INDEX IF NOT EXISTS IDX_CATEGORIES_DESCRIPTION ON CATEGORIES(DESCRIPTION);
                "
            Using cmd As New SQLiteCommand(sql, conn)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Private Sub CreateBarcodesTable()
        Using conn As New SQLiteConnection("Data Source=kiosk.db")
            conn.Open()
            Dim sql As String = "
                    CREATE TABLE IF NOT EXISTS BARCODES
                    (
                        BARCODE TEXT PRIMARY KEY,
                        PRODUCT_SERNO INTEGER,
                        PRODUCT_UUID TEXT,
                        KIOSKID TEXT,
                        UPDATED_AT TEXT,
                        FOREIGN KEY (KIOSKID) REFERENCES KIOSK(KIOSKID),
                        FOREIGN KEY (PRODUCT_UUID) REFERENCES PRODUCTS(UUID)
                    );
                    CREATE INDEX IF NOT EXISTS IDX_BARCODES_SYNC ON BARCODES(KIOSKID, UPDATED_AT);
                    CREATE INDEX IF NOT EXISTS IDX_BARCODES_PRODUCT_UUID ON BARCODES(PRODUCT_UUID);
                    CREATE INDEX IF NOT EXISTS IDX_BARCODES_PRODUCT_SERNO ON BARCODES(PRODUCT_SERNO);
                "
            Using cmd As New SQLiteCommand(sql, conn)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub
    Public Sub CreateSqliteTableStructure()
        Dim WhoAmI As String = "CreateSqliteDB"
        Try
            CreateSyncMetadataTable()
            CreateGlobalParamsTable()
            CreateUsersTable()
            CreateSessionsTable()
            CreateLotteryTable()
            CreateProductsTable()
            CreateVatTypesTable()
            CreateSuppliersTable()
            CreateCategoriesTable()
            CreateBarcodesTable()
        Catch ex As Exception
            CreateExceptionFile(WhoAmI + ": " + ex.Message, " ")
            MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Class SyncTables
        Private Function CreateCategoryUpsertCommand(conn As SQLiteConnection) As SQLiteCommand
            Dim sql As String =
                                "
                                INSERT INTO CATEGORIES
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
                                    @UPDATED_AT,
                                    0
                                )
                                ON CONFLICT(UUID)
                                DO UPDATE SET
                                    DESCRIPTION = excluded.DESCRIPTION,
                                    VAT = excluded.VAT,
                                    KIOSKID = excluded.KIOSKID,
                                    UPDATED_AT = excluded.UPDATED_AT,
                                    SYNC_STATUS=0;
                                "

            Dim cmd As New SQLiteCommand(sql, conn)

            cmd.Parameters.Add("@UUID", DbType.String)
            cmd.Parameters.Add("@DESCRIPTION", DbType.String)
            cmd.Parameters.Add("@VAT", DbType.Int32)
            cmd.Parameters.Add("@KIOSKID", DbType.String)
            cmd.Parameters.Add("@UPDATED_AT", DbType.String)
            Return cmd
        End Function

        Private Sub ExecuteCategoryUpsert(cmd As SQLiteCommand, dr As OracleDataReader)
            cmd.Parameters("@UUID").Value = If(IsDBNull(dr("UUID")), DBNull.Value, dr("UUID"))
            cmd.Parameters("@DESCRIPTION").Value = If(IsDBNull(dr("DESCRIPTION")), DBNull.Value, dr("DESCRIPTION"))
            cmd.Parameters("@VAT").Value = If(IsDBNull(dr("VAT")), DBNull.Value, dr("VAT"))
            cmd.Parameters("@KIOSKID").Value = If(IsDBNull(dr("KIOSKID")), DBNull.Value, dr("KIOSKID"))
            cmd.Parameters("@UPDATED_AT").Value = If(IsDBNull(dr("UPDATED_AT")), DBNull.Value, Convert.ToDateTime(dr("UPDATED_AT")).ToString("yyyy-MM-dd HH:mm:ss"))
            cmd.ExecuteNonQuery()
        End Sub

        Public Sub SyncCategories()
            Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                sqliteConn.Open()
                EnableForeignKeys(sqliteConn)
                Dim lastSync As DateTime = GetLastSync(sqliteConn, "CATEGORIES")

                Dim sql As String =
                                    "
                                    SELECT
                                        UUID,
                                        DESCRIPTION,
                                        VAT,
                                        KIOSKID,
                                        UPDATED_AT
                                    FROM CATEGORIES
                                    WHERE KIOSKID = :KIOSKID
                                    AND (UPDATED_AT IS NULL OR UPDATED_AT > :LASTSYNC)
                                    ORDER BY UPDATED_AT
                                    "

                Using oracleCmd As New OracleCommand(sql, conn)
                    oracleCmd.BindByName = True
                    oracleCmd.Parameters.Add("KIOSKID", OracleDbType.Varchar2).Value = kioskId
                    oracleCmd.Parameters.Add("LASTSYNC", OracleDbType.TimeStamp).Value = lastSync

                    Using reader = oracleCmd.ExecuteReader()
                        Dim newestTimestamp As DateTime = lastSync
                        Using trans = sqliteConn.BeginTransaction()

                            Dim upsertCmd = CreateCategoryUpsertCommand(sqliteConn)

                            While reader.Read()
                                ExecuteCategoryUpsert(upsertCmd, reader)

                                If Not IsDBNull(reader("UPDATED_AT")) Then
                                    Dim updatedAt As DateTime = Convert.ToDateTime(reader("UPDATED_AT"))
                                    If updatedAt > newestTimestamp Then
                                        newestTimestamp = updatedAt
                                    End If
                                End If
                            End While
                            trans.Commit()
                        End Using

                        SaveLastSync(sqliteConn, newestTimestamp, "CATEGORIES")
                    End Using
                End Using
            End Using
        End Sub

        Private Function CreateSupplierUpsertCommand(conn As SQLiteConnection) As SQLiteCommand
            Dim sql As String =
                                "
                                INSERT INTO SUPPLIERS
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
                                    WEB,
                                    ISDEFAULT,
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
                                    @WEB,
                                    @ISDEFAULT,
                                    @KIOSKID,
                                    @UPDATED_AT,
                                    0
                                )
                                ON CONFLICT(UUID)
                                DO UPDATE SET
                                    S_NAME = excluded.S_NAME,
                                    PHONE_1 = excluded.PHONE_1,
                                    PHONE_2 = excluded.PHONE_2,
                                    EMAIL = excluded.EMAIL,
                                    CONTACT_NAME = excluded.CONTACT_NAME,
                                    MON = excluded.MON,
                                    TUE = excluded.TUE,
                                    WED = excluded.WED,
                                    THU = excluded.THU,
                                    FRI = excluded.FRI,
                                    NOTES = excluded.NOTES,
                                    WEB = excluded.WEB,
                                    ISDEFAULT = excluded.ISDEFAULT,
                                    KIOSKID = excluded.KIOSKID,
                                    UPDATED_AT = excluded.UPDATED_AT,
                                    SYNC_STATUS = 0;
                                "

            Dim cmd As New SQLiteCommand(sql, conn)

            cmd.Parameters.Add("@UUID", DbType.String)
            cmd.Parameters.Add("@S_NAME", DbType.String)
            cmd.Parameters.Add("@PHONE_1", DbType.String)
            cmd.Parameters.Add("@PHONE_2", DbType.String)
            cmd.Parameters.Add("@EMAIL", DbType.String)
            cmd.Parameters.Add("@CONTACT_NAME", DbType.String)
            cmd.Parameters.Add("@MON", DbType.Int32)
            cmd.Parameters.Add("@TUE", DbType.Int32)
            cmd.Parameters.Add("@WED", DbType.Int32)
            cmd.Parameters.Add("@THU", DbType.Int32)
            cmd.Parameters.Add("@FRI", DbType.Int32)
            cmd.Parameters.Add("@NOTES", DbType.String)
            cmd.Parameters.Add("@WEB", DbType.Int32)
            cmd.Parameters.Add("@ISDEFAULT", DbType.Int32)
            cmd.Parameters.Add("@KIOSKID", DbType.String)
            cmd.Parameters.Add("@UPDATED_AT", DbType.String)
            Return cmd
        End Function

        Private Sub ExecuteSupplierUpsert(cmd As SQLiteCommand, dr As OracleDataReader)
            cmd.Parameters("@UUID").Value = If(IsDBNull(dr("UUID")), DBNull.Value, dr("UUID"))
            cmd.Parameters("@S_NAME").Value = If(IsDBNull(dr("S_NAME")), DBNull.Value, dr("S_NAME"))
            cmd.Parameters("@PHONE_1").Value = If(IsDBNull(dr("PHONE_1")), DBNull.Value, dr("PHONE_1"))
            cmd.Parameters("@PHONE_2").Value = If(IsDBNull(dr("PHONE_2")), DBNull.Value, dr("PHONE_2"))
            cmd.Parameters("@EMAIL").Value = If(IsDBNull(dr("EMAIL")), DBNull.Value, dr("EMAIL"))
            cmd.Parameters("@CONTACT_NAME").Value = If(IsDBNull(dr("CONTACT_NAME")), DBNull.Value, dr("CONTACT_NAME"))
            cmd.Parameters("@MON").Value = If(IsDBNull(dr("MON")), DBNull.Value, dr("MON"))
            cmd.Parameters("@TUE").Value = If(IsDBNull(dr("TUE")), DBNull.Value, dr("TUE"))
            cmd.Parameters("@WED").Value = If(IsDBNull(dr("WED")), DBNull.Value, dr("WED"))
            cmd.Parameters("@THU").Value = If(IsDBNull(dr("THU")), DBNull.Value, dr("THU"))
            cmd.Parameters("@FRI").Value = If(IsDBNull(dr("FRI")), DBNull.Value, dr("FRI"))
            cmd.Parameters("@NOTES").Value = If(IsDBNull(dr("NOTES")), DBNull.Value, dr("NOTES"))
            cmd.Parameters("@WEB").Value = If(IsDBNull(dr("WEB")), DBNull.Value, dr("WEB"))
            cmd.Parameters("@ISDEFAULT").Value = If(IsDBNull(dr("ISDEFAULT")), DBNull.Value, dr("ISDEFAULT"))
            cmd.Parameters("@KIOSKID").Value = If(IsDBNull(dr("KIOSKID")), DBNull.Value, dr("KIOSKID"))
            cmd.Parameters("@UPDATED_AT").Value = If(IsDBNull(dr("UPDATED_AT")), DBNull.Value, Convert.ToDateTime(dr("UPDATED_AT")).ToString("yyyy-MM-dd HH:mm:ss"))
            cmd.ExecuteNonQuery()
        End Sub

        Public Sub SyncSuppliers()

            Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                sqliteConn.Open()
                EnableForeignKeys(sqliteConn)
                Dim lastSync As DateTime = GetLastSync(sqliteConn, "SUPPLIERS")

                Dim sql As String =
                                    "
                                    SELECT
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
                                        WEB,
                                        ISDEFAULT,
                                        KIOSKID,
                                        UPDATED_AT
                                    FROM SUPPLIERS
                                    WHERE KIOSKID = :KIOSKID
                                    AND (UPDATED_AT IS NULL OR UPDATED_AT > :LASTSYNC)
                                    ORDER BY UPDATED_AT
                                    "

                Using oracleCmd As New OracleCommand(sql, conn)
                    oracleCmd.BindByName = True
                    oracleCmd.Parameters.Add("KIOSKID", OracleDbType.Varchar2).Value = kioskId
                    oracleCmd.Parameters.Add("LASTSYNC", OracleDbType.TimeStamp).Value = lastSync

                    Using reader = oracleCmd.ExecuteReader()
                        Dim newestTimestamp As DateTime = lastSync
                        Using trans = sqliteConn.BeginTransaction()
                            Dim upsertCmd = CreateSupplierUpsertCommand(sqliteConn)

                            While reader.Read()
                                ExecuteSupplierUpsert(upsertCmd, reader)

                                If Not IsDBNull(reader("UPDATED_AT")) Then
                                    Dim updatedAt As DateTime = Convert.ToDateTime(reader("UPDATED_AT"))
                                    If updatedAt > newestTimestamp Then
                                        newestTimestamp = updatedAt
                                    End If
                                End If
                            End While
                            trans.Commit()
                        End Using

                        SaveLastSync(sqliteConn, newestTimestamp, "SUPPLIERS")
                    End Using
                End Using
            End Using
        End Sub

        Public Sub SyncGlobalParams()
            Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                sqliteConn.Open()
                EnableForeignKeys(sqliteConn)
                Dim lastSync As DateTime = GetLastSync(sqliteConn, "GLOBAL_PARAMS")

                Dim sql As String = "
                                    SELECT
                                        PARAMKEY,
                                        PARAMVALUE,
                                        KIOSKID,
                                        UPDATED_AT
                                    FROM GLOBAL_PARAMS
                                    WHERE KIOSKID = :KIOSKID AND (UPDATED_AT IS NULL OR UPDATED_AT > :LASTSYNC)
                                    ORDER BY UPDATED_AT"

                Using cmd As New OracleCommand(sql, conn)
                    cmd.BindByName = True
                    cmd.Parameters.Add("KIOSKID", OracleDbType.Varchar2).Value = kioskId
                    cmd.Parameters.Add("LASTSYNC", OracleDbType.TimeStamp).Value = lastSync
                    Using reader = cmd.ExecuteReader()
                        Dim newestTimestamp As DateTime = lastSync

                        While reader.Read()
                            Dim updatedAt As DateTime = If(IsDBNull(reader("UPDATED_AT")), "01/01/1900", Convert.ToDateTime(reader("UPDATED_AT")))

                            UpsertGlobalParam(
                                    sqliteConn,
                                    reader("PARAMKEY").ToString(),
                                    If(IsDBNull(reader("PARAMVALUE")),
                                       Nothing,
                                       reader("PARAMVALUE").ToString()),
                                    If(IsDBNull(reader("KIOSKID")),
                                       Nothing,
                                       reader("KIOSKID").ToString()),
                                    updatedAt)

                            If updatedAt > newestTimestamp Then
                                newestTimestamp = updatedAt
                            End If
                        End While

                        SaveLastSync(sqliteConn, newestTimestamp, "GLOBAL_PARAMS")
                    End Using
                End Using
            End Using
        End Sub

        Private Function CreateVatTypeUpsertCommand(conn As SQLiteConnection) As SQLiteCommand
            Dim sql As String =
                                "
                                INSERT INTO VAT_TYPES
                                (
                                    UUID,
                                    DESCRIPTION,
                                    VAT,
                                    KIOSKID,
                                    UPDATED_AT
                                )
                                VALUES
                                (
                                    @UUID,
                                    @DESCRIPTION,
                                    @VAT,
                                    @KIOSKID,
                                    @UPDATED_AT
                                )
                                ON CONFLICT(UUID)
                                DO UPDATE SET
                                    DESCRIPTION = excluded.DESCRIPTION,
                                    VAT = excluded.VAT,
                                    KIOSKID = excluded.KIOSKID,
                                    UPDATED_AT = excluded.UPDATED_AT
                                "

            Dim cmd As New SQLiteCommand(sql, conn)
            cmd.Parameters.Add("@UUID", DbType.String)
            cmd.Parameters.Add("@DESCRIPTION", DbType.String)
            cmd.Parameters.Add("@VAT", DbType.Int32)
            cmd.Parameters.Add("@KIOSKID", DbType.String)
            cmd.Parameters.Add("@UPDATED_AT", DbType.String)
            Return cmd
        End Function

        Private Sub ExecuteVatTypeUpsert(cmd As SQLiteCommand, dr As OracleDataReader)
            cmd.Parameters("@UUID").Value = If(IsDBNull(dr("UUID")), DBNull.Value, dr("UUID"))
            cmd.Parameters("@DESCRIPTION").Value = If(IsDBNull(dr("DESCRIPTION")), DBNull.Value, dr("DESCRIPTION"))
            cmd.Parameters("@VAT").Value = If(IsDBNull(dr("VAT")), DBNull.Value, dr("VAT"))
            cmd.Parameters("@KIOSKID").Value = If(IsDBNull(dr("KIOSKID")), DBNull.Value, dr("KIOSKID"))
            cmd.Parameters("@UPDATED_AT").Value = If(IsDBNull(dr("UPDATED_AT")), DBNull.Value, Convert.ToDateTime(dr("UPDATED_AT")).ToString("yyyy-MM-dd HH:mm:ss"))
            cmd.ExecuteNonQuery()
        End Sub

        Public Sub SyncVatTypes()
            Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                sqliteConn.Open()
                EnableForeignKeys(sqliteConn)
                Dim lastSync As DateTime = GetLastSync(sqliteConn, "VAT_TYPES")

                Dim sql As String =
                                    "
                                    SELECT
                                        UUID,
                                        DESCRIPTION,
                                        VAT,
                                        KIOSKID,
                                        UPDATED_AT
                                    FROM VAT_TYPES
                                    WHERE KIOSKID = :KIOSKID
                                    AND (UPDATED_AT IS NULL OR UPDATED_AT > :LASTSYNC)
                                    ORDER BY UPDATED_AT
                                    "

                Using oracleCmd As New OracleCommand(sql, conn)
                    oracleCmd.BindByName = True
                    oracleCmd.Parameters.Add("KIOSKID", OracleDbType.Varchar2).Value = kioskId
                    oracleCmd.Parameters.Add("LASTSYNC", OracleDbType.TimeStamp).Value = lastSync
                    Using reader = oracleCmd.ExecuteReader()
                        Dim newestTimestamp As DateTime = lastSync
                        Using trans = sqliteConn.BeginTransaction()
                            Dim upsertCmd = CreateVatTypeUpsertCommand(sqliteConn)
                            While reader.Read()
                                ExecuteVatTypeUpsert(upsertCmd, reader)

                                If Not IsDBNull(reader("UPDATED_AT")) Then
                                    Dim updatedAt As DateTime = Convert.ToDateTime(reader("UPDATED_AT"))

                                    If updatedAt > newestTimestamp Then
                                        newestTimestamp = updatedAt
                                    End If
                                End If
                            End While
                            trans.Commit()
                        End Using

                        SaveLastSync(sqliteConn, newestTimestamp, "VAT_TYPES")
                    End Using
                End Using
            End Using
        End Sub

        Public Sub SyncUsers()
            Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                sqliteConn.Open()
                EnableForeignKeys(sqliteConn)

                Dim lastSync As DateTime = GetLastSync(sqliteConn, "USERS")
                Dim sql As String =
                                    "
                                    SELECT
                                        UUID,
                                        USERNAME,
                                        DELETED,
                                        ACCESS_LEVEL,
                                        IS_UNLOCK,
                                        PASS,
                                        FULLNAME,
                                        PHONE,
                                        ADDRESS,
                                        ID_NUM,
                                        DELETED_BY,
                                        CREATED_BY,
                                        VIEW_REPORTS,
                                        EDIT_PROD,
                                        EDIT_PROD_FULL,
                                        KIOSKID,
                                        UPDATED_AT
                                    FROM USERS
                                    WHERE KIOSKID = :KIOSKID
                                    AND (UPDATED_AT IS NULL OR UPDATED_AT > :LASTSYNC)
                                    ORDER BY UPDATED_AT
                                    "

                Using cmd As New OracleCommand(sql, conn)
                    cmd.BindByName = True
                    cmd.Parameters.Add("KIOSKID", OracleDbType.Varchar2).Value = kioskId
                    cmd.Parameters.Add("LASTSYNC", OracleDbType.TimeStamp).Value = lastSync
                    Using reader = cmd.ExecuteReader()
                        Dim newestTimestamp As DateTime = lastSync
                        While reader.Read()
                            Dim updatedAt As DateTime = If(IsDBNull(reader("UPDATED_AT")), New DateTime(1900, 1, 1), Convert.ToDateTime(reader("UPDATED_AT")))

                            UpsertUser(
                                    sqliteConn,
                                    reader("UUID").ToString(),
                                    If(IsDBNull(reader("USERNAME")), Nothing, reader("USERNAME").ToString()),
                                    If(IsDBNull(reader("DELETED")), 0, Convert.ToInt32(reader("DELETED"))),
                                    If(IsDBNull(reader("ACCESS_LEVEL")), 0, Convert.ToInt32(reader("ACCESS_LEVEL"))),
                                    If(IsDBNull(reader("IS_UNLOCK")), 0, Convert.ToInt32(reader("IS_UNLOCK"))),
                                    If(IsDBNull(reader("PASS")), Nothing, reader("PASS").ToString()),
                                    If(IsDBNull(reader("FULLNAME")), Nothing, reader("FULLNAME").ToString()),
                                    If(IsDBNull(reader("PHONE")), Nothing, reader("PHONE").ToString()),
                                    If(IsDBNull(reader("ADDRESS")), Nothing, reader("ADDRESS").ToString()),
                                    If(IsDBNull(reader("ID_NUM")), Nothing, reader("ID_NUM").ToString()),
                                    If(IsDBNull(reader("DELETED_BY")), Nothing, reader("DELETED_BY").ToString()),
                                    If(IsDBNull(reader("CREATED_BY")), Nothing, reader("CREATED_BY").ToString()),
                                    If(IsDBNull(reader("VIEW_REPORTS")), 0, Convert.ToInt32(reader("VIEW_REPORTS"))),
                                    If(IsDBNull(reader("EDIT_PROD")), 0, Convert.ToInt32(reader("EDIT_PROD"))),
                                    If(IsDBNull(reader("EDIT_PROD_FULL")), 0, Convert.ToInt32(reader("EDIT_PROD_FULL"))),
                                    If(IsDBNull(reader("KIOSKID")), Nothing, reader("KIOSKID").ToString()),
                                    updatedAt)

                            If updatedAt > newestTimestamp Then
                                newestTimestamp = updatedAt
                            End If
                        End While

                        SaveLastSync(sqliteConn, newestTimestamp, "USERS")
                    End Using
                End Using
            End Using
        End Sub

        Private Sub UpsertUser(
                                conn As SQLiteConnection,
                                uuid As String,
                                username As String,
                                deleted As Integer,
                                accessLevel As Integer,
                                isUnlock As Integer,
                                pass As String,
                                fullName As String,
                                phone As String,
                                address As String,
                                idNum As String,
                                deletedBy As String,
                                createdBy As String,
                                viewReports As Integer,
                                editProd As Integer,
                                editProdFull As Integer,
                                kioskId As String,
                                updatedAt As DateTime)

            Dim sql As String =
                                "
                                INSERT INTO USERS
                                (
                                    UUID,
                                    USERNAME,
                                    DELETED,
                                    ACCESS_LEVEL,
                                    IS_UNLOCK,
                                    PASS,
                                    FULLNAME,
                                    PHONE,
                                    ADDRESS,
                                    ID_NUM,
                                    DELETED_BY,
                                    CREATED_BY,
                                    VIEW_REPORTS,
                                    EDIT_PROD,
                                    EDIT_PROD_FULL,
                                    KIOSKID,
                                    UPDATED_AT
                                )
                                VALUES
                                (
                                    @UUID,
                                    @USERNAME,
                                    @DELETED,
                                    @ACCESS_LEVEL,
                                    @IS_UNLOCK,
                                    @PASS,
                                    @FULLNAME,
                                    @PHONE,
                                    @ADDRESS,
                                    @ID_NUM,
                                    @DELETED_BY,
                                    @CREATED_BY,
                                    @VIEW_REPORTS,
                                    @EDIT_PROD,
                                    @EDIT_PROD_FULL,
                                    @KIOSKID,
                                    @UPDATED_AT
                                )
                                ON CONFLICT(UUID)
                                DO UPDATE SET
                                    USERNAME = excluded.USERNAME,
                                    DELETED = excluded.DELETED,
                                    ACCESS_LEVEL = excluded.ACCESS_LEVEL,
                                    IS_UNLOCK = excluded.IS_UNLOCK,
                                    PASS = excluded.PASS,
                                    FULLNAME = excluded.FULLNAME,
                                    PHONE = excluded.PHONE,
                                    ADDRESS = excluded.ADDRESS,
                                    ID_NUM = excluded.ID_NUM,
                                    DELETED_BY = excluded.DELETED_BY,
                                    CREATED_BY = excluded.CREATED_BY,
                                    VIEW_REPORTS = excluded.VIEW_REPORTS,
                                    EDIT_PROD = excluded.EDIT_PROD,
                                    EDIT_PROD_FULL = excluded.EDIT_PROD_FULL,
                                    KIOSKID = excluded.KIOSKID,
                                    UPDATED_AT = excluded.UPDATED_AT
                                "

            Using cmd As New SQLiteCommand(sql, conn)
                cmd.Parameters.AddWithValue("@UUID", uuid)
                cmd.Parameters.AddWithValue("@USERNAME", If(username, DBNull.Value))
                cmd.Parameters.AddWithValue("@DELETED", deleted)
                cmd.Parameters.AddWithValue("@ACCESS_LEVEL", accessLevel)
                cmd.Parameters.AddWithValue("@IS_UNLOCK", isUnlock)
                cmd.Parameters.AddWithValue("@PASS", If(pass, DBNull.Value))
                cmd.Parameters.AddWithValue("@FULLNAME", If(fullName, DBNull.Value))
                cmd.Parameters.AddWithValue("@PHONE", If(phone, DBNull.Value))
                cmd.Parameters.AddWithValue("@ADDRESS", If(address, DBNull.Value))
                cmd.Parameters.AddWithValue("@ID_NUM", If(idNum, DBNull.Value))
                cmd.Parameters.AddWithValue("@DELETED_BY", If(deletedBy, DBNull.Value))
                cmd.Parameters.AddWithValue("@CREATED_BY", If(createdBy, DBNull.Value))
                cmd.Parameters.AddWithValue("@VIEW_REPORTS", viewReports)
                cmd.Parameters.AddWithValue("@EDIT_PROD", editProd)
                cmd.Parameters.AddWithValue("@EDIT_PROD_FULL", editProdFull)
                cmd.Parameters.AddWithValue("@KIOSKID", If(kioskId, DBNull.Value))
                cmd.Parameters.AddWithValue("@UPDATED_AT", updatedAt.ToString("yyyy-MM-dd HH:mm:ss"))
                cmd.ExecuteNonQuery()
            End Using
        End Sub

        Private Sub UpsertGlobalParam(conn As SQLiteConnection, paramKey As String, paramValue As String, kioskId As String, updatedAt As DateTime)
            Dim sql As String = "
                                INSERT INTO GLOBAL_PARAMS
                                (
                                    PARAMKEY,
                                    PARAMVALUE,
                                    KIOSKID,
                                    UPDATED_AT,
                                    SYNC_STATUS
                                )
                                VALUES
                                (
                                    @PARAMKEY,
                                    @PARAMVALUE,
                                    @KIOSKID,
                                    @UPDATED_AT,
                                    0
                                )

                                ON CONFLICT(PARAMKEY)
                                DO UPDATE SET
                                    KIOSKID = excluded.KIOSKID,
                                    PARAMVALUE=excluded.PARAMVALUE,
                                    UPDATED_AT=excluded.UPDATED_AT,
                                    SYNC_STATUS=0;
                                "

            Using cmd As New SQLiteCommand(sql, conn)
                cmd.Parameters.AddWithValue("@PARAMKEY", paramKey)
                cmd.Parameters.AddWithValue("@PARAMVALUE", If(paramValue, DBNull.Value))
                cmd.Parameters.AddWithValue("@KIOSKID", If(kioskId, DBNull.Value))
                cmd.Parameters.AddWithValue("@UPDATED_AT", updatedAt.ToString("yyyy-MM-dd HH:mm:ss"))
                cmd.ExecuteNonQuery()
            End Using
        End Sub

        Public Sub SyncLottery()
            Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                sqliteConn.Open()
                EnableForeignKeys(sqliteConn)
                Dim lastSync As DateTime = GetLastSync(sqliteConn, "LOTTERY")
                Dim sql As String =
                                    "
                                    SELECT
                                        BARCODE,
                                        KIOSKID,
                                        UPDATED_AT
                                    FROM LOTTERY
                                    WHERE KIOSKID = :KIOSKID
                                    AND (UPDATED_AT IS NULL OR UPDATED_AT > :LASTSYNC)
                                    ORDER BY UPDATED_AT
                                    "

                Using cmd As New OracleCommand(sql, conn)
                    cmd.BindByName = True
                    cmd.Parameters.Add("KIOSKID", OracleDbType.Varchar2).Value = kioskId
                    cmd.Parameters.Add("LASTSYNC", OracleDbType.TimeStamp).Value = lastSync
                    Using reader = cmd.ExecuteReader()
                        Dim newestTimestamp As DateTime = lastSync
                        While reader.Read()
                            Dim updatedAt As DateTime = If(IsDBNull(reader("UPDATED_AT")), New DateTime(1900, 1, 1), Convert.ToDateTime(reader("UPDATED_AT")))

                            UpsertLottery(
                                        sqliteConn,
                                        reader("BARCODE").ToString(),
                                        If(IsDBNull(reader("KIOSKID")),
                                           Nothing,
                                           reader("KIOSKID").ToString()),
                                        updatedAt
                                         )

                            If updatedAt > newestTimestamp Then
                                newestTimestamp = updatedAt
                            End If
                        End While
                        SaveLastSync(sqliteConn, newestTimestamp, "LOTTERY")
                    End Using
                End Using
            End Using
        End Sub

        Private Sub UpsertLottery(conn As SQLiteConnection, barcode As String, kioskId As String, updatedAt As DateTime)
            Dim sql As String =
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
                                    @UPDATED_AT,
                                    0
                                )
                                ON CONFLICT(BARCODE,KIOSKID)
                                DO UPDATE SET
                                    KIOSKID = excluded.KIOSKID,
                                    UPDATED_AT = excluded.UPDATED_AT,
                                    SYNC_STATUS = 0
                                "

            Using cmd As New SQLiteCommand(sql, conn)
                cmd.Parameters.AddWithValue("@BARCODE", barcode)
                If kioskId Is Nothing Then
                    cmd.Parameters.AddWithValue("@KIOSKID", DBNull.Value)
                Else
                    cmd.Parameters.AddWithValue("@KIOSKID", kioskId)
                End If
                cmd.Parameters.AddWithValue("@UPDATED_AT", updatedAt.ToString("yyyy-MM-dd HH:mm:ss"))
                cmd.ExecuteNonQuery()
            End Using
        End Sub

        Public Sub SyncBarcodes()
            Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                sqliteConn.Open()
                EnableForeignKeys(sqliteConn)
                Dim lastSync As DateTime = GetLastSync(sqliteConn, "BARCODES")

                Dim sql As String =
                                    "
                                    SELECT
                                        BARCODE,
                                        PRODUCT_SERNO,
                                        PRODUCT_UUID,
                                        KIOSKID,
                                        UPDATED_AT
                                    FROM BARCODES
                                    WHERE KIOSKID = :KIOSKID
                                    AND (UPDATED_AT IS NULL OR UPDATED_AT > :LASTSYNC)
                                    ORDER BY UPDATED_AT
                                    "

                Using oracleCmd As New OracleCommand(sql, conn)
                    oracleCmd.BindByName = True
                    oracleCmd.Parameters.Add("KIOSKID", OracleDbType.Varchar2).Value = kioskId
                    oracleCmd.Parameters.Add("LASTSYNC", OracleDbType.TimeStamp).Value = lastSync

                    Using reader = oracleCmd.ExecuteReader()
                        Dim newestTimestamp As DateTime = lastSync
                        Using trans = sqliteConn.BeginTransaction()
                            Dim cmd = CreateBarcodeUpsertCommand(sqliteConn)
                            cmd.Transaction = trans
                            While reader.Read()
                                ExecuteBarcodeUpsert(cmd, reader)
                                If Not IsDBNull(reader("UPDATED_AT")) Then
                                    Dim ts = Convert.ToDateTime(reader("UPDATED_AT"))
                                    If ts > newestTimestamp Then
                                        newestTimestamp = ts
                                    End If
                                End If
                            End While
                            trans.Commit()
                        End Using

                        SaveLastSync(sqliteConn, newestTimestamp, "BARCODES")
                    End Using
                End Using
            End Using
        End Sub

        Private Function CreateBarcodeUpsertCommand(conn As SQLiteConnection) As SQLiteCommand
            Dim sql As String =
                                "
                                INSERT INTO BARCODES
                                (
                                    BARCODE,
                                    PRODUCT_SERNO,
                                    PRODUCT_UUID,
                                    KIOSKID,
                                    UPDATED_AT
                                )
                                VALUES
                                (
                                    @BARCODE,
                                    @PRODUCT_SERNO,
                                    @PRODUCT_UUID,
                                    @KIOSKID,
                                    @UPDATED_AT
                                )

                                ON CONFLICT(BARCODE)

                                DO UPDATE SET

                                    PRODUCT_SERNO = excluded.PRODUCT_SERNO,
                                    PRODUCT_UUID = excluded.PRODUCT_UUID,
                                    KIOSKID = excluded.KIOSKID,
                                    UPDATED_AT = excluded.UPDATED_AT;
"

            Dim cmd As New SQLiteCommand(sql, conn)
            cmd.Parameters.Add("@BARCODE", DbType.String)
            cmd.Parameters.Add("@PRODUCT_SERNO", DbType.Int64)
            cmd.Parameters.Add("@PRODUCT_UUID", DbType.String)
            cmd.Parameters.Add("@KIOSKID", DbType.String)
            cmd.Parameters.Add("@UPDATED_AT", DbType.String)
            Return cmd
        End Function

        Private Sub ExecuteBarcodeUpsert(cmd As SQLiteCommand, dr As OracleDataReader)
            cmd.Parameters("@BARCODE").Value = If(IsDBNull(dr("BARCODE")), DBNull.Value, dr("BARCODE"))
            cmd.Parameters("@PRODUCT_SERNO").Value = If(IsDBNull(dr("PRODUCT_SERNO")), DBNull.Value, dr("PRODUCT_SERNO"))
            cmd.Parameters("@PRODUCT_UUID").Value = If(IsDBNull(dr("PRODUCT_UUID")), DBNull.Value, dr("PRODUCT_UUID"))
            cmd.Parameters("@KIOSKID").Value = If(IsDBNull(dr("KIOSKID")), DBNull.Value, dr("KIOSKID"))
            cmd.Parameters("@UPDATED_AT").Value = If(IsDBNull(dr("UPDATED_AT")), DBNull.Value, Convert.ToDateTime(dr("UPDATED_AT")).ToString("yyyy-MM-dd HH:mm:ss"))
            cmd.ExecuteNonQuery()
        End Sub

        Private Function CreateProductUpsertCommand(conn As SQLiteConnection) As SQLiteCommand
            Dim sql As String =
                                "
                                INSERT INTO PRODUCTS
                                (
                                UUID,                                
                                DESCRIPTION,
                                MIN_QUANTITY,
                                AVAIL_QUANTITY,
                                ALERT_ON_MIN,
                                EXPIRY_DATE,
                                ALERT_ON_EXPIRY,
                                ALERT_DATE,
                                SERNO,
                                BUY_AMT,
                                SELL_AMT,
                                CATEGORY_ID,
                                SUPPLIER_ID,
                                NOTES,
                                STOCK_QUANTITY,
                                PROFIT_PERCENT,
                                AMT_PROFIT,
                                OFFER,
                                OFFER_TYPE,
                                OFFER_X,
                                OFFER_Y,
                                OFFER_DISC,
                                OFFER_AT,
                                LASTMODIFIEDBY,
                                LASTMODIFIEDSCREEN,
                                VATTYPE_ID,
                                ISBOX,
                                BOX_QNT,
                                OFFERFROMDATE,
                                OFFERTODATE,
                                BUY_AMT_NO_VAT,
                                BUY_AMT_NEW,
                                KIOSKID,
                                UPDATED_AT,
                                SYNC_STATUS
                                )
                                VALUES
                                (
                                @UUID,
                                @DESCRIPTION,
                                @MIN_QUANTITY,
                                @AVAIL_QUANTITY,
                                @ALERT_ON_MIN,
                                @EXPIRY_DATE,
                                @ALERT_ON_EXPIRY,
                                @ALERT_DATE,
                                @SERNO,
                                @BUY_AMT,
                                @SELL_AMT,
                                @CATEGORY_ID,
                                @SUPPLIER_ID,
                                @NOTES,
                                @STOCK_QUANTITY,
                                @PROFIT_PERCENT,
                                @AMT_PROFIT,
                                @OFFER,
                                @OFFER_TYPE,
                                @OFFER_X,
                                @OFFER_Y,
                                @OFFER_DISC,
                                @OFFER_AT,
                                @LASTMODIFIEDBY,
                                @LASTMODIFIEDSCREEN,
                                @VATTYPE_ID,
                                @ISBOX,
                                @BOX_QNT,
                                @OFFERFROMDATE,
                                @OFFERTODATE,
                                @BUY_AMT_NO_VAT,
                                @BUY_AMT_NEW,
                                @KIOSKID,
                                @UPDATED_AT,
                                0
                                )
                                ON CONFLICT(UUID)
                                DO UPDATE SET

                                DESCRIPTION=excluded.DESCRIPTION,
                                MIN_QUANTITY=excluded.MIN_QUANTITY,
                                AVAIL_QUANTITY=excluded.AVAIL_QUANTITY,
                                ALERT_ON_MIN=excluded.ALERT_ON_MIN,
                                EXPIRY_DATE=excluded.EXPIRY_DATE,
                                ALERT_ON_EXPIRY=excluded.ALERT_ON_EXPIRY,
                                ALERT_DATE=excluded.ALERT_DATE,
                                BUY_AMT=excluded.BUY_AMT,
                                SELL_AMT=excluded.SELL_AMT,
                                CATEGORY_ID=excluded.CATEGORY_ID,
                                SUPPLIER_ID=excluded.SUPPLIER_ID,
                                NOTES=excluded.NOTES,
                                STOCK_QUANTITY=excluded.STOCK_QUANTITY,
                                PROFIT_PERCENT=excluded.PROFIT_PERCENT,
                                AMT_PROFIT=excluded.AMT_PROFIT,
                                OFFER=excluded.OFFER,
                                OFFER_TYPE=excluded.OFFER_TYPE,
                                OFFER_X=excluded.OFFER_X,
                                OFFER_Y=excluded.OFFER_Y,
                                OFFER_DISC=excluded.OFFER_DISC,
                                OFFER_AT=excluded.OFFER_AT,
                                LASTMODIFIEDBY=excluded.LASTMODIFIEDBY,
                                LASTMODIFIEDSCREEN=excluded.LASTMODIFIEDSCREEN,
                                VATTYPE_ID=excluded.VATTYPE_ID,
                                ISBOX=excluded.ISBOX,
                                BOX_QNT=excluded.BOX_QNT,
                                OFFERFROMDATE=excluded.OFFERFROMDATE,
                                OFFERTODATE=excluded.OFFERTODATE,
                                BUY_AMT_NO_VAT=excluded.BUY_AMT_NO_VAT,
                                BUY_AMT_NEW=excluded.BUY_AMT_NEW,
                                KIOSKID=excluded.KIOSKID,
                                UPDATED_AT=excluded.UPDATED_AT,
                                SYNC_STATUS=excluded.SYNC_STATUS
                                "

            Dim cmd As New SQLiteCommand(sql, conn)
            cmd.Parameters.Add("@UUID", DbType.String)
            cmd.Parameters.Add("@DESCRIPTION", DbType.String)
            cmd.Parameters.Add("@MIN_QUANTITY", DbType.Int32)
            cmd.Parameters.Add("@AVAIL_QUANTITY", DbType.Int32)
            cmd.Parameters.Add("@ALERT_ON_MIN", DbType.Int32)
            cmd.Parameters.Add("@EXPIRY_DATE", DbType.String)
            cmd.Parameters.Add("@ALERT_ON_EXPIRY", DbType.Int32)
            cmd.Parameters.Add("@ALERT_DATE", DbType.String)
            cmd.Parameters.Add("@SERNO", DbType.Int64)
            cmd.Parameters.Add("@BUY_AMT", DbType.Decimal)
            cmd.Parameters.Add("@SELL_AMT", DbType.Decimal)
            cmd.Parameters.Add("@CATEGORY_ID", DbType.String)
            cmd.Parameters.Add("@SUPPLIER_ID", DbType.String)
            cmd.Parameters.Add("@NOTES", DbType.String)
            cmd.Parameters.Add("@STOCK_QUANTITY", DbType.Int32)
            cmd.Parameters.Add("@PROFIT_PERCENT", DbType.Decimal)
            cmd.Parameters.Add("@AMT_PROFIT", DbType.Decimal)
            cmd.Parameters.Add("@OFFER", DbType.Int32)
            cmd.Parameters.Add("@OFFER_TYPE", DbType.Int32)
            cmd.Parameters.Add("@OFFER_X", DbType.Int32)
            cmd.Parameters.Add("@OFFER_Y", DbType.Int32)
            cmd.Parameters.Add("@OFFER_DISC", DbType.Decimal)
            cmd.Parameters.Add("@OFFER_AT", DbType.Int32)
            cmd.Parameters.Add("@LASTMODIFIEDBY", DbType.String)
            cmd.Parameters.Add("@LASTMODIFIEDSCREEN", DbType.Int32)
            cmd.Parameters.Add("@VATTYPE_ID", DbType.String)
            cmd.Parameters.Add("@ISBOX", DbType.Int32)
            cmd.Parameters.Add("@BOX_QNT", DbType.Int32)
            cmd.Parameters.Add("@OFFERFROMDATE", DbType.String)
            cmd.Parameters.Add("@OFFERTODATE", DbType.String)
            cmd.Parameters.Add("@BUY_AMT_NO_VAT", DbType.Decimal)
            cmd.Parameters.Add("@BUY_AMT_NEW", DbType.Decimal)
            cmd.Parameters.Add("@KIOSKID", DbType.String)
            cmd.Parameters.Add("@UPDATED_AT", DbType.String)
            Return cmd
        End Function

        Private Sub ExecuteProductUpsert(cmd As SQLiteCommand, dr As OracleDataReader)
            cmd.Parameters("@UUID").Value = dr("UUID")
            cmd.Parameters("@DESCRIPTION").Value = If(IsDBNull(dr("DESCRIPTION")), DBNull.Value, dr("DESCRIPTION"))
            cmd.Parameters("@MIN_QUANTITY").Value = If(IsDBNull(dr("MIN_QUANTITY")), DBNull.Value, dr("MIN_QUANTITY"))
            cmd.Parameters("@AVAIL_QUANTITY").Value = If(IsDBNull(dr("AVAIL_QUANTITY")), 0, dr("AVAIL_QUANTITY"))
            cmd.Parameters("@ALERT_ON_MIN").Value = If(IsDBNull(dr("ALERT_ON_MIN")), DBNull.Value, dr("ALERT_ON_MIN"))
            cmd.Parameters("@EXPIRY_DATE").Value = If(IsDBNull(dr("EXPIRY_DATE")), DBNull.Value, Convert.ToDateTime(dr("EXPIRY_DATE")).ToString("yyyy-MM-dd"))
            cmd.Parameters("@ALERT_ON_EXPIRY").Value = If(IsDBNull(dr("ALERT_ON_EXPIRY")), DBNull.Value, dr("ALERT_ON_EXPIRY"))
            cmd.Parameters("@ALERT_DATE").Value = If(IsDBNull(dr("ALERT_DATE")), DBNull.Value, Convert.ToDateTime(dr("ALERT_DATE")).ToString("yyyy-MM-dd"))
            cmd.Parameters("@SERNO").Value = dr("SERNO")
            cmd.Parameters("@BUY_AMT").Value = If(IsDBNull(dr("BUY_AMT")), DBNull.Value, dr("BUY_AMT"))
            cmd.Parameters("@SELL_AMT").Value = If(IsDBNull(dr("SELL_AMT")), DBNull.Value, dr("SELL_AMT"))
            cmd.Parameters("@CATEGORY_ID").Value = If(IsDBNull(dr("CATEGORY_ID")), DBNull.Value, dr("CATEGORY_ID"))
            cmd.Parameters("@SUPPLIER_ID").Value = If(IsDBNull(dr("SUPPLIER_ID")), DBNull.Value, dr("SUPPLIER_ID"))
            cmd.Parameters("@NOTES").Value = If(IsDBNull(dr("NOTES")), DBNull.Value, dr("NOTES"))
            cmd.Parameters("@STOCK_QUANTITY").Value = If(IsDBNull(dr("STOCK_QUANTITY")), DBNull.Value, dr("STOCK_QUANTITY"))
            cmd.Parameters("@PROFIT_PERCENT").Value = If(IsDBNull(dr("PROFIT_PERCENT")), DBNull.Value, dr("PROFIT_PERCENT"))
            cmd.Parameters("@AMT_PROFIT").Value = If(IsDBNull(dr("AMT_PROFIT")), DBNull.Value, dr("AMT_PROFIT"))
            cmd.Parameters("@OFFER").Value = If(IsDBNull(dr("OFFER")), DBNull.Value, dr("OFFER"))
            cmd.Parameters("@OFFER_TYPE").Value = If(IsDBNull(dr("OFFER_TYPE")), DBNull.Value, dr("OFFER_TYPE"))
            cmd.Parameters("@OFFER_X").Value = If(IsDBNull(dr("OFFER_X")), DBNull.Value, dr("OFFER_X"))
            cmd.Parameters("@OFFER_Y").Value = If(IsDBNull(dr("OFFER_Y")), DBNull.Value, dr("OFFER_Y"))
            cmd.Parameters("@OFFER_DISC").Value = If(IsDBNull(dr("OFFER_DISC")), DBNull.Value, dr("OFFER_DISC"))
            cmd.Parameters("@OFFER_AT").Value = If(IsDBNull(dr("OFFER_AT")), DBNull.Value, dr("OFFER_AT"))
            cmd.Parameters("@LASTMODIFIEDBY").Value = If(IsDBNull(dr("LASTMODIFIEDBY")), DBNull.Value, dr("LASTMODIFIEDBY"))
            cmd.Parameters("@LASTMODIFIEDSCREEN").Value = If(IsDBNull(dr("LASTMODIFIEDSCREEN")), DBNull.Value, dr("LASTMODIFIEDSCREEN"))
            cmd.Parameters("@VATTYPE_ID").Value = If(IsDBNull(dr("VATTYPE_ID")), DBNull.Value, dr("VATTYPE_ID"))
            cmd.Parameters("@ISBOX").Value = If(IsDBNull(dr("ISBOX")), DBNull.Value, dr("ISBOX"))
            cmd.Parameters("@BOX_QNT").Value = If(IsDBNull(dr("BOX_QNT")), DBNull.Value, dr("BOX_QNT"))
            cmd.Parameters("@OFFERFROMDATE").Value = If(IsDBNull(dr("OFFERFROMDATE")), DBNull.Value, Convert.ToDateTime(dr("OFFERFROMDATE")).ToString("yyyy-MM-dd"))
            cmd.Parameters("@OFFERTODATE").Value = If(IsDBNull(dr("OFFERTODATE")), DBNull.Value, Convert.ToDateTime(dr("OFFERTODATE")).ToString("yyyy-MM-dd"))
            cmd.Parameters("@BUY_AMT_NO_VAT").Value = If(IsDBNull(dr("BUY_AMT_NO_VAT")), DBNull.Value, dr("BUY_AMT_NO_VAT"))
            cmd.Parameters("@BUY_AMT_NEW").Value = If(IsDBNull(dr("BUY_AMT_NEW")), DBNull.Value, dr("BUY_AMT_NEW"))
            cmd.Parameters("@KIOSKID").Value = If(IsDBNull(dr("KIOSKID")), DBNull.Value, dr("KIOSKID"))
            cmd.Parameters("@UPDATED_AT").Value = If(IsDBNull(dr("UPDATED_AT")), DBNull.Value, Convert.ToDateTime(dr("UPDATED_AT")).ToString("yyyy-MM-dd HH:mm:ss"))
            cmd.ExecuteNonQuery()
        End Sub

        Public Sub SyncProducts()
            Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                sqliteConn.Open()
                EnableForeignKeys(sqliteConn)

                Using pragmaCmd As New SQLiteCommand("PRAGMA journal_mode=WAL;", sqliteConn)
                    pragmaCmd.ExecuteNonQuery()
                End Using

                Dim lastSync As DateTime = GetLastSync(sqliteConn, "PRODUCTS")
                Dim sql As String =
                                    "
                                    SELECT
                                        DESCRIPTION,
                                        MIN_QUANTITY,
                                        AVAIL_QUANTITY,
                                        ALERT_ON_MIN,
                                        EXPIRY_DATE,
                                        ALERT_ON_EXPIRY,
                                        ALERT_DATE,
                                        SERNO,
                                        BUY_AMT,
                                        SELL_AMT,
                                        CATEGORY_ID,
                                        SUPPLIER_ID,
                                        NOTES,
                                        STOCK_QUANTITY,
                                        PROFIT_PERCENT,
                                        AMT_PROFIT,
                                        OFFER,
                                        OFFER_TYPE,
                                        OFFER_X,
                                        OFFER_Y,
                                        OFFER_DISC,
                                        OFFER_AT,
                                        LASTMODIFIEDBY,
                                        LASTMODIFIEDSCREEN,
                                        VATTYPE_ID,
                                        ISBOX,
                                        BOX_QNT,
                                        OFFERFROMDATE,
                                        OFFERTODATE,
                                        BUY_AMT_NO_VAT,
                                        BUY_AMT_NEW,
                                        KIOSKID,
                                        UPDATED_AT,
                                        UUID
                                    FROM PRODUCTS
                                    WHERE KIOSKID = :KIOSKID
                                    AND (UPDATED_AT IS NULL OR UPDATED_AT > :LASTSYNC)
                                    ORDER BY UPDATED_AT
                                    "

                Using oracleCmd As New OracleCommand(sql, conn)
                    oracleCmd.BindByName = True
                    oracleCmd.Parameters.Add("KIOSKID", OracleDbType.Varchar2).Value = kioskId
                    oracleCmd.Parameters.Add("LASTSYNC", OracleDbType.TimeStamp).Value = lastSync

                    Using reader = oracleCmd.ExecuteReader()
                        Dim newestTimestamp As DateTime = lastSync
                        Using trans = sqliteConn.BeginTransaction()

                            Dim upsertCmd = CreateProductUpsertCommand(sqliteConn)
                            upsertCmd.Transaction = trans

                            While reader.Read()
                                ExecuteProductUpsert(upsertCmd, reader)

                                If Not IsDBNull(reader("UPDATED_AT")) Then
                                    Dim updatedAt As DateTime = Convert.ToDateTime(reader("UPDATED_AT"))
                                    If updatedAt > newestTimestamp Then
                                        newestTimestamp = updatedAt
                                    End If
                                End If
                            End While
                            trans.Commit()
                        End Using

                        SaveLastSync(sqliteConn, newestTimestamp, "PRODUCTS")
                    End Using
                End Using
            End Using
        End Sub

        Private Function GetLastSync(conn As SQLiteConnection, TableName As String) As DateTime
            Dim sql As String = "
                            SELECT VALUE
                            FROM SYNC_METADATA
                            WHERE KEY='" + TableName + "_LAST_SYNC'
                            AND KIOSKID=:KIOSKID
                            "

            Using cmd As New SQLiteCommand(sql, conn)
                cmd.Parameters.Add("KIOSKID", OracleDbType.Varchar2).Value = kioskId
                Dim result = cmd.ExecuteScalar()

                If result Is Nothing _
                OrElse result Is DBNull.Value Then
                    Return New DateTime(2000, 1, 1)
                End If

                Return DateTime.Parse(result.ToString())
            End Using
        End Function

        Private Sub SaveLastSync(conn As SQLiteConnection, syncDate As DateTime, TableName As String)
            Dim sql As String = "
                            INSERT INTO SYNC_METADATA
                            (
                                KEY,
                                VALUE,
                                KIOSKID
                            )
                            VALUES
                            (
                                '" + TableName + "_LAST_SYNC',
                                @VALUE,
                                '" + kioskId + "'
                            )
                            ON CONFLICT(KEY)
                            DO UPDATE SET
                                VALUE = excluded.VALUE
                            "

            Using cmd As New SQLiteCommand(sql, conn)
                cmd.Parameters.AddWithValue("@VALUE", syncDate.ToString("yyyy-MM-dd HH:mm:ss"))
                cmd.ExecuteNonQuery()
            End Using

        End Sub

        Private Sub EnableForeignKeys(conn As SQLiteConnection)
            Using cmd As New SQLiteCommand("PRAGMA foreign_keys = ON;", conn)
                cmd.ExecuteNonQuery()
            End Using
        End Sub

        Public Sub UploadGlobalParams()
            If Not isConnOpen() Then
                Exit Sub
            End If

            Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                sqliteConn.Open()
                Dim selectSql As String =
                                        "
                                        SELECT
                                            PARAMKEY,
                                            PARAMVALUE,
                                            KIOSKID,
                                            UPDATED_AT
                                        FROM GLOBAL_PARAMS
                                        WHERE SYNC_STATUS = 1
                                        ORDER BY UPDATED_AT
                                        "

                Using selectCmd As New SQLiteCommand(selectSql, sqliteConn)
                    Using reader = selectCmd.ExecuteReader()
                        While reader.Read()
                            UploadGlobalParam(sqliteConn, reader)
                        End While
                    End Using
                End Using
            End Using
        End Sub

        Private Sub UploadGlobalParam(sqliteConn As SQLiteConnection, reader As SQLiteDataReader)

            Dim mergeSql As String =
                                    "
                            MERGE INTO GLOBAL_PARAMS g

                            USING
                            (
                            SELECT

                            :PARAMKEY PARAMKEY,
                            :PARAMVALUE PARAMVALUE,
                            :KIOSKID KIOSKID,
                            :UPDATED_AT UPDATED_AT

                            FROM dual

                            ) x

                            ON
                            (
                            g.PARAMKEY=x.PARAMKEY
                            AND g.KIOSKID=x.KIOSKID
                            )

                            WHEN MATCHED THEN

                            UPDATE SET

                            g.PARAMVALUE=x.PARAMVALUE,
                            g.UPDATED_AT=x.UPDATED_AT

                            WHEN NOT MATCHED THEN

                            INSERT
                            (
                            PARAMKEY,
                            PARAMVALUE,
                            KIOSKID,
                            UPDATED_AT
                            )

                            VALUES
                            (
                            x.PARAMKEY,
                            x.PARAMVALUE,
                            x.KIOSKID,
                            x.UPDATED_AT
                            )
                            "

            Using oracleCmd As New OracleCommand(mergeSql, conn)
                oracleCmd.BindByName = True
                oracleCmd.Parameters.Add("PARAMKEY", OracleDbType.Varchar2).Value = reader("PARAMKEY").ToString()
                oracleCmd.Parameters.Add("PARAMVALUE", OracleDbType.Varchar2).Value = If(IsDBNull(reader("PARAMVALUE")), DBNull.Value, reader("PARAMVALUE"))
                oracleCmd.Parameters.Add("KIOSKID", OracleDbType.Varchar2).Value = reader("KIOSKID").ToString()
                oracleCmd.Parameters.Add("UPDATED_AT", OracleDbType.TimeStamp).Value = Convert.ToDateTime(reader("UPDATED_AT"))
                oracleCmd.ExecuteNonQuery()
            End Using

            MarkGlobalParamSynced(sqliteConn, reader("PARAMKEY").ToString())
        End Sub

        Private Sub MarkGlobalParamSynced(sqliteConn As SQLiteConnection, paramKey As String)
            Dim sql As String =
                                "
                                    UPDATE GLOBAL_PARAMS
                                    SET SYNC_STATUS=0
                                    WHERE PARAMKEY=@PARAMKEY
                                "

            Using cmd As New SQLiteCommand(sql, sqliteConn)
                cmd.Parameters.AddWithValue("@PARAMKEY", paramKey)
                cmd.ExecuteNonQuery()
            End Using
        End Sub

        Public Sub UploadCategories()

            If Not isConnOpen() Then Exit Sub

            Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")

                sqliteConn.Open()

                Dim sql As String =
                "
        SELECT
            UUID,
            DESCRIPTION,
            VAT,
            KIOSKID,
            UPDATED_AT
        FROM CATEGORIES
        WHERE SYNC_STATUS = 1
        ORDER BY UPDATED_AT
        "

                Using cmd As New SQLiteCommand(sql, sqliteConn)

                    Using reader As SQLiteDataReader = cmd.ExecuteReader()

                        While reader.Read()

                            UploadCategory(sqliteConn, reader)

                        End While

                    End Using

                End Using

            End Using

        End Sub

        Private Sub UploadCategory(sqliteConn As SQLiteConnection, reader As SQLiteDataReader)

            Dim uuid As String = reader("UUID").ToString()
            Dim description As Object = If(IsDBNull(reader("DESCRIPTION")), DBNull.Value, reader("DESCRIPTION").ToString())
            Dim vat As Object = If(IsDBNull(reader("VAT")), DBNull.Value, Convert.ToDecimal(reader("VAT")))
            Dim kiosk As Object = If(IsDBNull(reader("KIOSKID")), DBNull.Value, reader("KIOSKID").ToString())

            '-------------------------
            ' UPDATE
            '-------------------------

            Dim updateSql As String =
                                    "
                                    UPDATE CATEGORIES
                                       SET DESCRIPTION = :DESCRIPTION,
                                           VAT = :VAT,
                                           KIOSKID = :KIOSKID,
                                           UPDATED_AT = SYSTIMESTAMP
                                     WHERE UUID = :UUID
                                    "

            Dim rows As Integer
            Using cmd As New OracleCommand(updateSql, conn)
                cmd.BindByName = True
                cmd.Parameters.Add("DESCRIPTION", OracleDbType.Varchar2).Value = description
                cmd.Parameters.Add("VAT", OracleDbType.Decimal).Value = vat
                cmd.Parameters.Add("KIOSKID", OracleDbType.Varchar2).Value = kiosk
                cmd.Parameters.Add("UUID", OracleDbType.Varchar2).Value = uuid
                rows = cmd.ExecuteNonQuery()
            End Using

            '-------------------------
            ' INSERT
            '-------------------------
            If rows = 0 Then
                Dim insertSql As String =
                                        "
                                        INSERT INTO CATEGORIES
                                        (
                                            UUID,
                                            DESCRIPTION,
                                            VAT,
                                            KIOSKID,
                                            UPDATED_AT
                                        )
                                        VALUES
                                        (
                                            :UUID,
                                            :DESCRIPTION,
                                            :VAT,
                                            :KIOSKID,
                                            SYSTIMESTAMP
                                        )
                                        "

                Using cmd As New OracleCommand(insertSql, conn)
                    cmd.BindByName = True
                    cmd.Parameters.Add("UUID", OracleDbType.Varchar2).Value = uuid
                    cmd.Parameters.Add("DESCRIPTION", OracleDbType.Varchar2).Value = description
                    cmd.Parameters.Add("VAT", OracleDbType.Decimal).Value = vat
                    cmd.Parameters.Add("KIOSKID", OracleDbType.Varchar2).Value = kiosk
                    cmd.ExecuteNonQuery()
                End Using
            End If
            MarkCategorySynced(sqliteConn, uuid)
        End Sub

        Private Sub MarkCategorySynced(sqliteConn As SQLiteConnection, uuid As String)
            Dim sql As String =
                                "
                                UPDATE CATEGORIES
                                SET SYNC_STATUS = 0
                                WHERE UUID = @UUID
                                "

            Using cmd As New SQLiteCommand(sql, sqliteConn)
                cmd.Parameters.AddWithValue("@UUID", uuid)
                cmd.ExecuteNonQuery()
            End Using
        End Sub

        Public Sub UploadSuppliers()
            Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                sqliteConn.Open()
                Dim sql As String =
                                "
                                SELECT *
                                FROM SUPPLIERS
                                WHERE KIOSKID=@KIOSKID
                                  AND SYNC_STATUS=1
                                ORDER BY UPDATED_AT
                                "

                Using cmd As New SQLiteCommand(sql, sqliteConn)
                    cmd.Parameters.AddWithValue("@KIOSKID", kioskId)
                    Using reader = cmd.ExecuteReader()
                        While reader.Read()
                            UploadSupplier(sqliteConn, reader)
                        End While
                    End Using
                End Using
            End Using

            'Delete from oracle any suppliers deleted locally 
            While DeletedSuppliers.Count > 0
                Dim item As Object = DeletedSuppliers(0)
                Try
                    DeleteSupplierInOracle(item)
                Catch ex As Exception
                    Exit Sub
                End Try
                DeletedSuppliers.RemoveAt(0)
            End While

        End Sub

        Private Sub DeleteSupplierInOracle(supplierId As String)
            Dim sql As String = "DELETE FROM SUPPLIERS WHERE UUID=:UUID"
            Using cmd As New OracleCommand(sql, conn)
                cmd.BindByName = True
                cmd.Parameters.Add("UUID", OracleDbType.Varchar2).Value = supplierId
                cmd.ExecuteNonQuery()
            End Using
        End Sub
        Private Sub UploadSupplier(sqliteConn As SQLiteConnection, reader As SQLiteDataReader)
            Dim mergeSql As String =
                                    "
                            MERGE INTO SUPPLIERS s

                            USING
                            (
                                SELECT

                                    :UUID UUID,
                                    :S_NAME S_NAME,
                                    :PHONE_1 PHONE_1,
                                    :PHONE_2 PHONE_2,
                                    :EMAIL EMAIL,
                                    :CONTACT_NAME CONTACT_NAME,
                                    :MON MON,
                                    :TUE TUE,
                                    :WED WED,
                                    :THU THU,
                                    :FRI FRI,
                                    :NOTES NOTES,
                                    :ISDEFAULT ISDEFAULT,
                                    :KIOSKID KIOSKID,
                                    :UPDATED_AT UPDATED_AT

                                FROM dual

                            ) x

                            ON
                            (
                                s.UUID=x.UUID
                            )

                            WHEN MATCHED THEN

                            UPDATE SET

                                s.S_NAME=x.S_NAME,
                                s.PHONE_1=x.PHONE_1,
                                s.PHONE_2=x.PHONE_2,
                                s.EMAIL=x.EMAIL,
                                s.CONTACT_NAME=x.CONTACT_NAME,
                                s.MON=x.MON,
                                s.TUE=x.TUE,
                                s.WED=x.WED,
                                s.THU=x.THU,
                                s.FRI=x.FRI,
                                s.NOTES=x.NOTES,
                                s.ISDEFAULT=x.ISDEFAULT,
                                s.KIOSKID=x.KIOSKID,
                                s.UPDATED_AT=x.UPDATED_AT

                            WHEN NOT MATCHED THEN

                            INSERT
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
                                ISDEFAULT,
                                KIOSKID,
                                UPDATED_AT
                            )

                            VALUES
                            (
                                x.UUID,
                                x.S_NAME,
                                x.PHONE_1,
                                x.PHONE_2,
                                x.EMAIL,
                                x.CONTACT_NAME,
                                x.MON,
                                x.TUE,
                                x.WED,
                                x.THU,
                                x.FRI,
                                x.NOTES,
                                x.ISDEFAULT,
                                x.KIOSKID,
                                x.UPDATED_AT
                            )
                            "

            Using cmd As New OracleCommand(mergeSql, conn)
                cmd.BindByName = True
                cmd.Parameters.Add("UUID", OracleDbType.Varchar2).Value = reader("UUID").ToString()
                cmd.Parameters.Add("S_NAME", OracleDbType.Varchar2).Value = If(IsDBNull(reader("S_NAME")), DBNull.Value, reader("S_NAME").ToString())
                cmd.Parameters.Add("PHONE_1", OracleDbType.Varchar2).Value = If(IsDBNull(reader("PHONE_1")), DBNull.Value, reader("PHONE_1").ToString())
                cmd.Parameters.Add("PHONE_2", OracleDbType.Varchar2).Value = If(IsDBNull(reader("PHONE_2")), DBNull.Value, reader("PHONE_2").ToString())
                cmd.Parameters.Add("EMAIL", OracleDbType.Varchar2).Value = If(IsDBNull(reader("EMAIL")), DBNull.Value, reader("EMAIL").ToString())
                cmd.Parameters.Add("CONTACT_NAME", OracleDbType.Varchar2).Value = If(IsDBNull(reader("CONTACT_NAME")), DBNull.Value, reader("CONTACT_NAME").ToString())
                cmd.Parameters.Add("MON", OracleDbType.Int32).Value = Convert.ToInt32(reader("MON"))
                cmd.Parameters.Add("TUE", OracleDbType.Int32).Value = Convert.ToInt32(reader("TUE"))
                cmd.Parameters.Add("WED", OracleDbType.Int32).Value = Convert.ToInt32(reader("WED"))
                cmd.Parameters.Add("THU", OracleDbType.Int32).Value = Convert.ToInt32(reader("THU"))
                cmd.Parameters.Add("FRI", OracleDbType.Int32).Value = Convert.ToInt32(reader("FRI"))
                cmd.Parameters.Add("NOTES", OracleDbType.Varchar2).Value = If(IsDBNull(reader("NOTES")), DBNull.Value, reader("NOTES").ToString())
                cmd.Parameters.Add("ISDEFAULT", OracleDbType.Int32).Value = 0 'Convert.ToInt32(reader("ISDEFAULT"))
                'cmd.Parameters.Add("DELETED", OracleDbType.Int32).Value = Convert.ToInt32(reader("DELETED"))
                cmd.Parameters.Add("KIOSKID", OracleDbType.Varchar2).Value = reader("KIOSKID").ToString()
                cmd.Parameters.Add("UPDATED_AT", OracleDbType.TimeStamp).Value = Convert.ToDateTime(reader("UPDATED_AT"))
                cmd.ExecuteNonQuery()
            End Using
            MarkSupplierSynced(sqliteConn, reader("UUID").ToString())
        End Sub

        Private Sub MarkSupplierSynced(sqliteConn As SQLiteConnection, uuid As String)
            Dim sql As String =
                               "
                                UPDATE SUPPLIERS
                                SET SYNC_STATUS=0
                                WHERE UUID=@UUID
                               "

            Using cmd As New SQLiteCommand(sql, sqliteConn)
                cmd.Parameters.AddWithValue("@UUID", uuid)
                cmd.ExecuteNonQuery()
            End Using
        End Sub

        Public Sub UploadLottery()
            Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                sqliteConn.Open()
                'Delete existing Oracle rows
                Using cmd As New OracleCommand("DELETE FROM LOTTERY WHERE KIOSKID = :KIOSKID", conn)
                    cmd.Parameters.Add("KIOSKID", OracleDbType.Varchar2).Value = kioskId
                    cmd.ExecuteNonQuery()
                End Using

                'Insert all SQLite rows
                Const sql As String =
                                    "
                                    SELECT BARCODE,
                                           KIOSKID,
                                           UPDATED_AT
                                    FROM LOTTERY
                                    WHERE KIOSKID=@KIOSKID
                                    ORDER BY BARCODE
                                    "

                Using cmd As New SQLiteCommand(sql, sqliteConn)
                    cmd.Parameters.AddWithValue("@KIOSKID", kioskId)
                    Using reader = cmd.ExecuteReader()
                        While reader.Read()
                            InsertLotteryRow(reader)
                        End While
                    End Using
                End Using
                MarkLotterySynced(sqliteConn, kioskId)
            End Using
        End Sub

        Private Sub InsertLotteryRow(reader As SQLiteDataReader)
            Const sql As String =
                                "
                                INSERT INTO LOTTERY
                                (
                                    BARCODE,
                                    KIOSKID,
                                    UPDATED_AT
                                )
                                VALUES
                                (
                                    :BARCODE,
                                    :KIOSKID,
                                    :UPDATED_AT
                                )
                                "

            Using cmd As New OracleCommand(sql, conn)
                cmd.BindByName = True

                cmd.Parameters.Add("BARCODE", OracleDbType.Varchar2).Value = reader("BARCODE").ToString()
                cmd.Parameters.Add("KIOSKID", OracleDbType.Varchar2).Value = reader("KIOSKID").ToString()
                cmd.Parameters.Add("UPDATED_AT", OracleDbType.TimeStamp).Value = Convert.ToDateTime(reader("UPDATED_AT"))
                cmd.ExecuteNonQuery()
            End Using
        End Sub

        Private Sub MarkLotterySynced(sqliteConn As SQLiteConnection, kioskId As String)
            Const sql As String =
                                "
                                UPDATE LOTTERY
                                   SET SYNC_STATUS = 0
                                 WHERE KIOSKID = @KIOSKID
                                "

            Using cmd As New SQLiteCommand(sql, sqliteConn)
                cmd.Parameters.AddWithValue("@KIOSKID", kioskId)
                cmd.ExecuteNonQuery()
            End Using
        End Sub
    End Class
End Module
