<%@ Control language="vb" AutoEventWireup="false" Explicit="True" Inherits="DotNetNuke.UI.Skins.Skin" %>
<style type="text/css">
    .flipV{
    -moz-transform: scaleY(-1);
    -o-transform: scaleY(-1);
    -webkit-transform: scaleY(-1);
    transform: scaleY(-1);
    filter: flipv;
    -ms-filter:flipv;
    }
    <!--
.ReadMsgBody
	{width:100%}
.ExternalClass
	{width:100%}

    
body {
	background-color: #DDDDDD;
}
.indoc_bodycopy {
	font-family: Verdana, Arial, Helvetica, sans-serif;
	font-size: 12px;
	line-height: 20px;
	color: #333333;
	font-style: normal;
	text-align: justify;
	padding-top: 3px;
	padding-right: 15px;
	padding-bottom: 3px;
	padding-left: 15px;
}
.indoc_bodycopyname {
	font-family: Verdana, Arial, Helvetica, sans-serif;
	font-size: 10pt;
	line-height: 16px;
	color: #999999;
	font-style: italic;
	text-align: justify;
	padding-top: 3px;
	padding-right: 15px;
	padding-bottom: 3px;
	padding-left: 1px;
}
.indoc_link {
	color: #660000;
	text-decoration: underline;
}
.indoc_pics {
	font-family: Verdana, Arial, Helvetica, sans-serif;
	font-size: 12px;
	line-height: 20px;
	color: #333333;
	font-style: normal;
	text-align: justify;
	padding-top: 3px;
	padding-right: 0px;
	padding-bottom: 3px;
	padding-left: 15px;
}
.indoc_newstitles {
	font-size: 16px;
	font-weight: bold;
}
.indoc_bodycopysmall {
	font-family: Verdana, Arial, Helvetica, sans-serif;
	font-size: 10px;
	line-height: 14px;
	color: #333333;
	font-style: normal;
	text-align: center;
	padding-top: 5px;
	padding-right: 0px;
	padding-bottom: 0px;
	padding-left: 0px;
}

</style>
<div style="background-color: #dddddd;">
    <table border="0" cellspacing="0" cellpadding="0" width="550" align="center" style="background-color: #ffffff;">
        <!--DWLayoutTable-->
        <tbody>
            <tr>
                <td valign="top" style="width: 550px; height: 37px;">
                    <div>
                        <img alt="Page top" width="550" height="37" src="http://localhost:13059/Portals/_default/skins/AgapeBlue/images/header2.gif" /></div>
                </td>
            </tr>
            <tr>
                <td class="indoc_bodycopy" valign="top">
                    <div style="font-size: large; color: #660000; font-weight: bold;">
                        [MINISTRY]</div>
                    <div style="margin-top: 3px; font-size: xx-large; color: #660000; font-weight: bold;">
                        YOUR REIMBURSEMENT</div>
                    <p class="indoc_bodycopyname" style="text-align: right;">
                        Our Ref: Rmb#[RMBNO]<br />
                        Your Ref: [USERREF]</p>
                    <div style="text-align: left;">
                        <span style="font-family: verdana; font-size: 10pt;">Dear [STAFFNAME],</span></div>
                    <span style="font-family: verdana; font-size: 10pt;">
                        <br />
                        This is to confirm that you have submitted a reimbursement. [STAFFACTION] [EXTRA]<br />
                        <br />
                        This reimbursement must be approved by one of the following: </span>
                    <br />
                    <br />
                    <div style="margin-left: 40px;">
                        <strong><span style="font-family: verdana; font-size: 10pt;">[APPROVERS]</span></strong></div>
                    <br />
                    <span style="font-size: 10pt;"><span style="font-family: verdana;">An email has been
                        sent to them with details of how to approve your reimbursement. You can track the
                        status of this reimbursement from the Reimbursement section of the Agap&eacute;
                        website.<br />
                    </span><span style="font-family: verdana; font-size: 10pt;">
                        <br />
                        If you have any questions, please contact the accounts department on (0121) 683
                        5094.</span></span><br />
                    <br />
                    <p>
                        <span style="font-family: verdana; font-size: 10pt;">In Christ,<br />
                            [MINISTRY]</span></p>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <img alt="logo" src="/portals/_default/skins/agapeblue/[LOGOURL]" style="margin-right: 5px;" />
                </td>
            </tr>
            <tr>
                <td valign="top" style="width: 550px; height: 37px;">
                    <div>
                        <img alt="Page bottom" width="550" height="37" class="flipV" src="http://www.agape.org.uk/Portals/0/Email%20Templates/page_top.gif" /></div>
                </td>
            </tr>
        </tbody>
    </table>
    <table border="0" cellspacing="0" cellpadding="0" width="550" align="center" style="background-color: #dddddd;">
        <!--DWLayoutTable-->
        <tbody>
            <tr>
                <td valign="top" style="text-align: center; background-color: #dddddd; width: 550px;">
                    <span style="line-height: 170%; font-family: verdana; font-size: 7pt;">This email is
                        intended for staff of [MINISTRY] only. If you are not the intended recipient, please
                        delete this email immediately. [MINISTRY] is part of Campus Crusade for Christ International.</span><br />
                    &nbsp;
                </td>
            </tr>
        </tbody>
    </table>
</div>
</div>
