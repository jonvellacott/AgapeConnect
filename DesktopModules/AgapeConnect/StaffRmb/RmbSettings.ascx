<%@ Control Language="vb" AutoEventWireup="false" CodeFile="RmbSettings.ascx.vb"
    Inherits="DotNetNuke.Modules.StaffRmb.RmbSettings" %>
<%@ Register Assembly="DotNetNuke" Namespace="DotNetNuke.UI.WebControls" TagPrefix="cc1" %>
<%@ Register TagPrefix="dnn" TagName="TextEditor" Src="~/controls/TextEditor.ascx" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<script src="/js/jquery.watermarkinput.js" type="text/javascript"></script>
<script src="/js/jquery.numeric.js" type="text/javascript"></script>
<script type="text/javascript">

    (function ($, Sys) {
        function setUpMyTabs() {
            var selectedTabIndex = $('#<%= theHiddenTabIndex.ClientID  %>').attr('value');
            $('#tabs').tabs({
                show: function () {
                    var newIdx = $('#tabs').tabs('option', 'selected');
                    $('#<%= theHiddenTabIndex.ClientID  %>').val(newIdx);
                },
                selected: selectedTabIndex
            });

            $('.numeric').numeric();
            $('.pdName').Watermark('Rate Name');
            $('.pdValue').Watermark('Rate Value');

        }

        $(document).ready(function () {
            setUpMyTabs();
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
                setUpMyTabs();
            });
        });
    } (jQuery, window.Sys));

   
