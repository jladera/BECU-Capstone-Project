/**
 * @author Victor Chimenti
 * @file map.js
 */





function initMap() {

    // *** set BECU Headquarters for default map starting location *** //
    var headquarters = {
        lat: 47.490209,
        lng: -122.272126
    };

    // *** set default map parameters *** //  
    var mapOptions = {
        center: headquarters,
        gestureHandling: 'cooperative',
        mapTypeId: google.maps.MapTypeId.ROADMAP,
        zoom: 10,
    };

    // *** render map to index page map-wrapper div in the center-index *** //
    map = new google.maps.Map(document.getElementById('map-wrapper'), mapOptions);

    // *** add directions service *** //
    directionsService = new google.maps.DirectionsService();
    directionsRenderer = new google.maps.DirectionsRenderer();

    // *** initialize directions renderer to the directionsPanel div below the map-wrapper *** //
    directionsRenderer.setMap(map);
    directionsRenderer.setPanel(document.getElementById('directionsPanel'));

    // *** initiate ajax call to get marker list *** //
    getJsonData();
}