Module loginQueries
    'Login Screen
    Public GET_USERS_ON_LOGIN As String = "select username from users where deleted = 0"

    Public GET_USER_INFO As String = "select uuid, access_level, NVL(is_unlock,0), NVL(view_reports,0), NVL(edit_prod,0), NVL(edit_prod_full,0) from users where username = :1 and pass = :2 "

    Public NEW_SESSION As String = "insert into sessions (uuid, login_when, is_active, machine_name, user_id, amountLaxeiaOnLogin) " & _
                                   "values (sys_guid(), (select systimestamp from dual), 1, :1, " & _
                                   "       (select uuid from users where username = :2), :3)"

    Public CHECK_IF_LOGGED_IN As String = "select count(*) from sessions " & _
                                          "where user_id = (select uuid from users where username = :1) and is_active = 1"

    Public GET_ALL_PASS_ENCRYPTED As String = "select paramvalue from global_params where paramkey='all.pass.encrypted'"

    Public GET_ALL_USERS_AND_PASS As String = "select username, pass from users"
End Module
