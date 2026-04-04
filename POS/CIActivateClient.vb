Imports System
Imports System.Net
Imports System.Net.Http
Imports System.Net.Http.Headers
Imports System.Text
Imports System.Threading.Tasks
Imports Newtonsoft.Json
Imports POS.CIActivateModels


Public Class CIActivateClient
    Private ReadOnly client As HttpClient
    Private ReadOnly handler As HttpClientHandler
    Private ReadOnly baseUrl As String
    Private jwtTokenValue As String
    Private ReadOnly machineIp As String

    Public Sub New(baseUrl As String, machineIp As String)
        Me.baseUrl = baseUrl.TrimEnd("/"c)
        Me.machineIp = machineIp

        handler = New HttpClientHandler()
        handler.CookieContainer = New CookieContainer()
        client = New HttpClient(handler)
    End Sub

    Public Async Function Login(username As String, password As String) As Task(Of Boolean)
        Dim auth = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes($"{username}:{password}"))
        Dim request = New HttpRequestMessage(HttpMethod.Get, $"{baseUrl}/login")
        request.Headers.Authorization = New System.Net.Http.Headers.AuthenticationHeaderValue("Basic", auth)

        Dim response = Await client.SendAsync(request)
        If response.IsSuccessStatusCode Then
            ' Extract JWT token from Set-Cookie header
            Dim cookies = response.Headers.GetValues("Set-Cookie")
            For Each c In cookies
                If c.StartsWith($"jwt_token_{machineIp}") Then
                    jwtTokenValue = c.Split("="c)(1).Split(";"c)(0)
                    ' Add cookie with correct Path using handler.CookieContainer
                    Dim uri = New Uri(baseUrl)
                    handler.CookieContainer.Add(uri, New Cookie($"jwt_token_{machineIp}", jwtTokenValue, "/api", uri.Host))
                    Exit For
                End If
            Next
            Return True
        Else
            Return False
        End If
    End Function

    Public Async Function GetStatus() As Task(Of String)
        Dim response = Await client.GetAsync($"{baseUrl}/status")
        response.EnsureSuccessStatusCode()
        Return Await response.Content.ReadAsStringAsync()
    End Function

    ' --- Ping ---
    Public Async Function Ping() As Task(Of PingResponse)
        Dim response = Await client.GetStringAsync(baseUrl & "/ping")
        Return JsonConvert.DeserializeObject(Of PingResponse)(response)
    End Function

    ' --- Logout ---
    Public Async Function Logout() As Task(Of String)
        Dim response = Await client.GetStringAsync(baseUrl & "/logout")
        Return response
    End Function

    ' --- Start Transaction ---
    Public Async Function StartTransaction(request As CIActivateTransactionRequest) _
    As Task(Of CIActivateTransactionLog)

        Dim json = JsonConvert.SerializeObject(request)
        Dim content = New StringContent(json, Encoding.UTF8, "application/json")

        ' Send POST to /transaction without ?device
        Dim response = Await client.PostAsync($"{baseUrl}/transaction", content)
        response.EnsureSuccessStatusCode()

        Dim resultJson = Await response.Content.ReadAsStringAsync()
        Return JsonConvert.DeserializeObject(Of CIActivateTransactionLog)(resultJson)
    End Function

    ' --- Start Collect ---
    'Public Async Function StartCollection(device As String, request As CollectRequest) _
    '   As Task(Of Dictionary(Of String, Integer))

    '  Dim json = JsonConvert.SerializeObject(request)
    ' Dim content = New StringContent(json, Encoding.UTF8, "application/json")
    'Dim response = Await client.PostAsync($"{baseUrl}/collect?device={device}", content)
    'response.EnsureSuccessStatusCode()
    'Dim resultJson = Await response.Content.ReadAsStringAsync()
    'Return JsonConvert.DeserializeObject(Of Dictionary(Of String, Integer))(resultJson)
    'End Function

    ' --- Cancel Transaction ---
    Public Async Function CancelTransaction(device As String) As Task(Of Boolean)
        Dim response = Await client.PostAsync($"{baseUrl}/cancel", Nothing)
        response.EnsureSuccessStatusCode()
        Return True
    End Function

    ' --- Refund Transaction ---
    Public Async Function RefundTransaction(device As String, request As CIActivateTransactionRequest) _
        As Task(Of CIActivateTransactionLog)

        Dim json = JsonConvert.SerializeObject(request)
        Dim content = New StringContent(json, Encoding.UTF8, "application/json")
        Dim response = Await client.PostAsync($"{baseUrl}/refund", content)
        response.EnsureSuccessStatusCode()
        Dim resultJson = Await response.Content.ReadAsStringAsync()
        Return JsonConvert.DeserializeObject(Of CIActivateTransactionLog)(resultJson)
    End Function

    ' --- Wait for Transaction to Complete ---
    Public Async Function WaitTransaction(seqNo As String) _
        As Task(Of CIActivateTransactionLog)

        Dim response = Await client.GetStringAsync($"{baseUrl}/wait/{seqNo}")
        Return JsonConvert.DeserializeObject(Of CIActivateTransactionLog)(response)
    End Function

    ' --- Cash In ---
    Public Async Function CashIn(seqNo As String) _
        As Task(Of CIActivateTransactionLog)

        Dim body = New With {Key .seqNo = seqNo}
        Dim json = JsonConvert.SerializeObject(body)
        Dim content = New StringContent(json, Encoding.UTF8, "application/json")
        Dim response = Await client.PostAsync($"{baseUrl}/cashin", content)
        response.EnsureSuccessStatusCode()
        Dim resultJson = Await response.Content.ReadAsStringAsync()
        Return JsonConvert.DeserializeObject(Of CIActivateTransactionLog)(resultJson)
    End Function

    ' --- Cash Out ---
    Public Async Function CashOut(denominations As Dictionary(Of String, Integer)) As Task(Of CIActivateTransactionLog)

        Dim json = JsonConvert.SerializeObject(denominations)
        Dim content = New StringContent(json, Encoding.UTF8, "application/json")
        Dim response = Await client.PostAsync($"{baseUrl}/cashout", content)
        response.EnsureSuccessStatusCode()
        Dim resultJson = Await response.Content.ReadAsStringAsync()
        Return JsonConvert.DeserializeObject(Of CIActivateTransactionLog)(resultJson)
    End Function

    ' --- Show Admin Popup ---
    Public Async Function ShowAdmin(device As String, page As String, back As Boolean) As Task(Of Boolean)
        Dim body = New With {Key .page = page, Key .back = back}
        Dim json = JsonConvert.SerializeObject(body)
        Dim content = New StringContent(json, Encoding.UTF8, "application/json")
        Dim response = Await client.PostAsync($"{baseUrl}/admin", content)
        response.EnsureSuccessStatusCode()
        Return True
    End Function

    ' --- Show Prompt Popup ---
    Public Async Function ShowPrompt(device As String) As Task(Of Boolean)
        Dim response = Await client.PostAsync($"{baseUrl}/prompt", Nothing)
        response.EnsureSuccessStatusCode()
        Return True
    End Function

    ' --- Quit CI-Activate ---
    Public Async Function Quit() As Task(Of Boolean)
        Dim response = Await client.PostAsync($"{baseUrl}/quit", Nothing)
        response.EnsureSuccessStatusCode()
        Return True
    End Function

    ' --- Reset Device ---
    Public Async Function ResetDevice(device As String, Optional seqNo As String = Nothing) As Task(Of Boolean)
        Dim body = If(seqNo IsNot Nothing, New With {Key .seqNo = seqNo}, Nothing)
        Dim content As HttpContent = If(body IsNot Nothing, New StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json"), Nothing)
        Dim response = Await client.PostAsync($"{baseUrl}/reset", content)
        response.EnsureSuccessStatusCode()
        Return True
    End Function

    ' --- Restart Device ---
    Public Async Function RestartDevice(device As String, Optional seqNo As String = Nothing) As Task(Of Boolean)
        Dim body = If(seqNo IsNot Nothing, New With {Key .seqNo = seqNo}, Nothing)
        Dim content As HttpContent = If(body IsNot Nothing, New StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json"), Nothing)
        Dim response = Await client.PostAsync($"{baseUrl}/restart", content)
        response.EnsureSuccessStatusCode()
        Return True
    End Function

    ' --- Unlock Device ---
    Public Async Function UnlockDevice(device As String, seqNo As String, typeId As Integer) As Task(Of Boolean)
        Dim body = New With {Key .seqNo = seqNo, Key .type = typeId}
        Dim json = JsonConvert.SerializeObject(body)
        Dim content = New StringContent(json, Encoding.UTF8, "application/json")
        Dim response = Await client.PostAsync($"{baseUrl}/unlock", content)
        response.EnsureSuccessStatusCode()
        Return True
    End Function

    ' --- Get Inventory ---
    Public Async Function GetInventory() As Task(Of InventoryResponse)
        Dim response = Await client.GetAsync(baseUrl & "/inventory")
        response.EnsureSuccessStatusCode()
        Dim json = Await response.Content.ReadAsStringAsync()
        Return JsonConvert.DeserializeObject(Of InventoryResponse)(json)
    End Function

    ' --- Get History ---
    Public Async Function GetHistory(device As String, Optional dateStr As String = Nothing) As Task(Of List(Of History))
        Dim url = $"{baseUrl}/history"
        If dateStr IsNot Nothing Then url &= $"&date={dateStr}"
        Dim response = Await client.GetStringAsync(url)
        Return JsonConvert.DeserializeObject(Of List(Of History))(response)
    End Function

    ' --- Start Replenishment ---
    Public Async Function StartReplenishment(device As String, seqNo As String, type As String) As Task(Of Boolean)
        Dim body = New With {Key .seqNo = seqNo, Key .type = type} ' type = "entrance" or "cassette"
        Dim json = JsonConvert.SerializeObject(body)
        Dim content = New StringContent(json, Encoding.UTF8, "application/json")
        Dim response = Await client.PostAsync($"{baseUrl}/replenish", content)
        response.EnsureSuccessStatusCode()
        Return True
    End Function

    ' --- End Replenishment ---
    Public Async Function EndReplenishment(device As String) As Task(Of Dictionary(Of String, Integer))
        Dim response = Await client.DeleteAsync($"{baseUrl}/replenish")
        response.EnsureSuccessStatusCode()
        Dim json = Await response.Content.ReadAsStringAsync()
        Return JsonConvert.DeserializeObject(Of Dictionary(Of String, Integer))(json)
    End Function

    ' --- Start Collection ---
    Public Async Function StartCollection(device As String, collect As CollectRequest) As Task(Of Dictionary(Of String, Integer))
        Dim json = JsonConvert.SerializeObject(collect)
        Dim content = New StringContent(json, Encoding.UTF8, "application/json")
        Dim response = Await client.PostAsync($"{baseUrl}/collect", content)
        response.EnsureSuccessStatusCode()
        Dim responseJson = Await response.Content.ReadAsStringAsync()
        Return JsonConvert.DeserializeObject(Of Dictionary(Of String, Integer))(responseJson)
    End Function


End Class