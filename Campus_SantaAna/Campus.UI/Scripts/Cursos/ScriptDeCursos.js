function AgregarCurso(event) {
    $.ajax({
        url: "/Cursos/AgregarCursoParcial",
        data: { },
        type: "GET",
        success: function (data) {
            $(".modal-body").html(data);
            $(".modal-title").html("Agregar Curso");
            $("#CursosModal").modal("show");
        },
        error: function (error) {
            console.log(error);
        }
    });
};
function DetallesCurso(event) {
    var id = $(this).data('id');
    $.ajax({
        url: "/Cursos/DetallesDeCursoParcial",
        data: {id:id},
        type: "GET",
        success: function (data) {
            $(".modal-body").html(data);
            $(".modal-title").html("Detalles Curso");
            $("#CursosModal").modal("show");
        },
        error: function (error) {
            console.log(error);
        }
    });
};

$(document).on('click', '.btn-Agregar-Curso', AgregarCurso)
$(document).on('click', '.btn-Detalles-Curso', DetallesCurso)