Imports PMSPkgSql
Partial Public Class UserErrorNew
    Inherits System.Web.UI.Page
    Public url As String
    Public Errmessage As String
    Protected pageTitle As New HtmlGenericControl
    Private PMSName As PMSNAMES
    Private PMSVersion As String
    Private CouponVersion As String
    Dim room As String = ""
    Dim lastname As String = ""
    Dim plan As String = ""
    Dim mc1 As String = ""
    Dim mc2 As String = ""
    Dim mc3 As String = ""

    Dim pgCookie As New CCookies
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        Try
            If Not Session("cu") Is Nothing Then
                Page.UICulture = Session("cu")
            End If

        Catch ex As Exception

        End Try


        Dim objlog As LoggerService
        objlog = LoggerService.gtInstance

        'Put user code to initialize the page here
        Dim objSysConfig As New CSysConfig
        Dim commonFun As PMSCommonFun



        commonFun = PMSCommonFun.getInstance
        url = commonFun.BrowserQueryString(Request)
        Dim mac As String = ""
        Dim qs As String = Request.QueryString("encry")
        mac = commonFun.DecrptQueryString("MA", qs)


        ' msgPara.InnerText = Request.QueryString("Msg").ToString()
        Dim r1 As String = ""
        Dim r2 As String = ""
        Dim pd = Request.QueryString("pid")
        Try
            r1 = Request.QueryString("rm")
            r2 = Request.QueryString("ln")
            Try
                objlog.write2LogFile("UEN_" & commonFun.DecrptQueryString("MA", qs), "MIFI before split r1" & r1)
            Catch ex As Exception

            End Try


            Try
                Dim str() = r1.Split(",")
                Dim ind As Integer = 0
                ind = str.Length - 1

                r1 = str(ind)
                objlog.write2LogFile("UEN_" & commonFun.DecrptQueryString("MA", qs), " after split r1" & r1)

            Catch ex As Exception

            End Try
            objlog.write2LogFile("UEN_" & commonFun.DecrptQueryString("MA", qs), " before split r2" & r2)

            Try
                Dim str() = r2.Split(",")
                Dim ind As Integer = 0
                ind = str.Length - 1

                r2 = str(ind)
                objlog.write2LogFile("UEN_" & commonFun.DecrptQueryString("MA", qs), "err after split r2" & r2)

            Catch ex As Exception

            End Try

            Try
                Dim str() = pd.Split(",")
                Dim ind As Integer = 0
                ind = str.Length - 1

                pd = str(ind)
                objlog.write2LogFile("UEN_" & commonFun.DecrptQueryString("MA", qs), "after split r2" & pd)

            Catch ex As Exception

            End Try


        Catch ex As Exception

        End Try


        Try
            room = r1
            lastname = r2
            plan = pd
        Catch ex As Exception

        End Try


        Try
            If plan = 2 Then
                logut_btn1.Text = "Upgrade to Premium plan."
                msgPara.InnerText = "You are already connected with your 6 devices if you want to connect please buy a premium plan for the high speed internet access."

            Else
                logut_btn1.Text = "Purchase"
                msgPara.InnerText = "  Dear Guest, This is to inform you that you are already connected with 6 devices. In case, you would like to opt for the 7 th device please contact Guest Services.  "

                logut_btn1.Visible = True
            End If





            'logut_btn.Text = GetLocalResourceObject("m3").ToString()
            ' Maccount(r2, r1, mac)
        Catch ex As Exception

        End Try




    End Sub


    

    Private Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        Dim img As ImageButton = DirectCast(Master.FindControl("LinkButton8"), ImageButton)
        img.ImageUrl = GetLocalResourceObject("C1").ToString()


        Dim img1 As ImageButton = DirectCast(Master.FindControl("LinkButton9"), ImageButton)
        img1.ImageUrl = GetLocalResourceObject("C2").ToString()


        'Dim img2 As ImageButton = DirectCast(Master.FindControl("LinkButton10"), ImageButton)
        'img2.ImageUrl = GetLocalResourceObject("C3").ToString()


       




    End Sub

    Protected Sub logut_btn1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles logut_btn1.Click
        Dim AAA As AAAService
        Dim output As String = ""
        AAA = AAAService.getInstance
        Dim commonFun As PMSCommonFun

        Dim objlog As LoggerService
        objlog = LoggerService.gtInstance

        commonFun = PMSCommonFun.getInstance
        url = commonFun.BrowserQueryString(Request)
        Dim mac As String = ""
        Dim qs As String = Request.QueryString("encry")
        mac = commonFun.DecrptQueryString("MA", qs)


        Dim r1 As String = ""
        Dim r2 As String = ""
        Dim pd = Request.QueryString("pid")
        Try
            r1 = Request.QueryString("rm")
            r2 = Request.QueryString("ln")
            Try
                objlog.write2LogFile("UEN_" & commonFun.DecrptQueryString("MA", qs), "MIFI before split r1" & r1)
            Catch ex As Exception

            End Try


            Try
                Dim str() = r1.Split(",")
                Dim ind As Integer = 0
                ind = str.Length - 1

                r1 = str(ind)
                objlog.write2LogFile("UEN_" & commonFun.DecrptQueryString("MA", qs), " after split r1" & r1)

            Catch ex As Exception

            End Try
            objlog.write2LogFile("UEN_" & commonFun.DecrptQueryString("MA", qs), " before split r2" & r2)

            Try
                Dim str() = r2.Split(",")
                Dim ind As Integer = 0
                ind = str.Length - 1

                r2 = str(ind)
                objlog.write2LogFile("UEN1_" & commonFun.DecrptQueryString("MA", qs), "err after split r2" & r2)

            Catch ex As Exception

            End Try

            Try
                Dim str() = pd.Split(",")
                Dim ind As Integer = 0
                ind = str.Length - 1

                pd = str(ind)
                objlog.write2LogFile("UEN1_" & commonFun.DecrptQueryString("MA", qs), "after split r2" & pd)

            Catch ex As Exception

            End Try


        Catch ex As Exception

        End Try


        Try
            room = r1
            lastname = r2
            plan = pd
        Catch ex As Exception

        End Try
        Dim objSysConfig As New CSysConfig
        Dim serverIP As String = ""
        Try
            serverIP = objSysConfig.GetConfig("BillServer_IP")
            'objlog.write2LogFile("serverip", "http://" & serverIP & "/mob/Welcome.aspx?")
        Catch ex As Exception

        End Try

        If logut_btn1.Text = "Upgrade to Premium plan." Then

            Try
                Response.Redirect("http://" & serverIP & "/upgrade/PlanSel.aspx?" & GetRedirectQS() & "&plan=" & 0 & "&rm=" & room & "&ln=" & lastname & "&ct=" & Page.UICulture)

            Catch ex As Exception

            End Try

        Else

            Try
                Response.Redirect("http://" & serverIP & "/upgrade1/PlanSel.aspx?" & GetRedirectQS() & "&plan=" & 0 & "&rm=" & room & "&ln=" & lastname & "&ct=" & Page.UICulture)

            Catch ex As Exception

            End Try


        End If

        'Try
        '    Dim objlog As LoggerService
        '    objlog = LoggerService.gtInstance
        '    objlog.write2LogFile("UEN_" & mac, "Mac 1" & mc1)
        'Catch ex As Exception

        'End Try

        'Try
        '    Dim objlog As LoggerService
        '    objlog = LoggerService.gtInstance
        '    objlog.write2LogFile("UEN_" & mac, "Mac2" & mc2)
        'Catch ex As Exception

        'End Try

        'Try
        '    Dim objlog As LoggerService
        '    objlog = LoggerService.gtInstance
        '    objlog.write2LogFile("UEN_" & mac, "Mac3" & mc3)
        'Catch ex As Exception

        'End Try

        'Try
        '    Dim objlog As LoggerService
        '    objlog = LoggerService.gtInstance
        '    objlog.write2LogFile("UEN_" & mac, "room" & room)
        '    objlog.write2LogFile("UEN_" & mac, "lastname" & lastname)
        '    objlog.write2LogFile("UEN_" & mac, "plan" & plan)
        'Catch ex As Exception

        'End Try




        'Dim objSysConfig As New CSysConfig
        'commonFun = PMSCommonFun.getInstance
        'objSysConfig = New CSysConfig
        'Try
        '    PMSName = commonFun.GetPMSType(Trim(objSysConfig.GetConfig("PMSName")))
        '    PMSVersion = objSysConfig.GetConfig("PMSVersion")
        '    CouponVersion = objSysConfig.GetConfig("CouponVersion")
        'Catch ex As Exception

        'End Try




        'Dim userCrdential As New UserContext(room, lastname, plan, PMSName, PMSVersion, HttpContext.Current.Request)
        'userCrdential.item("usertype") = EUSERTYPE.ROOM
        'userCrdential.item("accesstype") = 0 ' wireless login
        'userCrdential.item("logintype") = LOGINTYPE.NEWLOGIN
        'Try
        '    userCrdential.item("grcid") = ""
        'Catch ex As Exception

        'End Try

        ''--------------------------- START avoid Parallel Login Process ---------------
        'userCrdential.item("UsrIP1") = room
        'userCrdential.item("UsrIP2") = lastname
        'userCrdential.wiredlessMac = userCrdential.machineId

        'Try
        '    Dim objin As LogInOutService
        '    objin = LogInOutService.getInstance

        '    userCrdential.wiredMac = objin.GetWiredMAC(userCrdential.wiredlessMac, userCrdential.item("UsrIP1"))

        'Catch ex As Exception

        'End Try


        'Try
        '    Login(userCrdential)
        'Catch ex As Exception

        'End Try


    End Sub

    Protected Sub logut_btn_Click(ByVal sender As Object, ByVal e As EventArgs) Handles logut_btn.Click
        Dim objlog As LoggerService
        objlog = LoggerService.gtInstance

        Dim commonFun As PMSCommonFun
        commonFun = PMSCommonFun.getInstance
        url = commonFun.BrowserQueryString(Request)


        Try


            Response.Redirect("welcome.aspx?" & GetRedirectQS())

        Catch ex As Exception

        End Try
    End Sub

    


    

    


    Private Function GetRedirectQS()
        Dim redirectQS As String = ""
        redirectQS = "encry=" & Request.QueryString("encry")
        Return redirectQS
    End Function
End Class