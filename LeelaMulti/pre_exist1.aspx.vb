Imports security
Imports PMSPkgSql
Partial Public Class pre_exist1
    Inherits System.Web.UI.Page

    Private planId As Long
    Private PMSName As PMSNAMES
    Private PMSVersion As String
    Private CouponVersion As String
    Public url As String
    Private accesstype As Integer
    Private sql_query As String
    Dim pgCookie As New CCookies

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    ' Private designerPlaceholderDeclaration As System.Object
    Dim planamount As String

    'Private planId As Long
    Private utype As Long
    Dim nasip As String
    Dim wd As String = ""
    Private Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender



        Dim img As ImageButton = DirectCast(Master.FindControl("LinkButton8"), ImageButton)
        img.ImageUrl = GetLocalResourceObject("C1").ToString()


        Dim img1 As ImageButton = DirectCast(Master.FindControl("LinkButton9"), ImageButton)
        img1.ImageUrl = GetLocalResourceObject("C2").ToString()

        'Dim img2 As ImageButton = DirectCast(Master.FindControl("LinkButton10"), ImageButton)
        '' img2.ImageUrl = GetLocalResourceObject("C3").ToString()

        'img2.Visible = False
        'Dim img2 As ImageButton = DirectCast(Master.FindControl("LinkButton10"), ImageButton)
        'img2.ImageUrl = GetLocalResourceObject("C3").ToString()

        'img2.Visible = False

        'Dim img2 As ImageButton = DirectCast(Master.FindControl("LinkButton10"), ImageButton)
        'img2.ImageUrl = GetLocalResourceObject("C3").ToString()

        Button1.Text = GetLocalResourceObject("m11").ToString()
        'Label1.Text = GetLocalResourceObject("m7").ToString()
        Label2.Text = GetLocalResourceObject("m8").ToString()



    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objSysConfig As New CSysConfig
        Dim objPlan As New CPlan
        Dim RN As String = ""
        Dim grcid As String = ""
        Dim objlog As LoggerService
        objlog = LoggerService.gtInstance()
        Dim NSEID As String
        Dim encrypt As New Datasealing
        Dim MAC As String = ""
        Dim qs As String = Request.QueryString("encry")
        Dim commonFun As PMSCommonFun
        commonFun = PMSCommonFun.getInstance
        url = commonFun.BrowserQueryString(Request)
        MAC = commonFun.DecrptQueryString("MA", qs)
        RN = commonFun.DecrptQueryString("RN", qs)
        RN = ""

        Dim objloginout As LogInOutService
        objloginout = LogInOutService.getInstance
        Dim isMac2 As Integer = 0
        '*****************************LOCAL VARIABLE DECLARATION ENDS HERE**************************

        '----------------------START PMS Config ----------------------------------------
        PMSName = PMSNAMES.FIDELIO
        PMSVersion = "2.3.8"
        CouponVersion = "1"
        '----------------------END PMS Config ----------------------------------------

        '-----------------  START Findout Wired or wireless login ----------------
        If RN = "" Then
            accesstype = 0
        Else
            accesstype = 1
        End If
        '-----------------  END Findout Wired or wireless login ----------------



        Try
            'objlog.write2LogFile("wired", "rn=" & url)
        Catch ex As Exception

        End Try



        If Not IsPostBack() Then

            If MAC = "" Then Response.Redirect("UserError.aspx?Msg=Access Denied.")

            NSEID = commonFun.DecrptQueryString("UI", qs)
            Dim chknseid As String = objSysConfig.GetNasConfig(encrypt.GetEncryptedData(NSEID))
            If chknseid = "" Then
                Response.Redirect("UserError.aspx?Msg=Your Nomadix is not registered to work with this billing system")
            End If
            nasip = chknseid







            Try

                ' RN = commonFun.DecrptQueryString("RN", qs)

                If RN = "100" Or RN = "102" Or RN = "101" Then
                    RN = ""
                End If
                RN = commonFun.DecrptQueryString("RN", qs)
                RN = ""
                If RN <> "" Then

                    Try
                        ' objlog.write2LogFile("pre exist wired", "Rn=" & RN & "port=" & commonFun.DecrptQueryString("PORT", qs))
                    Catch ex As Exception

                    End Try

                    Dim intValid1 As String = ""

                    Try
                        Dim objguestnew1 As GuestService
                        objguestnew1 = GuestService.getInstance
                        intValid1 = objguestnew1.getGuestLastName(RN)

                        Try
                            'objlog.write2LogFile("pre exist wired", "Guestname" & intValid1)
                        Catch ex As Exception

                        End Try


                        If intValid1 = "-1" Or intValid1 = "" Then
                            Response.Redirect("UserError.aspx?" & GetRedirectQS() & "&Msg=" & "Dear Guest, room vacant." & "&findurl=mifilogin" & "&ct=" & Page.UICulture)
                        Else
                            wired(RN, intValid1)
                        End If


                    Catch ex As Exception

                    End Try
                End If

            Catch ex As Exception

            End Try



        End If
    End Sub
    Public Function GetLoginPwd(ByVal str As String, ByVal pd As String) As String
        Dim db As DbaseService

        Try
            Try

                db = DbaseService.getInstance
            Catch ex As Exception
                Return "-1"
            End Try

            Dim Ctime As DateTime
            Ctime = Now
            pd = UCase(pd)

            Dim ds As DataSet
            ds = db.DsWithoutUpdate("select GuestName from guestnew where upper(Guestpwd) = '" & pd.Replace("'", "''") & "' and gueststatus='A' and ExpTime > '" & Ctime & "' and GuestRoomNo ='" & str & "'")

            If ds.Tables(0).Rows.Count > 0 Then
                Return ds.Tables(0).Rows(0)(0).ToString()
            Else
                Return "-1"
            End If
        Catch ex As Exception
            Return "-1"
        End Try








    End Function

    Public Function GetLoginPwd1(ByVal str As String, ByVal pd As String) As String
        Dim db As DbaseService

        Try
            Try

                db = DbaseService.getInstance
            Catch ex As Exception
                Return "-1"
            End Try

            Dim Ctime As DateTime
            Ctime = Now
            pd = UCase(pd)

            Dim ds As DataSet
            ds = db.DsWithoutUpdate("select Guestpwd from guestnew where upper(GuestName) = '" & pd.Replace("'", "''") & "' and gueststatus='A' and ExpTime > '" & Ctime & "' and GuestRoomNo ='" & str & "'")

            If ds.Tables(0).Rows.Count > 0 Then
                Return ds.Tables(0).Rows(0)(0).ToString()
            Else
                Return "-1"
            End If
        Catch ex As Exception
            Return "-1"
        End Try








    End Function



    Protected Sub wired(ByVal room As String, ByVal lastname As String)

        Dim mac As String = ""

        Dim cc As String = ""


        Session("back") = ""
        Dim objlog As LoggerService
        objlog = LoggerService.gtInstance
        Dim commonFun As PMSCommonFun
        commonFun = PMSCommonFun.getInstance
        planId = 0
        Dim qs As String = Request.QueryString("encry")
        url = commonFun.BrowserQueryString(Request)
        mac = commonFun.DecrptQueryString("MA", qs)





        Dim roomno As String = ""
        Dim pwd As String = ""
        Dim RN As String = ""
        RN = commonFun.DecrptQueryString("RN", qs)
        RN = ""

        Try
            Session("splan") = ""
            Session("roomno") = ""
            Session("pwd") = ""
        Catch ex As Exception

        End Try

        Try

            If lastname = "" Or room = "" Then

                Try
                    pgCookie.ResetCookie(HttpContext.Current.Response)
                    Response.Redirect("UserError.aspx?" & GetRedirectQS() & "&Msg=" & "Dear Guest, Please enter LastName/Roomno" & "&findurl=mifilogin" & "&ct=" & Page.UICulture)
                Catch ex As Exception
                    'objlog.write2LogFile("MACtrack-" & userCrdential.machineId & "Error_welcome", ex.Message)
                End Try

            End If

        Catch ex As Exception

        End Try


        Try
            If Not IsNumeric(room) Then
                Response.Redirect("UserError.aspx?" & GetRedirectQS() & "&Msg=" & "Dear Guest, room vacant." & "&findurl=mifilogin" & "&ct=" & Page.UICulture)

            End If
        Catch ex As Exception

        End Try












        roomno = room
        pwd = lastname



        'Try
        '    Dim intValid As Integer = 0

        '    Dim objguestnew As GuestService
        '    objguestnew = GuestService.getInstance
        '    intValid = objguestnew.getGuestStatus(roomno, pwd)
        '    If intValid = 0 Then
        '        ' Response.Redirect("UserError.aspx?" & GetRedirectQS() & "&Msg=" & "Dear Guest, You have entered an invalid room number, for further assistance please call 0 / 1510." & "&findurl=mifilogin" & "&ct=" & Page.UICulture)


        '        Try
        '            Dim loginpwd As String = ""

        '            loginpwd = GetLoginPwd(roomno, pwd)
        '            loginpwd = loginpwd.Trim

        '            Try
        '                objlog.write2LogFile("preUser" & "_" & roomno, "Guest not found searc ing loginpwd " & loginpwd & "guestEntered" & pwd)
        '            Catch ex As Exception

        '            End Try



        '            If loginpwd = "-1" Then

        '                Try
        '                    loginFail(roomno, pwd, mac, "ErrorCode: 6 |  ErrorMsg: Invalid RoomNo/LastName.")
        '                Catch ex As Exception

        '                End Try
        '                Response.Redirect("UserError.aspx?" & GetRedirectQS() & "&Msg=" & "Dear Guest, You have entered an invalid room number, for further assistance please call 0 / 1510." & "&findurl=mifilogin" & "&ct=" & Page.UICulture)
        '                Try
        '                    objlog.write2LogFile("preUser" & "_" & roomno, "loginpwd not found  loginpwd " & loginpwd & "guestEntered" & pwd)
        '                Catch ex As Exception

        '                End Try


        '            Else

        '                Try
        '                    objlog.write2LogFile("preUser" & "_" & roomno, "loginpwd  found  Actual guestName " & loginpwd & "guestEntered Login Password" & pwd)
        '                Catch ex As Exception

        '                End Try

        '                pwd = loginpwd

        '            End If


        '        Catch ex As Exception

        '        End Try









        '    Else


        '        Dim loginpwd As String = ""

        '        loginpwd = GetLoginPwd1(roomno, pwd)
        '        loginpwd = loginpwd.Trim



        '        If loginpwd <> "-1" Then

        '            Try
        '                objlog.write2LogFile("preUser", "Guest recourd found searc ing loginpwd " & loginpwd & "guestEntered" & pwd)
        '            Catch ex As Exception

        '            End Try

        '            Try
        '                'loginFail(roomno, pwd, mac, "ErrorCode: 6 |  ErrorMsg: Invalid LoginPassword.")
        '            Catch ex As Exception

        '            End Try
        '            'Response.Redirect("UserError.aspx?" & GetRedirectQS() & "&Msg=" & "Dear Guest, You have entered an invalid room number, for further assistance please call 0 / 1510." & "&findurl=mifilogin" & "&ct=" & Page.UICulture)




        '        Else

        '            Try
        '                objlog.write2LogFile("preUser", "Guest recourd found searc ing loginpwd " & loginpwd & "guestEntered" & pwd)
        '            Catch ex As Exception

        '            End Try
        '        End If






        '    End If
        'Catch ex As Exception

        'End Try



        Try
            Dim uagent As String = Request.ServerVariables("HTTP_USER_AGENT").Trim().ToLower()
            ' objlog.write2LogFile("Device_" & mac, "Agent=" & uagent)

            Dim objdb As DbaseService
            objdb = DbaseService.getInstance
            'objdb.insertUpdateDelete("delete from MobileDetails where mac='" & mac & "'")

            If uagent.Contains("ipad") Then
                objdb.insertUpdateDelete("insert into MobileDetails(DeviceDetails,mac, devicetype,roomno,lastname) values('" & uagent & "','" & mac & "','" & "IPAD" & "','" & roomno & "',' " & pwd.Replace("'", "''") & "' )")
            Else
                objdb.insertUpdateDelete("insert into MobileDetails(DeviceDetails,mac, devicetype,roomno,lastname) values('" & uagent & "','" & mac & "','" & "Laptop" & "','" & roomno & "',' " & pwd.Replace("'", "''") & "' )")

            End If
            Dim ctime As DateTime
            ctime = Now
            'objdb.insertUpdateDelete("update MobileDetails set roomno='" & roomno & "' , LastName='" & pwd & "', loginTime='" & ctime & "' where mac='" & mac & "' and ( roomno is null or roomno ='' ) ")
        Catch ex As Exception

        End Try


        Try
            Session("roomno") = roomno
            Session("pwd") = pwd
        Catch ex As Exception

        End Try
        Dim logi As Long
        logi = 0
        Try
            logi = login_Click(roomno, pwd)  '------'remaining time
        Catch ex As Exception
            objlog.write2LogFile("login", "err" & ex.Message)
        End Try
        If logi > 0 Then
            'hdPlanSelection.Value = "0"
            'chk1.Visible = False
            'cont.Visible = False




            Try
                Response.Redirect("mifilogin.aspx?" & GetRedirectQS() & "&plan=" & planId & "&rm=" & roomno & "&ln=" & pwd & "&ct=" & Page.UICulture)
            Catch ex As Exception

            End Try

        Else
            'hdPlanSelection.Value = "1"
            'plans_t.Visible = True
            'tax.Visible = True
            'lblmsg.Visible = True
            'tit.Visible = True
            'chk1.Visible = False
            'cont.Visible = False
            'txtlastname.Text = Session("pwd")
            Try
                Response.Redirect("PlanSel.aspx?" & GetRedirectQS() & "&plan=" & planId & "&rm=" & roomno & "&ln=" & pwd & "&ct=" & Page.UICulture)
            Catch ex As Exception

            End Try
        End If
    End Sub


    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click
        Dim mac As String = ""

        Dim cc As String = ""


        Session("back") = ""
        Dim objlog As LoggerService
        objlog = LoggerService.gtInstance
        Dim commonFun As PMSCommonFun
        commonFun = PMSCommonFun.getInstance
        planId = 0
        Dim qs As String = Request.QueryString("encry")
        url = commonFun.BrowserQueryString(Request)
        mac = commonFun.DecrptQueryString("MA", qs)


        Dim r1 As String = ""

        Try
            r1 = Request.QueryString("rm")

        Catch ex As Exception

        End Try

        Try
            Dim str1() = r1.Split(",")
            Dim ind As Integer = 0
            ind = str1.Length - 1

            r1 = str1(ind)
            '  ObjElog.write2LogFile("Mac_" & commonFun.DecrptQueryString("MA", qs), "MIFI after split r1" & r1)

        Catch ex As Exception

        End Try


        Dim roomno As String = ""
        Dim pwd As String = ""
        Dim RN As String = ""
        RN = commonFun.DecrptQueryString("RN", qs)
        RN = ""

        Try
            Session("splan") = ""
            Session("roomno") = ""
            Session("pwd") = ""
        Catch ex As Exception

        End Try

        Try

            If txtlastname.Text = "" Then

                Try
                    pgCookie.ResetCookie(HttpContext.Current.Response)
                    Response.Redirect("UserError.aspx?" & GetRedirectQS() & "&Msg=" & "Dear Guest, Please enter LastName/Roomno" & "&findurl=mifilogin" & "&ct=" & Page.UICulture)
                Catch ex As Exception
                    'objlog.write2LogFile("MACtrack-" & userCrdential.machineId & "Error_welcome", ex.Message)
                End Try

            End If

        Catch ex As Exception

        End Try





        'Try
        '    If Not IsNumeric(txtRoomNo.Text.Trim) Then
        '        Response.Redirect("UserError.aspx?" & GetRedirectQS() & "&Msg=" & "Dear Guest, Please enter Valid Room Number." & "&findurl=mifilogin" & "&ct=" & Page.UICulture)

        '    End If
        'Catch ex As Exception

        'End Try


        If Not Validator.ValidateName(txtlastname.Text) Then
            ' Response.Redirect("UserError.aspx?" & GetRedirectQS() & "&Msg=" & "Dear Guest, Please enter Valid Last Name." & "&findurl=mifilogin" & "&ct=" & Page.UICulture)

        End If









        roomno = r1
        pwd = txtlastname.Text.Trim()
        If getMember3(roomno) <> "-1" Then
            Try
                Response.Redirect("UserError.aspx?" & GetRedirectQS() & "&Msg=" & "Dear Guest, Access Denied." & "&findurl=mifilogin" & "&ct=" & Page.UICulture)

            Catch ex As Exception

            End Try
        End If

        Dim res As String = ""


        res = getname(roomno, pwd)


        If res = "-1" Or res = "" Then
            Try
                loginFail(roomno, txtlastname.Text, mac, "ErrorCode: 6 |  ErrorMsg: Invalid RoomNo/Password.")
            Catch ex As Exception

            End Try
            Response.Redirect("UserError.aspx?" & GetRedirectQS() & "&Msg=" & "Dear Guest You have entered an incorrect login password for further assistance please call reception." & "&findurl=mifilogin" & "&ct=" & Page.UICulture)
            Return

        Else

            pwd = res
        End If





        Try
            Dim intValid As Integer = 0

            Dim objguestnew As GuestService
            objguestnew = GuestService.getInstance
            intValid = getGuestStatus(roomno, pwd)
            Try
                If (intValid = 1) Then
                    pwd = getGuestName(roomno, pwd)
                End If
            Catch ex As Exception

            End Try
            If intValid = 0 Then
                ' Response.Redirect("UserError.aspx?" & GetRedirectQS() & "&Msg=" & "Dear Guest, You have entered an invalid room number, for further assistance please call 0 / 1510." & "&findurl=mifilogin" & "&ct=" & Page.UICulture)


                Try
                    Dim loginpwd As String = ""

                    loginpwd = GetLoginPwd(roomno, pwd)
                    loginpwd = loginpwd.Trim

                    Try
                        objlog.write2LogFile(roomno, "Guest not found searc ing loginpwd " & loginpwd & "guestEntered" & pwd)
                    Catch ex As Exception

                    End Try



                    If loginpwd = "-1" Then

                        Try
                            loginFail(roomno, txtlastname.Text, mac, "ErrorCode: 6 |  ErrorMsg: Invalid RoomNo/LastName.")
                        Catch ex As Exception

                        End Try
                        Response.Redirect("UserError.aspx?" & GetRedirectQS() & "&Msg=" & "Dear Guest, You have entered an invalid room number, for further assistance please call 0 / 1510." & "&findurl=mifilogin" & "&ct=" & Page.UICulture)
                        Try
                            objlog.write2LogFile(roomno, "loginpwd not found  loginpwd " & loginpwd & "guestEntered" & pwd)
                        Catch ex As Exception

                        End Try


                    Else

                        Try
                            objlog.write2LogFile(roomno, "loginpwd  found  Actual guestName " & loginpwd & "guestEntered Login Password" & pwd)
                        Catch ex As Exception

                        End Try

                        pwd = loginpwd

                    End If


                Catch ex As Exception

                End Try









            Else


                Dim loginpwd As String = ""

                loginpwd = GetLoginPwd1(roomno, pwd)
                loginpwd = loginpwd.Trim



                If loginpwd <> "-1" Then

                    Try
                        ' objlog.write2LogFile("preUser", "Guest recourd found searc ing loginpwd " & loginpwd & "guestEntered" & pwd)
                    Catch ex As Exception

                    End Try

                    Try
                        'loginFail(roomno, pwd, mac, "ErrorCode: 6 |  ErrorMsg: Invalid LoginPassword.")
                    Catch ex As Exception

                    End Try
                    'Response.Redirect("UserError.aspx?" & GetRedirectQS() & "&Msg=" & "Dear Guest, You have entered an invalid room number, for further assistance please call 0 / 1510." & "&findurl=mifilogin" & "&ct=" & Page.UICulture)




                Else

                    Try
                        objlog.write2LogFile(roomno, "Guest recourd found searc ing loginpwd " & loginpwd & "guestEntered" & pwd)
                    Catch ex As Exception

                    End Try
                End If






            End If
        Catch ex As Exception

        End Try



        Try
            Dim uagent As String = Request.ServerVariables("HTTP_USER_AGENT").Trim().ToLower()
            '  objlog.write2LogFile("Device_" & mac, "Agent=" & uagent)

            Dim objdb As DbaseService
            objdb = DbaseService.getInstance
            'objdb.insertUpdateDelete("delete from MobileDetails where mac='" & mac & "'")

            If uagent.Contains("ipad") Then
                objdb.insertUpdateDelete("insert into MobileDetails(DeviceDetails,mac, devicetype,roomno,lastname) values('" & uagent & "','" & mac & "','" & "IPAD" & "','" & roomno & "',' " & pwd.Replace("'", "''") & "' )")
            Else
                objdb.insertUpdateDelete("insert into MobileDetails(DeviceDetails,mac, devicetype,roomno,lastname) values('" & uagent & "','" & mac & "','" & "Laptop" & "','" & roomno & "',' " & pwd.Replace("'", "''") & "' )")

            End If
            Dim ctime As DateTime
            ctime = Now
            'objdb.insertUpdateDelete("update MobileDetails set roomno='" & roomno & "' , LastName='" & pwd & "', loginTime='" & ctime & "' where mac='" & mac & "' and ( roomno is null or roomno ='' ) ")
        Catch ex As Exception

        End Try


        Try
            Session("roomno") = roomno
            Session("pwd") = pwd
        Catch ex As Exception

        End Try
        Dim logi As Long
        logi = 0
        Try
            logi = login_Click(roomno, pwd)  '------'remaining time
        Catch ex As Exception
            objlog.write2LogFile("login", "err" & ex.Message)
        End Try
        If logi > 0 Then
            'hdPlanSelection.Value = "0"
            'chk1.Visible = False
            'cont.Visible = False




            Try
                Response.Redirect("mifilogin.aspx?" & GetRedirectQS() & "&plan=" & planId & "&rm=" & roomno & "&ln=" & pwd & "&ct=" & Page.UICulture)
            Catch ex As Exception

            End Try

        Else
            'hdPlanSelection.Value = "1"
            'plans_t.Visible = True
            'tax.Visible = True
            'lblmsg.Visible = True
            'tit.Visible = True
            'chk1.Visible = False
            'cont.Visible = False
            'txtlastname.Text = Session("pwd")
            Try
                Response.Redirect("PlanSel.aspx?" & GetRedirectQS() & "&plan=" & planId & "&rm=" & roomno & "&ln=" & pwd & "&ct=" & Page.UICulture)
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub login(ByRef userCrdential As UserContext)
        Dim objDbase As DbaseService
        objDbase = objDbase.getInstance
        Dim objlog As LoggerService
        objlog = LoggerService.gtInstance
        Dim AAA As AAAService
        Dim output As String = ""
        Dim MAC As String = ""
        Dim qs As String = Request.QueryString("encry")
        Dim commonFun As PMSCommonFun
        commonFun = PMSCommonFun.getInstance
        url = commonFun.BrowserQueryString(Request)
        MAC = commonFun.DecrptQueryString("MA", qs)

        Try
            Dim uagent As String = Request.ServerVariables("HTTP_USER_AGENT").Trim().ToLower()
            ' objlog.write2LogFile("Device_" & MAC, "Agent=" & uagent)

            Dim objdb As DbaseService
            objdb = DbaseService.getInstance
            'objdb.insertUpdateDelete("delete from MobileDetails where mac='" & MAC & "'")

            If uagent.Contains("ipad") Then
                objdb.insertUpdateDelete("insert into MobileDetails(DeviceDetails,mac, devicetype,roomno,lastname) values('" & uagent & "','" & MAC & "','" & "IPAD" & "','" & userCrdential.roomNo & "',' " & userCrdential.guestName.Replace("'", "''") & "' )")
            Else
                objdb.insertUpdateDelete("insert into MobileDetails(DeviceDetails,mac, devicetype,roomno,lastname) values('" & uagent & "','" & MAC & "','" & "Laptop" & "','" & userCrdential.roomNo & "',' " & userCrdential.guestName.Replace("'", "''") & "' )")
            End If
            Dim ctime As DateTime
            ctime = Now
            'objdb.insertUpdateDelete("update MobileDetails set roomno='" & userCrdential.roomNo & "' , LastName='" & userCrdential.guestName & "', loginTime='" & ctime & "' where mac='" & MAC & "' and ( roomno is null or roomno ='' ) ")

        Catch ex As Exception

        End Try


        Dim cc As String = ""
        Try
            cc = Request.QueryString("ct")


            Try
                Dim str() = cc.Split(",")
                Dim ind As Integer = 0
                ind = str.Length - 1

                cc = str(ind)


            Catch ex As Exception

            End Try

        Catch ex As Exception

        End Try



        AAA = AAAService.getInstance
        Try
            output = AAA.AAA(userCrdential)
        Catch ex As Exception

        End Try
        If UCase(output) = "SUCCESS" Then

            Try
                Session.Add("us", userCrdential)
                Response.Redirect("PostLogin.aspx?" & GetRedirectQS() & "&ct=" & Page.UICulture)
            Catch ex As Exception
                'objlog.write2LogFile("MACtrack-" & userCrdential.machineId & "Error_welcome", ex.Message)
            End Try
        ElseIf UCase(output) = "COOKIE" Then
            Try
                pgCookie.ResetCookie(HttpContext.Current.Response)
                Response.Redirect("welcome.aspx?" & GetRedirectQS() & "&ct=" & Page.UICulture)
            Catch ex As Exception
                'objlog.write2LogFile("MACtrack-" & userCrdential.machineId & "Error_welcome", ex.Message)
            End Try
        Else

            Try
                pgCookie.ResetCookie(HttpContext.Current.Response)
                Response.Redirect("UserError.aspx?" & GetRedirectQS() & "&Msg=" & output & "&findurl=mifilogin" & "&ct=" & Page.UICulture)
            Catch ex As Exception
                'objlog.write2LogFile("MACtrack-" & userCrdential.machineId & "Error_welcome", ex.Message)
            End Try
        End If
    End Sub

    Private Function login_Click(ByVal lr1 As String, ByVal lr2 As String) As Long
        Dim output As Long
        output = 0
        Dim objlog As LoggerService
        objlog = LoggerService.gtInstance
        Try
            Dim objDbase As DbaseService
            Dim commonFun As PMSCommonFun
            commonFun = PMSCommonFun.getInstance

            Dim roomno As String = ""
            Dim pwd As String = ""

            roomno = Session("roomno")
            pwd = Session("pwd")

            Try
                If roomno = "" Then


                    roomno = lr1



                End If

                If pwd = "" Then


                    pwd = lr2



                End If


            Catch ex As Exception

            End Try



            Dim qs As String = Request.QueryString("encry")

            Try
                objDbase = DbaseService.getInstance

            Catch ex As Exception

            End Try


            Dim ObjElog As LoggerService
            ObjElog = LoggerService.gtInstance



            If roomno <> "" Or pwd <> "" Then
                Dim objSysConfig As New CSysConfig
                objSysConfig = New CSysConfig
                Try
                    PMSName = commonFun.GetPMSType(Trim(objSysConfig.GetConfig("PMSName")))
                    PMSVersion = objSysConfig.GetConfig("PMSVersion")
                    CouponVersion = objSysConfig.GetConfig("CouponVersion")
                Catch ex As Exception

                End Try

                Dim userCrdential As New UserContext(roomno, pwd, planId, PMSName, PMSVersion, HttpContext.Current.Request)
                userCrdential.item("usertype") = EUSERTYPE.ROOM
                userCrdential.item("accesstype") = 0 ' wireless login
                userCrdential.item("logintype") = LOGINTYPE.NEWLOGIN

                Try
                    userCrdential.item("grcid") = ""
                Catch ex As Exception

                End Try

                Try
                    Dim uagent As String = ""

                    Try
                        uagent = Request.ServerVariables("HTTP_USER_AGENT").Trim().ToLower()
                        If uagent.Contains("ipad") Then
                            userCrdential.item("accesstype") = 3
                        End If
                    Catch ex As Exception

                    End Try
                Catch ex As Exception

                End Try

                '--------------------------- START avoid Parallel Login Process ---------------
                userCrdential.item("UsrIP1") = roomno
                userCrdential.item("UsrIP2") = pwd


                Try

                    If roomno = "" Or pwd = "" Then
                        Response.Redirect("UserError.aspx?" & GetRedirectQS() & "&Msg=Dear Guest, Please enter LastName/Roomno" & "&ct=" & Page.UICulture)
                    End If

                Catch ex As Exception

                End Try



                objDbase = DbaseService.getInstance


                Dim MAC As String = commonFun.DecrptQueryString("MA", qs)

                userCrdential.wiredlessMac = MAC

                Try
                    Dim objin As LogInOutService
                    objin = LogInOutService.getInstance

                    userCrdential.wiredMac = objin.GetWiredMAC(userCrdential.wiredlessMac, userCrdential.item("UsrIP1"))

                Catch ex As Exception

                End Try
                ' RefResultset.Close()
                output = loginWifi(userCrdential)
                '--------------------------- END avoid Parallel Login Process ---------------
                Return output
            End If
        Catch ex As Exception

        End Try

        Return output

    End Function


    Private Function loginWifi(ByRef userCrdential As UserContext) As Long
        Dim objDbase As DbaseService
        Dim objlog As LoggerService
        objlog = LoggerService.gtInstance
        Dim output As Long
        Try
            objDbase = objDbase.getInstance
        Catch ex As Exception

        End Try



        If userCrdential.wiredMac = "" Then
            Try
                Dim ctime As DateTime
                ctime = Now
                Try
                    Dim sql_query As String = ""
                    sql_query = "SELECT distinct LoginMAC FROM ((LogDetails LEFT JOIN Bill ON LogDetails.LoginBillId = Bill.Billid) LEFT JOIN Guest ON Guest.Guestid = Bill.BillGrCId) " & _
                               "WHERE (Bill.BillType = " & PMSBill.ROOM & _
                                       " AND LoginAccessType=1 AND Guest.GuestName='" & userCrdential.item("UsrIP2").ToString().Replace("'", "''") & "' and      Guest.GuestRoomNo = '" & userCrdential.item("UsrIP1") & "' AND LogDetails.LoginMAC<>'" & userCrdential.wiredlessMac & "' and  LogDetails.LoginExpTime > '" & ctime & "') "
                    Dim ds As DataSet
                    Dim ind As Integer = 0
                    Try

                        ds = objDbase.DsWithoutUpdate(sql_query)
                        ind = ds.Tables(0).Rows.Count
                        If ind = 1 Then

                            Dim wl As String = ""

                            Try
                                Dim objloginout As LogInOutService
                                objloginout = LogInOutService.getInstance
                                wl = objloginout.GetWiredlessMAC(ds.Tables(0).Rows(0)("LoginMAC"), userCrdential.item("UsrIP1"))
                                If wl = "" Or wl = userCrdential.wiredlessMac Then
                                    userCrdential.wiredMac = ds.Tables(0).Rows(0)("LoginMAC")

                                End If
                            Catch ex As Exception

                            End Try

                        End If
                    Catch ex As Exception
                    End Try
                Catch ex As Exception

                End Try
            Catch ex As Exception

            End Try
        End If

        Try
            If userCrdential.wiredMac <> "" Then
                Dim userContext As New UserContext(userCrdential.roomNo, userCrdential.wiredMac, userCrdential.nomadixId)
                Dim gateWayQryResult As New NdxQueryGatewayResults
                Dim gateWay As IGatewayService
                Dim gateWayFact As GatewayServiceFactory
                gateWayFact = GatewayServiceFactory.getInstance
                gateWay = gateWayFact.getGatewayService("FIDELIO", "2.3.8")


            End If
        Catch ex As Exception
            objlog.write2LogFile("ExceptionMIFI", ex.Message)
        End Try

        Dim AAA As AAAService

        output = 0
        AAA = AAAService.getInstance

        Try
            output = AAA.AAAA(userCrdential)
        Catch ex As Exception

        End Try






        Return output

    End Function

    Protected Sub lk_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lk.Click
        Try
            Dim commonFun As PMSCommonFun
            commonFun = PMSCommonFun.getInstance
            url = commonFun.BrowserQueryString(Request)
            Response.Redirect("terms.aspx?" & GetRedirectQS() & "&bk=c" & "&ct=" & Page.UICulture)

        Catch ex As Exception

        End Try
    End Sub

    'Protected Sub lk1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles LinkButton1.Click
    '    Try
    '        Dim commonFun As PMSCommonFun
    '        commonFun = PMSCommonFun.getInstance
    '        url = commonFun.BrowserQueryString(Request)
    '        Response.Redirect("http://resetmypassword.in")

    '    Catch ex As Exception

    '    End Try
    'End Sub



    Public Sub loginFail(ByVal rno As String, ByVal ln As String, ByVal mac As String, ByVal str As String)

        Try
            Dim objMail As MailService1
            objMail = MailService1.getInstance
            objMail.SendAdminMail(rno, ln, 1, str, mac, ErrTypes.RLFGT)

        Catch ex As Exception

        End Try



        Try
            Dim objDBase As DbaseService
            objDBase = DbaseService.getInstance
            sql_query = "INSERT INTO Loginfails	(FailUserId, FailPassword, FailMAC, FailUserType, FailPlanId,FailRemarks,FailTime) VALUES ('" & rno & "', '" & Replace(ln, "'", "''") & "', '" & mac & "', 0, " & "1" & ", '" & Replace(str, "'", "''") & "', '" & Now & "') "
            objDBase.insertUpdateDelete(sql_query)

        Catch ex As Exception

        End Try
    End Sub

    Private Function GetRedirectQS1()

        Try
            Dim commonFun As PMSCommonFun
            commonFun = PMSCommonFun.getInstance
            Dim redirectQS As String = ""
            redirectQS = "encry=" & commonFun.EncrptQueryString(Request)
            Return redirectQS
        Catch ex As Exception

        End Try

    End Function


    Private Function GetRedirectQS()
        Dim redirectQS As String = ""
        redirectQS = "encry=" & Request.QueryString("encry")
        Return redirectQS
    End Function

    Public Function getGuestStatus(ByVal guestroomno As String, ByVal guestName As String) As Integer


        Dim SQL_query As String

        Dim objlog As LoggerService
        objlog = LoggerService.gtInstance

        Dim objDbase As DbaseService
        objDbase = DbaseService.getInstance

        Dim gn As String = ""

        gn = UCase(guestName)


        Dim refDataSet As DataSet

        ' SQL_query = "select top 1 GuestId,GuestRoomNo,GuestName from Guest where  upper(GuestName) =@GN and  GuestRoomNo = @RM and GuestStatus ='A' order by GuestId desc"
        SQL_query = "select top 1 GuestId,GuestRoomNo,GuestName from Guest where  SUBSTRING ( upper(GuestName), 1,4) = SUBSTRING ( '" & gn.Replace("'", "''") & "', 1,4) and  GuestRoomNo = '" & guestroomno & "' and GuestStatus ='A' order by GuestId desc"

        Try
            '   objlog.write2LogFile("gueststatusqry", SQL_query)
        Catch ex As Exception

        End Try


        Try
            refDataSet = objDbase.DsWithoutUpdate(SQL_query)


            If refDataSet.Tables(0).Rows.Count > 0 Then

                Return 1
            Else
                Return 0
            End If


        Catch ex As Exception

        End Try





    End Function




    Public Function getGuestName(ByVal guestroomno As String, ByVal guestName As String) As String


        Dim SQL_query As String

        Dim objlog As LoggerService
        objlog = LoggerService.gtInstance

        Dim objDbase As DbaseService
        objDbase = DbaseService.getInstance

        Dim gn As String = ""

        gn = UCase(guestName)


        Dim refDataSet As DataSet

        ' SQL_query = "select top 1 GuestId,GuestRoomNo,GuestName from Guest where  upper(GuestName) =@GN and  GuestRoomNo = @RM and GuestStatus ='A' order by GuestId desc"
        SQL_query = "select top 1 GuestName from Guest where   SUBSTRING ( upper(GuestName), 1,4) = SUBSTRING ( '" & gn.Replace("'", "''") & "', 1,4) and  GuestRoomNo = '" & guestroomno & "' and GuestStatus ='A' order by GuestId desc"

        Try
            ' objlog.write2LogFile("gueststatusqry1", SQL_query)
        Catch ex As Exception

        End Try


        Try
            refDataSet = objDbase.DsWithoutUpdate(SQL_query)


            If refDataSet.Tables(0).Rows.Count > 0 Then

                Return refDataSet.Tables(0).Rows(0)(0).ToString()
            Else
                Return "-1"
            End If


        Catch ex As Exception

        End Try





    End Function


    Public Function getMember3(ByVal str As String) As String
        Dim db As DbaseService
        Dim objlog As LoggerService
        objlog = LoggerService.gtInstance


        Try
            Try

                db = DbaseService.getInstance
            Catch ex As Exception
                Return "-1"
            End Try


            Dim ds As DataSet

            Try
                'objlog.write2LogFile("NomadixMembQry", "select GuestCFA1 from Guest where upper(GuestName) = '" & pd.Replace("'", "''") & "' and gueststatus='A'  and GuestRoomNo ='" & str & "' order by guestid desc")
            Catch ex As Exception

            End Try


            ds = db.DsWithoutUpdate("select name from room where name is not null and active=1 and  name='" & str & "'")

            If ds.Tables(0).Rows.Count > 0 Then
                Return ds.Tables(0).Rows(0)(0).ToString().Trim()
            Else
                Return "-1"
            End If
        Catch ex As Exception
            Return "-1"
        End Try








    End Function

    Public Function getname(ByVal rno As String, ByVal name As String) As String

        Try
            Dim db As DbaseService
            Dim result As DataSet
            Try

                Try

                    db = DbaseService.getInstance
                Catch ex As Exception
                    Return "-1"
                End Try

                Dim sq As String = ""

                sq = "select GuestName from guest where GuestRoomNo='" & rno & "'  and pwd='" & name.Replace("'", "''") & "' and Gueststatus='A' and  pwd is not null order by guestid desc"

                result = db.DsWithoutUpdate(sq)


                If result.Tables(0).Rows.Count > 0 Then
                    Return result.Tables(0).Rows(0)(0).ToString()
                Else

                    Return "-1"
                End If

            Catch ex As Exception
                Return "-1"
            End Try


        Catch ex As Exception
            Return "-1"
        End Try


    End Function

End Class