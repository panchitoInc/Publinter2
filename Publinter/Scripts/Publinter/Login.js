var interval = 0;
var progress = 0;
var bttn;
var instancia;
function ValidarLogin() {

    var user = $("#Usuario").val();
    var pass = $("#Clave").val();
    if (user != "" && pass != "") {

        $("#Usuario").css("border-bottom", "2px solid white");
        $("#Clave").css("border-bottom", "2px solid white");
        AjaxPost("../Account/Login", GetCurrentModel("formLogin"), ValidarLoginSuccess, 0);
    } else {
        clearInterval(interval);
        instancia._stop(-1);
        UIProgressButton.prototype.stop;
        focusOutUser($("#Usuario").val());
        focusOutPass($("#Clave").val());

    }
    }
        
function ValidarLoginSuccess(data, index) {
    if (data.access) {
        clearInterval(interval);
        instancia._stop(1);
        
    } else {
        clearInterval(interval);
        instancia._stop(-1);
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