


<%@ Page Language="vb" MasterPageFile="~/layout2.Master" AutoEventWireup="true" CodeBehind="SndRevBytes.aspx.vb"
   Title="Hyatt Regency" Inherits="LeelaMulti.SndRevBytes"%> 


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentPlace" runat="server">
    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="timersection" runat="server">

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlace" runat="server">

<h1>Data Bytes usage Details:</h1>  

<div><asp:GridView ID="gvwExample" Runat="server"
             AutoGenerateColumns="False" 
             
             CellPadding="30" CellSpacing="20"  
              cssClass="grid_veiw">

            <FooterStyle  
              BackColor="#F7DFB5"></FooterStyle>

            <PagerStyle   ForeColor="#8C4510" 
              HorizontalAlign="left"></PagerStyle>

            <HeaderStyle ForeColor="White" Font-Bold="True" 
               ></HeaderStyle>

            <Columns>
                
                <asp:BoundField HeaderText="Plan Name" 
                 DataField="PlanName" 
                 SortExpression="PlanName"></asp:BoundField>

 <asp:BoundField HeaderText="Mac Address" 
                 DataField="macid" 
                 SortExpression="macid"></asp:BoundField>
                
      <asp:BoundField HeaderText="Total Uploaded" 
                 DataField="Uploadtx" 
                 SortExpression="Uploadtx"></asp:BoundField>

 <asp:BoundField HeaderText="Total Downloaded" 
                 DataField="downloadrx" 
                 SortExpression="downloadrx"></asp:BoundField>
                
      <asp:BoundField HeaderText="Total Transfered" 
                 DataField="total" 
                 SortExpression="total"></asp:BoundField>


               



            </Columns>
            <SelectedRowStyle ForeColor="White" Font-Bold="True" 
              ></SelectedRowStyle>
            <RowStyle ForeColor="#8C4510" 
                ></RowStyle>
        </asp:GridView>   </div>
 





<asp:Button ID="btnActivateCode" runat="server" class="btn_style" Text="Back" />


</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptLayoutPlace" runat="server">

<script type="text/javascript">

        $(function() {

             

            $('#right_panel').css("width", "90%");

$('.grid_veiw tr:odd').addClass("grid_bg1");

$('.grid_veiw tr:even').addClass("grid_bg2");

        });
    </script>

</asp:Content>