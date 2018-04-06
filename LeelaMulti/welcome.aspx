<%@ Page Language="vb" MasterPageFile="~/layout.Master" AutoEventWireup="true" CodeBehind="welcome.aspx.vb"
     Title="Hyatt Regency" Inherits="LeelaMulti.welcome" %> 

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentPlace" runat="server">



<link rel="stylesheet" type="text/css" href="popup/source/jquery.fancybox.css?v=2.1.5"
        media="screen" />


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlace" runat="server">


<%--<p class="popup_img_set">
        <a class="fancybox" href="images/1.jpg" data-fancybox-group="gallery" title="HYATT"
            id="popup">
            <img src="images/1.jpg" alt="" width="1" height="1" /></a> 
            <a class="fancybox" href="images/2.jpg" data-fancybox-group="gallery" title="HYATT"
            id="A1">
            <img src="images/2.jpg" alt="" width="1" height="1" /></a>
              <a class="fancybox" href="images/3.jpg" data-fancybox-group="gallery" title="HYATT"
            id="A2">
            <img src="images/3.jpg" alt="" width="1" height="1" /></a>
            
  <a class="fancybox" href="images/5.jpg" data-fancybox-group="gallery" title="HYATT"
            id="A3">
            <img src="images/5.jpg" alt="" width="1" height="1" /></a>
            
            
           
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



    <h1 id="mm" runat ="server" >
        <%= GetLocalResourceObject("m1") %></h1>
    <h2>
         <%=GetLocalResourceObject("m2")%></h2>
    <h3>
      <%=GetLocalResourceObject("m3")%>.
    </h3>
    <div class="htl_interior">
    <img src="images/banner_bg.jpg" alt=""  />
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptLayoutPlace" runat="server">

<script type="text/javascript" src="popup/source/jquery.fancybox.js?v=2.1.5"></script>

    <script src="js/jquery.countdown.js" type="text/javascript"></script>

    <%--<script src="js/font/cufon-yui.js" type="text/javascript"></script>

    <script src="js/font/Myriad_Pro_italic_700.font.js" type="text/javascript"></script>--%>



    <script type="text/javascript">
    
    
   



    
    
            $(document).ready(function() {


$(".fancybox").fancybox({ autoPlay: true, 'closeBtn' : true, playSpeed  : 6000 }).trigger('click');

           
             
               
              


              
                
                
                
                
             
 function popup_close() {

                $.fancybox.close()

  
               

 
                

            }


            setInterval(popup_close, 24000);

                
            });
    </script>



</asp:Content>
