

function ValidarLogin() {

    var user = $("#Usuario").val();
    var pass = $("#Clave").val();
    if (user != "" && pass != "") {

        $("#Usuario").css("border-bottom", "2px solid white");
        $("#Clave").css("border-bottom", "2px solid white");
        AjaxPost("../Account/Login", GetCurrentModel("formLogin"), ValidarLoginSuccess, 0);
    } else {
        focusOutUser($("#Usuario").val());
        focusOutPass($("#Clave").val());
    }
    }
        
function ValidarLoginSuccess(data, index) {
    if (data.access) {
        window.location.href = "../Orden/Create";
    } else {
        $("#error").fadeIn();
    }

}

function focusOutUser(value) {
    if (value != "") {
        $("#Usuario").css("border-bottom", "2px solid #76bb65");    
        return true;
    }
    
    $("#Usuario").css("border-bottom", "2px solid #de0900");
    return false;
}

function focusOutPass(value) {
    if (value != "") {
        $("#Clave").css("border-bottom", "2px solid #76bb65");
        return true;
    }
    $("#Clave").css("border-bottom", "2px solid #de0900");
    return false;
}