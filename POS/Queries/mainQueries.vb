Module mainQueries
    'Main Screen
    Public Q_EXPORT_DB As String = "select paramvalue from global_params where paramkey = 'export.path'"
    Public Q_GET_INITIAL_AMOUNT As String = "select paramvalue from global_params where paramkey = 'init.fiscal.amt'"
End Module
