
<%@ Page Language="vb" MasterPageFile="~/layout.Master" AutoEventWireup="true" CodeBehind="pre_exist.aspx.vb"
    Inherits="LeelaMulti.pre_exist" Title="Hyatt Regency" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentPlace" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlace" runat="server">
    <h1>
        <%= GetLocalResourceObject("m1") %></h1>
    <h2>
        <%=GetLocalResourceObject("m2")%></h2>
    <h3>
        <%=GetLocalResourceObject("m3")%>.</h3>
    <hr />
    <div id="sub_Section">
        <h1>
            <%=GetLocalResourceObject("m4")%>
        </h1>
        <h2>
             <%=GetLocalResourceObject("m5")%>
        </h2>
        <h3>
           <%=GetLocalResourceObject("m6")%>
        </h3>
        <div class="login_details">
            <p>
                <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                <asp:TextBox ID="txtRoomNo" runat="server" class="txt_box"></asp:TextBox>
            </p>
            <p>
                <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                <asp:TextBox ID="txtlastname" TextMode ="Password" runat="server" CssClass="txt_box"></asp:TextBox>
            </p>
            <p style="margin-left: 10.2em;">
                 <%= GetLocalResourceObject("m9") %> <asp:LinkButton ID="lk" runat="server">  <%=GetLocalResourceObject("m10")%></asp:LinkButton>
           <%=GetLocalResourceObject("z1")%>
            </p>
            <p style="margin-left: 10.2em;">
                <asp:Button ID="Button1" runat="server" Text="" CssClass="btn_style" />
            </p>
            
             <p style="margin-left: 10.2em;">
               <%=GetLocalResourceObject("h1")%> <a target="_blank" href="http://password.in">       <b>password.in</b>  </a>
          <%=GetLocalResourceObject("h2")%>
            </p>
            
            <asp:Label ID="Label3" runat="server" Text="" CssClass="lbl_error"></asp:Label>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ScriptLayoutPlace" runat="server">
  <script type="text/javascript" src="js/ddaccordion.js"></script>

    <script type="text/javascript">
  $(document).ready(function() {
                var txtRoomNo = $("input[id$=txtRoomNo]");
                txtRoomNo.focus();
            });

 </script>

</asp:Content>
