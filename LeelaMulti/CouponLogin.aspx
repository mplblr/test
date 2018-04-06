<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteBase.Master"
    CodeBehind="CouponLogin.aspx.vb" Inherits="MobileApp.CouponLogin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript" src="js/jquery-1.3.2.min.js"></script>

    <script type="text/javascript" src="js/supersleight.plugin.js"></script>

    <script type="text/javascript" src="js/validation.js"></script>

    <style type="text/css">
        .hover_nav2
        {
            background: #dca034 url(images/bg_hover.gif) repeat-x scroll left top;
            padding-top: .6em;
            font-weight: bold;
            padding-bottom: .9em;
            padding-left: .5em;
            padding-right: .5em;
        }
    </style>
</asp:Content>
<asp:Content ID="PageContent" ContentPlaceHolderID="PageContentPlace" runat="server">
    <div id="contents">
        <div style="min-height: 3em; margin-bottom: 1em; text-align: center;">
            <asp:Label ID="lblerr" runat="server" Text="" CssClass="lbl_error"></asp:Label>
        </div>
        <div class="login_details">
            <asp:Label runat="server" ID="lbl1" Text="User ID"></asp:Label>
            <asp:TextBox ID="txtUserID" CssClass="txt_box" runat="server"></asp:TextBox>
        </div>
        <div class="login_details">
            <asp:Label ID="lbl2" runat="server" Text="Password"></asp:Label>
            <asp:TextBox CssClass="txt_box" runat="server" TextMode="Password" ID="txtPassword"></asp:TextBox>
        </div>
        <div class="btn_panel">
        </div>
        <asp:Button ID="btnContinue" Text="Login >>&nbsp;  I Agree to Terms & Conditions"
            runat="server" class="btn_style"></asp:Button>
        <div class="terms_link">
            <img src="images/arrow_left.gif" alt="" /><a href="Terms.aspx" target="terms">Terms
                & Conditions</a>
        </div>
    </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContentPlace" runat="server">

    <script type="text/javascript">
        $(function() {
        
            $("input[id$=imgbtnlogin]").click(function(e) {
                if($("input[id$=hdDisableClick]").val() === "1") {
                    e.preventDefault();            
                }
                else {
                    $("input[id$=hdDisableClick]").val("1");
                }
                
            });
        

    <style type="text/css">
        .style1
        {
            height: 29px;
        }
    </style>
    
     function validate(frm) {

            if (parseInt(frm.hdbutclick.value) != 0) {
                return false;
            }

            else if (isEmpty(frm.txtRoomNo, "Please enter User Id.")) {
                return false;
            }
            else if (isEmpty(frm.txtlastname, "Please enter your Password.")) {
                return false;
            }

            frm.hdbutclick.value = 1;
        }

       

    </script>

</asp:Content>
