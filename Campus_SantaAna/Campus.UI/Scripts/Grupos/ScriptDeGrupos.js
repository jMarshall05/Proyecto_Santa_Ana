$(document).ready(function () {
    // Inicializar DataTable principal
    $('#TablaDeGrupos').DataTable({
        responsive: true,
        language: {
            url: '//cdn.datatables.net/plug-ins/1.13.6/i18n/es-ES.json',
            search: "_INPUT_",
            searchPlaceholder: "Buscar grupos...",
            lengthMenu: "Mostrar _MENU_ registros por página",
            info: "Mostrando _START_ a _END_ de _TOTAL_ grupos",
            infoEmpty: "No hay grupos para mostrar",
            infoFiltered: "(filtrado de _MAX_ grupos totales)",
            paginate: {
                first: "Primera",
                last: "Última",
                next: "Siguiente",
                previous: "Anterior"
            }
        },
        dom: '<"top"<"d-flex justify-content-between align-items-center"lf>>rt<"bottom"ip><"clear">',
        columnDefs: [
            { orderable: false, targets: [4] }, // Columna de acciones
            { searchable: false, targets: [0, 4] }
        ],
        initComplete: function () {
            $('.dataTables_length').addClass('mb-3');
            $('.dataTables_length label').addClass('d-flex align-items-center');
            $('.dataTables_length select').addClass('form-select-sm');
            $('.dataTables_filter').addClass('mb-3');
            $('.dataTables_filter label').addClass('position-relative');
        }
    });

    // Inicializar tooltips
    function initTooltips() {
        // Destruir tooltips existentes antes de crear nuevos
        $('[data-bs-toggle="tooltip"]').tooltip('dispose');
        var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
        tooltipTriggerList.map(function (tooltipTriggerEl) {
            return new bootstrap.Tooltip(tooltipTriggerEl);
        });
    }
    initTooltips();

    // Efecto hover para filas
    $('.table-hover tbody tr').hover(
        function () { $(this).css('cursor', 'pointer'); },
        function () { $(this).css('cursor', 'default'); }
    );

    // Función para inicializar DataTable de usuarios (en modal)
    function initUsuariosDataTable() {
        if ($.fn.DataTable.isDataTable('#TablaDeUsuarios')) {
            $('#TablaDeUsuarios').DataTable().destroy();
        }

        $('#TablaDeUsuarios').DataTable({
            responsive: true,
            language: {
                url: '//cdn.datatables.net/plug-ins/1.13.6/i18n/es-ES.json',
                search: "_INPUT_",
                searchPlaceholder: "Buscar usuarios...",
                lengthMenu: "Mostrar _MENU_ registros por página",
                info: "Mostrando _START_ a _END_ de _TOTAL_ usuarios",
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
                { orderable: false, targets: [4] },
                { searchable: false, targets: [0, 4] }
            ],
            initComplete: function () {
                $('.dataTables_length').addClass('mb-3');
                $('.dataTables_length label').addClass('d-flex align-items-center');
                $('.dataTables_length select').addClass('form-select-sm');
                $('.dataTables_filter').addClass('mb-3');
                $('.dataTables_filter label').addClass('position-relative');
            }
        });
    }

    // Agregar Grupo
    function AgregarGrupo(event) {
        event.preventDefault();
        $.ajax({
            url: "/Grupos/AgregarGrupoParcial",
            data: {},
            type: "GET",
            success: function (data) {
                $(".modal-body-premium").html(data); // CORREGIDO: usar modal-body-premium
                $(".modal-title").html("Agregar Grupo");
                $("#GruposModal").modal("show");
                initTooltips();
            },
            error: function (error) {
                console.log("Error al cargar la vista de agregar grupo:", error);
            }
        });
    }
    $(document).on('click', '.btn-Agregar-Grupo', AgregarGrupo);

    // Detalles del Grupo
    function DetallesGrupo(event) {
        event.preventDefault();
        var id = $(this).data('id');
        console.log('Cargando detalles del grupo ID:', id);

        $.ajax({
            url: "/Grupos/DetallesDeGrupoParcial",
            data: { id: id },
            type: "GET",
            success: function (data) {
                $(".modal-body-premium").html(data); // CORREGIDO: usar modal-body-premium
                $(".modal-title").html("Detalles del Grupo");
                $("#GruposModal").modal("show");

                // Esperar un momento para que el DOM se actualice
                setTimeout(function () {
                    // Verificar si existe la tabla antes de inicializarla
                    if ($('#TablaDeUsuarios').length > 0) {
                        initUsuariosDataTable();
                    }
                    initTooltips();

                    // Efecto hover para filas del modal
                    $('.table-hover tbody tr').hover(
                        function () { $(this).css('cursor', 'pointer'); },
                        function () { $(this).css('cursor', 'default'); }
                    );
                }, 100);
            },
            error: function (error) {
                console.log("Error al cargar detalles del grupo:", error);
            }
        });
    }
    $(document).on('click', '.btn-Detalles-Grupo', DetallesGrupo);

    // Editar Grupo
    function EditarGrupo(event) {
        event.preventDefault();
        var id = $(this).data('id');
        console.log('Cargando vista de edición para grupo ID:', id);

        $.ajax({
            url: "/Grupos/EditarGrupoParcial",
            data: { id: id },
            type: "GET",
            success: function (data) {
                $(".modal-body-premium").html(data); 
                $(".modal-title").html("Modificar Grupo");
                $("#GruposModal").modal("show");
                initTooltips();
            },
            error: function (error) {
                console.log("Error al cargar la vista de edición:", error);
            }
        });
    }
    $(document).on('click', '.btn-Editar-Grupo', EditarGrupo);

    // Limpiar modal al cerrarse
    $('#GruposModal').on('hidden.bs.modal', function () {
        $(".modal-body-premium").html('');
        $(".modal-title").html('Gestión de Grupo');

        // Destruir DataTable de usuarios si existe
        if ($.fn.DataTable.isDataTable('#TablaDeUsuarios')) {
            $('#TablaDeUsuarios').DataTable().destroy();
        }
    });

    // Botón de filtros
    const filterBtn = document.getElementById('filterBtn');
    if (filterBtn) {
        filterBtn.addEventListener('click', function () {
            alert('Funcionalidad de filtros será implementada próximamente');
        });
    }
});