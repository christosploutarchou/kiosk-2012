Imports System.Data.SQLite
Imports System.Drawing.Printing
Imports Oracle.DataAccess.Client

Module SessionModule
    Public Class Session
        Public Sub LogoutUser(ByVal username As String)
            Dim WhoAmI As String = "Session.LogoutUser"
            Dim sql As String = ""
            Try
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
                    CreateExceptionFile(WhoAmI + " " + ex.ToString(), "")
                End Try
            Catch ex As Exception
                CreateExceptionFile(WhoAmI + " " + ex.ToString(), sql)
                MessageBox.Show(WhoAmI + " " + ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Public Sub LogoutUserByUserID(ByVal uuid As String)
            Dim WhoAmI As String = "Session.LogoutUserByUserID"
            Dim sql As String = ""

            Try
                Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                    sqliteConn.Open()
                    sql =
                            "UPDATE SESSIONS " &
                            "SET IS_ACTIVE = 0, " &
                            "    LOGOUT_WHEN = CURRENT_TIMESTAMP, " &
                            "    UPDATED_AT = CURRENT_TIMESTAMP, " &
                            "    SYNC_STATUS = 1 " &
                            "WHERE USER_ID = @USER_ID " &
                            "AND IS_ACTIVE = 1 " &
                            "AND KIOSKID = @KIOSKID"

                    Using cmd As New SQLiteCommand(sql, sqliteConn)
                        cmd.Parameters.AddWithValue("@USER_ID", uuid)
                        cmd.Parameters.AddWithValue("@KIOSKID", kioskId)
                        cmd.ExecuteNonQuery()
                    End Using
                End Using

                Try
                    Dim sync As New SyncTables()
                    sync.UploadSessions()
                Catch ex As Exception
                    CreateExceptionFile(WhoAmI + " " + ex.Message, sql)
                End Try
            Catch ex As Exception
                CreateExceptionFile(WhoAmI + " " + ex.Message, sql)
                MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Public Function CheckIfUserAlreadyLoggedIn(username As String) As Boolean
            Dim WhoAmI As String = "Session.CheckIfUserAlreadyLoggedIn"

            Dim sql As String = "SELECT COUNT(*) FROM sessions s 
                                WHERE KIOSKID = :KIOSKID AND 
                                s.is_active = 1 AND s.user_id = (
                                SELECT 
                                    u.uuid 
                                FROM users u 
                                WHERE  KIOSKID = :KIOSKID AND u.username = :username) "

            Try
                Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                    sqliteConn.Open()
                    Using cmd As New SQLiteCommand(sql, sqliteConn)
                        cmd.Parameters.Add("KIOSKID", OracleDbType.Varchar2).Value = kioskId
                        cmd.Parameters.Add("username", OracleDbType.Varchar2).Value = username
                        Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                        Return (count > 0)
                    End Using
                End Using
            Catch ex As Exception
                CreateExceptionFile(WhoAmI + " " + ex.ToString(), " " + sql)
                MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End Try
        End Function

        Public Sub LogoutUserByUUID(ByVal uuid As String)
            Dim WhoAmI As String = "Session.LogoutUserByUUID"
            Dim sql As String = ""

            Try
                sql =
                    "UPDATE SESSIONS
                     SET IS_ACTIVE = 0,
                         LOGOUT_WHEN = CURRENT_TIMESTAMP,
                         SYNC_STATUS = 1
                     WHERE USER_ID = @USER_ID
                     AND KIOSKID = @KIOSKID
                     AND IS_ACTIVE = 1"

                Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                    sqliteConn.Open()
                    Using cmd As New SQLiteCommand(sql, sqliteConn)
                        cmd.Parameters.AddWithValue("@USER_ID", uuid)
                        cmd.Parameters.AddWithValue("@KIOSKID", kioskId)
                        cmd.ExecuteNonQuery()
                    End Using
                End Using

                Try
                    Dim sync As New SyncTables()
                    sync.SyncSessions()
                Catch ex As Exception
                End Try

            Catch ex As Exception
                CreateExceptionFile(WhoAmI & ": " & ex.Message, sql)
                MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

    End Class
End Module
