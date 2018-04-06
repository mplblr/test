Imports PMSPkgSql
Imports System.Threading
Imports System.Globalization
Partial Public Class layout2
    Inherits System.Web.UI.MasterPage
    Public PlantimeN As Double

    Private Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender

        Try

            If Not Session("cu") Is Nothing Then
                Page.UICulture = Session("cu")

                If Session("cu") = "ar-SA" Then
                    arabicstyle.Href = "arabic.css"
                Else
                    arabicstyle.Href = "dmmy.css"
                End If
            End If


        Catch ex As Exception

        End Try

        Try
            If Session("cu") Is Nothing Then

                Dim objdb As DbaseService
                objdb = DbaseService.getInstance
                Dim ds As DataSet
                Dim commonFun As PMSCommonFun
                commonFun = PMSCommonFun.getInstance
                Dim url As String
                url = commonFun.BrowserQueryString(Request)
                Dim mac As String = ""
                Dim qs As String = Request.QueryString("encry")
                mac = commonFun.DecrptQueryString("MA", qs)
                ds = objdb.DsWithoutUpdate("select code from culture where mac='" & mac & "' order by cid desc")
                If ds.Tables(0).Rows.Count > 0 Then

                    Dim objlog As LoggerService
                    objlog = LoggerService.gtInstance
                    objlog.write2LogFile("b_culture", ds.Tables(0).Rows(0)(0))
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


    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try

            If Not Session("cu") Is Nothing Then
                Page.UICulture = Session("cu")

                If Session("cu") = "ar-SA" Then
                    arabicstyle.Href = "arabic.css"
                Else
                    arabicstyle.Href = "dmmy.css"
                End If
            End If


        Catch ex As Exception

        End Try

        Try
            If Session("cu") Is Nothing Then

                Dim objdb As DbaseService
                objdb = DbaseService.getInstance
                Dim ds As DataSet
                Dim commonFun As PMSCommonFun
                commonFun = PMSCommonFun.getInstance
                Dim url As String
                url = commonFun.BrowserQueryString(Request)
                Dim mac As String = ""
                Dim qs As String = Request.QueryString("encry")
                mac = commonFun.DecrptQueryString("MA", qs)
                ds = objdb.DsWithoutUpdate("select code from culture where mac='" & mac & "' order by cid desc")
                If ds.Tables(0).Rows.Count > 0 Then

                    Dim objlog As LoggerService
                    objlog = LoggerService.gtInstance
                    objlog.write2LogFile("b2_culture", ds.Tables(0).Rows(0)(0))
                    Page.UICulture = ds.Tables(0).Rows(0)(0)

                End If


            End If
        Catch ex As Exception

        End Try


        Try
            Dim qs As String = Request.QueryString("encry")
            Dim commonFun As PMSCommonFun
            commonFun = PMSCommonFun.getInstance
            If commonFun.DecrptQueryString("PORT", qs) = "11" Or commonFun.DecrptQueryString("PORT", qs) = "11" Then

                jj.Visible = False
                pp.Visible = False
            End If
        Catch ex As Exception

        End Try
        'Try
        '    PlantimeN = hdmsg.Text
        '    hdoffset.Value = DateTime.Now.ToString()
        'Catch ex As Exception

        'End Try

    End Sub

End Class