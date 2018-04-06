

<%@ Page Language="vb" MasterPageFile="~/layout.Master" AutoEventWireup="true" CodeBehind="ChangePwd.aspx.vb"
    Inherits="LeelaMulti.ChangePwd" Title="Hyatt Regency" %>

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
        
        <h3><%=GetLocalResourceObject("h1")%></h3>

<h3> <%=GetLocalResourceObject("h2")%></h3>
        
         <asp:Label ID="Label6" runat="server" Text="" CssClass="lbl_error"></asp:Label>
        <div class="login_details">
           
            
            
            <p>
                <asp:Label ID="Label4" runat="server" Text=""></asp:Label>
                <asp:TextBox ID="TextBox1" runat="server" class="txt_box"></asp:TextBox>
            </p>
            <p>
                <asp:Label ID="Label5" runat="server" Text=""></asp:Label>
                <asp:TextBox ID="TextBox2" TextMode="Password"    runat="server" CssClass="txt_box"></asp:TextBox>
           
           
            </p>
            
            <p>
                <asp:Label ID="Label1" runat="server" Text="Secret code"></asp:Label>
                <asp:TextBox ID="TextBox3"     runat="server" CssClass="txt_box"></asp:TextBox>
           
           
            </p>
            
           <%-- <p style="margin-left: 10.2em;">
                 <%= GetLocalResourceObject("m9") %> <asp:LinkButton ID="lk" runat="server">  <%=GetLocalResourceObject("m10")%></asp:LinkButton>
            </p>--%>
            <p style="margin-left: 10.2em;">
                <asp:Button ID="Button1" runat="server" Text="" CssClass="btn_style" />
                 <asp:Button visible="false" ID="Button2" runat="server" Text="" CssClass="btn_style" />
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
