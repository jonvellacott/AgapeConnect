<%@ Control Language="vb" AutoEventWireup="false" Explicit="True" Inherits="DotNetNuke.UI.Skins.Skin" %>
<%@ Register TagPrefix="dnn" TagName="LOGO" Src="~/Admin/Skins/Logo.ascx" %>
<%@ Register TagPrefix="dnn" TagName="NAV" Src="~/Admin/Skins/Nav.ascx" %>
<%@ Register TagPrefix="dnn" TagName="USER" Src="~/Admin/Skins/User.ascx" %>
<%@ Register TagPrefix="dnn" TagName="LOGIN" Src="~/Admin/Skins/Login.ascx" %>
<%@ Register TagPrefix="dnn" TagName="SEARCH" Src="~/Admin/Skins/Search.ascx" %>
<%@ Register TagPrefix="dnn" TagName="COPYRIGHT" Src="~/Admin/Skins/Copyright.ascx" %>
<%@ Register TagPrefix="dnn" TagName="TERMS" Src="~/Admin/Skins/Terms.ascx" %>
<%@ Register TagPrefix="dnn" TagName="PRIVACY" Src="~/Admin/Skins/Privacy.ascx" %>
<link href="/Portals/_default/skins/AC-Boxes/OrangeSkin.css" rel="stylesheet" type="text/css" />
<style type="text/css">
    .style1
    {
        width: 104px;
    }
</style>
<div id="outerContainer" class="outerContainer" align="center">
    <div id="innerContainer" class="innerContainer">
        <!-- The Control Panel -->
        <div id="controlPanelContainer">
            <div id="ControlPanel" runat="server" />
        </div>
        <div id="Header">
            <table width="100%" cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td align="left">
                        <dnn:LOGO runat="server" ID="dnnLOGO" />
                    </td>
                    <td width="100%">
                        <div align="right" style="z-index: 10000; position: relative;">
                            <dnn:NAV runat="server" ID="dnnNAV" ProviderName="DNNMenuNavigationProvider" IndicateChildren="True"
                                IndicateChildImageRoot="/images/1x1.GIF" IndicateChildImageSub="/images/action_right.gif"
                                ControlOrientation="Horizontal" CSSNodeRoot="main_dnnmenu_rootitem" CSSNodeHoverRoot="main_dnnmenu_rootitem_hover"
                                CSSNodeSelectedRoot="main_dnnmenu_rootitem_selected" CSSBreadCrumbRoot="main_dnnmenu_rootitem_selected"
                                CSSContainerSub="main_dnnmenu_submenu" CSSNodeHoverSub="main_dnnmenu_itemhover"
                                CSSNodeSelectedSub="main_dnnmenu_itemselected" CSSContainerRoot="main_dnnmenu_container"
                                CSSControl="main_dnnmenu_bar" CSSBreak="main_dnnmenu_break" />
                        </div>
                        <div align="right">
                            <div style="float: right;">
                                <dnn:SEARCH runat="server" ID="dnnSEARCH" CssClass="ServerSkinWidget" UseDropDownList="True"
                                    ShowWeb="False" ShowSite="False" Submit="<img src=&quot;images/SearchIcon.gif&quot; border=&quot;0&quot; alt=&quot;Search&quot; /&gt;" />
                            </div>
                            <div style="clear: both;" />
                            <dnn:USER runat="server" ID="dnnUSER" CssClass="user" />
                            &nbsp; &nbsp;<dnn:LOGIN runat="server" ID="dnnLOGIN" CssClass="user" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td class="FullTopLeft" >
                                </td>
                                <td class="FullTop">
                                </td>
                               <td class="FullTopRight" >
                                </td>
                            </tr>
                            <tr>
                                <td class="FullLeft">
                                </td>
                                <td class="FullBack">
                                    <div id="mainContainer">
                                        <table cellspacing="0" cellpadding="0" border="0" width="100%" class="mainContentContainer">
                                            
                                            <tr valign="top">
                                                <td class="LeftPane" id="LeftPane" runat="server" valign="top">
                                                   
                                                </td>
                                                <td class="ContentPane" id="ContentPane" runat="server" valign="top" width="100%">
                                                   
                                                </td>
                                                <td class="RightPane" id="RightPane" runat="server" valign="top">
                                                   
                                                </td>
                                            </tr>
                                            
                                        </table>
                                    </div>
                                </td>
                                <td class="FullRight"></td>
                            </tr>
                            <tr>
                                <td class="FullBottomLeft" >
                                </td>
                                <td class="FullBottom" >
                                </td>
                                <td class="FullBottomRight" >
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <div style="width: 100%; direction: rtl;">
        </div>
        <div id="footer">
            <table width="100%">
                <tr>
                    <td>
                        <dnn:COPYRIGHT runat="server" ID="dnnCOPYRIGHT" CssClass="footer" />
                        &nbsp; &nbsp;
                        <dnn:TERMS runat="server" ID="dnnTERMS" CssClass="footer" />
                        &nbsp; &nbsp;
                        <dnn:PRIVACY runat="server" ID="dnnPRIVACY" CssClass="footer" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</div>
