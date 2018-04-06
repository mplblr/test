

<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/layout.Master" CodeBehind="terms.aspx.vb"
    Inherits="LeelaMulti.terms" Title="Hyatt Regency" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentPlace" runat="server">

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlace" runat="server">
    


<h1>
       <%= GetLocalResourceObject("m1") %></h1>
    <h2>
        <%=GetLocalResourceObject("m2")%></h2>
    <div style="margin-top:2em;">
        <h4>
            <img src="images/arrow_left.jpg" alt="" />
           <%=GetLocalResourceObject("m3")%>
        </h4>
    </div>
    <div id="support_wrapper1">
        <p>
            <%=GetLocalResourceObject("m4")%>
        </p>
        <p>
            <%=GetLocalResourceObject("m5")%>
        </p>
        <p>
            <%=GetLocalResourceObject("m6")%>
        </p>
        
          <p>
            <%=GetLocalResourceObject("m7")%>
        </p>
        
          <p>
            <%=GetLocalResourceObject("m8")%>
        </p>
        <p>
            &nbsp;
        </p>
    </div>

<asp:Button ID="btnActivateCode" runat="server" class="btn_style" Text="Back" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ScriptLayoutPlace" runat="server">
</asp:Content>
