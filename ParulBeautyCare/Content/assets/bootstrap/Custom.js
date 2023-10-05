

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
    $('.DashboardTable').DataTable({
        "bJQueryUI": true,
        'dom': '<"top"f>rtip',
        "iDisplayLength": 5,
        language: {
            paginate: {
                next: '<i class="fa-solid fa-angle-right"></i>',
                previous: '<i class="fa-solid fa-angle-left"></i>'
            }
        },
        columnDefs: [
            { orderable: false, targets: [0] }, // Add other column indexes to remove
        ],
        aaSorting: [[0]]
    });

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


    var table = $('#Mybase-style2').DataTable({
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
                subTitle: function () { return SubTitle(); },
                title: function () { return MainTitle(); },
                filename: function () { return getExportFileName(); },
                className: 'btn border-0',
                action: function (e, dt, button, config) {
                    ExportDataTableToExcel(dt);
                }
            }
        ],
        columnDefs: [
            { orderable: false, targets: [0] }, // Add other column indexes to remove
        ],
        sort: false,
        initComplete: function () {
            var filterWrapper = $('.dataTables_filter');
            var buttonsContainer = $('<div class="buttons-container float-end d-flex"></div>').insertAfter(filterWrapper);
            $('.buttons-container').append($('.dt-buttons'));
        }
    });

    $('#Mybase-style2 thead tr:eq(0) th').each(function (i) {
        var title = $(this).text();
        $(this).html('<span class="headerText">' + title + '</span>');
        var column = table.column(i);
        var select = $('<select class="form-control js-example-basic-single" style="margin-top: 15px;"><option value="">' + title + '</option></select>')
            .appendTo($(this))
            .on('change', function () {
                var val = $.fn.dataTable.util.escapeRegex($(this).val());
                column.search(val ? '^' + val + '$' : '', true, false).draw();
            });

        column.data().unique().sort().each(function (d) {
            select.append($('<option value="' + d + '">' + d + '</option>'));
        });
        $(".js-example-basic-single").select2();
    });

    var divElement2 = $('<div>').addClass('table-responsive');

    var tableElement2 = $('#Mybase-style2');
    tableElement2.wrap(divElement2);

    function ExportDataTableToExcel(dataTable) {
        debugger
        var workbook = new ExcelJS.Workbook();
        var worksheet = workbook.addWorksheet('Sheet1');

        var title = MainTitle();
        var subtitle = SubTitle();
        // Add data from the DataTable to the worksheet starting from the 4th row
        var headers = [];
        dataTable.columns().every(function () {
            debugger
            var headerText = $(this.header()).find('.headerText').text().trim();
            headers.push(headerText);
        });

        // Determine the number of columns in the DataTable headers
        var headerColumns = headers.length;

        // Add empty rows for spacing
        worksheet.addRow([]); // Empty row for spacing
        worksheet.addRow([]); // Empty row for spacing

        // Add the headers from the DataTable
        worksheet.addRow(headers).eachCell((cell) => {
            cell.border = {
                top: { style: 'thin' },
                left: { style: 'thin' },
                bottom: { style: 'thin' },
                right: { style: 'thin' }
            },
                cell.font = { bold: true },
                cell.alignment = {
                    vertical: 'middle',
                    horizontal: 'center',
                    wrapText: true // Wrap text to fit within the cell
                };

            // Add padding (5 spaces on both sides)
            cell.value = '   ' + cell.value + '   ';

            // Adjust column width to fit the content (you can adjust the width as needed)
            const headerColumn = worksheet.getColumn(cell.col);
            headerColumn.width = Math.max(10, cell.value.length + 5); // Minimum width of 10 and add extra 5 to account for padding
        });


        // Merge cells for the title
        var titleMergeRange = 'A1:' + getColumnName(headerColumns) + '1';
        var titleCell = worksheet.getCell('A1');
        worksheet.mergeCells(titleMergeRange);

        // Set the value for the merged cell (title)
        titleCell.value = title;
        titleCell.font = {
            bold: true,
            size: 24
        };
        titleCell.fill = {
            type: 'pattern',
            pattern: 'solid',
            fgColor: { argb: 'dce6f1' }, // Set the background color to blue
        };
        titleCell.alignment = {
            vertical: 'middle',
            horizontal: 'center'
        };

        // Merge cells for the subtitle
        var subtitleMergeRange = 'A2:' + getColumnName(headerColumns) + '2';
        var subtitleCell = worksheet.getCell('A2');
        worksheet.mergeCells(subtitleMergeRange);

        // Set the value for the merged cell (subtitle)
        subtitleCell.value = subtitle;
        subtitleCell.font = {
            bold: true,
            size: 20
        };
        subtitleCell.fill = {
            type: 'pattern',
            pattern: 'solid',
            fgColor: { argb: 'dce6f1' }, // Set the background color to blue
        };
        subtitleCell.alignment = {
            vertical: 'middle',
            horizontal: 'center'
        };

        const startCell = worksheet.getCell('A3'); // Start cell for the filter range
        const endCell = worksheet.getCell(getColumnName(headerColumns) + '3'); // End cell for the filter range
        worksheet.autoFilter = {
            from: startCell.address,
            to: endCell.address
        };
        // Add sky blue background color to the row with filters
        worksheet.getRow(3).eachCell((cell) => {
            cell.fill = {
                type: 'pattern',
                pattern: 'solid',
                fgColor: { argb: '87CEEB' }, // Set the background color to sky blue
            };
            cell.font = {
                bold: true,
                size: 14
            };
        });

        dataTable.rows().every(function () {
            var rowData = [];
            $(this.node()).children('td').each(function (index) {
                // Exclude the first two columns from the export
           
                    var cellData = $(this).text().trim();
                    rowData.push('     ' + cellData + '     '); // Add padding to each cell's data
                
            });
            worksheet.addRow(rowData).eachCell((cell) => {
                cell.alignment = {
                    vertical: 'middle',
                    horizontal: 'center',
                    wrapText: true // Wrap text to fit within the cell
                };

                // Adjust column width to fit the content (you can adjust the width as needed)
                const column = worksheet.getColumn(cell.col);
                column.width = Math.max(15, cell.value.length + 5); // Minimum width of 10 and add extra 5 to account for padding
            });
        });

        workbook.xlsx.writeBuffer().then(function (buffer) {
            var blob = new Blob([buffer], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' });

            var url = URL.createObjectURL(blob);
            var link = document.createElement('a');
            link.href = url;
            link.download = getExportFileName() + '.xlsx';
            link.click();
            URL.revokeObjectURL(url);
        });
    }

    var table = $('#ReportTable').DataTable({
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
                title: function () { return MainTitle(); },
                filename: function () { return getExportFileName(); },
                className: 'btn border-0',
                action: function (e, dt, button, config) {
                    ReportTableToExcel(dt);
                }
            }
        ],
        columnDefs: [
            { orderable: false, targets: [0] }, // Add other column indexes to remove
        ],
        sort: false,
        initComplete: function () {
            var filterWrapper = $('.dataTables_filter');
            var buttonsContainer = $('<div class="buttons-container float-end d-flex"></div>').insertAfter(filterWrapper);
            $('.buttons-container').append($('.dt-buttons'));
        }
    });

    var divElement2 = $('<div>').addClass('table-responsive');

    var tableElement2 = $('#ReportTable');
    tableElement2.wrap(divElement2);

    function ReportTableToExcel(dataTable) {
        var workbook = new ExcelJS.Workbook();
        var worksheet = workbook.addWorksheet('Sheet1');

        var title = MainTitle();
        // Add data from the DataTable to the worksheet starting from the 4th row
        var headers = [];
        dataTable.columns().every(function () {
            headers.push(this.header().textContent.trim());
        });

        // Determine the number of columns in the DataTable headers
        var headerColumns = headers.length;

        // Add empty rows for spacing
        worksheet.addRow([]); // Empty row for spacing

        // Add the headers from the DataTable
        worksheet.addRow(headers).eachCell((cell) => {
            cell.border = {
                top: { style: 'thin' },
                left: { style: 'thin' },
                bottom: { style: 'thin' },
                right: { style: 'thin' }
            },
                cell.alignment = {
                    vertical: 'middle',
                    horizontal: 'center',
                    wrapText: true // Wrap text to fit within the cell
                };

            // Add padding (5 spaces on both sides)
            cell.value = '   ' + cell.value + '   ';

            // Adjust column width to fit the content (you can adjust the width as needed)
            const headerColumn = worksheet.getColumn(cell.col);
            headerColumn.width = Math.max(10, cell.value.length + 5); // Minimum width of 10 and add extra 5 to account for padding
        });


        // Merge cells for the title
        var titleMergeRange = 'A1:' + getColumnName(headerColumns) + '1';
        var titleCell = worksheet.getCell('A1');
        worksheet.mergeCells(titleMergeRange);

        // Set the value for the merged cell (title)
        titleCell.value = title;
        titleCell.font = {
            bold: true,
            size: 24
        };
        titleCell.fill = {
            type: 'pattern',
            pattern: 'solid',
            fgColor: { argb: 'dce6f1' }, // Set the background color to blue
        };
        titleCell.alignment = {
            vertical: 'middle',
            horizontal: 'center'
        };

     
        const startCell = worksheet.getCell('A2'); // Start cell for the filter range
        const endCell = worksheet.getCell(getColumnName(headerColumns) + '2'); // End cell for the filter range
        worksheet.autoFilter = {
            from: startCell.address,
            to: endCell.address
        };
        // Add sky blue background color to the row with filters
        worksheet.getRow(2).eachCell((cell) => {
            cell.fill = {
                type: 'pattern',
                pattern: 'solid',
                fgColor: { argb: '87CEEB' }, // Set the background color to sky blue
            };
            cell.font = {
                bold: true,
                size: 11
            };
        });
        dataTable.rows().every(function () {
            var rowData = [];
            $(this.node()).children('td').each(function (index) {
                // Exclude the first two columns from the export

                var cellData = $(this).text().trim();
                rowData.push('     ' + cellData + '     '); // Add padding to each cell's data

            });
            worksheet.addRow(rowData).eachCell((cell) => {
                cell.alignment = {
                    vertical: 'middle',
                    horizontal: 'center',
                    wrapText: true // Wrap text to fit within the cell
                };

                // Adjust column width to fit the content (you can adjust the width as needed)
                const column = worksheet.getColumn(cell.col);
                column.width = Math.max(15, cell.value.length + 5); // Minimum width of 10 and add extra 5 to account for padding
            });
        });

        var tfootData = [];

        // Calculate the starting row for tfoot data
        const tfootStartRow = 3 + dataTable.rows().count();

        dataTable.columns().every(function () {
            tfootData.push(this.footer().textContent.trim());
        });
        
        var headerColumns = headers.length;

        // Merge the cells for the first four columns in the tfoot row
        const mergeEndCol = 4; // Merge up to the 4th column
        const tfootMergeRange = 'A' + tfootStartRow + ':' + getColumnName(mergeEndCol) + tfootStartRow;
        worksheet.mergeCells(tfootMergeRange);

        // Set the value of tfootData[0] to the merged cell
        const tfootCell = worksheet.getCell('A' + tfootStartRow);
        tfootCell.value = tfootData[0];

        // Add formatting for the merged cell
        cellFormatting(tfootCell, '87CEEB');

        const fifthColumnCell = worksheet.getCell(getColumnName(mergeEndCol + 1) + tfootStartRow);
        fifthColumnCell.value = tfootData[4];

        // Add formatting for tfoot data in the fourth column
        cellFormatting(fifthColumnCell, 'FFFF00'); // Use the specified background color


        workbook.xlsx.writeBuffer().then(function (buffer) {
            var blob = new Blob([buffer], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' });

            var url = URL.createObjectURL(blob);
            var link = document.createElement('a');
            link.href = url;
            link.download = getExportFileName() + '.xlsx';
            link.click();
            URL.revokeObjectURL(url);
        });
    }

    function getColumnName(columnNumber) {
        var dividend = columnNumber;
        var columnName = '';
        var modulo;

        while (dividend > 0) {
            modulo = (dividend - 1) % 26;
            columnName = String.fromCharCode(65 + modulo) + columnName;
            dividend = Math.floor((dividend - modulo) / 26);
        }

        return columnName;
    }

    function cellFormatting(cell, bgColor) {
        cell.border = {
            top: { style: 'thin' },
            left: { style: 'thin' },
            bottom: { style: 'thin' },
            right: { style: 'thin' }
        };
        cell.alignment = {
            vertical: 'middle',
            horizontal: 'center',
            wrapText: true // Wrap text to fit within the cell
        };
        // Apply background color
        cell.fill = {
            type: 'pattern',
            pattern: 'solid',
            fgColor: { argb: bgColor || 'FFFFFF' }, // Set the background color (default to white)
        };
        cell.font = {
            bold: true,
            size: 11
        }
    }
});

/**/