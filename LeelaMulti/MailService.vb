Imports System.Web.Mail
Imports PMSPkgSql
Imports System.Net
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



Imports System.Net.NetworkInformation
Imports System.Text
Imports System.Net.Sockets
Imports System.Data.Common

Public Enum MailTypes
    Login = 0
    NightAudit = 1
    BusinessCenter = 2
End Enum

Public Enum ErrTypes
    'C = Coupon, R  = Room, L = Login, B = Bill, P = Posted, A = Added, N = Nomadix, S = Successfully, F = Failure
    'Re = Relogn, Exp = Expired, Na = NightAudit, Bc = business center, Au - Already used
    'MAC = MachineId, BK = blocked, REG = Registration Code, GT = Getname


    RLBPandANS
    RLBPandANSGT
    RLF
    RLFGT
    RLANSprvBP
    RLFbutBP
    RLFbutBPGT
    RReLANS
    RReLF
    RReLMACchgS
    RReLMACchgF
    RLFMACBK
    RREGLF

    ndxRLBPandANS
    ndxRLF
    ndxRReLANS
    ndxRReLF
    ndxRReLMACchgS
    ndxRReLMACchgF

    CLF
    CLANS
    CReLANS
    CReLF
    CnotAN
    CExp
    CReLMACchgS
    CReLMACchgF

    NaLANSBF
    NaBPS
    NaBF
    NaANF

    BcLANS
    BcLF
    BcReLANS
    BcLFAu
    BcBPS
    BcBF
    RLFGT1
