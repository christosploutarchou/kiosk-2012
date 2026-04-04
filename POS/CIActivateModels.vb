Imports System
Imports System.Collections.Generic

Namespace CIActivateModels

    ' --- Ping response ---
    Public Class PingResponse
        Public Property port As Integer
        Public Property [date] As DateTime
    End Class

    Public Class LogResponse
        Public Property seqNo As String
        Public Property user As String
        Public Property amount As Double
        Public Property deposit As Double
        Public Property dateTime As DateTime
        Public Property status As Integer
        Public Property statusTxt As String
        Public Property inTotal As Double
        Public Property outTotal As Double
        Public Property comments As String
    End Class

    Public Class LoginResponse
        Public Property message As String
    End Class

    Public Class StatusResponse
        Public Property status As Integer
        Public Property statusTxt As String
        Public Property ping As Boolean
        Public Property transaction As String
        Public Property occupied As Boolean
    End Class
    ' --- Transaction request ---
    Public Class CIActivateTransactionRequest
        Public Property seqNo As String
        Public Property amount As Double
        Public Property async As Boolean = False
        Public Property comments As String
        Public Property rate As Double?
        Public Property occupy As Integer?
    End Class

    ' --- Transaction log ---
    Public Class CIActivateTransactionLog
        Public Property seqNo As String
        Public Property user As String
        Public Property amount As Double
        Public Property deposit As Double
        Public Property [date] As DateTime
        Public Property status As Integer
        Public Property statusTxt As String
        Public Property inTotal As Double
        Public Property outTotal As Double
        Public Property comments As String
    End Class

    ' --- Inventory ---
    Public Class Inventory
        Public Property currencies As List(Of String)
        Public Property note As DeviceInventory
        Public Property coin As DeviceInventory
    End Class

    Public Class DeviceInventory
        Public Property devid As Integer
        Public Property type As String
        Public Property exists As Boolean
    End Class

    ' --- History ---
    Public Class History
        Public Property type As String
        Public Property row As Integer
        Public Property id As String
        Public Property seqNo As String
        Public Property user As String
        Public Property [date] As DateTime
        Public Property statusCode As Integer
        Public Property status As String
        Public Property detection As Double
        Public Property amount As Double
        Public Property deposit As Double
        Public Property cashIn As Double
        Public Property cashOut As Double
    End Class

    ' --- Collect request ---
    Public Class CollectRequest
        Public Property [option] As Integer
        Public Property verification As Integer
        Public Property cassette As Integer
        Public Property mix As Integer
        Public Property [partial] As Integer
        Public Property cash As Integer
        Public Property denominations As Dictionary(Of String, Integer)
    End Class

End Namespace