<%@ Control language="vb" AutoEventWireup="false" Explicit="True" Inherits="DotNetNuke.UI.Skins.Skin" %>

    <div id="outerContainer" style="width: 100%; margin-left: 0px; margin-right: 0px;"
        align="center">
        <div id="innerContainer" style="width: 600px;">
            <!-- The Control Panel -->
            <table cellpadding="0" cellspacing="0" style="width: 100%; background-color: #FFF;
                -moz-border-radius-bottomleft: 15px; -moz-border-radius-bottomright: 15px; -moz-border-radius-topleft: 3px;
                -moz-border-radius-topright: 3px;">
                <tr>
                    <td class="headerLeft">
                    </td>
                    <td class="headerRepeat">
                    </td>
                    <td class="headerRight">
                    </td>
                </tr>
                <tr valign="top">
                    <td class="mainLeft">
                        &nbsp
                    </td>
                    <td class="mainContentContainer">
                        <div style="font-size: large; color: #026890; ">
                            [MINISTRY]
                        </div>
                        <div style="font-size: xx-large; font-weight: bold; color: #026890">
                            Your Reimbursement</div>
                        <div>
                        <p class="indoc_bodycopyname" style="text-align: right;">
                                Our Ref: Rmb#[RMBNO]<br />
                                Your Ref: [USERREF]
                        </p>
                        <p>Dear [STAFFNAME],</p>
                        <p>This is to confirm that you have submitted a reimbursement. [STAFFACTION] [EXTRA]<br /><br />
                                This reimbursement must be approved by one of the following: </p>
                        <div style="margin-left: 40px;">
                                <strong><span>[APPROVERS]</span></strong></div>
                            
                            <p>
                        An email has been
                                sent to them with details of how to approve your reimbursement. You can track the
                                status of this reimbursement from the Reimbursement section of the Agap&eacute;
                                website.</p>
                            
                                <p>
                                If you have any questions, please contact the accounts department on (0121) 683
                                5094.</p>
                            <p>
                                <span style="font-family: verdana; font-size: 10pt;">In Christ,<br />
                                    [MINISTRY]</span></p>
                        </div>
                        <div style="width: 100%; text-align: right;"> 
                            <img alt="" src="/portals/_default/skins/agapeblue/[LOGOURL]" style="margin-bottom: 10px" />
                        </div>

                    </td>
                    <td class="mainRight">
                        &nbsp
                    </td>
                </tr>
                <tr>
                    <td class="footerLeft">
                    </td>
                    <td class="footerCentre">
                    </td>
                    <td class="footerRight">
                    </td>
                </tr>
            </table>
        </div>
    </div>

