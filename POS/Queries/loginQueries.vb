Module loginQueries
    'Login Screen
    Public NEW_SESSION As String = "insert into sessions (uuid, login_when, is_active, machine_name, user_id, amountLaxeiaOnLogin) " &
                                   "values (sys_guid(), (select systimestamp from dual), 1, :1, " &
                                   "       (select uuid from users where username = :2), :3)"

    Public GET_ALL_USERS_AND_PASS As String = "select username, pass from users"
End Module
