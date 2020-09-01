/**
 * @author Victor Chimenti
 * @file mapMethods.js
 */





// *** drop a marker on each atm provided by the database *** //
function createMarkerFromJsonRecord(record, searchArea) {

    // launch new marker
    var atmMarker = new google.maps.Marker({
        map: map,
        position: record.position,
        title: record.name,
        id: record.locationId
    });

    //display name when hovered
    google.maps.event.addListener(atmMarker, 'hover', function () {
        map.setZoom(16);
        map.setCenter(atmMarker.getPosition());
    });

    // user clicks marker to initiate directions
    google.maps.event.addListener(atmMarker, 'click', function () {
        var request = {
            origin: searchArea,
            destination: record.position,
            travelMode: 'DRIVING'
        };
        directionsService.route(request, function (response, status) {
            if (status == 'OK') {
                directionsRenderer.setDirections(response);
            }
        });
    });
};



// *** drop a marker on the user's location or search request area *** //
function createSearchAreaMarker(searchArea) {

    // launch a user marker
    var userMarker = new google.maps.Marker({
        animation: google.maps.Animation.DROP,
        icon: {
            path: google.maps.SymbolPath.CIRCLE,
            fillColor: '#4470D6',
            fillOpacity: 1,
            scale: 9,
            strokeColor: '#F4F5F5',
            strokeWeight: 3
        },
        map: map,
        position: searchArea,
        title: 'My Location',
    });

    // center and zoom map on user location
    map.setCenter(searchArea);
    map.setZoom(15);

    // double click on user location marker to delete it
    userMarker.addListener('dblclick', function () {
        userMarker.setMap(null);
    });
}




// *** initiate production of map markers *** //  
function processRecords(userPosition) {

    // create a lat lng object for the map
    var searchArea = new google.maps.LatLng(userPosition.lat, userPosition.lng);

    // launch a marker and functionality on the user's requested search area
    createSearchAreaMarker(searchArea);

    // launch a marker for each atm or nfc in the list of records
    for (let i = 0; i < records.length; i++) {
        let record = records[i];
        createMarkerFromJsonRecord(record, searchArea);
    }
};




// *** send get request to the locations controller for clean data *** //  
async function getJsonData() {

    // assign function scope variables to unmarshal json object
    var doubleLat;
    var doubleLng;
    var userPosition = {}

    // receive ajax json message from the locations controller method cardjson
    $.ajax({
        traditional: true,
        url: '../locations/cardjson',
        type: "GET",
        data: {},
        contentType: 'application/json',
        success: function (data) {

            // process and assign location data
            doubleLat = parseFloat(data.point.lat);
            doubleLng = parseFloat(data.point.lng);
            userPosition = { lat: doubleLat, lng: doubleLng };

            // add clean location list to global js array
            for (let i = 0; i < data.cleanLocationList.length; i++) {
                records[i] = data.cleanLocationList[i];
            }
        },

        error: function (xhr, status, error) {
            var err = JSON.parse(xhr.responseText);
            alert(err.Message);
        },

    }).done(function () {

        // initiate call to map marker functions
        processRecords(userPosition);
    });
};
