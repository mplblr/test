Imports Security
Imports PMSPkgSql

Partial Public Class Newuser
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
    Protected Sub lk1(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton2.Click, LinkButton3.Click, LinkButton4.Click, LinkButton5.Click


        Try
            Dim commonFun As PMSCommonFun
            commonFun = PMSCommonFun.getInstance
            url = commonFun.BrowserQueryString(Request)
            Response.Redirect("terms.aspx?" & GetRedirectQS() & "&bk=a")

        Catch ex As Exception

        End Try




    End Sub
    Protected Sub lk11(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton3.Click

        Try
            Dim commonFun As PMSCommonFun
            commonFun = PMSCommonFun.getInstance
            url = commonFun.BrowserQueryString(Request)
            Response.Redirect("terms.aspx?" & GetRedirectQS() & "&bk=a")

        Catch ex As Exception

        End Try




    End Sub





    Protected Sub lk(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton1.Click

        Try
            Dim commonFun As PMSCommonFun
            commonFun = PMSCommonFun.getInstance
            url = commonFun.BrowserQueryString(Request)
            Response.Redirect("terms.aspx?" & GetRedirectQS() & "&bk=a")

        Catch ex As Exception

        End Try




    End Sub

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

        Button1.Text = GetLocalResourceObject("m10").ToString()
        Label1.Text = GetLocalResourceObject("m8").ToString()
        Label2.Text = GetLocalResourceObject("m9").ToString()


        Button2.Text = GetLocalResourceObject("m10").ToString()

        Button3.Text = GetLocalResourceObject("m10").ToString()

        Button5.Text = GetLocalResourceObject("m10").ToString()
        Button4.Text = GetLocalResourceObject("m10").ToString()


        Label4.Text = GetLocalResourceObject("p1").ToString()
        'Label5.Text = GetLocalResourceObject("p2").ToString()


        Label11.Text = GetLocalResourceObject("m11").ToString()
        Label5.Text = GetLocalResourceObject("m11").ToString()
       
        Label50.Text = GetLocalResourceObject("t1").ToString()
        Button6.Text = GetLocalResourceObject("t2").ToString()
        Label14.Text = GetLocalResourceObject("t3").ToString()
    End Sub
  
    'PAGE LOAD EVENT FUNCTION


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



        



        If Not IsPostBack() Then

            'p1.Visible = False
            'p2.Visible = False
            'p3.Visible = False
            'p4.Visible = False
            'p5.Visible = False
            'p6.Visible = False


            If MAC = "" Then Response.Redirect("UserError.aspx?Msg=Access Denied.")

            NSEID = commonFun.DecrptQueryString("UI", qs)
            Dim chknseid As String = objSysConfig.GetNasConfig(encrypt.GetEncryptedData(NSEID))
            If chknseid = "" Then
                Response.Redirect("UserError.aspx?Msg=Your Nomadix is not registered to work with this billing system")
            End If
            nasip = chknseid

            Try
                If commonFun.DecrptQueryString("PORT", qs) = "4000" Then

                    Try
                        '  a10.Visible = False
                    Catch ex As Exception

                    End Try
                    c1.Visible = False
                    c2.Visible = False
                    c3.Visible = False
                    s2.Visible = False
                    s3.Visible = False
                    Div1.Visible = False
                    Div2.Visible = False
                    Div3.Visible = False
                    Div4.Visible = False

                ElseIf commonFun.DecrptQueryString("PORT", qs) = "4001" Then
                    Try
                        '  a10.Visible = False
                    Catch ex As Exception

                    End Try
                    c1.Visible = False
                    c2.Visible = False
                    c3.Visible = False


                    s2.Visible = False
                    s3.Visible = False
                    s4.Visible = False
                    s5.Visible = False
                    Div3.Visible = False
                    Div4.Visible = False

                ElseIf commonFun.DecrptQueryString("PORT", qs) = "700" Then
                    c1.Visible = False
                    c2.Visible = False
                    c3.Visible = False
                    s1.Visible = False
                    k1.Visible = False

                    s2.Visible = False
                    s3.Visible = False
                    s4.Visible = False
                    s5.Visible = False
                    Div3.Visible = False
                    Div4.Visible = False
                    p3.Visible = False
                    p4.Visible = False
                ElseIf commonFun.DecrptQueryString("RN", qs) = "3090" Or commonFun.DecrptQueryString("PORT", qs) = "3090" Then

                    c1.Visible = False
                    c2.Visible = False
                    c3.Visible = False


                    a11.Visible = False

                    s1.Visible = False
                    k1.Visible = False

                    s4.Visible = False
                    s5.Visible = False
                    Div1.Visible = False
                    Dim img1 As ImageButton = DirectCast(Master.FindControl("LinkButton9"), ImageButton)
                    img1.ImageUrl = GetLocalResourceObject("C2").ToString()

                    img1.Enabled = False

                    Div1.Visible = False

                    Div2.Visible = False

                    Div3.Visible = False
                    Div4.Visible = False

                ElseIf commonFun.DecrptQueryString("RN", qs) = "11" Or commonFun.DecrptQueryString("PORT", qs) = "11" Then

                    c1.Visible = False
                    c2.Visible = False
                    c3.Visible = False


                    a11.Visible = False

                    s1.Visible = False
                    k1.Visible = False
                    s3.Visible = False

                    s2.Visible = False
                    Dim img1 As ImageButton = DirectCast(Master.FindControl("LinkButton9"), ImageButton)
                    img1.ImageUrl = GetLocalResourceObject("C2").ToString()

                    img1.Enabled = False
                    Div1.Visible = False

                    Div2.Visible = False
                    Div3.Visible = False
                    Div4.Visible = False
                End If
            Catch ex As Exception

            End Try



            Try

                RN = commonFun.DecrptQueryString("RN", qs)
                RN = ""

              

                Dim port As Integer = 0
                Dim room As Integer = 0

                Try
                    port = commonFun.DecrptQueryString("PORT", qs)
                Catch ex As Exception

                End Try

                Try
                    room = commonFun.DecrptQueryString("RN", qs)
                Catch ex As Exception

                End Try




                If (port >= 101 And port <= 692) Or (room >= 101 And room <= 692) Then

                    Try
                        objlog.write2LogFile("nwe user wired", "Room=" & room & "port=" & port)
                    Catch ex As Exception

                    End Try


                    Dim froom As Integer = 0

                    If port >= 101 And port <= 692 Then
                        froom = port
                    ElseIf room >= 101 And room <= 692 Then
                        froom = room
                    End If
                    Try
                        objlog.write2LogFile("nwe user wired", "finalRoom=" & froom)
                    Catch ex As Exception

                    End Try

                    Dim intValid1 As String = ""

                    Try
                        Dim objguestnew1 As GuestService
                        objguestnew1 = GuestService.getInstance
                        intValid1 = objguestnew1.getGuestLastName(froom)
                        Try
                            objlog.write2LogFile("nwe user wired", "Guestname" & intValid1)
                        Catch ex As Exception

                        End Try

                        If intValid1 = "-1" Or intValid1 = "" Then
                            Response.Redirect("UserError.aspx?" & GetRedirectQS() & "&Msg=" & "Dear Guest, room vacant." & "&findurl=mifilogin" & "&ct=" & Page.UICulture)
                        Else
                            wired(froom, intValid1)
                        End If


                    Catch ex As Exception

                    End Try
                End If

            Catch ex As Exception

            End Try


        End If

    End Sub

    Private Sub login2(ByRef userCrdential As UserContext, ByVal otp As String)
        Dim cc As String = ""
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
            objlog.write2LogFile("Device_" & MAC, "Agent=" & uagent)

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
            ' objdb.insertUpdateDelete("update MobileDetails set roomno='" & userCrdential.roomNo & "' , LastName='" & userCrdential.guestName & "', loginTime='" & ctime & "' where mac='" & MAC & "' and ( roomno is null or roomno ='' ) ")



        Catch ex As Exception

        End Try






        AAA = AAAService.getInstance
        Try
            output = AAA.AAA(userCrdential)
        Catch ex As Exception

        End Try
        If UCase(output) = "SUCCESS" Then


            Try
                insertLog("", otp, TextBox5.Text, userCrdential.userId, "", userCrdential.userId, userCrdential.machineId)
            Catch ex As Exception

            End Try


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


    Private Sub login(ByRef userCrdential As UserContext)
        Dim cc As String = ""
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
            objlog.write2LogFile("Device_" & MAC, "Agent=" & uagent)

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
            ' objdb.insertUpdateDelete("update MobileDetails set roomno='" & userCrdential.roomNo & "' , LastName='" & userCrdential.guestName & "', loginTime='" & ctime & "' where mac='" & MAC & "' and ( roomno is null or roomno ='' ) ")



        Catch ex As Exception

        End Try


        



        AAA = AAAService.getInstance
        Try
            output = AAA.AAA(userCrdential)
        Catch ex As Exception

        End Try
        If UCase(output) = "SUCCESS" Then


            Try
                updateemail(userCrdential.userId, TextBox6.Text)
            Catch ex As Exception

            End Try


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


        Try

            Dim orgName As String = ""

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


            '-----------------------------------------------------------------
            'objlog.write2LogFile("TimingAnalysis", DateTime.Now.ToString() & ": " & "Button just clicked...")
            '-----------------------------------------------------------------


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
                        Response.Redirect("UserError.aspx?" & GetRedirectQS() & "&Msg=" & "Dear Guest, room vacant" & "&findurl=mifilogin" & "&ct=" & Page.UICulture)
                    Catch ex As Exception
                        'objlog.write2LogFile("MACtrack-" & userCrdential.machineId & "Error_welcome", ex.Message)
                    End Try

                End If

            Catch ex As Exception

            End Try


            Try
                If Not IsNumeric(room) Then
                    Response.Redirect("UserError.aspx?" & GetRedirectQS() & "&Msg=" & "Dear Guest, Please enter Valid Room Number." & "&findurl=mifilogin" & "&ct=" & Page.UICulture)

                End If
            Catch ex As Exception

            End Try


           









            roomno = room

            pwd = lastname


            ''-----------------------------------------------------------------
            'objlog.write2LogFile("TimingAnalysis", DateTime.Now.ToString() & ": " & String.Format("Room No: {0}, Last Name: {1}", roomno, pwd))
            ''-----------------------------------------------------------------


            'Try
            '    Dim intValid As Integer = 0

            '    Dim objguestnew As GuestService
            '    objguestnew = GuestService.getInstance
            '    intValid = objguestnew.getGuestStatus(roomno, pwd)

            '    '-----------------------------------------------------------------
            '    objlog.write2LogFile("TimingAnalysis", DateTime.Now.ToString() & ": " & String.Format("Valid Guest: intValid = {0}", intValid.ToString()))
            '    '-----------------------------------------------------------------

            '    If intValid = 0 Then
            '        ' Response.Redirect("UserError.aspx?" & GetRedirectQS() & "&Msg=" & "Dear Guest, You have entered an invalid room number, for further assistance please call 0 / 1510." & "&findurl=mifilogin" & "&ct=" & Page.UICulture)


            '        Try
            '            Dim loginpwd As String = ""

            '            loginpwd = GetLoginPwd(roomno, pwd)
            '            loginpwd = loginpwd.Trim

            '            '-----------------------------------------------------------------
            '            objlog.write2LogFile("TimingAnalysis", DateTime.Now.ToString() & ": " & String.Format("GetLoginPwd(roomno, pwd): {0}", loginpwd))
            '            '-----------------------------------------------------------------

            '            Try
            '                objlog.write2LogFile("NewUser" & "_" & roomno, "Guest not found searc ing loginpwd " & loginpwd & "guestEntered" & pwd)
            '            Catch ex As Exception

            '            End Try



            '            If loginpwd = "-1" Then
            '                Try
            '                    loginFail(roomno, pwd, mac, "ErrorCode: 6 |  ErrorMsg: Invalid RoomNo/LastName.")
            '                Catch ex As Exception

            '                End Try

            '                Response.Redirect("UserError.aspx?" & GetRedirectQS() & "&Msg=" & "Dear Guest, You have entered an invalid room number, for further assistance please call 0 / 1510." & "&findurl=mifilogin" & "&ct=" & Page.UICulture)
            '                Try
            '                    objlog.write2LogFile("NewUser" & "_" & roomno, "loginpwd not found  loginpwd " & loginpwd & "guestEntered" & pwd)
            '                Catch ex As Exception

            '                End Try






            '            Else

            '                Try
            '                    objlog.write2LogFile("NewUser" & "_" & roomno, "loginpwd  found  Actual guestName " & loginpwd & "guestEntered Login Password" & pwd)
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

            '        '-----------------------------------------------------------------
            '        objlog.write2LogFile("TimingAnalysis", DateTime.Now.ToString() & ": " & String.Format("GetLoginPwd1(roomno, pwd): {0}", loginpwd))
            '        '-----------------------------------------------------------------



            '        If loginpwd <> "-1" Then

            '            Try
            '                objlog.write2LogFile("NewUser", "Guest recourd found searc ing loginpwd " & loginpwd & "guestEntered" & pwd)
            '            Catch ex As Exception

            '            End Try

            '            Try
            '                ' loginFail(roomno, pwd, mac, "ErrorCode: 6 |  ErrorMsg: Invalid LoginPassword.")
            '            Catch ex As Exception

            '            End Try

            '            'Response.Redirect("UserError.aspx?" & GetRedirectQS() & "&Msg=" & "Dear Guest, You have entered an invalid room number, for further assistance please call 0 / 1510." & "&findurl=mifilogin" & "&ct=" & Page.UICulture)





            '        Else

            '            Try
            '                objlog.write2LogFile("NewUser", "Guest recourd found searc ing loginpwd " & loginpwd & "guestEntered" & pwd)
            '            Catch ex As Exception

            '            End Try
            '        End If






            '    End If
            'Catch ex As Exception

            'End Try

            'Try
            '    Dim uagent As String = Request.ServerVariables("HTTP_USER_AGENT").Trim().ToLower()
            '    objlog.write2LogFile("Device_" & mac, "Agent=" & uagent)

            '    Dim objdb As DbaseService
            '    objdb = DbaseService.getInstance
            '    objdb.insertUpdateDelete("delete from MobileDetails where mac='" & mac & "'")

            '    If uagent.Contains("ipad") Then
            '        objdb.insertUpdateDelete("insert into MobileDetails(DeviceDetails,mac, devicetype) values('" & uagent & "', '" & mac & "','" & "IPad" & "')")


            '    Else

            '        objdb.insertUpdateDelete("insert into MobileDetails(DeviceDetails,mac, devicetype) values('" & uagent & "','" & mac & "','" & "Laptop" & "')")


            '    End If


            '    Dim ctime As DateTime
            '    ctime = Now

            '    objdb.insertUpdateDelete("update MobileDetails set roomno='" & roomno & "' , LastName='" & pwd & "', loginTime='" & ctime & "' where mac='" & mac & "' ")




            'Catch ex As Exception

            'End Try



            Try
                Session("roomno") = roomno
                Session("pwd") = pwd
            Catch ex As Exception

            End Try
            Dim logi As Long
            logi = 0
            Try

                '-----------------------------------------------------------------
                objlog.write2LogFile("TimingAnalysis", DateTime.Now.ToString() & ": " & String.Format("Before login_Click(roomno, pwd)"))
                '-----------------------------------------------------------------

                logi = login_Click(roomno, pwd)  '------'remaining time

                '-----------------------------------------------------------------
                objlog.write2LogFile("TimingAnalysis", DateTime.Now.ToString() & ": " & String.Format("After login_Click(roomno, pwd)"))
                '-----------------------------------------------------------------

            Catch ex As Exception
                objlog.write2LogFile("login", "err" & ex.Message)
            End Try
            If logi > 0 Then






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

                '-----------------------------------------------------------------
                'objlog.write2LogFile("TimingAnalysis", DateTime.Now.ToString() & ": " & String.Format("Before redirecting mifilogin.aspx"))
                '-----------------------------------------------------------------

                Try
                    Response.Redirect("mifilogin.aspx?" & GetRedirectQS() & "&plan=" & planId & "&rm=" & roomno & "&ln=" & pwd & "&ct=" & Page.UICulture)
                Catch ex As Exception

                End Try

            Else

                '-----------------------------------------------------------------
                objlog.write2LogFile("TimingAnalysis", DateTime.Now.ToString() & ": " & String.Format("Before redirecting PlanSel.aspx"))
                '-----------------------------------------------------------------

                Try



                    Response.Redirect("PlanSel.aspx?" & GetRedirectQS() & "&plan=" & planId & "&rm=" & roomno & "&ln=" & pwd & "&ct=" & Page.UICulture)
                Catch ex As Exception

                End Try
            End If


        Catch ex As Exception

        End Try







    End Sub


    


    Protected Sub LoginButton_clic(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click


        Try

            Dim orgName As String = ""

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


            '-----------------------------------------------------------------
            objlog.write2LogFile("TimingAnalysis", DateTime.Now.ToString() & ": " & "Button just clicked...")
            '-----------------------------------------------------------------


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

                If txtlastname.Text = "" Or txtRoomNo.Text = "" Then

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
                If Not IsNumeric(txtRoomNo.Text.Trim) Then
                    Response.Redirect("UserError.aspx?" & GetRedirectQS() & "&Msg=" & "Dear Guest, Please enter Valid Room Number." & "&findurl=mifilogin" & "&ct=" & Page.UICulture)

                End If
            Catch ex As Exception

            End Try


            Try
                If Integer.Parse(txtRoomNo.Text.Trim()) >= 9000 And Integer.Parse(txtRoomNo.Text.Trim()) <= 9999 Then
                    Response.Redirect("UserError.aspx?" & GetRedirectQS() & "&Msg=" & "Dear Guest, Please enter Valid Room Number." & "&findurl=mifilogin" & "&ct=" & Page.UICulture)

                End If
            Catch ex As Exception

            End Try






            If Not Validator.ValidateName(txtlastname.Text) Then
                ' Response.Redirect("UserError.aspx?" & GetRedirectQS() & "&Msg=" & "Dear Guest, Please enter Valid Last Name." & "&findurl=mifilogin" & "&ct=" & Page.UICulture)

            End If









            roomno = txtRoomNo.Text.Trim()

            pwd = txtlastname.Text.Trim()


            '-----------------------------------------------------------------
            objlog.write2LogFile("TimingAnalysis", DateTime.Now.ToString() & ": " & String.Format("Room No: {0}, Last Name: {1}", roomno, pwd))
            '-----------------------------------------------------------------


            Try
                Dim intValid As Integer = 0

                Dim objguestnew As GuestService
                objguestnew = GuestService.getInstance
                intValid = objguestnew.getGuestStatus(roomno, pwd)

                '-----------------------------------------------------------------
                objlog.write2LogFile("TimingAnalysis", DateTime.Now.ToString() & ": " & String.Format("Valid Guest: intValid = {0}", intValid.ToString()))
                '-----------------------------------------------------------------

                If intValid = 0 Then
                    ' Response.Redirect("UserError.aspx?" & GetRedirectQS() & "&Msg=" & "Dear Guest, You have entered an invalid room number, for further assistance please call 0 / 1510." & "&findurl=mifilogin" & "&ct=" & Page.UICulture)


                    Try
                        Dim loginpwd As String = ""

                        loginpwd = GetLoginPwd(roomno, pwd)
                        loginpwd = loginpwd.Trim

                        '-----------------------------------------------------------------
                        objlog.write2LogFile("TimingAnalysis", DateTime.Now.ToString() & ": " & String.Format("GetLoginPwd(roomno, pwd): {0}", loginpwd))
                        '-----------------------------------------------------------------

                        Try
                            objlog.write2LogFile("NewUser" & "_" & roomno, "Guest not found searc ing loginpwd " & loginpwd & "guestEntered" & pwd)
                        Catch ex As Exception

                        End Try



                        If loginpwd = "-1" Then
                            Try
                                loginFail(roomno, pwd, mac, "ErrorCode: 6 |  ErrorMsg: Invalid RoomNo/LastName.")
                            Catch ex As Exception

                            End Try

                            Response.Redirect("UserError.aspx?" & GetRedirectQS() & "&Msg=" & "Dear Guest, You have entered an invalid room number, for further assistance please call 0 / 1510." & "&findurl=mifilogin" & "&ct=" & Page.UICulture)
                            Try
                                objlog.write2LogFile("NewUser" & "_" & roomno, "loginpwd not found  loginpwd " & loginpwd & "guestEntered" & pwd)
                            Catch ex As Exception

                            End Try






                        Else

                            Try
                                objlog.write2LogFile("NewUser" & "_" & roomno, "loginpwd  found  Actual guestName " & loginpwd & "guestEntered Login Password" & pwd)
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

                    '-----------------------------------------------------------------
                    objlog.write2LogFile("TimingAnalysis", DateTime.Now.ToString() & ": " & String.Format("GetLoginPwd1(roomno, pwd): {0}", loginpwd))
                    '-----------------------------------------------------------------



                    If loginpwd <> "-1" Then

                        Try
                            objlog.write2LogFile("NewUser", "Guest recourd found searc ing loginpwd " & loginpwd & "guestEntered" & pwd)
                        Catch ex As Exception

                        End Try

                        Try
                            ' loginFail(roomno, pwd, mac, "ErrorCode: 6 |  ErrorMsg: Invalid LoginPassword.")
                        Catch ex As Exception

                        End Try

                        'Response.Redirect("UserError.aspx?" & GetRedirectQS() & "&Msg=" & "Dear Guest, You have entered an invalid room number, for further assistance please call 0 / 1510." & "&findurl=mifilogin" & "&ct=" & Page.UICulture)





                    Else

                        Try
                            objlog.write2LogFile("NewUser", "Guest recourd found searc ing loginpwd " & loginpwd & "guestEntered" & pwd)
                        Catch ex As Exception

                        End Try
                    End If






                End If
            Catch ex As Exception

            End Try

            'Try
            '    Dim uagent As String = Request.ServerVariables("HTTP_USER_AGENT").Trim().ToLower()
            '    objlog.write2LogFile("Device_" & mac, "Agent=" & uagent)

            '    Dim objdb As DbaseService
            '    objdb = DbaseService.getInstance
            '    objdb.insertUpdateDelete("delete from MobileDetails where mac='" & mac & "'")

            '    If uagent.Contains("ipad") Then
            '        objdb.insertUpdateDelete("insert into MobileDetails(DeviceDetails,mac, devicetype) values('" & uagent & "', '" & mac & "','" & "IPad" & "')")


            '    Else

            '        objdb.insertUpdateDelete("insert into MobileDetails(DeviceDetails,mac, devicetype) values('" & uagent & "','" & mac & "','" & "Laptop" & "')")


            '    End If


            '    Dim ctime As DateTime
            '    ctime = Now

            '    objdb.insertUpdateDelete("update MobileDetails set roomno='" & roomno & "' , LastName='" & pwd & "', loginTime='" & ctime & "' where mac='" & mac & "' ")




            'Catch ex As Exception

            'End Try



            Try
                Session("roomno") = roomno
                Session("pwd") = pwd
            Catch ex As Exception

            End Try
            Dim logi As Long
            logi = 0
            Try

                '-----------------------------------------------------------------
                objlog.write2LogFile("TimingAnalysis", DateTime.Now.ToString() & ": " & String.Format("Before login_Click(roomno, pwd)"))
                '-----------------------------------------------------------------

                logi = login_Click(roomno, pwd)  '------'remaining time

                '-----------------------------------------------------------------
                objlog.write2LogFile("TimingAnalysis", DateTime.Now.ToString() & ": " & String.Format("After login_Click(roomno, pwd)"))
                '-----------------------------------------------------------------

            Catch ex As Exception
                objlog.write2LogFile("login", "err" & ex.Message)
            End Try
            If logi > 0 Then






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

                '-----------------------------------------------------------------
                objlog.write2LogFile("TimingAnalysis", DateTime.Now.ToString() & ": " & String.Format("Before redirecting mifilogin.aspx"))
                '-----------------------------------------------------------------

                Dim res As String = ""

                Try
                    res = getpwd(roomno, pwd)

                Catch ex As Exception

                End Try
               
                If res = "-1" Or res = "" Then
                    Try
                        Response.Redirect("changepwd.aspx?" & GetRedirectQS() & "&plan=" & planId & "&rm=" & roomno & "&ln=" & pwd & "&ct=" & Page.UICulture)
                    Catch ex As Exception

                End Try

                Else
                    Try
                        Response.Redirect("pre_exist1.aspx?" & GetRedirectQS() & "&plan=" & planId & "&rm=" & roomno & "&ln=" & pwd & "&ct=" & Page.UICulture)
                    Catch ex As Exception

                    End Try

                End If





                

            Else

                '-----------------------------------------------------------------
                objlog.write2LogFile("TimingAnalysis", DateTime.Now.ToString() & ": " & String.Format("Before redirecting PlanSel.aspx"))
                '-----------------------------------------------------------------

                Try



                    Dim res As String = ""

                    res = getpwd(roomno, pwd)

                    If res = "-1" Or res = "" Then
                        Try
                            Response.Redirect("changepwd.aspx?" & GetRedirectQS() & "&plan=" & planId & "&rm=" & roomno & "&ln=" & pwd & "&ct=" & Page.UICulture)
                        Catch ex As Exception

                        End Try

                    Else
                        Try
                            Response.Redirect("pre_exist1.aspx?" & GetRedirectQS() & "&plan=" & planId & "&rm=" & roomno & "&ln=" & pwd & "&ct=" & Page.UICulture)
                        Catch ex As Exception

                        End Try

                    End If
                Catch ex As Exception

                End Try
            End If


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

    Protected Sub pk(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button3.Click


        Try






            Dim userCrdential As New UserContext(TextBox1.Text.Trim(), TextBox1.Text.Trim(), PMSName.UNKNOWN, CouponVersion, HttpContext.Current.Request)
            userCrdential.item("usertype") = EUSERTYPE.COUPON
            userCrdential.item("MaxAmountReached") = 0
            userCrdential.item("accesstype") = 0
            login(userCrdential)


        Catch ex As Exception

        End Try


    End Sub

    Private Function GenerateString() As String

        Dim xCharArray() As Char = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray
        Dim xNoArray() As Char = "0123456789".ToCharArray
        Dim xGenerator As System.Random = New System.Random()
        Dim xStr As String = String.Empty

        While xStr.Length < 4

            If xGenerator.Next(0, 2) = 0 Then
                xStr &= xCharArray(xGenerator.Next(0, xCharArray.Length))
            Else
                xStr &= xNoArray(xGenerator.Next(0, xNoArray.Length))
            End If

        End While

        Return xStr

    End Function



    Protected Sub otp(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button6.Click
      

        Dim objlog As LoggerService
        objlog = LoggerService.gtInstance

        Try
            Dim mob As String = ""
            mob = TextBox2.Text.Trim()


            If mob = "" Then

                Label10.Text = "Dear Guest,Please enter valid phone number"
                Return

            Else
               
                Label10.Text = ""
                Dim otp As String = ""

                Try
                    Dim objDbase As DbaseService
                    objDbase = DbaseService.getInstance
                    Try
                        'objDbase = objDbase.getInstance

                        Dim st As String = ""

                        st = "delete from coupons where CouponUserId='" & mob & "'"
                        objDbase.DsWithoutUpdate(st)
                    Catch ex As Exception

                    End Try

                    Try

                        Dim ctime As DateTime
                        ctime = DateTime.Now


                        Dim st As String = "insert into coupons (CouponUserId,CouponPassword,CouponPlanid,CouponCreatedTime,CouponCreatedBy,CouponCount,CouponType,CouponSerial) values('" & mob & "','" & mob & "',501,'" & ctime.ToString() & "','MSPL',100,0,'" & mob & "')"
                        objDbase.insertUpdateDelete(st)

                        Dim ds As DataSet
                        st = "select CouponId from coupons where CouponUserId='" & mob & "'"

                        ds = objDbase.DsWithoutUpdate(st)
                        '  otp = ds.Tables(0).Rows(0)(0).ToString()

                        Session("couponid") = ds.Tables(0).Rows(0)(0).ToString()

                        Try
                            otp = GenerateString()
                        Catch ex As Exception

                        End Try




                        Session("otp") = otp

                        objlog.write2LogFile("otp", otp & "couponid" & Session("couponid"))
                    Catch ex As Exception

                    End Try


                    Dim SMS As PushSMSService
                    SMS = PushSMSService.getInstance

                    Dim T_Struct As SMSResponse

                    Dim msg As String = ""

                    ' msg = "Dear Guest, Your internet access code is" & otp & ". Thank you."

                    msg = "Welcome to Hyatt Regency Mumbai, Your internet access code is" & " " & otp & " valid for 2 hours At Hyatt Regency Mumbai Stay Connected."

                    'msg = "Welcome to Hyatt Regency Mumbai & thankyou for dining with us.Your internet access code is" & otp & " valid for 2 hours # At Hyatt Regency Mumbai Stay Connected."

                    T_Struct = SMS.SendSMSToMobile_ValueFirst(mob, msg)

                    Try
                        objlog.write2LogFile("CouponDeatis", "status" & T_Struct.SMSSentStatus)
                        objlog.write2LogFile("CouponDeatis", "code" & T_Struct.SMSErrorCode)
                        objlog.write2LogFile("CouponDeatis", "DESC" & T_Struct.SMSErrorDesc)
                    Catch ex As Exception

                    End Try

                Catch ex As Exception

                End Try


            End If

        Catch ex As Exception

        End Try


    End Sub


    Protected Sub otplogin(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button4.Click


        Try
            'p1.Visible = True

            'p2.Visible = True

            'p3.Visible = True

            'p4.Visible = True

            'p5.Visible = True





            'Dim userCrdential As New UserContext(TextBox3.Text.Trim(), TextBox3.Text.Trim(), PMSName.UNKNOWN, CouponVersion, HttpContext.Current.Request)
            'userCrdential.item("usertype") = EUSERTYPE.COUPON
            'userCrdential.item("MaxAmountReached") = 0
            'userCrdential.item("accesstype") = 0
            'login(userCrdential)


        Catch ex As Exception

        End Try

        Dim commonFun As PMSCommonFun

        Dim MAC As String
        commonFun = PMSCommonFun.getInstance
        url = commonFun.BrowserQueryString(Request)

        Dim qs As String = Request.QueryString("encry")
        MAC = commonFun.DecrptQueryString("MA", qs)

        Dim cid As String = ""

        cid = TextBox4.Text.Trim()


        If cid.Length = 4 Then


            If cid <> Session("otp") Then
                Label10.Text = "Please enter valid Access Code"

            Else

                Dim uid As String = ""

                Dim objlog As LoggerService
                objlog = LoggerService.gtInstance


                objlog.write2LogFile("OTPMAC", "mac=" & MAC & "otp entered" & cid & "couponid" & Session("couponid"))








                Dim objDbase As DbaseService
                objDbase = DbaseService.getInstance

                Try
                    Dim ds As DataSet
                    Dim st As String
                    st = "select CouponUserId from coupons where CouponId='" & Session("couponid") & "'"

                    ds = objDbase.DsWithoutUpdate(st)

                    


                    If ds.Tables(0).Rows.Count = 0 Then
                        Label10.Text = "Please enter valid Access Code"
                    Else
                        Label10.Text = ""

                        uid = ds.Tables(0).Rows(0)(0).ToString()

                        objlog.write2LogFile("OTPMAC", "mac= " & MAC & "otp entered " & cid & "userId " & uid & "session couponid" & Session("couponid"))


                        Try
                            st = "update coupons set otp='" & Session("otp") & "' where CouponId='" & Session("couponid") & "'"
                            objDbase.insertUpdateDelete(st)
                        Catch ex As Exception

                        End Try


                        Try
                            Dim userCrdential As New UserContext(uid, uid, PMSNAMES.UNKNOWN, CouponVersion, HttpContext.Current.Request)
                            userCrdential.item("usertype") = EUSERTYPE.COUPON
                            userCrdential.item("MaxAmountReached") = 0
                            userCrdential.item("accesstype") = 0
                            login2(userCrdential, cid)


                        Catch ex As Exception

                        End Try

                    End If



                Catch ex As Exception

                End Try

            End If





        Else
            Dim userCrdential As New UserContext(cid.Trim, cid.Trim, PMSName.UNKNOWN, CouponVersion, HttpContext.Current.Request)
            userCrdential.item("usertype") = EUSERTYPE.COUPON
            userCrdential.item("MaxAmountReached") = 0
            userCrdential.item("accesstype") = 0
            login(userCrdential)
        End If


      




    End Sub

    Public Sub insertLog(ByVal name As String, ByVal otp As String, ByVal email As String, ByVal accesscode As String, ByVal remarks As String, ByVal mobile As String, ByVal mac As String)

        Try
            Dim ob As DbaseService
            ob = DbaseService.getInstance

            Dim logintime As DateTime
            logintime = Now



            Dim st As String = "insert into Eventlog(otp,Accesscode,Name,mobile,logintime,email,remarks,mac) values('" & otp & "','" & accesscode & "','" & name & "','" & mobile & "',' " & logintime & "','" & email & "','" & remarks & "','" & mac & "'  )"
            ob.insertUpdateDelete(st)

        Catch ex As Exception

        End Try


    End Sub

    Protected Sub b1_click11(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button5.Click


        Try






            Dim userCrdential As New UserContext(TextBox3.Text.Trim(), TextBox3.Text.Trim(), PMSName.UNKNOWN, CouponVersion, HttpContext.Current.Request)
            userCrdential.item("usertype") = EUSERTYPE.COUPON
            userCrdential.item("MaxAmountReached") = 0
            userCrdential.item("accesstype") = 0
            login(userCrdential)


        Catch ex As Exception

        End Try


    End Sub
    Public Sub updateemail(ByVal cid As String, ByVal email As String)

        Try
            Dim ob As DbaseService
            ob = DbaseService.getInstance

            Dim logintime As DateTime
            logintime = Now



            Dim st As String = "Update coupons set email='" & email & "' where couponuserid='" & cid & "'"
            ob.insertUpdateDelete(st)
        Catch ex As Exception

        End Try


    End Sub

    Protected Sub b1_click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click


        Try
           





            Dim userCrdential As New UserContext(userid.Text.Trim(), userid.Text.Trim(), PMSName.UNKNOWN, CouponVersion, HttpContext.Current.Request)
            userCrdential.item("usertype") = EUSERTYPE.COUPON
            userCrdential.item("MaxAmountReached") = 0
            userCrdential.item("accesstype") = 0
            login(userCrdential)


        Catch ex As Exception

        End Try


    End Sub

    Private Function GetRedirectQS()

        Try
            Dim redirectQS As String = ""
            redirectQS = "encry=" & Request.QueryString("encry")
            Return redirectQS
        Catch ex As Exception

        End Try

        
    End Function


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


    Public Function getpwd(ByVal rno As String, ByVal name As String) As String

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

                sq = "select pwd from guest where GuestRoomNo='" & rno & "'  and Guestname='" & name.Replace("'", "''") & "' and Gueststatus='A' and pwd is not null order by guestid desc"

                result = db.DsWithoutUpdate(sq)

                'com.CommandText = sq

                'Microsense.CodeBase.DatabaseUtil.AddInputParameter(com, "@RM", DbType.String, rno)
                'Microsense.CodeBase.DatabaseUtil.AddInputParameter(com, "@PD", DbType.String, name)


                'result = Microsense.CodeBase.DatabaseUtil.ExecuteSelect(com)


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