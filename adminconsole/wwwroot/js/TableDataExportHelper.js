/** Sets the exportDataType attribute of the bootstrap
     * table to 'all'. This allows the export to export all
     * of the data in the table, instead of only the rows on
     * the current page. */
var $table = $('#table');
$(function () {
    $table.bootstrapTable('destroy').bootstrapTable({
        exportDataType: 'all',
        exportTypes: ['excel', 'csv', 'pdf']
    })
});