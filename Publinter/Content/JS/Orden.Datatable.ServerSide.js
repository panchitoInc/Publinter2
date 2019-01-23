function OrdenTableIndexServerSide(url) {
    table = $('table#OrdenIndexTabla').dataTable({
        "searching": true,
        "paginatorAlwaysVisible": false,
        "pagingType": "full_numbers",
        "bLengthChange": false,
        "pageLength": 30,
        "scrollCollapse": true,
        //"scrollY": "600px",
        "info": false,
        //"columnDefs":
        //    [{"targets": [2],"visible": false,"searchable": false},
        //     {"targets": [3],"visible": false}
        //    ],
        "language": {
            "sProcessing": "Procesando...",
            "sLengthMenu": "Mostrar _MENU_ registros",
            "sZeroRecords": "No se encontraron resultados",
            "sEmptyTable": "Ningún dato disponible en esta tabla",
            "sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
            "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
            "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
            "sInfoPostFix": "",
            "sSearch": "Buscar:",
            "sUrl": "",
            "sInfoThousands": ",",
            "sLoadingRecords": "Cargando...",
            "oPaginate": {
                "sFirst": " Primero ",
                "sLast": " Último ",
                "sNext": " | Siguiente | ",
                "sPrevious": " | Anterior | "
            },
            "oAria": {
                "sSortAscending": ": Activar para ordenar la columna de manera ascendente",
                "sSortDescending": ": Activar para ordenar la columna de manera descendente"
            }
        },
        "serverSide": true,
        "ajax": {
            "url": url,
            "type": "POST"
        },
        "columns": [//datos con que se carga en las columnas y configuración
               {"data": "EmisionString", "orderable": true, "sType": 'date' },
               { "data": "Descripcion", "orderable": true, "class": "text-align-left" },
               { "data": "Campania", "orderable": true, "class": "text-align-left Cliente" },
               {
                   "data": "Total", "orderable": true, "class": "text-align-right",
                   "render": function (data, type, row)
                   { return "$ " + floatToStringDecimals(data, 2) }
               },
               {
			       "data": null, "orderable": false, "class": "text-align-right no-sort Acciones",
			       "render": function (data, type, row)
			       { return "" }
               },
			   { "data": "OrdenId", "class": "hidden" }
        ],
        "drawCallback": function (settings) {
            //despues de dibujar tabla
        },
        "order": [[0, "asc"]]
    });
}