</script>
<div id="tabs" style="width: 90%; text-align: Left;">
    <ul>
        <li><a href='#Tab1-tab'>Settings</a></li>
        <li><a href='#Tab2-tab'>Rates</a></li>
        <li><a href='#Tab3-tab'>Roles</a></li>
        <li><a href='#Tab4-tab'>Expense Types</a></li>
        <li><a href='#Tab5-tab'>Help</a></li>
    </ul>
    <div style="width: 100%; min-height: 350px; background-color: #FFFFFF;">
        <div id='Tab1-tab'>
            <table style="font-size: 9pt;">
                <tr>
                    <td>
                        <b>
                            <dnn:Label ID="lblNoReceipt" runat="server" ControlName="tbNoReceipt" ResourceKey="lblNoReceipt" />
                        </b>
                    </td>
                    <td>
                        <asp:TextBox ID="tbNoReceipt" runat="server" Width="80px"></asp:TextBox>
                    </td>
                </tr>
                <tr style="opacity: 0.4; filter: alpha(opacity=40);">
                    <td>
                        <b>
                            <dnn:Label ID="lblVAT" runat="server" ControlName="cbVAT" ResourceKey="lblVAT" />
                        </b>
                    </td>
                    <td>
                        <asp:CheckBox ID="cbVAT" runat="server" Enabled="false" />
                        *Not yet Implemented (Coming soon!)
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>
                            <dnn:Label ID="lblExpire" runat="server" ControlName="tbExpire" ResourceKey="lblExpire" />
                        </b>
                    </td>
                    <td>
                        <asp:TextBox ID="tbExpire" runat="server" Width="80px"></asp:TextBox>
                        <asp:Label ID="Label21" runat="server" resourcekey="Months"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>
                            <dnn:Label ID="lblTeamLeaderLimit" runat="server" ControlName="tbTeamLeaderLimit"
                                ResourceKey="lblTeamLeaderLimit" />
                        </b>
                    </td>
                    <td>
                        <asp:TextBox ID="tbTeamLeaderLimit" runat="server" Width="80px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label9" runat="server" resourcekey="lblDistanceUnit"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlDistance" runat="server">
                            <asp:ListItem Text="Miles" Value="miles" />
                            <asp:ListItem Text="Kilometers" Value="km" />
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>
                            <dnn:Label ID="lblMenuSize" runat="server" ControlName="tbMenuSize" ResourceKey="lblMenuSize" />
                        </b>
                    </td>
                    <td>
                        <asp:TextBox ID="tbMenuSize" runat="server" Width="80px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>
                            <dnn:Label ID="Label8" runat="server" ControlName="cbCurConverter" ResourceKey="lblCurConverter" />
                        </b>
                    </td>
                    <td>
                        <asp:CheckBox ID="cbCurConverter" runat="server"   />
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>
                            <dnn:Label ID="Label5" runat="server" ControlName="ddlDownloadFormat" ResourceKey="lblDownloadFormat" />
                        </b>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlDownloadFormat" runat="server">
                            <asp:ListItem Text="Solomon: Desc, Debit, Credit" Value="DDC" />
                            <asp:ListItem Text="Solomon: Debit, Credit, Description" Value="DCD" />
                            <asp:ListItem Text="Solomon: Company, Desc, Debit, Credit" Value="CDDC" />
                            <asp:ListItem Text="Solomon: Company, Debit, Credit, Description" Value="CDCD" />
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>
                            <dnn:Label ID="lblControlAccount" runat="server" ControlName="ddlControlAccount"
                                ResourceKey="lblControlAccount" />
                        </b>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlControlAccount" runat="server" DataSourceID="dsCostCenters"
                            DataTextField="DisplayName" DataValueField="CostCentreCode" AppendDataBoundItems="true">
                            <asp:ListItem Text="" Value="" />
                        </asp:DropDownList>
                        <asp:Label ID="oopsControlAccount" runat="server" Text="" ForeColor="Red"></asp:Label>
                        <asp:LinqDataSource ID="dsCostCenters" runat="server" ContextTypeName="StaffBroker.StaffBrokerDataContext"
                            EntityTypeName="" OrderBy="CostCentreCode" Select="new (CostCentreCode,CostCentreCode + ' ' + '-' + ' ' + CostCentreName as DisplayName)"
                            TableName="AP_StaffBroker_CostCenters" Where="PortalId == @PortalId">
                            <WhereParameters>
                                <asp:ControlParameter ControlID="hfPortalId" DefaultValue="-1" Name="PortalId" PropertyName="Value"
                                    Type="Int32" />
                            </WhereParameters>
                        </asp:LinqDataSource>
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>
                            <dnn:Label ID="lblAccountsReceivable" runat="server" ControlName="ddlAccountsReceivable"
                                ResourceKey="lblAcctRec" />
                        </b>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlAccountsReceivable" runat="server" Width="60px" DataSourceID="dsAccountCodes2"
                            DataTextField="DisplayName" DataValueField="AccountCode">
                        </asp:DropDownList>
                        <asp:Label ID="oopsAccountsReceivable" runat="server" Text="" ForeColor="Red"></asp:Label>
                        <asp:LinqDataSource ID="dsAccountCodes2" runat="server" ContextTypeName="StaffRmb.StaffRmbDataContext"
                            EntityTypeName="" Select="new (AccountCode,  AccountCode + ' ' + '-' + ' ' + AccountCodeName  as DisplayName )"
                            TableName="AP_StaffBroker_AccountCodes" OrderBy="AccountCode" Where="PortalId == @PortalId &amp;&amp; AccountCodeType == @AccountCodeType">
                            <WhereParameters>
                                <asp:ControlParameter ControlID="hfPortalId" DefaultValue="-1" Name="PortalId" PropertyName="Value"
                                    Type="Int32" />
                                <asp:Parameter DefaultValue="1" Name="AccountCodeType" Type="Byte" />
                            </WhereParameters>
                        </asp:LinqDataSource>
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>
                            <dnn:Label ID="lblTaxAccountsReceivable" runat="server" ControlName="ddlTaxAccountsReceivable"
                                ResourceKey="lblTaxAccRec" />
                        </b>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlTaxAccountsReceivable" runat="server" Width="60px" DataSourceID="dsAccountCodes2"
                            DataTextField="DisplayName" DataValueField="AccountCode">
                        </asp:DropDownList>
                        <asp:Label ID="oopsTaxAccountsReceivable" runat="server" Text="" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>
                            <dnn:Label ID="Label4" runat="server" ControlName="ddlAccountsPayable" ResourceKey="lblAccPay" />
                        </b>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlAccountsPayable" runat="server" Width="60px" DataSourceID="dsAccountCodes3"
                            DataTextField="DisplayName" DataValueField="AccountCode">
                        </asp:DropDownList>
                        <asp:Label ID="oopsAccountsPayable" runat="server" Text="" ForeColor="Red"></asp:Label>
                        <asp:LinqDataSource ID="dsAccountCodes3" runat="server" ContextTypeName="StaffRmb.StaffRmbDataContext"
                            EntityTypeName="" Select="new (AccountCode,  AccountCode + ' ' + '-' + ' ' + AccountCodeName  as DisplayName )"
                            TableName="AP_StaffBroker_AccountCodes" OrderBy="AccountCode" Where="PortalId == @PortalId &amp;&amp; AccountCodeType == @AccountCodeType">
                            <WhereParameters>
                                <asp:ControlParameter ControlID="hfPortalId" DefaultValue="-1" Name="PortalId" PropertyName="Value"
                                    Type="Int32" />
                                <asp:Parameter DefaultValue="2" Name="AccountCodeType" Type="Byte" />
                            </WhereParameters>
                        </asp:LinqDataSource>
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>
                            <dnn:Label ID="Label7" runat="server" ControlName="ddlPayrollPayable" ResourceKey="lblPayroll" />
                        </b>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlPayrollPayable" runat="server" Width="60px" DataSourceID="dsAccountCodes3"
                            DataTextField="DisplayName" DataValueField="AccountCode">
                        </asp:DropDownList>
                        <asp:Label ID="lblOopsPayroll" runat="server" Text="" ForeColor="Red"></asp:Label>
                        
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>
                            <dnn:Label ID="Label25" runat="server" ControlName="ddlSalaryAccount" ResourceKey="lblSalary" />
                        </b>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlSalaryAccount" runat="server" Width="60px" DataSourceID="dsAccountCodes"
                            DataTextField="DisplayName" DataValueField="AccountCode">
                        </asp:DropDownList>
                        <asp:Label ID="lblOopsSalary" runat="server" Text="" ForeColor="Red"></asp:Label>
                        
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>
                            <dnn:Label ID="Label26" runat="server" ControlName="ddlBankAccount" ResourceKey="lblBankAccount" />
                        </b>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlBankAccount" runat="server" Width="60px" DataSourceID="dsAccountCodes2"
                            DataTextField="DisplayName" DataValueField="AccountCode">
                        </asp:DropDownList>
                        <asp:Label ID="lblOopsBank" runat="server" Text="" ForeColor="Red"></asp:Label>
                        
                    </td>
                </tr>
            </table>
        </div>
        <div id='Tab2-tab'>
            <table style="font-size: 9pt;">
                <tr valign="top">
                    <td>
                        <b>
                            <dnn:Label ID="lblMileage" runat="server" ControlName="tbTeamLeaderLimit" ResourceKey="lblMileage" />
                        </b>
                    </td>
                    <td>
                        <table style="font-size: 9pt">
                            <tr>
                                <td>
                                    <asp:Label ID="Label22" runat="server" resourcekey="lblRate1"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="tbMRate1Name" runat="server" Width="80px" CssClass="pdName"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="tbMRate1" runat="server" Width="80px" CssClass="numeric pdValue"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label6" runat="server" resourcekey="lblRate2"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="tbMRate2Name" runat="server" Width="80px" CssClass="pdName"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="tbMRate2" runat="server" Width="80px" CssClass="numeric pdValue"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label23" runat="server" resourcekey="lblRate3"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="tbMRate3Name" runat="server" Width="80px" CssClass="pdName"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="tbMRate3" runat="server" Width="80px" CssClass="numeric pdValue"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label24" runat="server" resourcekey="lblRate4"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="tbMRate4Name" runat="server" Width="80px" CssClass="pdName"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="tbMRate4" runat="server" Width="80px" CssClass="numeric pdValue"></asp:TextBox>
                                </td>
                            </tr>
                            
                        </table>
                    </td>
                </tr>
                <tr valign="top">
                    <td>
                        <b>
                            <dnn:Label ID="Label1" runat="server" ControlName="tbSubBreakfast" ResourceKey="lblPerDiem" />
                        </b>
                    </td>
                    <td>
                        <table style="font-size: 9pt">
                            <tr>
                                <td>
                                    <asp:Label ID="Label10" runat="server" resourcekey="lblRate1"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="tbPD1Name" runat="server" Width="80px" CssClass="pdName"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="tbPD1Value" runat="server" Width="80px" CssClass="numeric pdValue"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label11" runat="server" resourcekey="lblRate2"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="tbPD2Name" runat="server" Width="80px" CssClass="pdName"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="tbPD2Value" runat="server" Width="80px" CssClass="numeric pdValue"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label12" runat="server" resourcekey="lblRate3"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="tbPD3Name" runat="server" Width="80px" CssClass="pdName"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="tbPD3Value" runat="server" Width="80px" CssClass="numeric pdValue"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label13" runat="server" resourcekey="lblRate4"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="tbPD4Name" runat="server" Width="80px" CssClass="pdName"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="tbPD4Value" runat="server" Width="80px" CssClass="numeric pdValue"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label14" runat="server" resourcekey="lblRate5"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="tbPD5Name" runat="server" Width="80px" CssClass="pdName"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="tbPD5Value" runat="server" Width="80px" CssClass="numeric pdValue"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label15" runat="server" resourcekey="lblRate6"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="tbPD6Name" runat="server" Width="80px" CssClass="pdName"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="tbPD6Value" runat="server" Width="80px" CssClass="numeric pdValue"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr valign="top">
                    <td>
                        <b>
                            <dnn:Label ID="Label2" runat="server" ControlName="tbEntBreakfast" ResourceKey="lblEntertaining" />
                        </b>
                    </td>
                    <td>
                        <table style="font-size: 9pt">
                            <tr>
                                <td>
                                    <asp:Label ID="Label16" runat="server" resourcekey="lblBreakfast"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="tbEntBreakfast" runat="server" Width="80px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label17" runat="server" resourcekey="lblLunch"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="tbEntLunch" runat="server" Width="80px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label18" runat="server" resourcekey="lblDinner"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="tbEntDinner" runat="server" Width="80px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label19" runat="server" resourcekey="lblOvernight"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="tbEntOvernight" runat="server" Width="80px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label20" runat="server" resourcekey="lblDay"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="tbEntDay" runat="server" Width="80px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <div id='Tab3-tab'>
            <table style="font-size: 9pt;">
                <tr>
                    <td>
                        <b>
                            <dnn:Label ID="lblAccountsRole" runat="server" ControlName="rsgAccountsRoles" ResourceKey="lblAccountsRole" />
                        </b>
                    </td>
                    <td>
                        <cc1:RolesSelectionGrid ID="rsgAccountsRoles" runat="server">
                        </cc1:RolesSelectionGrid>
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>
                            <dnn:Label ID="lblAccountsName" runat="server" ControlName="tbAccountsName" ResourceKey="lblAccountsName" />
                        </b>
                    </td>
                    <td>
                        <asp:TextBox ID="tbAccountsName" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>
                            <dnn:Label ID="lblAccountsEmail" runat="server" ControlName="tbAccountsEmail" ResourceKey="lblAccountsEmail" />
                        </b>
                    </td>
                    <td>
                        <asp:TextBox ID="tbAccountsEmail" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>
                            <dnn:Label ID="lblAuthUser" runat="server" ControlName="ddlAuthUser" ResourceKey="lblAuthUser" />
                        </b>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlAuthUser" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>
                            <dnn:Label ID="lblAuthAuthUser" runat="server" ControlName="ddlAuthAuthAuthUser"
                                ResourceKey="lblAuthAuthUser" />
                        </b>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlAuthAuthUser" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </div>
        <div id='Tab4-tab'>
            <table style="font-size: 9pt;">
                <tr>
                    <td>
                        <b>
                            <dnn:Label ID="lblUseDCode" runat="server" ControlName="cbUserDCode" ResourceKey="lblUseDCode" />
                        </b>
                    </td>
                    <td>
                        <asp:CheckBox ID="cbUseDCode" runat="server" AutoPostBack="true" />
                    </td>
                </tr>
                <tr valign="top">
                    <td>
                        <b>
                            <dnn:Label ID="Label3" runat="server" ControlName="cblExpenseTypes" ResourceKey="lblExpenseTypes" />
                        </b>
                    </td>
                    <td>
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="LineTypeId"
                            DataSourceID="dsLineTypes">
                            <Columns>
                                <asp:BoundField DataField="TypeName" HeaderText="TypeName" SortExpression="TypeName" />
                                <asp:TemplateField HeaderText="Enable">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hfLineTypeId" runat="server" Value='<%# Eval("LineTypeId") %>' />
                                        <asp:CheckBox ID="cbEnable" runat="server" Checked='<%# IsEnabled(Eval("LineTypeId")) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Display Name">
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="tbDisplayName" Text='<%# GetDisplayName(Eval("LineTypeId"), Eval("TypeName")) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PCode">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlPCode" runat="server" Width="60px" DataSourceID="dsAccountCodes"
                                            DataTextField="DisplayName" SelectedValue='<%#  GetPCode(Eval("LineTypeId")) %>'
                                            DataValueField="AccountCode" AppendDataBoundItems="true">
                                            <asp:ListItem Text="" Value="" />
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="DCode">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlDCode" runat="server" Width="60px" DataSourceID="dsAccountCodes"
                                            DataTextField="DisplayName" SelectedValue='<%# GetDCode(Eval("LineTypeId")) %>'
                                            DataValueField="AccountCode" AppendDataBoundItems="true">
                                            <asp:ListItem Text="" Value="" />
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:LinqDataSource ID="dsLineTypes" runat="server" ContextTypeName="StaffRmb.StaffRmbDataContext"
                            EntityTypeName="" TableName="AP_Staff_RmbLineTypes" OrderBy="TypeName">
                        </asp:LinqDataSource>
                        <asp:LinqDataSource ID="dsAccountCodes" runat="server" ContextTypeName="StaffRmb.StaffRmbDataContext"
                            EntityTypeName="" Select="new (AccountCode,  AccountCode + ' ' + '-' + ' ' + AccountCodeName  as DisplayName )"
                            TableName="AP_StaffBroker_AccountCodes" OrderBy="AccountCode" Where="PortalId == @PortalId &amp;&amp; AccountCodeType == @AccountCodeType">
                            <WhereParameters>
                                <asp:ControlParameter ControlID="hfPortalId" DefaultValue="-1" Name="PortalId" PropertyName="Value"
                                    Type="Int32" />
                                <asp:Parameter DefaultValue="4" Name="AccountCodeType" Type="Byte" />
                            </WhereParameters>
                        </asp:LinqDataSource>
                    </td>
                </tr>
            </table>
        </div>
          <div id='Tab5-tab'>
           <iframe width="853" height="480" src="https://www.youtube.com/embed/7h1HFWFuCLk?rel=0&wmode=transparent" frameborder="0" allowfullscreen></iframe>
        </div>
    </div>
</div>
<asp:HiddenField ID="hfPortalId" runat="server" Value="-1" />
<asp:HiddenField ID="theHiddenTabIndex" runat="server" Value="0" ViewStateMode="Enabled" />
<asp:LinkButton ID="SaveBtn" runat="server" ResourceKey="btnSave"></asp:LinkButton>
&nbsp;
<asp:LinkButton ID="CancelBtn" runat="server" ResourceKey="btnCancel"></asp:LinkButton>