Module kronosAdminQueries
    'KRONOS Admin Screen
    Public Q_GET_ALL_KRONOS As String = "select NVL(BARCODE,''), NVL(ITEM_CODE,' '), NVL(ITEM_NAME,' '), NVL(EXTENDED_ITEM_NAME,' '), " & _
                                        "NVL(ISSUE_NUMBER,' '), NVL(DELIVERY_DATE,' ')," & _
                                        "NVL(PRICE,-1), NVL(VAT,' '), NVL(PRICE_1,-1), NVL(PRICE_2,-1), NVL(QUANTITY_SENT,-1), NVL(RETURN_DATE,' '), " & _
                                        "NVL(DAILY_FLAG,' '), NVL(SORTING_CODE,-1) " & _
                                        "from(kronos_products) where is_deleted = 0 " & _
                                        "order by BARCODE"

    Public Q_GET_ALL_PROCESSED As String = "select FILE_NAME, PROCESSED_WHEN " & _
                                           "from kronos_processed " & _
                                           "order by processed_when "

    Public Q_GET_ALL_REPORTS As String = "select FILE_NAME, PROCESSED_WHEN " & _
                                         "from kronos_reports " & _
                                         "order by processed_when "

    Public Q_GET_ALL_JOBS As String = "select NVL(to_char(LAST_DATE,'DD-MON-YY'),' '), NVL(LAST_SEC, 0), NVL(NEXT_DATE,''), NVL(NEXT_SEC,''), " & _
                                      "NVL(BROKEN,''), NVL(INTERVAL, ''), NVL(FAILURES,0), NVL(WHAT,'') " & _
                                      "from(user_jobs) order by job "

End Module
