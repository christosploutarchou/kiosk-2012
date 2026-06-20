Module unlockQueries
    'Unlock Screen
    Public UNLOCK_USER As String = "update sessions set is_active=0, logout_when = (select systimestamp from dual) " &
                                   "where user_id = :1 and is_active = 1"

    Public GET_TOTALS_FOR_X_REPORT As String = "select from_date, to_date, total_receipts, total5percent, total19percent, payments, initial_amt, " & _
                                               "final_amt, description, total0percent, amount_laxeia, initialAmtLaxeia, amountVisa, total3percent " & _
                                               "from x_report where user_id = :1 and created_on = (select max(created_on) from x_report)"
End Module
