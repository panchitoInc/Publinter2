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
            "type": "POST",
            "data": function (d) {
                d.anuncianteId = $("#selectAnunciante").val();
                d.campaniaId = $("#selectCampania").val();
                d.medioId = $("#selectMedio").val();
                d.search = $("#inpBusqueda").val();
            }
        },
        "columns": [//datos con que se carga en las columnas y configuración
           { "data": "EmisionString", "orderable": true, "sType": 'date' },
            {
                "data": "Descripcion", "orderable": true, "class": "text-align-left",
                "render": function (data, type, row) {
                    return (row.Anulada) ? '<span class="pull-left" style="color:red">' + data + '</span>' + ' <span class="pull-right" style="color:red">Anulada</span>' : data;
                }
            },
           { "data": "Medio", "orderable": true, "class": "text-align-left Medio" },
           { "data": "Anunciante", "orderable": true, "class": "text-align-left Anunciante" },
           { "data": "Campania", "orderable": true, "class": "text-align-left Cliente" },
           {
               "data": "Total", "orderable": true, "class": "text-align-right",
               "render": function (data, type, row)
               {
                   var retorno = "";
                   if (!row.Anulada) {
                       retorno += "$ " + floatToStringDecimals(data, 2);
                   }
                   return retorno;
               }
           },
           {
               "data": "Acciones",
               className: "text-align-right",
               "render": function (data, type, row) {
                   var retorno = "";
                   if (!row.Anulada) {
                       
                       retorno = '<div class="dropdown"><a class="dropdown-toggle text-align-right" data-toggle="dropdown" href="#" aria-expanded="false">' +
                                 'Acciones <span class="caret"></span>' +
                                 '</a>' +
                                 '<ul class="dropdown-menu">' +
                                 
                                 '<li role="presentation"><a class="text-align-center" role="menuitem" tabindex="-1" href="../Orden/DescargarPdf?ordenId=' + row.OrdenId + '">Descargar Pdf</a></li>' +
                                 '<li role="presentation"><a class="text-align-center" role="menuitem" tabindex="-1" href="javascript:CopiarOrden(' + row.OrdenId + ')">Copiar</a></li>' +
                                 '<li role="presentation"><a class="text-align-center" role="menuitem" tabindex="-1" href="javascript:AbrirPopupEmail(' + row.OrdenId +','+ row.MedioId + ')">Enviar</a></li>' +
                                 '</ul>' +
                                 '</div>';
                   }
                   return retorno;
               }
           },
		   { "data": "OrdenId", "class": "hidden" }
        ],
        "drawCallback": function (settings) {
            //despues de dibujar tabla
        },
        "order": [[0, "asc"]]
    });
}
