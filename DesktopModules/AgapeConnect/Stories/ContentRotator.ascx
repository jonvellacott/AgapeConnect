﻿<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ContentRotator.ascx.vb" Inherits="DotNetNuke.Modules.AgapeConnect.Stories.ContentRotator" %>
<script src="/js/jquery.nivo.slider.js" type="text/javascript"></script>
<link href="/DesktopModules/AgapeConnect/Stories/themes/default/default.css" rel="stylesheet" type="text/css" media="screen"  />
<link href="/js/nivo-slider.css" rel="stylesheet" type="text/css" media="screen"  />

 <script type="text/javascript">
     (function ($, Sys) {
         function setUpMyTabs() {
             $('#slider').nivoSlider({
                 effect: 'fade',
                 pauseTime: 3500
             });


            
               


         }

         $(document).ready(function () {
             setUpMyTabs();
             Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
                 setUpMyTabs();
             });
         });
     } (jQuery, window.Sys));
   

   function registerClick(c)
   {
        $.ajax({ type: 'POST', url: "<%= NavigateURL() %>",
                        data: ({ StoryLink: c })
                    });
   }
  
</script>

<style type="text/css">
   

</style>
Hello World
<div style="width: 420px; height: 420px; ">
    <div class="slider-wrapper theme-default">
        
        <div id="slider" class="nivoSlider" style="height: 420px; max-height: 420px;  background-color: Black; overflow: hidden;">
          <%--  <div style="height: 420px; width: 420px; #position: absolute; #top: 50%;display: table-cell; vertical-align: middle;">
              <div class="greenBorder" style="#position: relative; #top: -50%">--%>
            <asp:Literal ID="ltStories" runat="server"></asp:Literal>
            </div>
            </div>
       
        </div>
      
<%--    </div>
    
  
</div>--%>

