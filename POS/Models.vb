Imports System.Collections.Generic

'========================
' Ping
'========================
Public Class PingResponse
    Public Property port As Integer
    Public Property [date] As String
End Class

'========================
' Status
'========================
Public Class StatusResponse
    Public Property status As Integer
    Public Property statusTxt As String
    Public Property ping As Boolean
    Public Property transaction As String
    Public Property occupied As Boolean
End Class

'========================
' Transaction Request
'========================
Public Class TransactionRequest
    Public Property seqNo As String
    Public Property amount As Double
    Public Property async As Boolean
    Public Property comments As String
    Public Property rate As Double?
    Public Property occupy As Integer?
End Class

'========================
' Transaction Log
'========================
Public Class TransactionLog
    Public Property seqNo As String
    Public Property user As String
    Public Property amount As Double
    Public Property deposit As Double
    Public Property [date] As String
    Public Property status As Integer
    Public Property statusTxt As String
    Public Property inTotal As Double
    Public Property outTotal As Double
    Public Property comments As String
End Class

'========================
' Collect Request
'========================

Public Class CollectRequest
    Public Property [option] As Integer
    Public Property verification As Integer
    Public Property cassette As Integer
    Public Property mix As Integer
    Public Property [partial] As Integer
    Public Property cash As Integer
    Public Property denominations As Dictionary(Of String, Integer)
End Class

'========================
' Inventory
'========================
Public Class Inventory
    Public Property currencies As List(Of String)
    Public Property note As Device
    Public Property coin As Device
End Class

Public Class InventoryResponse
    Public Property currencies As List(Of String)
    Public Property note As DeviceMoney
    Public Property coin As DeviceMoney
End Class

Public Class DeviceMoney
    Public Property denominations As List(Of Denomination)
End Class

Public Class Denomination
    Public Property key As String
    Public Property total As Integer
    Public Property title As String
    Public Property dispensable_amount As Decimal
End Class


Public Class Device
    Public Property devid As Integer
    Public Property type As String
    Public Property exists As Boolean
    Public Property denominations As List(Of Denomination)
End Class

