
<%@ Page Language="vb" MasterPageFile="~/layout2.Master" AutoEventWireup="true" CodeBehind="PostLogin.aspx.vb"
   Title="Hyatt Regency" Inherits="LeelaMulti.PostLogin"%> 

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentPlace" runat="server">
    <style type="text/css">
        #right_panel
        {
            width: 98%;
            margin-left: 2%;
        }
    </style>

<link rel="stylesheet" type="text/css" href="popup/source/jquery.fancybox.css?v=2.1.5"
        media="screen" />


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="timersection" runat="server">
    <input id="hdoffset" type="hidden" name="hdoffset" runat="server" />

     
    <asp:HiddenField ID="hdRemainingTime" runat="server" />
    <asp:HiddenField ID="myurl" runat="server" />
    <div class="remaing_time" style="margin-left: 20%;">
        <h2>
            <%=GetLocalResourceObject("m11")%>
        </h2>
        <div id="TimerDiv" class="" runat="server" style="margin: auto;">
            <h3 id="lblTimer" style="font-size: 19px; letter-spacing: 2px; color: #fff; font-style: normal;">
            </h3>
        </div>
    </div>
    <div class="remaing_time">
        <h2 style="font-size: 11px;">
            <%=GetLocalResourceObject("m21")%>
        </h2>
        <p style="font-size: 11px;">
            <asp:Label ID="exp" Text="sss" runat="server"></asp:Label>
        </p>
    </div>
    
  
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlace" runat="server">

 <%--<p class="popup_img_set">
        <a class="fancybox" href="images/1_b.jpg" data-fancybox-group="gallery" title="Hyatt Regency"
            id="popup">
            <img src="images/1_s.jpg" alt="" width="1" height="1" /></a> 
            <a class="fancybox" href="images/1_b.jpg" data-fancybox-group="gallery" title="Hyatt Regency"
            id="A1">
            <img src="images/1_s.jpg" alt="" width="1" height="1" /></a>
            
            
           
    </p>--%>


    <asp:HiddenField ID="hdRequestLink" runat="server" Value="" />
    <asp:HiddenField ID="hdLoginHappened" runat="server" Value="0" />
    <div class="ScriptSection">

        <script src="js/jquery-1.4.2.min.js" type="text/javascript"></script>

        <script type="text/javascript">
            $(function() {
                var hdRequestLink = $("input[id$=hdRequestLink]");

                var checkRequestLink = function() {

                    var requestLink = $.trim(hdRequestLink.val());
                    if (requestLink !== "") {
                        window.location = requestLink;
                    }
                };
                checkRequestLink();
                setInterval(checkRequestLink, 30000);

            });
        </script>

    </div>
    <div class="content_panel">
        <h1>
            <%=GetLocalResourceObject("m1")%>
        </h1>
        <div class="panel1">
           

 <h2>
                <asp:Label ID="username" runat="server" Style="color: #000;"></asp:Label>
                <span>
                    <img src="images/arrow_left.jpg" alt="" style="margin-right: 2px;" runat="server"
                        id="img_arrow" class="img_arrow2" />
                    <%=GetLocalResourceObject("m2")%>.</span>
            </h2>

