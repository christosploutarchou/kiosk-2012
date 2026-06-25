Imports System.Data.SQLite
Imports Oracle.DataAccess.Client
Imports POS.SupplierModule

Module SupplierModule
    Public Class Supplier

        Public Function GetSuppliers() As ArrayList
            Dim WhoAmI As String = "Supplier.GetSuppliers"
            Dim result As New ArrayList
            Dim sql As String =
                                "
                            SELECT
                                *
                            FROM SUPPLIERS
                            WHERE KIOSKID=@KIOSKID
                            ORDER BY S_NAME
                            "
            Try
                Using sqliteConn As New SQLiteConnection("Data Source=kiosk.db")
                    sqliteConn.Open()
                    Using cmd As New SQLiteCommand(sql, sqliteConn)
                        cmd.Parameters.AddWithValue("@KIOSKID", kioskId)
                        Using dr As SQLiteDataReader = cmd.ExecuteReader()

                            While dr.Read()
                                Dim supplier As New Hashtable
                                For i As Integer = 0 To dr.FieldCount - 1
                                    supplier.Add(
                                        dr.GetName(i).ToLower(),
                                        If(IsDBNull(dr(i)),
                                           "",
                                          dr(i)))
                                Next
                                result.Add(supplier)
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
