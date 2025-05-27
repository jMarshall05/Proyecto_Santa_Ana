new DataTable('#TablaDeUsuarios');

$(document).ready(function () {

    $(document).on('click','.btn-Detalles' ,function(event){
        var id = $(this).data('id')
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

	$(document).on('click', '.btn-Editar', function (event) {
		var id = $(this).data('id')
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











})