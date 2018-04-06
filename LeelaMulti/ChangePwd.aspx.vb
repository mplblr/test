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
Partial Public Class ChangePwd
    Inherits System.Web.UI.Page
    Dim strRN As String = ""
    Dim strLN As String = ""
    Dim strGID As String = ""
    Dim newpwd As String = ""
    Private PMSName As PMSNAMES
    Private PMSVersion As String
    Private CouponVersion As String
    Private planId As Long = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        Dim img As ImageButton = DirectCast(Master.FindControl("LinkButton8"), ImageButton)
        img.ImageUrl = GetLocalResourceObject("C1").ToString()


        Dim img1 As ImageButton = DirectCast(Master.FindControl("LinkButton9"), ImageButton)
        img1.ImageUrl = GetLocalResourceObject("C2").ToString()


        'Dim img2 As ImageButton = DirectCast(Master.FindControl("LinkButton10"), ImageButton)
        'img2.ImageUrl = GetLocalResourceObject("C3").ToString()





        Button1.Text = GetLocalResourceObject("m11").ToString()
        'Label1.Text = GetLocalResourceObject("m7").ToString()
        'Label2.Text = GetLocalResourceObject("m8").ToString()


        Button2.Text = GetLocalResourceObject("f3").ToString()
        Label4.Text = GetLocalResourceObject("f1").ToString()
        Label5.Text = GetLocalResourceObject("f2").ToString()

        Label1.Text = GetLocalResourceObject("h3").ToString()
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click
        Dim exptime As String = ""
        Dim commonFun As PMSCommonFun
        commonFun = PMSCommonFun.getInstance


        Dim mac As String = ""

        Dim objlog As LoggerService
        objlog = LoggerService.gtInstance


        Dim qs As String = ""

        Try
            qs = Request.QueryString("encry")
            mac = commonFun.DecrptQueryString("MA", qs)
            exptime = GetLastPlanTime(mac)
        Catch ex As Exception

        End Try











        Dim r1 As String = ""
        Dim r2 As String = ""
        Dim r3 As String = ""


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
            '  objlog.write2LogFile("CPWD" & commonFun.DecrptQueryString("MA", qs), " Roomno" & r1 & " pwd" & r2)
        Catch ex As Exception

        End Try



        Try
            If r1 <> "" And r2 <> "" Then
                If strRN = "" Then
                    strRN = r1
                End If
                If strLN = "" Then
                    strLN = r2
                End If




            End If


            newpwd = TextBox1.Text.Trim()

            Try
                'objlog.write2LogFile("CPWD" & commonFun.DecrptQueryString("MA", qs), "Final GID " & strGID & " Roomno" & strRN & " pwd" & strLN & "newPwd" & newpwd & "exptime" & exptime)
            Catch ex As Exception

            End Try

            Dim p1 As String = TextBox1.Text.Trim



            If TextBox1.Text.Trim <> TextBox2.Text.Trim Then
                Label6.Text = GetLocalResourceObject("z2").ToString()
                Return

            ElseIf TextBox3.Text = "" Then

                Label6.Text = GetLocalResourceObject("p1").ToString()
                Return


            ElseIf p1.Length < 6 Then
                Label6.Text = GetLocalResourceObject("p2").ToString()

                Return



            ElseIf UCase(p1) = UCase(r2) Then
                Label6.Text = GetLocalResourceObject("p3").ToString()

                Return


            Else


                Try
                    objlog.write2LogFile(r1, "Password set " & strGID & " Roomno" & r1 & " pwd" & r2 & "newPwd" & p1 & "email" & TextBox3.Text.Trim())
                Catch ex As Exception

                End Try


                Try
                    setpwd(r1, r2, p1, TextBox3.Text.Trim())
                Catch ex As Exception

                End Try

                Try
                    setpwd1(r1, r2, p1)
                Catch ex As Exception

                End Try

                Dim logi As Long = 0

                Try
                    logi = login_Click(r1, r2)  '------'remaining time
                Catch ex As Exception
                    objlog.write2LogFile("login", "err" & ex.Message)
                End Try


                If logi > 0 Then

                    Try
                        Response.Redirect("MifiLogin.aspx?" & GetRedirectQS() & "&plan=" & planId & "&rm=" & r1 & "&ln=" & r2 & "&ct=" & Page.UICulture)
                    Catch ex As Exception

                    End Try

                Else

                    Try
                        Response.Redirect("PlanSel.aspx?" & GetRedirectQS() & "&plan=" & planId & "&rm=" & r1 & "&ln=" & r2 & "&ct=" & Page.UICulture)
                    Catch ex As Exception

                    End Try

                End If



            End If




            'If TextBox1.Text <> TextBox2.Text Then
            '    Label6.Text = "Dear Guest,New Password and Confirm New Password doest match."
            'Else
            '    newpwd = TextBox1.Text.Trim()
            'End If












        Catch ex As Exception

        End Try



        

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



    Public Function setpwd(ByVal rno As String, ByVal name As String, ByVal pwd As String, ByVal email As String) As String

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

                sq = "Update Guest set pwd='" & pwd.Replace("'", "''") & "' , email='" & email & "'   where GuestName='" & name.Replace("'", "''") & "'  and GuestRoomNo='" & rno & "'  and Gueststatus='A'   "

                db.DsWithoutUpdate(sq)





              

                Return "-1"


            Catch ex As Exception
                Return "-1"
            End Try


        Catch ex As Exception
            Return "-1"
        End Try


    End Function

    Public Function setpwd1(ByVal rno As String, ByVal name As String, ByVal pwd As String) As String

        Try
            Dim db As DbaseService
            Dim result As DataTable
            Try

                Try

                    db = DbaseService.getInstance
                Catch ex As Exception
                    Return "-1"
                End Try

                Dim sq As String = ""

                sq = "insert into temppwd(pwd,roomno,name) values('" & pwd & "','" & rno & "','" & name & "')"
                db.DsWithoutUpdate(sq)
              



            Catch ex As Exception
                Return "-1"
            End Try


        Catch ex As Exception
            Return "-1"
        End Try


    End Function



    Public Function GetLastPlanTime(ByVal MAC As String) As String
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




                Return _LoginExpTime.ToString()
            Else
                RemainingTime = 0
            End If

        Catch ex As Exception
            RemainingTime = 0
            objlog.writeExceptionLogFile("PT_EXP", ex)
        End Try
        Return RemainingTime
    End Function



    Public Sub insetpwd(ByVal roomno As String, ByVal lastname As String, ByVal gid As String, ByVal newpwd As String, ByVal exptime As String)
        Try
            Dim SQL_query As String
           
            Dim RefResultset As DataSet

            Dim count As Integer = 0


            Dim objDbase As DbaseService
            objDbase = DbaseService.getInstance
            Dim objlog As LoggerService
            objlog = LoggerService.gtInstance

            Dim sq As String = ""

            sq = "select * from guestnew where guestid =" & gid

            Try
                RefResultset = objDbase.DsWithoutUpdate(sq)

                count = RefResultset.Tables(0).Rows.Count
            Catch ex As Exception

            End Try

            Try
                SQL_query = "delete from guestnew where guestid=" & gid
                objDbase.insertUpdateDelete(SQL_query)
            Catch ex As Exception

            End Try
            

            SQL_query = "Insert into guestnew (GuestStatus,GuestName, GuestRoomNo,  exptime, guestid, Guestpwd ) Values ('A','" & lastname.Replace("'", "''") & "', '" & roomno & "', '" & exptime & "', " & gid & ",'" & newpwd.Replace("'", "''") & "'   )"

            Try
                count = objDbase.insertUpdateDelete(SQL_query)
            Catch ex As Exception

            End Try





            If count > 0 Then
                Label6.Text = "Dear Guest, Login Password changed successfully"

                Try
                    Response.Redirect("PostLogin.aspx?" & GetRedirectQS() & "&ct=" & Page.UICulture)
                Catch ex As Exception

                End Try
            Else
                Label6.Text = "Dear Guest, Login Password not set"
            End If

        Catch ex As Exception

        End Try



    End Sub



    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button2.Click
        Try
            Response.Redirect("PostLogin.aspx?" & GetRedirectQS() & "&ct=" & Page.UICulture)
        Catch ex As Exception

        End Try
    End Sub

    Private Function GetRedirectQS()
        Dim redirectQS As String = ""
        redirectQS = "encry=" & Request.QueryString("encry")
        Return redirectQS
    End Function

End Class