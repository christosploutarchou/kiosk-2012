Module moduleQueries    
    'Module Queries
    Public GET_VAT_FOR_GLOBAL_ITEMS As String = "select paramkey, paramvalue " & _
                                      "from global_params  " & _
                                      "where paramkey in ('vat.grafiki.ili','vat.laxeia', 'vat.efimerides', " & _
                                      "                   'vat.periodika', 'vat.stavrolexa')"

    Public GET_USER_BY_ID As String = "select username from users " & _
                                    "where uuid = :1"

    Public GET_TOTALS As String = "select count(*), NVL(sum(total_amt_with_disc),0), NVL(sum(total_vat5),0), " &
                                  "                 NVL(sum(total_vat19),0),         NVL(sum(total_vat0),0), NVL(sum(total_vat3),0) " &
                                  "from receipts " &
                                  "where created_by = :1 " &
                                  "and created_on between (select max(login_when) from sessions " &
                                  "                        where user_id = :1) " &
                                  "                        and (select systimestamp from dual) " &
                                  "order by created_on "
End Module
