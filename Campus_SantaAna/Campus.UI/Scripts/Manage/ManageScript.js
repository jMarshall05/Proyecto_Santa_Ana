$(function () {

    // Activar edición
    $("#btnEditar").on("click", function () {

        // Convertir celdas simples a inputs
        $("#tablaDatos td.valor").each(function () {
            var campo = $(this).data("campo");
            if (!campo || campo === "Telefonos") return;
            var valor = $(this).text().trim();
            $(this).html(`<input type="text" name="${campo}" class="form-control" value="${valor}" />`);
        });

        // Manejar teléfonos
        var telefonoTd = $("#tablaDatos td[data-campo='Telefonos']");
        var telefonos = [];

        // Extraer datos desde los div.telefono-item existentes
        telefonoTd.find('div.telefono-item').each(function (i) {
            var $div = $(this);
            var id = $div.data('id') || 0;
            var rawText = $div.clone().children().remove().end().text().trim();
            var m = rawText.match(/\(([^)]+)\)\s*([0-9\-]+):\s*(.*)/);
            var codigo = m ? m[1].replace(/\+/g, '').trim() : '';
            var numero = m ? m[2].replace(/-/g, '').trim() : '';
            var tipo = m ? m[3].trim() : '';
            telefonos.push({ id: id, codigo: codigo, numero: numero, tipo: tipo });
        });

        // Si no había teléfonos, crear uno vacío
        if (telefonos.length === 0) {
            telefonos.push({ id: 0, codigo: "", numero: "", tipo: "" });
        }

        // Generar HTML de edición
        var html = '<div id="telefonosContainer">';
        telefonos.forEach(function (t, i) {
            html += `
            <div class="row mb-2 telefono-edit-row" data-index="${i}">
                <input type="hidden" class="tel-id" data-prop="Id" value="${t.id}" />
                <div class="col-md-3">
                    <input type="text" class="form-control tel-codigo" data-prop="Codigo"
                        value="${t.codigo}" maxlength="3" placeholder="Código (ej: 506)"
                        oninput="this.value=this.value.replace(/[^0-9]/g,'').substring(0,3)" />
                </div>
                <div class="col-md-5">
                    <input type="text" class="form-control tel-numero" data-prop="Telefono"
                        value="${t.numero}" minlength="8" maxlength="8" placeholder="Número"
                        oninput="this.value=this.value.replace(/[^0-9]/g,'').substring(0,8)" />
                </div>
                <div class="col-md-3">
                    <select class="form-control tel-tipo" data-prop="Tipo">
                        <option value="">Seleccione</option>
                        <option value="Personal" ${t.tipo == "Personal" ? "selected" : ""}>Personal</option>
                        <option value="Trabajo" ${t.tipo == "Trabajo" ? "selected" : ""}>Trabajo</option>
                        <option value="Hogar" ${t.tipo == "Hogar" ? "selected" : ""}>Hogar</option>
                        <option value="Encargado" ${t.tipo == "Encargado" ? "selected" : ""}>Encargado</option>
                        <option value="Otro" ${t.tipo == "Otro" ? "selected" : ""}>Otro</option>
                    </select>
                </div>
            </div>`;
        });
        html += '</div>';

        // Botón para agregar teléfono nuevo
        html += `<div class="mt-2">
                    <button type="button" id="addTelefonoEdit" class="btn btn-sm btn-primary">Agregar Teléfono</button>
                 </div>`;

        telefonoTd.html(html);

        // Agregar fila de botones
        if ($("#filaGuardar").length === 0) {
            $("#tablaDatos tbody").append(`
                <tr id="filaGuardar">
                    <td colspan="2" class="text-end">
                        <button type="button" id="btnGuardar" class="btn btn-success">Guardar cambios</button>
                        <button type="button" id="btnCancelar" class="btn btn-secondary ms-2">Cancelar</button>
                    </td>
                </tr>`);
        }

        $(this).prop("disabled", true);
    });

    // Agregar un nuevo teléfono
    $(document).on("click", "#addTelefonoEdit", function (e) {
        e.preventDefault();
        var container = $("#telefonosContainer");
        var index = container.find('.telefono-edit-row').length;

        var newRow = `
        <div class="row mb-2 telefono-edit-row" data-index="${index}">
            <input type="hidden" class="tel-id" data-prop="Id" value="0" />
            <div class="col-md-3">
                <input type="text" class="form-control tel-codigo" data-prop="Codigo"
                    maxlength="3" placeholder="Código"
                    oninput="this.value=this.value.replace(/[^0-9]/g,'').substring(0,3)" />
            </div>
            <div class="col-md-5">
                <input type="text" class="form-control tel-numero" data-prop="Telefono"
                    minlength="8" maxlength="8" placeholder="Número"
                    oninput="this.value=this.value.replace(/[^0-9]/g,'').substring(0,8)" />
            </div>
            <div class="col-md-3">
                <select class="form-control tel-tipo" data-prop="Tipo">
                    <option value="">Seleccione</option>
                    <option value="Personal">Personal</option>
                    <option value="Trabajo">Trabajo</option>
                    <option value="Hogar">Hogar</option>
                    <option value="Encargado">Encargado</option>
                    <option value="Otro">Otro</option>
                </select>
            </div>
        </div>`;
        container.append(newRow);
    });

    // Manejar el click en Guardar
    $(document).on("click", "#btnGuardar", function (e) {
        e.preventDefault();

        console.log("=== PREPARANDO ENVÍO ===");

        // Obtener todos los teléfonos
        var container = $("#telefonosContainer");
        var rows = container.find('.telefono-edit-row');

        console.log("Filas de teléfono encontradas:", rows.length);

        // Construir el string de datos manualmente (SIN URL encoding de corchetes)
        var params = [];

        // Agregar campos simples
        params.push('Id=' + encodeURIComponent($('input[name="Id"]').val()));
        params.push('Nombre=' + encodeURIComponent($('input[name="Nombre"]').val()));
        params.push('Apellido=' + encodeURIComponent($('input[name="Apellido"]').val()));

        // Agregar teléfonos
        rows.each(function (index) {
            var $row = $(this);

            var id = $row.find('.tel-id').val() || "0";
            var codigo = $row.find('.tel-codigo').val() || "0";
            var telefono = $row.find('.tel-numero').val() || "0";
            var tipo = $row.find('.tel-tipo').val() || "";

            // Limpiar cualquier carácter no numérico
            codigo = codigo.replace(/[^0-9]/g, '') || "0";
            telefono = telefono.replace(/[^0-9]/g, '') || "0";

            console.log(`Teléfono ${index}:`, { id, codigo, telefono, tipo });

            // IMPORTANTE: NO codificar los corchetes, solo los valores
            params.push('Telefonos[' + index + '].Id=' + encodeURIComponent(id));
            params.push('Telefonos[' + index + '].Codigo=' + encodeURIComponent(codigo));
            params.push('Telefonos[' + index + '].Telefono=' + encodeURIComponent(telefono));
            params.push('Telefonos[' + index + '].Tipo=' + encodeURIComponent(tipo));
            params.push('Telefonos[' + index + '].Estado=' + encodeURIComponent('true'));
        });

        var dataString = params.join('&');

        console.log("=== ENVIANDO CON AJAX ===");
        console.log("Data string:", dataString);

        // Obtener el token antiforgery si existe
        var token = $('input[name="__RequestVerificationToken"]').val();
        if (token) {
            dataString += '&__RequestVerificationToken=' + encodeURIComponent(token);
        }

        // Enviar con AJAX usando application/x-www-form-urlencoded
        $.ajax({
            url: $("#formEditar").attr('action'),
            type: 'POST',
            data: dataString,
            contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
            dataType: 'json',
            success: function (response) {
                console.log("Respuesta del servidor:", response);
                if (response.success) {
                    alert(response.message || "Datos actualizados correctamente");
                    var redirectUrl = $("#formEditar").data('redirect-url');
                    if (redirectUrl) {
                        window.location.href = redirectUrl;
                    } else {
                        location.reload();
                    }
                } else {
                    alert(response.message || "Error al guardar los cambios");
                }
            },
            error: function (xhr, status, error) {
                console.error("Error:", error);
                console.error("Status:", status);
                console.error("Response:", xhr.responseText);
                alert("Error al guardar los cambios. Ver consola para detalles.");
            }
        });
    });

    // Cancelar edición
    $(document).on("click", "#btnCancelar", function () {
        location.reload();
    });

});