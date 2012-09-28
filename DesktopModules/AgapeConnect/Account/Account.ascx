﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Account.ascx.cs" Inherits="DotNetNuke.Modules.Account.AccountReport" %>
<link href="/Portals/_default/Skins/AgapeBlue/flick/jquery-ui-1.8.18.custom.css"
    rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>
<script src="/Scripts/linq.js" type="text/javascript"></script>

<script language="javascript" type="text/javascript" >
    google.load("visualization", "1", { packages: ["corechart"] });
     google.setOnLoadCallback(function() { drawVisualization() });
    $(function () {
        $("#accordion").accordion({
            autoHeight: false,
            navigation: true,
            collapsible: true,
            active: false,
             change: function(event, ui) {
        var newIndex = $(ui.newHeader).index('h3');
        if(newIndex==2)
        {
        var oldIndex = $(ui.oldHeader).index('h3');
            $(this).accordion( "activate" , oldIndex );
        }
      }
        });
       
    
   

        $("#divTransDetail").dialog({
                autoOpen: false,
                height: 300,
                width: 500,
                modal: true,
                title: "Transaction Detail",
                close: function () {
                  //  allFields.val("").removeClass("ui-state-error");
                }
            });
        $("#divTransDetail").parent().appendTo($("form:first"));

         $(".heading").unbind('click');
             $(".content").hide();
             $(".heading").click(function () {
                 $(this).next(".content").slideToggle(500, function(){
				        if ($(this).is(':hidden')) {
				            $("#<%= lblChange.ClientID  %>").text("(Click here to show Donations...)");
				        } else {
				            $("#<%= lblChange.ClientID %>").text("(Click here to hide Donations...)");
				        }
				
				        return false;
			       }  );

               
            });
    });

     function drawVisualization() {
      
                // create and populate the data table.
                var data = new google.visualization.DataTable();
                data.addColumn('string', 'period');
                data.addColumn('number', 'Income');
                data.addColumn('number', 'Average Income');
                data.addColumn({type:'string',role:'annotation'});
                data.addColumn({type:'boolean',role:'certainty'});
                data.addColumn('number', 'Expenses');
                data.addColumn('number', 'Balance');
               
                <%= getGoogleData() %>
   
                // create and draw the visualization.
                var chart = new google.visualization.LineChart(document.getElementById("IncExpGraph"));
              //  chart.draw(data,  {chartArea:{left:70,top:10,width:805,height:360}, legend: { position: 'in' }, pointSize: 5, vAxis:{gridLines: {color: '#333',format:'#,###'}}, hAxis:{font: 'Arial Bold'} ,   colors:['#3366cc','#3366cc','#dc3912','#dc3912','#ff9900','#ff9900'] });
                chart.draw(data,  {chartArea:{left:70,top:10,width:805,height:360}, legend: { position: 'in' }, pointSize: 5, vAxis:{gridLines: {color: '#333',format:'#,###'}}, hAxis:{font: 'Arial Bold'} ,  series: [{color: '#3366cc'},{color: '#b2c2e0', visibleInLegend: false, lineWidth: 2, pointSize: 0 }, {color: '#dc3912'}, {color: '#ff9900'}] });
              
               
            }

            function displayDetail(accountCode, period, title)
            {
                
                var data =  jQuery.parseJSON(unescape( document.getElementById('<%= hfTransactions.ClientID %>').value));
               
                var output = "<h5 style=\"margin: 0;\">" + title + "</h5><br /><table cellpadding=\"4px\" style=\"margin: 0;\"><tr><td><b>Date</b></td><td><b>Description</b></td><td><b>Amount</b></td></tr>";
               
                 Enumerable.From(data).Where(function(x) {return x.AC == accountCode && x.Pe==period } ).ForEach(function(i)
                 {
                   var method  = "";
                    if(i.Me!="")
                        method=" <span class=\"PVKEY\">" + i.Me + "</span>";
                  output += "<tr><td>" + i.Dt + "</td><td>" + i.De + method + "</td><td>" + i.Am +"</td></tr>" ;
                 });
      
            output += "</table>";
              
                $('#DetailTable').html(output);
                $("#divTransDetail").dialog("open");
            }
	</script>


    <style type="text/css">
    .CellHover
    {
        cursor: pointer;
    }
  .PVKEY
  {
    font-size: xx-small;
    color: #999;
    font-style: italic ;   
  }
  .heading {
margin: 1px; 

padding: 3px 10px;
cursor: pointer;
position: relative;
background-color:#E2CB9A;
border-bottom-style: dashed;
border-width: 1px ;
text-align: center;
font-style: italic;
}
.content {
padding: 5px 0;
background-color:#fafafa;
}
    </style>
