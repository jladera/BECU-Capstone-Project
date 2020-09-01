// Ajax call to get more records from a database

var rowCount = $('tr').length - 1;
var RANGE_SIZE = 100;
var continueLooping = true;

$(document).ready(function () {
    for (var i = 0; i < 2; i++){
        $.post("../locations/getrangeofrecords",
            {
                start_index: rowCount + RANGE_SIZE * i,
                num_records: RANGE_SIZE
            },
            function (data, status) {
                if (data['html'].length > 0) {
                    var dataAsHtml = $.parseHTML(data['html']);
                    $("tbody").append(dataAsHtml);
                }
                
            });
    }
});