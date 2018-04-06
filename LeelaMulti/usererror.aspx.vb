Imports PMSPkgSql

Partial Public Class usererror
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
    Dim pgCookie As New CCookies
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim objlog As LoggerService
        objlog = LoggerService.gtInstance

        'Put user code to initialize the page here
        Dim objSysConfig As New CSysConfig
        Dim commonFun As PMSCommonFun


        Try
            If Not Session("cu") Is Nothing Then
                Page.UICulture = Session("cu")
            End If

        Catch ex As Exception

        End Try

        commonFun = PMSCommonFun.getInstance
        url = commonFun.BrowserQueryString(Request)
        Dim mac As String = ""
        Dim qs As String = Request.QueryString("encry")
        mac = commonFun.DecrptQueryString("MA", qs)
        Dim mmm As String = ""

        mmm = Request.QueryString("Msg")

        ' objlog.write2LogFile("Mac_" & commonFun.DecrptQueryString("MA", qs), "MIFI before split mmm" & mmm)

        Try
            Dim str1() = mmm.Split(",")
            Dim ind As Integer = 0
            ind = str1.Length - 1

            mmm = str1(ind)
            ' objlog.write2LogFile("Mac_" & commonFun.DecrptQueryString("MA", qs), "MIFI after split mmm" & mmm)

        Catch ex As Exception

        End Try


        Try
            If mmm.Contains("technical error") Then
                msgPara.InnerText = GetLocalResourceObject("m1").ToString()

            ElseIf mmm.Contains("vacant") Then
                msgPara.InnerText = "Room Vacant, for further assistance please call reception"



            ElseIf mmm.Contains("You have entered an invalid room number, for further assistance please call 0 / 1510") Then
                msgPara.InnerText = GetLocalResourceObject("m2").ToString()

            ElseIf mmm.Contains("Please enter LastName/Roomno") Then
                msgPara.InnerText = GetLocalResourceObject("m3").ToString()


            ElseIf mmm.Contains("COUPON MAXIMUM USERS LIMIT REACHED") Then
                msgPara.InnerText = GetLocalResourceObject("m4").ToString()


            ElseIf mmm.Contains("The Selected COUPON has EXPIRED") Then
                msgPara.InnerText = GetLocalResourceObject("m5").ToString()


            ElseIf mmm.Contains("Invalid User Id / Password.Try again") Then
                msgPara.InnerText = GetLocalResourceObject("m6").ToString()



            ElseIf mmm.Contains("Please enter Valid Room Number") Then
                msgPara.InnerText = GetLocalResourceObject("m7").ToString()


            ElseIf mmm.Contains("Please enter Valid Last Name") Then
                msgPara.InnerText = GetLocalResourceObject("m8").ToString()

            ElseIf mmm.Contains("Please purchase plan by logging") Then
                msgPara.InnerText = GetLocalResourceObject("m10").ToString()

            ElseIf mmm.Contains("Please select a Package") Then
                msgPara.InnerText = GetLocalResourceObject("m9").ToString()

            ElseIf mmm.Contains("Please enter Valid Email Address") Then
                msgPara.InnerText = GetLocalResourceObject("m20").ToString()
            ElseIf mmm.Contains("The package which you had selected has expired") Then
                msgPara.InnerText = GetLocalResourceObject("m15").ToString()
            ElseIf mmm.Contains("Access") Then
                msgPara.InnerText = GetLocalResourceObject("k1").ToString()

            Else
                msgPara.InnerText = GetLocalResourceObject("m15").ToString()
            End If
        Catch ex As Exception

        End Try







        Dim img As ImageButton = DirectCast(Master.FindControl("LinkButton8"), ImageButton)
        img.ImageUrl = GetLocalResourceObject("C1").ToString()


        Dim img1 As ImageButton = DirectCast(Master.FindControl("LinkButton9"), ImageButton)
        img1.ImageUrl = GetLocalResourceObject("C2").ToString()


        'Dim img2 As ImageButton = DirectCast(Master.FindControl("LinkButton10"), ImageButton)
        'img2.ImageUrl = GetLocalResourceObject("C3").ToString()






    End Sub




    Private Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        Dim objlog As LoggerService
        objlog = LoggerService.gtInstance

        'Put user code to initialize the page here
        Dim objSysConfig As New CSysConfig
        Dim commonFun As PMSCommonFun

        Try
            If Not Session("cu") Is Nothing Then
                Page.UICulture = Session("cu")
            End If

        Catch ex As Exception

        End Try

        commonFun = PMSCommonFun.getInstance
        url = commonFun.BrowserQueryString(Request)
        Dim mac As String = ""
        Dim qs As String = Request.QueryString("encry")
        mac = commonFun.DecrptQueryString("MA", qs)
        Dim mmm As String = ""

        mmm = Request.QueryString("Msg")

        ' objlog.write2LogFile("Mac_" & commonFun.DecrptQueryString("MA", qs), "MIFI before split mmm" & mmm)

        Try
            Dim str1() = mmm.Split(",")
            Dim ind As Integer = 0
            ind = str1.Length - 1

            mmm = str1(ind)
            ' objlog.write2LogFile("Mac_" & commonFun.DecrptQueryString("MA", qs), "MIFI after split mmm" & mmm)

        Catch ex As Exception

        End Try


        Try
            If mmm.Contains("technical error") Then
                msgPara.InnerText = GetLocalResourceObject("m1").ToString()
            ElseIf mmm.Contains("vacant") Then
                msgPara.InnerText = "Room Vacant, for further assistance please call reception"


            ElseIf mmm.Contains("You have entered an invalid room number") Then
                msgPara.InnerText = GetLocalResourceObject("m2").ToString()

            ElseIf mmm.Contains("Please enter LastName/Roomno") Then
                msgPara.InnerText = GetLocalResourceObject("m3").ToString()


            ElseIf mmm.Contains("COUPON MAXIMUM USERS LIMIT REACHED") Then
                msgPara.InnerText = GetLocalResourceObject("m4").ToString()


            ElseIf mmm.Contains("The Selected COUPON has EXPIRED") Then
                msgPara.InnerText = GetLocalResourceObject("m5").ToString()


            ElseIf mmm.Contains("Invalid User Id / Password.Try again") Then
                msgPara.InnerText = GetLocalResourceObject("m6").ToString()



            ElseIf mmm.Contains("Please enter Valid Room Number") Then
                msgPara.InnerText = GetLocalResourceObject("m7").ToString()


            ElseIf mmm.Contains("Please enter Valid Last Name") Then
                msgPara.InnerText = GetLocalResourceObject("m8").ToString()

            ElseIf mmm.Contains("Please purchase plan by logging") Then
                msgPara.InnerText = GetLocalResourceObject("m10").ToString()

            ElseIf mmm.Contains("Please select a Package") Then
                msgPara.InnerText = GetLocalResourceObject("m9").ToString()

            ElseIf mmm.Contains("Please enter Valid Email Address") Then
                msgPara.InnerText = GetLocalResourceObject("m20").ToString()
            ElseIf mmm.Contains("The package which you had selected has expired") Then
                msgPara.InnerText = GetLocalResourceObject("m15").ToString()

            ElseIf mmm.Contains("Session Expired") Then
                msgPara.InnerText = GetLocalResourceObject("k1").ToString()

            ElseIf mmm.Contains("for further assistance") Then
                msgPara.InnerText = GetLocalResourceObject("m2").ToString()

            ElseIf mmm.Contains("Access") Then
                msgPara.InnerText = "Dear Guest, You are not authorized to access Internet, for further assistance please call reception. "



                Dim img2 As ImageButton = DirectCast(Master.FindControl("LinkButton8"), ImageButton)
                ' img2.ImageUrl = GetLocalResourceObject("C1").ToString()


                Dim img3 As ImageButton = DirectCast(Master.FindControl("LinkButton9"), ImageButton)
                ' img3.ImageUrl = GetLocalResourceObject("C2").ToString()

              

                img2.Enabled = False

                img3.Enabled = False

                Try
                    Dim s1 As HtmlControl = DirectCast(Master.FindControl("left_panel"), HtmlControl)
                    s1.Visible = False
                Catch ex As Exception

                End Try


            End If
        Catch ex As Exception

        End Try







        Dim img As ImageButton = DirectCast(Master.FindControl("LinkButton8"), ImageButton)
        img.ImageUrl = GetLocalResourceObject("C1").ToString()


        Dim img1 As ImageButton = DirectCast(Master.FindControl("LinkButton9"), ImageButton)
        img1.ImageUrl = GetLocalResourceObject("C2").ToString()


        'Dim img2 As ImageButton = DirectCast(Master.FindControl("LinkButton10"), ImageButton)
        'img2.ImageUrl = GetLocalResourceObject("C3").ToString()






    End Sub

    Private Function GetRedirectQS()
        Dim redirectQS As String = ""
        redirectQS = "encry=" & Request.QueryString("encry")
        Return redirectQS
    End Function

End Class