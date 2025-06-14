$(document).ready(function () {
    // Inicializar DataTable
    $('#TablaDeGrupos').DataTable({
        responsive: true,
        language: {
            url: '//cdn.datatables.net/plug-ins/1.13.6/i18n/es-ES.json',
            search: "_INPUT_",
            searchPlaceholder: "Buscar usuarios...", // Cambiado de "tareas" a "usuarios"
            lengthMenu: "Mostrar _MENU_ registros por página",
            info: "Mostrando _START_ a _END_ de _TOTAL_ usuarios", // Cambiado de "tareas" a "usuarios"
            infoEmpty: "No hay usuarios para mostrar",
            infoFiltered: "(filtrado de _MAX_ usuarios totales)",
            paginate: {
                first: "Primera",
                last: "Última",
                next: "Siguiente",
                previous: "Anterior"
            }
        },
        dom: '<"top"<"d-flex justify-content-between align-items-center"lf>>rt<"bottom"ip><"clear">',
        columnDefs: [
            { orderable: false, targets: [3] }, // Cambiado de [6] a [5] porque tienes 6 columnas (índice 0-5)
            { searchable: false, targets: [0, 3] } // Ajustado para las columnas correctas
        ],
        initComplete: function () {
            // Personalizar el dropdown de cantidad de registros
            $('.dataTables_length').addClass('mb-3');
            $('.dataTables_length label').addClass('d-flex align-items-center');
            $('.dataTables_length select').addClass('form-select-sm');

            // Personalizar la barra de búsqueda
            $('.dataTables_filter').addClass('mb-3');
            $('.dataTables_filter label').addClass('position-relative');
        }
    });


    var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
    var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl);
    });

    // Efecto hover para filas
    $('.table-hover tbody tr').hover(
        function () {
            $(this).css('cursor', 'pointer');
        },
        function () {
            $(this).css('cursor', 'default');
        }
    );

    function AgregarGrupo (event) {
        var id = $(this).data('id');
        $.ajax({
            url: "/Grupos/AgregarGrupoParcial",
            data: { id: id },
            type: "GET",
            success: function (data) {
                $(".modal-body").html(data);
                $(".modal-title").html("Agregar Grupo");
                $("#GruposModal").modal("show");

            },
            error: function (error) {
                console.log(error);
            }
        });
    };
    $(document).on('click', '.btn-Agregar-Grupo', AgregarGrupo) 



  function DetallesGrupo(event) {
        var id = $(this).data('id');
        $.ajax({
            url: "/Grupos/DetallesDeGrupoParcial",
            data: { id: id },
            type: "GET",
            success: function (data) {

                $(".modal-body").html(data);
                $(".modal-title").html("Detalles del Grupo");
                $("#GruposModal").modal("show");
                $('#TablaDeUsuarios').DataTable({
                    responsive: true,
                    language: {
                        url: '//cdn.datatables.net/plug-ins/1.13.6/i18n/es-ES.json',
                        search: "_INPUT_",
                        searchPlaceholder: "Buscar usuarios...", // Cambiado de "tareas" a "usuarios"
                        lengthMenu: "Mostrar _MENU_ registros por página",
                        info: "Mostrando _START_ a _END_ de _TOTAL_ usuarios", // Cambiado de "tareas" a "usuarios"
                        infoEmpty: "No hay usuarios para mostrar",
                        infoFiltered: "(filtrado de _MAX_ usuarios totales)",
                        paginate: {
                            first: "Primera",
                            last: "Última",
                            next: "Siguiente",
                            previous: "Anterior"
                        }
                    },
                    dom: '<"top"<"d-flex justify-content-between align-items-center"lf>>rt<"bottom"ip><"clear">',
                    columnDefs: [
                        { orderable: false, targets: [4] }, // Cambiado de [6] a [5] porque tienes 6 columnas (índice 0-5)
                        { searchable: false, targets: [0, 4] } // Ajustado para las columnas correctas
                    ],
                    initComplete: function () {
                        // Personalizar el dropdown de cantidad de registros
                        $('.dataTables_length').addClass('mb-3');
                        $('.dataTables_length label').addClass('d-flex align-items-center');
                        $('.dataTables_length select').addClass('form-select-sm');

                        // Personalizar la barra de búsqueda
                        $('.dataTables_filter').addClass('mb-3');
                        $('.dataTables_filter label').addClass('position-relative');
                    }
                });

                var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
                var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
                    return new bootstrap.Tooltip(tooltipTriggerEl);
                });

                // Efecto hover para filas
                $('.table-hover tbody tr').hover(
                    function () {
                        $(this).css('cursor', 'pointer');
                    },
                    function () {
                        $(this).css('cursor', 'default');
                    }
                );


            },
            error: function (error) {
                console.log(error);
            }
        });
    };
$(document).on('click', '.btn-Detalles-Grupo', DetallesGrupo) 



    function EditarGrupo(event) {
        var id = $(this).data('id');
        $.ajax({
            url: "/Grupos/EditarGrupoParcial",
            data: { id: id },
            type: "GET",
            success: function (data) {
                $(".modal-body").html(data);
                $(".modal-title").html("Modificar Grupo");
                $("#GruposModal").modal("show");

            },
            error: function (error) {
                console.log(error);
            }
        });
    };
 $(document).on('click', '.btn-Editar-Grupo', EditarGrupo) 












});
