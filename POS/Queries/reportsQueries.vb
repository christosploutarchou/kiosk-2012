Module reportsQueries
    'Reports Screen
    Public Q_PRODUCTS_PER_SUPPLIER As String = "select serno, s_name, concat(phone_1,NVL(phone_2,'')) phone, description, " & _
                                             "NVL(BUY_AMT_NO_VAT,0), NVL(sell_amt,0), " & _
                                             "(select barcode from barcodes where barcodes.PRODUCT_SERNO = serno and rownum < 2) barcode, " & _
                                             "to_number(avail_quantity,'99999'), " & _
                                             "to_number(stock_quantity,'99999') " & _
                                             "from products p " & _
                                             "inner join suppliers s on p.supplier_id = s.uuid " & _
                                             "where p.supplier_id = :1 " & _
                                             "order by to_number(avail_quantity,'99999')"

    Public Q_GET_SUPPLIERS As String = "select uuid, s_name, phone_1, phone_2 " & _
                                       "from suppliers order by s_name asc"

    Public Q_GET_USERS As String = "select uuid, username " & _
                                   "from users where deleted = 0 order by username asc"

    Public Q_GET_CATEGORIES As String = "select uuid, description, vat " & _
                               "from categories order by description asc"
End Module
