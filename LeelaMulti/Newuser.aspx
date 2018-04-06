<%@ Page Language="vb" MasterPageFile="~/layout.Master" AutoEventWireup="true" CodeBehind="Newuser.aspx.cs"
    Title="Hyatt regency" Inherits="LeelaMulti.Newuser" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentPlace" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlace" runat="server">
    <h1 id="c1" runat ="server" >
        <%= GetLocalResourceObject("m1") %></h1>
    <h2 id="c2" runat ="server">
        <%=GetLocalResourceObject("m2")%></h2>
    <h3 id="c3" runat ="server">
        <%=GetLocalResourceObject("m3")%>.
    </h3>
    <hr />
    <h1>
        <%=GetLocalResourceObject("m4")%>
    </h1>
    <h2 style="color: #353535;">
        <%=GetLocalResourceObject("m5")%>
    </h2>
    <h3 id="a10" runat ="server" >
        <%=GetLocalResourceObject("m6")%>.
    </h3>
    
     <h3 id="a11" runat ="server" >
        <%=GetLocalResourceObject("p9")%>
    </h3>
    
    
    <div id="k1" runat ="server"  class="technology">
        <div style="padding-left: 15px; margin-top: -15px;">
            <strong>
                <%=GetLocalResourceObject("m7")%></strong></div>
    </div>
    <div id="s1" runat ="server"  class="thelanguage">
        <div class="faq_inner_contents" style="font-weight: normal;">
            <div class="login_details">
                <p>
                    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                    <asp:TextBox ID="txtRoomNo" runat="server" class="txt_box"></asp:TextBox>
                </p>
                <p>
                    <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                    <asp:TextBox ID="txtlastname" runat="server" CssClass="txt_box"></asp:TextBox>
                </p>
                  <p>
                    <asp:Label ID="Label8" runat="server" Text=""></asp:Label>
                   <%=GetLocalResourceObject("p5")%> <asp:LinkButton  ID="LinkButton3"  runat="server"><%=GetLocalResourceObject("p6")%></asp:LinkButton>
                </p>
                
                <p style="margin-left: 10.2em;">
                    <asp:Button ID="Button1" runat="server" Text="" CssClass="btn_style" />
                </p>
                <asp:Label ID="Label3" runat="server" Text="" CssClass="lbl_error"></asp:Label>
            </div>
        </div>
    </div>
    <div id="s2" runat ="server"  class="technology">
        <div style="padding-left: 15px; margin-top: -15px;">
            <strong><%=GetLocalResourceObject("h5")%> </strong>
        </div>
    </div>
    <div id="s3" runat ="server"  class="thelanguage">
        <div class="faq_inner_contents" style="font-weight: normal;">
            <div class="conference_details">
               <%=GetLocalResourceObject("p7")%>.
                <p>
                    <span><%=GetLocalResourceObject("p8")%>:</span>
                    <asp:TextBox ID="TextBox1" runat="server" class="txt_box"></asp:TextBox>
                </p>
                
                 <p>
                    <asp:Label ID="Label7" runat="server" Text=""></asp:Label>
                   <%=GetLocalResourceObject("p5")%> <asp:LinkButton  ID="LinkButton2"  runat="server"><%=GetLocalResourceObject("p6")%></asp:LinkButton>
                </p>
                
                <p style="margin-left: 10.2em;">
                    <asp:Button ID="Button3" runat="server" Text="" CssClass="btn_style" />
                </p>
                
            </div>
        </div>
    </div>
    <div id="s5" runat ="server"  class="technology">
        <div style="padding-left: 15px; margin-top: -15px;">
            <strong>
                <%=GetLocalResourceObject("m11")%></strong></div>
    </div>
    <div id="s4" runat ="server"  class="thelanguage">
        <div class="faq_inner_contents" style="font-weight: normal;">
           
            <p class="prepaid_access">
             <span class="lbl_hdr2">
                <%=GetLocalResourceObject("p3")%></span>
               <%=GetLocalResourceObject("p4")%>
            </p>
            <div class="login_details">
                <p>
                  <strong>  <asp:Label ID="Label4" class="lbl_hdr3" runat="server" Text=""></asp:Label>  </strong>
                    <asp:TextBox ID="userid" runat="server" class="txt_box"></asp:TextBox>
                </p>
               
                <p>
                    <asp:Label ID="Label7_1" runat="server" Text=""></asp:Label>
                   <%=GetLocalResourceObject("p5")%> <asp:LinkButton  ID="LinkButton1"  runat="server"><%=GetLocalResourceObject("p6")%></asp:LinkButton>
                </p>
                <p style="margin-left: 10.2em;">
                    <asp:Button ID="Button2" runat="server" Text="" CssClass="btn_style" />
                </p>
                <asp:Label ID="Label6" runat="server" Text="" CssClass="lbl_error"></asp:Label>
            </div>
        </div>
    </div>
    
      <div id="Div1" runat ="server"  class="technology">
        <div style="padding-left: 15px; margin-top: -15px;">
            <strong>
              <%=GetLocalResourceObject("m11")%></strong></div>
    </div>
    <div id="Div2" runat ="server"  class="thelanguage">
        <div class="faq_inner_contents" style="font-weight: normal;">
           
            <p class="prepaid_access">
             
              <span class="lbl_hdr2">      <%=GetLocalResourceObject("p3")%>  </span> 
             <span>
                <%=GetLocalResourceObject("h1")%></span>
                <%=GetLocalResourceObject("h2")%>
            </p>
            <div class="login_details">
                <p>
                  <strong>  <asp:Label ID="Label50" class="lbl_hdr3" runat="server" Text="Phone Number"></asp:Label>  </strong>
                    <asp:TextBox ID="TextBox2" runat="server" class="txt_box"></asp:TextBox>
                    <asp:Button ID="Button6" runat="server" Text="Get Access Code" CssClass="btn_style" />
                </p>
                
                 <p id="p1" runat ="server">
                 <%=GetLocalResourceObject("h3")%>
                </p>
                <p id="p2" runat ="server">
                   <asp:Label ID="Label5" class="lbl_hdr3" runat="server" Text="Access Code"></asp:Label>  
                    <asp:TextBox ID="TextBox4" runat="server" class="txt_box"></asp:TextBox>
                    
                </p>
                <p id="p3" runat ="server">
                  <%=GetLocalResourceObject("h4")%>
                </p>
                  <p id="p4" runat ="server">
                 <asp:Label ID="Label14" runat="server" class="lbl_hdr3" Text="Email"></asp:Label> 
                    <asp:TextBox ID="TextBox5" runat="server" class="txt_box"></asp:TextBox>
                    
                </p>
               
                <p id="p5" runat ="server">
                    <asp:Label ID="Label9" runat="server" Text=""></asp:Label>
                   <%=GetLocalResourceObject("p5")%> <asp:LinkButton  ID="LinkButton4"  runat="server"><%=GetLocalResourceObject("p6")%></asp:LinkButton>
                </p>
                <p style="margin-left: 10.2em;" id="p6" runat ="server" >
                    <asp:Button ID="Button4" runat="server" Text="" CssClass="btn_style" />
                </p>
                <asp:Label ID="Label10" runat="server" Text="" CssClass="lbl_error"></asp:Label>
            </div>
        </div>
    </div>
    
     <div id="Div3" runat ="server"  class="technology">
        <div style="padding-left: 15px; margin-top: -15px;">
            <strong>
               Login by Access Code-NON OTP</strong> </div>
    </div>
    <div id="Div4" runat ="server"  class="thelanguage">
        <div class="faq_inner_contents" style="font-weight: normal;">
           
            <p class="prepaid_access">
            
         <span class="lbl_hdr2">      <%=GetLocalResourceObject("p3")%>  </span> 
               <%=GetLocalResourceObject("p4")%>
            </p>
            <div class="login_details">
                <p>
                  <strong>  <asp:Label ID="Label11" class="lbl_hdr3" runat="server" Text="Access Code"></asp:Label>  </strong>
                    <asp:TextBox ID="TextBox3" runat="server" class="txt_box"></asp:TextBox>
                </p>
              <p>  To receive special offer and promotions,provide us with your email </p>
               
                <p>
                  <strong>  <asp:Label ID="Label15" class="lbl_hdr3" runat="server" Text="Email:"></asp:Label>  </strong>
                    <asp:TextBox ID="TextBox6" runat="server" class="txt_box"></asp:TextBox>
                </p>
                <p>
                    <asp:Label ID="Label12" runat="server" Text=""></asp:Label>
                   <%=GetLocalResourceObject("p5")%> <asp:LinkButton  ID="LinkButton5"  runat="server"><%=GetLocalResourceObject("p6")%></asp:LinkButton>
                </p>
                <p style="margin-left: 10.2em;">
                    <asp:Button ID="Button5" runat="server" Text="" CssClass="btn_style" />
                </p>
                <asp:Label ID="Label13" runat="server" Text="" CssClass="lbl_error"></asp:Label>
            </div>
        </div>
    </div>
    
    <hr />
   
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptLayoutPlace" runat="server">

    <script type="text/javascript" src="js/ddaccordion.js"></script>

    <script type="text/javascript">
        ddaccordion.init({
            headerclass: "technology", //Shared CSS class name of headers group
            contentclass: "thelanguage", //Shared CSS class name of contents group
            revealtype: "click", //Reveal content when user clicks or onmouseover the header? Valid value: "click", "clickgo", or "mouseover"
            mouseoverdelay: 200, //if revealtype="mouseover", set delay in milliseconds before header expands onMouseover
            collapseprev: true, //Collapse previous content (so only one open at any time)? true/false 
            defaultexpanded: [0],
              //index of content(s) open by default [index1, index2, etc]. [] denotes no content.
            onemustopen: false, //Specify whether at least one header should be open always (so never all headers closed)
            animatedefault: false, //Should contents open by default be animated into view?
            persiststate: true, //persist state of opened contents within browser session?
            toggleclass: ["closedlanguage", "openlanguage"], //Two CSS classes to be applied to the header when it's collapsed and expanded, respectively ["class1", "class2"]
            togglehtml: ["prefix", "<img src='images/plus.gif' style='width:11px; height:11px' /> ", "<img src='images/minus.gif' style='width:11px; height:11px' /> "], //Additional HTML added to the header when it's collapsed and expanded, respectively  ["position", "html1", "html2"] (see docs)
            animatespeed: "fast", //speed of animation: integer in milliseconds (ie: 200), or keywords "fast", "normal", or "slow"
            oninit: function(expandedindices) { //custom code to run when headers have initalized
                //do nothing
            },
            onopenclose: function(header, index, state, isuseractivated) { //custom code to run whenever a header is opened or closed
                //do nothing
            }
        });

        $(document).ready(function() {
            var txtRoomNo = $("input[id$=txtRoomNo]");
            txtRoomNo.focus();
        });

    </script>

</asp:Content>
