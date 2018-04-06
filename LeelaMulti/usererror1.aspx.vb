Public Partial Class usererror1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        msgPara.InnerText = "Dear Guest, You are not authorized to access Internet, for further assistance please call reception. "
    End Sub

End Class