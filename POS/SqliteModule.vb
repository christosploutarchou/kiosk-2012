Imports System.Data.SQLite
Imports System.Web.UI.WebControls
Imports Microsoft.VisualBasic.ApplicationServices
Imports Oracle.DataAccess.Client

Module SqliteModule

    Public SqlLite As Boolean = False

    Public Function SqlLiteEnabled() As Boolean
        Const FILE_NAME As String = "C:\sqlite.txt"

        If Not IO.File.Exists(FILE_NAME) Then
            Return False
        Else
            Return True
        End If
    End Function

    Public Sub CreateSqliteDB()
        Dim WhoAmI As String = "CreateSqliteDB"
        Try
            Using conn As New SQLiteConnection("Data Source=kiosk.db")
                conn.Open()
                Dim sql As String = "
                    -- KIOSK
                    CREATE TABLE IF NOT EXISTS KIOSK (
                                                        KIOSKID TEXT PRIMARY KEY,
                                                        DESCRIPTION TEXT
                                                    );

                    -- GLOBAL_PARAMS
                    CREATE TABLE IF NOT EXISTS GLOBAL_PARAMS (
                        PARAMKEY TEXT PRIMARY KEY,
                        PARAMVALUE TEXT,
                        KIOSKID TEXT,
                        UPDATED_AT TEXT,
                        FOREIGN KEY (KIOSKID)
                            REFERENCES KIOSK(KIOSKID)
                    );

                    -- SYNC_METADATA
                    CREATE TABLE IF NOT EXISTS SYNC_METADATA (
                        KEY TEXT PRIMARY KEY,
                        VALUE TEXT
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
        Public Sub SyncGlobalParams()
            Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                sqliteConn.Open()
                EnableForeignKeys(sqliteConn)
                Dim lastSync As DateTime = GetLastSync(sqliteConn)

                Dim sql As String = "
                                    SELECT
                                        PARAMKEY,
                                        PARAMVALUE,
                                        KIOSKID,
                                        UPDATED_AT
                                    FROM GLOBAL_PARAMS
                                    WHERE UPDATED_AT > :LASTSYNC
                                    ORDER BY UPDATED_AT
                                    "

                Using cmd As New OracleCommand(sql, conn)

                    cmd.Parameters.Add("LASTSYNC", OracleDbType.TimeStamp).Value = lastSync
                    Using reader = cmd.ExecuteReader()
                        Dim newestTimestamp As DateTime = lastSync

                        While reader.Read()
                            Dim updatedAt As DateTime = Convert.ToDateTime(reader("UPDATED_AT"))

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

                        SaveLastSync(sqliteConn, newestTimestamp)
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

        Private Function GetLastSync(conn As SQLiteConnection) As DateTime

            Dim sql As String = "
                            SELECT VALUE
                            FROM SYNC_METADATA
                            WHERE KEY='GLOBAL_PARAMS_LAST_SYNC'
                            "

            Using cmd As New SQLiteCommand(sql, conn)

                Dim result = cmd.ExecuteScalar()

                If result Is Nothing _
                OrElse result Is DBNull.Value Then
                    Return New DateTime(2000, 1, 1)
                End If

                Return DateTime.Parse(result.ToString())
            End Using
        End Function

        Private Sub SaveLastSync(conn As SQLiteConnection, syncDate As DateTime)

            Dim sql As String = "
                            INSERT INTO SYNC_METADATA
                            (
                                KEY,
                                VALUE
                            )
                            VALUES
                            (
                                'GLOBAL_PARAMS_LAST_SYNC',
                                @VALUE
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
