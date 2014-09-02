<%@ Page Title="კონტაქტი | Fractal" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="Fractal.Contact" %>
<%@ Import Namespace="Core" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="cnt contact">
        <h1>კონტაქტი</h1>
        <p>თბილისი, ფალიაშვილის 44</p>

        <ul>
            <li class="tel">
                579 - 13 - 45 - 60
            </li>
            <li class="mail">
                <a href="mailto:fractal.co@gmail.com">fractal.co@gmail.com</a>
            </li>
            <li class="address">
                ფალიაშვილის 44
            </li>
        </ul>

    </section>
    <div id="map" class="map">

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptsPlaceHolder" runat="server">

<script src="//maps.google.com/maps/api/js?sensor=false" type="text/javascript"></script>
<script type="text/javascript">

    $("header nav li").eq(2).addClass("active");

    // When the window has finished loading create our google map below
    google.maps.event.addDomListener(window, 'load', init);
    
    function init() {
        var mapOptions = {
            zoom: 15,
            center: new google.maps.LatLng(41.708569, 44.766933),
            styles: [{ featureType: 'water', elementType: 'geometry', stylers: [{ hue: '#71ABC3' }, { saturation: -10 }, { lightness: -21 }, { visibility: 'simplified' }] }, { featureType: 'landscape.natural', elementType: 'geometry', stylers: [{ hue: '#7DC45C' }, { saturation: 37 }, { lightness: -41 }, { visibility: 'simplified' }] }, { featureType: 'landscape.man_made', elementType: 'geometry', stylers: [{ hue: '#C3E0B0' }, { saturation: 23 }, { lightness: -12 }, { visibility: 'simplified' }] }, { featureType: 'poi', elementType: 'all', stylers: [{ hue: '#A19FA0' }, { saturation: -98 }, { lightness: -20 }, { visibility: 'off' }] }, { featureType: 'road', elementType: 'geometry', stylers: [{ hue: '#FFFFFF' }, { saturation: -100 }, { lightness: 100 }, { visibility: 'simplified' }] }]
        };
        var mapElement = document.getElementById('map');
        var map = new google.maps.Map(mapElement, mapOptions);

        var FlagMarker = new google.maps.Marker({
            position: new google.maps.LatLng(41.708569, 44.766933),
            map: map
        });
    }
</script>
</asp:Content>