<br/>
<div> <p>   Please type <a href="http://myusageinfo.in"> <b> myusageinfo.in</b>  </a>in a browser to return to this page.</p>
   </div>


            <div class="login_details">
                <h3 id="x1" runat ="server" >
                    <%=GetLocalResourceObject("m3")%></h3>
                <div>
                    <p id="x2" runat ="server" >
                        <%=GetLocalResourceObject("m4")%></p>
                    <div id="hari" runat ="server">
                        <div class="display_items">
                            <p style="margin-top: 15px;">
                                <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                                <asp:Label ID="roomno" runat="server" Text="" CssClass="lbl_fields"></asp:Label>
                            </p>
                            <p>
                                <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                                <asp:Label ID="lastname" runat="server" Text="" CssClass="lbl_fields"></asp:Label>
                            </p>
                            
                        </div>
                    </div>
                    <div class="spacer">
                    </div>
                   
                   
                   
                    <div id="j8" runat ="server"  style="margin-top: 40px;">
                        
                    </div>
                   
                 
                    
                   
                    
                    
                </div>
                <div>
                    <asp:Label ID="lblerr" runat="server" Text="" CssClass="lbl_error" Style="width: 100%;"></asp:Label>
                     <asp:Label ID="Label4" runat="server" Text="" CssClass="lbl_green" Style="width: 100%;"></asp:Label>
                    
                </div>
            </div>
        </div>
        <div class="divider">
            <img src="images/line.jpg" alt="" />
        </div>
        <div class="panel2"  >
            <h3>
                <%=GetLocalResourceObject("m7")%></h3>
            <p>
                <span id="price_list">
                    <%=GetLocalResourceObject("m8")%></span> <span>
                        <asp:Label ID="plandetails" runat="server" Text=""></asp:Label>
                    </span>
                     
            </p>
          <h3 id="upgra" runat="server">
            <%=GetLocalResourceObject("s4")%>
          </h3>
            <div id="usage_details" runat="server" class="usage_details">            
                <%=GetLocalResourceObject("s5")%> <a href="http://upgrademyplan.in"> <b> upgrademyplan.in</b>  </a>  <%=GetLocalResourceObject("s6")%>.
            </div>
           
        <div>
          <asp:RadioButtonList  autopastback="true" CssClass="tbl_radio" runat="server" CellSpacing="0" CellPadding="0" ID="rdoplan1">
    
      
            
        </asp:RadioButtonList>
        
            </div>
            <div class="plan_qty"  id="upgra1" runat ="server"  >
            <span> <%=GetLocalResourceObject("s7")%></span><asp:DropDownList AutoPostBack="true" ID="DrpPlans1" runat="server">
            
            </asp:DropDownList>
            
        </div>
        <div class="ui_terms" id="term" runat ="server" > 
              <p>   <%=GetLocalResourceObject("s8")%>  <asp:LinkButton  ID="LinkButton1"  runat="server"> <%=GetLocalResourceObject("s9")%></asp:LinkButton> </p>
              <asp:Button ID="Button3" runat="server" Text="Confirm" CssClass="btn_style" />
              
                </div>
                <p>   <asp:Label ID="lblerr1" runat="server" Text="" CssClass="lbl_error" Style="width: 100%;"></asp:Label></p>
        </div>
        
        
       
        
        
        
    </div>
 
    <div class="banner_right">
        <img src="images/banner_bg2.jpg" alt="" />
    </div>
    <div class="spacer">
    </div>
    <div style="height: 50px; margin-top: 1em;" id="new_win_btn">
        <asp:Button ID="Button2" runat="server" Text="" CssClass="btn_style" /> &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="Button4" runat="server" Text="Stop Internet" CssClass="btn_style" />
    </div>
     
    <asp:Label ID="Label3" runat="server" Text="" CssClass="lbl_error"></asp:Label>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ScriptLayoutPlace" runat="server">

<script type="text/javascript" src="popup/source/jquery.fancybox.js?v=2.1.5"></script>

    <script src="js/jquery.countdown.js" type="text/javascript"></script>

    <%--<script src="js/font/cufon-yui.js" type="text/javascript"></script>

    <script src="js/font/Myriad_Pro_italic_700.font.js" type="text/javascript"></script>--%>



    <script type="text/javascript">
    
    
   



    
    
            $(document).ready(function() {


$(".fancybox").fancybox({ autoPlay: false, 'closeBtn' : true, playSpeed  : 20000 }).trigger('click');


                var hdRemainingTime = $("input[id$=hdRemainingTime]");
                 var hdurl = $("input[id$=myurl]");



                
                var MessageDiv = $("div[id$=MessageDiv]");

                var Maximize = function() {
                    window.innerWidth = 300;
                    window.innerHeight = 300;
                    window.screenX = 0;
                    window.screenY = 0;
                    alwaysLowered = false;
                }

                var tick = function(period) {

                  //  Cufon.replace('#lblTimer', { fontFamily: 'Myriad Pro' });

                    var hh = parseInt(period[4], 10);
                    var mm = parseInt(period[5], 10);
                    var ss = parseInt(period[6], 10);

                    var secondsleft = (hh * 60 * 60) + (mm * 60) + ss;

                    if (secondsleft === 300) {
                        MessageDiv.html("Your plan will expire soon...");
                        Maximize();

                    }

                    //if (hh === 0 && mm === 5 && ss === 0) {

                    //}
                };

                var expire = function() {
                var output="The package which you had selected has expired";
                var url = $.trim(hdurl.val());
                    window.location.href = 'UserError.aspx?'+ url +'&Msg='+output;
                };




                var remainingTime = $.trim(hdRemainingTime.val());
                if (remainingTime == "0") {
                    expire();
                }
                else {
                    $("#lblTimer").countdown({
                        until: remainingTime,
                        timeSeparator: ':',
                        tickInterval: 1,
                        compact: true,
                        format: 'HMS',
                        description: '',
                        onTick: tick,
                        onExpiry: expire
                    });
                }
                
                
                
                $("input[id$=Button2]").click(function(e) {
                    var popupurl = "https://mumbai.regency.hyatt.com/en/hotel/home.html";
                    
                    var newwindow_width = 1000;
                    var newwindow_height = 1000;
                    var top = 0;
                    var left = 0;
        
                    window.open(popupurl, 'newwindow', 'width=' + newwindow_width + ',height=' + newwindow_height + ',top=' + top + ',left=' + left + ',resizable=1,menubar=1,location=1,status=1,scrollbars=1,toolbar=1');                    
                    e.preventDefault();
                });
                
                
            });
    </script>

</asp:Content>
