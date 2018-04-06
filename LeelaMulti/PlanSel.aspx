<%@ Page Language="vb" MasterPageFile="~/layout.Master" AutoEventWireup="true" CodeBehind="plansel.aspx.vb"
    Inherits="LeelaMulti.PlanSel" Title="Hyatt Regency" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentPlace" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlace" runat="server">
    <asp:HiddenField ID="hdAccept" runat="server" Value="0" />
    <h1>
        <%= GetLocalResourceObject("m1") %></h1>
    <h2>
        <%=GetLocalResourceObject("m2")%></h2>
    <h3>
        <%=GetLocalResourceObject("m3")%>&nbsp;&nbsp;&nbsp;
    </h3>
    <div class="price_plan">
        <h1>
            <img src="images/arrow_down.jpg" alt="" /><%=GetLocalResourceObject("m4")%></h1>
        <hr style="margin: .4em 0em;" />
      
        <div class="new_plan_info">
            <div class="plan_lbl">
                <%=GetLocalResourceObject("m1")%>
            </div>
            <div id="zz" runat ="server"  class="prm_srv">
               <%=GetLocalResourceObject("z6")%>.
            </div>
            
            <div id="zzz" runat ="server"  class="prm_srv">
               <%=GetLocalResourceObject("z10")%>
            </div>
            
        </div>
        <div style="clear:both;"></div>
        <asp:RadioButtonList AutoPostBack="true" CssClass="tbl_radio" runat="server" CellSpacing="0" CellPadding="0" ID="Rdoplan1">
            
        </asp:RadioButtonList>
       
        <div class="plan_qty" runat="server" id="qty">
           <span>  <%=GetLocalResourceObject("h1")%>:</span>  &nbsp; &nbsp;     <%=GetLocalResourceObject("h2")%> &nbsp; &nbsp;
           
           
            <span> <%=GetLocalResourceObject("h3")%>:</span><asp:TextBox ID="txt1" enabled="false" runat="server" ></asp:TextBox>
        </div>
        
        <div class="plan_qty" runat="server" id="qty1">
           <span>  <%=GetLocalResourceObject("z7")%>:</span> <asp:DropDownList  AutoPostBack="true"  ID="DrpPlans1" runat="server">
           
            </asp:DropDownList>
            <span><%=GetLocalResourceObject("h3")%>:</span><asp:TextBox ID="txt2" enabled="false" runat="server" ></asp:TextBox>
        </div>
        
        <div id ="taxd" visible="false" runat ="server" class="tax_details">
           <h1> <%=GetLocalResourceObject("z5")%>.</h1>
 <p>
                 <%=GetLocalResourceObject("z9")%>.
            </p>           

 <p>
                <%=GetLocalResourceObject("z8")%>.
            </p>

<p>   </p>
           
        </div>
        
        <div class="terms">
            <%=GetLocalResourceObject("m10")%>
            <asp:LinkButton ID="LinkButton1" runat="server">  <%=GetLocalResourceObject("m11")%></asp:LinkButton>
            <%=GetLocalResourceObject("z1")%>
            <div style="position: relative; left: -2px;">
                <asp:Button ID="Button2" runat="server" Text="" CssClass="btn_style" />
            </div>
            <div id="nt" runat="server">
                <%=GetLocalResourceObject("f1")%>
                <a href="http://leelainternet.in" target="_blank" class="plan_option">best available
                    options.</a>
            </div>
        </div>
        
        <asp:Label ID="Label1" runat="server" Text="" CssClass="lbl_error"></asp:Label>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ScriptLayoutPlace" runat="server">

    <script type="text/javascript">
        $(function() {

            $("input[id$=Button2]").click(function(e) {
                var clickonce = $.trim($("input[id$=hdAccept]").val());
                if (clickonce === "1" || clickonce === 1) {
                    e.preventDefault();
                }
                else {
                    $("input[id$=hdAccept]").val("1");
                    $("input[id$=Button2]").attr("class", "btn_style_disable");
                }
            });



            $('.tbl_radio tr:even').css("clear", "both");
            $('.tbl_radio tr:odd').css("margin-left", "2em");

            $('.tbl_radio tr:eq(1)').attr("id", "basic1");
            $('.tbl_radio tr:eq(1)').addClass('radiobtn1');
            $('.tbl_radio tr:eq(1)').attr("runat", "server");




        });
    </script>

</asp:Content>
