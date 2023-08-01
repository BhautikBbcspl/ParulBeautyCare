

(function ($) {
    'use strict';
    $(function () {
        $('#MyTable').DataTable({
            "aLengthMenu": [
                [5, 10, 15, -1],
                [5, 10, 15, "All"]
            ],
            "iDisplayLength": 10,
            "language": {
                search: ""
            }
        });
        $('#MyTable').each(function () {
            var datatable = $(this);
            // SEARCH - Add the placeholder for Search and Turn this into in-line form control
            var search_input = datatable.closest('.dataTables_wrapper').find('div[id$=_filter] input');
            search_input.attr('placeholder', 'Search');
            search_input.removeClass('form-control-sm');
            // LENGTH - Inline-Form control
            var length_sel = datatable.closest('.dataTables_wrapper').find('div[id$=_length] select');
            length_sel.removeClass('form-control-sm');
        });
    });
})(jQuery);


$(document).ready(function () {
    $("#purchaseDate").flatpickr({
        dateFormat: "d/m/Y"
    });
  
});

/*Custome Data Table*/
$(document).ready(function () {
/*    $('#example').DataTable();*/
 
    $('#Mybase-style').DataTable({
        "bJQueryUI": true,
        'dom': '<"top"flB>rtip',

        language: {
            paginate: {
                next: '<i class="fa-solid fa-angle-right"></i>',
                previous: '<i class="fa-solid fa-angle-left"></i>'
            }
        },
        buttons: [
            {
                extend: 'excelHtml5',
                text: '<span class="fa fa-file-excel" style="font-size:1.5em;"></span>',
                footer: true,
                className: 'btn border-0',
                customizeData: function (data) {
                    var table = $('#Mybase-style').DataTable();
                    var rows = table.rows({ search: 'applied' }).nodes().to$();

                    var actionIndex = -1; // Store the index of the "Action" column header

                    for (var i = 0; i < data.header.length; i++) {
                        if (data.header[i] === 'Action') {
                            actionIndex = i;
                            break;
                        }
                    }

                    if (actionIndex !== -1) {
                        // Remove the "Action" column from the exported data
                        data.header.splice(actionIndex, 1);
                        data.body.forEach(function (row) {
                            row.splice(actionIndex, 1);
                        });
                    }

                    var isActiveIndex = -1; // Store the index of the "IsActive" column header

                    for (var i = 0; i < data.header.length; i++) {
                        if (data.header[i] === 'IsActive') {
                            isActiveIndex = i;
                            break;
                        }
                    }

                    if (isActiveIndex !== -1) {
                        for (var i = 0; i < rows.length; i++) {
                            var rowData = data.body[i];
                            var checkboxElement = $(rows[i]).find('input[type="checkbox"]');
                            var isActiveValue = checkboxElement.prop('checked') ? 'Yes' : 'No';
                            rowData[isActiveIndex] = isActiveValue; // Set "IsActive" value to the corresponding column
                        }
                    }
                }
            }
        ],
        columnDefs: [
            { orderable: false, targets: [0] }, // Add other column indexes to remove
        ],
        aaSorting: [[0]],
        initComplete: function () {
            var filterWrapper = $('.dataTables_filter');
            var buttonsContainer = $('<div class="buttons-container float-end d-flex"></div>').insertAfter(filterWrapper);
            $('.buttons-container').append($('.dt-buttons'));
        }
    });
	var divElement = $('<div>').addClass('table-responsive');

	// Select the table element by its ID
	var tableElement = $('#Mybase-style');

	// Wrap the table with the new div element
	tableElement.wrap(divElement);

});

/**/