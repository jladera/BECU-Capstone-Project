/**
 * @author Victor Chimenti
 * @file site.js
 */




// ** Globals ** //




// *** jquery card holder for items remaining to deisplay after user filter-search selections *** //
var visibleItems = [];
var assignVisibleItems = function () { };





// *** data received by map from ajax call to locations controller *** //
var records = [];




// *** set is populated with card items that become hidden during filter search
var removeMarker = new Set();

