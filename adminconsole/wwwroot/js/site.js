// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.



function findBootstrapEnvironment() {
    let envs = ['xs', 'sm', 'md', 'lg', 'xl'];

    let el = document.createElement('div');
    document.body.appendChild(el);

    let curEnv = envs.shift();

    for (let env of envs.reverse()) {
        el.classList.add(`d-${env}-none`);

        if (window.getComputedStyle(el).display === 'none') {
            curEnv = env;
            break;
        }
    }

    document.body.removeChild(el);
    return curEnv;
}


/**
 * Itinitalizes Google Map drawing with marker. This function is called on load
 * of Edit and Create views
 */
function initMap() {
    
    var lat = parseFloat($('#Latitude').attr('value'));
    var long = parseFloat($('#Longitude').attr('value'))

    console.log("lat: ", lat);
    console.log("long: ", long);

    console.log(findBootstrapEnvironment());

    if (!isNaN(lat) && !isNaN(long)) { // If we have a latitude and longitude, display map and drop pin


        displayMap(lat, long);


    }

}



/**
 * Displays the Google Map and a marker on the page
 * @param {any} lat:    Location Latitude
 * @param {any} long:   Location Longitude
 */
function displayMap(lat, long) {


    var location = { lat: lat, lng: long };




    // Center map on location
    var map = new google.maps.Map(
        document.getElementById('map-wrapper'), {
        zoom: 15,
        center: location
    });




    // Generate pin image to indicate position on map
    var image = {
        url: '/media/pin.png',
        scaledSize: new google.maps.Size(40, 50),
    };



    // Position marker on map
    var marker = new google.maps.Marker({
        position: location,
        map: map,
        icon: image,
        draggable: true
    });



    // Change value of Latitude and Longitude based on marker position
    google.maps.event.addListener(marker, 'dragend', function (marker) {
        var latLng = marker.latLng;
        var lat = latLng.lat().toFixedDown(6);
        var long = latLng.lng().toFixedDown(6);
        $('#Latitude').val(lat);
        $('#Longitude').val(long);
    });

}






/**
 * Gets the Latitude and Longitude from the Address and State fields 
 * then calls displayMap(lat, long) to drop the pin on the map
 **/
function getLatLongFromAddress() {
    // Get value used for geocoder
    var street = getStreet();
    var state = getState();


    var hasValidInput = hasAddress(street, state);
    console.log("hasValidInput: ", hasValidInput);
    if (!hasValidInput) {   // early return
        return;
    }


    var formattedAddress = street.concat(', ', state);

    geocoder = new google.maps.Geocoder();

    geocoder.geocode({ 'address': formattedAddress }, function (results, status) {
        if (status === 'OK') {

            lat = results[0]['geometry']['location'].lat();
            long = results[0]['geometry']['location'].lng();


            // Set Latitude and Longitude on the DOM to calculated values
            $('input[name="Latitude"]').val(lat);
            $('input[name="Longitude"]').val(long);


            displayMap(lat, long);
        } else {
            alert('Geocode was not successful for the following reason: ' + status);
        }
    });
}





/**
 * Checks if there are values in both Address and State fields.
 * 
 * @param {any} street : Address field on the DOM
 * @param {any} state : State field on the DOM
 * 
 * @returns {boolean} : true if address is okay for geocoding, otherwise false
 */
function hasAddress(street, state) {

    console.log(street);
    console.log(state);

    if (!street || !state) { // Both null

        alert('Please provide values for both Street and State before attempting to drop pin.');
        return false;

    }

    return true;
}




/**
 * Returns value of Street in Create and Edit Forms
 * 
 * @returns {string}: String representation of the address
 */
function getStreet() {
    var street = $("#Address").attr('value');

    if (street) {  // If on edit page
        console.log("Street: ", street);
        return street;
    }


    return $("#Address").val(); // If on create
}




/**
 * Returns value of State in Create and Edit Forms
 * 
 * @returns {string}: String representation of the state
 */
function getState() {
    return $("#State option:selected").text();
}




/**
 * Truncates JavaScript Float to maximum of 6 digits after the decimal. Left as 
 * a parameter for long-term code flexibility.
 */
Number.prototype.toFixedDown = function (digits = 6) {
    var re = new RegExp("([-+]?\\d+\\.\\d{" + digits + "})(\\d)"),
        m = this.toString().match(re);
    return m ? parseFloat(m[1]) : this.valueOf();
};

