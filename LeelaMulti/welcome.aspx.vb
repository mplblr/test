Imports security
Imports PMSPkgSql



Partial Public Class welcome
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
    Protected WithEvents rdoutype As System.Web.UI.WebControls.RadioButtonList
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



        Dim s1 As HtmlControl = DirectCast(Master.FindControl("s1"), HtmlControl)
        Dim s2 As HtmlControl = DirectCast(Master.FindControl("s2"), HtmlControl)
        's1.innerHTML= GetLocalResourceObject("m12").ToString();
        's2.innerHTML = GetLocalResourceObject("m12").ToString();
        's1.i

    End Sub

    Public Function Maccount(ByVal guestname As String, ByVal roomno As String, ByVal mac As String) As Integer

        Dim ind As Integer = 0
        Dim objDbase As DbaseService
        Dim objlog As LoggerService
        objlog = LoggerService.gtInstance
        Dim ds As DataSet
        Try
            objDbase = objDbase.getInstance
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
            objlog.write2LogFile("maccount", "no of wireless mac=" & ind & "QUERY" & sql_query)
        Catch ex As Exception

        End Try

        Return ind


    End Function
    'PAGE LOAD EVENT FUNCTION


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        '*****************************LOCAL VARIABLE DECLARATION STARTS HERE**************************

        Dim objSysConfig As New CSysConfig

        Dim objPlan As New CPlan

        Dim userContext As UserContext
        Dim userCredential As UserCredential
        Dim RN As String = ""
        Dim grcid As String = ""
        Dim objlog As LoggerService
        objlog = LoggerService.gtInstance()
        Dim objGuestSrv As GuestService

        Dim NSEID As String
        Dim encrypt As New Datasealing
        Dim getplanid As String
        Dim MAC As String = ""
        Dim qs As String = Request.QueryString("encry")
        Dim commonFun As PMSCommonFun
        commonFun = PMSCommonFun.getInstance
        url = commonFun.BrowserQueryString(Request)
        MAC = commonFun.DecrptQueryString("MA", qs)
        RN = commonFun.DecrptQueryString("RN", qs)
        RN = ""
       

        'Try
        '    If commonFun.DecrptQueryString("RN", qs) = "102" Or commonFun.DecrptQueryString("PORT", qs) = "102" Then
        '        Dim img1 As ImageButton = DirectCast(Master.FindControl("LinkButton9"), ImageButton)
        '        img1.ImageUrl = GetLocalResourceObject("C2").ToString()
        '        img1.Enabled = False
        '    End If
        'Catch ex As Exception

        'End Try


        Dim objloginout As LogInOutService
        objloginout = LogInOutService.getInstance
        Dim isMac2 As Integer = 0
        '*****************************LOCAL VARIABLE DECLARATION ENDS HERE**************************

        Try
            If MAC = "90FBA612B060" Or MAC = "90FBA610D023" Or MAC = "D8306262A910" Then

                isMac2 = 1

                objlog.write2LogFile("BCMAC___" & MAC, MAC)

            End If

        Catch ex As Exception

        End Try


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
            Dim uagent As String = ""

            Try
                uagent = Request.ServerVariables("HTTP_USER_AGENT").Trim().ToLower()
                If uagent.Contains("ipad") Then
                    accesstype = 3
                End If
            Catch ex As Exception

            End Try
        Catch ex As Exception

        End Try



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
                If commonFun.DecrptQueryString("RN", qs) = "3090" Or commonFun.DecrptQueryString("PORT", qs) = "11" Then
                    mm.Visible = False
                End If
            Catch ex As Exception

            End Try



            'THIS HOTEL  DO NOT SUPPORT PORT MAPPING SO RN IS SET TO BLANK BY DEFAULT.



            If RN = "" Then

                Try




                    '######################## Begin Cookies ############################
                  


                    Dim longinbymac As String = ""
                    longinbymac = MAC
                    objGuestSrv = GuestService.getInstance
                    grcid = objGuestSrv.WithoutCookiesGetCredential_Fidalio(MAC)


                    Try
                        objlog.write2LogFile("MAC-" & MAC, "================== Cookie Credetial  Not Found:  Login By MAC <WiredLess>  ==================" & vbCrLf & vbCrLf _
                         & Now() & "--From WireLess MAC" & vbCrLf _
                         & "----------------------  Mac Address: " & MAC & vbCrLf _
                         & "----------------------  GRCID: " & grcid & vbCrLf _
                          & "----------------------------------------------------------------------------" & vbCrLf)



                    Catch ex As Exception

                    End Try



                    If grcid <> "" Then

                        If objGuestSrv.isValid(MAC) Then
                            userCredential = objGuestSrv.getGuestInfo(grcid)
                            getplanid = objGuestSrv.GetPlanid(grcid)

                            userContext = New UserContext(userCredential, PMSName, PMSVersion, getplanid, HttpContext.Current.Request)
                            userContext.item("usertype") = EUSERTYPE.ROOM
                            userContext.item("logintype") = LOGINTYPE.RELOGIN
                            userContext.item("accesstype") = accesstype   ' wired or wireless login
                            Try
                                userContext.item("grcid") = grcid
                            Catch ex As Exception

                            End Try

                            Try
                                objlog.write2LogFile("MAC-" & MAC, "==================  Login BY MAC <WiredLess>  ==================" & vbCrLf & vbCrLf _
                                 & Now() & "--From WireLess MAC" & vbCrLf _
                                 & "----------------------  Mac Address: " & MAC & vbCrLf _
                                 & "----------------------  GRCID: " & grcid & vbCrLf _
                                 & "----------------------  Plan ID: " & getplanid & vbCrLf _
                                  & "----------------------------------------------------------------------------" & vbCrLf)


                                objlog.write2LogFile("room", "RM" & userCredential.usrId & "pwd" & userCredential.passwd)


                            Catch ex As Exception

                            End Try




                            Try
                                userContext.wiredlessMac = commonFun.DecrptQueryString("MA", qs)

                            Catch ex As Exception

                            End Try


                            Try


                                'objloginout.GetWiredlessMAC(wiredMac, userCredential.usrId)
                                userContext.wiredMac = objloginout.GetWiredMAC(userContext.wiredlessMac, userCredential.usrId)

                            Catch ex As Exception

                            End Try

                            Try
                                ' objlog.write2LogFile("MACTrack-" & MAC, "Login by mac Login function called")
                            Catch ex As Exception

                            End Try

                          

                            login(userContext)


                        Else
                            


                        End If

                    End If


                    MAC = commonFun.DecrptQueryString("MA", qs)
                    grcid = objGuestSrv.WithoutCookiesGetCredential(MAC)



                    If grcid <> "" Then
                        objlog.write2LogFile("LOginbymac", objGuestSrv.isValid(MAC))
                        If objGuestSrv.isValid(MAC) Then
                            userCredential = objGuestSrv.getCouponInfo(grcid)
                            getplanid = objGuestSrv.GetPlanidC(grcid)
                            userContext = New UserContext(userCredential, PMSName.UNKNOWN, CouponVersion, getplanid, HttpContext.Current.Request)
                            userContext.item("usertype") = EUSERTYPE.COUPON
                            userContext.item("logintype") = LOGINTYPE.RELOGIN
                            userContext.item("accesstype") = 0
                            Try
                                userContext.item("grcid") = grcid
                            Catch ex As Exception

                            End Try


                            Try
                                objlog.write2LogFile("MAC-" & MAC, "==================  Login By MAC <Coupon USER>    ==================" & vbCrLf & vbCrLf _
                                 & Now() & "--From WireLess MAC" & vbCrLf _
                                 & "----------------------  Mac Address: " & MAC & vbCrLf _
                                 & "----------------------  GRCID: " & grcid & vbCrLf)


                            Catch ex As Exception

                            End Try
                            If commonFun.DecrptQueryString("PORT", qs) = "700" Or commonFun.DecrptQueryString("PORT", qs) = "4001" Then



                            Else
                                login1(userContext)
                            End If


                        End If



                    End If



                    '  End If
                Catch ex As Exception

                End Try

            ElseIf RN <> "" Then
                Try

                    Try
                        objlog.write2LogFile("wired1", "rn=" & RN)
                    Catch ex As Exception

                    End Try


                Catch ex As Exception

                End Try
                Try
                    MAC = commonFun.DecrptQueryString("MA", qs)

                    If pgCookie.ReadCookie(Request) Then

                        If pgCookie.expiryTime > Now() And pgCookie.userType = EUSERTYPE.ROOM Then

                            '  Dim objlog As LoggerService
                            objlog = LoggerService.gtInstance
                            objGuestSrv = GuestService.getInstance
                            If pgCookie.grcId <> "" Then
                                userCredential = objGuestSrv.getGuestInfo(pgCookie.grcId)
                                getplanid = objGuestSrv.GetPlanid(pgCookie.grcId)
                                Dim wiredMac As String = ""
                                Dim wirelessMac As String = ""
                                Try
                                    wiredMac = pgCookie.wiredmac
                                    wirelessMac = pgCookie.wiredlessmac
                                Catch ex As Exception
                                End Try

                                If wiredMac = "" Then
                                    wiredMac = commonFun.DecrptQueryString("MA", qs)
                                End If
                                Try

                                    If wirelessMac = "" Then

                                        wirelessMac = objloginout.GetWiredlessMAC(wiredMac, userCredential.usrId)
                                    End If
                                Catch ex As Exception

                                End Try
                                userContext = New UserContext(userCredential, PMSName, PMSVersion, getplanid, wiredMac, wirelessMac, HttpContext.Current.Request)
                                userContext.item("usertype") = EUSERTYPE.ROOM
                                userContext.item("logintype") = LOGINTYPE.RELOGIN

                                If RN <> "" Then
                                    userContext.item("accesstype") = 1
                                Else
                                    userContext.item("accesstype") = 0
                                End If






                                Try
                                    userContext.item("grcid") = pgCookie.grcId
                                Catch ex As Exception

                                End Try
                                userContext.item("serviceaccess") = pgCookie.serviceAccess
                                userContext.item("loginbycookie") = True
                                objlog = LoggerService.gtInstance

                                Try
                                    If MAC = "90FBA612B060" Or MAC = "90FBA610D023" Or MAC = "D8306262A910" Then

                                        isMac2 = 1

                                        objlog.write2LogFile("BCMAC___" & MAC, MAC)

                                    End If

                                Catch ex As Exception

                                End Try


                                If isMac2 = 0 Then
                                    login1(userContext)
                                End If

                            Else

                                pgCookie.ResetCookie(HttpContext.Current.Response)
                                Response.Redirect("welcome.aspx?" & GetRedirectQS())
                            End If


                        ElseIf pgCookie.expiryTime > Now() And pgCookie.userType = EUSERTYPE.COUPON Then

                            Try
                                objGuestSrv = GuestService.getInstance
                                userCredential = objGuestSrv.getGuestInfo(pgCookie)
                                getplanid = objGuestSrv.GetPlanidC(pgCookie.grcId)
                                userContext = New UserContext(userCredential, PMSName.UNKNOWN, CouponVersion, getplanid, HttpContext.Current.Request)
                                userContext.item("usertype") = EUSERTYPE.COUPON
                                userContext.item("logintype") = LOGINTYPE.RELOGIN
                                If RN <> "" Then
                                    userContext.item("accesstype") = 1
                                Else
                                    userContext.item("accesstype") = 0
                                End If
                                userContext.item("loginbycookie") = True
                                login1(userContext)
                            Catch ex As Exception

                            End Try
                        Else

                            If pgCookie.userType = EUSERTYPE.COUPON Then
                                Response.Redirect("welcome.aspx" & url)
                            End If

                            Try
                                objGuestSrv = GuestService.getInstance
                                userCredential = objGuestSrv.getGuestInfo(pgCookie.grcId)
                                getplanid = objGuestSrv.GetPlanid(pgCookie.grcId)
                                userContext = New UserContext(userCredential, PMSName, PMSVersion, getplanid, HttpContext.Current.Request)
                                Dim objDbase As DbaseService
                                objDbase = DbaseService.getInstance
                                Dim wiredMac As String = ""
                                Dim wirelessMac As String = ""
                                Try
                                    wiredMac = pgCookie.wiredmac
                                    wirelessMac = pgCookie.wiredlessmac
                                Catch ex As Exception
                                End Try

                                If wiredMac = "" Then
                                    wiredMac = commonFun.DecrptQueryString("MA", qs)
                                End If
                                Try

                                    If wirelessMac = "" Then
                                        wirelessMac = objloginout.GetWiredlessMAC(wiredMac, userCredential.usrId)
                                    End If
                                Catch ex As Exception

                                End Try

                            Catch ex As Exception
                            End Try
                        End If
                        '######################## End Cookies ############################
                    Else
                        'Get Credential using the MAC##################################
                        ' Dim objlog As LoggerService
                        Dim longinbymac As String = ""
                        longinbymac = MAC
                        objlog = LoggerService.gtInstance()
                        objGuestSrv = GuestService.getInstance
                        Try
                            grcid = objGuestSrv.WithoutCookiesGetCredential_Fidalio(MAC)
                        Catch ex As Exception

                        End Try
                        Dim isval As Integer = 0
                        Try

                            If grcid <> "" Then
                                If objGuestSrv.isValid(MAC) Then
                                    isval = 0
                                Else
                                    isval = 1
                                End If

                            End If
                        Catch ex As Exception

                        End Try
                        If grcid = "" Or isval = 1 Then

                            Dim pmsRCode, pmsGuestname As String
                            Try
                                Dim ctime As DateTime
                                ctime = Now
                                Dim objdbase As DbaseService
                                objdbase = DbaseService.getInstance
                                pmsRCode = ""
                                pmsGuestname = ""

                                Try
                                    Dim sql_query As String = ""
                                    sql_query = "SELECT  distinct LoginMAC FROM ((LogDetails LEFT JOIN Bill ON LogDetails.LoginBillId = Bill.Billid) LEFT JOIN Guest ON Guest.Guestid = Bill.BillGrCId) " & _
                                          "WHERE (Bill.BillType = " & PMSBill.ROOM & _
                                                  "     AND    Guest.GuestStatus='A'  and   Guest.GuestRoomNo = '" & RN & "' AND LogDetails.LoginMAC<>'" & MAC & "' and  LogDetails.LoginExpTime > '" & ctime & "') "

                                    Dim ds As DataSet
                                    Dim ind As Integer = 0
                                    Try

                                        Try
                                            ds = objdbase.DsWithoutUpdate(sql_query)
                                        Catch ex As Exception

                                        End Try
                                        ind = ds.Tables(0).Rows.Count
                                        If ind = 1 Then
                                            longinbymac = ds.Tables(0).Rows(0)("LoginMAC")
                                            Try
                                                If longinbymac <> "" Then
                                                    Dim wm As String = ""
                                                    wm = objloginout.GetWiredMAC(longinbymac, RN)
                                                    If wm = "" Or wm = commonFun.DecrptQueryString("MA", qs) Then
                                                        grcid = objGuestSrv.WithoutCookiesGetCredential_Fidalio(longinbymac)
                                                    End If
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

                        If grcid <> "" Then
                            If objGuestSrv.isValid(longinbymac) Then
                                userCredential = objGuestSrv.getGuestInfo(grcid)
                                getplanid = objGuestSrv.GetPlanid(grcid)
                                userContext = New UserContext(userCredential, PMSName, PMSVersion, getplanid, HttpContext.Current.Request)
                                userContext.item("usertype") = EUSERTYPE.ROOM
                                userContext.item("logintype") = LOGINTYPE.RELOGIN
                                If RN <> "" Then
                                    userContext.item("accesstype") = 1
                                Else
                                    userContext.item("accesstype") = 0
                                End If ' wired or wireless login
                                Try
                                    userContext.wiredMac = commonFun.DecrptQueryString("MA", qs)
                                Catch ex As Exception

                                End Try
                                Try
                                    Try
                                        userContext.wiredlessMac = objloginout.GetWiredlessMAC(userContext.wiredMac, userCredential.usrId)

                                    Catch ex As Exception

                                    End Try
                                    If userContext.wiredlessMac = "" Then
                                        If commonFun.DecrptQueryString("MA", qs) <> longinbymac Then
                                            If longinbymac <> "" Then
                                                userContext.wiredlessMac = longinbymac
                                            End If
                                        End If
                                    End If
                                Catch ex As Exception

                                End Try


                                Try
                                    userContext.item("grcid") = grcid
                                Catch ex As Exception

                                End Try

                                Try
                                    If MAC = "90FBA612B060" Or MAC = "90FBA610D023" Or MAC = "D8306262A910" Then

                                        isMac2 = 1

                                        objlog.write2LogFile("BCMAC___" & MAC, MAC)

                                    End If

                                Catch ex As Exception

                                End Try

                                If isMac2 = 0 Then
                                    login1(userContext)
                                End If
                            Else
                                Try
                                    userCredential = objGuestSrv.getGuestInfo(grcid)
                                    getplanid = objGuestSrv.GetPlanid(grcid)
                                    userContext = New UserContext(userCredential, PMSName, PMSVersion, getplanid, HttpContext.Current.Request)

                                Catch ex As Exception
                                    'objlog.write2LogFile("processRN", ex.Message)
                                End Try

                            End If

                        End If

                    End If

                Catch ex As Exception
                    objlog.write2LogFile("Exception_Welcomepage", ex.Message)
                End Try



                ' end of mifi
            End If




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

            'objdb.insertUpdateDelete("update MobileDetails set roomno='" & userCrdential.roomNo & "' , LastName='" & userCrdential.guestName & "', loginTime='" & ctime & "' where mac='" & MAC & "' and ( roomno is null or roomno ='' ) ")



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
                Response.Redirect("welcome.aspx?" & GetRedirectQS())
            Catch ex As Exception
                'objlog.write2LogFile("MACtrack-" & userCrdential.machineId & "Error_welcome", ex.Message)
            End Try
        Else

            Try
                pgCookie.ResetCookie(HttpContext.Current.Response)
                Response.Redirect("UserError.aspx?" & GetRedirectQS() & "&Msg=" & output & "&findurl=mifilogin")
            Catch ex As Exception
                'objlog.write2LogFile("MACtrack-" & userCrdential.machineId & "Error_welcome", ex.Message)
            End Try
        End If
    End Sub

    Private Sub login1(ByRef userCrdential As UserContext)
        Dim objDbase As DbaseService
        objDbase = objDbase.getInstance
        Dim objlog As LoggerService
        objlog = LoggerService.gtInstance

        Dim AAA As AAAService
        Dim output As String
        '  Dim objDbase As DbaseService
        AAA = AAAService.getInstance
        output = AAA.AAA(userCrdential)
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
                Response.Redirect("welcome.aspx?" & GetRedirectQS())
            Catch ex As Exception
                'objlog.write2LogFile("MACtrack-" & userCrdential.machineId & "Error_welcome", ex.Message)
            End Try

        Else
            '--------------------------- START avoid Parallel Login Process ---------------
            Try
                pgCookie.ResetCookie(HttpContext.Current.Response)
                Response.Redirect("UserError.aspx?" & GetRedirectQS() & "&Msg=" & output & "&findurl=vlanlogin")
            Catch ex As Exception
                'objlog.write2LogFile("MACtrack-" & userCrdential.machineId & "Error_welcome", ex.Message)
            End Try
        End If
    End Sub



    Private Function GetRedirectQS()
        Dim redirectQS As String = ""
        redirectQS = "encry=" & Request.QueryString("encry")
        Return redirectQS
    End Function

End Class