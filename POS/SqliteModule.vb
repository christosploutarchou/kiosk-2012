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

    Public Sub CreateInvoicesTable()

        Dim sql As String =
                            "
                            CREATE TABLE IF NOT EXISTS INVOICES
                            (
                                UUID TEXT PRIMARY KEY,
                                SERNO INTEGER,
                                I_NUMBER TEXT,
                                SUPPLIER_ID TEXT,
                                TOTAL_AMT REAL,
                                INV_DATE TEXT,
                                CLOSED INTEGER,
                                EXTRA_DISC REAL DEFAULT 0,
                                KIOSKID TEXT,
                                UPDATED_AT TEXT DEFAULT CURRENT_TIMESTAMP,
                                SYNC_STATUS INTEGER NOT NULL DEFAULT 0,
                                FOREIGN KEY(SUPPLIER_ID) REFERENCES SUPPLIERS(UUID),
                                FOREIGN KEY(KIOSKID) REFERENCES KIOSK(KIOSKID)
                            );
                            CREATE INDEX IF NOT EXISTS IDX_INVOICES_SYNC ON INVOICES(SYNC_STATUS,KIOSKID);
                            CREATE INDEX IF NOT EXISTS IDX_INVOICES_UPDATED ON INVOICES(KIOSKID,UPDATED_AT);
                            CREATE INDEX IF NOT EXISTS IDX_INVOICES_SUPPLIER ON INVOICES(SUPPLIER_ID);
                            "

        Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
            sqliteConn.Open()
            Using cmd As New SQLiteCommand(sql, sqliteConn)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Sub CreateInvoicesDetTable()

        Dim sql As String =
                            "
                            CREATE TABLE IF NOT EXISTS INVOICES_DET
                            (
                                UUID TEXT PRIMARY KEY,
                                SERNO INTEGER,
                                INV_SERNO INTEGER,
                                INV_UUID TEXT,
                                INV_PR_SERNO INTEGER,
                                PRODUCT_UUID TEXT,
                                INV_PR_QNT INTEGER,
                                INV_PR_DESCR TEXT,
                                INV_PR_CUR_QNT INTEGER,
                                INV_PR_DISC REAL,
                                INV_PR_BUY_AMT REAL,
                                INV_PR_SELL_AMT REAL,
                                INV_PR_PROFIT REAL,
                                EXTRA_DISC REAL,
                                KIOSKID TEXT,
                                UPDATED_AT TEXT DEFAULT CURRENT_TIMESTAMP,
                                SYNC_STATUS INTEGER NOT NULL DEFAULT 0,
                                FOREIGN KEY(INV_UUID) REFERENCES INVOICES(UUID),
                                FOREIGN KEY(PRODUCT_UUID) REFERENCES PRODUCTS(UUID),
                                FOREIGN KEY(KIOSKID) REFERENCES KIOSK(KIOSKID)
                            );

                            CREATE INDEX IF NOT EXISTS IDX_INVDET_SYNC ON INVOICES_DET(SYNC_STATUS,KIOSKID);
                            CREATE INDEX IF NOT EXISTS IDX_INVDET_UPDATED ON INVOICES_DET(KIOSKID,UPDATED_AT);
                            CREATE INDEX IF NOT EXISTS IDX_INVDET_INV_UUID ON INVOICES_DET(INV_UUID);
                            CREATE INDEX IF NOT EXISTS IDX_INVDET_PRODUCT_UUID ON INVOICES_DET(PRODUCT_UUID);
                            "

        Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
            sqliteConn.Open()
            Using cmd As New SQLiteCommand(sql, sqliteConn)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Private Sub CreateZReportTable()

        Dim sql As String =
                            "
                            CREATE TABLE IF NOT EXISTS Z_REPORT
                            (
                                Z_UUID TEXT PRIMARY KEY,
                                Z_DATE TEXT,
                                Z_SEQ INTEGER,
                                TOTAL_RECEIPTS INTEGER,
                                TOTAL_AMOUNT0 REAL,
                                TOTAL_AMOUNT3 REAL,
                                TOTAL_AMOUNT5 REAL,
                                TOTAL_AMOUNT19 REAL,
                                TOTAL_AMOUNT REAL,
                                KIOSKID TEXT,
                                UPDATED_AT TEXT DEFAULT CURRENT_TIMESTAMP,
                                SYNC_STATUS INTEGER NOT NULL DEFAULT 0,
                                FOREIGN KEY(KIOSKID) REFERENCES KIOSK(KIOSKID)
                            );
                            CREATE INDEX IF NOT EXISTS IDX_Z_REPORT_SYNC ON Z_REPORT(SYNC_STATUS,KIOSKID);
                            CREATE INDEX IF NOT EXISTS IDX_Z_REPORT_UPDATED ON Z_REPORT(KIOSKID,UPDATED_AT);
                            "
        Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
            sqliteConn.Open()
            Using cmd As New SQLiteCommand(sql, sqliteConn)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Private Sub CreateXReportTable()
        Dim sql As String =
                            "
                            CREATE TABLE IF NOT EXISTS X_REPORT
                            (
                                USER_ID TEXT,
                                FROM_DATE TEXT,
                                TO_DATE TEXT,
                                TOTAL_RECEIPTS INTEGER,
                                TOTAL_AMT REAL,
                                TOTAL5PERCENT REAL,
                                TOTAL19PERCENT REAL,
                                TOTAL0PERCENT REAL,
                                TOTAL3PERCENT REAL,
                                INITIAL_AMT REAL,
                                FINAL_AMT REAL,
                                PAYMENTS REAL,
                                CREATED_ON TEXT,
                                AMOUNT_LAXEIA REAL,
                                INITIALAMTLAXEIA REAL,
                                AMOUNTVISA REAL,
                                FINALAMTLAXEIA REAL,
                                DESCRIPTION TEXT,
                                KIOSKID TEXT,
                                UPDATED_AT TEXT DEFAULT CURRENT_TIMESTAMP,
                                SYNC_STATUS INTEGER NOT NULL DEFAULT 0,
                                PRIMARY KEY
                                (
                                    USER_ID,
                                    FROM_DATE,
                                    TO_DATE,
                                    KIOSKID
                                ),
                                FOREIGN KEY(USER_ID) REFERENCES USERS(UUID),
                                FOREIGN KEY(KIOSKID) REFERENCES KIOSK(KIOSKID)
                            );

                            CREATE INDEX IF NOT EXISTS IDX_XREPORT_SYNC ON X_REPORT(SYNC_STATUS,KIOSKID);
                            CREATE INDEX IF NOT EXISTS IDX_XREPORT_UPDATED ON X_REPORT(KIOSKID,UPDATED_AT);
                            CREATE INDEX IF NOT EXISTS IDX_XREPORT_USER ON X_REPORT(USER_ID);
                            "
        Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
            sqliteConn.Open()
            Using cmd As New SQLiteCommand(sql, sqliteConn)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Private Sub CreateReceiptsDetTable()
        Dim sql As String =
                            "
                            CREATE TABLE IF NOT EXISTS RECEIPTS_DET
                            (
                                UUID TEXT PRIMARY KEY,
                                RECEIPT_SERNO INTEGER,
                                PRODUCT_SERNO INTEGER,
                                RECEIPT_UUID TEXT,
                                PRODUCT_UUID TEXT,
                                QUANTITY INTEGER,
                                CREATED_ON TEXT,
                                AMOUNT REAL,
                                VAT INTEGER,
                                KIOSKID TEXT,
                                UPDATED_AT TEXT,
                                SYNC_STATUS INTEGER DEFAULT 0
                            );

                            CREATE INDEX IF NOT EXISTS IDX_RECDET_RECEIPT ON RECEIPTS_DET(RECEIPT_UUID);
                            CREATE INDEX IF NOT EXISTS IDX_RECDET_PRODUCT ON RECEIPTS_DET(PRODUCT_UUID);
                            CREATE INDEX IF NOT EXISTS IDX_RECDET_SYNC ON RECEIPTS_DET(SYNC_STATUS,KIOSKID);
                            CREATE INDEX IF NOT EXISTS IDX_RECDET_UPDATED ON RECEIPTS_DET(KIOSKID,UPDATED_AT);
                            "

        Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
            sqliteConn.Open()
            Using cmd As New SQLiteCommand(sql, sqliteConn)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub


    Private Sub CreatePaymentsTable()

        Dim sql As String =
                            "
                            CREATE TABLE IF NOT EXISTS PAYMENTS
                            (
                                CREATED_BY TEXT,
                                CREATED_ON TEXT,
                                AMOUNT REAL,
                                VAT TEXT,
                                AMOUNTVAT REAL,
                                INV_NUMBER TEXT,
                                SUPPLIER_ID TEXT,
                                KIOSKID TEXT,
                                UPDATED_AT TEXT DEFAULT CURRENT_TIMESTAMP,
                                SYNC_STATUS INTEGER NOT NULL DEFAULT 0,
                                PRIMARY KEY
                                (
                                    CREATED_BY,
                                    CREATED_ON,
                                    INV_NUMBER,
                                    SUPPLIER_ID,
                                    KIOSKID
                                ),
                                FOREIGN KEY (CREATED_BY) REFERENCES USERS(UUID),
                                FOREIGN KEY (SUPPLIER_ID) REFERENCES SUPPLIERS(UUID),
                                FOREIGN KEY (KIOSKID) REFERENCES KIOSK(KIOSKID)
                            );

                            CREATE INDEX IF NOT EXISTS IDX_PAYMENTS_SYNC ON PAYMENTS(SYNC_STATUS);
                            CREATE INDEX IF NOT EXISTS IDX_PAYMENTS_DATE ON PAYMENTS(CREATED_ON);
                            "
        Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
            sqliteConn.Open()
            Using cmd As New SQLiteCommand(sql, sqliteConn)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Private Sub CreateReceiptsTable()
        Dim sql As String =
                            "
                            CREATE TABLE IF NOT EXISTS RECEIPTS
                            (
                                UUID TEXT PRIMARY KEY,
                                SERNO INTEGER,
                                PAYMENT_TYPE TEXT,
                                TOTAL_DISCOUNT REAL,
                                TOTAL_VAT19 REAL,
                                TOTAL_VAT5 REAL,
                                TOTAL_VAT3 REAL,
                                TOTAL_VAT0 REAL,
                                RETURN_AMT REAL,
                                TOTAL_AMT_WITH_DISC REAL,
                                TOTAL_AMT REAL,
                                PAYMENT_AMT REAL,
                                CREATED_ON TEXT,
                                CREATED_BY TEXT,
                                KIOSKID TEXT,
                                UPDATED_AT TEXT DEFAULT CURRENT_TIMESTAMP,
                                SYNC_STATUS INTEGER NOT NULL DEFAULT 0,
                                FOREIGN KEY (CREATED_BY) REFERENCES USERS(UUID),
                                FOREIGN KEY (KIOSKID) REFERENCES KIOSK(KIOSKID)
                            );

                            CREATE INDEX IF NOT EXISTS IDX_RECEIPTS_SERNO ON RECEIPTS(SERNO);
                            CREATE INDEX IF NOT EXISTS IDX_RECEIPTS_USER ON RECEIPTS(CREATED_BY);
                            CREATE INDEX IF NOT EXISTS IDX_RECEIPTS_DATE ON RECEIPTS(CREATED_ON);
                            CREATE INDEX IF NOT EXISTS IDX_RECEIPTS_SYNC ON RECEIPTS(SYNC_STATUS);
                            "
        Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
            sqliteConn.Open()
            Using cmd As New SQLiteCommand(sql, sqliteConn)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

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

    Private Sub CreateButtonTables()
        Using conn As New SQLiteConnection("Data Source=kiosk.db")
            conn.Open()
            For i As Integer = 1 To 23

                Dim sql As String =
                                $"
                            CREATE TABLE IF NOT EXISTS BTN_POS{i}
                            (
                                DISP_NAME      TEXT,
                                IS_VISIBLE     INTEGER,
                                KIOSKID        TEXT NOT NULL,
                                UPDATED_AT     TEXT DEFAULT CURRENT_TIMESTAMP,
                                SYNC_STATUS    INTEGER NOT NULL DEFAULT 0,
                                PRIMARY KEY (KIOSKID),
                                FOREIGN KEY (KIOSKID) REFERENCES KIOSK(KIOSKID)
                            );

                            CREATE TABLE IF NOT EXISTS BTN_POS{i}_DET
                            (
                                PRODUCT_SERNO  INTEGER,
                                PRODUCT_UUID   TEXT,
                                DISPLAY_DESC   TEXT,
                                SEQNO          INTEGER,
                                KIOSKID        TEXT NOT NULL,
                                UPDATED_AT     TEXT DEFAULT CURRENT_TIMESTAMP,
                                SYNC_STATUS    INTEGER NOT NULL DEFAULT 0,
                                PRIMARY KEY (KIOSKID, PRODUCT_UUID),
                                FOREIGN KEY (PRODUCT_UUID) REFERENCES PRODUCTS(UUID),
                                FOREIGN KEY (KIOSKID) REFERENCES KIOSK(KIOSKID)
                            );

                            CREATE INDEX IF NOT EXISTS IDX_BTN_POS{i}_DET_PRODUCT_UUID ON BTN_POS{i}_DET(PRODUCT_UUID);
                            CREATE INDEX IF NOT EXISTS IDX_BTN_POS{i}_DET_SEQNO ON BTN_POS{i}_DET(SEQNO);
                            CREATE INDEX IF NOT EXISTS IDX_BTN_POS{i}_DET_SYNC ON BTN_POS{i}_DET(KIOSKID, UPDATED_AT);
                        "

                Using cmd As New SQLiteCommand(sql, conn)
                    cmd.ExecuteNonQuery()
                End Using
            Next
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
                        SYNC_STATUS INTEGER NOT NULL DEFAULT 0,
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
                        SYNC_STATUS INTEGER NOT NULL DEFAULT 0,
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
            CreateButtonTables()
            CreatePaymentsTable()
            CreateReceiptsTable()
            CreateReceiptsDetTable()
            CreateXReportTable()
            CreateZReportTable()
            CreateInvoicesTable()
            CreateInvoicesDetTable()
        Catch ex As Exception
            CreateExceptionFile(WhoAmI + ": " + ex.Message, " ")
            MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub ValidateAndSyncTables()
        Dim WhoAmI As String = "ValidateAndSyncTables"
        Dim sync As New SyncTables()
        Try
            Dim tables As New Dictionary(Of String, Action) From
                {
                    {"GLOBAL_PARAMS", AddressOf sync.SyncGlobalParams},
                    {"USERS", AddressOf sync.SyncUsers},
                    {"LOTTERY", AddressOf sync.SyncLottery},
                    {"VAT_TYPES", AddressOf sync.SyncVatTypes},
                    {"SUPPLIERS", AddressOf sync.SyncSuppliers},
                    {"CATEGORIES", AddressOf sync.SyncCategories},
                    {"PRODUCTS", AddressOf sync.SyncProducts},
                    {"BARCODES", AddressOf sync.SyncBarcodes},
                    {"SESSIONS", AddressOf sync.SyncSessions},
                    {"PAYMENTS", AddressOf sync.SyncPayments},
                    {"RECEIPTS", AddressOf sync.SyncReceipts},
                    {"RECEIPTS_DET", AddressOf sync.SyncReceiptsDet},
                    {"X_REPORT", AddressOf sync.SyncXReport},
                    {"Z_REPORT", AddressOf sync.SyncZReport},
                    {"INVOICES", AddressOf sync.SyncInvoices},
                    {"INVOICES_DET", AddressOf sync.SyncInvoicesDet}
                }

            '--------------------------------------------
            ' ADD BTN_POS1 - BTN_POS23
            '--------------------------------------------
            For i As Integer = 1 To 23
                Dim idx As Integer = i
                tables.Add(
                "BTN_POS" & idx,
                Sub()
                    SyncBtnPos(idx)
                End Sub
            )

                tables.Add(
                "BTN_POS" & idx & "_DET",
                Sub()
                    SyncBtnPosDet(idx)
                End Sub
            )

            Next

            For Each item In tables
                Dim tableName As String = item.Key
                Dim sqliteRows As Long = GetSQLiteTableCount(tableName)
                Dim oracleRows As Long = GetOracleTableCount(tableName)

                If (sqliteRows = 0 And oracleRows > 0) OrElse sqliteRows < oracleRows Then
                    CreateExceptionFile("Sync required: " & tableName & " SQLite=" & sqliteRows & " Oracle=" & oracleRows, "")
                    item.Value.Invoke()
                End If
            Next
        Catch ex As Exception
            CreateExceptionFile(WhoAmI & ": " & ex.Message, "")
        End Try

    End Sub

    Private Sub SyncBtnPos(index As Integer)
        Dim tableName As String = "BTN_POS" & index
        Dim sql As String =
                            "
                            SELECT
                                DISP_NAME,
                                IS_VISIBLE,
                                KIOSKID,
                                UPDATED_AT
                            FROM " & tableName & "
                            WHERE KIOSKID=:KIOSKID
                            "

        Using oraCmd As New OracleCommand(sql, conn)
            oraCmd.Parameters.Add("KIOSKID", OracleDbType.Varchar2).Value = kioskId
            Using dr = oraCmd.ExecuteReader()
                Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                    sqliteConn.Open()

                    Using tr =
                    sqliteConn.BeginTransaction()

                        Dim insertSql As String =
                                                "
                                                DELETE FROM " & tableName & ";

                                                INSERT INTO " & tableName & "
                                                (
                                                DISP_NAME,
                                                IS_VISIBLE,
                                                KIOSKID,
                                                UPDATED_AT,
                                                SYNC_STATUS
                                                )
                                                VALUES
                                                (
                                                @DISP_NAME,
                                                @IS_VISIBLE,
                                                @KIOSKID,
                                                @UPDATED_AT,
                                                0
                                                )
                                                "
                        While dr.Read()
                            Using cmd As New SQLiteCommand(insertSql, sqliteConn)
                                cmd.Transaction = tr
                                cmd.Parameters.AddWithValue("@DISP_NAME", dr("DISP_NAME"))
                                cmd.Parameters.AddWithValue("@IS_VISIBLE", dr("IS_VISIBLE"))
                                cmd.Parameters.AddWithValue("@KIOSKID", dr("KIOSKID"))
                                cmd.Parameters.AddWithValue("@UPDATED_AT", dr("UPDATED_AT"))
                                cmd.ExecuteNonQuery()
                            End Using
                        End While
                        tr.Commit()
                    End Using
                End Using
            End Using
        End Using
    End Sub

    Private Sub SyncBtnPosDet(index As Integer)
        Dim tableName = "BTN_POS" & index & "_DET"
        Dim sql As String =
                            "
                            SELECT
                                PRODUCT_SERNO,
                                PRODUCT_UUID,
                                DISPLAY_DESC,
                                SEQNO,
                                KIOSKID,
                                UPDATED_AT

                            FROM " & tableName & "

                            WHERE KIOSKID=:KIOSKID
                            "

        Using oraCmd As New OracleCommand(sql, conn)
            oraCmd.Parameters.Add("KIOSKID", OracleDbType.Varchar2).Value = kioskId
            Using dr = oraCmd.ExecuteReader()
                Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                    sqliteConn.Open()
                    Using tr = sqliteConn.BeginTransaction()

                        Using del As New SQLiteCommand(
                        "DELETE FROM " & tableName,
                        sqliteConn)

                            del.Transaction = tr
                            del.ExecuteNonQuery()
                        End Using
                        While dr.Read()

                            Dim ins As String =
                                                "
                                                INSERT INTO " & tableName & "
                                                (
                                                PRODUCT_SERNO,
                                                PRODUCT_UUID,
                                                DISPLAY_DESC,
                                                SEQNO,
                                                KIOSKID,
                                                UPDATED_AT,
                                                SYNC_STATUS
                                                )
                                                VALUES
                                                (
                                                @PRODUCT_SERNO,
                                                @PRODUCT_UUID,
                                                @DISPLAY_DESC,
                                                @SEQNO,
                                                @KIOSKID,
                                                @UPDATED_AT,
                                                0
                                                )
                                                "

                            Using cmd As New SQLiteCommand(ins, sqliteConn)
                                cmd.Transaction = tr
                                For i = 0 To dr.FieldCount - 1
                                    cmd.Parameters.AddWithValue(
                                "@" & dr.GetName(i),
                                If(IsDBNull(dr(i)),
                                   DBNull.Value,
                                   dr(i)))

                                Next
                                cmd.ExecuteNonQuery()
                            End Using
                        End While
                        tr.Commit()
                    End Using
                End Using
            End Using
        End Using
    End Sub

    Private Function GetSQLiteTableCount(tableName As String) As Long
        Dim result As Long = 0

        Dim sql As String =
                                "SELECT COUNT(*) FROM " & tableName &
                                " WHERE KIOSKID=@KIOSKID"
        Try
            Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                sqliteConn.Open()

                Using cmd As New SQLiteCommand(sql, sqliteConn)
                    cmd.Parameters.AddWithValue("@KIOSKID", kioskId)
                    result = CLng(cmd.ExecuteScalar())
                End Using
            End Using
        Catch ex As Exception
            CreateExceptionFile("GetSQLiteTableCount " & tableName & ": " & ex.Message, sql)
        End Try
        Return result
    End Function

    Private Function GetOracleTableCount(tableName As String) As Long
        Dim result As Long = 0
        Dim sql As String =
                            "SELECT COUNT(*) FROM " &
                            tableName &
                            " WHERE KIOSKID=:KIOSKID"
        Try
            Using cmd As New OracleCommand(sql, conn)
                cmd.BindByName = True
                cmd.Parameters.Add("KIOSKID", OracleDbType.Varchar2).Value = kioskId
                result = CLng(cmd.ExecuteScalar())
            End Using
        Catch ex As Exception
            CreateExceptionFile("GetOracleTableCount " & tableName & ": " & ex.Message, sql)
        End Try
        Return result
    End Function

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

        Public Sub SyncInvoicesDet()

            Dim WhoAmI As String = "SyncInvoicesDet"
            Try
                Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                    sqliteConn.Open()
                    Dim lastSync As DateTime = GetLastSync(sqliteConn, "INVOICES_DET")

                    Dim sql As String =
                                        "
                                        SELECT
                                            UUID,
                                            SERNO,
                                            INV_SERNO,
                                            INV_UUID,
                                            INV_PR_SERNO,
                                            PRODUCT_UUID,
                                            INV_PR_QNT,
                                            INV_PR_DESCR,
                                            INV_PR_CUR_QNT,
                                            INV_PR_DISC,
                                            INV_PR_BUY_AMT,
                                            INV_PR_SELL_AMT,
                                            INV_PR_PROFIT,
                                            EXTRA_DISC,
                                            KIOSKID,
                                            UPDATED_AT
                                        FROM INVOICES_DET
                                        WHERE KIOSKID=:KIOSKID
                                        AND UPDATED_AT>:UPDATED_AT
                                        ORDER BY UPDATED_AT
                                        "

                    Using oraCmd As New OracleCommand(sql, conn)
                        oraCmd.BindByName = True
                        oraCmd.Parameters.Add("KIOSKID", OracleDbType.Varchar2).Value = kioskId
                        oraCmd.Parameters.Add("UPDATED_AT", OracleDbType.TimeStamp).Value = lastSync

                        Using dr = oraCmd.ExecuteReader()
                            Using tr = sqliteConn.BeginTransaction()
                                Dim upsert = CreateInvoicesDetUpsert(sqliteConn)
                                upsert.Transaction = tr

                                Dim newest As DateTime = lastSync
                                While dr.Read()
                                    For i = 0 To dr.FieldCount - 1
                                        upsert.Parameters(
                                                        "@" & dr.GetName(i)
                                                        ).Value =
                                                        If(IsDBNull(dr(i)),
                                                        DBNull.Value,
                                                        dr(i))
                                    Next

                                    upsert.ExecuteNonQuery()

                                    Dim upd = Convert.ToDateTime(dr("UPDATED_AT"))

                                    If upd > newest Then
                                        newest = upd
                                    End If
                                End While

                                tr.Commit()

                                SaveLastSync(sqliteConn, newest, "INVOICES_DET")
                            End Using
                        End Using
                    End Using
                End Using
            Catch ex As Exception
                CreateExceptionFile(WhoAmI & ": " & ex.Message, "")
            End Try
        End Sub

        Public Sub SyncInvoices()

            Dim WhoAmI As String = "SyncInvoices"

            Try
                Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                    sqliteConn.Open()
                    Dim lastSync As DateTime = GetLastSync(sqliteConn, "INVOICES")

                    Dim sql As String =
                                        "
                                        SELECT
                                            UUID,
                                            SERNO,
                                            I_NUMBER,
                                            SUPPLIER_ID,
                                            TOTAL_AMT,
                                            INV_DATE,
                                            CLOSED,
                                            EXTRA_DISC,
                                            KIOSKID,
                                            UPDATED_AT

                                        FROM INVOICES

                                        WHERE KIOSKID=:KIOSKID
                                        AND UPDATED_AT>:UPDATED_AT

                                        ORDER BY UPDATED_AT
                                        "
                    Using cmdOra As New OracleCommand(sql, conn)
                        cmdOra.BindByName = True
                        cmdOra.Parameters.Add("KIOSKID", OracleDbType.Varchar2).Value = kioskId
                        cmdOra.Parameters.Add("UPDATED_AT", OracleDbType.TimeStamp).Value = lastSync
                        Using dr = cmdOra.ExecuteReader()
                            Using tr = sqliteConn.BeginTransaction()
                                Dim upsert = CreateInvoicesUpsert(sqliteConn)
                                upsert.Transaction = tr

                                Dim newest As DateTime = lastSync

                                While dr.Read()
                                    For i = 0 To dr.FieldCount - 1
                                        upsert.Parameters(
                                                        "@" & dr.GetName(i)
                                                    ).Value =
                                                    If(IsDBNull(dr(i)),
                                                       DBNull.Value,
                                                       dr(i))
                                    Next
                                    upsert.ExecuteNonQuery()

                                    Dim upd = Convert.ToDateTime(dr("UPDATED_AT"))

                                    If upd > newest Then
                                        newest = upd
                                    End If
                                End While
                                tr.Commit()

                                SaveLastSync(sqliteConn, newest, "INVOICES")
                            End Using
                        End Using
                    End Using
                End Using
            Catch ex As Exception
                CreateExceptionFile(WhoAmI & ": " & ex.Message, "")
            End Try
        End Sub

        Private Function CreateInvoicesDetUpsert(sqliteConn As SQLiteConnection) As SQLiteCommand

            Dim sql As String =
                            "
                            INSERT INTO INVOICES_DET
                            (
                            UUID,
                            SERNO,
                            INV_SERNO,
                            INV_UUID,
                            INV_PR_SERNO,
                            PRODUCT_UUID,
                            INV_PR_QNT,
                            INV_PR_DESCR,
                            INV_PR_CUR_QNT,
                            INV_PR_DISC,
                            INV_PR_BUY_AMT,
                            INV_PR_SELL_AMT,
                            INV_PR_PROFIT,
                            EXTRA_DISC,
                            KIOSKID,
                            UPDATED_AT,
                            SYNC_STATUS
                            )

                            VALUES
                            (
                            @UUID,
                            @SERNO,
                            @INV_SERNO,
                            @INV_UUID,
                            @INV_PR_SERNO,
                            @PRODUCT_UUID,
                            @INV_PR_QNT,
                            @INV_PR_DESCR,
                            @INV_PR_CUR_QNT,
                            @INV_PR_DISC,
                            @INV_PR_BUY_AMT,
                            @INV_PR_SELL_AMT,
                            @INV_PR_PROFIT,
                            @EXTRA_DISC,
                            @KIOSKID,
                            @UPDATED_AT,
                            0
                            )


                            ON CONFLICT(UUID)

                            DO UPDATE SET

                            INV_PR_QNT=excluded.INV_PR_QNT,
                            INV_PR_DESCR=excluded.INV_PR_DESCR,
                            INV_PR_CUR_QNT=excluded.INV_PR_CUR_QNT,

                            INV_PR_DISC=excluded.INV_PR_DISC,

                            INV_PR_BUY_AMT=excluded.INV_PR_BUY_AMT,

                            INV_PR_SELL_AMT=excluded.INV_PR_SELL_AMT,

                            INV_PR_PROFIT=excluded.INV_PR_PROFIT,

                            EXTRA_DISC=excluded.EXTRA_DISC,

                            UPDATED_AT=excluded.UPDATED_AT
                            "

            Dim cmd As New SQLiteCommand(sql, sqliteConn)
            For Each p In
            {
            "UUID",
            "SERNO",
            "INV_SERNO",
            "INV_UUID",
            "INV_PR_SERNO",
            "PRODUCT_UUID",
            "INV_PR_QNT",
            "INV_PR_DESCR",
            "INV_PR_CUR_QNT",
            "INV_PR_DISC",
            "INV_PR_BUY_AMT",
            "INV_PR_SELL_AMT",
            "INV_PR_PROFIT",
            "EXTRA_DISC",
            "KIOSKID",
            "UPDATED_AT"
            }

                cmd.Parameters.Add("@" & p, DbType.Object)
            Next
            Return cmd
        End Function

        Public Sub UploadInvoicesDet()

            Dim sql As String =
                                "
                                SELECT *
                                FROM INVOICES_DET
                                WHERE SYNC_STATUS=1
                                AND KIOSKID=@KIOSKID
                                "

            Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                sqliteConn.Open()
                Using cmd As New SQLiteCommand(sql, sqliteConn)
                    cmd.Parameters.AddWithValue("@KIOSKID", kioskId)
                    Using dr = cmd.ExecuteReader()

                        While dr.Read()
                            UploadSingleInvoiceDet(sqliteConn, dr)
                        End While
                    End Using
                End Using
            End Using
        End Sub

        Private Sub UploadSingleInvoiceDet(sqliteConn As SQLiteConnection, dr As SQLiteDataReader)

            Dim sql As String =
                                "
                                MERGE INTO INVOICES_DET d

                                USING
                                (
                                SELECT :UUID UUID FROM dual
                                ) src

                                ON
                                (
                                d.UUID=src.UUID
                                )

                                WHEN MATCHED THEN UPDATE SET

                                d.INV_PR_QNT=:INV_PR_QNT,
                                d.INV_PR_DESCR=:INV_PR_DESCR,
                                d.INV_PR_CUR_QNT=:INV_PR_CUR_QNT,
                                d.INV_PR_DISC=:INV_PR_DISC,
                                d.INV_PR_BUY_AMT=:INV_PR_BUY_AMT,
                                d.INV_PR_SELL_AMT=:INV_PR_SELL_AMT,
                                d.INV_PR_PROFIT=:INV_PR_PROFIT,
                                d.EXTRA_DISC=:EXTRA_DISC,
                                d.UPDATED_AT=:UPDATED_AT


                                WHEN NOT MATCHED THEN INSERT
                                (
                                UUID,
                                SERNO,
                                INV_SERNO,
                                INV_UUID,
                                INV_PR_SERNO,
                                PRODUCT_UUID,
                                INV_PR_QNT,
                                INV_PR_DESCR,
                                INV_PR_CUR_QNT,
                                INV_PR_DISC,
                                INV_PR_BUY_AMT,
                                INV_PR_SELL_AMT,
                                INV_PR_PROFIT,
                                EXTRA_DISC,
                                KIOSKID,
                                UPDATED_AT
                                )

                                VALUES
                                (
                                :UUID,
                                INVOICES_DET_SEQ.NEXTVAL,
                                :INV_SERNO,
                                :INV_UUID,
                                :INV_PR_SERNO,
                                :PRODUCT_UUID,
                                :INV_PR_QNT,
                                :INV_PR_DESCR,
                                :INV_PR_CUR_QNT,
                                :INV_PR_DISC,
                                :INV_PR_BUY_AMT,
                                :INV_PR_SELL_AMT,
                                :INV_PR_PROFIT,
                                :EXTRA_DISC,
                                :KIOSKID,
                                :UPDATED_AT
                                )
                                "

            Using cmd As New OracleCommand(sql, conn)
                cmd.BindByName = True
                cmd.Parameters.Add("UUID", OracleDbType.Varchar2).Value = dr("UUID").ToString()
                'Lookup Oracle invoice SERNO from UUID
                cmd.Parameters.Add("INV_SERNO", OracleDbType.Int32).Value = GetInvoiceSernoByUUID(dr("INV_UUID").ToString())
                cmd.Parameters.Add("INV_UUID", OracleDbType.Varchar2).Value = dr("INV_UUID").ToString()
                'Lookup Oracle product SERNO from UUID
                cmd.Parameters.Add("INV_PR_SERNO", OracleDbType.Int32).Value = GetProductSernoByUUID(dr("PRODUCT_UUID").ToString())
                cmd.Parameters.Add("PRODUCT_UUID", OracleDbType.Varchar2).Value = dr("PRODUCT_UUID").ToString()
                cmd.Parameters.Add("INV_PR_QNT", OracleDbType.Int32).Value = Convert.ToInt32(dr("INV_PR_QNT"))
                cmd.Parameters.Add("INV_PR_DESCR", OracleDbType.Varchar2).Value = dr("INV_PR_DESCR").ToString()
                cmd.Parameters.Add("INV_PR_CUR_QNT", OracleDbType.Int32).Value = Convert.ToInt32(dr("INV_PR_CUR_QNT"))
                cmd.Parameters.Add("INV_PR_DISC", OracleDbType.Decimal).Value = Convert.ToDecimal(dr("INV_PR_DISC"))
                cmd.Parameters.Add("INV_PR_BUY_AMT", OracleDbType.Decimal).Value = Convert.ToDecimal(dr("INV_PR_BUY_AMT"))
                cmd.Parameters.Add("INV_PR_SELL_AMT", OracleDbType.Decimal).Value = Convert.ToDecimal(dr("INV_PR_SELL_AMT"))
                cmd.Parameters.Add("INV_PR_PROFIT", OracleDbType.Decimal).Value = Convert.ToDecimal(dr("INV_PR_PROFIT"))
                If IsDBNull(dr("EXTRA_DISC")) Then
                    cmd.Parameters.Add("EXTRA_DISC", OracleDbType.Decimal).Value = DBNull.Value
                Else
                    cmd.Parameters.Add("EXTRA_DISC", OracleDbType.Decimal).Value = Decimal.Parse(dr("EXTRA_DISC").ToString(), Globalization.CultureInfo.InvariantCulture)
                End If
                cmd.Parameters.Add("KIOSKID", OracleDbType.Varchar2).Value = dr("KIOSKID").ToString()
                cmd.Parameters.Add("UPDATED_AT", OracleDbType.TimeStamp).Value = If(IsDBNull(dr("UPDATED_AT")), DBNull.Value, Convert.ToDateTime(dr("UPDATED_AT")))
                cmd.ExecuteNonQuery()
            End Using
            MarkInvoiceDetSynced(sqliteConn, dr("UUID").ToString())
        End Sub

        Private Function GetInvoiceSernoByUUID(invoiceUUID As String) As Integer
            Using cmd As New OracleCommand("SELECT SERNO FROM INVOICES WHERE UUID=:UUID", conn)
                cmd.Parameters.Add("UUID", OracleDbType.Varchar2).Value = invoiceUUID
                Return Convert.ToInt32(cmd.ExecuteScalar())
            End Using
        End Function

        Private Function GetProductSernoByUUID(productUUID As String) As Integer
            Using cmd As New OracleCommand("SELECT SERNO FROM PRODUCTS WHERE UUID=:UUID", conn)
                cmd.Parameters.Add("UUID", OracleDbType.Varchar2).Value = productUUID
                Return Convert.ToInt32(cmd.ExecuteScalar())
            End Using
        End Function

        Private Sub MarkInvoiceDetSynced(sqliteConn As SQLiteConnection, uuid As String)

            Dim sql As String =
                                "
                                UPDATE INVOICES_DET
                                SET SYNC_STATUS=0
                                WHERE UUID=@UUID
                                "

            Using cmd As New SQLiteCommand(sql, sqliteConn)
                cmd.Parameters.AddWithValue("@UUID", uuid)
                cmd.ExecuteNonQuery()
            End Using
        End Sub

        Private Function CreateInvoicesUpsert(sqliteConn As SQLiteConnection) As SQLiteCommand

            Dim sql As String =
                                "
                                INSERT INTO INVOICES
                                (
                                UUID,
                                SERNO,
                                I_NUMBER,
                                SUPPLIER_ID,
                                TOTAL_AMT,
                                INV_DATE,
                                CLOSED,
                                EXTRA_DISC,
                                KIOSKID,
                                UPDATED_AT,
                                SYNC_STATUS
                                )
                                VALUES
                                (
                                @UUID,
                                @SERNO,
                                @I_NUMBER,
                                @SUPPLIER_ID,
                                @TOTAL_AMT,
                                @INV_DATE,
                                @CLOSED,
                                @EXTRA_DISC,
                                @KIOSKID,
                                @UPDATED_AT,
                                0
                                )

                                ON CONFLICT(UUID)

                                DO UPDATE SET

                                SERNO=excluded.SERNO,
                                I_NUMBER=excluded.I_NUMBER,
                                SUPPLIER_ID=excluded.SUPPLIER_ID,
                                TOTAL_AMT=excluded.TOTAL_AMT,
                                INV_DATE=excluded.INV_DATE,
                                CLOSED=excluded.CLOSED,
                                EXTRA_DISC=excluded.EXTRA_DISC,
                                UPDATED_AT=excluded.UPDATED_AT
                                "

            Dim cmd As New SQLiteCommand(sql, sqliteConn)
            For Each p In
            {
            "UUID",
            "SERNO",
            "I_NUMBER",
            "SUPPLIER_ID",
            "TOTAL_AMT",
            "INV_DATE",
            "CLOSED",
            "EXTRA_DISC",
            "KIOSKID",
            "UPDATED_AT"
            }

                cmd.Parameters.Add("@" & p, DbType.Object)
            Next
            Return cmd
        End Function

        Public Sub UploadInvoices()

            Dim sql As String =
                                "
                                SELECT *
                                FROM INVOICES
                                WHERE SYNC_STATUS=1
                                AND KIOSKID=@KIOSKID
                                "

            Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                sqliteConn.Open()
                Using cmd As New SQLiteCommand(sql, sqliteConn)
                    cmd.Parameters.AddWithValue("@KIOSKID", kioskId)
                    Using dr = cmd.ExecuteReader()
                        While dr.Read()
                            UploadSingleInvoice(sqliteConn, dr)
                        End While
                    End Using
                End Using
            End Using
        End Sub

        Private Sub UploadSingleInvoice(sqliteConn As SQLiteConnection, dr As SQLiteDataReader)

            Dim sql As String =
                            "
                            MERGE INTO INVOICES i

                            USING
                            (
                            SELECT :UUID UUID FROM dual
                            ) src

                            ON
                            (
                            i.UUID = src.UUID
                            )


                            WHEN MATCHED THEN UPDATE SET

                            i.I_NUMBER=:I_NUMBER,
                            i.SUPPLIER_ID=:SUPPLIER_ID,
                            i.TOTAL_AMT=:TOTAL_AMT,
                            i.INV_DATE=:INV_DATE,
                            i.CLOSED=:CLOSED,
                            i.EXTRA_DISC=:EXTRA_DISC,
                            i.UPDATED_AT=:UPDATED_AT


                            WHEN NOT MATCHED THEN INSERT
                            (
                            UUID,
                            SERNO,
                            I_NUMBER,
                            SUPPLIER_ID,
                            TOTAL_AMT,
                            INV_DATE,
                            CLOSED,
                            EXTRA_DISC,
                            KIOSKID,
                            UPDATED_AT
                            )

                            VALUES
                            (
                            :UUID,
                            invoicesSeq.NEXTVAL,
                            :I_NUMBER,
                            :SUPPLIER_ID,
                            :TOTAL_AMT,
                            :INV_DATE,
                            :CLOSED,
                            :EXTRA_DISC,
                            :KIOSKID,
                            :UPDATED_AT
                            )
                            "

            Using cmd As New OracleCommand(sql, conn)
                cmd.BindByName = True
                cmd.Parameters.Add("UUID", OracleDbType.Varchar2).Value = dr("UUID").ToString()
                cmd.Parameters.Add("I_NUMBER", OracleDbType.Varchar2).Value = dr("I_NUMBER").ToString()
                cmd.Parameters.Add("SUPPLIER_ID", OracleDbType.Varchar2).Value = dr("SUPPLIER_ID").ToString()
                cmd.Parameters.Add("TOTAL_AMT", OracleDbType.Decimal).Value = If(IsDBNull(dr("TOTAL_AMT")), DBNull.Value, Convert.ToDecimal(dr("TOTAL_AMT")))
                cmd.Parameters.Add("INV_DATE", OracleDbType.TimeStamp).Value = If(IsDBNull(dr("INV_DATE")), DBNull.Value, Convert.ToDateTime(dr("INV_DATE")))
                cmd.Parameters.Add("CLOSED", OracleDbType.Int32).Value = If(IsDBNull(dr("CLOSED")), 0, Convert.ToInt32(dr("CLOSED")))
                cmd.Parameters.Add("EXTRA_DISC", OracleDbType.Decimal).Value = If(IsDBNull(dr("EXTRA_DISC")), 0D, Convert.ToDecimal(dr("EXTRA_DISC")))
                cmd.Parameters.Add("KIOSKID", OracleDbType.Varchar2).Value = dr("KIOSKID").ToString()
                cmd.Parameters.Add("UPDATED_AT", OracleDbType.TimeStamp).Value = If(IsDBNull(dr("UPDATED_AT")), DBNull.Value, Convert.ToDateTime(dr("UPDATED_AT")))
                cmd.ExecuteNonQuery()
            End Using

            MarkInvoiceSynced(sqliteConn, dr("UUID").ToString())

        End Sub

        Private Sub MarkInvoiceSynced(sqliteConn As SQLiteConnection, uuid As String)

            Dim sql As String =
                                "
                                UPDATE INVOICES
                                SET SYNC_STATUS=0
                                WHERE UUID=@UUID
                                "

            Using cmd As New SQLiteCommand(sql, sqliteConn)
                cmd.Parameters.AddWithValue("@UUID", uuid)
                cmd.ExecuteNonQuery()
            End Using
        End Sub

        Public Sub SyncZReport()
            Dim WhoAmI As String = "SyncZReport"
            Try
                Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                    sqliteConn.Open()
                    Dim lastSync As DateTime = GetLastSync(sqliteConn, "Z_REPORT")
                    Dim sql As String =
                                        "
                                        SELECT
                                            Z_UUID,
                                            Z_DATE,
                                            Z_SEQ,
                                            TOTAL_RECEIPTS,
                                            TOTAL_AMOUNT0,
                                            TOTAL_AMOUNT3,
                                            TOTAL_AMOUNT5,
                                            TOTAL_AMOUNT19,
                                            TOTAL_AMOUNT,
                                            KIOSKID,
                                            UPDATED_AT
                                        FROM Z_REPORT
                                        WHERE KIOSKID=:KIOSKID
                                        AND UPDATED_AT>:UPDATED_AT
                                        ORDER BY UPDATED_AT
                                        "
                    Using oraCmd As New OracleCommand(sql, conn)
                        oraCmd.BindByName = True
                        oraCmd.Parameters.Add("KIOSKID", OracleDbType.Varchar2).Value = kioskId
                        oraCmd.Parameters.Add("UPDATED_AT", OracleDbType.TimeStamp).Value = lastSync
                        Using dr = oraCmd.ExecuteReader()
                            Using tr = sqliteConn.BeginTransaction()
                                Dim upsert = CreateZReportUpsert(sqliteConn)
                                upsert.Transaction = tr
                                Dim newest As DateTime = lastSync

                                While dr.Read()
                                    For i = 0 To dr.FieldCount - 1
                                        upsert.Parameters("@" & dr.GetName(i)).Value = If(IsDBNull(dr(i)), DBNull.Value, dr(i))
                                    Next
                                    upsert.ExecuteNonQuery()

                                    Dim upd = Convert.ToDateTime(dr("UPDATED_AT"))
                                    If upd > newest Then
                                        newest = upd
                                    End If
                                End While
                                tr.Commit()

                                SaveLastSync(sqliteConn, newest, "Z_REPORT")
                            End Using
                        End Using
                    End Using
                End Using
            Catch ex As Exception
                CreateExceptionFile(WhoAmI & ": " & ex.Message, "")
            End Try
        End Sub

        Private Function CreateZReportUpsert(sqliteConn As SQLiteConnection) As SQLiteCommand

            Dim sql As String =
                            "
                            INSERT INTO Z_REPORT
                            (
                            Z_UUID,
                            Z_DATE,
                            Z_SEQ,
                            TOTAL_RECEIPTS,
                            TOTAL_AMOUNT0,
                            TOTAL_AMOUNT3,
                            TOTAL_AMOUNT5,
                            TOTAL_AMOUNT19,
                            TOTAL_AMOUNT,
                            KIOSKID,
                            UPDATED_AT,
                            SYNC_STATUS
                            )
                            VALUES
                            (
                            @Z_UUID,
                            @Z_DATE,
                            @Z_SEQ,
                            @TOTAL_RECEIPTS,
                            @TOTAL_AMOUNT0,
                            @TOTAL_AMOUNT3,
                            @TOTAL_AMOUNT5,
                            @TOTAL_AMOUNT19,
                            @TOTAL_AMOUNT,
                            @KIOSKID,
                            @UPDATED_AT,
                            0
                            )

                            ON CONFLICT(Z_UUID)

                            DO UPDATE SET

                            Z_DATE=excluded.Z_DATE,
                            Z_SEQ=excluded.Z_SEQ,
                            TOTAL_RECEIPTS=excluded.TOTAL_RECEIPTS,

                            TOTAL_AMOUNT0=excluded.TOTAL_AMOUNT0,
                            TOTAL_AMOUNT3=excluded.TOTAL_AMOUNT3,
                            TOTAL_AMOUNT5=excluded.TOTAL_AMOUNT5,
                            TOTAL_AMOUNT19=excluded.TOTAL_AMOUNT19,

                            TOTAL_AMOUNT=excluded.TOTAL_AMOUNT,

                            UPDATED_AT=excluded.UPDATED_AT
                            "

            Dim cmd As New SQLiteCommand(sql, sqliteConn)
            For Each p In
            {
            "Z_UUID",
            "Z_DATE",
            "Z_SEQ",
            "TOTAL_RECEIPTS",
            "TOTAL_AMOUNT0",
            "TOTAL_AMOUNT3",
            "TOTAL_AMOUNT5",
            "TOTAL_AMOUNT19",
            "TOTAL_AMOUNT",
            "KIOSKID",
            "UPDATED_AT"
            }
                cmd.Parameters.Add("@" & p, DbType.Object)
            Next
            Return cmd
        End Function

        Public Sub SyncXReport()

            Dim WhoAmI As String = "SyncXReport"
            Dim lastSync As DateTime = GetLastSyncValue("X_REPORT_LAST_SYNC")
            Dim sql As String =
                                "
                                SELECT
                                    USER_ID,
                                    FROM_DATE,
                                    TO_DATE,
                                    TOTAL_RECEIPTS,
                                    TOTAL_AMT,
                                    TOTAL5PERCENT,
                                    TOTAL19PERCENT,
                                    INITIAL_AMT,
                                    FINAL_AMT,
                                    PAYMENTS,
                                    CREATED_ON,
                                    TOTAL0PERCENT,
                                    AMOUNT_LAXEIA,
                                    INITIALAMTLAXEIA,
                                    AMOUNTVISA,
                                    FINALAMTLAXEIA,
                                    DESCRIPTION,
                                    TOTAL3PERCENT,
                                    KIOSKID,
                                    UPDATED_AT
                                FROM X_REPORT
                                WHERE KIOSKID=:KIOSKID
                                AND UPDATED_AT>:UPDATED_AT
                                ORDER BY UPDATED_AT
                                "
            Try
                Using oraCmd As New OracleCommand(sql, conn)
                    oraCmd.BindByName = True
                    oraCmd.Parameters.Add("KIOSKID", OracleDbType.Varchar2).Value = kioskId
                    oraCmd.Parameters.Add("UPDATED_AT", OracleDbType.TimeStamp).Value = lastSync
                    Using dr = oraCmd.ExecuteReader()

                        Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                            sqliteConn.Open()
                            Using tr = sqliteConn.BeginTransaction()

                                Dim upsert = CreateXReportUpsert(sqliteConn)
                                upsert.Transaction = tr
                                Dim newest = lastSync
                                While dr.Read()
                                    For i = 0 To dr.FieldCount - 1
                                        upsert.Parameters(
                                                        "@" & dr.GetName(i)
                                                    ).Value =
                                                    If(IsDBNull(dr(i)),
                                                       DBNull.Value,
                                                       dr(i))
                                    Next

                                    upsert.ExecuteNonQuery()

                                    Dim upd = Convert.ToDateTime(dr("UPDATED_AT"))
                                    If upd > newest Then
                                        newest = upd
                                    End If
                                End While
                                tr.Commit()

                                SaveLastSync(sqliteConn, newest, "X_REPORT")
                            End Using
                        End Using
                    End Using
                End Using
            Catch ex As Exception
                CreateExceptionFile(WhoAmI & ": " & ex.Message, sql)
            End Try
        End Sub

        Private Function CreateXReportUpsert(sqliteConn As SQLiteConnection) As SQLiteCommand

            Dim sql As String =
                                "
                                INSERT INTO X_REPORT
                                (
                                USER_ID,
                                FROM_DATE,
                                TO_DATE,
                                TOTAL_RECEIPTS,
                                TOTAL_AMT,
                                TOTAL5PERCENT,
                                TOTAL19PERCENT,
                                INITIAL_AMT,
                                FINAL_AMT,
                                PAYMENTS,
                                CREATED_ON,
                                TOTAL0PERCENT,
                                AMOUNT_LAXEIA,
                                INITIALAMTLAXEIA,
                                AMOUNTVISA,
                                FINALAMTLAXEIA,
                                DESCRIPTION,
                                TOTAL3PERCENT,
                                KIOSKID,
                                UPDATED_AT,
                                SYNC_STATUS
                                )
                                VALUES
                                (
                                @USER_ID,
                                @FROM_DATE,
                                @TO_DATE,
                                @TOTAL_RECEIPTS,
                                @TOTAL_AMT,
                                @TOTAL5PERCENT,
                                @TOTAL19PERCENT,
                                @INITIAL_AMT,
                                @FINAL_AMT,
                                @PAYMENTS,
                                @CREATED_ON,
                                @TOTAL0PERCENT,
                                @AMOUNT_LAXEIA,
                                @INITIALAMTLAXEIA,
                                @AMOUNTVISA,
                                @FINALAMTLAXEIA,
                                @DESCRIPTION,
                                @TOTAL3PERCENT,
                                @KIOSKID,
                                @UPDATED_AT,
                                0
                                )


                                ON CONFLICT
                                (
                                USER_ID,
                                FROM_DATE,
                                TO_DATE,
                                KIOSKID
                                )

                                DO UPDATE SET

                                TOTAL_RECEIPTS=excluded.TOTAL_RECEIPTS,
                                TOTAL_AMT=excluded.TOTAL_AMT,
                                FINAL_AMT=excluded.FINAL_AMT,
                                PAYMENTS=excluded.PAYMENTS,
                                DESCRIPTION=excluded.DESCRIPTION,
                                UPDATED_AT=excluded.UPDATED_AT
                                "

            Dim cmd As New SQLiteCommand(sql, sqliteConn)
            For Each p In
                        {
                        "USER_ID",
                        "FROM_DATE",
                        "TO_DATE",
                        "TOTAL_RECEIPTS",
                        "TOTAL_AMT",
                        "TOTAL5PERCENT",
                        "TOTAL19PERCENT",
                        "INITIAL_AMT",
                        "FINAL_AMT",
                        "PAYMENTS",
                        "CREATED_ON",
                        "TOTAL0PERCENT",
                        "AMOUNT_LAXEIA",
                        "INITIALAMTLAXEIA",
                        "AMOUNTVISA",
                        "FINALAMTLAXEIA",
                        "DESCRIPTION",
                        "TOTAL3PERCENT",
                        "KIOSKID",
                        "UPDATED_AT"
                        }

                cmd.Parameters.Add("@" & p, DbType.Object)
            Next
            Return cmd
        End Function
        Public Sub UploadProducts()
            Dim WhoAmI As String = "UploadProducts"
            Dim sql As String =
                                "
                                SELECT *
                                FROM PRODUCTS
                                WHERE SYNC_STATUS = 1
                                AND KIOSKID=@KIOSKID
                                "
            Try
                Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                    sqliteConn.Open()
                    Using cmd As New SQLiteCommand(sql, sqliteConn)
                        cmd.Parameters.AddWithValue("@KIOSKID", kioskId)
                        Using dr As SQLiteDataReader = cmd.ExecuteReader()
                            While dr.Read()
                                UploadSingleProduct(sqliteConn, dr)
                            End While
                        End Using
                    End Using
                End Using
            Catch ex As Exception
                CreateExceptionFile(WhoAmI & ": " & ex.Message, sql)
            End Try
        End Sub

        Private Sub UploadSingleProduct(sqliteConn As SQLiteConnection, dr As SQLiteDataReader)
            Dim WhoAmI As String = "UploadSingleProduct"
            Dim sql As String =
                                "
                                MERGE INTO PRODUCTS p

                                USING
                                (
                                    SELECT :UUID UUID FROM dual
                                ) src

                                ON
                                (
                                    p.UUID = src.UUID
                                )


                                WHEN MATCHED THEN UPDATE SET

                                    p.DESCRIPTION = :DESCRIPTION,
                                    p.MIN_QUANTITY = :MIN_QUANTITY,
                                    p.AVAIL_QUANTITY = :AVAIL_QUANTITY,

                                    p.ALERT_ON_MIN = :ALERT_ON_MIN,
                                    p.EXPIRY_DATE = :EXPIRY_DATE,
                                    p.ALERT_ON_EXPIRY = :ALERT_ON_EXPIRY,
                                    p.ALERT_DATE = :ALERT_DATE,

                                    p.BUY_AMT = :BUY_AMT,
                                    p.SELL_AMT = :SELL_AMT,

                                    p.CATEGORY_ID = :CATEGORY_ID,
                                    p.SUPPLIER_ID = :SUPPLIER_ID,

                                    p.NOTES = :NOTES,

                                    p.STOCK_QUANTITY = :STOCK_QUANTITY,

                                    p.PROFIT_PERCENT = :PROFIT_PERCENT,
                                    p.AMT_PROFIT = :AMT_PROFIT,

                                    p.OFFER = :OFFER,
                                    p.OFFER_TYPE = :OFFER_TYPE,
                                    p.OFFER_X = :OFFER_X,
                                    p.OFFER_Y = :OFFER_Y,
                                    p.OFFER_DISC = :OFFER_DISC,
                                    p.OFFER_AT = :OFFER_AT,

                                    p.LASTMODIFIEDBY = :LASTMODIFIEDBY,
                                    p.LASTMODIFIEDSCREEN = :LASTMODIFIEDSCREEN,

                                    p.VATTYPE_ID = :VATTYPE_ID,

                                    p.ISBOX = :ISBOX,
                                    p.BOX_QNT = :BOX_QNT,

                                    p.OFFERFROMDATE = :OFFERFROMDATE,
                                    p.OFFERTODATE = :OFFERTODATE,

                                    p.BUY_AMT_NO_VAT = :BUY_AMT_NO_VAT,
                                    p.BUY_AMT_NEW = :BUY_AMT_NEW,

                                    p.UPDATED_AT = :UPDATED_AT


                                WHEN NOT MATCHED THEN INSERT
                                (
                                    UUID,
                                    SERNO,

                                    DESCRIPTION,

                                    MIN_QUANTITY,
                                    AVAIL_QUANTITY,

                                    ALERT_ON_MIN,
                                    EXPIRY_DATE,
                                    ALERT_ON_EXPIRY,
                                    ALERT_DATE,

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
                                    UPDATED_AT
                                )

                                VALUES
                                (
                                    :UUID,
                                    :SERNO,

                                    :DESCRIPTION,

                                    :MIN_QUANTITY,
                                    :AVAIL_QUANTITY,

                                    :ALERT_ON_MIN,
                                    :EXPIRY_DATE,
                                    :ALERT_ON_EXPIRY,
                                    :ALERT_DATE,

                                    :BUY_AMT,
                                    :SELL_AMT,

                                    :CATEGORY_ID,
                                    :SUPPLIER_ID,

                                    :NOTES,

                                    :STOCK_QUANTITY,

                                    :PROFIT_PERCENT,
                                    :AMT_PROFIT,

                                    :OFFER,
                                    :OFFER_TYPE,
                                    :OFFER_X,
                                    :OFFER_Y,
                                    :OFFER_DISC,
                                    :OFFER_AT,

                                    :LASTMODIFIEDBY,
                                    :LASTMODIFIEDSCREEN,

                                    :VATTYPE_ID,

                                    :ISBOX,
                                    :BOX_QNT,

                                    :OFFERFROMDATE,
                                    :OFFERTODATE,

                                    :BUY_AMT_NO_VAT,
                                    :BUY_AMT_NEW,

                                    :KIOSKID,
                                    :UPDATED_AT
                                )
                                "

            Try
                Using cmd As New OracleCommand(sql, conn)
                    cmd.BindByName = True
                    For i As Integer = 0 To dr.FieldCount - 1
                        Dim col As String = dr.GetName(i).ToUpper()
                        If col = "SYNC_STATUS" Then
                            Continue For
                        End If

                        Dim value As Object = If(IsDBNull(dr(i)), DBNull.Value, dr(i))

                        Select Case col
                            '-------------------------
                            ' TIMESTAMP columns
                            '-------------------------
                            Case "EXPIRY_DATE",
                                 "ALERT_DATE",
                                 "OFFERFROMDATE",
                                 "OFFERTODATE",
                                 "UPDATED_AT"

                                cmd.Parameters.Add(col, OracleDbType.TimeStamp).Value = If(value Is DBNull.Value, DBNull.Value, Convert.ToDateTime(value))

                            '-------------------------
                            ' INTEGER columns
                            '-------------------------
                            Case "SERNO",
                                 "MIN_QUANTITY",
                                 "AVAIL_QUANTITY",
                                 "ALERT_ON_MIN",
                                 "ALERT_ON_EXPIRY",
                                 "STOCK_QUANTITY",
                                 "OFFER",
                                 "OFFER_TYPE",
                                 "OFFER_X",
                                 "OFFER_Y",
                                 "OFFER_AT",
                                 "LASTMODIFIEDSCREEN",
                                 "ISBOX",
                                 "BOX_QNT"

                                cmd.Parameters.Add(col, OracleDbType.Int32).Value = If(value Is DBNull.Value, DBNull.Value, Convert.ToInt32(value))

                            '-------------------------
                            ' DECIMAL columns
                            '-------------------------
                            Case "BUY_AMT",
                                 "SELL_AMT",
                                 "PROFIT_PERCENT",
                                 "AMT_PROFIT",
                                 "OFFER_DISC",
                                 "BUY_AMT_NO_VAT",
                                 "BUY_AMT_NEW"

                                cmd.Parameters.Add(col, OracleDbType.Decimal).Value = If(value Is DBNull.Value, DBNull.Value, Convert.ToDecimal(value))

                            '-------------------------
                            ' CLOB columns
                            '-------------------------
                            Case "NOTES"
                                cmd.Parameters.Add(col, OracleDbType.Clob).Value = If(value Is DBNull.Value, DBNull.Value, value.ToString())

                                '-------------------------
                                ' Everything else
                                '-------------------------
                            Case Else
                                cmd.Parameters.Add(col, OracleDbType.Varchar2).Value = value
                        End Select
                    Next

                    For Each p As OracleParameter In cmd.Parameters
                        Debug.WriteLine($"{p.ParameterName} - {p.OracleDbType} - {If(p.Value Is DBNull.Value, "NULL", p.Value)}")
                    Next
                    cmd.ExecuteNonQuery()
                End Using

                MarkProductSynced(sqliteConn, dr("UUID").ToString())
            Catch ex As Exception
                CreateExceptionFile(WhoAmI & ": " & ex.Message, sql)
            End Try
        End Sub

        Private Sub AddOracleParameter(cmd As OracleCommand, name As String, type As OracleDbType, value As Object)
            Dim p As OracleParameter = cmd.Parameters.Add(name, type)
            If value Is Nothing OrElse IsDBNull(value) Then
                p.Value = DBNull.Value
            Else
                p.Value = value
            End If
        End Sub

        Private Function DBInt(value As Object) As Object
            If value Is DBNull.Value Then
                Return DBNull.Value
            End If
            Return Convert.ToInt32(value)
        End Function

        Private Function DBDecimal(value As Object) As Object
            If value Is DBNull.Value Then
                Return DBNull.Value
            End If
            Return Convert.ToDecimal(value)
        End Function

        Private Function DBString(value As Object) As Object
            If value Is DBNull.Value Then
                Return DBNull.Value
            End If
            Return value.ToString()
        End Function

        Private Function DBTimestamp(value As Object) As Object
            If value Is DBNull.Value Then
                Return DBNull.Value
            End If
            Return Convert.ToDateTime(value)
        End Function

        Private Sub MarkProductSynced(sqliteConn As SQLiteConnection, uuid As String)
            Dim sql As String =
                                "
                                UPDATE PRODUCTS
                                SET SYNC_STATUS=0
                                WHERE UUID=@UUID
                                "

            Using cmd As New SQLiteCommand(sql, sqliteConn)
                cmd.Parameters.AddWithValue("@UUID", uuid)
                cmd.ExecuteNonQuery()
            End Using
        End Sub

        Public Sub UploadUsers()

            Dim WhoAmI As String = "UploadUsers"
            Dim sql As String =
                                "
                                SELECT *
                                FROM USERS
                                WHERE SYNC_STATUS = 1
                                AND KIOSKID=@KIOSKID
                                "
            Try
                Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                    sqliteConn.Open()
                    Using cmd As New SQLiteCommand(sql, sqliteConn)
                        cmd.Parameters.AddWithValue("@KIOSKID", kioskId)
                        Using dr As SQLiteDataReader = cmd.ExecuteReader()
                            While dr.Read()
                                UploadSingleUser(sqliteConn, dr)
                            End While
                        End Using
                    End Using
                End Using
            Catch ex As Exception
                CreateExceptionFile(WhoAmI & ": " & ex.Message, sql)
            End Try
        End Sub

        Private Sub UploadSingleUser(sqliteConn As SQLiteConnection, dr As SQLiteDataReader)
            Dim WhoAmI As String = "UploadSingleUser"
            Dim sql As String =
                                "
                                MERGE INTO USERS u

                                USING
                                (
                                    SELECT :UUID UUID FROM dual
                                ) src

                                ON
                                (
                                    u.UUID = src.UUID
                                )


                                WHEN MATCHED THEN UPDATE SET

                                    u.USERNAME = :USERNAME,
                                    u.DELETED = :DELETED,
                                    u.ACCESS_LEVEL = :ACCESS_LEVEL,
                                    u.IS_UNLOCK = :IS_UNLOCK,
                                    u.PASS = :PASS,
                                    u.FULLNAME = :FULLNAME,
                                    u.PHONE = :PHONE,
                                    u.ADDRESS = :ADDRESS,
                                    u.ID_NUM = :ID_NUM,

                                    u.DELETED_BY = :DELETED_BY,
                                    u.CREATED_BY = :CREATED_BY,

                                    u.VIEW_REPORTS = :VIEW_REPORTS,
                                    u.EDIT_PROD = :EDIT_PROD,
                                    u.EDIT_PROD_FULL = :EDIT_PROD_FULL,

                                    u.KIOSKID = :KIOSKID,
                                    u.UPDATED_AT = :UPDATED_AT


                                WHEN NOT MATCHED THEN

                                INSERT
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
                                    :UUID,
                                    :USERNAME,
                                    :DELETED,
                                    :ACCESS_LEVEL,
                                    :IS_UNLOCK,
                                    :PASS,
                                    :FULLNAME,
                                    :PHONE,
                                    :ADDRESS,
                                    :ID_NUM,
                                    :DELETED_BY,
                                    :CREATED_BY,
                                    :VIEW_REPORTS,
                                    :EDIT_PROD,
                                    :EDIT_PROD_FULL,
                                    :KIOSKID,
                                    :UPDATED_AT
                                )
                                "
            Try
                Using cmd As New OracleCommand(sql, conn)
                    cmd.BindByName = True
                    cmd.Parameters.Add("UUID", OracleDbType.Varchar2).Value = dr("UUID")
                    cmd.Parameters.Add("USERNAME", OracleDbType.Varchar2).Value = dr("USERNAME")
                    cmd.Parameters.Add("DELETED", OracleDbType.Int32).Value = dr("DELETED")
                    cmd.Parameters.Add("ACCESS_LEVEL", OracleDbType.Int32).Value = dr("ACCESS_LEVEL")
                    cmd.Parameters.Add("IS_UNLOCK", OracleDbType.Int32).Value = dr("IS_UNLOCK")
                    cmd.Parameters.Add("PASS", OracleDbType.Varchar2).Value = dr("PASS")
                    cmd.Parameters.Add("FULLNAME", OracleDbType.Varchar2).Value = dr("FULLNAME")
                    cmd.Parameters.Add("PHONE", OracleDbType.Varchar2).Value = dr("PHONE")
                    cmd.Parameters.Add("ADDRESS", OracleDbType.Varchar2).Value = dr("ADDRESS")
                    cmd.Parameters.Add("ID_NUM", OracleDbType.Varchar2).Value = dr("ID_NUM")
                    cmd.Parameters.Add("DELETED_BY", OracleDbType.Varchar2).Value = If(IsDBNull(dr("DELETED_BY")), DBNull.Value, dr("DELETED_BY"))
                    cmd.Parameters.Add("CREATED_BY", OracleDbType.Varchar2).Value = If(IsDBNull(dr("CREATED_BY")), DBNull.Value, dr("CREATED_BY"))
                    cmd.Parameters.Add("VIEW_REPORTS", OracleDbType.Int32).Value = dr("VIEW_REPORTS")
                    cmd.Parameters.Add("EDIT_PROD", OracleDbType.Int32).Value = dr("EDIT_PROD")
                    cmd.Parameters.Add("EDIT_PROD_FULL", OracleDbType.Int32).Value = dr("EDIT_PROD_FULL")
                    cmd.Parameters.Add("KIOSKID", OracleDbType.Varchar2).Value = dr("KIOSKID")
                    cmd.Parameters.Add("UPDATED_AT", OracleDbType.TimeStamp).Value = Convert.ToDateTime(dr("UPDATED_AT"))
                    cmd.ExecuteNonQuery()
                End Using

                MarkUserSynced(sqliteConn, dr("UUID").ToString())
            Catch ex As Exception
                CreateExceptionFile(WhoAmI & ": " & ex.Message, sql)
            End Try
        End Sub

        Private Sub MarkUserSynced(sqliteConn As SQLiteConnection, uuid As String)

            Dim sql As String =
                                "
                                UPDATE USERS
                                SET SYNC_STATUS=0
                                WHERE UUID=@UUID
                                "

            Using cmd As New SQLiteCommand(sql, sqliteConn)
                cmd.Parameters.AddWithValue("@UUID", uuid)
                cmd.ExecuteNonQuery()
            End Using
        End Sub

        Public Sub UploadZReport()
            Dim sql As String =
                                "
                                SELECT *
                                FROM Z_REPORT
                                WHERE SYNC_STATUS=1
                                AND KIOSKID=@KIOSKID
                                "

            Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                sqliteConn.Open()
                Using cmd As New SQLiteCommand(sql, sqliteConn)
                    cmd.Parameters.AddWithValue("@KIOSKID", kioskId)
                    Using dr = cmd.ExecuteReader()

                        While dr.Read()
                            UploadSingleZReport(sqliteConn, dr)
                        End While
                    End Using
                End Using
            End Using
        End Sub

        Private Sub UploadSingleZReport(sqliteConn As SQLiteConnection, dr As SQLiteDataReader)

            Dim sql As String =
                            "
                            MERGE INTO Z_REPORT z

                            USING
                            (
                            SELECT :Z_UUID Z_UUID FROM dual
                            ) src

                            ON
                            (
                            z.Z_UUID = src.Z_UUID
                            )

                            WHEN MATCHED THEN UPDATE SET

                            z.Z_DATE=:Z_DATE,
                            z.Z_SEQ=:Z_SEQ,

                            z.TOTAL_RECEIPTS=:TOTAL_RECEIPTS,

                            z.TOTAL_AMOUNT0=:TOTAL_AMOUNT0,
                            z.TOTAL_AMOUNT3=:TOTAL_AMOUNT3,
                            z.TOTAL_AMOUNT5=:TOTAL_AMOUNT5,
                            z.TOTAL_AMOUNT19=:TOTAL_AMOUNT19,

                            z.TOTAL_AMOUNT=:TOTAL_AMOUNT,

                            z.UPDATED_AT=:UPDATED_AT


                            WHEN NOT MATCHED THEN INSERT
                            (
                            Z_UUID,
                            Z_DATE,
                            Z_SEQ,
                            TOTAL_RECEIPTS,
                            TOTAL_AMOUNT0,
                            TOTAL_AMOUNT3,
                            TOTAL_AMOUNT5,
                            TOTAL_AMOUNT19,
                            TOTAL_AMOUNT,
                            KIOSKID,
                            UPDATED_AT
                            )

                            VALUES
                            (
                            :Z_UUID,
                            :Z_DATE,
                            :Z_SEQ,
                            :TOTAL_RECEIPTS,
                            :TOTAL_AMOUNT0,
                            :TOTAL_AMOUNT3,
                            :TOTAL_AMOUNT5,
                            :TOTAL_AMOUNT19,
                            :TOTAL_AMOUNT,
                            :KIOSKID,
                            :UPDATED_AT
                            )
                            "

            Using cmd As New OracleCommand(sql, conn)
                cmd.BindByName = True
                For Each c In
                {
                "Z_UUID",
                "Z_DATE",
                "Z_SEQ",
                "TOTAL_RECEIPTS",
                "TOTAL_AMOUNT0",
                "TOTAL_AMOUNT3",
                "TOTAL_AMOUNT5",
                "TOTAL_AMOUNT19",
                "TOTAL_AMOUNT",
                "KIOSKID",
                "UPDATED_AT"
                }

                    cmd.Parameters.Add(c, OracleDbType.Varchar2).Value = dr(c)
                Next
                cmd.ExecuteNonQuery()
            End Using

            MarkZReportSynced(sqliteConn, dr("Z_UUID").ToString())
        End Sub

        Private Sub MarkZReportSynced(sqliteConn As SQLiteConnection, uuid As String)

            Dim sql As String =
                                "
                                UPDATE Z_REPORT
                                SET SYNC_STATUS=0
                                WHERE Z_UUID=@UUID
                                "
            Using cmd As New SQLiteCommand(sql, sqliteConn)
                cmd.Parameters.AddWithValue("@UUID", uuid)
                cmd.ExecuteNonQuery()
            End Using
        End Sub

        Public Sub UploadXReport()
            Dim sql As String =
                                "
                                SELECT *
                                FROM X_REPORT
                                WHERE SYNC_STATUS=1
                                AND KIOSKID=@KIOSKID
                                "

            Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                sqliteConn.Open()
                Using cmd As New SQLiteCommand(sql, sqliteConn)
                    cmd.Parameters.AddWithValue("@KIOSKID", kioskId)
                    Using dr = cmd.ExecuteReader()
                        While dr.Read()
                            UploadSingleXReport(sqliteConn, dr)
                        End While
                    End Using
                End Using
            End Using
        End Sub

        Private Sub UploadSingleXReport(sqliteConn As SQLiteConnection, dr As SQLiteDataReader)

            Dim WhoAmI As String = "UploadSingleXReport"
            Dim sql As String =
                                "
                                MERGE INTO X_REPORT x

                                USING
                                (
                                    SELECT
                                        :USER_ID USER_ID,
                                        :FROM_DATE FROM_DATE,
                                        :TO_DATE TO_DATE,
                                        :KIOSKID KIOSKID
                                    FROM dual
                                ) src

                                ON
                                (
                                    x.USER_ID = src.USER_ID
                                    AND x.FROM_DATE = src.FROM_DATE
                                    AND x.TO_DATE = src.TO_DATE
                                    AND x.KIOSKID = src.KIOSKID
                                )

                                WHEN MATCHED THEN UPDATE SET

                                    x.TOTAL_RECEIPTS = :TOTAL_RECEIPTS,
                                    x.TOTAL_AMT = :TOTAL_AMT,
                                    x.TOTAL5PERCENT = :TOTAL5PERCENT,
                                    x.TOTAL19PERCENT = :TOTAL19PERCENT,
                                    x.TOTAL0PERCENT = :TOTAL0PERCENT,
                                    x.TOTAL3PERCENT = :TOTAL3PERCENT,

                                    x.INITIAL_AMT = :INITIAL_AMT,
                                    x.FINAL_AMT = :FINAL_AMT,

                                    x.PAYMENTS = :PAYMENTS,

                                    x.AMOUNT_LAXEIA = :AMOUNT_LAXEIA,
                                    x.INITIALAMTLAXEIA = :INITIALAMTLAXEIA,
                                    x.AMOUNTVISA = :AMOUNTVISA,
                                    x.FINALAMTLAXEIA = :FINALAMTLAXEIA,

                                    x.DESCRIPTION = :DESCRIPTION,

                                    x.UPDATED_AT = :UPDATED_AT


                                WHEN NOT MATCHED THEN

                                INSERT
                                (
                                    USER_ID,
                                    FROM_DATE,
                                    TO_DATE,

                                    TOTAL_RECEIPTS,

                                    TOTAL_AMT,

                                    TOTAL5PERCENT,
                                    TOTAL19PERCENT,
                                    TOTAL0PERCENT,
                                    TOTAL3PERCENT,

                                    INITIAL_AMT,
                                    FINAL_AMT,

                                    PAYMENTS,

                                    CREATED_ON,

                                    AMOUNT_LAXEIA,
                                    INITIALAMTLAXEIA,
                                    AMOUNTVISA,
                                    FINALAMTLAXEIA,

                                    DESCRIPTION,

                                    KIOSKID,

                                    UPDATED_AT
                                )
                                VALUES
                                (
                                    :USER_ID,
                                    :FROM_DATE,
                                    :TO_DATE,

                                    :TOTAL_RECEIPTS,

                                    :TOTAL_AMT,

                                    :TOTAL5PERCENT,
                                    :TOTAL19PERCENT,
                                    :TOTAL0PERCENT,
                                    :TOTAL3PERCENT,

                                    :INITIAL_AMT,
                                    :FINAL_AMT,

                                    :PAYMENTS,

                                    :CREATED_ON,

                                    :AMOUNT_LAXEIA,
                                    :INITIALAMTLAXEIA,
                                    :AMOUNTVISA,
                                    :FINALAMTLAXEIA,

                                    :DESCRIPTION,

                                    :KIOSKID,

                                    :UPDATED_AT
                                )
                                "

            Try
                Using cmd As New OracleCommand(sql, conn)
                    cmd.BindByName = True
                    cmd.Parameters.Add("USER_ID", OracleDbType.Varchar2).Value = dr("USER_ID")
                    cmd.Parameters.Add("FROM_DATE", OracleDbType.TimeStamp).Value = If(IsDBNull(dr("FROM_DATE")), CType(DBNull.Value, Object), Convert.ToDateTime(dr("FROM_DATE")))
                    cmd.Parameters.Add("TO_DATE", OracleDbType.TimeStamp).Value = Convert.ToDateTime(dr("TO_DATE"))
                    cmd.Parameters.Add("TO_DATE", OracleDbType.TimeStamp).Value = If(IsDBNull(dr("TO_DATE")), CType(DBNull.Value, Object), Convert.ToDateTime(dr("TO_DATE")))
                    cmd.Parameters.Add("KIOSKID", OracleDbType.Varchar2).Value = dr("KIOSKID")

                    For Each c As String In
                    {
                        "TOTAL_RECEIPTS",
                        "TOTAL_AMT",
                        "TOTAL5PERCENT",
                        "TOTAL19PERCENT",
                        "TOTAL0PERCENT",
                        "TOTAL3PERCENT",
                        "INITIAL_AMT",
                        "FINAL_AMT",
                        "PAYMENTS",
                        "AMOUNT_LAXEIA",
                        "INITIALAMTLAXEIA",
                        "AMOUNTVISA",
                        "FINALAMTLAXEIA"
                    }
                        cmd.Parameters.Add(c, OracleDbType.Decimal).Value = If(IsDBNull(dr(c)), 0, dr(c))
                    Next

                    cmd.Parameters.Add("DESCRIPTION", OracleDbType.Varchar2).Value = If(IsDBNull(dr("DESCRIPTION")), "", dr("DESCRIPTION").ToString())
                    cmd.Parameters.Add("CREATED_ON", OracleDbType.TimeStamp).Value = If(IsDBNull(dr("CREATED_ON")), CType(DBNull.Value, Object), Convert.ToDateTime(dr("CREATED_ON")))
                    cmd.Parameters.Add("UPDATED_AT", OracleDbType.TimeStamp).Value = If(IsDBNull(dr("UPDATED_AT")), CType(DBNull.Value, Object), Convert.ToDateTime(dr("UPDATED_AT")))

                    For Each p As OracleParameter In cmd.Parameters
                        CreateExceptionFile(
        $"{p.ParameterName} = {If(p.Value Is Nothing OrElse IsDBNull(p.Value), "NULL", p.Value.ToString())}",
        "")
                    Next
                    cmd.ExecuteNonQuery()
                End Using

                MarkXReportSynced(sqliteConn, dr("USER_ID").ToString(), dr("FROM_DATE").ToString(), dr("TO_DATE").ToString())
            Catch ex As Exception
                CreateExceptionFile(WhoAmI & ": " & ex.Message, sql)
            End Try
        End Sub

        Private Sub MarkXReportSynced(sqliteConn As SQLiteConnection, userId As String, fromDate As String, toDate As String)

            Dim sql As String =
                                "
                                UPDATE X_REPORT
                                SET SYNC_STATUS=0
                                WHERE USER_ID=@USER_ID
                                AND FROM_DATE=@FROM_DATE
                                AND TO_DATE=@TO_DATE
                                AND KIOSKID=@KIOSKID
                                "
            Using cmd As New SQLiteCommand(sql, sqliteConn)
                cmd.Parameters.AddWithValue("@USER_ID", userId)
                cmd.Parameters.AddWithValue("@FROM_DATE", fromDate)
                cmd.Parameters.AddWithValue("@TO_DATE", toDate)
                cmd.Parameters.AddWithValue("@KIOSKID", kioskId)
                cmd.ExecuteNonQuery()
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
                                    UPDATED_AT,
                                    SYNC_STATUS
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
                                    @UPDATED_AT,
                                    0
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

        Public Sub SyncButtonTables()
            Using conn As New SQLiteConnection("Data Source=kiosk.db")
                conn.Open()
                Dim x As Integer = 0
                Try
                    Using trans = conn.BeginTransaction()
                        For i As Integer = 1 To 23
                            x = i
                            SyncButtonTable(conn, trans, i)
                        Next
                        trans.Commit()
                    End Using
                Catch ex As Exception
                End Try
            End Using
        End Sub

        Private Sub SyncButtonTable(sqliteConn As SQLiteConnection, trans As SQLiteTransaction, buttonNo As Integer)
            SyncButtonHeader(sqliteConn, trans, buttonNo)
            SyncButtonDetails(sqliteConn, trans, buttonNo)
        End Sub

        Private Sub SyncButtonHeader(sqliteConn As SQLiteConnection, trans As SQLiteTransaction, buttonNo As Integer)
            Dim sql As String =
                                $"SELECT DISP_NAME,
                                 IS_VISIBLE,
                                 KIOSKID,
                                 UPDATED_AT
                                 FROM BTN_POS{buttonNo}
                                 WHERE KIOSKID=:KIOSKID"

            Using oraCmd As New OracleCommand(sql, conn)

                oraCmd.Parameters.Add("KIOSKID", OracleDbType.Varchar2).Value = kioskId
                Using dr = oraCmd.ExecuteReader()
                    Dim sqliteCmd = CreateButtonHeaderUpsert(sqliteConn, trans, buttonNo)
                    While dr.Read()
                        sqliteCmd.Parameters("@DISP_NAME").Value = If(dr.IsDBNull(0), DBNull.Value, dr("DISP_NAME"))
                        sqliteCmd.Parameters("@IS_VISIBLE").Value = If(dr.IsDBNull(1), DBNull.Value, dr("IS_VISIBLE"))
                        sqliteCmd.Parameters("@KIOSKID").Value = dr("KIOSKID").ToString()
                        sqliteCmd.Parameters("@UPDATED_AT").Value = Convert.ToDateTime(dr("UPDATED_AT")).ToString("yyyy-MM-dd HH:mm:ss")
                        sqliteCmd.ExecuteNonQuery()
                    End While
                End Using
            End Using
        End Sub

        Private Function CreateButtonHeaderUpsert(conn As SQLiteConnection, trans As SQLiteTransaction, buttonNo As Integer) As SQLiteCommand
            Dim sql As String =
                                $"
                                INSERT INTO BTN_POS{buttonNo}
                                (
                                    DISP_NAME,
                                    IS_VISIBLE,
                                    KIOSKID,
                                    UPDATED_AT,
                                    SYNC_STATUS
                                )
                                VALUES
                                (
                                    @DISP_NAME,
                                    @IS_VISIBLE,
                                    @KIOSKID,
                                    @UPDATED_AT,
                                    0
                                )
                                ON CONFLICT(KIOSKID)
                                DO UPDATE SET

                                    DISP_NAME=excluded.DISP_NAME,
                                    IS_VISIBLE=excluded.IS_VISIBLE,
                                    UPDATED_AT=excluded.UPDATED_AT
                                "

            Dim cmd As New SQLiteCommand(sql, conn, trans)

            cmd.Parameters.Add("@DISP_NAME", DbType.String)
            cmd.Parameters.Add("@IS_VISIBLE", DbType.Int32)
            cmd.Parameters.Add("@KIOSKID", DbType.String)
            cmd.Parameters.Add("@UPDATED_AT", DbType.String)
            Return cmd
        End Function

        Private Sub SyncButtonDetails(sqliteConn As SQLiteConnection, trans As SQLiteTransaction, buttonNo As Integer)

            Dim sql As String =
                                $"
                                SELECT
                                    PRODUCT_SERNO,
                                    PRODUCT_UUID,
                                    DISPLAY_DESC,
                                    SEQNO,
                                    KIOSKID,
                                    UPDATED_AT
                                FROM BTN_POS{buttonNo}_DET
                                WHERE KIOSKID=:KIOSKID
                        "

            Using oraCmd As New OracleCommand(sql, conn)

                oraCmd.Parameters.Add("KIOSKID", OracleDbType.Varchar2).Value = kioskId
                Using dr = oraCmd.ExecuteReader()
                    Dim sqliteCmd = CreateButtonDetailUpsert(sqliteConn, trans, buttonNo)

                    While dr.Read()
                        sqliteCmd.Parameters("@PRODUCT_SERNO").Value = If(dr.IsDBNull(0), DBNull.Value, dr("PRODUCT_SERNO"))
                        sqliteCmd.Parameters("@PRODUCT_UUID").Value = dr("PRODUCT_UUID").ToString()
                        sqliteCmd.Parameters("@DISPLAY_DESC").Value = If(dr.IsDBNull(2), DBNull.Value, dr("DISPLAY_DESC"))
                        sqliteCmd.Parameters("@SEQNO").Value = dr("SEQNO")
                        sqliteCmd.Parameters("@KIOSKID").Value = dr("KIOSKID").ToString()
                        sqliteCmd.Parameters("@UPDATED_AT").Value = Convert.ToDateTime(dr("UPDATED_AT")).ToString("yyyy-MM-dd HH:mm:ss")
                        sqliteCmd.ExecuteNonQuery()
                    End While
                End Using
            End Using
        End Sub

        Private Function CreateButtonDetailUpsert(conn As SQLiteConnection, trans As SQLiteTransaction, buttonNo As Integer) As SQLiteCommand

            Dim sql As String =
                                $"
                                INSERT INTO BTN_POS{buttonNo}_DET
                                (
                                    PRODUCT_SERNO,
                                    PRODUCT_UUID,
                                    DISPLAY_DESC,
                                    SEQNO,
                                    KIOSKID,
                                    UPDATED_AT,
                                    SYNC_STATUS
                                )
                                VALUES
                                (
                                    @PRODUCT_SERNO,
                                    @PRODUCT_UUID,
                                    @DISPLAY_DESC,
                                    @SEQNO,
                                    @KIOSKID,
                                    @UPDATED_AT,
                                    0
                                )

                                ON CONFLICT(KIOSKID,PRODUCT_UUID)

                                DO UPDATE SET

                                    PRODUCT_SERNO=excluded.PRODUCT_SERNO,
                                    DISPLAY_DESC=excluded.DISPLAY_DESC,
                                    SEQNO=excluded.SEQNO,
                                    UPDATED_AT=excluded.UPDATED_AT
                        "

            Dim cmd As New SQLiteCommand(sql, conn, trans)

            cmd.Parameters.Add("@PRODUCT_SERNO", DbType.Int64)
            cmd.Parameters.Add("@PRODUCT_UUID", DbType.String)
            cmd.Parameters.Add("@DISPLAY_DESC", DbType.String)
            cmd.Parameters.Add("@SEQNO", DbType.Int32)
            cmd.Parameters.Add("@KIOSKID", DbType.String)
            cmd.Parameters.Add("@UPDATED_AT", DbType.String)
            Return cmd
        End Function

        Public Sub SyncSessions()

            Dim sql As String =
                                "SELECT " &
                                "UUID, " &
                                "LOGIN_WHEN, " &
                                "IS_ACTIVE, " &
                                "MACHINE_NAME, " &
                                "USER_ID, " &
                                "LOGOUT_WHEN, " &
                                "AMOUNTLAXEIAONLOGIN, " &
                                "KIOSKID, " &
                                "UPDATED_AT " &
                                "FROM SESSIONS " &
                                "WHERE KIOSKID = :KIOSKID"

            Try
                isConnOpen()
            Catch ex As Exception
                Exit Sub
            End Try

            Using cmdOracle As New OracleCommand(sql, conn)

                cmdOracle.BindByName = True
                cmdOracle.Parameters.Add("KIOSKID", OracleDbType.Varchar2).Value = kioskId

                Using dr As OracleDataReader = cmdOracle.ExecuteReader()
                    Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                        sqliteConn.Open()

                        Using trans = sqliteConn.BeginTransaction()

                            Dim upsertCmd As SQLiteCommand = CreateSessionUpsertCommand(sqliteConn)
                            upsertCmd.Transaction = trans

                            While dr.Read()
                                upsertCmd.Parameters("@UUID").Value = dr("UUID").ToString()
                                upsertCmd.Parameters("@LOGIN_WHEN").Value = If(dr.IsDBNull(dr.GetOrdinal("LOGIN_WHEN")), DBNull.Value, CType(dr("LOGIN_WHEN"), DateTime).ToString("yyyy-MM-dd HH:mm:ss"))
                                upsertCmd.Parameters("@IS_ACTIVE").Value = If(dr.IsDBNull(dr.GetOrdinal("IS_ACTIVE")), 0, Convert.ToInt32(dr("IS_ACTIVE")))
                                upsertCmd.Parameters("@MACHINE_NAME").Value = If(dr.IsDBNull(dr.GetOrdinal("MACHINE_NAME")), DBNull.Value, dr("MACHINE_NAME").ToString())
                                upsertCmd.Parameters("@USER_ID").Value = If(dr.IsDBNull(dr.GetOrdinal("USER_ID")), DBNull.Value, dr("USER_ID").ToString())
                                upsertCmd.Parameters("@LOGOUT_WHEN").Value = If(dr.IsDBNull(dr.GetOrdinal("LOGOUT_WHEN")), DBNull.Value, CType(dr("LOGOUT_WHEN"), DateTime).ToString("yyyy-MM-dd HH:mm:ss"))
                                upsertCmd.Parameters("@AMOUNTLAXEIAONLOGIN").Value = If(dr.IsDBNull(dr.GetOrdinal("AMOUNTLAXEIAONLOGIN")), 0, Convert.ToDouble(dr("AMOUNTLAXEIAONLOGIN")))
                                upsertCmd.Parameters("@KIOSKID").Value = dr("KIOSKID").ToString()
                                upsertCmd.Parameters("@UPDATED_AT").Value = If(dr.IsDBNull(dr.GetOrdinal("UPDATED_AT")), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), CType(dr("UPDATED_AT"), DateTime).ToString("yyyy-MM-dd HH:mm:ss"))
                                upsertCmd.ExecuteNonQuery()
                            End While

                            trans.Commit()
                        End Using
                    End Using
                End Using
            End Using
        End Sub

        Private Function CreateSessionUpsertCommand(conn As SQLiteConnection) As SQLiteCommand
            Dim sql As String =
                                "
                                INSERT INTO SESSIONS
                                (
                                    UUID,
                                    LOGIN_WHEN,
                                    IS_ACTIVE,
                                    MACHINE_NAME,
                                    USER_ID,
                                    LOGOUT_WHEN,
                                    AMOUNTLAXEIAONLOGIN,
                                    KIOSKID,
                                    UPDATED_AT,
                                    SYNC_STATUS
                                )
                                VALUES
                                (
                                    @UUID,
                                    @LOGIN_WHEN,
                                    @IS_ACTIVE,
                                    @MACHINE_NAME,
                                    @USER_ID,
                                    @LOGOUT_WHEN,
                                    @AMOUNTLAXEIAONLOGIN,
                                    @KIOSKID,
                                    @UPDATED_AT,
                                    0
                                )

                                ON CONFLICT(UUID)
                                DO UPDATE SET

                                LOGIN_WHEN=excluded.LOGIN_WHEN,
                                IS_ACTIVE=excluded.IS_ACTIVE,
                                MACHINE_NAME=excluded.MACHINE_NAME,
                                USER_ID=excluded.USER_ID,
                                LOGOUT_WHEN=excluded.LOGOUT_WHEN,
                                AMOUNTLAXEIAONLOGIN=excluded.AMOUNTLAXEIAONLOGIN,
                                KIOSKID=excluded.KIOSKID,
                                UPDATED_AT=excluded.UPDATED_AT
                                "

            Dim cmd As New SQLiteCommand(sql, conn)

            cmd.Parameters.Add("@UUID", DbType.String)
            cmd.Parameters.Add("@LOGIN_WHEN", DbType.String)
            cmd.Parameters.Add("@IS_ACTIVE", DbType.Int32)
            cmd.Parameters.Add("@MACHINE_NAME", DbType.String)
            cmd.Parameters.Add("@USER_ID", DbType.String)
            cmd.Parameters.Add("@LOGOUT_WHEN", DbType.String)
            cmd.Parameters.Add("@AMOUNTLAXEIAONLOGIN", DbType.Double)
            cmd.Parameters.Add("@KIOSKID", DbType.String)
            cmd.Parameters.Add("@UPDATED_AT", DbType.String)

            Return cmd
        End Function

        Public Sub UploadSessions()
            Dim whoAmI As String = "UploadSessions"
            Try
                Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                    sqliteConn.Open()

                    Const sql As String =
                        "
                        SELECT *
                        FROM SESSIONS
                        WHERE SYNC_STATUS = 1
                        ORDER BY UPDATED_AT
                    "

                    Using cmd As New SQLiteCommand(sql, sqliteConn)
                        Using reader As SQLiteDataReader = cmd.ExecuteReader()
                            While reader.Read()
                                UploadSession(sqliteConn, reader)
                            End While
                        End Using
                    End Using
                End Using
            Catch ex As Exception
                CreateExceptionFile(whoAmI + " " + ex.Message, "")
            End Try
        End Sub

        Private Sub UploadSession(sqliteConn As SQLiteConnection, reader As SQLiteDataReader)

            Const mergeSql As String =
                                        "
                                MERGE INTO SESSIONS s

                                USING
                                (
                                    SELECT
                                        :UUID UUID,
                                        :LOGIN_WHEN LOGIN_WHEN,
                                        :IS_ACTIVE IS_ACTIVE,
                                        :MACHINE_NAME MACHINE_NAME,
                                        :USER_ID USER_ID,
                                        :LOGOUT_WHEN LOGOUT_WHEN,
                                        :AMOUNTLAXEIAONLOGIN AMOUNTLAXEIAONLOGIN,
                                        :KIOSKID KIOSKID,
                                        :UPDATED_AT UPDATED_AT
                                    FROM dual
                                ) x

                                ON
                                (
                                    s.UUID = x.UUID
                                )

                                WHEN MATCHED THEN

                                UPDATE SET

                                    s.LOGIN_WHEN = x.LOGIN_WHEN,
                                    s.IS_ACTIVE = x.IS_ACTIVE,
                                    s.MACHINE_NAME = x.MACHINE_NAME,
                                    s.USER_ID = x.USER_ID,
                                    s.LOGOUT_WHEN = x.LOGOUT_WHEN,
                                    s.AMOUNTLAXEIAONLOGIN = x.AMOUNTLAXEIAONLOGIN,
                                    s.KIOSKID = x.KIOSKID,
                                    s.UPDATED_AT = x.UPDATED_AT

                                WHEN NOT MATCHED THEN

                                INSERT
                                (
                                    UUID,
                                    LOGIN_WHEN,
                                    IS_ACTIVE,
                                    MACHINE_NAME,
                                    USER_ID,
                                    LOGOUT_WHEN,
                                    AMOUNTLAXEIAONLOGIN,
                                    KIOSKID,
                                    UPDATED_AT
                                )

                                VALUES
                                (
                                    x.UUID,
                                    x.LOGIN_WHEN,
                                    x.IS_ACTIVE,
                                    x.MACHINE_NAME,
                                    x.USER_ID,
                                    x.LOGOUT_WHEN,
                                    x.AMOUNTLAXEIAONLOGIN,
                                    x.KIOSKID,
                                    x.UPDATED_AT
                                )
                                "

            Using cmd As New OracleCommand(mergeSql, conn)

                cmd.BindByName = True

                cmd.Parameters.Add("UUID", OracleDbType.Varchar2).Value = reader("UUID").ToString()
                cmd.Parameters.Add("LOGIN_WHEN", OracleDbType.TimeStamp).Value = Convert.ToDateTime(reader("LOGIN_WHEN"))
                cmd.Parameters.Add("IS_ACTIVE", OracleDbType.Int32).Value = Convert.ToInt32(reader("IS_ACTIVE"))
                cmd.Parameters.Add("MACHINE_NAME", OracleDbType.Varchar2).Value = If(IsDBNull(reader("MACHINE_NAME")), DBNull.Value, reader("MACHINE_NAME").ToString())
                cmd.Parameters.Add("USER_ID", OracleDbType.Varchar2).Value = If(IsDBNull(reader("USER_ID")), DBNull.Value, reader("USER_ID").ToString())

                If IsDBNull(reader("LOGOUT_WHEN")) OrElse String.IsNullOrWhiteSpace(reader("LOGOUT_WHEN").ToString()) Then
                    cmd.Parameters.Add("LOGOUT_WHEN", OracleDbType.TimeStamp).Value = DBNull.Value
                Else
                    cmd.Parameters.Add("LOGOUT_WHEN", OracleDbType.TimeStamp).Value = Convert.ToDateTime(reader("LOGOUT_WHEN"))
                End If

                cmd.Parameters.Add("AMOUNTLAXEIAONLOGIN", OracleDbType.Decimal).Value = If(IsDBNull(reader("AMOUNTLAXEIAONLOGIN")), DBNull.Value, Convert.ToDecimal(reader("AMOUNTLAXEIAONLOGIN")))
                cmd.Parameters.Add("KIOSKID", OracleDbType.Varchar2).Value = reader("KIOSKID").ToString()
                cmd.Parameters.Add("UPDATED_AT", OracleDbType.TimeStamp).Value = Convert.ToDateTime(reader("UPDATED_AT"))
                cmd.ExecuteNonQuery()

            End Using

            MarkSessionSynced(sqliteConn, reader("UUID").ToString())
        End Sub

        Private Sub MarkSessionSynced(sqliteConn As SQLiteConnection, uuid As String)

            Const sql As String =
                            "
                                UPDATE SESSIONS
                                SET SYNC_STATUS = 0
                                WHERE UUID = @UUID
                            "

            Using cmd As New SQLiteCommand(sql, sqliteConn)
                cmd.Parameters.AddWithValue("@UUID", uuid)
                cmd.ExecuteNonQuery()
            End Using
        End Sub

        Private Function GetLastSyncValue(syncKey As String) As DateTime

            Dim WhoAmI As String = "GetLastSyncValue"

            Dim sql As String =
        "SELECT VALUE " &
        "FROM SYNC_METADATA " &
        "WHERE KEY=@KEY " &
        "AND KIOSKID=@KIOSKID"

            Try

                Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")

                    sqliteConn.Open()

                    Using cmd As New SQLiteCommand(sql, sqliteConn)

                        cmd.Parameters.AddWithValue("@KEY", syncKey)
                        cmd.Parameters.AddWithValue("@KIOSKID", kioskId)

                        Dim result = cmd.ExecuteScalar()

                        If result IsNot Nothing AndAlso
                   result IsNot DBNull.Value Then

                            Return Convert.ToDateTime(result)

                        End If

                    End Using

                End Using


            Catch ex As Exception

                CreateExceptionFile(
            WhoAmI & ": " & ex.Message,
            sql)

            End Try


            'first sync - get everything
            Return New DateTime(2000, 1, 1)

        End Function
        Public Sub SyncReceipts()
            Dim WhoAmI As String = "SyncReceipts"
            Dim lastSync As String = GetLastSyncValue("RECEIPTS_LAST_SYNC")


            Dim sql As String =
    "
    SELECT
        UUID,
        SERNO,
        PAYMENT_TYPE,
        TOTAL_DISCOUNT,
        TOTAL_VAT19,
        TOTAL_VAT5,
        TOTAL_VAT3,
        TOTAL_VAT0,
        RETURN_AMT,
        TOTAL_AMT_WITH_DISC,
        TOTAL_AMT,
        PAYMENT_AMT,
        CREATED_ON,
        CREATED_BY,
        KIOSKID,
        UPDATED_AT
    FROM RECEIPTS
    WHERE KIOSKID=:KIOSKID
    AND UPDATED_AT > :LASTSYNC
    ORDER BY UPDATED_AT
    "


            Try

                Using oraCmd As New OracleCommand(sql, conn)

                    oraCmd.BindByName = True

                    oraCmd.Parameters.Add("KIOSKID",
                OracleDbType.Varchar2).Value = kioskId


                    oraCmd.Parameters.Add("LASTSYNC",
                OracleDbType.TimeStamp).Value =
                Convert.ToDateTime(lastSync)


                    Using dr = oraCmd.ExecuteReader()


                        Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")

                            sqliteConn.Open()


                            Using tr = sqliteConn.BeginTransaction()


                                Dim upsert =
                            CreateReceiptUpsertCommand(sqliteConn)

                                upsert.Transaction = tr


                                Dim newestSync As DateTime =
                            Convert.ToDateTime(lastSync)


                                While dr.Read()


                                    For i = 0 To dr.FieldCount - 1

                                        upsert.Parameters(
                                    "@" & dr.GetName(i)
                                ).Value =
                                If(IsDBNull(dr(i)),
                                   DBNull.Value,
                                   dr(i))

                                    Next


                                    upsert.ExecuteNonQuery()


                                    Dim upd =
                                Convert.ToDateTime(
                                    dr("UPDATED_AT"))

                                    If upd > newestSync Then
                                        newestSync = upd
                                    End If


                                End While


                                tr.Commit()

                                SaveLastSync(sqliteConn, newestSync, "RECEIPTS")



                            End Using


                        End Using


                    End Using


                End Using


            Catch ex As Exception

                CreateExceptionFile(
        WhoAmI & ": " & ex.Message,
        Sql)

            End Try


        End Sub

        Private Function CreateReceiptUpsertCommand(conn As SQLiteConnection) As SQLiteCommand

            Dim sql As String =
                            "
                            INSERT INTO RECEIPTS
                            (
                            UUID,
                            SERNO,
                            PAYMENT_TYPE,
                            TOTAL_DISCOUNT,
                            TOTAL_VAT19,
                            TOTAL_VAT5,
                            TOTAL_VAT3,
                            TOTAL_VAT0,
                            RETURN_AMT,
                            TOTAL_AMT_WITH_DISC,
                            TOTAL_AMT,
                            PAYMENT_AMT,
                            CREATED_ON,
                            CREATED_BY,
                            KIOSKID,
                            UPDATED_AT,
                            SYNC_STATUS
                            )
                            VALUES
                            (
                            @UUID,
                            @SERNO,
                            @PAYMENT_TYPE,
                            @TOTAL_DISCOUNT,
                            @TOTAL_VAT19,
                            @TOTAL_VAT5,
                            @TOTAL_VAT3,
                            @TOTAL_VAT0,
                            @RETURN_AMT,
                            @TOTAL_AMT_WITH_DISC,
                            @TOTAL_AMT,
                            @PAYMENT_AMT,
                            @CREATED_ON,
                            @CREATED_BY,
                            @KIOSKID,
                            @UPDATED_AT,
                            0
                            )

                            ON CONFLICT(UUID)

                            DO UPDATE SET

                            PAYMENT_TYPE=excluded.PAYMENT_TYPE,
                            TOTAL_AMT=excluded.TOTAL_AMT,
                            TOTAL_AMT_WITH_DISC=excluded.TOTAL_AMT_WITH_DISC,
                            UPDATED_AT=excluded.UPDATED_AT
                            "

            Dim cmd As New SQLiteCommand(sql, conn)
            For Each p In
            {
            "UUID",
            "SERNO",
            "PAYMENT_TYPE",
            "TOTAL_DISCOUNT",
            "TOTAL_VAT19",
            "TOTAL_VAT5",
            "TOTAL_VAT3",
            "TOTAL_VAT0",
            "RETURN_AMT",
            "TOTAL_AMT_WITH_DISC",
            "TOTAL_AMT",
            "PAYMENT_AMT",
            "CREATED_ON",
            "CREATED_BY",
            "KIOSKID",
            "UPDATED_AT"
            }

                cmd.Parameters.Add("@" & p, DbType.Object)
            Next
            Return cmd
        End Function

        Private Function CreateReceiptDetUpsertCommand(sqliteConn As SQLiteConnection) As SQLiteCommand

            Dim sql As String =
                            "
                            INSERT INTO RECEIPTS_DET
                            (
                            UUID,
                            RECEIPT_UUID,
                            PRODUCT_UUID,
                            RECEIPT_SERNO,
                            PRODUCT_SERNO,
                            QUANTITY,
                            CREATED_ON,
                            AMOUNT,
                            VAT,
                            KIOSKID,
                            UPDATED_AT,
                            SYNC_STATUS
                            )
                            VALUES
                            (
                            @UUID,
                            @RECEIPT_UUID,
                            @PRODUCT_UUID,
                            @RECEIPT_SERNO,
                            @PRODUCT_SERNO,
                            @QUANTITY,
                            @CREATED_ON,
                            @AMOUNT,
                            @VAT,
                            @KIOSKID,
                            @UPDATED_AT,
                            0
                            )

                            ON CONFLICT(UUID)

                            DO UPDATE SET

                            QUANTITY=excluded.QUANTITY,
                            AMOUNT=excluded.AMOUNT,
                            VAT=excluded.VAT,
                            UPDATED_AT=excluded.UPDATED_AT
                            "

            Dim cmd As New SQLiteCommand(sql, sqliteConn)
            For Each p In
            {
            "UUID",
            "RECEIPT_UUID",
            "PRODUCT_UUID",
            "RECEIPT_SERNO",
            "PRODUCT_SERNO",
            "QUANTITY",
            "CREATED_ON",
            "AMOUNT",
            "VAT",
            "KIOSKID",
            "UPDATED_AT"
            }

                cmd.Parameters.Add("@" & p, DbType.Object)
            Next
            Return cmd
        End Function

        Public Sub SyncReceiptsDet()

            Dim WhoAmI As String = "SyncReceiptsDet"

            Dim oracleRows As Integer = 0
            Dim sqliteRows As Integer = 0
            Dim failedRows As Integer = 0
            Dim noAffectedRows As Integer = 0

            Try
                Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                    sqliteConn.Open()
                    Dim sql As String =
                                        "
                                        SELECT
                                        UUID,
                                        RECEIPT_SERNO,
                                        PRODUCT_SERNO,
                                        RECEIPT_UUID,
                                        PRODUCT_UUID,
                                        QUANTITY,
                                        CREATED_ON,
                                        AMOUNT,
                                        VAT,
                                        KIOSKID,
                                        UPDATED_AT
                                        FROM RECEIPTS_DET
                                        WHERE KIOSKID=:KIOSKID
                                        "

                    Using oraCmd As New OracleCommand(sql, conn)
                        oraCmd.BindByName = True
                        oraCmd.Parameters.Add("KIOSKID", OracleDbType.Varchar2).Value = kioskId

                        Using dr As OracleDataReader = oraCmd.ExecuteReader()
                            Using tr = sqliteConn.BeginTransaction()
                                Dim insertCmd = CreateReceiptDetUpsertCommand(sqliteConn)
                                insertCmd.Transaction = tr

                                While dr.Read()
                                    oracleRows += 1
                                    Try
                                        For i As Integer = 0 To dr.FieldCount - 1
                                            insertCmd.Parameters("@" & dr.GetName(i)).Value = If(IsDBNull(dr(i)), DBNull.Value, dr(i))
                                        Next

                                        Dim affected As Integer = insertCmd.ExecuteNonQuery()

                                        If affected > 0 Then
                                            sqliteRows += 1
                                        Else
                                            noAffectedRows += 1

                                            CreateExceptionFile(
                                                                "NO ROW AFFECTED RECEIPTS_DET" &
                                                                vbCrLf &
                                                                "Oracle row=" & oracleRows &
                                                                vbCrLf &
                                                                "Receipt UUID=" &
                                                                dr("RECEIPT_UUID").ToString() &
                                                                vbCrLf &
                                                                "Product UUID=" &
                                                                dr("PRODUCT_UUID").ToString() &
                                                                vbCrLf &
                                                                "Created=" &
                                                                dr("CREATED_ON").ToString(),
                                                                "")
                                        End If

                                    Catch rowEx As Exception
                                        failedRows += 1
                                        CreateExceptionFile(
                                                            "FAILED RECEIPTS_DET" &
                                                            vbCrLf &
                                                            "Oracle row=" & oracleRows &
                                                            vbCrLf &
                                                            "Receipt serno=" &
                                                            dr("RECEIPT_SERNO").ToString() &
                                                            vbCrLf &
                                                            "Product serno=" &
                                                            dr("PRODUCT_SERNO").ToString() &
                                                            vbCrLf &
                                                            "Receipt UUID=" &
                                                            dr("RECEIPT_UUID").ToString() &
                                                            vbCrLf &
                                                            "Product UUID=" &
                                                            dr("PRODUCT_UUID").ToString() &
                                                            vbCrLf &
                                                            rowEx.Message,
                                                            "")

                                    End Try


                                    'If oracleRows Mod 10000 = 0 Then
                                    'Debug.WriteLine(
                                    '                   "Oracle=" & oracleRows &
                                    '                   " SQLite=" & sqliteRows &
                                    '                   " Failed=" & failedRows &
                                    '                   " NoAffected=" & noAffectedRows)
                                    '
                                    'End If
                                End While
                                tr.Commit()
                            End Using
                        End Using
                    End Using
                End Using
            Catch ex As Exception
                CreateExceptionFile(WhoAmI & ": " & ex.Message, "")
            End Try
        End Sub

        Public Sub UploadReceiptsDet()
            Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                sqliteConn.Open()

                Dim sql As String =
                                    "
                                    SELECT *
                                    FROM RECEIPTS_DET
                                    WHERE SYNC_STATUS=1
                                    AND KIOSKID=@KIOSKID
                                    "

                Using cmd As New SQLiteCommand(sql, sqliteConn)
                    cmd.Parameters.AddWithValue("@KIOSKID", kioskId)
                    Using dr = cmd.ExecuteReader()

                        While dr.Read()
                            UploadReceiptDet(sqliteConn, dr)
                        End While
                    End Using
                End Using
            End Using
        End Sub

        Private Sub UploadReceiptDet(sqliteConn As SQLiteConnection, dr As SQLiteDataReader)

            Dim sql As String =
                                "
                                INSERT INTO RECEIPTS_DET
                                (
                                UUID,
                                RECEIPT_SERNO,
                                PRODUCT_SERNO,
                                QUANTITY,
                                CREATED_ON,
                                AMOUNT,
                                VAT,
                                RECEIPT_UUID,
                                PRODUCT_UUID,
                                KIOSKID,
                                UPDATED_AT
                                )
                                VALUES
                                (
                                :UUID,
                                :RECEIPT_SERNO,
                                :PRODUCT_SERNO,
                                :QUANTITY,
                                :CREATED_ON,
                                :AMOUNT,
                                :VAT,
                                :RECEIPT_UUID,
                                :PRODUCT_UUID,
                                :KIOSKID,
                                :UPDATED_AT
                                )
                                "


            Using cmd As New OracleCommand(sql, conn)
                cmd.BindByName = True

                For Each c In
                {
                    "UUID"
                }
                    cmd.Parameters.Add(c, OracleDbType.Varchar2).Value = dr(c)
                Next

                cmd.Parameters.Add("RECEIPT_SERNO", OracleDbType.Int32).Value = dr("RECEIPT_SERNO")
                cmd.Parameters.Add("PRODUCT_SERNO", OracleDbType.Int32).Value = dr("PRODUCT_SERNO")
                cmd.Parameters.Add("QUANTITY", OracleDbType.Int32).Value = dr("QUANTITY")
                cmd.Parameters.Add("CREATED_ON", OracleDbType.TimeStamp).Value = Convert.ToDateTime(dr("CREATED_ON"))
                cmd.Parameters.Add("AMOUNT", OracleDbType.Decimal).Value = dr("AMOUNT")
                cmd.Parameters.Add("VAT", OracleDbType.Int32).Value = dr("VAT")
                cmd.Parameters.Add("UPDATED_AT", OracleDbType.TimeStamp).Value = Convert.ToDateTime(dr("UPDATED_AT"))

                cmd.ExecuteNonQuery()
            End Using

            MarkReceiptDetSynced(sqliteConn, dr("RECEIPT_UUID").ToString(), dr("PRODUCT_UUID").ToString())
        End Sub

        Private Sub MarkReceiptDetSynced(sqliteConn As SQLiteConnection, receiptUUID As String, productUUID As String)

            Dim sql As String =
                                "
                                UPDATE RECEIPTS_DET
                                SET SYNC_STATUS=0
                                WHERE RECEIPT_UUID=@R
                                AND PRODUCT_UUID=@P
                                "

            Using cmd As New SQLiteCommand(sql, sqliteConn)
                cmd.Parameters.AddWithValue("@R", receiptUUID)
                cmd.Parameters.AddWithValue("@P", productUUID)
                cmd.ExecuteNonQuery()
            End Using
        End Sub

        Public Sub SyncPayments()

            Dim sql As String =
                                "
                                SELECT
                                    CREATED_BY,
                                    CREATED_ON,
                                    AMOUNT,
                                    VAT,
                                    AMOUNTVAT,
                                    INV_NUMBER,
                                    SUPPLIER_ID,
                                    KIOSKID,
                                    UPDATED_AT
                                FROM PAYMENTS
                                WHERE KIOSKID=:KIOSKID
                                "

            Using cmd As New OracleCommand(sql, conn)
                cmd.BindByName = True
                cmd.Parameters.Add("KIOSKID", OracleDbType.Varchar2).Value = kioskId

                Using dr = cmd.ExecuteReader()
                    Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                        sqliteConn.Open()

                        While dr.Read()
                            Dim sqlLite =
                                         "
                                        INSERT INTO PAYMENTS
                                        (
                                        CREATED_BY,
                                        CREATED_ON,
                                        AMOUNT,
                                        VAT,
                                        AMOUNTVAT,
                                        INV_NUMBER,
                                        SUPPLIER_ID,
                                        KIOSKID,
                                        UPDATED_AT,
                                        SYNC_STATUS
                                        )
                                        VALUES
                                        (
                                        @CREATED_BY,
                                        @CREATED_ON,
                                        @AMOUNT,
                                        @VAT,
                                        @AMOUNTVAT,
                                        @INV_NUMBER,
                                        @SUPPLIER_ID,
                                        @KIOSKID,
                                        @UPDATED_AT,
                                        0
                                        )

                                        ON CONFLICT
                                        (
                                        CREATED_BY,
                                        CREATED_ON,
                                        INV_NUMBER,
                                        SUPPLIER_ID,
                                        KIOSKID
                                        )

                                        DO UPDATE SET

                                        AMOUNT=excluded.AMOUNT,
                                        VAT=excluded.VAT,
                                        AMOUNTVAT=excluded.AMOUNTVAT,
                                        UPDATED_AT=excluded.UPDATED_AT
                                    "

                            Using ins As New SQLiteCommand(sqlLite, sqliteConn)
                                ins.Parameters.AddWithValue("@CREATED_BY", dr("CREATED_BY"))
                                ins.Parameters.AddWithValue("@CREATED_ON", CDate(dr("CREATED_ON")).ToString("yyyy-MM-dd HH:mm:ss"))
                                ins.Parameters.AddWithValue("@AMOUNT", dr("AMOUNT"))
                                ins.Parameters.AddWithValue("@VAT", dr("VAT"))
                                ins.Parameters.AddWithValue("@AMOUNTVAT", dr("AMOUNTVAT"))
                                ins.Parameters.AddWithValue("@INV_NUMBER", dr("INV_NUMBER"))
                                ins.Parameters.AddWithValue("@SUPPLIER_ID", dr("SUPPLIER_ID"))
                                ins.Parameters.AddWithValue("@KIOSKID", dr("KIOSKID"))
                                ins.Parameters.AddWithValue("@UPDATED_AT", dr("UPDATED_AT"))
                                ins.ExecuteNonQuery()
                            End Using
                        End While
                    End Using
                End Using
            End Using
        End Sub

        Public Sub UploadPayments()
            Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                sqliteConn.Open()

                Dim sql As String =
                                    "SELECT *
                                     FROM PAYMENTS
                                     WHERE SYNC_STATUS=1"

                Using cmd As New SQLiteCommand(sql, sqliteConn)
                    Using dr = cmd.ExecuteReader()
                        While dr.Read()
                            UploadPayment(sqliteConn, dr)
                        End While
                    End Using
                End Using
            End Using
        End Sub

        Private Sub UploadPayment(sqliteConn As SQLiteConnection, dr As SQLiteDataReader)
            Dim sql As String =
                                "
                                INSERT INTO PAYMENTS
                                (
                                CREATED_BY,
                                CREATED_ON,
                                AMOUNT,
                                VAT,
                                AMOUNTVAT,
                                INV_NUMBER,
                                SUPPLIER_ID,
                                KIOSKID,
                                UPDATED_AT
                                )
                                VALUES
                                (
                                :CREATED_BY,
                                :CREATED_ON,
                                :AMOUNT,
                                :VAT,
                                :AMOUNTVAT,
                                :INV_NUMBER,
                                :SUPPLIER_ID,
                                :KIOSKID,
                                :UPDATED_AT
                                )
                        "
            Using cmd As New OracleCommand(sql, conn)
                cmd.BindByName = True

                cmd.Parameters.Add("CREATED_BY", OracleDbType.Varchar2).Value = dr("CREATED_BY")
                cmd.Parameters.Add("CREATED_ON", OracleDbType.TimeStamp).Value = Convert.ToDateTime(dr("CREATED_ON"))
                cmd.Parameters.Add("AMOUNT", OracleDbType.Decimal).Value = Convert.ToDecimal(dr("AMOUNT"))
                cmd.Parameters.Add("VAT", OracleDbType.Varchar2).Value = dr("VAT")
                cmd.Parameters.Add("AMOUNTVAT", OracleDbType.Decimal).Value = Convert.ToDecimal(dr("AMOUNTVAT"))
                cmd.Parameters.Add("INV_NUMBER", OracleDbType.Varchar2).Value = dr("INV_NUMBER")
                cmd.Parameters.Add("SUPPLIER_ID", OracleDbType.Varchar2).Value = dr("SUPPLIER_ID")
                cmd.Parameters.Add("KIOSKID", OracleDbType.Varchar2).Value = dr("KIOSKID")
                cmd.Parameters.Add("UPDATED_AT", OracleDbType.TimeStamp).Value = Convert.ToDateTime(dr("UPDATED_AT"))
                cmd.ExecuteNonQuery()
            End Using
            MarkPaymentSynced(sqliteConn, dr("CREATED_BY").ToString(), dr("CREATED_ON").ToString())
        End Sub

        Private Sub MarkPaymentSynced(sqliteConn As SQLiteConnection, createdBy As String, createdOn As String)
            Dim sql As String =
                                "
                                UPDATE PAYMENTS
                                SET SYNC_STATUS=0
                                WHERE CREATED_BY=@CREATED_BY
                                AND CREATED_ON=@CREATED_ON
                                "

            Using cmd As New SQLiteCommand(sql, sqliteConn)
                cmd.Parameters.AddWithValue("@CREATED_BY", createdBy)
                cmd.Parameters.AddWithValue("@CREATED_ON", createdOn)
                cmd.ExecuteNonQuery()
            End Using
        End Sub

        Public Sub UploadReceipts()
            Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                sqliteConn.Open()

                Using cmd As New SQLiteCommand("SELECT * FROM RECEIPTS WHERE SYNC_STATUS=1", sqliteConn)
                    Using dr = cmd.ExecuteReader()
                        While dr.Read()
                            UploadReceipt(sqliteConn, dr)
                        End While
                    End Using
                End Using
            End Using
        End Sub

        Private Sub UploadReceipt(sqliteConn As SQLiteConnection, dr As SQLiteDataReader)

            Dim sql As String =
                                "
                                MERGE INTO RECEIPTS r

                                USING
                                (
                                SELECT
                                :UUID UUID
                                FROM dual
                                )x

                                ON
                                (
                                r.UUID=x.UUID
                                )

                                WHEN NOT MATCHED THEN

                                INSERT
                                (
                                UUID,
                                SERNO,
                                PAYMENT_TYPE,
                                TOTAL_DISCOUNT,
                                TOTAL_VAT19,
                                TOTAL_VAT5,
                                TOTAL_VAT3,
                                TOTAL_VAT0,
                                RETURN_AMT,
                                TOTAL_AMT_WITH_DISC,
                                TOTAL_AMT,
                                PAYMENT_AMT,
                                CREATED_ON,
                                CREATED_BY,
                                KIOSKID,
                                UPDATED_AT
                                )

                                VALUES
                                (
                                :UUID,
                                :SERNO,
                                :PAYMENT_TYPE,
                                :TOTAL_DISCOUNT,
                                :TOTAL_VAT19,
                                :TOTAL_VAT5,
                                :TOTAL_VAT3,
                                :TOTAL_VAT0,
                                :RETURN_AMT,
                                :TOTAL_AMT_WITH_DISC,
                                :TOTAL_AMT,
                                :PAYMENT_AMT,
                                :CREATED_ON,
                                :CREATED_BY,
                                :KIOSKID,
                                :UPDATED_AT
                                )
                                "

            Using cmd As New OracleCommand(sql, conn)
                cmd.BindByName = True

                For Each col As String In
                {
                "UUID",
                "PAYMENT_TYPE",
                "CREATED_BY",
                "KIOSKID"
                }
                    cmd.Parameters.Add(col, OracleDbType.Varchar2).Value = dr(col)
                Next

                cmd.Parameters.Add("SERNO", OracleDbType.Int32).Value = dr("SERNO")

                For Each col As String In
                {
                "TOTAL_DISCOUNT",
                "TOTAL_VAT19",
                "TOTAL_VAT5",
                "TOTAL_VAT3",
                "TOTAL_VAT0",
                "RETURN_AMT",
                "TOTAL_AMT_WITH_DISC",
                "TOTAL_AMT",
                "PAYMENT_AMT"
                }

                    cmd.Parameters.Add(col, OracleDbType.Decimal).Value = dr(col)
                Next

                cmd.Parameters.Add("CREATED_ON", OracleDbType.TimeStamp).Value = Convert.ToDateTime(dr("CREATED_ON"))
                cmd.Parameters.Add("UPDATED_AT", OracleDbType.TimeStamp).Value = Convert.ToDateTime(dr("UPDATED_AT"))
                cmd.ExecuteNonQuery()
            End Using
            MarkReceiptSynced(sqliteConn, dr("UUID").ToString())
        End Sub

        Private Sub MarkReceiptSynced(sqliteConn As SQLiteConnection, uuid As String)
            Using cmd As New SQLiteCommand("UPDATE RECEIPTS SET SYNC_STATUS=0 WHERE UUID=@UUID", sqliteConn)
                cmd.Parameters.AddWithValue("@UUID", uuid)
                cmd.ExecuteNonQuery()
            End Using
        End Sub
    End Class
End Module
