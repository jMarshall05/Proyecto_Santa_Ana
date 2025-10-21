function AgregarCurso(event) {
    $.ajax({
        url: "/Cursos/AgregarCursoParcial",
        data: {},
        type: "GET",
        success: function (data) {
            $(".modal-body-premium").html(data);
            $(".modal-title").html("Agregar Curso");
            $("#CursosModal").modal("show");
        },
        error: function (error) {
            console.log(error);
        }
    });
};

$(document).on('click', '.btn-Agregar-Curso', AgregarCurso)

function GenerarReporte(event) {
    $.ajax({
        type: "GET",
        success: function (data) {
            let modalDialog = $("#CursosModal .modal-dialog");
            modalDialog.removeClass("modal-xl");
            modalDialog.addClass("modal-sm");

            $(".modal-body-premium").html(`
                <div class="text-center">
                    <a href="/Cursos/GenerarReportePDF">
                        <img src="/Cursos/GenerarReporteQR" class="img-fluid rounded shadow" alt="Código QR del reporte" style="width:150px; height: 150px; object-fit: contain;" />
                        
                    </a>
                    <p >Haz clic en el QR o escanealo descargar el reporte</p>
                </div>
            `);
            $(".modal-title").html("Generar Reporte");
            $(".modal-footer-premium").html();
            $("#CursosModal").modal("show");
        },
        error: function (error) {
            console.log(error);
        }
    });
};

$(document).on('click', '.btn-GenerarReporte', GenerarReporte)

