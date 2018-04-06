Imports System.Web.Mail
Imports PMSPkgSql
Public Enum MailTypes1
    Login = 0
    NightAudit = 1
    BusinessCenter = 2
End Enum

Public Enum ErrTypes1
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
End Enum

Public Class MailService1
    Private Shared gtMailServiceInst As MailService1

    Private Sub New()
        'Nothing
    End Sub

    Public Shared Function getInstance() As MailService1
        If gtMailServiceInst Is Nothing Then gtMailServiceInst = New MailService1
        Return gtMailServiceInst
    End Function

    Public Function SendAdminMail(ByVal rn As String, ByVal name As String, ByVal pid As String, ByVal err As String, ByVal mac As String, ByVal typeofMail As ErrTypes) As String
        Dim Html_str, Subject, ExpiryTime, mailresult, Hotelname As String
        Dim Mtype As Integer
        Dim UsrTyp As String = "ROOM"
        Dim objSysConfig As New CSysConfig
        Hotelname = objSysConfig.GetConfig("HotelName")
        Html_str = "Dear Help Desk<br />" & _
          "Have a Great Day! The following is the information about a SUIT guest, who tried to login.<br /><br /><br />"
        Subject = ""
        Select Case (typeofMail)
            'Case ErrTypes.ndxRReLANS
            '    'Re - Login Successfully
            '    Html_str = Html_str & "<table  border='1' align='center' >" & _
            '        "<tr bgcolor=#00CCFF><td colspan='2'>Information -- " & Hotelname & "</td></tr>" & _
            '        "<tr><td colspan='2'>Re-Login Added in Nomadix Successfully, No Bill Posted</td></tr>" & _
            '        "<tr><td>RoomNo: </td><td>" & userContext.roomNo & "</td></tr>" & _
            '        "<tr><td>LastName: </td><td>" & userContext.password & "</td></tr>" & _
            '        "<tr><td>RegCode: </td><td>" & userContext.regCode & "</td></tr>" & _
            '        "<tr><td>Mac: </td><td>" & userContext.machineId & "</td></tr>" & _
            '        "<tr><td>LoginTime: </td><td>" & Now() & "</td></tr>" & _
            '        "<tr><td>Remaning time: </td><td>" & userContext.item("remainingtime") & "</td></tr>" & _
            '        "<tr><td>User Type: </td><td> Room </td></tr>" & _
            '        "<tr><td>User Requested Url: </td><td>" & userContext.requestedPage & "</td></tr></table>"
            '    Subject = "Re-Login Succeed:" & Hotelname & " : " & userContext.roomNo
            '    Mtype = MailTypes.Login

            Case ErrTypes.ndxRReLF
                ' Re - Login Failure
                'Html_str = Html_str & "<table  border='1' align='center' >" & _
                '    "<tr bgcolor=#00CCFF><td colspan='2'>Information -- " & Hotelname & "</td></tr>" & _
                '    "<tr><td colspan='2'>Re-Login Failure, Not Added in Nomadix Successfully, No Bill Posted</td></tr>" & _
                '    "<tr><td>RoomNo: </td><td>" & userContext.roomNo & "</td></tr>" & _
                '    "<tr><td>LastName: </td><td>" & userContext.password & "</td></tr>" & _
                '    "<tr><td>RegCode: </td><td>" & userContext.regCode & "</td></tr>" & _
                '    "<tr><td>Mac: </td><td>" & userContext.machineId & "</td></tr>" & _
                '    "<tr><td>LoginTime: </td><td>" & Now() & "</td></tr>" & _
                '    "<tr><td>Remaning time: </td><td>" & userContext.item("remainingtime") & "</td></tr>" & _
                '    "<tr><td>User Type: </td><td> Room </td></tr>" & _
                '     "<tr><td>User Requested Url: </td><td>" & userContext.requestedPage & "</td></tr></table>"
                'Subject = "Re-Login Failure:" & Hotelname & " : " & userContext.roomNo
                'Mtype = MailTypes.Login

            Case ErrTypes.ndxRLBPandANS
                ' Successful(Login)
                'Dim objPlan As New CPlan
                'objPlan.getPlaninfo(userContext.selectedPlanId)
                'Html_str = Html_str & "<table  border='1' align='center' >" & _
                '    "<tr bgcolor=#00CCFF><td colspan='2'>Information -- " & Hotelname & "</td></tr>" & _
                '    "<tr><td colspan='2'>Bill Posted and Added in Nomadix Successfully </td></tr>" & _
                '    "<tr><td>RoomNo: </td><td>" & userContext.roomNo & "</td></tr>" & _
                '    "<tr><td>LastName: </td><td>" & userContext.password & "</td></tr>" & _
                '    "<tr><td>RegCode: </td><td>" & userContext.regCode & "</td></tr>" & _
                '    "<tr><td>Mac: </td><td>" & userContext.machineId & "</td></tr>" & _
                '    "<tr><td>LoginTime: </td><td>" & Now() & "</td></tr>" & _
                '    "<tr><td>Plan: </td><td>" & objPlan.planName & "</td></tr>" & _
                '    "<tr><td>Plan Amount: </td><td>" & objPlan.planAmount & "</td></tr>" & _
                '    "<tr><td>Duration: </td><td>" & objPlan.planTime & "</td></tr>" & _
                '    "<tr><td>User Type: </td><td> Room </td></tr>" & _
                '    "<tr><td>User Requested Url: </td><td>" & userContext.requestedPage & "</td></tr></table>"
                'Subject = "Login Succeed:" & Hotelname & " : " & userContext.roomNo
                'Mtype = MailTypes.Login

            Case ErrTypes.ndxRLF
                ' Invalid(RoomNo / LastName)
                'Html_str = Html_str & "<table  border='1' align='center' >" & _
                '     "<tr bgcolor=#00CCFF><td colspan='2'>Error Message -- " & Hotelname & "</td></tr>" & _
                '     "<tr><td colspan='2'>Invalid RoomNo / Lastname/ RegCode</td></tr>" & _
                '     "<tr><td>RoomNo: </td><td>" & userContext.roomNo & "</td></tr>" & _
                '     "<tr><td>LastName: </td><td>" & userContext.password & "</td></tr>" & _
                '     "<tr><td>RegCode: </td><td>" & userContext.regCode & "</td></tr>" & _
                '     "<tr><td>Mac Address: </td><td>" & userContext.machineId & "</td></tr>" & _
                '     "<tr><td>Failure Date & Time </td><td>" & Now() & "</td></tr>" & _
                '     "<tr><td>User Type: </td><td> Room </td></tr>" & _
                '     "<tr><td>User Requested Url: </td><td>" & userContext.requestedPage & "</td></tr></table>"
                'Subject = "Invalid User:" & Hotelname & " : " & userContext.roomNo
                'Mtype = MailTypes.Login

            Case ErrTypes.RLF
                ' Invalid(RoomNo / LastName)
                'Html_str = Html_str & "<table  border='1' align='center' >" & _
                '     "<tr bgcolor=#00CCFF><td colspan='2'>Error Message -- " & Hotelname & "</td></tr>" & _
                '     "<tr><td colspan='2'>Invalid RoomNo / Lastname</td></tr>" & _
                '     "<tr><td>RoomNo: </td><td>" & userContext.roomNo & "</td></tr>" & _
                '     "<tr><td>LastName: </td><td>" & userContext.password & "</td></tr>" & _
                '     "<tr><td>Mac Address: </td><td>" & userContext.machineId & "</td></tr>" & _
                '     "<tr><td>Failure Date & Time </td><td>" & Now() & "</td></tr>" & _
                '     "<tr><td>User Type: </td><td> Room </td></tr>" & _
                '     "<tr><td>PMS Result: </td><td>" & userContext.item("pmsrcode") & "</td></tr>" & _
                '     "<tr><td>PMS Message: </td><td>" & userContext.item("pmsrmessage") & "</td></tr>" & _
                '     "<tr><td>User Requested Url: </td><td>" & userContext.requestedPage & "</td></tr></table>"
                'Subject = "Invalid User:" & Hotelname & " : " & userContext.roomNo
                'Mtype = MailTypes.Login

            Case ErrTypes.RLFGT
                'Invalid(RoomNo / LastName)
                Html_str = Html_str & "<table  border='1' align='center' >" & _
                     "<tr bgcolor=#00CCFF><td colspan='2'>Error Message -- " & Hotelname & "</td></tr>" & _
                     "<tr><td colspan='2'>Invalid RoomNo / Lastname</td></tr>" & _
                     "<tr><td>RoomNo: </td><td>" & rn & "</td></tr>" & _
                     "<tr><td>LastName: </td><td>" & name & "</td></tr>" & _
                     "<tr><td>Mac Address: </td><td>" & mac & "</td></tr>" & _
                     "<tr><td>Failure Date & Time </td><td>" & Now() & "</td></tr>" & _
                     "<tr><td>User Type: </td><td>SUIT Room </td></tr>" & _
                     "<tr><td>PMS Message: </td><td>" & err & "</td></tr>" & _
                     "</table>"
                Subject = "Invalid User:" & Hotelname & " : " & rn
                Mtype = MailTypes.Login
            Case ErrTypes.RREGLF
                '   Invalid(RoomNo / RegCode)
                '    Html_str = Html_str & "<table  border='1' align='center' >" & _
                '         "<tr bgcolor=#00CCFF><td colspan='2'>Error Message -- " & Hotelname & "</td></tr>" & _
                '         "<tr><td colspan='2'>Invalid RoomNo / RegCode</td></tr>" & _
                '         "<tr><td>RoomNo: </td><td>" & userContext.roomNo & "</td></tr>" & _
                '         "<tr><td>RegCode: </td><td>" & userContext.regCode & "</td></tr>" & _
                '         "<tr><td>Mac Address: </td><td>" & userContext.machineId & "</td></tr>" & _
                '         "<tr><td>Failure Date & Time </td><td>" & Now() & "</td></tr>" & _
                '         "<tr><td>User Type: </td><td> Room </td></tr>" & _
                '         "<tr><td>PMS Result: </td><td>" & userContext.item("pmsrcode") & "</td></tr>" & _
                '         "<tr><td>PMS Message: </td><td>" & userContext.item("pmsrmessage") & "</td></tr>" & _
                '         "<tr><td>User Requested Url: </td><td>" & userContext.requestedPage & "</td></tr></table>"
                '    Subject = "Invalid User:" & Hotelname & " : " & userContext.roomNo
                '    Mtype = MailTypes.Login

                'Case ErrTypes.RLFMACBK
                '    ' Invalid RoomNo / LastName your MAC Address BLOCKED
                '    Html_str = Html_str & "<table  border='1' align='center' >" & _
                '         "<tr bgcolor=#00CCFF><td colspan='2'>Error Message -- " & Hotelname & "</td></tr>" & _
                '         "<tr><td colspan='2'>MAC Address BLOCKED</td></tr>" & _
                '         "<tr><td>RoomNo: </td><td>" & userContext.roomNo & "</td></tr>" & _
                '         "<tr><td>LastName: </td><td>" & userContext.password & "</td></tr>" & _
                '         "<tr><td>Mac Address: </td><td>" & userContext.machineId & "</td></tr>" & _
                '         "<tr><td>Failure Date & Time </td><td>" & Now() & "</td></tr>" & _
                '         "<tr><td>User Type: </td><td> Room </td></tr>" & _
                '         "<tr><td>User Requested Url: </td><td>" & userContext.requestedPage & "</td></tr></table>"
                '    Subject = "MAC Address BLOCKED:" & Hotelname & " : " & userContext.roomNo
                '    Mtype = MailTypes.Login

                'Case ErrTypes.RLBPandANS
                '    ' Successful(Login)
                '    Dim objPlan As New CPlan
                '    objPlan.getPlaninfo(userContext.selectedPlanId)
                '    Html_str = Html_str & "<table  border='1' align='center' >" & _
                '        "<tr bgcolor=#00CCFF><td colspan='2'>Information -- " & Hotelname & "</td></tr>" & _
                '        "<tr><td colspan='2'>Bill Posted and Added in Nomadix Successfully </td></tr>" & _
                '        "<tr><td>RoomNo: </td><td>" & userContext.roomNo & "</td></tr>" & _
                '        "<tr><td>LastName: </td><td>" & userContext.password & "</td></tr>" & _
                '        "<tr><td>Mac: </td><td>" & userContext.machineId & "</td></tr>" & _
                '        "<tr><td>LoginTime: </td><td>" & Now() & "</td></tr>" & _
                '        "<tr><td>Plan: </td><td>" & objPlan.planName & "</td></tr>" & _
                '        "<tr><td>Plan Amount: </td><td>" & objPlan.planAmount & "</td></tr>" & _
                '        "<tr><td>Duration: </td><td>" & objPlan.planTime & "</td></tr>" & _
                '        "<tr><td>User Type: </td><td> Room </td></tr>" & _
                '        "<tr><td>PMS Result: </td><td>" & userContext.item("pmsrcode") & "</td></tr>" & _
                '        "<tr><td>PMS Message: </td><td>" & userContext.item("pmsrmessage") & "</td></tr>" & _
                '        "<tr><td>User Requested Url: </td><td>" & userContext.requestedPage & "</td></tr></table>"
                '    Subject = "Login Succeed:" & Hotelname & " : " & userContext.roomNo
                '    Mtype = MailTypes.Login

            Case ErrTypes.RLBPandANSGT
                ' Successful Login for 2.3.7
                Dim objPlan As New CPlan
                objPlan.getPlaninfo(pid)
                Html_str = Html_str & "<table  border='1' align='center' >" & _
                    "<tr bgcolor=#00CCFF><td colspan='2'>Information -- " & Hotelname & "</td></tr>" & _
                    "<tr><td colspan='2'>Bill Posted and Added in Nomadix Successfully </td></tr>" & _
                    "<tr><td>RoomNo: </td><td>" & rn & "</td></tr>" & _
                    "<tr><td>LastName: </td><td>" & name & "</td></tr>" & _
                    "<tr><td>Mac: </td><td>" & mac & "</td></tr>" & _
                    "<tr><td>LoginTime: </td><td>" & Now() & "</td></tr>" & _
                    "<tr><td>Plan: </td><td>" & objPlan.planName & "</td></tr>" & _
                    "<tr><td>Plan Amount: </td><td>" & objPlan.planAmount & "</td></tr>" & _
                    "<tr><td>Duration: </td><td>" & objPlan.planTime & "</td></tr>" & _
                    "<tr><td>User Type: </td><td> SUIT Room </td></tr>" & _
                    "<tr><td>PMS Result: </td><td>" & err & "</td></tr>" & _
                    "</table>"
                Subject = "Login Succeed:" & Hotelname & " : " & rn
                Mtype = MailTypes.Login

                'Case ErrTypes.RLANSprvBP
                '    ' Successful Login for Previous Bill post (bill posted, but not added in the nomadix)
                '    Dim objPlan As New CPlan
                '    objPlan.getPlaninfo(userContext.item("planid"))
                '    Html_str = Html_str & "<table  border='1' align='center' >" & _
                '        "<tr bgcolor=#00CCFF><td colspan='2'>Information -- " & Hotelname & "</td></tr>" & _
                '        "<tr><td colspan='2'>Added in Nomadix Successfully, Already Bill Posted</td></tr>" & _
                '        "<tr><td>RoomNo: </td><td>" & userContext.roomNo & "</td></tr>" & _
                '        "<tr><td>LastName: </td><td>" & userContext.password & "</td></tr>" & _
                '        "<tr><td>Mac: </td><td>" & userContext.machineId & "</td></tr>" & _
                '        "<tr><td>LoginTime: </td><td>" & Now() & "</td></tr>" & _
                '        "<tr><td>Plan: </td><td>" & objPlan.planName & "</td></tr>" & _
                '        "<tr><td>Plan Amount: </td><td>" & objPlan.planAmount & "</td></tr>" & _
                '        "<tr><td>Duration: </td><td>" & userContext.item("remainingtime") & "</td></tr>" & _
                '        "<tr><td>User Type: </td><td> Room </td></tr>" & _
                '        "<tr><td>User Requested Url: </td><td>" & userContext.requestedPage & "</td></tr></table>"
                '    Subject = "Login Succeed and Bill Posted already:" & Hotelname & " : " & userContext.roomNo
                '    Mtype = MailTypes.Login

                'Case ErrTypes.RLFbutBP
                '    ' Login failure because of Gateway error but Bill was posted in PMS
                '    Dim objPlan As New CPlan
                '    objPlan.getPlaninfo(userContext.selectedPlanId)
                '    Html_str = Html_str & "<table  border='1' align='center' >" & _
                '        "<tr bgcolor=#00CCFF><td colspan='2'>Information -- " & Hotelname & "</td></tr>" & _
                '        "<tr><td colspan='2'>Login Failure but Bill Posted</td></tr>" & _
                '        "<tr><td>RoomNo: </td><td>" & userContext.roomNo & "</td></tr>" & _
                '        "<tr><td>LastName: </td><td>" & userContext.password & "</td></tr>" & _
                '        "<tr><td>Mac: </td><td>" & userContext.machineId & "</td></tr>" & _
                '        "<tr><td>LoginTime: </td><td>" & Now() & "</td></tr>" & _
                '        "<tr><td>Plan: </td><td>" & objPlan.planName & "</td></tr>" & _
                '        "<tr><td>Plan Amount: </td><td>" & objPlan.planAmount & "</td></tr>" & _
                '        "<tr><td>Duration: </td><td>" & objPlan.planTime & "</td></tr>" & _
                '        "<tr><td>User Type: </td><td> Room </td></tr>" & _
                '        "<tr><td>PMS Result: </td><td>" & userContext.item("pmsrcode") & "</td></tr>" & _
                '        "<tr><td>PMS Message: </td><td>" & userContext.item("pmsrmessage") & "</td></tr>" & _
                '        "<tr><td>User Requested Url: </td><td>" & userContext.requestedPage & "</td></tr></table>"
                '    Subject = "Login Failure, but Bill Posted:" & Hotelname & " : " & userContext.roomNo
                '    Mtype = MailTypes.Login
                'Case ErrTypes.RLFbutBPGT
                '    '   Login failure because of Gateway error but Bill was posted in PMS
                '    Dim objPlan As New CPlan
                '    objPlan.getPlaninfo(userContext.selectedPlanId)
                '    Html_str = Html_str & "<table  border='1' align='center' >" & _
                '        "<tr bgcolor=#00CCFF><td colspan='2'>Information -- " & Hotelname & "</td></tr>" & _
                '        "<tr><td colspan='2'>Login Failure but Bill Posted</td></tr>" & _
                '        "<tr><td>RoomNo: </td><td>" & userContext.roomNo & "</td></tr>" & _
                '        "<tr><td>LastName: </td><td>" & userContext.item("guestname") & "</td></tr>" & _
                '        "<tr><td>Mac: </td><td>" & userContext.machineId & "</td></tr>" & _
                '        "<tr><td>LoginTime: </td><td>" & Now() & "</td></tr>" & _
                '        "<tr><td>Plan: </td><td>" & objPlan.planName & "</td></tr>" & _
                '        "<tr><td>Plan Amount: </td><td>" & objPlan.planAmount & "</td></tr>" & _
                '        "<tr><td>Duration: </td><td>" & objPlan.planTime & "</td></tr>" & _
                '        "<tr><td>User Type: </td><td> Room </td></tr>" & _
                '        "<tr><td>PMS Result: </td><td>" & userContext.item("pmsrcode") & "</td></tr>" & _
                '        "<tr><td>PMS Message: </td><td>" & userContext.item("pmsrmessage") & "</td></tr>" & _
                '        "<tr><td>User Requested Url: </td><td>" & userContext.requestedPage & "</td></tr></table>"
                '    Subject = "Login Failure, but Bill Posted:" & Hotelname & " : " & userContext.roomNo
                '    Mtype = MailTypes.Login

                'Case ErrTypes.RReLANS
                '    ' Re - Login Successfully
                '    Html_str = Html_str & "<table  border='1' align='center' >" & _
                '        "<tr bgcolor=#00CCFF><td colspan='2'>Information -- " & Hotelname & "</td></tr>" & _
                '        "<tr><td colspan='2'>Re-Login Added in Nomadix Successfully, No Bill Posted</td></tr>" & _
                '        "<tr><td>RoomNo: </td><td>" & userContext.roomNo & "</td></tr>" & _
                '        "<tr><td>LastName: </td><td>" & userContext.password & "</td></tr>" & _
                '        "<tr><td>Mac: </td><td>" & userContext.machineId & "</td></tr>" & _
                '        "<tr><td>LoginTime: </td><td>" & Now() & "</td></tr>" & _
                '        "<tr><td>Remaning time: </td><td>" & userContext.item("remainingtime") & "</td></tr>" & _
                '        "<tr><td>User Type: </td><td> Room </td></tr>" & _
                '        "<tr><td>User Requested Url: </td><td>" & userContext.requestedPage & "</td></tr></table>"
                '    Subject = "Re-Login Succeed:" & Hotelname & " : " & userContext.roomNo
                '    Mtype = MailTypes.Login

                'Case ErrTypes.RReLF
                '    ' Re - Login Failure
                '    Html_str = Html_str & "<table  border='1' align='center' >" & _
                '        "<tr bgcolor=#00CCFF><td colspan='2'>Information -- " & Hotelname & "</td></tr>" & _
                '        "<tr><td colspan='2'>Re-Login Failure, Not Added in Nomadix Successfully, No Bill Posted</td></tr>" & _
                '        "<tr><td>RoomNo: </td><td>" & userContext.roomNo & "</td></tr>" & _
                '        "<tr><td>LastName: </td><td>" & userContext.password & "</td></tr>" & _
                '        "<tr><td>Mac: </td><td>" & userContext.machineId & "</td></tr>" & _
                '        "<tr><td>LoginTime: </td><td>" & Now() & "</td></tr>" & _
                '        "<tr><td>Remaning time: </td><td>" & userContext.item("remainingtime") & "</td></tr>" & _
                '        "<tr><td>User Type: </td><td> Room </td></tr>" & _
                '         "<tr><td>User Requested Url: </td><td>" & userContext.requestedPage & "</td></tr></table>"
                '    Subject = "Re-Login Failure:" & Hotelname & " : " & userContext.roomNo
                '    Mtype = MailTypes.Login

                'Case ErrTypes.CLF
                '    ' Re - Login Failure
                '    Html_str = Html_str & "<table  border='1' align='center' >" & _
                '        "<tr bgcolor=#00CCFF><td colspan='2'>Information -- " & Hotelname & "</td></tr>" & _
                '        "<tr><td colspan='2'>InValid Username / Password</td></tr>" & _
                '        "<tr><td>RoomNo: </td><td>" & userContext.userId & "</td></tr>" & _
                '        "<tr><td>LastName: </td><td>" & userContext.password & "</td></tr>" & _
                '        "<tr><td>Mac: </td><td>" & userContext.machineId & "</td></tr>" & _
                '        "<tr><td>LoginTime: </td><td>" & Now() & "</td></tr>" & _
                '        "<tr><td>User Type: </td><td> Coupon </td></tr>" & _
                '        "<tr><td>User Requested Url: </td><td>" & userContext.requestedPage & "</td></tr></table>"
                '    Subject = "Invalid User, Login Failure:" & Hotelname & " : " & userContext.userId
                '    Mtype = MailTypes.Login

                'Case ErrTypes.CLANS
                '    '  Coupon Successful Login
                '    Dim objPlan As New CPlan
                '    If userContext.item("coupontype") = ECOUPONTYPE.BULKEXTRA Then
                '        objPlan.getPlaninfo(userContext.selectedPlanId, ECOUPONTYPE.BULKEXTRA)

                '    Else
                '        objPlan.getPlaninfo(userContext.selectedPlanId)

                '    End If
                '    Html_str = Html_str & "<table  border='1' align='center' >" & _
                '        "<tr bgcolor=#00CCFF><td colspan='2'>Information -- " & Hotelname & "</td></tr>" & _
                '        "<tr><td colspan='2'>Bill Posted and Added in Nomadix Successfully & </td></tr>" & _
                '        "<tr><td>Username: </td><td>" & userContext.userId & "</td></tr>" & _
                '        "<tr><td>Password: </td><td>" & userContext.password & "</td></tr>" & _
                '        "<tr><td>Mac: </td><td>" & userContext.machineId & "</td></tr>" & _
                '        "<tr><td>LoginTime: </td><td>" & Now() & "</td></tr>" & _
                '        "<tr><td>Plan: </td><td>" & objPlan.planName & "</td></tr>" & _
                '        "<tr><td>Plan Amount: </td><td>" & objPlan.planAmount & "</td></tr>" & _
                '        "<tr><td>Duration: </td><td>" & objPlan.planTime & "</td></tr>" & _
                '        "<tr><td>User Type: </td><td> Coupon </td></tr>" & _
                '        "<tr><td>User Requested Url: </td><td>" & userContext.requestedPage & "</td></tr></table>"
                '    Subject = "Login Succeed:" & Hotelname & " : " & userContext.userId
                '    Mtype = MailTypes.Login

                'Case ErrTypes.CReLANS
                '    '  Coupon Re - Login Successfully                
                '    Html_str = Html_str & "<table  border='1' align='center' >" & _
                '        "<tr bgcolor=#00CCFF><td colspan='2'>Information -- " & Hotelname & "</td></tr>" & _
                '        "<tr><td colspan='2'>Coupon Re-Login Added in Nomadix Successfully, No Bill Posted</td></tr>" & _
                '        "<tr><td>UserName: </td><td>" & userContext.userId & "</td></tr>" & _
                '        "<tr><td>Password: </td><td>" & userContext.password & "</td></tr>" & _
                '        "<tr><td>Mac: </td><td>" & userContext.machineId & "</td></tr>" & _
                '        "<tr><td>LoginTime: </td><td>" & Now() & "</td></tr>" & _
                '        "<tr><td>Remaning time: </td><td>" & userContext.item("remainingtime") & "</td></tr>" & _
                '        "<tr><td>User Type: </td><td> Coupon </td></tr>" & _
                '        "<tr><td>User Requested Url: </td><td>" & userContext.requestedPage & "</td></tr></table>"
                '    Subject = "Coupon Re-Login Succeed:" & Hotelname & " : " & userContext.userId
                '    Mtype = MailTypes.Login

                'Case ErrTypes.CReLF
                '    '  Coupon Re - Login Failure
                '    Html_str = Html_str & "<table  border='1' align='center' >" & _
                '        "<tr bgcolor=#00CCFF><td colspan='2'>Information -- " & Hotelname & "</td></tr>" & _
                '        "<tr><td colspan='2'>Coupon Re-Login, Not Added in Nomadix Successfully, No Bill Posted</td></tr>" & _
                '        "<tr><td>UserName: </td><td>" & userContext.userId & "</td></tr>" & _
                '        "<tr><td>Password: </td><td>" & userContext.password & "</td></tr>" & _
                '        "<tr><td>Mac: </td><td>" & userContext.machineId & "</td></tr>" & _
                '        "<tr><td>LoginTime: </td><td>" & Now() & "</td></tr>" & _
                '        "<tr><td>Remaning time: </td><td>" & userContext.item("remainingtime") & "</td></tr>" & _
                '        "<tr><td>User Type: </td><td> Coupon </td></tr>" & _
                '        "<tr><td>User Requested Url: </td><td>" & userContext.requestedPage & "</td></tr></table>"
                '    Subject = "Coupon Re-Login Succeed:" & Hotelname & " : " & userContext.userId
                '    Mtype = MailTypes.Login


                'Case ErrTypes.CnotAN
                '    ' Coupon Re - Login Failure
                '    Html_str = Html_str & "<table  border='1' align='center' >" & _
                '        "<tr bgcolor=#00CCFF><td colspan='2'>Information -- " & Hotelname & "</td></tr>" & _
                '        "<tr><td colspan='2'>Coupon Re-Login, Not Added in Nomadix Successfully, No Bill Posted</td></tr>" & _
                '        "<tr><td>UserName: </td><td>" & userContext.userId & "</td></tr>" & _
                '        "<tr><td>Password: </td><td>" & userContext.password & "</td></tr>" & _
                '        "<tr><td>Mac: </td><td>" & userContext.machineId & "</td></tr>" & _
                '        "<tr><td>LoginTime: </td><td>" & Now() & "</td></tr>" & _
                '        "<tr><td>Remaning time: </td><td>" & userContext.item("remainingtime") & "</td></tr>" & _
                '        "<tr><td>User Type: </td><td> Coupon </td></tr>" & _
                '        "<tr><td>User Requested Url: </td><td>" & userContext.requestedPage & "</td></tr></table>"
                '    Subject = "Coupon Re-Login Succeed:" & Hotelname & " : " & userContext.userId
                '    Mtype = MailTypes.Login

                'Case ErrTypes.CExp
                '    '  Coupon(Expired)
                '    Html_str = Html_str & "<table  border='1' align='center' >" & _
                '         "<tr bgcolor=#00CCFF><td colspan='2'>Error Message -- " & Hotelname & "</td></tr>" & _
                '         "<tr><td colspan='2'>Coupon Expired</td></tr>" & _
                '         "<tr><td>Username: </td><td>" & userContext.userId & "</td></tr>" & _
                '         "<tr><td>Password: </td><td>" & userContext.password & "</td></tr>" & _
                '         "<tr><td>Mac Address: </td><td>" & userContext.machineId & "</td></tr>" & _
                '         "<tr><td>User Type: </td><td> Coupon </td></tr>" & _
                '         "<tr><td>Failure Time: </td><td>" & Now() & "</td></tr>" & _
                '         "<tr><td>User Requested Url: </td><td>" & userContext.requestedPage & "</td></tr></table>"
                '    Subject = "Coupon Expired:" & Hotelname & " : " & userContext.userId
                '    Mtype = MailTypes.Login

                'Case ErrTypes.ndxRReLMACchgS
                '    ' Mac Change wired to wireless vice versa
                '    Html_str = Html_str & "<table  border='1' align='center' >" & _
                '        "<tr bgcolor=#00CCFF><td colspan='2'>Information -- " & Hotelname & "</td></tr>" & _
                '        "<tr><td colspan='2'>ReLogin Mac Change wired to wireless vice versa</td></tr>" & _
                '        "<tr><td>UserName: </td><td>" & userContext.roomNo & "</td></tr>" & _
                '        "<tr><td>GuestName: </td><td>" & userContext.password & "</td></tr>" & _
                '        "<tr><td>RegCode: </td><td>" & userContext.regCode & "</td></tr>" & _
                '        "<tr><td>Old Mac: </td><td>" & userContext.item("oldmac") & "</td></tr>" & _
                '        "<tr><td>New Mac: </td><td>" & userContext.machineId & "</td></tr>" & _
                '        "<tr><td>MacChange Time: </td><td>" & Now() & "</td></tr>" & _
                '        "<tr><td>Duration: </td><td>" & userContext.item("remainingtime") & "</td></tr>" & _
                '        "<tr><td>User Type: </td><td> Room </td></tr>" & _
                '        "<tr><td>User Requested Url: </td><td>" & userContext.requestedPage & "</td></tr></table>"
                '    Subject = "MAC Change Succeed:" & Hotelname & " : " & userContext.roomNo
                '    Mtype = MailTypes.Login

                'Case ErrTypes.ndxRReLMACchgF
                '    '  Mac Change wired to wireless vice versa, but field in gateway addition
                '    Html_str = Html_str & "<table  border='1' align='center' >" & _
                '        "<tr bgcolor=#00CCFF><td colspan='2'>Information -- " & Hotelname & "</td></tr>" & _
                '        "<tr><td colspan='2'>ReLogin, Mac Change wired to wireless vice versa Failed, GateWay Error </td></tr>" & _
                '        "<tr><td>UserName: </td><td>" & userContext.roomNo & "</td></tr>" & _
                '        "<tr><td>GuestName: </td><td>" & userContext.password & "</td></tr>" & _
                '        "<tr><td>RegCode: </td><td>" & userContext.regCode & "</td></tr>" & _
                '        "<tr><td>Old Mac: </td><td>" & userContext.item("oldmac") & "</td></tr>" & _
                '        "<tr><td>New Mac: </td><td>" & userContext.machineId & "</td></tr>" & _
                '        "<tr><td>MacChange Time: </td><td>" & Now() & "</td></tr>" & _
                '        "<tr><td>Duration: </td><td>" & userContext.item("remainingtime") & "</td></tr>" & _
                '        "<tr><td>User Type: </td><td> Room </td></tr>" & _
                '        "<tr><td>User Requested Url: </td><td>" & userContext.requestedPage & "</td></tr></table>"
                '    Subject = "MAC Change Succeed:" & Hotelname & " : " & userContext.roomNo
                '    Mtype = MailTypes.Login

                'Case ErrTypes.RReLMACchgS
                '    '   Mac Change wired to wireless vice versa
                '    Html_str = Html_str & "<table  border='1' align='center' >" & _
                '        "<tr bgcolor=#00CCFF><td colspan='2'>Information -- " & Hotelname & "</td></tr>" & _
                '        "<tr><td colspan='2'>ReLogin Mac Change wired to wireless vice versa</td></tr>" & _
                '        "<tr><td>UserName: </td><td>" & userContext.roomNo & "</td></tr>" & _
                '        "<tr><td>Password: </td><td>" & userContext.password & "</td></tr>" & _
                '        "<tr><td>Old Mac: </td><td>" & userContext.item("oldmac") & "</td></tr>" & _
                '        "<tr><td>New Mac: </td><td>" & userContext.machineId & "</td></tr>" & _
                '        "<tr><td>MacChange Time: </td><td>" & Now() & "</td></tr>" & _
                '        "<tr><td>Duration: </td><td>" & userContext.item("remainingtime") & "</td></tr>" & _
                '        "<tr><td>User Type: </td><td> Room </td></tr>" & _
                '        "<tr><td>User Requested Url: </td><td>" & userContext.requestedPage & "</td></tr></table>"
                '    Subject = "MAC Change Succeed:" & Hotelname & " : " & userContext.roomNo
                '    Mtype = MailTypes.Login

                'Case ErrTypes.RReLMACchgF
                '    '   Mac Change wired to wireless vice versa, but field in gateway addition
                '    Html_str = Html_str & "<table  border='1' align='center' >" & _
                '        "<tr bgcolor=#00CCFF><td colspan='2'>Information -- " & Hotelname & "</td></tr>" & _
                '        "<tr><td colspan='2'>ReLogin, Mac Change wired to wireless vice versa Failed, GateWay Error </td></tr>" & _
                '        "<tr><td>UserName: </td><td>" & userContext.roomNo & "</td></tr>" & _
                '        "<tr><td>Password: </td><td>" & userContext.password & "</td></tr>" & _
                '        "<tr><td>Old Mac: </td><td>" & userContext.item("oldmac") & "</td></tr>" & _
                '        "<tr><td>New Mac: </td><td>" & userContext.machineId & "</td></tr>" & _
                '        "<tr><td>MacChange Time: </td><td>" & Now() & "</td></tr>" & _
                '        "<tr><td>Duration: </td><td>" & userContext.item("remainingtime") & "</td></tr>" & _
                '        "<tr><td>User Type: </td><td> Room </td></tr>" & _
                '        "<tr><td>User Requested Url: </td><td>" & userContext.requestedPage & "</td></tr></table>"
                '    Subject = "MAC Change Succeed:" & Hotelname & " : " & userContext.roomNo
                '    Mtype = MailTypes.Login

                'Case ErrTypes.CReLMACchgS
                '    '  Mac Change wired to wireless vice versa
                '    Html_str = Html_str & "<table  border='1' align='center' >" & _
                '        "<tr bgcolor=#00CCFF><td colspan='2'>Information -- " & Hotelname & "</td></tr>" & _
                '        "<tr><td colspan='2'>ReLogin Mac Change wired to wireless vice versa</td></tr>" & _
                '        "<tr><td>UserName: </td><td>" & userContext.userId & "</td></tr>" & _
                '        "<tr><td>GuestName: </td><td>" & userContext.password & "</td></tr>" & _
                '        "<tr><td>Old Mac: </td><td>" & userContext.item("oldmac") & "</td></tr>" & _
                '        "<tr><td>New Mac: </td><td>" & userContext.machineId & "</td></tr>" & _
                '        "<tr><td>MacChange Time: </td><td>" & Now() & "</td></tr>" & _
                '        "<tr><td>Duration: </td><td>" & userContext.item("remainingtime") & "</td></tr>" & _
                '        "<tr><td>User Type: </td><td> Coupon </td></tr>" & _
                '        "<tr><td>User Requested Url: </td><td>" & userContext.requestedPage & "</td></tr></table>"
                '    Subject = "MAC Change Succeed:" & Hotelname & " : " & userContext.userId
                '    Mtype = MailTypes.Login

                'Case ErrTypes.CReLMACchgF
                '    ' Mac Change wired to wireless vice versa, but field in gateway addition
                '    Html_str = Html_str & "<table  border='1' align='center' >" & _
                '        "<tr bgcolor=#00CCFF><td colspan='2'>Information -- " & Hotelname & "</td></tr>" & _
                '        "<tr><td colspan='2'>ReLogin, Mac Change wired to wireless vice versa Failed, GateWay Error </td></tr>" & _
                '        "<tr><td>UserName: </td><td>" & userContext.userId & "</td></tr>" & _
                '        "<tr><td>GuestName: </td><td>" & userContext.password & "</td></tr>" & _
                '        "<tr><td>RegCode: </td><td>" & userContext.regCode & "</td></tr>" & _
                '        "<tr><td>Old Mac: </td><td>" & userContext.item("oldmac") & "</td></tr>" & _
                '        "<tr><td>New Mac: </td><td>" & userContext.machineId & "</td></tr>" & _
                '        "<tr><td>MacChange Time: </td><td>" & Now() & "</td></tr>" & _
                '        "<tr><td>Duration: </td><td>" & userContext.item("remainingtime") & "</td></tr>" & _
                '        "<tr><td>User Type: </td><td> Coupon </td></tr>" & _
                '        "<tr><td>User Requested Url: </td><td>" & userContext.requestedPage & "</td></tr></table>"
                '    Subject = "MAC Change Succeed:" & Hotelname & " : " & userContext.userId
                '    Mtype = MailTypes.Login

                'Case ErrTypes.NaLANSBF
                '    ' During Night Audit Login And Added in Nomadix but bill posting is pending
                '    Dim objPlan As New CPlan
                '    objPlan.getPlaninfo(userContext.selectedPlanId)
                '    Html_str = Html_str & "<table  border='1' align='center' >" & _
                '                         "<tr bgcolor=#00CCFF><td colspan='2'>Information Message -- " & Hotelname & "</td></tr>" & _
                '                         "<tr><td colspan='2'>Night Audit--Login Successfully, but bill posting is Pending</td></tr>" & _
                '                         "<tr><td>Username: </td><td>" & userContext.roomNo & "</td></tr>" & _
                '                         "<tr><td>Password: </td><td>" & userContext.guestName & "</td></tr>" & _
                '                         "<tr><td>Mac Address: </td><td>" & userContext.machineId & "</td></tr>" & _
                '                         "<tr><td>LoginTime: </td><td>" & Now() & "</td></tr>" & _
                '                         "<tr><td>Plan: </td><td>" & objPlan.planName & "</td></tr>" & _
                '                         "<tr><td>Plan Amount: </td><td>" & objPlan.planAmount & "</td></tr>" & _
                '                         "<tr><td>Duration: </td><td>" & objPlan.planTime & "</td></tr>" & _
                '                         "<tr><td>User Type: </td><td> Room </td></tr>" & _
                '                         "<tr><td>PMS Result: </td><td>" & userContext.item("pmsrcode") & "</td></tr>" & _
                '                         "<tr><td>PMS Message: </td><td>" & userContext.item("pmsrmessage") & "</td></tr>" & _
                '                         "<tr><td>User Requested Url: </td><td>" & userContext.requestedPage & "</td></tr></table>"
                '    Subject = "Night Audit--Login Successfully, Bill Pending: " & Hotelname & " : " & userContext.roomNo
                '    Mtype = MailTypes.Login

                'Case ErrTypes.NaANF
                '    'During Night Audit Login And Added in Nomadix Failure
                '    Html_str = Html_str & "<table  border='1' align='center' >" & _
                '                         "<tr bgcolor=#00CCFF><td colspan='2'>Information Message -- " & Hotelname & "</td></tr>" & _
                '                         "<tr><td colspan='2'>Night Audit--Login Successfully, but bill posting is Pending</td></tr>" & _
                '                         "<tr><td>Username: </td><td>" & Uid & "</td></tr>" & _
                '                         "<tr><td>Password: </td><td>" & Pwd & "</td></tr>" & _
                '                         "<tr><td>Mac Address: </td><td>" & Mac & "</td></tr>" & _
                '                         "<tr><td>Plan: </td><td>" & Planname & "</td></tr>" & _
                '                         "<tr><td>Plan Amount: </td><td>" & PlanAmount & "</td></tr>" & _
                '                         "<tr><td>Duration: </td><td>" & Duration & "</td></tr>" & _
                '                         "<tr><td>User Type: </td><td> " & UsrTyp & " </td></tr>" & _
                '                         "<tr><td>Login Failure: </td><td>" & Now() & "</td></tr>" & _
                '                         "<tr><td>User Requested Url: </td><td>" & Url & "</td></tr></table>"
                '    Subject = "Night Audit--Login, Not Added in Nomadix : " & Hotelname & " : " & Uid
                '    Mtype = MailTypes.Login

            Case ErrTypes.NaBPS
                ''During Night Audit Login And Added in Nomadix Failure
                'Html_str = Html_str & "<table  border='1' align='center' >" & _
                '                     "<tr bgcolor=#00CCFF><td colspan='2'>Information Message -- " & Hotelname & "</td></tr>" & _
                '                     "<tr><td colspan='2'>Night Audit--Bill Posted Successfully</td></tr>" & _
                '                     "<tr><td>Username: </td><td>" & Uid & "</td></tr>" & _
                '                     "<tr><td>Password: </td><td>" & Pwd & "</td></tr>" & _
                '                     "<tr><td>Mac Address: </td><td>" & Mac & "</td></tr>" & _
                '                     "<tr><td>Plan: </td><td>" & Planname & "</td></tr>" & _
                '                     "<tr><td>Plan Amount: </td><td>" & PlanAmount & "</td></tr>" & _
                '                     "<tr><td>Duration: </td><td>" & Duration & "</td></tr>" & _
                '                     "<tr><td>User Type: </td><td> " & UsrTyp & " </td></tr>" & _
                '                     "<tr><td>Login Time: </td><td>" & OtherMsg & "</td></tr>" & _
                '                     "<tr><td>Bill Posted: </td><td>" & Now() & "</td></tr>" & _
                '                     "<tr><td>User Requested Url: </td><td>" & Url & "</td></tr></table>"
                'Subject = "Night Audit -- Bill Posted Successfully : " & Hotelname & " : " & Uid
                'Mtype = MailTypes.Login

                'Case ErrTypes.NaBF
                '    'During Night Audit Login And Added in Nomadix Failure
                '    Html_str = Html_str & "<table  border='1' align='center' >" & _
                '                         "<tr bgcolor=#00CCFF><td colspan='2'>Information Message -- " & Hotelname & "</td></tr>" & _
                '                         "<tr><td colspan='2'>Night Audit--Bill Posting Unsuccessful, RoomNo / LastName incorrect, Deleted the Following MAC address from Nomadix</td></tr>" & _
                '                         "<tr><td>Username: </td><td>" & Uid & "</td></tr>" & _
                '                         "<tr><td>Password: </td><td>" & Pwd & "</td></tr>" & _
                '                         "<tr><td>Mac Address: </td><td>" & Mac & "</td></tr>" & _
                '                         "<tr><td>Plan: </td><td>" & Planname & "</td></tr>" & _
                '                         "<tr><td>Plan Amount: </td><td>" & PlanAmount & "</td></tr>" & _
                '                         "<tr><td>Duration: </td><td>" & Duration & "</td></tr>" & _
                '                         "<tr><td>User Type: </td><td> " & UsrTyp & " </td></tr>" & _
                '                         "<tr><td>Login Time: </td><td>" & OtherMsg & "</td></tr>" & _
                '                         "<tr><td>Bill Failure: </td><td>" & Now() & "</td></tr>" & _
                '                         "<tr><td>User Requested Url: </td><td>" & Url & "</td></tr></table>"
                '    Subject = "Night Audit -- Bill Posting Unsuccessfull : " & Hotelname & " : " & Uid
                '    Mtype = MailTypes.Login

                'Case ErrTypes.BcLANS
                '    'BC Login And Added in Nomadix Successfully
                '    Html_str = Html_str & "<table  border='1' align='center' >" & _
                '                         "<tr bgcolor=#00CCFF><td colspan='2'>Information Message -- " & Hotelname & "</td></tr>" & _
                '                         "<tr><td colspan='2'>BC Login Successful</td></tr>" & _
                '                         "<tr><td>Username: </td><td>" & Uid & "</td></tr>" & _
                '                         "<tr><td>Password: </td><td>" & Pwd & "</td></tr>" & _
                '                         "<tr><td>Mac Address: </td><td>" & Mac & "</td></tr>" & _
                '                         "<tr><td>User Type: </td><td> " & UsrTyp & " </td></tr>" & _
                '                         "<tr><td>Login Time: </td><td>" & OtherMsg & "</td></tr>" & _
                '                         "<tr><td>User Requested Url: </td><td>" & Url & "</td></tr></table>"
                '    Subject = "BC Login Successfull : " & Hotelname & " : " & Uid
                '    Mtype = MailTypes.Login

                'Case ErrTypes.BcReLANS
                '    'BC ReLogin And Added in Nomadix Successfully
                '    Html_str = Html_str & "<table  border='1' align='center' >" & _
                '                         "<tr bgcolor=#00CCFF><td colspan='2'>Information Message -- " & Hotelname & "</td></tr>" & _
                '                         "<tr><td colspan='2'>BC Re-Login Successful</td></tr>" & _
                '                         "<tr><td>Username: </td><td>" & Uid & "</td></tr>" & _
                '                         "<tr><td>Password: </td><td>" & Pwd & "</td></tr>" & _
                '                         "<tr><td>Mac Address: </td><td>" & Mac & "</td></tr>" & _
                '                         "<tr><td>User Type: </td><td> " & UsrTyp & " </td></tr>" & _
                '                         "<tr><td>Login Time: </td><td>" & OtherMsg & "</td></tr>" & _
                '                         "<tr><td>User Requested Url: </td><td>" & Url & "</td></tr></table>"
                '    Subject = "BC Re-Login Successfull : " & Hotelname & " : " & Uid
                '    Mtype = MailTypes.Login

                'Case ErrTypes.BcLF
                '    'BC ReLogin And Added in Nomadix Successfully
                '    Html_str = Html_str & "<table  border='1' align='center' >" & _
                '                         "<tr bgcolor=#00CCFF><td colspan='2'>Information Message -- " & Hotelname & "</td></tr>" & _
                '                         "<tr><td colspan='2'>BC Login Failure</td></tr>" & _
                '                         "<tr><td>Username: </td><td>" & Uid & "</td></tr>" & _
                '                         "<tr><td>Password: </td><td>" & Pwd & "</td></tr>" & _
                '                         "<tr><td>Mac Address: </td><td>" & Mac & "</td></tr>" & _
                '                         "<tr><td>User Type: </td><td> " & UsrTyp & " </td></tr>" & _
                '                         "<tr><td>Failure Time: </td><td>" & OtherMsg & "</td></tr>" & _
                '                         "<tr><td>User Requested Url: </td><td>" & Url & "</td></tr></table>"
                '    Subject = "BC Login Failure : " & Hotelname & " : " & Uid
                '    Mtype = MailTypes.Login

                'Case ErrTypes.BcBPS
                '    'BC Bill Posted Successfully
                '    Html_str = Html_str & "<table  border='1' align='center' >" & _
                '                         "<tr bgcolor=#00CCFF><td colspan='2'>Information Message -- " & Hotelname & "</td></tr>" & _
                '                         "<tr><td colspan='2'>BC Bill Posted Successfully</td></tr>" & _
                '                         "<tr><td>Username: </td><td>" & Uid & "</td></tr>" & _
                '                         "<tr><td>Password: </td><td>" & Pwd & "</td></tr>" & _
                '                         "<tr><td>Mac Address: </td><td>" & Mac & "</td></tr>" & _
                '                         "<tr><td>User Type: </td><td> " & UsrTyp & " </td></tr>" & _
                '                         "<tr><td>Bill Posted Time: </td><td>" & OtherMsg & "</td></tr>" & _
                '                         "</table>"
                '    Subject = "BC Bill Posted Login Successfully : " & Hotelname & " : " & Uid
                '    Mtype = MailTypes.Login
        End Select
        Call Sendmail(Mtype, Subject, Html_str, rn)
    End Function

    Private Sub Sendmail(ByVal mailtype As Integer, ByVal Subject As String, ByVal Body As String, ByVal logIdentifier As String)
        Dim FromAddr, ToAddr, CcAddr As String

        Dim Mail As New MailMessage
        Dim objSysConfig As New CSysConfig
        Dim ObjElog As LoggerService

        If UCase(objSysConfig.GetConfig("MailAlert")) = "TRUE" Then

            Select Case (mailtype)
                Case MailTypes.Login
                    FromAddr = objSysConfig.GetConfig("HlpDskID")
                    ToAddr = objSysConfig.GetConfig("SF_ToAdd")
                    CcAddr = objSysConfig.GetConfig("SF_CcAdd")
                Case Else
                    FromAddr = objSysConfig.GetConfig("HlpDskID")
                    ToAddr = objSysConfig.GetConfig("SF_ToAdd")
                    CcAddr = objSysConfig.GetConfig("SF_CcAdd")
            End Select


            Try
                SmtpMail.SmtpServer = objSysConfig.GetConfig("MailServer") 'Smtp Server ip or mail server name
                Mail.To = ToAddr ' To Address
                Mail.From = FromAddr ' From address
                Mail.Cc = CcAddr 'cc Address
                Mail.Subject = Subject ' message subject
                Mail.BodyFormat = MailFormat.Html 'Mail Format
                Mail.Body = "<html><body>" & Body & "</body></html>" 'Mail Body
                SmtpMail.Send(Mail) 'Send a Mail

            Catch ex As Exception
                Dim errInfo As String

                ObjElog = LoggerService.gtInstance
                errInfo = "Error Message : " & ex.Message & vbCrLf & "Error Source : " & ex.Source & "  Sendmail()" & vbCrLf & "Error Description : " & ex.StackTrace & vbCrLf & "Error Date: " & Now & "  version 1.0 " & vbCrLf
                'check the InnerException
                While Not IsNothing(ex.InnerException)
                    errInfo = errInfo & "-----The following InnerException reported: " + ex.InnerException.ToString() & vbCrLf
                    ex = ex.InnerException
                End While
                ObjElog.write2LogFile(logIdentifier, errInfo)

            Finally
                Mail = Nothing
                objSysConfig = Nothing
            End Try
        End If
    End Sub
End Class

