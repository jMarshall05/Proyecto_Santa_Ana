$(document).ready(function () {
    // Inicializar DataTable con configuración mejorada
    $('#tareasTable').DataTable({
        responsive: true,
        language: {
            url: '//cdn.datatables.net/plug-ins/1.13.6/i18n/es-ES.json',
            search: "_INPUT_",
            searchPlaceholder: "Buscar tareas...",
            lengthMenu: "Mostrar _MENU_ registros por página",
            info: "Mostrando _START_ a _END_ de _TOTAL_ tareas",
            infoEmpty: "No hay tareas para mostrar",
            infoFiltered: "(filtrado de _MAX_ tareas totales)",
            paginate: {
                first: "Primera",
                last: "Última",
                next: "Siguiente",
                previous: "Anterior"
            }
        },
        dom: '<"top"<"d-flex justify-content-between align-items-center"lf>>rt<"bottom"ip><"clear">',
        columnDefs: [
            { orderable: false, targets: [6] },
            { searchable: false, targets: [0, 4, 6] }
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

    // Inicializar tooltips
    $('[data-bs-toggle="tooltip"]').tooltip({
        trigger: 'hover',
        placement: 'top'
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
});