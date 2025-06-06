$(document).ready(function () {
    // Inicializar DataTable
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
            { orderable: false, targets: [5] }, // Columna de acciones no ordenable
            { searchable: false, targets: [5] } // Columna de acciones no buscable
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

    // Inicializar tooltips para Bootstrap 5
    var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
    var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl, {
            trigger: 'hover',
            placement: 'top'
        });
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

    // Event handler para el botón Detalles
    $(document).on('click', '.btn-Detalles', function (event) {
        var id = $(this).data('id');
        $.ajax({
            url: "/Usuarios/DetallesDeUsuarioParcial",
            data: { id: id },
            type: "GET",
            success: function (data) {
                $(".modal-body").html(data);
                $(".modal-title").html("Detalles De Usuario");
                $("#UsuariosModal").modal("show");
            },
            error: function (error) {
                console.log(error);
            }
        });
    });

    // Event handler para el botón Editar
    $(document).on('click', '.btn-Editar', function (event) {
        var id = $(this).data('id');
        $.ajax({
            url: "/Usuarios/EditarUsuarioParcial",
            data: { id: id },
            type: "GET",
            success: function (data) {
                $(".modal-body").html(data);
                $(".modal-title").html("Editar Usuario");
                $("#UsuariosModal").modal("show");
            },
            error: function (error) {
                console.log(error);
            }
        });
    });
});