//       alert('Wow!');
//     document.cookie = "latitude=" + position.coords.latitude;
//   document.cookie = "longitude=" + position.coords.longitude;


// Copyright All Rights Reserved
// Code provided by Mike Koenig for Seattle University Based Senior Projects
// All other usage of this code is prohibited without writen premission from Mike Koenig

//$(function () {
//    alert('Wow!');
//    setLocationCookie();
//});

function setCookie(cname, cvalue, exdays) {
    var d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    var expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + "; " + expires + "; path=/";
}

function getCookie(cname) {
    var name = cname + "=";
    var ca = document.cookie.split(";");
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) === " ") c = c.substring(1);
        if (c.indexOf(name) === 0) return c.substring(name.length, c.length);
    }
    return "";
}

function deleteCookie(cname) {
    var d = new Date();
    d.setTime(d.getTime() + (-1 * 24 * 60 * 60 * 1000));
    var expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + "" + "; " + expires;
}

function getLocation() {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(setLocationCookie);
    } else {
        Alert("Geolocation is not supported by this browser.");
    }
}

function setLocationCookie(position) {

    var LocationCookieLatitude = "latitude";
    var LocationCookieLongitude = "longitude";

    var myExpiration = 20;

    // if the Location cookie does not exists create one.
    var myLatitudeValue = getCookie(LocationCookieLatitude);
    var myLongitudeValue = getCookie(LocationCookieLongitude);

    if (myLatitudeValue === "" || myLongitudeValue === "") {

        // No cookie, so try to set one.

        // check if the browser supports cookie
        var testCookie = "testcookieLocationCookie";
        setCookie(testCookie, "1", myExpiration);
        myCookieValue = getCookie(testCookie);

        if (myCookieValue !== "") {
            //Supports Cookies

            // delete the test cookie
            deleteCookie(testCookie);

            // create a new cookie
            setCookie(LocationCookieLatitude, position.coords.latitude, myExpiration);
            setCookie(LocationCookieLongitude, position.coords.longitude, myExpiration);

            // re-load the page
            location.reload();
        }
    }
    else {
        // Has cookie.
        // if the current Location and the one stored in cookie are different
        // then store the new Location in the cookie and refresh the page.

        var PreviousLatitudeValue = parseFloat(myLatitudeValue);
        var PreviousLongitudeValue = parseFloat(myLongitudeValue);

        // user may have changed the Location
        if (PreviousLatitudeValue !== position.coords.latitude|| PreviousLongitudeValue !== position.coords.longitude) {

            // create a new cookie
            setCookie(LocationCookieLatitude, position.coords.latitude, myExpiration);
            setCookie(LocationCookieLongitude, position.coords.longitude, myExpiration);

            location.reload();
        }
    }
}