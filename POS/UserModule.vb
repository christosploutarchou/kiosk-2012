Imports System.Data.SQLite
Imports Oracle.DataAccess.Client

Module UserModule

    Public Class User
        Public Function GetUsernames() As List(Of String)
            Dim WhoAmI As String = "GetUsernames"
            Dim result As New List(Of String)
            Dim sql As String =
                                "
                                SELECT USERNAME
                                FROM USERS
                                WHERE DELETED = 0
                                AND KIOSKID = @KIOSKID
                                ORDER BY USERNAME
                                "

            Try
                Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                    sqliteConn.Open()
                    Using cmd As New SQLiteCommand(sql, sqliteConn)
                        cmd.Parameters.AddWithValue("@KIOSKID", kioskId)
                        Using dr As SQLiteDataReader = cmd.ExecuteReader()

                            While dr.Read()
                                result.Add(dr("USERNAME").ToString())
                            End While
                        End Using
                    End Using
                End Using
            Catch ex As Exception
                CreateExceptionFile(WhoAmI & " " & ex.Message, sql)
                MessageBox.Show(ex.Message, APPLICATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
            Return result
        End Function






    End Class

End Module
