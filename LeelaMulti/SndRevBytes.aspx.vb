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
Partial Public Class SndRevBytes

    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            Try
                Dim r1 As String = ""

                Try
                    r1 = Request.QueryString("bid")


                    Dim str1() = r1.Split(",")
                    Dim ind As Integer = 0
                    ind = str1.Length - 1

                    r1 = str1(ind)

                Catch ex As Exception

                End Try


                Dim objdb As DbaseService
                objdb = DbaseService.getInstance

                Dim ds As DataSet = objdb.DsWithoutUpdate("select  planname,macid,uploadtx,downloadrx,total from bytesusagesummary  where billid=" & r1)

                gvwExample.DataSource = ds.Tables(0)

                gvwExample.DataBind()

            Catch ex As Exception

            End Try




            Try
                '
                'LinkButton10.ImageUrl = "~/images/backbutton.png"
            Catch ex As Exception

            End Try

            '    Dim roomNo, MACaddress As String
            '    roomNo = Request("RN")
            '    MACaddress = Request("MA")
            '    Dim userContext As New UserContext(roomNo, MACaddress)
            '    Dim gateWayQryResult As NdxQueryGatewayResults
            '    Dim gateWay As IGatewayService
            '    Dim gateWayFact As GatewayServiceFactory
            '    gateWayFact = GatewayServiceFactory.getInstance
            '    gateWay = gateWayFact.getGatewayService("FIDELIO", "2.3.8")
            '    gateWayQryResult = gateWay.query(userContext)
            '    Dim showMB As Double
            '    Dim bytesStr As String = ""
            '    showMB = Double.Parse(gateWayQryResult.ndxDateVolume)
            '    Try
            '        If ((showMB / 1073741824) > 1) Then
            '            bytesStr = Math.Round((showMB / 1073741824), 2) & " GB"
            '        ElseIf ((showMB / 1048576) > 1) Then
            '            bytesStr = Math.Round((showMB / 1048576), 2) & " MB"

            '        ElseIf ((showMB / 1024) > 1) Then
            '            bytesStr = Math.Round((showMB / 1024), 2) & " KB"
            '        Else
            '            bytesStr = Math.Round((showMB), 2) + " Bytes"
            '        End If
            '    Catch ex As Exception

            '    End Try
            '    Response.Write(bytesStr)

        End If


       
    End Sub

   

    Protected Sub btnActivateCode_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnActivateCode.Click
        Dim url As String = ""
        Dim commonFun As PMSCommonFun
        commonFun = PMSCommonFun.getInstance
        url = commonFun.BrowserQueryString(Request)
        Response.Redirect("postlogin.aspx?" & url & "&ct=" & Page.UICulture)

    End Sub
End Class