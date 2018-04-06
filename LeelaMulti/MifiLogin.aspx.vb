Imports PMSPkgSql
Imports security
Partial Public Class MifiLogin
    Inherits System.Web.UI.Page
    Private planId As Long = 0
    Private PMSName As PMSNAMES
    Private PMSVersion As String
    Private CouponVersion As String
    Public url As String
    Private accesstype As Integer
    Private sql_query As String
    Dim pgCookie As New CCookies

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

       

        Dim commonFun As PMSCommonFun
        Dim RN As String = ""
        ' Dim planid As Long
        Dim objlog As LoggerService
        objlog = LoggerService.gtInstance()
        Dim strplan As String = ""
        Dim encrypt As New Datasealing
        Dim MAC As String = ""
        ' RN = Request.QueryString("RN")
        commonFun = PMSCommonFun.getInstance
        url = commonFun.BrowserQueryString(Request)

        Session("back") = ""

        Try
            If planId = 0 Then
                planId = Session("splan")
            End If
        Catch ex As Exception

        End Try


        Dim qs As String = Request.QueryString("encry")
        MAC = commonFun.DecrptQueryString("MA", qs)
        RN = ""

        RN = ""

        Try
            planId = Session("splan")
        Catch ex As Exception

        End Try

        '-------------- START VLAN Login page 28 - 12 - 2009 -------------------
        If RN <> "" Then
            Response.Redirect("Vlanlogin.aspx?" & url)
        End If
        '-------------- END VLAN Login page 28 - 12 - 2009 -------------------

        '----------------------START PMS Config --------------------------------------
        commonFun = PMSCommonFun.getInstance

        '----------------------END PMS Config ----------------------------------------
        ' RN = Request.QueryString("RN")
        'MAC = Request("Ma")
        '-----------------  START Findout Wired or wireless login ----------------
        If RN = "" Then
            accesstype = 0
        Else
            accesstype = 1
        End If
        '-----------------  END Findout Wired or wireless login ----------------

        


        If Not IsPostBack() Then

            If MAC = "" Then Response.Redirect("UserError.aspx?Msg=Access Denied.")


            Try
                planId = Session("splan")
            Catch ex As Exception

            End Try

            Try
                'objlog.write2LogFile("Mobile_" & commonFun.DecrptQueryString("MA", qs), "Selected Plan in mifi " & planId)
            Catch ex As Exception

            End Try


            ' Dim strplan As String = ""

            Try
                strplan = Request.QueryString("plan")
                ' objlog.write2LogFile("Mobile_" & commonFun.DecrptQueryString("MA", qs), "MIFI strplan" & strplan)

                Try
                    Dim str() = strplan.Split(",")
                    Dim ind As Integer = 0
                    ind = str.Length - 1

                    strplan = str(ind)
                    'objlog.write2LogFile("Mobile_" & commonFun.DecrptQueryString("MA", qs), "MIFI after split strplan" & strplan)

                Catch ex As Exception

                End Try





            Catch ex As Exception

            End Try

            Try
                'objlog.write2LogFile("Mobile_" & commonFun.DecrptQueryString("MA", qs), " query Plan selected: " & strplan)
            Catch ex As Exception

            End Try


            Try
                If planId = 0 Then
                    planId = Long.Parse(strplan.Trim())
                End If
            Catch ex As Exception

            End Try

            Try
                'objlog.write2LogFile("Mobile_" & commonFun.DecrptQueryString("MA", qs), " final Plan selected: " & planId)
            Catch ex As Exception

            End Try






            






            Try
                login_Click()

            Catch ex As Exception

            End Try



        End If



    End Sub
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


    Private Sub login(ByRef userCrdential As UserContext)
        Dim objDbase As DbaseService
        Dim objlog As LoggerService
        objlog = LoggerService.gtInstance
        Try
            objDbase = DbaseService.getInstance
        Catch ex As Exception

        End Try


        Try

            Dim mac As String = ""
            mac = userCrdential.machineId

            Dim uagent As String = Request.ServerVariables("HTTP_USER_AGENT").Trim().ToLower()
            objlog.write2LogFile("Device_" & mac, "Agent=" & uagent)

            Dim objdb As DbaseService
            objdb = DbaseService.getInstance
            'objdb.insertUpdateDelete("delete from MobileDetails where mac='" & MAC & "'")
            ' objdb.insertUpdateDelete("delete from MobileDetails where mac='" & mac & "'")
            'objdb.insertUpdateDelete("delete from MobileDetails where mac='" & mac & "'")

            If uagent.Contains("ipad") Then
                objdb.insertUpdateDelete("insert into MobileDetails(DeviceDetails,mac, devicetype,roomno,lastname) values('" & uagent & "', '" & mac & "','" & "IPad" & "','" & userCrdential.roomNo & "',' " & userCrdential.guestName.Replace("'", "''") & "' )")



            Else

                objdb.insertUpdateDelete("insert into MobileDetails(DeviceDetails,mac, devicetype,roomno,lastname) values('" & uagent & "','" & mac & "','" & "Laptop" & "','" & userCrdential.roomNo & "',' " & userCrdential.guestName.Replace("'", "''") & "' )")



            End If
            Dim ctime As DateTime
            ctime = Now

            'objdb.insertUpdateDelete("update MobileDetails set roomno='" & userCrdential.roomNo & "' , LastName='" & userCrdential.guestName & "', loginTime='" & ctime & "' where mac='" & mac & "' and ( roomno is null or roomno ='' ) ")




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
                        '  objlog.write2LogFile("MAC-" & userCrdential.wiredlessMac, "Login by other mac--> Query=" & sql_query & "count=" & ind & "WiredMac" & userCrdential.wiredMac)






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
                'objlog.write2LogFile("RLogout", "LOGOUT wired MAC:" & userCrdential.wiredMac)
                gateWayFact = GatewayServiceFactory.getInstance
                gateWay = gateWayFact.getGatewayService("FIDELIO", "2.3.8")

                Try

                    Try
                        'gateWayQryResult = gateWay.logout(userContext)

                    Catch ex As Exception

                    End Try



                    'updateProcess(userCrdential.wiredMac, userCrdential.machineId, userCrdential)



                Catch ex As Exception
                    ' objlog.write2LogFile("logoutwired", "err" & ex.Message)
                End Try
            End If
        Catch ex As Exception
            objlog.write2LogFile("ExceptionMIFI", ex.Message)
        End Try

        Dim AAA As AAAService
        Dim output As String = ""
        AAA = AAAService.getInstance

        Try
            output = AAA.AAA(userCrdential)
        Catch ex As Exception

        End Try




        Try
            objlog.write2LogFile("OUTPUT_" & userCrdential.machineId, "AAA Result output=" & output)

        Catch ex As Exception

        End Try


        If UCase(output) = "SUCCESS" Then

            Try
                objlog.write2LogFile("sucess", userCrdential.requestedPage)
            Catch ex As Exception

            End Try



            Try
                Try
                    Session.Add("us", userCrdential)
                Catch ex As Exception

                End Try


                Dim tipid As Long = 0
                Try


                    tipid = AAA.GetoldPlanid(userCrdential)

                Catch ex As Exception

                End Try


                Dim kid As Long = 0

                Try
                    kid = userCrdential.selectedPlanId
                Catch ex As Exception

                End Try



                Try
                    objlog.write2LogFile("laptoptest", "TPlanid" & tipid & "kid=" & tipid)
                Catch ex As Exception

                End Try

                Try
                    Dim objSysConfig As New CSysConfig
                    Dim serverIP As String = ""
                    serverIP = objSysConfig.GetConfig("BillServer_IP")


                    If kid = 101 Or tipid = 101 Then

                        Try

                            ' objlog.write2LogFile("Hari", userCrdential.password & userCrdential.roomNo)
                        Catch ex As Exception

                        End Try
                        Try
                            Response.Redirect("PostLogin.aspx?" & GetRedirectQS() & "&ct=" & Page.UICulture)
                        Catch ex As Exception

                        End Try


                    Else

                        Try
                            Response.Redirect("PostLogin.aspx?" & GetRedirectQS() & "&ct=" & Page.UICulture)
                        Catch ex As Exception

                        End Try


                    End If


                Catch ex As Exception

                End Try















                Try
                    Response.Redirect("PostLogin.aspx?" & GetRedirectQS() & "&ct=" & Page.UICulture)
                Catch ex As Exception

                End Try



            Catch ex As Exception

            End Try


            ' Try
            '    Try
            '        Dim qs As String = ""
            '        Dim macaddr As String = ""
            '        Dim commonFun1 As PMSCommonFun
            '        commonFun1 = PMSCommonFun.getInstance
            '        qs = Request.QueryString("encry")
            '        macaddr = commonFun1.DecrptQueryString("MA", qs)
            '        Dim lpwd As String = ""

            '        Dim ds As DataSet
            '        ds = GetRoomNo(macaddr)

            '        Dim rno As String = ""
            '        Dim gn As String = ""
            '        Dim strGID As String = ""

            '        rno = ds.Tables(0).Rows(0)(0)
            '        gn = ds.Tables(0).Rows(0)(1)
            '        strGID = ds.Tables(0).Rows(0)(2)
            '        Try
            '            objlog.write2LogFile("Loginpwd", "rno" & rno & "gn" & gn & "gid" & strGID)
            '            lpwd = GetLoginPwd1(rno, gn)
            '        Catch ex As Exception

            '        End Try





            '        Try
            '            objlog.write2LogFile("Loginpwd", "pwd" & lpwd)
            '        Catch ex As Exception

            '        End Try


            '        If lpwd = "-1" Then

            '            Try
            '                objlog.write2LogFile("reqPageMifi", userCrdential.requestedPage)
            '            Catch ex As Exception

            '            End Try

            '            ' Response.Redirect("ChangePwd.aspx?" & GetRedirectQS() & "&rm=" & rno & "&ln=" & gn & "&gid=" & strGID & "&ct=" & Page.UICulture)

            '        Else
            '            Try
            '                Response.Redirect("PostLogin.aspx?" & GetRedirectQS() & "&ct=" & Page.UICulture)
            '            Catch ex As Exception

            '            End Try

            '        End If








            '    Catch ex As Exception
            '        Try
            '            'Response.Redirect("PostLogin.aspx?" & GetRedirectQS() & "&ct=" & Page.UICulture)
            '        Catch ex1 As Exception

            '        End Try
            '    End Try
            'Catch ex As Exception

            'End Try





            'Catch ex As Exception

            'End Try



        ElseIf UCase(output) = "COOKIE" Then
            Try
                pgCookie.ResetCookie(HttpContext.Current.Response)
                Response.Redirect("welcome.aspx?" & GetRedirectQS())
            Catch ex As Exception

            End Try


        Else
            '--------------------------- START avoid Parallel Login Process ---------------


            '  objlog.write2LogFile("AAA", sql_query)

            'objDbase = DbaseService.getInstance


            '--------------------------- END avoid Parallel Login Process -----------------

            Dim commonFun As PMSCommonFun
            commonFun = PMSCommonFun.getInstance
            url = commonFun.BrowserQueryString(Request)




            Try
                pgCookie.ResetCookie(HttpContext.Current.Response)
                Response.Redirect("UserError.aspx?" & GetRedirectQS() & "&Msg=" & output & "&findurl=mifilogin")

            Catch ex As Exception

            End Try



        End If
    End Sub
    'Public Function GetRoomNo(ByVal MAC As String) As DataSet
    '    Dim SQL_query As String
    '    Dim LoginPlanTime As Long
    '    Dim UsedTime As Long
    '    Dim RemainingTime As Long
    '    Dim LoginTime As Date
    '    Dim RefResultset As DataSet
    '    Dim objDbase As DbaseService
    '    Dim objlog As LoggerService
    '    Dim _LoginExpTime As DateTime
    '    objlog = LoggerService.gtInstance

    '    SQL_query = "select  guestroomno,guestname,guestid from guest where guestid in ( select top 1  billgrcid from bill  where billid in ( select top 1 loginbillid from logdetails where loginmac='" & MAC & "' order by LoginId desc))"
    '    Try

    '        objDbase = DbaseService.getInstance
    '        RefResultset = objDbase.DsWithoutUpdate(SQL_query)
    '        objlog.write2LogFile("subbu", SQL_query)

    '        If RefResultset.Tables(0).Rows.Count = 0 Then


    '            SQL_query = "select couponuserid, couponpassword from coupons where couponid in ( select top 1  billgrcid from bill where billmac='" & MAC & "'   order by billid desc) "

    '            objDbase = DbaseService.getInstance
    '            RefResultset = objDbase.DsWithoutUpdate(SQL_query)


    '        End If



    '        Return RefResultset

    '    Catch ex As Exception

    '        Try
    '            objlog.write2LogFile("subbu", "err" & ex.Message)
    '        Catch ex1 As Exception

    '        End Try
    '    End Try

    'End Function


    Public Function Maccount(ByVal guestname As String, ByVal roomno As String, ByVal mac As String) As Integer

        Dim ind As Integer = 0
        Dim objDbase As DbaseService
        Dim objlog As LoggerService
        objlog = LoggerService.gtInstance
        Dim ds As DataSet
        Try
            objDbase = DbaseService.getInstance
        Catch ex As Exception

        End Try

        Dim Ctime As DateTime
        Ctime = Now

        Try
            Dim sql_query As String = ""

            sql_query = "SELECT distinct LOGINMAC FROM ((LogDetails LEFT JOIN Bill ON LogDetails.LoginBillId = Bill.Billid) LEFT JOIN Guest ON Guest.Guestid = Bill.BillGrCId) " & _
                             "WHERE (Bill.BillType = " & PMSBill.ROOM & _
                                    "  AND Guest.GuestName='" & guestname.Replace("'", "''") & "' and   LOGINMAC <> '" & mac & "' and LoginExpTime > '" & Ctime & "'  and Guest.GuestRoomNo = '" & roomno & "' ) "



            Try

                ds = objDbase.DsWithoutUpdate(sql_query)
                ind = ds.Tables(0).Rows.Count

            Catch ex As Exception

            End Try
        Catch ex As Exception

        End Try

        Try
            ' objlog.write2LogFile("maccount", "no of wireless mac=" & ind & "QUERY" & sql_query)
        Catch ex As Exception

        End Try

        Return ind


    End Function

    Private Sub login_Click()
        Dim ObjElog As LoggerService
        ObjElog = LoggerService.gtInstance
        Dim noofdays As Integer = 0


        Try
            Dim qs As String = Request.QueryString("encry")
            Dim objDbase As DbaseService
            'Dim ProcessStatus As Integer
            'Dim RefResultset As DataSet
            Dim commonFun As PMSCommonFun
            commonFun = PMSCommonFun.getInstance
            Dim strplan As String = ""
            Dim roomno As String = ""
            Dim pwd As String = ""
            Try
                roomno = Session("roomno")
                pwd = Session("pwd")
            Catch ex As Exception

            End Try


            Try
                'ObjElog.write2LogFile("Mobile_" & commonFun.DecrptQueryString("MA", qs), "Selected Plan in mifi planid " & planId & "ses Roomno" & roomno & "sess pwd" & pwd)
            Catch ex As Exception

            End Try

            Dim r1 As String = ""
            Dim r2 As String = ""

            Try
                r1 = Request.QueryString("rm")
                r2 = Request.QueryString("ln")

                'ObjElog.write2LogFile("Mobile_" & commonFun.DecrptQueryString("MA", qs), "MIFI before split r1" & r1)

                Try
                    Dim str() = r1.Split(",")
                    Dim ind As Integer = 0
                    ind = str.Length - 1

                    r1 = str(ind)
                    'ObjElog.write2LogFile("Mobile_" & commonFun.DecrptQueryString("MA", qs), "MIFI after split r1" & r1)

                Catch ex As Exception

                End Try
                'ObjElog.write2LogFile("Mobile_" & commonFun.DecrptQueryString("MA", qs), "MIFI before split r2" & r2)

                Try
                    Dim str() = r2.Split(",")
                    Dim ind As Integer = 0
                    ind = str.Length - 1

                    r2 = str(ind)
                    ' ObjElog.write2LogFile("Mobile_" & commonFun.DecrptQueryString("MA", qs), "MIFI after split r2" & r2)

                Catch ex As Exception

                End Try



            Catch ex As Exception

            End Try

            Try
                'objlog.write2LogFile("Mobile_" & commonFun.DecrptQueryString("MA", qs), "Selected Plan in radio event planid " & planId & "query Roomno" & r1 & "query pwd" & r2)
            Catch ex As Exception

            End Try

            Try
                If r1 <> "" And r2 <> "" Then
                    If roomno = "" Then
                        roomno = r1
                    End If
                    If pwd = "" Then
                        pwd = r2
                    End If
                End If

            Catch ex As Exception

            End Try


            Try
                'ObjElog.write2LogFile("Mobile_" & commonFun.DecrptQueryString("MA", qs), "Selected Plan in mifi final planid " & planId & "ses Roomno" & roomno & "sess pwd" & pwd)
            Catch ex As Exception

            End Try







            Dim objlog As LoggerService
            objlog = LoggerService.gtInstance



            'MAC = commonFun.DecrptQueryString("MA", qs)
            ' RN=""

            Try
                objDbase = DbaseService.getInstance

            Catch ex As Exception

            End Try



            Try
                planId = Session("splan")
            Catch ex As Exception

            End Try

            Try
                ' objlog.write2LogFile("Mobile_" & commonFun.DecrptQueryString("MA", qs), "Selected Plan in mifi " & planId)
            Catch ex As Exception

            End Try


            ' Dim strplan As String = ""

            Try
                strplan = Request.QueryString("plan")
                ' objlog.write2LogFile("Mobile_" & commonFun.DecrptQueryString("MA", qs), "MIFI strplan" & strplan)

                Try
                    Dim str() = strplan.Split(",")
                    Dim ind As Integer = 0
                    ind = str.Length - 1

                    strplan = str(ind)
                    'objlog.write2LogFile("Mobile_" & commonFun.DecrptQueryString("MA", qs), "MIFI after split strplan" & strplan)

                Catch ex As Exception

                End Try



            Catch ex As Exception

            End Try
            Dim strdys As String = ""

            Try
                Try
                    strdys = Request.QueryString("nd")
                    ' objlog.write2LogFile("Mobile_" & commonFun.DecrptQueryString("MA", qs), "MIFI strplan" & strplan)

                    Try
                        Dim str() = strdys.Split(",")
                        Dim ind As Integer = 0
                        ind = str.Length - 1

                        strdys = str(ind)
                        'objlog.write2LogFile("Mobile_" & commonFun.DecrptQueryString("MA", qs), "MIFI after split strplan" & strplan)

                    Catch ex As Exception

                    End Try



                Catch ex As Exception

                End Try
            Catch ex As Exception

            End Try

            Try
                noofdays = strdys
            Catch ex As Exception

            End Try








            Try
                'objlog.write2LogFile("Mobile_" & commonFun.DecrptQueryString("MA", qs), " mifi query Plan selected: " & strplan)
            Catch ex As Exception

            End Try


            Try
                If planId = 0 Then
                    planId = Long.Parse(strplan.Trim())
                End If
            Catch ex As Exception

            End Try

            Try
                'objlog.write2LogFile("Mobile_" & commonFun.DecrptQueryString("MA", qs), " mifi final Plan selected: " & planId)
            Catch ex As Exception

            End Try





            Try
                'planId = Session("splan")
                'ObjElog.write2LogFile("Mobile_" & commonFun.DecrptQueryString("MA", qs), "Mifi login Plan selected----> " & planId & "pwd=" & pwd & "room no" & roomno)

            Catch ex As Exception

            End Try


            'If planId = 0 Then

            '    Response.Redirect("UserErrorSql.aspx?" & url & "&Msg=Dear Guest, Please select Plan.&findurl=welcomepage")
            'End If




            If roomno <> "" Or pwd <> "" Then
                Dim objSysConfig As New CSysConfig
                'Dim Lastname = objSysConfig.GetConfig("PMSLastname")

                '  If Lastname <> pwd Then  ' If Guest enter $$$123$$, It's showing Invalid Username
                ' lblerr.Visible = False

                objSysConfig = New CSysConfig
                Try
                    PMSName = commonFun.GetPMSType(Trim(objSysConfig.GetConfig("PMSName")))
                    PMSVersion = objSysConfig.GetConfig("PMSVersion")
                    CouponVersion = objSysConfig.GetConfig("CouponVersion")
                Catch ex As Exception

                End Try
                ' currency = objSysConfig.GetConfig("Currency")


                Dim dmax As Integer = 6

                Dim dm1 As Integer = 6


                Dim tpid As Long = 0

                Try
                    tpid = planId

                    Try
                        objlog.write2LogFile("mytest", "Planid" & planId)
                    Catch ex As Exception

                    End Try


                    Dim userCrdential1 As New UserContext(roomno, pwd, planId, PMSName, PMSVersion, HttpContext.Current.Request)
                    userCrdential1.item("usertype") = EUSERTYPE.ROOM
                    userCrdential1.item("accesstype") = 0

                    Dim aaa As AAAService
                    aaa = AAAService.getInstance

                    tpid = aaa.GetoldPlanid(userCrdential1)

                Catch ex As Exception

                End Try


                Try
                    objlog.write2LogFile("mytest", "TPlanid" & tpid)
                Catch ex As Exception

                End Try


                Try
                    If tpid = 102 Or tpid = 103 Then
                        dmax = 6
                        dm1 = 6
                    Else
                        dmax = 6
                        dm1 = 2

                    End If
                Catch ex As Exception

                End Try




                Try
                    If Maccount(pwd, roomno, commonFun.DecrptQueryString("MA", qs)) >= 500 Then


                        Try
                            Dim odb As DbaseService
                            odb = DbaseService.getInstance


                            sql_query = "INSERT INTO Loginfails	(FailUserId, FailPassword, FailMAC, FailUserType, FailPlanId,FailRemarks,FailTime) VALUES ('" & roomno & "', '" & Replace(pwd, "'", "''") & "', '" & commonFun.DecrptQueryString("MA", qs) & "', 0, " & planId & ", '" & "policy is to allow use of only " & dmax & "  wired/wireless connections" & "', '" & Now & "') "
                            odb.insertUpdateDelete(sql_query)

                        Catch ex As Exception

                        End Try
                        Response.Redirect("UserErrorNew.aspx?" & url & "&Msg=Dear guest  Our current policy is to allow use of only " & dmax & " wired/wireless connections from a room." & "&rm=" & roomno & "&ln=" & pwd & "&pid=" & dm1)

                        'ObjElog.write2LogFile("Mobile_" & commonFun.DecrptQueryString("MA", qs), "Max device")
                    End If

                Catch ex As Exception

                End Try


                Try
                    'ObjElog.write2LogFile("Mifi", " Plan selected----> " & planId & "pwd=" & pwd & "rm" & roomno & "pms name" & PMSName & "ver" & PMSVersion)
                Catch ex As Exception

                End Try


                Dim userCrdential As New UserContext(roomno, pwd, planId, PMSName, PMSVersion, HttpContext.Current.Request)
                userCrdential.item("usertype") = EUSERTYPE.ROOM
                userCrdential.item("accesstype") = 0

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




                ' wireless login









                userCrdential.item("logintype") = LOGINTYPE.NEWLOGIN

                Try
                    userCrdential.item("grcid") = ""
                Catch ex As Exception

                End Try



                '--------------------------- START avoid Parallel Login Process ---------------
                userCrdential.item("UsrIP1") = roomno
                userCrdential.item("UsrIP2") = pwd


                Try

                    If roomno = "" Or pwd = "" Then
                        Response.Redirect("UserError.aspx?" & GetRedirectQS() & "&Msg=Dear Guest, Please Enable cookie")
                    End If

                Catch ex As Exception

                End Try



                objDbase = DbaseService.getInstance


                Dim MAC As String = commonFun.DecrptQueryString("MA", qs)

                Try


                    'planId = Session("splan")



                Catch ex As Exception

                End Try

                userCrdential.wiredlessMac = MAC

                Try
                    Dim objin As LogInOutService
                    objin = LogInOutService.getInstance

                    userCrdential.wiredMac = objin.GetWiredMAC(userCrdential.wiredlessMac, userCrdential.item("UsrIP1"))

                Catch ex As Exception

                End Try


                Try
                    userCrdential.item("NOD") = noofdays
                Catch ex As Exception

                End Try


                ' RefResultset.Close()
                login(userCrdential)
                '--------------------------- END avoid Parallel Login Process ---------------


            End If
        Catch ex As Exception

        End Try



    End Sub

    Private Function GetRedirectQS()
        Dim redirectQS As String = ""
        redirectQS = "encry=" & Request.QueryString("encry")
        Return redirectQS
    End Function

   

End Class