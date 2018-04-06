Imports PMSPkgSql
Imports System.Threading
Imports System.Globalization
Partial Public Class layout
    Inherits System.Web.UI.MasterPage
    Public url As String = ""
    Dim cc As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If Not Session("CultureCode") Is Nothing Then
                Dim cultureCode As [String] = Session("CultureCode").ToString()
                Session("cu") = cultureCode
                'Page.Culture = cultureCode
                Page.UICulture = cultureCode

                cc = cultureCode

                If cultureCode = "ar-SA" Then
                    arabicstyle.Href = "arabic.css"
                Else
                    arabicstyle.Href = "dmmy.css"
                End If
                '  Session("CultureCode") = "en-US"
            End If
        Catch ex As Exception

        End Try

        Try
            If Session("CultureCode") Is Nothing Then

                Dim objdb As DbaseService
                objdb = DbaseService.getInstance
                Dim ds As DataSet
                Dim commonFun As PMSCommonFun
                commonFun = PMSCommonFun.getInstance
                url = commonFun.BrowserQueryString(Request)
                Dim mac As String = ""
                Dim qs As String = Request.QueryString("encry")
                mac = commonFun.DecrptQueryString("MA", qs)



                ds = objdb.DsWithoutUpdate("select code from culture where mac='" & mac & "' order by cid desc")
                If ds.Tables(0).Rows.Count > 0 Then

                    Dim objlog As LoggerService
                    objlog = LoggerService.gtInstance
                    objlog.write2LogFile("a_culture", ds.Tables(0).Rows(0)(0))
                    Page.UICulture = ds.Tables(0).Rows(0)(0)
                    If ds.Tables(0).Rows(0)(0) = "ar-SA" Then
                        arabicstyle.Href = "arabic.css"
                    Else
                        arabicstyle.Href = "dmmy.css"
                    End If

                End If


            End If
        Catch ex As Exception

        End Try
        Try
            Dim qs As String = Request.QueryString("encry")
            Dim commonFun As PMSCommonFun
            commonFun = PMSCommonFun.getInstance
            If commonFun.DecrptQueryString("PORT", qs) = "3090" Or commonFun.DecrptQueryString("PORT", qs) = "11" Or commonFun.DecrptQueryString("PORT", qs) = "4001" Or commonFun.DecrptQueryString("PORT", qs) = "700" Then

                k1.Visible = False

            End If


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
                k1.Visible = False
            End If

        Catch ex As Exception

        End Try


    End Sub

    Protected Sub ChangeLanguage(ByVal sender As Object, ByVal e As CommandEventArgs)

        Try
            Dim button As LinkButton = DirectCast(sender, LinkButton)
            Dim cultureCode As [String] = button.CommandArgument

            Try
                Session("CultureCode") = cultureCode
            Catch ex As Exception

            End Try
            'Page.Culture = cultureCode
            Page.UICulture = cultureCode

            
            Try
                Dim commonFun As PMSCommonFun
                commonFun = PMSCommonFun.getInstance
                url = commonFun.BrowserQueryString(Request)
                Dim mac As String = ""
                Dim qs As String = Request.QueryString("encry")

                MAC = commonFun.DecrptQueryString("MA", qs)
                Dim objdb As DbaseService
                objdb = DbaseService.getInstance
                objdb.insertUpdateDelete("insert into culture(mac, code) values('" & mac & "','" & cultureCode & "')")

            Catch ex As Exception

            End Try


            cc = cultureCode
            If cultureCode = "ar-SA" Then
                arabicstyle.Href = "arabic.css"
            Else
                arabicstyle.Href = "dmmy.css"
            End If
            Session("cu") = cultureCode
            'LinkButton9.ImageUrl = GetLocalResourceObject("c2").ToString();
            ' LinkButton10.ImageUrl = GetLocalResourceObject("c3").ToString();
        Catch ex As Exception

        End Try
      

    End Sub


    Protected Sub b1_click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton8.Click
        Dim commonFun As PMSCommonFun
        commonFun = PMSCommonFun.getInstance
        url = commonFun.BrowserQueryString(Request)
        Response.Redirect("newuser.aspx?" & url & "&ct=" & Page.UICulture)

    End Sub

    Protected Sub b1_click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton9.Click
        Dim commonFun As PMSCommonFun
        commonFun = PMSCommonFun.getInstance
        url = commonFun.BrowserQueryString(Request)
        Response.Redirect("pre_exist.aspx?" & url & "&ct=" & Page.UICulture)

    End Sub

    'Protected Sub b1_click2(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton10.Click


    '    Dim r1 As String = ""

    '    Try
    '        r1 = Request.QueryString("bk")

    '    Catch ex As Exception

    '    End Try


    '    Try
    '        Dim str1() = r1.Split(",")
    '        Dim ind As Integer = 0
    '        ind = str1.Length - 1

    '        r1 = str1(ind)
    '        '  ObjElog.write2LogFile("Mac_" & commonFun.DecrptQueryString("MA", qs), "MIFI after split r1" & r1)

    '        If r1 = "a" Then
    '            Dim commonFun As PMSCommonFun
    '            commonFun = PMSCommonFun.getInstance
    '            url = commonFun.BrowserQueryString(Request)
    '            Response.Redirect("newuser.aspx?" & url & "&ct=" & Page.UICulture)

    '        Else
    '            Dim commonFun As PMSCommonFun
    '            commonFun = PMSCommonFun.getInstance
    '            url = commonFun.BrowserQueryString(Request)
    '            Response.Redirect("plansel.aspx?" & url & "&ct=" & Page.UICulture)


    '        End If


    '    Catch ex As Exception

    '    End Try

    'End Sub


End Class