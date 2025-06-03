$(document).ready(function () {
    // Función para activar modo edición
    $("#btnEditar").click(function () {
        // Convertir celdas a inputs
        $("#tablaDatos td.valor").each(function () {
            var campo = $(this).data("campo");
            var valor = $(this).text().trim();
            $(this).html(`<input type="text" name="${campo}" class="form-control" value="${valor}" />`);
        });

        // Agregar botón guardar
        if ($("#btnGuardar").length === 0) {
            $("#tablaDatos tbody").append(`
                <tr id="filaGuardar">
                    <td colspan="2" class="text-end">
                        <button type="button" id="btnGuardar" class="btn btn-success">Guardar cambios</button>
                        <button type="button" id="btnCancelar" class="btn btn-secondary ms-2">Cancelar</button>
                    </td>
                </tr>
            `);
        }

        $(this).prop("disabled", true);
    });

    // Función para guardar cambios con AJAX
    $(document).on("click", "#btnGuardar", function () {
        var datos = {
            Id: $("[data-id]").data("id"),
            Nombre: $("input[name='Nombre']").val(),
            Apellido: $("input[name='Apellido']").val(),
            Email: $("input[name='Email']").val()
        };

        $.ajax({
            url: $("#formEditar").attr("action"),
            type: "POST",
            data: datos,
            success: function (response) {
                // Actualizar las celdas con los nuevos valores
                $("input[name='Nombre']").parent().text(datos.Nombre);
                $("input[name='Apellido']").parent().text(datos.Apellido);
                $("input[name='Email']").parent().text(datos.Email);

                // Remover fila de botones y reactivar editar
                $("#filaGuardar").remove();
                $("#btnEditar").prop("disabled", false);

                // Mostrar mensaje de éxito
                alert("Datos guardados correctamente");
            },
            error: function () {
                alert("Error al guardar los datos");
            }
        });
    });

    // Función para cancelar edición
    $(document).on("click", "#btnCancelar", function () {
        location.reload(); // Recarga la página para restaurar valores originales
    });
});