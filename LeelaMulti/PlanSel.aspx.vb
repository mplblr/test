Imports PMSPkgSql
Imports System.Security
Imports System.Security.Policy
Imports security

Partial Public Class PlanSel
    Inherits System.Web.UI.Page
    Private planId As Long
    Private PMSName As PMSNAMES
    Private PMSVersion As String
    Private CouponVersion As String
    Public url As String
    Private accesstype As Integer
    Private sql_query As String
    Dim pgCookie As New CCookies
    Dim planamount As String
    Dim nasip As String
    Dim wd As String = ""
    Dim basic1 As HtmlControl
    Dim flag As Boolean = False

    

    Private Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        Dim member As String = ""
        Dim rate As String = ""
        Dim intM As Integer = 0
        Dim intR As Integer = 0
        Dim intP As Integer = 0


        Dim stay As Integer = 0
        Try
            Dim img As ImageButton = DirectCast(Master.FindControl("LinkButton8"), ImageButton)
            img.ImageUrl = GetLocalResourceObject("C1").ToString()

            Dim img1 As ImageButton = DirectCast(Master.FindControl("LinkButton9"), ImageButton)
            img1.ImageUrl = GetLocalResourceObject("C2").ToString()
            'Dim img2 As ImageButton = DirectCast(Master.FindControl("LinkButton10"), ImageButton)
            '' img2.ImageUrl = GetLocalResourceObject("C3").ToString()

            'img2.Visible = False

            'Dim img2 As ImageButton = DirectCast(Master.FindControl("LinkButton10"), ImageButton)
            'img2.ImageUrl = GetLocalResourceObject("C3").ToString()

            Button2.Text = GetLocalResourceObject("m12").ToString()

        Catch ex As Exception

        End Try


        
        
        Try


        Catch ex As Exception

        End Try



        Dim objSysConfig As New CSysConfig

        Dim objPlan As New CPlan

        Dim RN As String = ""
        Dim grcid As String = ""
        Dim objlog As LoggerService
        objlog = LoggerService.gtInstance()


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

        Dim roomno As String = ""
        Dim pwd As String = ""

        Dim comp As String = ""



        Try
            Dim objSysConfig2 As New CSysConfig
            Try
                isMac2 = objSysConfig2.isBCMachine(commonFun.DecrptQueryString("MA", qs))

                Try
                    roomno = Session("roomno")
                    pwd = Session("pwd")

                Catch ex As Exception

                End Try

                Try
                    ' objlog.write2LogFile("ps_Session" & commonFun.DecrptQueryString("MA", qs), "Selected Plan in btn event planid " & planId & " Roomno" & roomno & " pwd" & pwd)
                Catch ex As Exception

                End Try



                Try
                    basic1.Visible = False
                    qty.Visible = False
                Catch ex As Exception

                End Try

                Dim lt As New ListItem()

                Dim r1 As String = ""
                Dim r2 As String = ""
                Dim r3 As String = ""

                Try
                    r1 = Request.QueryString("rm")
                    r2 = Request.QueryString("ln")
                    r3 = Request.QueryString("comp")
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
                    Dim str2() = r3.Split(",")
                    Dim ind As Integer = 0
                    ind = str2.Length - 1

                    r3 = str2(ind)
                    ' ObjElog.write2LogFile("Mac_" & commonFun.DecrptQueryString("MA", qs), "MIFI after split r2" & r2)

                Catch ex As Exception

                End Try

                Try
                    'objlog.write2LogFile("ps_Query" & commonFun.DecrptQueryString("MA", qs), "Selected Plan in btn event planid " & planId & "query Roomno" & r1 & "query pwd" & r2 & "comp" & comp)
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
                    nt.Visible = False
                Catch ex As Exception

                End Try

                Try
                    comp = Session("comp")
                    If comp = "" Then
                        comp = r3
                    End If

                Catch ex As Exception

                End Try


                Try
                    objlog.write2LogFile("ps_Query" & commonFun.DecrptQueryString("MA", qs), "Selected Plan in btn event planid " & planId & "query Roomno" & r1 & "query pwd" & r2 & "comp" & comp)
                Catch ex As Exception

                End Try


                Try
                    stay = GetGuestDaysStay(roomno, pwd)
                    objlog.write2LogFile(roomno, "No Of stay=" & stay)
                Catch ex As Exception

                End Try




                Try
                    intM = getMember(roomno, pwd)

                    ' objlog.write2LogFile("member", member)
                Catch ex As Exception

                End Try


                Try
                    intR = getMember1(roomno, pwd)
                    intP = getMember2(roomno, pwd)
                Catch ex As Exception

                End Try




                Try


                    objlog.write2LogFile(roomno & "_MembQry", "Membership exist=: " & intM & "Rate code exist:" & intR & "intp=" & intP)
                Catch ex As Exception

                End Try

                Try
                    If intM = 1 Or intR = 1 Or intP = 1 Then
                        Try
                            objlog.write2LogFile(roomno, "Database Complimentary guest found:  Member Code=: " & member & "RateCode=: " & rate & "Membership exist=: " & intM & "Rate code exist:" & intR)
                        Catch ex As Exception

                        End Try

                    Else




                        'Dim pri As String = "-1"

                        'Try
                        '    pri = getPrimaryGuestName(roomno, pwd)
                        'Catch ex As Exception

                        'End Try

                        ' objlog.write2LogFile(roomno, "Searching for Primary Guest Name" & pri)


                        'If pri <> "-1" And pri <> "" Then
                        '    Try
                        '        intM = getMember(roomno, pri)

                        '        ' objlog.write2LogFile("member", member)
                        '    Catch ex As Exception

                        '    End Try


                        '    Try
                        '        intR = getMember1(roomno, pri)

                        '    Catch ex As Exception

                        '    End Try

                        '    objlog.write2LogFile(roomno & "_MembQry", "Primary Membership exist=: " & intM & "Primary Rate code exist:" & intR)

                        'End If








                    End If
                Catch ex As Exception

                End Try


                'Dim myrate As String = ""

                'Try
                '    If rate = "NRF42" Or rate = "NRF46" Or rate = "PKG2" Or rate = "RFP14" Or rate = "RFP30" Or rate = "RFP35" Or rate = "RFP34" Or rate = "RFP40" Or rate = "RFP41" Or rate = "RFP42" Or rate = "RFP49" Or rate = "RFP70" Or rate = "RFP71" Then
                '        myrate = "1"
                '    End If
                '    objlog.write2LogFile(roomno, "Member Code=: " & member & "RateCode=: " & rate & "comp guest status=: " & myrate)
                'Catch ex As Exception

                'End Try

                'If (intM = 1 Or intR = 1) And comp = "" Then
                '    Try
                '        objlog.write2LogFile(roomno, "Complimentary guest found:  Member Code=: " & member & "RateCode=: " & rate)
                '    Catch ex As Exception

                '    End Try

                '    ' myroom.Visible = False
                '    nt.Visible = True
                '    '  si.Visible = False
                '    Rdoplan1.Items.Add(New ListItem("<span class='radiospan complimentary'>" & "&nbsp;&nbsp;" & GetLocalResourceObject("z2") & "</span><span class=""WebRupee""></span> " & "", 15))
                'ElseIf isMac2 = 0 Then
                ' si.Visible = True
                '   myroom.Visible = True

                '    If stay = 0 Then
                '        'Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;Basic 1" & GetLocalResourceObject("m7").ToString() & "</span><span class=""WebRupee"">INR 199</span> " & "", 1))
                '        'Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;Premium 1" & GetLocalResourceObject("m7").ToString() & "</span><span class=""WebRupee"">INR 299</span> " & "", 8))
                '        'Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;Basic 3" & GetLocalResourceObject("m8").ToString() & "</span><span class=""WebRupee"">INR 299</span> " & "", 2))
                '        'Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;Premium 3" & GetLocalResourceObject("m8").ToString() & "</span><span class=""WebRupee"">INR 399</span> " & "", 9))
                '        'Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;Basic 12" & GetLocalResourceObject("m8").ToString() & "</span><span class=""webrupee"">INR 499</span> " & "", 3))
                '        'Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;Premium 12" & GetLocalResourceObject("m8").ToString() & "</span><span class=""webrupee"">INR 699</span> " & "", 10))
                '        'Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;Basic 1" & GetLocalResourceObject("dd").ToString() & "</span><span class=""webrupee"">INR 699</span> " & "", 4))
                '        'Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;Premium 1" & GetLocalResourceObject("dd").ToString() & "</span><span class=""webrupee"">INR 899</span> " & "", 11))
                '        'Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;Basic 2" & GetLocalResourceObject("ddd").ToString() & "</span><span class=""webrupee"">INR 1299</span> " & "", 5))
                '        'Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;Premium 2" & GetLocalResourceObject("ddd").ToString() & "</span><span class=""webrupee"">INR 1699</span> " & "", 12))
                '        'Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;Basic 5" & GetLocalResourceObject("ddd").ToString() & "</span><span class=""webrupee"">INR 2899</span> " & "", 6))
                '        'Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;Premium 5" & GetLocalResourceObject("ddd").ToString() & "</span><span class=""webrupee"">INR 3399</span> " & "", 13))
                '        'Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;Basic 7" & GetLocalResourceObject("ddd").ToString() & "</span><span class=""WebRupee"">INR 3599</span> " & "", 7))
                '        'Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;premium 7" & GetLocalResourceObject("ddd").ToString() & "</span><span class=""webrupee"">INR 4099</span> " & "", 14))


                '    ElseIf stay = 1 Then

                '        'Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;Basic 1" & GetLocalResourceObject("m7").ToString() & "</span><span class=""WebRupee"">INR 199</span> " & "", 1))
                '        'Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;Premium 1" & GetLocalResourceObject("m7").ToString() & "</span><span class=""WebRupee"">INR 299</span> " & "", 8))
                '        'Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;Basic 3" & GetLocalResourceObject("m8").ToString() & "</span><span class=""WebRupee"">INR 299</span> " & "", 2))
                '        'Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;Premium 3" & GetLocalResourceObject("m8").ToString() & "</span><span class=""WebRupee"">INR 399</span> " & "", 9))
                '        'Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;Basic 12" & GetLocalResourceObject("m8").ToString() & "</span><span class=""webrupee"">INR 499</span> " & "", 3))
                '        'Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;Premium 12" & GetLocalResourceObject("m8").ToString() & "</span><span class=""webrupee"">INR 699</span> " & "", 10))
                '        'Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;Basic 1" & GetLocalResourceObject("dd").ToString() & "</span><span class=""webrupee"">INR 699</span> " & "", 4))
                '        'Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;Premium 1" & GetLocalResourceObject("dd").ToString() & "</span><span class=""webrupee"">INR 899</span> " & "", 11))


                '    ElseIf stay = 2 Then

                '        Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;Basic 1" & GetLocalResourceObject("m7").ToString() & "</span><span class=""WebRupee"">INR 199</span> " & "", 1))
                '        Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;Premium 1" & GetLocalResourceObject("m7").ToString() & "</span><span class=""WebRupee"">INR 299</span> " & "", 8))
                '        Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;Basic 3" & GetLocalResourceObject("m8").ToString() & "</span><span class=""WebRupee"">INR 299</span> " & "", 2))
                '        Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;Premium 3" & GetLocalResourceObject("m8").ToString() & "</span><span class=""WebRupee"">INR 399</span> " & "", 9))
                '        Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;Basic 12" & GetLocalResourceObject("m8").ToString() & "</span><span class=""webrupee"">INR 499</span> " & "", 3))
                '        Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;Premium 12" & GetLocalResourceObject("m8").ToString() & "</span><span class=""webrupee"">INR 699</span> " & "", 10))
                '        Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;Basic 1" & GetLocalResourceObject("dd").ToString() & "</span><span class=""webrupee"">INR 699</span> " & "", 4))
                '        Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;Premium 1" & GetLocalResourceObject("dd").ToString() & "</span><span class=""webrupee"">INR 899</span> " & "", 11))
                '        Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;Basic 2" & GetLocalResourceObject("ddd").ToString() & "</span><span class=""webrupee"">INR 1299</span> " & "", 5))
                '        Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;Premium 2" & GetLocalResourceObject("ddd").ToString() & "</span><span class=""webrupee"">INR 1699</span> " & "", 12))

                '    ElseIf stay = 3 Or stay = 4 Then

                '        Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;Basic 1" & GetLocalResourceObject("m7").ToString() & "</span><span class=""WebRupee"">INR 199</span> " & "", 1))
                '        Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;Premium 1" & GetLocalResourceObject("m7").ToString() & "</span><span class=""WebRupee"">INR 299</span> " & "", 8))
                '        Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;Basic 3" & GetLocalResourceObject("m8").ToString() & "</span><span class=""WebRupee"">INR 299</span> " & "", 2))
                '        Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;Premium 3" & GetLocalResourceObject("m8").ToString() & "</span><span class=""WebRupee"">INR 399</span> " & "", 9))
                '        Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;Basic 12" & GetLocalResourceObject("m8").ToString() & "</span><span class=""webrupee"">INR 499</span> " & "", 3))
                '        Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;Premium 12" & GetLocalResourceObject("m8").ToString() & "</span><span class=""webrupee"">INR 699</span> " & "", 10))
                '        Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;Basic 1" & GetLocalResourceObject("dd").ToString() & "</span><span class=""webrupee"">INR 699</span> " & "", 4))
                '        Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;Premium 1" & GetLocalResourceObject("dd").ToString() & "</span><span class=""webrupee"">INR 899</span> " & "", 11))
                '        Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;Basic 2" & GetLocalResourceObject("ddd").ToString() & "</span><span class=""webrupee"">INR 1299</span> " & "", 5))
                '        Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;Premium 2" & GetLocalResourceObject("ddd").ToString() & "</span><span class=""webrupee"">INR 1699</span> " & "", 12))
                '        Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;Basic 5" & GetLocalResourceObject("ddd").ToString() & "</span><span class=""webrupee"">INR 2899</span> " & "", 6))
                '        Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;Premium 5" & GetLocalResourceObject("ddd").ToString() & "</span><span class=""webrupee"">INR 3399</span> " & "", 13))




                '    ElseIf stay = 5 Then
                '        Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;Basic 1" & GetLocalResourceObject("m7").ToString() & "</span><span class=""WebRupee"">INR 199</span> " & "", 1))
                '        Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;Premium 1" & GetLocalResourceObject("m7").ToString() & "</span><span class=""WebRupee"">INR 299</span> " & "", 8))
                '        Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;Basic 3" & GetLocalResourceObject("m8").ToString() & "</span><span class=""WebRupee"">INR 299</span> " & "", 2))
                '        Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;Premium 3" & GetLocalResourceObject("m8").ToString() & "</span><span class=""WebRupee"">INR 399</span> " & "", 9))
                '        Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;Basic 12" & GetLocalResourceObject("m8").ToString() & "</span><span class=""webrupee"">INR 499</span> " & "", 3))
                '        Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;Premium 12" & GetLocalResourceObject("m8").ToString() & "</span><span class=""webrupee"">INR 699</span> " & "", 10))
                '        Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;Basic 1" & GetLocalResourceObject("dd").ToString() & "</span><span class=""webrupee"">INR 699</span> " & "", 4))
                '        Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;Premium 1" & GetLocalResourceObject("dd").ToString() & "</span><span class=""webrupee"">INR 899</span> " & "", 11))
                '        Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;Basic 2" & GetLocalResourceObject("ddd").ToString() & "</span><span class=""webrupee"">INR 1299</span> " & "", 5))
                '        Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;Premium 2" & GetLocalResourceObject("ddd").ToString() & "</span><span class=""webrupee"">INR 1699</span> " & "", 12))
                '        Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;Basic 5" & GetLocalResourceObject("ddd").ToString() & "</span><span class=""webrupee"">INR 2899</span> " & "", 6))
                '        Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;Premium 5" & GetLocalResourceObject("ddd").ToString() & "</span><span class=""webrupee"">INR 3399</span> " & "", 13))


                '    ElseIf stay = 6 Then

                '        Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;Basic 1" & GetLocalResourceObject("m7").ToString() & "</span><span class=""WebRupee"">INR 199</span> " & "", 1))
                '        Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;Premium 1" & GetLocalResourceObject("m7").ToString() & "</span><span class=""WebRupee"">INR 299</span> " & "", 8))
                '        Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;Basic 3" & GetLocalResourceObject("m8").ToString() & "</span><span class=""WebRupee"">INR 299</span> " & "", 2))
                '        Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;Premium 3" & GetLocalResourceObject("m8").ToString() & "</span><span class=""WebRupee"">INR 399</span> " & "", 9))
                '        Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;Basic 12" & GetLocalResourceObject("m8").ToString() & "</span><span class=""webrupee"">INR 499</span> " & "", 3))
                '        Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;Premium 12" & GetLocalResourceObject("m8").ToString() & "</span><span class=""webrupee"">INR 699</span> " & "", 10))
                '        Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;Basic 1" & GetLocalResourceObject("dd").ToString() & "</span><span class=""webrupee"">INR 699</span> " & "", 4))
                '        Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;Premium 1" & GetLocalResourceObject("dd").ToString() & "</span><span class=""webrupee"">INR 899</span> " & "", 11))
                '        Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;Basic 2" & GetLocalResourceObject("ddd").ToString() & "</span><span class=""webrupee"">INR 1299</span> " & "", 5))
                '        Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;Premium 2" & GetLocalResourceObject("ddd").ToString() & "</span><span class=""webrupee"">INR 1699</span> " & "", 12))
                '        Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;Basic 5" & GetLocalResourceObject("ddd").ToString() & "</span><span class=""webrupee"">INR 2899</span> " & "", 6))
                '        Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;Premium 5" & GetLocalResourceObject("ddd").ToString() & "</span><span class=""webrupee"">INR 3399</span> " & "", 13))
                '        Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;Basic 7" & GetLocalResourceObject("ddd").ToString() & "</span><span class=""WebRupee"">INR 3599</span> " & "", 7))
                '        Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;premium 7" & GetLocalResourceObject("ddd").ToString() & "</span><span class=""webrupee"">INR 4099</span> " & "", 14))



                '    ElseIf stay >= 7 Then

                '        Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;Basic 1" & GetLocalResourceObject("m7").ToString() & "</span><span class=""WebRupee"">INR 199</span> " & "", 1))
                '        Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;Premium 1" & GetLocalResourceObject("m7").ToString() & "</span><span class=""WebRupee"">INR 299</span> " & "", 8))
                '        Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;Basic 3" & GetLocalResourceObject("m8").ToString() & "</span><span class=""WebRupee"">INR 299</span> " & "", 2))
                '        Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;Premium 3" & GetLocalResourceObject("m8").ToString() & "</span><span class=""WebRupee"">INR 399</span> " & "", 9))
                '        Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;Basic 12" & GetLocalResourceObject("m8").ToString() & "</span><span class=""webrupee"">INR 499</span> " & "", 3))
                '        Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;Premium 12" & GetLocalResourceObject("m8").ToString() & "</span><span class=""webrupee"">INR 699</span> " & "", 10))
                '        Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;Basic 1" & GetLocalResourceObject("dd").ToString() & "</span><span class=""webrupee"">INR 699</span> " & "", 4))
                '        Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;Premium 1" & GetLocalResourceObject("dd").ToString() & "</span><span class=""webrupee"">INR 899</span> " & "", 11))
                '        Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;Basic 2" & GetLocalResourceObject("ddd").ToString() & "</span><span class=""webrupee"">INR 1299</span> " & "", 5))
                '        Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;Premium 2" & GetLocalResourceObject("ddd").ToString() & "</span><span class=""webrupee"">INR 1699</span> " & "", 12))
                '        Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;Basic 5" & GetLocalResourceObject("ddd").ToString() & "</span><span class=""webrupee"">INR 2899</span> " & "", 6))
                '        Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;Premium 5" & GetLocalResourceObject("ddd").ToString() & "</span><span class=""webrupee"">INR 3399</span> " & "", 13))
                '        Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;Basic 7" & GetLocalResourceObject("ddd").ToString() & "</span><span class=""WebRupee"">INR 3599</span> " & "", 7))
                '        Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;premium 7" & GetLocalResourceObject("ddd").ToString() & "</span><span class=""webrupee"">INR 4099</span> " & "", 14))








                '    End If





                'ElseIf isMac2 = 1 Then
                '    si.Visible = True
                '    myroom.Visible = True
                '    Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;Basic 1" & GetLocalResourceObject("m7").ToString() & "</span><span class=""WebRupee"">INR 199</span> " & "", 1))

                '    Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;Premium 1" & GetLocalResourceObject("m7").ToString() & "</span><span class=""WebRupee"">INR 299</span> " & "", 8))

                '    Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;Basic 3" & GetLocalResourceObject("m8").ToString() & "</span><span class=""WebRupee"">INR 299</span> " & "", 2))

                '    Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;Premium 3" & GetLocalResourceObject("m8").ToString() & "</span><span class=""WebRupee"">INR 399</span> " & "", 9))

                '    Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;Basic 12" & GetLocalResourceObject("m8").ToString() & "</span><span class=""webrupee"">INR 499</span> " & "", 3))

                '    Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "&nbsp;&nbsp;Premium 12" & GetLocalResourceObject("m8").ToString() & "</span><span class=""webrupee"">INR 699</span> " & "", 10))


                'End If

            Catch ex As Exception

            End Try
        Catch ex As Exception

        End Try


        Try
            
        Catch ex As Exception

        End Try



       



    End Sub
    Private Sub Page_load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load, Me.Load
        Label1.Text = ""
        Dim member As String = ""
        Dim rate As String = ""
        Dim intM As Integer = 0
        Dim intR As Integer = 0
        Dim stay As Integer = 0
        If Not IsPostBack Then
            Try
                DrpPlans1.SelectedIndex = 0
            Catch ex As Exception

            End Try

            txt1.Text = 0
            txt2.Text = 499

            qty.Visible = False
            qty1.Visible = False
            Dim objSysConfig As New CSysConfig

            Dim objPlan As New CPlan

            Dim RN As String = ""
            Dim grcid As String = ""
            Dim objlog As LoggerService
            objlog = LoggerService.gtInstance()

            Dim intp As Integer = 0
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

            Dim roomno As String = ""
            Dim pwd As String = ""

            Dim comp As String = ""




            Dim objSysConfig2 As New CSysConfig

            isMac2 = objSysConfig2.isBCMachine(commonFun.DecrptQueryString("MA", qs))

            Try
                roomno = Session("roomno")
                pwd = Session("pwd")

            Catch ex As Exception

            End Try

            Try
                ' objlog.write2LogFile("ps_Session" & commonFun.DecrptQueryString("MA", qs), "Selected Plan in btn event planid " & planId & " Roomno" & roomno & " pwd" & pwd)
            Catch ex As Exception

            End Try



            Try
                basic1.Visible = False
                qty.Visible = False
            Catch ex As Exception

            End Try

            Dim lt As New ListItem()

            Dim r1 As String = ""
            Dim r2 As String = ""
            Dim r3 As String = ""

            Try
                r1 = Request.QueryString("rm")
                r2 = Request.QueryString("ln")
                r3 = Request.QueryString("comp")
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
                Dim str2() = r3.Split(",")
                Dim ind As Integer = 0
                ind = str2.Length - 1

                r3 = str2(ind)
                ' ObjElog.write2LogFile("Mac_" & commonFun.DecrptQueryString("MA", qs), "MIFI after split r2" & r2)

            Catch ex As Exception

            End Try

            Try
                'objlog.write2LogFile("ps_Query" & commonFun.DecrptQueryString("MA", qs), "Selected Plan in btn event planid " & planId & "query Roomno" & r1 & "query pwd" & r2 & "comp" & comp)
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
                nt.Visible = False
            Catch ex As Exception

            End Try

            Try
                comp = Session("comp")
                If comp = "" Then
                    comp = r3
                End If

            Catch ex As Exception

            End Try


            Try
                objlog.write2LogFile("ps_Query" & commonFun.DecrptQueryString("MA", qs), "Selected Plan in btn event planid " & planId & "query Roomno" & r1 & "query pwd" & r2 & "comp" & comp)
            Catch ex As Exception

            End Try


            Try
                stay = GetGuestDaysStay(roomno, pwd)
                objlog.write2LogFile(roomno, "No Of stay=" & stay)
            Catch ex As Exception

            End Try




            Try
                intM = getMember(roomno, pwd)

                ' objlog.write2LogFile("member", member)
            Catch ex As Exception

            End Try


            Try
                intR = getMember1(roomno, pwd)
                intp = getMember2(roomno, pwd)

            Catch ex As Exception

            End Try




            Try


                objlog.write2LogFile(roomno & "_MembQry", "Membership exist=: " & intM & "Rate code exist:" & intR)
            Catch ex As Exception

            End Try


            Dim credit As Integer = 0


            Try
                credit = NOCREDIT(roomno, pwd)



                Try
                    objlog.write2LogFile("credit", credit)
                Catch ex As Exception

                End Try
            Catch ex As Exception

            End Try




            Try
                Rdoplan1.Items.Clear()

                If credit = 1 Or getMember20(roomno, pwd) = 1 Then

                    zzz.Visible = False

                    Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "Standard-Complimentary Internet" & "</span> " & "", 101))


                ElseIf getMember200(roomno, pwd) = 1 Then
                    txt1.Text = 0
                    Try
                        objlog.write2LogFile(roomno, "Database Complimentary guest found:  Member Code=: " & member & "RateCode=: " & rate & "Membership exist=: " & intM & "Rate code exist:" & intR)
                        taxd.Visible = False

                    Catch ex As Exception

                    End Try
                    zz.Visible = True
                    zzz.Visible = False
                    Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "High Speed Internet Acces" & "</span> " & "", 701))

                ElseIf intM = 1 Or intR = 1 Or intp = 1 Or roomno = 233 Or roomno = 333 Or roomno = 433 Or roomno = 533 Or roomno = 633 Then
                    txt1.Text = 0
                    Try
                        objlog.write2LogFile(roomno, "Database Complimentary guest found:  Member Code=: " & member & "RateCode=: " & rate & "Membership exist=: " & intM & "Rate code exist:" & intR)
                        taxd.Visible = False

                    Catch ex As Exception

                    End Try
                    zz.Visible = True
                    zzz.Visible = False
                    Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "Premium-Complimentary Internet" & "</span> " & "", 302))
                Else


                    zzz.Visible = False
                    zz.Visible = False
                    Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "Standard-Complimentary Internet" & "</span><span class=""WebRupee""></span> " & "", 101))
                    ' Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "Premium Service - 1 hour access" & "</span><span class=""WebRupee"">INR 400</span> " & "", 2))
                    Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "Premium 12 Mbps at 499 INR/day" & "</span><span class=""WebRupee""></span> " & "", 202))
                    'Rdoplan1.Items.Add(New ListItem("<span class='radiospan'>" & "Executive Service - 24-hour Access" & "</span><span class=""WebRupee"">INR 500</span> " & "", 105))
                End If

            Catch ex As Exception

            End Try




         



            Try

                stay = stay + 1

                If stay > 30 Then
                    stay = 30
                End If


            Catch ex As Exception

            End Try

            DrpPlans1.Items.Clear()
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




        End If

    End Sub

    Protected Sub DrpPlans1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DrpPlans1.SelectedIndexChanged
        Try

            Dim pid As Long = 0

            Try
                pid = Rdoplan1.SelectedValue

            Catch ex As Exception

            End Try
            
            If pid = 202 Then
                Dim noofdays As Long
                noofdays = DrpPlans1.SelectedValue

                Try
                    txt2.Text = noofdays * 499
                Catch ex As Exception

                End Try

            Else
                txt2.Text = 0
            End If

           


        Catch ex As Exception

        End Try



    End Sub



    'Protected Sub lk1(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton2.Click

    '    Try
    '        flag = True
    '    Catch ex As Exception

    '    End Try




    'End Sub


    Protected Sub lk(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton1.Click

        Try
            Dim commonFun As PMSCommonFun
            commonFun = PMSCommonFun.getInstance
            url = commonFun.BrowserQueryString(Request)
            Response.Redirect("terms.aspx?" & GetRedirectQS() & "&bk=b")


        Catch ex As Exception

        End Try




    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button2.Click
        Dim noofdays As Integer = 0

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
            planId = Rdoplan1.SelectedValue
        Catch ex As Exception

        End Try


        If planId <= 0 Then
            Label1.Visible = True
            Label1.Text = GetLocalResourceObject("p1").ToString()
            hdAccept.Value = "0"


            'Response.Redirect("UserError.aspx?" & url & "&Msg=" & "Dear Guest, Please select a Package." & "&findurl=mifilogin")

        Else


            Try
                Session("splan") = planId
            Catch ex As Exception

            End Try



            Try
                If planId = 202 Or planId = 103 Then
                    noofdays = DrpPlans1.SelectedValue

                End If
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

          


            Try
                Response.Redirect("mifilogin.aspx?" & GetRedirectQS() & "&plan=" & planId & "&rm=" & roomno & "&ln=" & pwd & "&nd=" & noofdays & "&ct=" & Page.UICulture)
            Catch ex As Exception

            End Try
        End If


    End Sub

    Private Function GetRedirectQS()
        Dim redirectQS As String = ""
        redirectQS = "encry=" & Request.QueryString("encry")
        Return redirectQS
    End Function

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



    Public Function getPrimaryGuestName(ByVal str As String, ByVal pd As String) As String
        Dim db As DbaseService
        Dim objlog As LoggerService
        objlog = LoggerService.gtInstance
        Try

            db = DbaseService.getInstance

            Dim Ctime As DateTime
            Ctime = Now
            pd = UCase(pd)

            Dim ds As DataSet
            Dim ds1 As DataSet

            ds = db.DsWithoutUpdate("select GuestCFA1 from Guest where upper(GuestName) = '" & pd.Replace("'", "''") & "' and gueststatus='A'  and GuestRoomNo ='" & str & "' order by guestid desc")

            If ds.Tables(0).Rows.Count > 0 Then

                Try
                    objlog.write2LogFile(str, "IS sharer" & ds.Tables(0).Rows(0)(0).ToString().Trim())
                Catch ex As Exception

                End Try


                If ds.Tables(0).Rows(0)(0).ToString().Trim() = "SHARER" Then

                    ds1 = db.DsWithoutUpdate("Select Guestname from guest where GuestRoomNo ='" & str & "' and gueststatus='A' and GuestCFA1 <> 'SHARER' order by guestid desc")

                    If ds1.Tables(0).Rows.Count > 0 Then

                        Try
                            objlog.write2LogFile(str, "IS Primary name" & ds1.Tables(0).Rows(0)(0).ToString().Trim())
                        Catch ex As Exception

                        End Try
                        Return ds1.Tables(0).Rows(0)(0).ToString()

                    Else
                        Return "-1"
                    End If

                Else
                    Return "-1"

                End If
            Else
                Return "-1"

            End If



        Catch ex As Exception
            Return "-1"
        End Try



    End Function

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




    Public Function getMember(ByVal str As String, ByVal pd As String) As Integer

        Dim code As String = ""
        Dim result As Integer = -1

        Dim db As DbaseService
        Dim objlog As LoggerService
        objlog = LoggerService.gtInstance


        Try
            Try

                db = DbaseService.getInstance
            Catch ex As Exception
                Return -1
            End Try

            Dim Ctime As DateTime
            Ctime = Now
            pd = UCase(pd)

            Dim ds As DataSet

            Try
                'objlog.write2LogFile("MembQry", "select GuestCFA0 from Guest where upper(GuestName) = '" & pd.Replace("'", "''") & "' and gueststatus='A'  and GuestRoomNo ='" & str & "' order by guestid desc")
            Catch ex As Exception

            End Try


            ds = db.DsWithoutUpdate("select GuestCFA0 from Guest where upper(GuestName) = '" & pd.Replace("'", "''") & "' and gueststatus='A'  and GuestRoomNo ='" & str & "' order by guestid desc")


            If ds.Tables(0).Rows.Count = 0 Then

                Return -1

            ElseIf ds.Tables(0).Rows.Count = 1 Then

                code = ds.Tables(0).Rows(0)(0).ToString()
                code = code.Trim()





                Try
                    objlog.write2LogFile(str & "_MembQry", "Membership code= " & code & "count=" & ds.Tables(0).Rows.Count)
                Catch ex As Exception

                End Try

                Try

                    If code <> "" Then
                        result = Memberexist(code.Trim())

                    End If




                    objlog.write2LogFile(str & "_MembQry", "Membership code result= " & result)
                Catch ex As Exception

                End Try

                Return result

            ElseIf ds.Tables(0).Rows.Count >= 2 Then

                code = ds.Tables(0).Rows(0)(0).ToString()
                code = code.Trim()
                Try
                    objlog.write2LogFile(str & "_MembQry", "Membership code sharer=" & code & "count=" & ds.Tables(0).Rows.Count)
                Catch ex As Exception

                End Try

                Try

                    If code <> "" Then
                        result = Memberexist(code.Trim())
                    End If


                    If result = -1 Then

                        code = ds.Tables(0).Rows(1)(0).ToString()

                        Try
                            objlog.write2LogFile(str & "_MembQry", "Membership code primary=" & code & "count=" & ds.Tables(0).Rows.Count)
                        Catch ex As Exception

                        End Try
                        If code <> "" Then
                            result = Memberexist(code.Trim())
                        End If

                    End If


                    objlog.write2LogFile(str & "_MembQry", "Membership code result= " & result)
                Catch ex As Exception

                End Try

                Return result



            End If





            'If ds.Tables(0).Rows.Count > 0 Then
            '    Return ds.Tables(0).Rows(0)(0).ToString()
            'Else
            '    Return "-1"
            'End If
        Catch ex As Exception
            Return -1
        End Try








    End Function


    Public Function getMember1(ByVal str As String, ByVal pd As String) As Integer
        Dim db As DbaseService
        Dim objlog As LoggerService
        objlog = LoggerService.gtInstance
        Dim code As String = ""
        Dim result As Integer = -1

        Try
            Try

                db = DbaseService.getInstance
            Catch ex As Exception
                Return -1
            End Try

            Dim Ctime As DateTime
            Ctime = Now
            pd = UCase(pd)

            Dim ds As DataSet

            Try
                'objlog.write2LogFile("MembQry", "select GuestCFA1 from Guest where upper(GuestName) = '" & pd.Replace("'", "''") & "' and gueststatus='A'  and GuestRoomNo ='" & str & "' order by guestid desc")
            Catch ex As Exception

            End Try


            ds = db.DsWithoutUpdate("select GuestCFA1 from Guest where upper(GuestName) = '" & pd.Replace("'", "''") & "' and gueststatus='A'  and GuestRoomNo ='" & str & "' order by guestid desc")

            'If ds.Tables(0).Rows.Count > 0 Then
            '    Return ds.Tables(0).Rows(0)(0).ToString()
            'Else
            '    Return "-1"
            'End If



            If ds.Tables(0).Rows.Count = 0 Then

                Return -1

            ElseIf ds.Tables(0).Rows.Count = 1 Then

                code = ds.Tables(0).Rows(0)(0).ToString()
                code = code.Trim()
                Try
                    objlog.write2LogFile(str & "_MembQry", "Rate code= " & code & "count=" & ds.Tables(0).Rows.Count)
                Catch ex As Exception

                End Try

                Try

                    If code <> "" Then
                        result = Rateexist(code.Trim())
                    End If

                    objlog.write2LogFile(str & "_MembQry", "Rate code result= " & result)
                Catch ex As Exception

                End Try

                Return result

            ElseIf ds.Tables(0).Rows.Count >= 2 Then

                code = ds.Tables(0).Rows(0)(0).ToString()
                code = code.Trim()
                Try
                    objlog.write2LogFile(str & "_MembQry", "Rate code sharer=" & code & "count=" & ds.Tables(0).Rows.Count)
                Catch ex As Exception

                End Try

                Try
                    If code <> "" Then
                        result = Rateexist(code.Trim())
                    End If

                    If result = -1 Then

                        code = ds.Tables(0).Rows(1)(0).ToString()

                        Try
                            objlog.write2LogFile(str & "_MembQry", "Rate code primary=" & code & "count=" & ds.Tables(0).Rows.Count)
                        Catch ex As Exception

                        End Try

                        If code <> "" Then
                            result = Rateexist(code.Trim())
                        End If
                    End If


                    objlog.write2LogFile(str & "_MembQry", "Rate code result= " & result)
                Catch ex As Exception

                End Try

                Return result



            End If



        Catch ex As Exception
            Return -1
        End Try








    End Function

    Public Function getMember200(ByVal str As String, ByVal pd As String) As Integer
        Dim db As DbaseService
        Dim objlog As LoggerService
        objlog = LoggerService.gtInstance
        Dim code As String = ""
        Dim result As Integer = -1

        Try
            Try

                db = DbaseService.getInstance
            Catch ex As Exception
                Return -1
            End Try

            Dim Ctime As DateTime
            Ctime = Now
            pd = UCase(pd)

            Dim ds As DataSet

            Try
                'objlog.write2LogFile("MembQry", "select GuestCFA1 from Guest where upper(GuestName) = '" & pd.Replace("'", "''") & "' and gueststatus='A'  and GuestRoomNo ='" & str & "' order by guestid desc")
            Catch ex As Exception

            End Try


            ds = db.DsWithoutUpdate("select GuestCFA1 from Guest where upper(GuestName) = '" & pd.Replace("'", "''") & "' and gueststatus='A'  and GuestRoomNo ='" & str & "'   and ( GuestCFA1 like '%HIINT%'  or GuestCFA1 like 'HIINT%' or GuestCFA0 like '%HIINT%'  or GuestCFA0 like 'HIINT%' )  order by guestid desc")

            'If ds.Tables(0).Rows.Count > 0 Then
            '    Return ds.Tables(0).Rows(0)(0).ToString()
            'Else
            '    Return "-1"
            'End If



            If ds.Tables(0).Rows.Count > 0 Then

                Return 1

            Else

                Return -1

            End If



        Catch ex As Exception
            Return -1
        End Try








    End Function

    Public Function getMember20(ByVal str As String, ByVal pd As String) As Integer
        Dim db As DbaseService
        Dim objlog As LoggerService
        objlog = LoggerService.gtInstance
        Dim code As String = ""
        Dim result As Integer = -1

        Try
            Try

                db = DbaseService.getInstance
            Catch ex As Exception
                Return -1
            End Try

            Dim Ctime As DateTime
            Ctime = Now
            pd = UCase(pd)

            Dim ds As DataSet

            Try
                'objlog.write2LogFile("MembQry", "select GuestCFA1 from Guest where upper(GuestName) = '" & pd.Replace("'", "''") & "' and gueststatus='A'  and GuestRoomNo ='" & str & "' order by guestid desc")
            Catch ex As Exception

            End Try


            ds = db.DsWithoutUpdate("select GuestCFA1 from Guest where upper(GuestName) = '" & pd.Replace("'", "''") & "' and gueststatus='A'  and GuestRoomNo ='" & str & "'   and ( GuestCFA1 like '%CREW%'  or GuestCFA1 like 'CREW%' or GuestCFA0 like '%CREW%'  or GuestCFA0 like 'CREW%' )  order by guestid desc")

            'If ds.Tables(0).Rows.Count > 0 Then
            '    Return ds.Tables(0).Rows(0)(0).ToString()
            'Else
            '    Return "-1"
            'End If



            If ds.Tables(0).Rows.Count > 0 Then

                Return 1

            Else

                Return -1

            End If



        Catch ex As Exception
            Return -1
        End Try








    End Function

    Public Function getMember2(ByVal str As String, ByVal pd As String) As Integer
        Dim db As DbaseService
        Dim objlog As LoggerService
        objlog = LoggerService.gtInstance
        Dim code As String = ""
        Dim result As Integer = -1

        Try
            Try

                db = DbaseService.getInstance
            Catch ex As Exception
                Return -1
            End Try

            Dim Ctime As DateTime
            Ctime = Now
            pd = UCase(pd)

            Dim ds As DataSet

            Try
                'objlog.write2LogFile("MembQry", "select GuestCFA1 from Guest where upper(GuestName) = '" & pd.Replace("'", "''") & "' and gueststatus='A'  and GuestRoomNo ='" & str & "' order by guestid desc")
            Catch ex As Exception

            End Try


            ds = db.DsWithoutUpdate("select GuestCFA1 from Guest where upper(GuestName) = '" & pd.Replace("'", "''") & "' and gueststatus='A'  and GuestRoomNo ='" & str & "'   and GuestCFA1 like '%PINT%' order by guestid desc")

            'If ds.Tables(0).Rows.Count > 0 Then
            '    Return ds.Tables(0).Rows(0)(0).ToString()
            'Else
            '    Return "-1"
            'End If



            If ds.Tables(0).Rows.Count > 0 Then

                Return 1

            Else

                Return -1

            End If



        Catch ex As Exception
            Return -1
        End Try








    End Function





    Public Function Memberexist(ByVal pd As String) As Integer
        Dim db As DbaseService
        Dim objlog As LoggerService
        objlog = LoggerService.gtInstance


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




            ds = db.DsWithoutUpdate("select name from MemberShip where ( upper(name) like  '%" & pd.Replace("'", "''") & "%'     or upper(name) like  '" & pd.Replace("'", "''") & "%' )  and active=1  ")

            If ds.Tables(0).Rows.Count > 0 Then
                Return 1
            Else
                Return -1
            End If
        Catch ex As Exception
            Return -1
        End Try








    End Function

    Public Function Rateexist(ByVal pd As String) As Integer
        Dim db As DbaseService
        Dim objlog As LoggerService
        objlog = LoggerService.gtInstance


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




            ds = db.DsWithoutUpdate("select name from RateCode where ( upper(name) like '%" & pd.Replace("'", "''") & "%' or upper(name) like  '" & pd.Replace("'", "''") & "%' ) and active=1  ")

            If ds.Tables(0).Rows.Count > 0 Then
                Return 1
            Else
                Return -1
            End If
        Catch ex As Exception
            Return -1
        End Try








    End Function


    Protected Sub Rdoplan1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Rdoplan1.SelectedIndexChanged
        Try
            Dim pid As Long = 0
            pid = Rdoplan1.SelectedValue


            If pid = 101 Or pid = 302 Or pid = 701 Then

                qty.Visible = True
                qty1.Visible = False
                'dy.Visible = True
                'DrpPlans1.Visible = True
                DrpPlans1.SelectedIndex = 0
                txt1.Text = 0
                'drp.Visible = True
            ElseIf pid = 202 Then
                'dy.Visible = False
                'DrpPlans1.Visible = False
                'DrpPlans1.SelectedIndex = 0
                'lblBillToPost.Text = ""
                'drp.Visible = False

                DrpPlans1.SelectedIndex = 0
                txt2.Text = 499

                qty1.Visible = True
                qty.Visible = False

            Else
                qty.Visible = False
                qty1.Visible = False
                DrpPlans1.SelectedIndex = 0

                'lblBillToPost.Text = ""
                'drp.Visible = False
            End If

        Catch ex As Exception

        End Try


    End Sub


End Class