<div align="center" style="display: none">        
    <div>
        <asp:Label ID="btnHome" runat="server" class="">Home</asp:Label>
        <asp:Label ID="lblFinancial" runat="server" class="">Accounts</asp:Label>
    </div>
    <div><asp:Label ID="lbltest" runat="server" Visible="true"></asp:Label></div>
    <div><asp:Label ID="lblDate" runat="server" class=""></asp:Label></div>
    <div><asp:Label ID="lblCostCenter" runat="server" class="" ></asp:Label></div>
    </div>
   
   <table width="100%">
    <tr valign="top" >
        <td style="width: 100%">
            <div style="text-align:left">    
                <div style="width:auto">          
                    <div><b>Country:</b><br />
                        <asp:DropDownList ID="MyCountries" runat="server" AutoPostBack="true" Font-Bold="true" style="margin-bottom: 10px;"
                            Width="100%" Font-Size="8pt" OnSelectedIndexChanged="MyCountries_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                    <div><b>Profile:</b> <br />
                        <asp:DropDownList ID="MyProfiles" runat="server" AutoPostBack="true" Width="100%" style="margin-bottom: 10px;"
                            Font-Size="8pt" OnSelectedIndexChanged="MyProfiles_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                    <div><b>Responsibility Centre: </b><br />
                        <asp:DropDownList ID="MyAccounts" runat="server" AutoPostBack="true" Width="100%" style="margin-bottom: 10px;"
                         Font-Size="8pt" OnSelectedIndexChanged="MyAccounts_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>  
            </div>
            
        </td>
        <td ><div id="IncExpGraph" style="width: 875px; height: 400px;"></div></td>
    </tr>
   
   </table>
   
   


   

    <div id="accordion">
        <h3><a href="#Income">   
        <asp:GridView ID="gvIncome" runat="server" 
                ShowHeader="False" GridLines="None" CellPadding="0" Width="100%" >
            <RowStyle BorderStyle="None" HorizontalAlign="Right" />
        
        </asp:GridView></a></h3>
        <div style="margin:0px 0px 0px 0px ;padding:5px 6px 5px 29px">
            <asp:GridView ID="gvIncomeGLSummary" runat="server"  ShowHeader="False" 
                GridLines="None" RowStyle-BorderStyle="None" CellPadding="0" Width="100%" 
                onrowdatabound="gvIncomeGLSummary_RowDataBound"  >
                <AlternatingRowStyle BackColor="White" />
                
                <RowStyle BackColor="#EFF3FB" BorderStyle="None" HorizontalAlign="Right" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" />
            
                
            </asp:GridView>
            <div class="heading" style="">
                <asp:Label ID="lblChange" runat="server" Text="(Click here to show Donations...)"></asp:Label>
              
            </div>
            <div id="Donations" class="content">
                <asp:GridView ID="gvDonationSummary" runat="server"  ShowHeader="False"  BorderStyle="Solid" BorderColor="#999" BorderWidth="1px"
                GridLines="None" RowStyle-BorderStyle="None" CellPadding="0" Width="100%" 
                onrowdatabound="gvDonationSummary_RowDataBound"  >
                <AlternatingRowStyle BackColor="White" />
                
                <RowStyle BackColor="#fff8c8" BorderStyle="None" HorizontalAlign="Right" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" />
            
        
            </asp:GridView>

            </div>


        </div>
      
        <h3><a href="#Expenses"><asp:GridView ID="gvExpenses" AutoGenerateColumns="true" runat="server" ShowHeader="False" GridLines="None" CellPadding="0" Width="100%"  >
        <RowStyle BorderStyle="None" HorizontalAlign="Right" />
        </asp:GridView></a></h3>
        <div  style="margin:0px 0px 0px 0px ;padding:5px 6px 5px 29px">
            <asp:GridView ID="gvExpensesGLSummary" runat="server" ShowHeader="False" 
                GridLines="None" CellPadding="0" Width="100%" 
                onrowdatabound="gvExpensesGLSummary_RowDataBound" >
                <AlternatingRowStyle BackColor="White" />
                <RowStyle BackColor="#EFF3FB" HorizontalAlign="Right" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" />
                
            </asp:GridView>

        </div>
        <h3><a href="#Balance"><asp:GridView ID="gvBalance" AutoGenerateColumns="true" runat="server" ShowHeader="False" GridLines="None" CellPadding="0" Width="100%"  >
        <RowStyle BorderStyle="None" HorizontalAlign="Right"  />
        </asp:GridView></a></h3>
        <div>
           
        </div>
    </div>
    <div style="color:Gray; font-size: smaller;"> Starting Balance: <asp:Label ID="StartingBalance" runat="server" /> &nbsp; &nbsp;
    Ending Balance: <asp:Label ID="EndingBalance" runat="server" /></div>


<asp:HiddenField ID="hfTransactions" runat="server" />
<div id="divTransDetail" >
    <div id="DetailTable">
    </div>
</div>