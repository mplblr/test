Imports System.Collections.Generic
Imports System.Text
Imports System.Net.Mail


Public Class MailUtil
    Private Shared context As System.Web.HttpContext = System.Web.HttpContext.Current

    ''' <summary>
    ''' A static method that will help to send an email through web
    ''' </summary>
    ''' <param name="toAddress">The address where to send the email</param>
    ''' <param name="fromAddress">The address where the email is coming from</param>
    ''' <param name="subject">The subject of the email message</param>
    ''' <param name="body">Message body</param>
    ''' <param name="host">The IP string of the SMPT hosting server</param>
    Public Shared Sub SendMail(ByVal toAddress As [String], ByVal fromAddress As [String], ByVal subject As [String], ByVal body As [String], ByVal host As [String], ByVal password As [String])
        Try
            Dim mailMessage As [String] = context.Server.HtmlDecode(body.ToString())

            Dim message As New MailMessage(New MailAddress(fromAddress), New MailAddress(toAddress))

            message.Subject = subject
            message.Body = mailMessage
            message.IsBodyHtml = True

            Dim client As New SmtpClient()
            client.Credentials = New System.Net.NetworkCredential(fromAddress, password)
            client.Host = host
            client.DeliveryMethod = SmtpDeliveryMethod.Network
            client.Send(message)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class


