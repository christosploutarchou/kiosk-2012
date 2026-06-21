Module moduleQueries
    'Module Queries
    Public GET_VAT_FOR_GLOBAL_ITEMS As String = "select paramkey, paramvalue " &
                                      "from global_params  " &
                                      "where paramkey in ('vat.grafiki.ili','vat.laxeia', 'vat.efimerides', " &
                                      "                   'vat.periodika', 'vat.stavrolexa')"




End Module
