Imports PMSPkgSql
Partial Public Class index
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim url As String
        Dim objSysConfig As New CSysConfig
        Dim serverIP As String = ""

        


        Dim commonFun As PMSCommonFun
        Dim objlog As LoggerService
        objlog = LoggerService.gtInstance

        Try
            serverIP = objSysConfig.GetConfig("BillServer_IP")
            'objlog.write2LogFile("serverip", "http://" & serverIP & "/mob/Welcome.aspx?")
        Catch ex As Exception

        End Try


        Try
            commonFun = PMSCommonFun.getInstance
            url = commonFun.BrowserQueryString(Request)


        Catch ex As Exception

        End Try
        commonFun = PMSCommonFun.getInstance
        Try
            url = "encry=" & commonFun.EncrptQueryString(Request)

            Dim port As Integer = 0
            Dim room As Integer = 0


            Try
                port = Request.QueryString("PORT")
                objlog.write2LogFile("Index", "Mac=" & Request.QueryString("MA") & "Room" & Request.QueryString("RN") & "port=" & Request.QueryString("PORT"))
            Catch ex As Exception

            End Try


            Try
                room = Request.QueryString("RN")
            Catch ex As Exception

            End Try

            If (port >= 101 And port <= 692) Or (room >= 101 And room <= 692) Then
                Try
                    Response.Redirect("welcome.aspx?" & url)
                Catch ex As Exception

                End Try


            End If


            If port = 700 Then
                Try
                    Response.Redirect("welcome.aspx?" & url)
                Catch ex As Exception

                End Try
            End If


            Try
                If getMember3(Request.QueryString("MA")) <> "-1" Then
                    Try
                        Response.Redirect("UserError1.aspx?" & url & "&Msg=" & "Dear Guest, Access Denied." & "&findurl=mifilogin" & "&ct=" & Page.UICulture)

                    Catch ex As Exception

                    End Try
                End If
            Catch ex As Exception

            End Try




            Try

                Try
                    'If Request.QueryString("MA") = "90FBA612B060" Or Request.QueryString("MA") = "90FBA610D023" Or Request.QueryString("MA") = "D8306262A910" Then

                    '    Response.Redirect("welcome.aspx?" & url)

                    'End If

                Catch ex As Exception

                End Try
            Catch ex As Exception

            End Try






            If Not IsPostBack Then
                Try
                    Dim uagent As String = ""

                    Try
                        uagent = Request.ServerVariables("HTTP_USER_AGENT").Trim().ToLower()
                        'objlog.write2LogFile("Detect", "uagent" & uagent)
                    Catch ex As Exception

                    End Try



                    If uagent.Contains("ipad") Then


                        Try
                            Response.Redirect("welcome.aspx?" & url)
                        Catch ex As Exception

                        End Try


                    ElseIf uagent.Contains("windows ce") Or uagent.Contains("blackberry") Or uagent.Contains("iphone") Or uagent.Contains("mobile") Or uagent.Contains("nokia") Or uagent.Contains("android") Or (uagent.Contains("safari") And Not uagent.Contains("chrome")) Then
                        Try
                            Response.Redirect("http://" & serverIP & "/mob/Welcome.aspx?" & url)
                        Catch ex As Exception

                        End Try

                    Else

                        Try
                            Response.Redirect("welcome.aspx?" & url)
                        Catch ex As Exception

                        End Try

                    End If









                Catch ex As Exception


                End Try

            End If





            'Response.Redirect("welcomepageSql.aspx?" & url)
        Catch ex As Exception
            Try
                'objlog.write2LogFile("DetError1", ex.Message)
                'Response.Redirect("welcomepageSql.aspx?" & url)
            Catch ex2 As Exception

            End Try

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

End Class