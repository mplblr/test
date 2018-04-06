Imports PMSPkgSql
Imports System.Threading
Imports System.Globalization

Partial Public Class terms
    Inherits System.Web.UI.Page

    Private Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender

        Dim img As ImageButton = DirectCast(Master.FindControl("LinkButton8"), ImageButton)
        img.ImageUrl = GetLocalResourceObject("C1").ToString()


        Dim img1 As ImageButton = DirectCast(Master.FindControl("LinkButton9"), ImageButton)
        img1.ImageUrl = GetLocalResourceObject("C2").ToString()


        'Dim img2 As ImageButton = DirectCast(Master.FindControl("LinkButton10"), ImageButton)
        'img2.ImageUrl = GetLocalResourceObject("C3").ToString()

        'Dim img2 As ImageButton = DirectCast(Master.FindControl("LinkButton10"), ImageButton)
        'img2.ImageUrl = "~/images/backbutton.png"


        Try
            'LinkButton10.ImageUrl = "~/images/backbutton.png"
        Catch ex As Exception

        End Try



    End Sub
    Protected Sub b1_click2()

        Dim r1 As String = ""

        Try
            r1 = Request.QueryString("bk")

        Catch ex As Exception

        End Try

        Dim url As String
        Try
            Dim str1() = r1.Split(",")
            Dim ind As Integer = 0
            ind = str1.Length - 1

            r1 = str1(ind)
            '  ObjElog.write2LogFile("Mac_" & commonFun.DecrptQueryString("MA", qs), "MIFI after split r1" & r1)

            If r1 = "a" Then
                Dim commonFun As PMSCommonFun
                commonFun = PMSCommonFun.getInstance
                url = commonFun.BrowserQueryString(Request)
                Response.Redirect("newuser.aspx?" & url & "&ct=" & Page.UICulture)

            ElseIf r1 = "c" Then
                Dim commonFun As PMSCommonFun
                commonFun = PMSCommonFun.getInstance
                url = commonFun.BrowserQueryString(Request)
                Response.Redirect("pre_exist.aspx?" & url & "&ct=" & Page.UICulture)

            ElseIf r1 = "e" Then
                Dim commonFun As PMSCommonFun
                commonFun = PMSCommonFun.getInstance
                url = commonFun.BrowserQueryString(Request)
                Response.Redirect("PostLogin.aspx?" & url & "&ct=" & Page.UICulture)


            Else
                Dim commonFun As PMSCommonFun
                commonFun = PMSCommonFun.getInstance
                url = commonFun.BrowserQueryString(Request)
                Response.Redirect("plansel.aspx?" & url & "&ct=" & Page.UICulture)


            End If


        Catch ex As Exception

        End Try

    End Sub

    Protected Sub btnActivateCode_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnActivateCode.Click
        b1_click2()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

End Class