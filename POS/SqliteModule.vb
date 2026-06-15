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

    Public Sub CreateSqliteTableStructure()
        Dim WhoAmI As String = "CreateSqliteDB"
        Try
            Using conn As New SQLiteConnection("Data Source=kiosk.db")
                conn.Open()
                Dim sql As String = "
                    -- SYNC_METADATA
                    CREATE TABLE IF NOT EXISTS SYNC_METADATA (
                        KEY TEXT PRIMARY KEY,
                        KIOSKID TEXT,
                        VALUE TEXT
                    );

                    -- GLOBAL_PARAMS
                    CREATE TABLE IF NOT EXISTS GLOBAL_PARAMS (
                        PARAMKEY TEXT PRIMARY KEY,
                        PARAMVALUE TEXT,
                        KIOSKID TEXT,
                        UPDATED_AT TEXT,
                        FOREIGN KEY (KIOSKID) REFERENCES KIOSK(KIOSKID)
                    );"

                Using cmd As New SQLiteCommand(sql, conn)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            CreateExceptionFile(WhoAmI + ": " + ex.Message, " ")
            MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Public Class SyncTables
        'GLOBAL_PARAMS
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

        Private Sub UpsertGlobalParam(conn As SQLiteConnection, paramKey As String, paramValue As String, kioskId As String, updatedAt As DateTime)
            Dim sql As String = "
                                INSERT INTO GLOBAL_PARAMS
                                (
                                    PARAMKEY,
                                    PARAMVALUE,
                                    KIOSKID,
                                    UPDATED_AT
                                )
                                VALUES
                                (
                                    @PARAMKEY,
                                    @PARAMVALUE,
                                    @KIOSKID,
                                    @UPDATED_AT
                                )
                                ON CONFLICT(PARAMKEY)
                                DO UPDATE SET
                                    PARAMVALUE = excluded.PARAMVALUE,
                                    KIOSKID = excluded.KIOSKID,
                                    UPDATED_AT = excluded.UPDATED_AT
                                "

            Using cmd As New SQLiteCommand(sql, conn)
                cmd.Parameters.AddWithValue("@PARAMKEY", paramKey)
                cmd.Parameters.AddWithValue("@PARAMVALUE", If(paramValue, DBNull.Value))
                cmd.Parameters.AddWithValue("@KIOSKID", If(kioskId, DBNull.Value))
                cmd.Parameters.AddWithValue("@UPDATED_AT", updatedAt.ToString("yyyy-MM-dd HH:mm:ss"))
                cmd.ExecuteNonQuery()
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

    End Class
End Module
