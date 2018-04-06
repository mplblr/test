
<%@ Page Language="vb" MasterPageFile="~/layout2.Master" AutoEventWireup="true" CodeBehind="connect.aspx.vb"
   Title="Hyatt Regency" Inherits="LeelaMulti.connect"%> 

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
 
   
    <div class="content_panel">
       
<h1>Thankyou for using Internet service.</h1>
 
 <asp:Button ID="Button2" runat="server" Text="Click here to Reconnect" CssClass="btn_style" />


    
        </div>
      
    
        
       
        
        
        
   
 
   
   
  
     
    
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ScriptLayoutPlace" runat="server">



</asp:Content>
