

<%@ Page Language="vb" MasterPageFile="~/layout.Master" AutoEventWireup="true" CodeBehind="UserErrorNew.aspx.vb"
    Inherits="LeelaMulti.UserErrorNew" Title="Hyatt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentPlace" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavBar" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlace" runat="server">
      <asp:HiddenField ID="hdAccept" runat="server" Value="0" />
    <hr />
    
    
    <div id="user_error2">
        <div style="text-align: center;">
            <img src="images/alert.png" alt="" style="vertical-align: middle;" />
        </div>
        <div id="msgPara" runat="server" style="margin-top: .7em; margin-bottom: .7em; text-align: left;    font: 14px 'PTSansBold', Verdana;">
           
        </div>
        
        <div>
            <asp:Button ID="logut_btn1" CssClass="btn_style" Text="" runat="server">
            </asp:Button>
            &nbsp;
            <asp:Button  Visible ="false" ID="logut_btn" CssClass="btn_style" Text="" runat="server">
            </asp:Button>
        </div>
    </div>
    <hr />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ScriptLayoutPlace" runat="server">
<script type="text/javascript">
        $(function() {

            $("input[id$=logut_btn1]").click(function(e) {
                var clickonce = $.trim($("input[id$=hdAccept]").val());
                if (clickonce === "1" || clickonce === 1) {
                    e.preventDefault();
                }
                else {
                    $("input[id$=hdAccept]").val("1");
                    $("input[id$=logut_btn1]").attr("class", "btn_style_disable");
                }
            });


  });


       
    </script>


</asp:Content>