End Enum
Public Class MailService
    Private Shared gtMailServiceInst As MailService
    Private Sub New()
        'Nothing
    End Sub

    Public Shared Function getInstance() As MailService
        If gtMailServiceInst Is Nothing Then gtMailServiceInst = New MailService
        Return gtMailServiceInst
    End Function

    Public Function SendAdminMail(ByVal roomno As String, ByVal lastname As String, ByVal emailID As String) As String
        Dim Html_str, Subject, ExpiryTime, mailresult, Hotelname As String
        Dim Mtype As Integer
        Dim UsrTyp As String = "ROOM"

        Dim objlog As LoggerService
        objlog = LoggerService.gtInstance

        lastname = lastname.Trim()
        roomno = roomno.Trim()

        Dim rn As String = "Room No:"
        Dim ln As String = "Last Name:"

        Try
            If Not IsNumeric(roomno) Then
                rn = "Access Code:"
                ln = ""
                lastname = ""
            End If
        Catch ex As Exception

        End Try



        Html_str = "<div id='wrapper' style='width: 100%; font: 11px verdana;'>" & _
        "<div id='header' style='background: #201547; min-height: 3em; text-align: center;'>" & _
        "<img src='http://www.microsenseindia.com/mail/logo.jpg' alt=''>  </div>" & _
        "<div id='contents' style='min-height: 25em;'> <p style='font-family: Verdana; font-size: 12px; font-weight: bold; margin-left: 3%;line-height: 1.9em;'>" & _
        " Dear Guest,<br />  The following is your Login information. </p>" & _
       " <div style='margin-left: 3%; line-height: 20px; background: #e6e6e6; border: solid 1px #b3b3b3;   padding:1em;  width: 100%; margin-bottom: 20px; font-weight:bold;'>Login information.<br />" & _
       " <span style='width: 20%; display: inline-block;'>" & rn & " </span><span style='font-weight:bold; text-align:left;'>" & roomno & "</span>  <br />" & _
       "<span style='width: 20%; display: inline-block;'>" & ln & " </span><span style='font-weight:bold;text-align:left;'>" & lastname & "</span>   </div>" & _
      "<div style='color: #92703e; font-size: 14px; font-weight: bold; margin-left: 3%;'>  Have a Great Day! </div>" & _
     " <div style='border-top:solid 1px #b7915d; width:60%; margin-top:1em; margin-bottom:1em; margin-left:3%;'>   </div>" & _
      "  <div style='font-weight: bold; margin-left: 3%; margin-top: 1em;'> Note: Please do not reply to this mail. If you have any problems, please contact  HOTEL Support Team. </div>" & _
              "  <div style=1border-top:solid 1px #b7915d; width:60%; margin-top:1em; margin-bottom:1em; margin-left:3%;'></div>" & _
        " <div id='footer' style='background: #201547;min-height: 2em; text-align: right'><img src='http://www.microsenseindia.com/mail/footer_logo.jpg' alt=''/></div></div>"













        Try

            'Html_str = Html_str & "<table  border='1' align='left' >" & _
            '     "<tr bgcolor=#00CCFF><td colspan='2'>Hotel Name -- " & "The Leela Palace" & "</td></tr>" & _
            '     "<tr><td>RoomNo: </td><td>" & roomno & "</td></tr>" & _
            '     "<tr><td>LastName: </td><td>" & lastname & "</td></tr></table>"


            'Html_str = Html_str & "<div> <br/></br> </br></br> </br></br/> Have a Great Day! </div> " & Environment.NewLine
            'Html_str = Html_str & Environment.NewLine + "<div><br/> Note: Please do not reply to this mail. If you have any problems, please contact HOTEL Support Team.</div>"
            'Html_str = Html_str & Environment.NewLine + "<div><br/>For Assistance Please dial 0 from your telephone.</div>"


            Mtype = MailTypes.Login

            

            Dim text As String

            text = SendMailSMTPClient(Mtype, "Login Credential", Html_str, roomno, emailID)

            Return text
            objlog.write2LogFile("Mail", "text=" & text)
        Catch ex As Exception
            Return ex.Message
            objlog.write2LogFile("Mail", "er1" & ex.Message)
        End Try






    End Function

    'Private Sub Sendmail(ByVal mailtype As Integer, ByVal Subject As String, ByVal Body As String, ByVal logIdentifier As String, ByVal emailid As String)
    '    Dim FromAddr, ToAddr, CcAddr As String


    '    Dim Mail As New MailMessage




    '    Select Case (mailtype)
    '        Case MailTypes.Login
    '            FromAddr = objSysConfig.GetConfig("HlpDskID")
    '            ToAddr = objSysConfig.GetConfig("SF_ToAdd")
    '            CcAddr = objSysConfig.GetConfig("SF_CcAdd")
    '        Case Else
    '            FromAddr = objSysConfig.GetConfig("HlpDskID")
    '            ToAddr = objSysConfig.GetConfig("SF_ToAdd")
    '            CcAddr = objSysConfig.GetConfig("SF_CcAdd")
    '    End Select
    '    Dim objlog As LoggerService
    '    objlog = LoggerService.gtInstance

    '    Try
    '        SmtpMail.SmtpServer = "124.124.238.189"
    '        Mail.To = emailid ' To Address
    '        Mail.From = "hema@microsensesoftware.com"

    '        Mail.Subject = Subject ' message subject
    '        Mail.BodyFormat = MailFormat.Html 'Mail Format
    '        Mail.Body = "<html><body>" & Body & "</body></html>" 'Mail Body
    '        SmtpMail.Send(Mail) 'Send a Mail

    '    Catch ex As Exception

    '        objlog.write2LogFile("Mail", "er3" & ex.Message)


    '    End Try

    'End Sub


    Public Function SendMailSMTPClient(ByVal mailtype As Integer, ByVal Subject As String, ByVal Body As String, ByVal logIdentifier As String, ByVal emailid As String) As String


        If emailid <> "" Then
            Dim mail_Id As [String] = Nothing
            mail_Id = emailid

            Dim _newsLetterPath As [String] = "Mailindex.html"
            Dim objlog As LoggerService
            objlog = LoggerService.gtInstance
            Try
                Dim mailConfig As Configuration = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath)
                Dim settings As MailSettingsSectionGroup = DirectCast(mailConfig.GetSectionGroup("system.net/mailSettings"), MailSettingsSectionGroup)
                Dim from_Address As [String] = settings.Smtp.Network.UserName
                Dim ip_Address As [String] = settings.Smtp.Network.Host
                Dim password As [String] = settings.Smtp.Network.Password

                Try
                    objlog.write2LogFile("Mail", "1p" & ip_Address)
                Catch ex As Exception

                End Try

                MailUtil.SendMail(mail_Id, from_Address, "Login Credentials Hyatt", Body, ip_Address, password)
                Return "Email sent successfully"

            Catch ex As Exception
                objlog.write2LogFile("Mail", "er3" & ex.Message)


                Return ex.Message
                'throw ex;
                ' lblMessage.Text = ex.Message
                'if (System.IO.File.Exists(HttpContext.Current.Request.ApplicationPath + "/Log.txt"))
                '    {
                '        System.IO.StreamWriter Sr = new System.IO.StreamWriter(HttpContext.Current.Server.MapPath(HttpContext.Current.Request.ApplicationPath+"/Log.txt"),true);
                '        Sr.WriteLine(DateTime.Now.ToString()+":"+ex.ToString());
                '    }
                ' lblMessage.Visible = False
            End Try
            'txtEmail.Value = ""
        End If




        'Dim MailTo As String
        'Dim mailcc As String
        'Dim objlog As LoggerService
        'objlog = LoggerService.gtInstance

        'Try
        '    MailTo = emailid
        '    mailcc = "subbu@microsenseindia.com"
        '    If MailTo <> "" Then
        '        If Subject <> "" And Body <> "" Then
        '            Try
        '                Dim objMsg As New System.Net.Mail.MailMessage()
        '                objMsg.From = New System.Net.Mail.MailAddress("subbu@microsenseindia.com", "Service Desk Admin")
        '                If MailTo.Contains(",") Then
        '                    Dim arrTo As String() = MailTo.Split(",")
        '                    For Each ToAddress As String In arrTo
        '                        objMsg.To.Add(New System.Net.Mail.MailAddress(ToAddress))
        '                    Next
        '                Else
        '                    objMsg.To.Add(New System.Net.Mail.MailAddress(MailTo))
        '                End If
        '                'objMsg.To.Add(New System.Net.Mail.MailAddress("abraham@microsenseindia.com"))
        '                If mailcc <> "" Then
        '                    If mailcc.Contains(",") Then
        '                        Dim arrCC As String() = mailcc.Split(",")
        '                        For Each CCAddress As String In arrCC
        '                            objMsg.CC.Add(New System.Net.Mail.MailAddress(CCAddress))
        '                        Next
        '                    Else
        '                        objMsg.CC.Add(New System.Net.Mail.MailAddress(mailcc))
        '                    End If
        '                End If
        '                objMsg.Subject = Subject
        '                objMsg.Body = "MESSAGE FROM HOTEL HELP DESK:" + Environment.NewLine '+ Body
        '                objMsg.Body &= Environment.NewLine + "Note: Please do not reply to this mail. If you have any problems, please contact HOTEL Support Team."
        '                ' objMsg.Body &= Environment.NewLine + "For details, please see the ticket or call log at http://support.microsenseindia.com/"
        '                objMsg.IsBodyHtml = False
        '                objMsg.Priority = System.Net.Mail.MailPriority.High
        '                Dim objSmtpClient As New System.Net.Mail.SmtpClient()
        '                objSmtpClient.Port = 25
        '                objSmtpClient.Host = "mail.microsenseindia.com"
        '                objSmtpClient.UseDefaultCredentials = False
        '                objSmtpClient.EnableSsl = False
        '                Dim pass As String = "subbu"
        '                Dim basicCredential As New NetworkCredential("subbu@microsenseindia.com", pass)
        '                objSmtpClient.Credentials = basicCredential
        '                objSmtpClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network
        '                objSmtpClient.Send(objMsg)
        '                Return "Mail Sent Successfully"
        '            Catch ex As Exception
        '                If ex.ToString.Contains("Mailbox unavailable") And ex.ToString.Contains("we do not relay") Then
        '                    Return "The username or password is not correct."
        '                ElseIf ex.ToString.Contains("Mailbox unavailable") And ex.ToString.Contains("User unknown; rejecting") Then
        '                    Return "The email address " + ex.ToString.Remove(ex.ToString.IndexOf(">")).Remove(0, ex.ToString.IndexOf("<") + 1) + " is wrong. Please ask admin to correct it."
        '                Else
        '                    Console.WriteLine(ex.ToString)
        '                    Return ex.ToString
        '                End If
        '            End Try
        '        Else
        '            Return "Mail not sent. Subject or Body missing."
        '        End If
        '    Else
        '        Return "Mail not sent. To address is missing." + Environment.NewLine + "Ask the Admin to assign a user to this Region(s) in which the hotel is present"
        '    End If
        'Catch ex As Exception
        '    objlog.write2LogFile("Mail", "er31" & ex.Message)
        '    'LoggerService.LogException("SMTP001 - Exception trying to Send Emails", ex)
        '    Return ex.ToString
        'End Try
    End Function


End Class
