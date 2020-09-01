# Locator
MVC Razor Page ATM Locator

## Views - Locations - Index
This is the index page that renders as Home and contains the  map, the card sidebar and the filter search options. It uses a 3 column layout.
1. **div#filter-wrapper**
- This column contains the filter/search fields
2. **div#center-index**
- This column contains the Google Map block
3. **div#sidebar**
- This column contains the list of ATM object cards

## LocationsController
Two Primary Methods
1. **Index()**
- Returns the results from the CleanLocationViewModel to div#sidebar in the Index View.
- The ATM Object Cards that render in the right column are derived from this method.
2. **CardJson()**
- Returns the results from the CleanLocationViewModel to the Ajax call in mapMethods.js at GetJsonData().
- The ATM Object Map Markers that render on the Google Map are derived from this method. 

## CleanLocationViewModel
This ViewModel generates the list of ATM object cards which are rendered to the razor page model. The list is generated from the CleanLocationModel.

## CleanLocationModel
This model has 3 partial classes
1. **CleanLocationModel_properties**
- This partial class is a declaration of all attributes in the Database Library Locations Model which will be used to create ATM objects for both cards and map marker pins.
2. **CleanLocationModel_methods**
- This partial class is the implementation of the parsing logic which scrubs null and undefined values before being passed on to the CleanLocationViewModel.
3. **CleanLocationModel_builder**
- This partial class generates the raw html tags that will populate the Razor page

## JavasScript Files
The global javascript declarations are in the site.js file.

### location.js
This file contains the session cookie which gathers and stores user location coordinates. This data is called by the LocationsController which handles the default location logic when the user declines cookies or location data.

### filterCards.js
This file contains the jQuery logic for the filter/search check boxes on the Razor page

### map.js
This file contains the basic map init() function to render a google map.

### mapMethods.js
This file contains the methods used by the google map to produce map markers.

## Next Steps
These items should be addressed:
1. The distance value used to sort the ATM Objects by distance from the user is currently displayed on the Razor page as a default geography value. The value is derived using the NetTopologySuite library and can be converted to kilometers or miles. This conversion needs to be done yet.
2. The mapMethods.js file has a method called createMarkerFromJsonRecord() which produces the map markers. This method also creates the event listeners that are used on the marker to display the title box and generate directions. This method needs to add a thrid event listener so that the cards hide and display based on the filter-search checkboxes.
3. The Razor page uses a for loop to display the ATM Object Cards in the div#sidebar. This logic can potentiall be moved server-side to the ViewModel level or the Controller level.
 