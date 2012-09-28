<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Europe.ascx.vb" Inherits="DotNetNuke.Modules.AgapeConnect.Europe" %>
<script src="/DesktopModules/AgapeEurope/Map/jqvmap/jquery.vmap.js" type="text/javascript"></script>
<script src="/DesktopModules/AgapeEurope/Map/jqvmap/maps/jquery.vmap.europe.js" type="text/javascript"></script>
<link href="/DesktopModules/AgapeEurope/Map/jqvmap/jqvmap.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">
    (function ($, Sys) {
        function setUpMyTabs() {
            $('#vmap').vectorMap({
                map: 'europe_en',
                enableZoom: false,
                showTooltip: true,
                onRegionClick: function (element, code, region) {

                    $('.Country').hide();
                    $('#' + code).show();
                   

                    var message = 'You clicked "'
            + region
            + '" which has the code: '
            + code.toUpperCase();

                    //  alert(message);
                }
            });


        }

        $(document).ready(function () {
            setUpMyTabs();
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
                setUpMyTabs();
            });
        });
    } (jQuery, window.Sys));
   
  
</script>

            <div id="vmap" style="width: 420px; height: 420px;">
            </div>
       
