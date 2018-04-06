Imports System.Configuration
Imports System.Data
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.Configuration
Imports System.Net.Configuration


Imports System.Net
Imports System.Net.NetworkInformation
Imports System.Text
Imports System.Net.Sockets
Imports System.Data.Common
Imports PMSPkgSql
Imports System.Runtime.Serialization.Formatters


Partial Public Class PostLogin
    Inherits System.Web.UI.Page
    Public ReqPage, Macadd, grcid, roomNo1 As String
    Public Plantime As Long
    Public usertype As Integer
    Private planId As Long
    Private PMSName As PMSNAMES
    Private PMSVersion As String
    Private accesstype As Integer
    Private userContext As UserContext
    Dim pgCookie As New CCookies
    Private sql_query
    Dim RN As String = ""
    Dim urp As String = ""
    Dim url As String = ""

    Public rno As String = ""
    Dim gn As String = ""
    Dim a1 As String
    Dim a2 As String
    Dim a3 As String

    Dim strRN As String = ""
    Dim strLN As String = ""
    Dim strGID As String = ""
    Dim loginpwd As String = ""

    Public Function GetGuestDaysStay(ByVal str As String, ByVal pd As String) As Integer
        Dim SQL_query As String
        Dim objDbase As DbaseService
        Dim RefResultset As DataSet
        Dim NoOfDayStay As Integer = 0
        Dim ObjLog As LoggerService

        'Synchronize the no. of days to stay for a guest on on GuestChkInTime and GuestExpChkOutTime
        'Try
        '    ExtendedUtil.SynchronizeGuestNoDays(GuestID.ToString())
        'Catch ex As Exception

        'End Try

        'This will consider how many days the guest has remaining for his stay
        SQL_query = "SELECT dbo.GetNoOfDays(GetDate(), GuestExpChkOutTime) As GuestNofDStay FROM Guest  where upper(GuestName) = '" & pd.Replace("'", "''") & "' and gueststatus='A'  and GuestRoomNo ='" & str & "' order by guestid desc"

        Try
            objDbase = DbaseService.getInstance
            RefResultset = objDbase.DsWithoutUpdate(SQL_query)
            If RefResultset.Tables(0).Rows.Count > 0 Then
                NoOfDayStay = Integer.Parse(RefResultset.Tables(0).Rows(0).Item("GuestNofDStay").ToString())
            Else
                NoOfDayStay = 0
            End If
        Catch ex As Exception
            NoOfDayStay = 0
            ObjLog = LoggerService.gtInstance
            ObjLog.writeExceptionLogFile("GuestStayExp", ex)
        End Try
        Return NoOfDayStay
    End Function
  



    Private Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender

        Try
            Dim objlog As LoggerService
            objlog = LoggerService.gtInstance




            Try
                Page.UICulture = Session("cu")
                If Session("cu") = "ar-SA" Then
                    img_arrow.Src = "images/arrow_arabic.jpg"
                Else
                    img_arrow.Src = "images/arrow_left.jpg"

                End If
            Catch ex As Exception

            End Try

            Dim cc As String = ""








            Try
                'Button1.Text = GetLocalResourceObject("m6").ToString()
                Button2.Text = GetLocalResourceObject("m9").ToString()

                Label1.Text = GetLocalResourceObject("k1").ToString()
                Label2.Text = GetLocalResourceObject("k2").ToString()
            Catch ex As Exception

            End Try
            Try
                a1 = GetLocalResourceObject("e1").ToString()
                a2 = GetLocalResourceObject("e2").ToString()
                a3 = GetLocalResourceObject("e3").ToString()
            Catch ex As Exception

            End Try



            Try
                Session("e1") = GetLocalResourceObject("e1").ToString()
                Session("e2") = GetLocalResourceObject("e2").ToString()
                Session("e3") = GetLocalResourceObject("e3").ToString()
            Catch ex As Exception

            End Try



            Dim objSysConfig As New CSysConfig
            Dim gateWay As IGatewayService
            Dim gateWayFact As GatewayServiceFactory
            Dim gatWayQryResult As NdxQueryGatewayResults
            Dim pgCookie As New CCookies
            '  Me.pageTitle.InnerText = objSysConfig.GetConfig("HotelName")

            Dim macaddr As String = ""


            '----------------------START PMS Config --------------------------------------
            Dim commonFun As PMSCommonFun
            Try
                commonFun = PMSCommonFun.getInstance
                url = commonFun.BrowserQueryString(Request)
                PMSName = commonFun.GetPMSType(Trim(objSysConfig.GetConfig("PMSName")))
                PMSVersion = objSysConfig.GetConfig("PMSVersion")
            Catch ex As Exception

            End Try





            '----------------------END PMS Config ----------------------------------------

            'lblProNEWS.Text = objSysConfig.GetConfig("HotelName")
            'ImgPromotion.ImageUrl = "images/" & objSysConfig.GetConfig("PromotionIMAGE")
            ' btnupgrade.Attributes.Add("OnClick", "document.frmPostlogin.hdalert.value=1;")


            'Try
            '    MyBase.OnPreRender(e)
            '    Dim strDisAbleBackButton As String
            '    strDisAbleBackButton = "<SCRIPT language=javascript>" & vbLf
            '    strDisAbleBackButton += "window.history.forward(1);" & vbLf
            '    strDisAbleBackButton += vbLf & "</SCRIPT>"
            '    ClientScript.RegisterClientScriptBlock(Me.Page.[GetType](), "clientScript", strDisAbleBackButton)
            'Catch ex As Exception

            'End Try



            Dim indx As Long
            Dim planName As String
            Dim duration As String
            Dim exptime As String
            Dim validity, planamount, getplanid As String
            Dim validity1, validity2 As Long
            Dim allPlans As DataSet
            Dim objPlan1 As New CPlan
            Dim objPlan As New CPlan
            Dim objPlan2 As New CPlan
            Dim userCredential As UserCredential
            Dim objGuestSrv As GuestService
            Dim msg As String = ""
            Dim msg1 As String = ""

            Dim grcid As String
            Dim qs As String = ""

            Try
                qs = Request.QueryString("encry")
                macaddr = commonFun.DecrptQueryString("MA", qs)

            Catch ex As Exception

            End Try

            Try
                If Not Session("us") Is Nothing Then
                    ' lblerr.Visible = False
                    userContext = Session("us")
                    Dim objLogInOut1 As LogInOutService
                    objLogInOut1 = LogInOutService.getInstance
                    macaddr = commonFun.DecrptQueryString("MA", qs)
                    Plantime = objLogInOut1.GetLastPlanTime(userContext)
                    Try
                        If userContext.item("usertype") = EUSERTYPE.COUPON Then

                            Plantime = objLogInOut1.GetCouponLastPlanTime(userContext.item("grcid"))
                            gn = userContext.password

                            Try
                                x1.Visible = False
                                x2.Visible = False
                                plandetails.Text = "Complimentary"
                            Catch ex As Exception

                            End Try


                        Else

                            Try
                                strRN = userContext.roomNo

                                strLN = userContext.password

                                strGID = userContext.item("grcid")

                                objlog.write2LogFile("pup1", "name" & strLN & "RoomNo" & strRN & "gid=" & strGID)
                            Catch ex As Exception

                            End Try



                            Try
                                Label1.Text = GetLocalResourceObject("s1").ToString()
                                Label2.Text = GetLocalResourceObject("s2").ToString()
                            Catch ex As Exception

                            End Try


                        End If

                    Catch ex As Exception

                    End Try

                    ' urp = userContext.requestedPage

                    Try
                        hdRemainingTime.Value = Plantime
                        exp.Text = DateAdd(DateInterval.Second, Convert.ToDouble(hdRemainingTime.Value), DateTime.Now)

                    Catch ex As Exception

                    End Try
                Else
                    ' Dim qs As String = ""
                    ' Dim macaddr As String = ""
                    Dim objLogInOut1 As LogInOutService
                    objLogInOut1 = LogInOutService.getInstance
                    ' Dim commonFun As PMSCommonFun
                    Try
                        Label1.Text = GetLocalResourceObject("s1").ToString()
                        Label2.Text = GetLocalResourceObject("s2").ToString()
                    Catch ex As Exception

                    End Try
                    commonFun = PMSCommonFun.getInstance
                    Try
                        qs = Request.QueryString("encry")
                        macaddr = commonFun.DecrptQueryString("MA", qs)
                        Plantime = objLogInOut1.GetLastPlanTime(macaddr)
                        hdRemainingTime.Value = Plantime
                        exp.Text = DateAdd(DateInterval.Second, Convert.ToDouble(hdRemainingTime.Value), DateTime.Now)
                        Dim ds As DataSet
                        ds = GetRoomNo(Macadd)

                        rno = ds.Tables(0).Rows(0)(0)
                        gn = ds.Tables(0).Rows(0)(1)
                        strGID = ds.Tables(0).Rows(0)(2)
                        Try
                            strRN = rno
                            strLN = gn
                            objlog.write2LogFile("pup2", "name" & strLN & "RoomNo" & strRN & "gid=" & strGID)
                        Catch ex As Exception

                        End Try



                    Catch ex As Exception

                    End Try

                End If




            Catch ex As Exception

            End Try




            Try

                If Not IsPostBack() Then

                    'If Request("Ma") = "" Then Response.Redirect("UserError.aspx?Msg=This Error has probably occurred because you directly accessed our login screen. Please use your browser to access any internet website, and if you are not already authenticated, you will be automatically be presented with a login screen.")

                    Try
                        'upgra.Visible = False
                        'usage_details.Visible = False
                        'rdoplan1.Visible = False
                        'upgra.Visible = False
                        'term.Visible = False
                        'upgra1.Visible = False
                        'Try
                        '    rdoplan1.Items.Clear()
                        'Catch ex As Exception

                        'End Try

                    Catch ex As Exception

                    End Try


                    Try
                        DrpPlans1.SelectedIndex = 0
                    Catch ex As Exception

                    End Try


                   




                    Dim stay As Integer = 0

                    


                    Try
                        stay = GetGuestDaysStay(strRN, strLN)
                    Catch ex As Exception

                    End Try


                    Try
                        If stay = 0 Then
                            stay = 1
                        End If

                        stay = stay + 1
                    Catch ex As Exception

                    End Try
                    Try
                        objlog.write2LogFile("stay", "name" & strLN & "RoomNo" & strRN & "stay=" & stay)
                    Catch ex As Exception

                    End Try

                    Try
                        Dim i As Integer = 1

                        While i <= stay
                            Try
                                DrpPlans1.Items.Add(New ListItem(i.ToString(), i.ToString()))

                            Catch ex As Exception

                            End Try
                            i = i + 1

                        End While

                    Catch ex As Exception

                    End Try

                    Try
                        DrpPlans1.SelectedIndex = 0


                    Catch ex As Exception

                    End Try





                    Try
                        Dim commonFun1 As PMSCommonFun
                        commonFun1 = PMSCommonFun.getInstance
                        myurl.Value = commonFun1.BrowserQueryString(Request)
                    Catch ex As Exception

                    End Try


                    If Not Session("us") Is Nothing Then

                        userContext = Session("us")
                        Try
                            objlog.write2LogFile("MACtrack-" & userContext.machineId, "user access")
                        Catch ex As Exception

                        End Try


                        Try
                            Dim objLogInOut1 As LogInOutService
                            objLogInOut1 = LogInOutService.getInstance
                            macaddr = commonFun.DecrptQueryString("MA", qs)
                            Plantime = objLogInOut1.GetLastPlanTime(userContext)
                            urp = userContext.requestedPage

                            Try
                                hdRemainingTime.Value = Plantime
                                exp.Text = DateAdd(DateInterval.Second, Convert.ToDouble(hdRemainingTime.Value), DateTime.Now)

                            Catch ex As Exception

                            End Try




                            roomno.Text = userContext.roomNo
                            If userContext.item("usertype") = EUSERTYPE.COUPON Then
                                lastname.Text = userContext.password
                                gn = userContext.password

                                Try
                                    x1.Visible = False
                                    x2.Visible = False
                                Catch ex As Exception

                                End Try


                            Else
                                
                                Try

                                    loginpwd = GetLName1(userContext.item("grcid"))
                                    If loginpwd <> "" Then
                                        lastname.Text = loginpwd

                                    Else
                                        lastname.Text = GetLName(userContext.item("grcid"))

                                    End If
                                Catch ex As Exception

                                End Try


                            End If


                            'Try
                            '    Try
                            '        strRN = userContext.roomNo

                            '        strLN = userContext.password

                            '        strGID = userContext.item("grcid")

                            '        objlog.write2LogFile("pup", "name" & strLN & "RoomNo" & strRN & "gid=" & strGID)
                            '    Catch ex As Exception

                            '    End Try

                            'Catch ex As Exception

                            'End Try



                            'username.Text = GetLName(userContext.item("grcid")) & " Of " & userContext.roomNo

                            username.Text = GetLocalResourceObject("s3").ToString()

                            Try
                                If userContext.selectedPlanId = "101" Or userContext.selectedPlanId = "201" Then
                                    plandetails.Text = "Standard-Complementary Internet."


                                    Try
                                        upgra.Visible = True
                                        usage_details.Visible = True
                                        rdoplan1.Visible = True
                                        upgra.Visible = True
                                        term.Visible = True
                                        upgra1.Visible = True




                                        Try
                                            Try
                                                rdoplan1.Items.Clear()
                                            Catch ex As Exception

                                            End Try
                                            '   rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "Premium Service - 24-hour access" & "</span><span class=""WebRupee"">INR 900</span> " & "", 102))
                                            rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "Premium 12 Mbps at 499 INR/day" & "</span><span class=""WebRupee""></span> " & "", 202))



                                        Catch ex As Exception

                                        End Try


                                    Catch ex As Exception

                                    End Try

                                    Dim credit As Integer = 0

                                    Try
                                        credit = NOCREDIT(strRN, strLN)

                                        If credit = 1 Then

                                            upgra.Visible = False
                                            usage_details.Visible = False
                                            rdoplan1.Visible = False
                                            upgra.Visible = False
                                            term.Visible = False
                                            upgra1.Visible = False


                                        End If

                                    Catch ex As Exception

                                    End Try






                                ElseIf userContext.selectedPlanId = "103" Then
                                    plandetails.Text = "Premium Service - 24-hour access-" & "Complimentary"
                                ElseIf userContext.selectedPlanId = "202" Then
                                    plandetails.Text = "Premium 12 Mbps per day" & " INR 499"

                                ElseIf userContext.selectedPlanId = "302" Then
                                    plandetails.Text = "Premium-Complimentary Internet"
                                ElseIf userContext.selectedPlanId = "302" Then
                                    plandetails.Text = "Premium-Complimentary Internet"

                                ElseIf userContext.selectedPlanId = "701" Then
                                    plandetails.Text = "High Speed Internet Access"
                                ElseIf userContext.selectedPlanId = "105" Then
                                    plandetails.Text = "Executive Service - 24-hour access" & " INR 500"

                                    Try
                                        upgra.Visible = True
                                        usage_details.Visible = True
                                        '  rdoplan1.Visible = True
                                        upgra.Visible = True
                                        term.Visible = True
                                        upgra1.Visible = True




                                        Try
                                            Try
                                                '  rdoplan1.Items.Clear()
                                            Catch ex As Exception

                                            End Try
                                            '  rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "Premium Service - 24-hour access" & "</span><span class=""WebRupee"">INR 900</span> " & "", 102))
                                            ' rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "Executive Service - 24-hour access" & "</span><span class=""WebRupee"">INR 500</span> " & "", 105))



                                        Catch ex As Exception

                                        End Try


                                    Catch ex As Exception

                                    End Try




                                ElseIf userContext.selectedPlanId = "5" Then
                                    plandetails.Text = "Basic 2" & GetLocalResourceObject("p4").ToString() & " INR 1299"

                                ElseIf userContext.selectedPlanId = "6" Then
                                    plandetails.Text = "Basic 5" & GetLocalResourceObject("p4").ToString() & " INR 2899"

                                ElseIf userContext.selectedPlanId = "7" Then
                                    plandetails.Text = "Basic 7" & GetLocalResourceObject("p4").ToString() & " INR 3599"




                                ElseIf userContext.selectedPlanId = "8" Then
                                    plandetails.Text = "Premium 1" & GetLocalResourceObject("p2").ToString() & " INR 299"

                                ElseIf userContext.selectedPlanId = "9" Then
                                    plandetails.Text = "Premium 3" & GetLocalResourceObject("p3").ToString() & " INR 399"
                                ElseIf userContext.selectedPlanId = "10" Then
                                    plandetails.Text = "Premium 12 " & GetLocalResourceObject("p3").ToString() & " INR 699"
                                ElseIf userContext.selectedPlanId = "11" Then
                                    plandetails.Text = "Premium 1 " & GetLocalResourceObject("p1").ToString() & " INR 899"
                                ElseIf userContext.selectedPlanId = "12" Then
                                    plandetails.Text = "Premium 2 " & GetLocalResourceObject("p4").ToString() & " INR 1699"

                                ElseIf userContext.selectedPlanId = "13" Then
                                    plandetails.Text = "Premium 5" & GetLocalResourceObject("p4").ToString() & " INR 3399"
                                ElseIf userContext.selectedPlanId = "14" Then
                                    plandetails.Text = "Premium 7 " & GetLocalResourceObject("p4").ToString() & " INR 4099"
                                ElseIf userContext.selectedPlanId = "15" Then
                                    plandetails.Text = GetLocalResourceObject("p40").ToString()

                                End If



                            Catch ex As Exception

                            End Try












                            Try
                                If plandetails.Text = "" Then

                                    If userContext.item("planid") = "101" Or userContext.item("planid") = "201" Then
                                        plandetails.Text = "Standard-Complementary Internet. "
                                        Try
                                            upgra.Visible = True
                                            usage_details.Visible = True
                                            rdoplan1.Visible = True
                                            upgra.Visible = True
                                            term.Visible = True
                                            upgra1.Visible = True

                                            Try
                                                rdoplan1.Items.Clear()
                                            Catch ex As Exception

                                            End Try



                                            Try
                                                ' rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "Premium Service - 24-hour access" & "</span><span class=""WebRupee"">INR 900</span> " & "", 102))
                                                ' rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "Executive Service - 24-hour access" & "</span><span class=""WebRupee"">INR 500</span> " & "", 105))

                                                rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "Premium 12 Mbps at 499 INR/day" & "</span><span class=""WebRupee""></span> " & "", 202))

                                            Catch ex As Exception

                                            End Try


                                        Catch ex As Exception

                                        End Try


                                        Dim credit As Integer = 0

                                        Try
                                            credit = NOCREDIT(strRN, strLN)

                                            If credit = 1 Then

                                                upgra.Visible = False
                                                usage_details.Visible = False
                                                rdoplan1.Visible = False
                                                upgra.Visible = False
                                                term.Visible = False
                                                upgra1.Visible = False


                                            End If

                                        Catch ex As Exception

                                        End Try



                                    ElseIf userContext.item("planid") = "103" Then
                                        plandetails.Text = "Premium Service - 24-hour access-" & " Complimentary"
                                    ElseIf userContext.item("planid") = "202" Then
                                        plandetails.Text = "Premium 12 Mbps per day" & " INR 499"

                                    ElseIf userContext.item("planid") = "302" Then
                                        plandetails.Text = "Premium-Complimentary Internet"

                                    ElseIf userContext.item("planid") = "701" Then
                                        plandetails.Text = "High Speed Internet Access"

                                    ElseIf userContext.item("planid") = "105" Then

                                        Try
                                            upgra.Visible = True
                                            usage_details.Visible = True
                                            '   rdoplan1.Visible = True
                                            upgra.Visible = True
                                            term.Visible = True
                                            upgra1.Visible = True


                                            Try
                                                '  rdoplan1.Items.Clear()
                                            Catch ex As Exception

                                            End Try

                                            Try
                                                '      rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "Premium Service - 24-hour access" & "</span><span class=""WebRupee"">INR 900</span> " & "", 102))
                                                ' rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "Executive Service - 24-hour access" & "</span><span class=""WebRupee"">INR 500</span> " & "", 105))



                                            Catch ex As Exception

                                            End Try


                                        Catch ex As Exception

                                        End Try


                                        plandetails.Text = "Executive Service - 24-hour access" & " INR 500"

                                    ElseIf userContext.item("planid") = "5" Then
                                        plandetails.Text = "Basic 2" & GetLocalResourceObject("p4").ToString() & " INR 1299"

                                    ElseIf userContext.item("planid") = "6" Then
                                        plandetails.Text = "Basic 5" & GetLocalResourceObject("p4").ToString() & " INR 2899"

                                    ElseIf userContext.item("planid") = "7" Then
                                        plandetails.Text = "Basic 7" & GetLocalResourceObject("p4").ToString() & " INR 3599"




                                    ElseIf userContext.item("planid") = "8" Then
                                        plandetails.Text = "Premium 1" & GetLocalResourceObject("p2").ToString() & " INR 299"

                                    ElseIf userContext.item("planid") = "9" Then
                                        plandetails.Text = "Premium 3" & GetLocalResourceObject("p3").ToString() & " INR 399"
                                    ElseIf userContext.item("planid") = "10" Then
                                        plandetails.Text = "Premium 12 " & GetLocalResourceObject("p3").ToString() & " INR 699"
                                    ElseIf userContext.item("planid") = "11" Then
                                        plandetails.Text = "Premium 1 " & GetLocalResourceObject("p1").ToString() & " INR 899"
                                    ElseIf userContext.item("planid") = "12" Then
                                        plandetails.Text = "Premium 2 " & GetLocalResourceObject("p4").ToString() & " INR 1699"

                                    ElseIf userContext.item("planid") = "13" Then
                                        plandetails.Text = "Premium 5" & GetLocalResourceObject("p4").ToString() & " INR 3399"
                                    ElseIf userContext.item("planid") = "14" Then
                                        plandetails.Text = "Premium 7 " & GetLocalResourceObject("p4").ToString() & " INR 4099"
                                    ElseIf userContext.item("planid") = "15" Then
                                        plandetails.Text = GetLocalResourceObject("p40").ToString()

                                    End If



                                End If




                            Catch ex As Exception

                            End Try



                            objlog.write2LogFile("MACtrack-" & userContext.machineId, "user access Remaining PlanTime" & Plantime)
                        Catch ex As Exception

                        End Try

                        Try



                            Dim currtime As DateTime
                            currtime = Now
                            Dim validtime As DateTime
                            validtime = DateAdd(DateInterval.Second, Plantime, currtime)
                            'Label1.Text = "Connection Valid till<br> " & validtime.ToString("dddd,MMMM, dd yyyy hh:mm:ss tt")
                            ' Label1.Visible = False
                        Catch ex As Exception
                            objlog.write2LogFile("timediff", "err" & ex.Message)
                        End Try

                        Try
                            If userContext.item("usertype") = EUSERTYPE.COUPON Then

                                roomno.Text = userContext.roomNo
                                lastname.Text = userContext.password
                                ' username.Text = userContext.roomNo & " Of " & userContext.password
                                username.Text = GetLocalResourceObject("s3").ToString()
                                gn = userContext.password

                                Try
                                    x1.Visible = False
                                    x2.Visible = False
                                Catch ex As Exception

                                End Try


                                Try
                                    Dim objloginout As LogInOutService
                                    objloginout = LogInOutService.getInstance
                                    Plantime = objloginout.GetCouponLastPlanTime(userContext.item("grcid"))

                                    Try
                                        hdRemainingTime.Value = Plantime
                                        exp.Text = DateAdd(DateInterval.Second, Convert.ToDouble(hdRemainingTime.Value), DateTime.Now)

                                    Catch ex As Exception

                                    End Try
                                    ' objlog.write2LogFile("PTC", Plantime)



                                Catch ex As Exception
                                    ' objlog.write2LogFile("PTC", "err" & ex.Message)
                                End Try
                            Else
                            End If
                        Catch ex As Exception

                        End Try

                        Try
                            If Not userContext.item("grcid") Is Nothing Then pgCookie.SetCookie(userContext, Response)
                            ' hdMAC.Value = userContext.machineId
                            ' hdRegPg.Value = userContext.requestedPage
                            Try
                                ' Browse.HRef = userContext.requestedPage
                            Catch ex As Exception

                            End Try
                            roomno.Text = userContext.roomNo

                            rno = userContext.roomNo
                            gn = GetLName(userContext.item("grcid"))
                            Try

                                loginpwd = GetLName1(userContext.item("grcid"))
                                If loginpwd <> "" Then
                                    lastname.Text = loginpwd

                                Else
                                    lastname.Text = GetLName(userContext.item("grcid"))

                                End If
                            Catch ex As Exception

                            End Try


                            Try
                                strRN = rno

                                strLN = gn

                                strGID = userContext.item("grcid")

                                objlog.write2LogFile("pup3", "name" & strLN & "RoomNo" & strRN & "gid=" & strGID)
                            Catch ex As Exception

                            End Try




                            

                            ' username.Text = GetLName(userContext.item("grcid")) & "  Of " & userContext.roomNo & ","
                            username.Text = GetLocalResourceObject("s3").ToString()
                        Catch ex As Exception
                            objlog.writeExceptionLogFile("UserAccessError2", ex)
                        End Try




                    Else

                        If pgCookie.ReadCookie(Request) Then
                            If pgCookie.expiryTime > Now() Then
                                'hdoffset.Value = DateTime.Now.ToString()
                                objGuestSrv = GuestService.getInstance
                                Try
                                    'hdgrcid.Value = pgCookie.grcId

                                    'hdusertype.Value = pgCookie.userType
                                    'hdoffset.Value = DateTime.Now.ToString()
                                    'hdRoomNo.Value = pgCookie.grcId
                                    'hdRegPg.Value = userContext.requestedPage



                                    '   username.Text = GetLName(pgCookie.grcId) & " Of " & userContext.roomNo

                                    username.Text = GetLocalResourceObject("s3").ToString()

                                    roomno.Text = userContext.roomNo
                                    'lastname.Text = GetLName(pgCookie.grcId)
                                    If lastname.Text = "" Then
                                        'lastname.Text = userContext.password
                                    End If

                                    Try
                                        loginpwd = GetLName1(pgCookie.grcId)
                                        If loginpwd <> "" Then
                                            lastname.Text = loginpwd

                                        Else
                                            lastname.Text = GetLName(pgCookie.grcId)
                                        End If

                                    Catch ex As Exception

                                    End Try



                                    Try
                                        ' Browse.HRef = userContext.requestedPage
                                    Catch ex As Exception

                                    End Try
                                    '  hdMAC.Value = commonFun.DecrptQueryString("MA", qs)
                                Catch ex As Exception
                                    objlog.writeExceptionLogFile("UserAccessError8", ex)
                                End Try
                                Try
                                    'hdurl.Value = commonFun.BrowserQueryString(Request)

                                    'hdurl.Value = "www.delhi.regency.hyatt.com"
                                Catch ex As Exception

                                End Try
                                'userContext = New UserContext(hdRoomNo.Value, hdMAC.Value)

                                'userContext.item("usertype") = pgCookie.userType
                                'userContext.item("serviceaccess") = pgCookie.serviceAccess

                                If pgCookie.userType = EUSERTYPE.ROOM Then
                                    userContext.item("usertype") = EUSERTYPE.ROOM
                                Else
                                    userContext.item("usertype") = EUSERTYPE.COUPON
                                End If
                            Else
                                If userContext.item("usertype") = EUSERTYPE.COUPON Then
                                    'Response.Redirect("UserErrorSql.aspx?" & url & "&Msg=" & "The package which you had selected has expired")
                                Else

                                    'Response.Redirect("UserErrorSql.aspx?" & url & "&Msg=" & "Your Account Expired&findurl=welcomepage")
                                End If


                            End If
                        Else
                            ' Response.Redirect("UserErrorSql.aspx?" & url & "&Msg=" & "Please close the browser and Try again") 'No Cookies
                        End If

                        'HLinkBMark1.NavigateUrl = "javascript:bookmark();"
                        'HLinkBMark2.NavigateUrl = "javascript:bookmark();"
                        'HLinkvirus1.NavigateUrl = objSysConfig.GetConfig("VirusAdvisory")
                        'HLinkvirus2.NavigateUrl = objSysConfig.GetConfig("VirusAdvisory")

                    End If
                Else

                    If Not Session("us") Is Nothing Then
                        userContext = Session("us")
                        Try
                            Session("oldplan") = userContext.selectedPlanId
                        Catch ex As Exception

                        End Try

                    Else
                        If pgCookie.ReadCookie(Request) Then

                            If pgCookie.expiryTime > Now() Then



                                Try
                                    objGuestSrv = GuestService.getInstance
                                    userCredential = objGuestSrv.getGuestInfo(pgCookie.grcId)
                                    getplanid = objGuestSrv.GetPlanid(pgCookie.grcId)
                                    Try
                                        Session("oldplan") = getplanid
                                    Catch ex As Exception

                                    End Try

                                    roomno.Text = userContext.roomNo


                                    Try

                                        loginpwd = GetLName1(pgCookie.grcId)
                                        If loginpwd <> "" Then
                                            lastname.Text = loginpwd

                                        Else
                                            lastname.Text = GetLName(pgCookie.grcId)

                                        End If
                                    Catch ex As Exception

                                    End Try



                                    

                                    rno = userContext.roomNo

                                Catch ex As Exception

                                End Try
                                userContext = New UserContext(userCredential, PMSName, PMSVersion, getplanid, HttpContext.Current.Request)

                                userContext.item("usertype") = pgCookie.userType
                                userContext.item("serviceaccess") = pgCookie.serviceAccess

                                If pgCookie.userType = EUSERTYPE.ROOM Then
                                    userContext.item("usertype") = EUSERTYPE.ROOM
                                Else
                                    userContext.item("usertype") = EUSERTYPE.COUPON
                                End If

                                Session.Add("tempus", userContext)
                            Else

                                If userContext.item("usertype") = EUSERTYPE.COUPON Then
                                    'Response.Redirect("UserErrorSql.aspx?" & url & "&Msg=" & "The package which you had selected has expired")
                                Else

                                    'Response.Redirect("UserErrorSql.aspx?" & url & "&Msg=" & "The package which you had selected has expired&findurl=welcomepage")
                                End If

                            End If
                        Else
                            'Response.Redirect("UserErrorSql.aspx?" & url & "&Msg=" & "Please close the browser and Try again") 'No Cookies

                        End If
                    End If

                End If




                ' objlog.write2LogFile("PT", Plantime)
            Catch ex As Exception

            End Try


            '----------------- START Get LatestPlanTime ---------------

            'gateWayFact = GatewayServiceFactory.getInstance
            'gateWay = gateWayFact.getGatewayService("FIDELIO", "2.3.8")

            'gatWayQryResult = gateWay.query(userContext)
            'If UCase(gatWayQryResult.gtStatus) = "ERROR" Then
            '    Plantime = 0
            'Else
            '    Plantime = gatWayQryResult.ndxExpireTime
            'End If
        Catch ex As Exception

        End Try
       
        Try
            Dim qs As String = ""
            qs = Request.QueryString("encry")
            Dim macaddr As String
            Dim commonFun As PMSCommonFun
            Dim objlog As LoggerService
            objlog = LoggerService.gtInstance

            commonFun = PMSCommonFun.getInstance
            macaddr = commonFun.DecrptQueryString("MA", qs)


            Dim CBILL As String = ""

            CBILL = Getcouponbill(macaddr)


            Dim CPLAN As String = ""


            CPLAN = Getcouponplan(CBILL)


            Try
                objlog.write2LogFile("subbuNew", "Bill Id" & CBILL & "Plan type=" & CPLAN)
            Catch ex As Exception

            End Try


            If CPLAN = "2" Then

                j8.Visible = False
                'email_fields.Visible = False

                Dim count As String = ""
                x1.Visible = False
                x2.Visible = False
                plandetails.Text = GetPlanname(CBILL)

                Label1.Visible = False
                Label2.Visible = False

                roomno.Visible = False
                lastname.Visible = False
                hari.Visible = False
                Button4.Visible = False
                Try
                    Dim ds As DataSet
                    ds = getcouponcount(macaddr)
                    count = ds.Tables(0).Rows(0)(0).ToString()
                Catch ex As Exception

                End Try

                Try
                    plandetails.Text = "meeting for " & count & " cuncurrent users " & " from" & Getstart(CBILL) & " to " & Getend(CBILL)
                Catch ex As Exception

                End Try

            ElseIf CPLAN = "1" Then

                j8.Visible = False
                'email_fields.Visible = False

                Label1.Visible = False
                Label2.Visible = False

                roomno.Visible = False
                lastname.Visible = False

                hari.Visible = False

                Label1.Text = "Conference Code"

                x1.Visible = False
                x2.Visible = False

                plandetails.Text = GetPlanname(CBILL)
                Button4.Visible = False

                hari.Visible = False


            ElseIf CPLAN = "3" Or CPLAN = "4" Then
                Label1.Visible = False
                Label2.Visible = False

                j8.Visible = False
                'email_fields.Visible = False

                roomno.Visible = False
                lastname.Visible = False

                hari.Visible = False

                Label1.Text = "Conference Code"

                x1.Visible = False
                x2.Visible = False

                plandetails.Text = GetPlanname(CBILL)

                Button4.Visible = True
                hari.Visible = False

                If CPLAN = 3 Then

                    Button4.Visible = False
                End If


            Else
                Button4.Visible = False


            End If






        Catch ex As Exception

        End Try
      

        Try
            Dim ds As DataSet
            ds = GetRoomNo(Macadd)

            If IsNumeric(ds.Tables(0).Rows(0)(0)) Then



                lastname.Text = ds.Tables(0).Rows(0)(1)
            End If
        Catch ex As Exception

        End Try
    End Sub
    Public Function NOCREDIT(ByVal guestroomno As String, ByVal gn As String) As Integer
        Try
            Dim SQL_query As String


            Dim objlog As LoggerService
            objlog = LoggerService.gtInstance


            Dim result As DataSet

            Dim ob As DbaseService
            ob = DbaseService.getInstance




            gn = UCase(gn)

            SQL_query = "select GuestNoBillPost  from Guest where  upper(GuestName) ='" & gn.Replace("'", "''") & "' and  GuestRoomNo = '" & guestroomno & "' and GuestStatus ='A' order by GuestId desc"

            Try


                result = ob.DsWithoutUpdate(SQL_query)
            Catch ex As Exception
                objlog.write2LogFile("Guest", "GuestERRUDT =" & ex.Message)
            End Try

            Try
                objlog.write2LogFile("NOCREDIT" & guestroomno, "Guestnobillpost=" & result.Tables(0).Rows(0)(0))
            Catch ex As Exception

            End Try



            '  objlog.write2LogFile("Process", SQL_query)
            If result.Tables(0).Rows.Count > 0 Then



                If result.Tables(0).Rows(0)(0) = True Then
                    Return 1
                End If
            Else





                Return 0
            End If
        Catch ex As Exception
            Return 0
        End Try




    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try

        Catch ex As Exception

        End Try




        Try
            If Not Session("us") Is Nothing Then

                userContext = Session("us")
                Dim objLogInOut1 As LogInOutService
                objLogInOut1 = LogInOutService.getInstance

                Plantime = objLogInOut1.GetLastPlanTime(userContext)
                Try
                    If userContext.item("usertype") = EUSERTYPE.COUPON Then

                        Plantime = objLogInOut1.GetCouponLastPlanTime(userContext.item("grcid"))
                        username.Visible = False
                        Try
                            x1.Visible = False
                            x2.Visible = False
                            Button4.Visible = False
                        Catch ex As Exception

                        End Try

                    Else
                        username.Visible = True
                    End If
                Catch ex As Exception

                End Try


                ' urp = userContext.requestedPage

                Try
                    hdRemainingTime.Value = Plantime
                    exp.Text = DateAdd(DateInterval.Second, Convert.ToDouble(hdRemainingTime.Value), DateTime.Now)
                    rno = userContext.roomNo
                    'hdRoomNo.Value = rno

                    ' hdMAC.Value = userContext.machineId

                    gn = GetLName(userContext.item("grcid"))
                Catch ex As Exception

                End Try


            Else
                Dim qs As String = ""
                Dim macaddr As String = ""
                Dim objLogInOut1 As LogInOutService
                objLogInOut1 = LogInOutService.getInstance
                Dim commonFun As PMSCommonFun

                commonFun = PMSCommonFun.getInstance
                Try
                    qs = Request.QueryString("encry")
                    macaddr = commonFun.DecrptQueryString("MA", qs)
                    'hdMAC.Value = macaddr

                    Plantime = objLogInOut1.GetLastPlanTime(macaddr)
                    hdRemainingTime.Value = Plantime
                    exp.Text = DateAdd(DateInterval.Second, Convert.ToDouble(hdRemainingTime.Value), DateTime.Now)
                    Dim ds As DataSet
                    ds = GetRoomNo(Macadd)

                    rno = ds.Tables(0).Rows(0)(0)
                    gn = ds.Tables(0).Rows(0)(1)

                    'hdRoomNo.Value = rno

                Catch ex As Exception

                End Try

                If Not IsPostBack() Then
                    Try
                        Dim pid As String
                        pid = GetPlanid(macaddr)

                        If pid = "101" Or pid = "201" Then
                            plandetails.Text = "Standard-Complementary Internet. "
                            Try
                                upgra.Visible = True
                                usage_details.Visible = True
                                rdoplan1.Visible = True
                                upgra.Visible = True
                                term.Visible = True
                                upgra1.Visible = True
                                Button4.Visible = False

                                Try
                                    rdoplan1.Items.Clear()
                                Catch ex As Exception

                                End Try
                                Try
                                    '  rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "Premium Service - 24-hour access" & "</span><span class=""WebRupee"">INR 900</span> " & "", 102))
                                    '  rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "Executive Service - 24-hour access" & "</span><span class=""WebRupee"">INR 500</span> " & "", 105))

                                    rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "Premium 12 Mbps at 499 INR/day" & "</span><span class=""WebRupee""></span> " & "", 202))

                                Catch ex As Exception

                                End Try


                            Catch ex As Exception

                            End Try

                        ElseIf pid = "202" Then
                            plandetails.Text = "Premium 12 Mbps per day" & " INR 499"
                        ElseIf pid = "302" Then
                            plandetails.Text = "Premium-Complimentary Internet"

                        ElseIf pid = "701" Then
                            plandetails.Text = "High Speed Internet Access"
                        ElseIf pid = "3" Then
                            plandetails.Text = "Premium Service - 24-hour access" & " INR 900"
                        ElseIf pid = "105" Then

                            Try
                                upgra.Visible = True
                                usage_details.Visible = True
                                ' rdoplan1.Visible = True
                                upgra.Visible = True
                                term.Visible = True
                                upgra1.Visible = True


                                Try
                                    ' rdoplan1.Items.Clear()
                                Catch ex As Exception

                                End Try

                                Try
                                    'rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "Premium Service - 24-hour access" & "</span><span class=""WebRupee"">INR 900</span> " & "", 102))
                                    ' rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "Executive Service - 24-hour access" & "</span><span class=""WebRupee"">INR 500</span> " & "", 105))



                                Catch ex As Exception

                                End Try


                            Catch ex As Exception

                            End Try

                            plandetails.Text = "Executive Service - 24-hour access" & " INR 500"

                        ElseIf pid = "5" Then
                            plandetails.Text = "Basic 2" & GetLocalResourceObject("p4").ToString() & " INR 1299"

                        ElseIf pid = "6" Then
                            plandetails.Text = "Basic 5" & GetLocalResourceObject("p4").ToString() & " INR 2899"

                        ElseIf pid = "7" Then
                            plandetails.Text = "Basic 7" & GetLocalResourceObject("p4").ToString() & " INR 3599"




                        ElseIf pid = "8" Then
                            plandetails.Text = "Premium 1" & GetLocalResourceObject("p2").ToString() & " INR 299"

                        ElseIf pid = "9" Then
                            plandetails.Text = "Premium 3" & GetLocalResourceObject("p3").ToString() & " INR 399"
                        ElseIf pid = "10" Then
                            plandetails.Text = "Premium 12 " & GetLocalResourceObject("p3").ToString() & " INR 699"
                        ElseIf pid = "11" Then
                            plandetails.Text = "Premium 1 " & GetLocalResourceObject("p1").ToString() & " INR 899"
                        ElseIf pid = "12" Then
                            plandetails.Text = "Premium 2 " & GetLocalResourceObject("p4").ToString() & " INR 1699"

                        ElseIf pid = "13" Then
                            plandetails.Text = "Premium 5" & GetLocalResourceObject("p4").ToString() & " INR 3399"
                        ElseIf pid = "14" Then
                            plandetails.Text = "Premium 7 " & GetLocalResourceObject("p4").ToString() & " INR 4099"
                        ElseIf pid = "15" Then
                            plandetails.Text = GetLocalResourceObject("p40").ToString()

                        End If


                    Catch ex As Exception

                    End Try
                End If
               


            End If

        Catch ex As Exception

        End Try



        If Not IsPostBack() Then
            If Not Session("us") Is Nothing Then
                If userContext.selectedPlanId = "101" Or userContext.selectedPlanId = "201" Then
                    plandetails.Text = "Standard Complimentary Internet. "
                    Try
                        upgra.Visible = True
                        usage_details.Visible = True
                        rdoplan1.Visible = True
                        upgra.Visible = True
                        term.Visible = True
                        upgra1.Visible = True


                        Try
                            rdoplan1.Items.Clear()
                        Catch ex As Exception

                        End Try

                        Try
                            '  rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "Premium Service - 24-hour access" & "</span><span class=""WebRupee"">INR 900</span> " & "", 102))
                            rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "Premium 12 Mbps at 499 INR/day" & "</span><span class=""WebRupee""></span> " & "", 202))



                        Catch ex As Exception

                        End Try


                    Catch ex As Exception

                    End Try

                ElseIf userContext.selectedPlanId = "202" Then
                    plandetails.Text = "Premium 12 Mbps per day" & " INR 499"

                ElseIf userContext.selectedPlanId = "302" Then
                    plandetails.Text = "Premium-Complimentary Internet"

                ElseIf userContext.selectedPlanId = "701" Then
                    plandetails.Text = "High Speed Internet Access"
                ElseIf userContext.selectedPlanId = "3" Then
                    plandetails.Text = "Premium Service - 24-hour access" & " INR 900"
                ElseIf userContext.selectedPlanId = "105" Then
                    Try
                        upgra.Visible = True
                        usage_details.Visible = True
                        ' rdoplan1.Visible = True
                        upgra.Visible = True
                        term.Visible = True
                        upgra1.Visible = True


                        Try
                            ' rdoplan1.Items.Clear()
                        Catch ex As Exception

                        End Try

                        Try
                            'rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "Premium Service - 24-hour access" & "</span><span class=""WebRupee"">INR 900</span> " & "", 102))
                            ' rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "Executive Service - 24-hour access" & "</span><span class=""WebRupee"">INR 500</span> " & "", 105))



                        Catch ex As Exception

                        End Try


                    Catch ex As Exception

                    End Try
                    plandetails.Text = "Executive Service - 24-hour access" & " INR 500"

                ElseIf userContext.selectedPlanId = "5" Then
                    plandetails.Text = "Basic 2" & GetLocalResourceObject("p4").ToString() & " INR 1299"

                ElseIf userContext.selectedPlanId = "6" Then
                    plandetails.Text = "Basic 5" & GetLocalResourceObject("p4").ToString() & " INR 2899"

                ElseIf userContext.selectedPlanId = "7" Then
                    plandetails.Text = "Basic 7" & GetLocalResourceObject("p4").ToString() & " INR 3599"




                ElseIf userContext.selectedPlanId = "8" Then
                    plandetails.Text = "Premium 1" & GetLocalResourceObject("p2").ToString() & " INR 299"

                ElseIf userContext.selectedPlanId = "9" Then
                    plandetails.Text = "Premium 3" & GetLocalResourceObject("p3").ToString() & " INR 399"
                ElseIf userContext.selectedPlanId = "10" Then
                    plandetails.Text = "Premium 12 " & GetLocalResourceObject("p3").ToString() & " INR 699"
                ElseIf userContext.selectedPlanId = "11" Then
                    plandetails.Text = "Premium 1 " & GetLocalResourceObject("p1").ToString() & " INR 899"
                ElseIf userContext.selectedPlanId = "12" Then
                    plandetails.Text = "Premium 2 " & GetLocalResourceObject("p4").ToString() & " INR 1699"

                ElseIf userContext.selectedPlanId = "13" Then
                    plandetails.Text = "Premium 5" & GetLocalResourceObject("p4").ToString() & " INR 3399"
                ElseIf userContext.selectedPlanId = "14" Then
                    plandetails.Text = "Premium 7 " & GetLocalResourceObject("p4").ToString() & " INR 4099"
                ElseIf userContext.selectedPlanId = "15" Then
                    plandetails.Text = GetLocalResourceObject("p40").ToString()

                End If


                Try
                    If plandetails.Text = "" Then

                        If userContext.item("planid") = "101" Or userContext.item("planid") = "201" Then
                            Try
                                upgra.Visible = True
                                usage_details.Visible = True
                                rdoplan1.Visible = True
                                upgra.Visible = True
                                term.Visible = True
                                upgra1.Visible = True


                                Try
                                    rdoplan1.Items.Clear()
                                Catch ex As Exception

                                End Try

                                Try
                                    ' rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "Premium Service - 24-hour access" & "</span><span class=""WebRupee"">INR 900</span> " & "", 102))
                                    '  rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "Executive Service - 24-hour access" & "</span><span class=""WebRupee"">INR 500</span> " & "", 105))

                                    rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "Premium 12 Mbps at 499 INR/day" & "</span><span class=""WebRupee""></span> " & "", 202))

                                Catch ex As Exception

                                End Try


                            Catch ex As Exception

                            End Try

                            plandetails.Text = "Complementary Service. "

                        ElseIf userContext.item("planid") = "2" Then
                            plandetails.Text = "Premium Service - 1 hour access" & " INR 400"
                        ElseIf userContext.item("planid") = "3" Then
                            plandetails.Text = "Premium Service - 24-hour access" & " INR 900"
                        ElseIf userContext.item("planid") = "105" Then

                            Try
                                upgra.Visible = True
                                usage_details.Visible = True
                                ' rdoplan1.Visible = True
                                upgra.Visible = True
                                term.Visible = True
                                upgra1.Visible = True


                                Try
                                    '  rdoplan1.Items.Clear()
                                Catch ex As Exception

                                End Try

                                Try
                                    ' rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "Premium Service - 24-hour access" & "</span><span class=""WebRupee"">INR 900</span> " & "", 102))
                                    ' rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "Executive Service - 24-hour access" & "</span><span class=""WebRupee"">INR 500</span> " & "", 105))



                                Catch ex As Exception

                                End Try


                            Catch ex As Exception

                            End Try
                            plandetails.Text = "Executive Service - 24-hour access" & " INR 500"

                        ElseIf userContext.item("planid") = "5" Then
                            plandetails.Text = "Basic 2" & GetLocalResourceObject("p4").ToString() & " INR 1299"

                        ElseIf userContext.item("planid") = "6" Then
                            plandetails.Text = "Basic 5" & GetLocalResourceObject("p4").ToString() & " INR 2899"

                        ElseIf userContext.item("planid") = "7" Then
                            plandetails.Text = "Basic 7" & GetLocalResourceObject("p4").ToString() & " INR 3599"




                        ElseIf userContext.item("planid") = "8" Then
                            plandetails.Text = "Premium 1" & GetLocalResourceObject("p2").ToString() & " INR 299"

                        ElseIf userContext.item("planid") = "9" Then
                            plandetails.Text = "Premium 3" & GetLocalResourceObject("p3").ToString() & " INR 399"
                        ElseIf userContext.item("planid") = "10" Then
                            plandetails.Text = "Premium 12 " & GetLocalResourceObject("p3").ToString() & " INR 699"
                        ElseIf userContext.item("planid") = "11" Then
                            plandetails.Text = "Premium 1 " & GetLocalResourceObject("p1").ToString() & " INR 899"
                        ElseIf userContext.item("planid") = "12" Then
                            plandetails.Text = "Premium 2 " & GetLocalResourceObject("p4").ToString() & " INR 1699"

                        ElseIf userContext.item("planid") = "13" Then
                            plandetails.Text = "Premium 5" & GetLocalResourceObject("p4").ToString() & " INR 3399"
                        ElseIf userContext.item("planid") = "14" Then
                            plandetails.Text = "Premium 7 " & GetLocalResourceObject("p4").ToString() & " INR 4099"
                        ElseIf userContext.item("planid") = "15" Then
                            plandetails.Text = GetLocalResourceObject("p40").ToString()

                        End If
                    End If


                Catch ex As Exception

                End Try


            End If
        End If




      

        Try


           

            
           


            If Session("us") Is Nothing Or gn = "" Then
                Dim qs As String = ""
                qs = Request.QueryString("encry")
                Dim macaddr As String
                Dim commonFun As PMSCommonFun
                Dim objlog As LoggerService
                objlog = LoggerService.gtInstance

                commonFun = PMSCommonFun.getInstance
                macaddr = commonFun.DecrptQueryString("MA", qs)
                'hdMAC.Value = macaddr
                Dim ds As DataSet
                ds = GetRoomNo(macaddr)
                'hdRoomNo.Value = rno

                rno = ds.Tables(0).Rows(0)(0)
                gn = ds.Tables(0).Rows(0)(1)

                Try
                    objlog.write2LogFile("subbu", "rno=" & rno & "Last" & gn)
                Catch ex As Exception

                End Try

                roomno.Text = rno

                Try
                    loginpwd = GetLName(ds.Tables(0).Rows(0)(2))
                    If loginpwd <> "" Then
                        lastname.Text = loginpwd

                    Else
                        lastname.Text = gn

                    End If

                Catch ex As Exception

                End Try



                'username.Text = gn & " Of " & rno
                username.Text = GetLocalResourceObject("s3").ToString()
            End If



        Catch ex As Exception

        End Try



        Try
            Dim qs As String = ""
            qs = Request.QueryString("encry")
            Dim macaddr As String
            Dim commonFun As PMSCommonFun
            Dim objlog As LoggerService
            objlog = LoggerService.gtInstance

            commonFun = PMSCommonFun.getInstance
            macaddr = commonFun.DecrptQueryString("MA", qs)


            Dim CBILL As String = ""

            CBILL = Getcouponbill(macaddr)


            Dim CPLAN As String = ""


            CPLAN = Getcouponplan(CBILL)


            Try
                objlog.write2LogFile("subbuNew", "Bill Id" & CBILL & "Plan type=" & CPLAN)
            Catch ex As Exception

            End Try


            If CPLAN = "2" Then

                x1.Visible = False
                x2.Visible = False
                '  plandetails.Text = "Complimentary"
                Button4.Visible = True
            ElseIf CPLAN = "1" Then

                Label1.Text = "Conference Code"

                x1.Visible = False
                x2.Visible = False
                Button4.Visible = True

            ElseIf CPLAN = "3" Then
                x1.Visible = False
                x2.Visible = False
                '  plandetails.Text = "Complimentary"
                Button4.Visible = True

                ' plandetails.Text = "Charges Applicable as per agreed Event Pricing"

            End If
           





        Catch ex As Exception

        End Try

        Try
            Dim commonFun As PMSCommonFun
            Dim objlog As LoggerService
            objlog = LoggerService.gtInstance
            Dim qs As String = ""
            qs = Request.QueryString("encry")
            Dim macaddr As String
            commonFun = PMSCommonFun.getInstance
            macaddr = commonFun.DecrptQueryString("MA", qs)
            Dim pid As String
            pid = GetPlanid(macaddr)

            objlog.write2LogFile("kapi", "planid=" & pid)


            If pid = "101" Or pid = "201" Then

                Try
                    upgra.Visible = True
                    usage_details.Visible = True
                    rdoplan1.Visible = True
                    upgra.Visible = True
                    term.Visible = True
                    upgra1.Visible = True
                Catch ex As Exception
                    objlog.write2LogFile("kapi", "err1" & ex.Message)

                End Try


            Else

                Try
                    upgra.Visible = False
                    usage_details.Visible = False
                    rdoplan1.Visible = False
                    upgra.Visible = False
                    term.Visible = False
                    upgra1.Visible = False
                Catch ex As Exception
                    objlog.write2LogFile("kapi", "err1" & ex.Message)

                End Try


            End If


            If pid = "101" Or pid = "201" Then
                Try
                    Try
                        rdoplan1.Items.Clear()
                    Catch ex As Exception
                        objlog.write2LogFile("kapi", "err2" & ex.Message)
                    End Try

                    Try
                        Try
                            rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "Premium 12 Mbps at 499 INR/day" & "</span><span class=""WebRupee""></span> " & "", 202))
                            ' rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "Premium Service - 24-hour access" & "</span><span class=""WebRupee"">INR 900</span> " & "", 102))
                            '  rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "Executive Service - 24-hour access" & "</span><span class=""WebRupee"">INR 500</span> " & "", 105))

                        Catch ex As Exception
                            objlog.write2LogFile("kapi", "err10" & ex.Message)

                        End Try

                        Try
                            upgra.Visible = True
                            usage_details.Visible = True
                            rdoplan1.Visible = True
                            upgra.Visible = True
                            term.Visible = True
                            upgra1.Visible = True
                        Catch ex As Exception
                            objlog.write2LogFile("kapi", "err1" & ex.Message)

                        End Try

                    Catch ex As Exception
                        objlog.write2LogFile("kapi", "err3" & ex.Message)
                    End Try
                Catch ex As Exception

                End Try

            End If

            If pid = "105" Then
                Try
                    Try
                        ' rdoplan1.Items.Clear()
                    Catch ex As Exception
                        objlog.write2LogFile("kapi", "err2" & ex.Message)
                    End Try

                    Try
                        Try
                            rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "Premium Service - 24-hour access" & "</span><span class=""WebRupee"">INR 900</span> " & "", 102))

                        Catch ex As Exception
                            objlog.write2LogFile("kapi", "err11" & ex.Message)
                        End Try
                        ' rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "Executive Service - 24-hour access" & "</span><span class=""WebRupee"">INR 500</span> " & "", 105))

                        Try
                            upgra.Visible = True
                            usage_details.Visible = True
                            rdoplan1.Visible = True
                            upgra.Visible = True
                            term.Visible = True
                            upgra1.Visible = True
                        Catch ex As Exception
                            objlog.write2LogFile("kapi", "err1" & ex.Message)

                        End Try

                    Catch ex As Exception
                        objlog.write2LogFile("kapi", "err3" & ex.Message)
                    End Try
                Catch ex As Exception

                End Try

            End If





        Catch ex As Exception

        End Try

        Try
            Dim ds As DataSet
            ds = GetRoomNo(Macadd)

            If IsNumeric(ds.Tables(0).Rows(0)(0)) Then



                lastname.Text = ds.Tables(0).Rows(0)(1)
            End If
        Catch ex As Exception

        End Try


    End Sub

    Public Function GetLName1(ByVal str As String) As String
        Dim db As DbaseService
        Try

            db = DbaseService.getInstance
        Catch ex As Exception

        End Try

        Dim Ctime As DateTime
        Ctime = Now


        Dim ds As DataSet
        ds = db.DsWithoutUpdate("select  Guestpwd from guestnew where gueststatus='A' and ExpTime > '" & Ctime & "' and guestid ='" & str & "'")

        If ds.Tables(0).Rows.Count > 0 Then
            Return ds.Tables(0).Rows(0)(0)
        End If





    End Function


    Public Function GetLName(ByVal str As String) As String
        Dim db As DbaseService
        Try

            db = DbaseService.getInstance
        Catch ex As Exception

        End Try

        Dim ds As DataSet
        ds = db.DsWithoutUpdate("select guestname from guest where guestid ='" & str & "'")

        If ds.Tables(0).Rows.Count > 0 Then
            Return ds.Tables(0).Rows(0)(0)
        End If





    End Function
    Protected Sub Button1_Click2(ByVal sender As Object, ByVal e As EventArgs)
        Dim macaddr As String = ""
        Dim qs As String = ""
        Dim objlog As LoggerService
        objlog = LoggerService.gtInstance
        Try
            Dim commonFun As PMSCommonFun
            commonFun = PMSCommonFun.getInstance
            qs = Request.QueryString("encry")
            macaddr = commonFun.DecrptQueryString("MA", qs)


            Dim ds As DataSet
            ds = GetRoomNo(macaddr)

            rno = ds.Tables(0).Rows(0)(0)
            gn = ds.Tables(0).Rows(0)(1)
            strGID = ds.Tables(0).Rows(0)(2)
            Try
                strRN = rno
                strLN = gn
                objlog.write2LogFile("cup", "name" & strLN & "RoomNo" & strRN & "gid=" & strGID)



                If strGID <> "" And strLN <> "" And strRN <> "" Then
                    Try
                        Session("aa") = strGID
                        Session("bb") = strRN
                        Session("cc") = strLN
                    Catch ex As Exception

                    End Try

                    Try
                        Response.Redirect("ChangePwd.aspx?" & GetRedirectQS() & "&rm=" & strRN & "&ln=" & strLN & "&gid=" & strGID & "&ct=" & Page.UICulture)
                    Catch ex As Exception

                    End Try



                End If


            Catch ex As Exception

            End Try
        Catch ex As Exception

        End Try

    End Sub

    Private Function GetRedirectQS()
        Dim redirectQS As String = ""
        redirectQS = "encry=" & Request.QueryString("encry")
        Return redirectQS
    End Function

    'Protected Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click
    '    Dim emailid As String = ""
    '    emailid = email.Text.Trim()

    '    Dim objlog As LoggerService
    '    objlog = LoggerService.gtInstance
    '    lblerr.Text = ""



    '    If Not Validator.ValidateEmail(emailid) Or emailid = "" Then
    '        ' Response.Redirect("UserError.aspx?" & url & "&Msg=" & "Dear Guest, Please enter Valid Email Address." & "&findurl=mifilogin")


    '        If Not Session("e1") Is Nothing Then
    '            lblerr.Text = Session("e1")
    '        Else
    '            lblerr.Text = "Please enter Valid Email Address."

    '        End If




    '    Else

    '        lblerr.Visible = True
    '        Try
    '            Dim objmail As MailService
    '            objmail = MailService.getInstance





    '            Try
    '                Dim qs As String = ""
    '                Dim macaddr As String = ""

    '                Dim commonFun As PMSCommonFun
    '                commonFun = PMSCommonFun.getInstance
    '                qs = Request.QueryString("encry")
    '                macaddr = commonFun.DecrptQueryString("MA", qs)
    '                Dim gt As String = ""
    '                Dim ds As DataSet
    '                ds = GetRoomNo(macaddr)

    '                gt = GetLName1(ds.Tables(0).Rows(0)(2))

    '                If gt <> "" Then
    '                    gn = gt
    '                End If

    '            Catch ex As Exception

    '            End Try


    '            Try
    '                objlog.write2LogFile("mailtest", "roomno" & rno & "Last Name" & gn & "emailid" & emailid)
    '            Catch ex As Exception

    '            End Try

    '            Try
    '                lblerr.Text = objmail.SendAdminMail(rno, gn, emailid)
    '            Catch ex As Exception
    '                objlog.write2LogFile("Mailg", "er" & ex.Message)
    '            End Try




    '            If lblerr.Text.Contains("sent") Then
    '                ' Label4.Text = Session("e2")
    '                lblerr.Text = ""

    '                If Not Session("e2") Is Nothing Then
    '                    Label4.Text = Session("e2")
    '                Else
    '                    Label4.Text = "Email sent successfully."

    '                End If



    '            Else
    '                If Not Session("e3") Is Nothing Then
    '                    lblerr.Text = Session("e3")
    '                Else
    '                    lblerr.Text = "Failed to send Email."

    '                End If
    '            End If


    '        Catch ex As Exception
    '            objlog.write2LogFile("Mail", "er" & ex.Message)
    '        End Try

    '    End If




    'End Sub

  
    Public Function GetLastPlanTime(ByVal MAC As String) As Long
        Dim SQL_query As String
        Dim LoginPlanTime As Long
        Dim UsedTime As Long
        Dim RemainingTime As Long
        Dim LoginTime As Date
        Dim RefResultset As DataSet
        Dim objDbase As DbaseService
        Dim objlog As LoggerService
        Dim _LoginExpTime As DateTime
        objlog = LoggerService.gtInstance

        SQL_query = "select LoginExpTime from logdetails where loginmac='" & MAC & "' order by loginid desc"
        Try
            LoginTime = Now()
            objDbase = DbaseService.getInstance
            RefResultset = objDbase.DsWithoutUpdate(SQL_query)
            If RefResultset.Tables(0).Rows.Count > 0 Then
                _LoginExpTime = RefResultset.Tables(0).Rows(0).Item("LoginExpTime")
                RemainingTime = DateDiff(DateInterval.Second, LoginTime, _LoginExpTime)




                Return RemainingTime
            Else
                RemainingTime = 0
            End If

        Catch ex As Exception
            RemainingTime = 0
            objlog.writeExceptionLogFile("PT_EXP", ex)
        End Try
        Return RemainingTime
    End Function
   
    Protected Sub DrpPlans1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DrpPlans1.SelectedIndexChanged
        Try
            Dim noofdays As Long
            noofdays = DrpPlans1.SelectedValue
            Dim amt As Integer

            amt = noofdays * 499

            Try
                rdoplan1.Items.Clear()

                Dim str As String = ""

                str = "Premium 12 Mbps at " & amt & " INR for " & noofdays & " day(s)"

                rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & str & "</span><span class=""WebRupee""></span> " & "", 202))
            Catch ex As Exception

            End Try




        Catch ex As Exception

        End Try



    End Sub
    Protected Sub lk(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton1.Click

        Try
            Dim commonFun As PMSCommonFun
            commonFun = PMSCommonFun.getInstance
            url = commonFun.BrowserQueryString(Request)
            Response.Redirect("terms.aspx?" & GetRedirectQS() & "&bk=e")

        Catch ex As Exception

        End Try




    End Sub




    Public Function GetPlanid(ByVal MAC As String) As Long
        Dim SQL_query As String
        Dim LoginPlanTime As Long
        Dim UsedTime As Long
        Dim RemainingTime As Long
        Dim LoginTime As Date
        Dim RefResultset As DataSet
        Dim objDbase As DbaseService
        Dim objlog As LoggerService
        Dim _LoginExpTime As DateTime
        objlog = LoggerService.gtInstance

        SQL_query = "select billplanid from bill where billmac='" & MAC & "' order by billid desc"
        Try
            LoginTime = Now()
            objDbase = DbaseService.getInstance
            RefResultset = objDbase.DsWithoutUpdate(SQL_query)

            Return RefResultset.Tables(0).Rows(0)(0)

        Catch ex As Exception
            RemainingTime = 0

        End Try
        Return RemainingTime
    End Function


    Public Function GetRoomNo(ByVal MAC As String) As DataSet
        Dim SQL_query As String
        Dim LoginPlanTime As Long
        Dim UsedTime As Long
        Dim RemainingTime As Long
        Dim LoginTime As Date
        Dim RefResultset As DataSet
        Dim objDbase As DbaseService
        Dim objlog As LoggerService
        Dim _LoginExpTime As DateTime
        objlog = LoggerService.gtInstance


        Try
            If MAC = "" Then

                Dim commonFun1 As PMSCommonFun
                commonFun1 = PMSCommonFun.getInstance

                Dim qs As String = ""

                Try
                    qs = Request.QueryString("encry")
                    MAC = commonFun1.DecrptQueryString("MA", qs)

                Catch ex As Exception

                End Try

            End If
        Catch ex As Exception

        End Try






        SQL_query = "select  guestroomno,guestname,guestid from guest where guestid in ( select top 1  billgrcid from bill  where billid in ( select top 1 loginbillid from logdetails where loginmac='" & MAC & "' order by LoginId desc))"
        Try

            objDbase = DbaseService.getInstance
            RefResultset = objDbase.DsWithoutUpdate(SQL_query)
            objlog.write2LogFile("subbu", SQL_query)

            If RefResultset.Tables(0).Rows.Count = 0 Then


                SQL_query = "select couponuserid, couponpassword,couponcount from coupons where couponid in ( select top 1  billgrcid from bill where billmac='" & MAC & "'   order by billid desc) "

                objDbase = DbaseService.getInstance
                RefResultset = objDbase.DsWithoutUpdate(SQL_query)


            End If



            Return RefResultset

        Catch ex As Exception

            Try
                objlog.write2LogFile("subbu", "err" & ex.Message)
            Catch ex1 As Exception

            End Try
        End Try

    End Function

    Public Function getcouponcount(ByVal MAC As String) As DataSet
        Dim SQL_query As String
        Dim LoginPlanTime As Long
        Dim UsedTime As Long
        Dim RemainingTime As Long
        Dim LoginTime As Date
        Dim RefResultset As DataSet
        Dim objDbase As DbaseService
        Dim objlog As LoggerService
        Dim _LoginExpTime As DateTime
        objlog = LoggerService.gtInstance

        SQL_query = "select couponcount from coupons where couponid in ( select top 1  billgrcid from bill  where billid in ( select top 1 loginbillid from logdetails where loginmac='" & MAC & "' order by LoginId desc))"
        Try

           
          

            ' SQL_query = "select couponcount from coupons where couponid in ( select top 1  billgrcid from bill where billmac='" & MAC & "'   order by billid desc) "

            objDbase = DbaseService.getInstance
            RefResultset = objDbase.DsWithoutUpdate(SQL_query)






            Return RefResultset

        Catch ex As Exception

            Try
                objlog.write2LogFile("subbu", "err" & ex.Message)
            Catch ex1 As Exception

            End Try
        End Try

    End Function

    
  
    Protected Sub Button3_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button3.Click
        Dim noofdays As Integer = 1

        Try
            Session("back") = ""
        Catch ex As Exception

        End Try

        Dim objlog As LoggerService
        Label1.Text = ""
        objlog = LoggerService.gtInstance
        Dim commonFun As PMSCommonFun
        commonFun = PMSCommonFun.getInstance
        planId = 0
        Dim qs As String = Request.QueryString("encry")
        url = commonFun.BrowserQueryString(Request)
        Dim roomno As String = ""
        Dim pwd As String = ""
        Dim RN As String = ""
        RN = commonFun.DecrptQueryString("RN", qs)
        RN = ""

        Label1.Visible = False
        Try
            planId = rdoplan1.SelectedValue
        Catch ex As Exception

        End Try
        Try
            objlog.write2LogFile("ps_Session" & commonFun.DecrptQueryString("MA", qs), "Selected Plan in btn event planid " & planId & " Roomno" & roomno & " pwd" & pwd)
        Catch ex As Exception

        End Try
        planId = 102

        If planId <= 0 Then
            Label1.Visible = True
            Label1.Text = GetLocalResourceObject("p1").ToString()
            'hdAccept.Value = "0"
            lblerr1.Visible = True
            lblerr1.Text = "Please select a plan"

            'Response.Redirect("UserError.aspx?" & url & "&Msg=" & "Dear Guest, Please select a Package." & "&findurl=mifilogin")

        Else
            lblerr1.Text = ""

            Try
                Session("splan") = planId
            Catch ex As Exception

            End Try



            Try

                noofdays = DrpPlans1.SelectedValue


            Catch ex As Exception

            End Try



            ' If RN <> "" Then
            ' Dim roomno As String = ""
            ' Dim pwd As String = ""

            Try
                roomno = Session("roomno")
                pwd = Session("pwd")

            Catch ex As Exception

            End Try
            Try
                objlog.write2LogFile("ps_Session" & commonFun.DecrptQueryString("MA", qs), "Selected Plan in btn event planid " & planId & " Roomno" & roomno & " pwd" & pwd)
            Catch ex As Exception

            End Try


            Dim r1 As String = ""
            Dim r2 As String = ""

            Try
                r1 = Request.QueryString("rm")
                r2 = Request.QueryString("ln")
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
            'ObjElog.write2LogFile("Mac_" & commonFun.DecrptQueryString("MA", qs), "MIFI before split r2" & r2)

            Try
                Dim str2() = r2.Split(",")
                Dim ind As Integer = 0
                ind = str2.Length - 1

                r2 = str2(ind)
                ' ObjElog.write2LogFile("Mac_" & commonFun.DecrptQueryString("MA", qs), "MIFI after split r2" & r2)

            Catch ex As Exception

            End Try


            Try
                ' objlog.write2LogFile("ps_Query" & commonFun.DecrptQueryString("MA", qs), "Selected Plan in btn event planid " & planId & "query Roomno" & r1 & "query pwd" & r2)
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

            Dim serverip As String = ""

            Try
                Dim objSysConfig As New CSysConfig
                serverip = objSysConfig.GetConfig("BillServer_IP")
                'objlog.write2LogFile("serverip", "http://" & serverIP & "/mob/Welcome.aspx?")
            Catch ex As Exception

            End Try


            Try
                Response.Redirect("http://" & serverip & "/upgrade/mifilogin.aspx?" & GetRedirectQS() & "&plan=" & planId & "&rm=" & roomno & "&ln=" & pwd & "&nd=" & noofdays & "&ct=" & Page.UICulture)
            Catch ex As Exception

            End Try
        End If



    End Sub










    Public Function GetPlanname(ByVal bill As String) As String
        Dim SQL_query As String

        Dim RefResultset As DataSet
        Dim objDbase As DbaseService
        Dim objlog As LoggerService
        Dim _LoginExpTime As String = ""
        objlog = LoggerService.gtInstance

        SQL_query = "select BillPlanId, (select planname from plans where planid=billplanid) from bill  where billid='" & bill & "' and billtype=2"
        Try

            objDbase = DbaseService.getInstance
            RefResultset = objDbase.DsWithoutUpdate(SQL_query)
            If RefResultset.Tables(0).Rows.Count > 0 Then
                _LoginExpTime = RefResultset.Tables(0).Rows(0)(1)




                Return _LoginExpTime
            Else
                Return "0"
            End If

        Catch ex As Exception
            Return _LoginExpTime
            objlog.writeExceptionLogFile("PT_EXP", ex)
        End Try
        Return _LoginExpTime
    End Function

    Public Function Getstart(ByVal bill As String) As String
        Dim SQL_query As String

        Dim RefResultset As DataSet
        Dim objDbase As DbaseService
        Dim objlog As LoggerService
        Dim _LoginExpTime As String = ""
        objlog = LoggerService.gtInstance

        SQL_query = "select billtime from bill  where billid='" & bill & "' and billtype=2"
        Try

            objDbase = DbaseService.getInstance
            RefResultset = objDbase.DsWithoutUpdate(SQL_query)
            If RefResultset.Tables(0).Rows.Count > 0 Then
                _LoginExpTime = RefResultset.Tables(0).Rows(0)(0)




                Return _LoginExpTime
            Else
                Return "0"
            End If

        Catch ex As Exception
            Return _LoginExpTime
            objlog.writeExceptionLogFile("PT_EXP", ex)
        End Try
        Return _LoginExpTime
    End Function

    Public Function Getend(ByVal bill As String) As String
        Dim SQL_query As String

        Dim RefResultset As DataSet
        Dim objDbase As DbaseService
        Dim objlog As LoggerService
        Dim _LoginExpTime As String = ""
        objlog = LoggerService.gtInstance

        SQL_query = "select loginexptime from logdetails  where loginbillid=" & bill
        Try

            objDbase = DbaseService.getInstance
            RefResultset = objDbase.DsWithoutUpdate(SQL_query)
            If RefResultset.Tables(0).Rows.Count > 0 Then
                _LoginExpTime = RefResultset.Tables(0).Rows(0)(0)




                Return _LoginExpTime
            Else
                Return "0"
            End If

        Catch ex As Exception
            Return _LoginExpTime
            objlog.writeExceptionLogFile("PT_EXP", ex)
        End Try
        Return _LoginExpTime
    End Function

    Public Function Getcouponplan(ByVal bill As String) As String
        Dim SQL_query As String

        Dim RefResultset As DataSet
        Dim objDbase As DbaseService
        Dim objlog As LoggerService
        Dim _LoginExpTime As String = ""
        objlog = LoggerService.gtInstance

        SQL_query = "select BillPlanId, (select plantype from plans where planid=billplanid) from bill  where billid='" & bill & "' and billtype=2"
        Try

            objDbase = DbaseService.getInstance
            RefResultset = objDbase.DsWithoutUpdate(SQL_query)
            If RefResultset.Tables(0).Rows.Count > 0 Then
                _LoginExpTime = RefResultset.Tables(0).Rows(0)(1)




                Return _LoginExpTime
            Else
                Return "0"
            End If

        Catch ex As Exception
            Return _LoginExpTime
            objlog.writeExceptionLogFile("PT_EXP", ex)
        End Try
        Return _LoginExpTime
    End Function



    Public Function Getcouponbill(ByVal MAC As String) As String
        Dim SQL_query As String

        Dim RefResultset As DataSet
        Dim objDbase As DbaseService
        Dim objlog As LoggerService
        Dim _LoginExpTime As String = ""
        objlog = LoggerService.gtInstance

        SQL_query = "select loginbillid from logdetails where loginmac='" & MAC & "' and loginexptime > getdate() order by loginid desc"
        Try

            objDbase = DbaseService.getInstance
            RefResultset = objDbase.DsWithoutUpdate(SQL_query)
            If RefResultset.Tables(0).Rows.Count > 0 Then
                _LoginExpTime = RefResultset.Tables(0).Rows(0).Item("loginbillid")




                Return _LoginExpTime
            Else
                Return "0"
            End If

        Catch ex As Exception
            Return _LoginExpTime
            objlog.writeExceptionLogFile("PT_EXP", ex)
        End Try
        Return _LoginExpTime
    End Function








    Public Function GetLastPlanTime1(ByVal MAC As String) As String
        Dim SQL_query As String

        Dim RefResultset As DataSet
        Dim objDbase As DbaseService
        Dim objlog As LoggerService
        Dim _LoginExpTime As String = ""
        objlog = LoggerService.gtInstance

        SQL_query = "select loginbillid from logdetails where loginmac='" & MAC & "' order by loginid desc"
        Try

            objDbase = DbaseService.getInstance
            RefResultset = objDbase.DsWithoutUpdate(SQL_query)
            If RefResultset.Tables(0).Rows.Count > 0 Then
                _LoginExpTime = RefResultset.Tables(0).Rows(0).Item("loginbillid")




                Return _LoginExpTime
            Else
                Return "0"
            End If

        Catch ex As Exception
            Return _LoginExpTime
            objlog.writeExceptionLogFile("PT_EXP", ex)
        End Try
        Return _LoginExpTime
    End Function

    'Protected Sub lk_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lk.Click
    '    Try
    '        Dim commonFun As PMSCommonFun
    '        commonFun = PMSCommonFun.getInstance


    '        Dim qs As String = ""
    '        Dim macadd As String = ""
    '        Dim bid As String = ""
    '        Try
    '            qs = Request.QueryString("encry")
    '            macadd = commonFun.DecrptQueryString("MA", qs)
    '            bid = GetLastPlanTime1(macadd)
    '        Catch ex As Exception

    '        End Try



    '        url = commonFun.BrowserQueryString(Request)
    '        Response.Redirect("SndRevBytes.aspx?" & GetRedirectQS() & "&bid=" & bid)

    '    Catch ex As Exception

    '    End Try
    'End Sub

    Protected Sub Button4_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button4.Click
        Try
            Dim commonFun As PMSCommonFun
            commonFun = PMSCommonFun.getInstance
            Dim qs As String = ""
            Dim macaddr As String
            Try
                qs = Request.QueryString("encry")
                macaddr = commonFun.DecrptQueryString("MA", qs)

                Dim aaa As AAAService
                aaa = AAAService.getInstance

                aaa.logmeout(macaddr)

            Catch ex As Exception

            End Try

            Try
                Response.Redirect("connect.aspx?" & GetRedirectQS())
            Catch ex As Exception

            End Try

        Catch ex As Exception

        End Try
    End Sub
End Class