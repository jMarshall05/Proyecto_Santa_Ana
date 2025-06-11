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
            { orderable: false, targets: [5] }, // Cambiado de [6] a [5] porque tienes 6 columnas (índice 0-5)
            { searchable: false, targets: [0, 4, 5] } // Ajustado para las columnas correctas
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

    // Event handler para el botón Detalles
    function Detalles(event) {
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
    };

    $(document).on('click', '.btn-Detalles', Detalles);


    // Event handler para el botón Editar
    function EditarUsuario(event) {
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
    };
        $(document).on('click', '.btn-Editar', EditarUsuario)
});