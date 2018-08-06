

function CrearUsuario() {
    AjaxPost("../Usuario/Create", GetCurrentModel("UsuarioCreateForm"), CreateUsuarioSuccess, 0);
}

function CreateUsuarioSuccess(data,index) {
    if (data && parseInt(data) > 0) {

        $("#Mensaje").html("Usuario creado.").fadeIn();
        $("#UsuarioCreateForm input").val('');
        $("#UsuarioCreateForm select").val(4);
    } else {
        $("#Mensaje").html("Error al crear el usuario.").fadeIn();
    }
    setTimeout(function () {
        $("#Mensaje").fadeOut(600);
    }, 1500);
}