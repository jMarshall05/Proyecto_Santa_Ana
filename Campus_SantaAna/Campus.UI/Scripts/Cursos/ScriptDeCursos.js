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

$(document).on('click', '.btn-Agregar-Curso', AgregarCurso)