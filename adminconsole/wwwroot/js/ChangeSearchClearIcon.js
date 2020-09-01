/** Sets the exportDataType attribute of the bootstrap
     * table to 'all'. This allows the export to export all
     * of the data in the table, instead of only the rows on
     * the current page. */

window.onload = function () {
    var x = document.getElementsByClassName("fa-trash");
    x[0].className = "fa fa-times";
};