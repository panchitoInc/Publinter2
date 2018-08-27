function validarLargo(idOobj, largo) {
    var texto;

    if (typeof (idOobj) == "string")//si es un id
        idOobj = $(idOobj);//obtiene objeto con ese id

    texto = $(idOobj).val();
    if (texto.length > largo) {
        $(idOobj).removeClass('danger');
        $(idOobj).addClass('success');
        return true;
    }
    else {
        $(idOobj).removeClass('success');
        $(idOobj).addClass('danger');
        return false;
    }
}

//valida que el numero de rut sea real (calculado)
function validarRUT(rut) {
    if (rut.length != 12) {
        return false;
    }
    if (!/^([0-9])*$/.test(rut)) {
        return false;
    }
    var dc = rut.substr(11, 1);
    var rut = rut.substr(0, 11);
    var total = 0;
    var factor = 2;
    for (i = 10; i >= 0; i--) {
        total += (factor * rut.substr(i, 1));
        factor = (factor == 9) ? 2 : ++factor;
    }
    var dv = 11 - (total % 11);
    if (dv == 11) {
        dv = 0;
    } else if (dv == 10) {
        dv = 1;
    }
    if (dv == dc) {
        return true;
    }
    return false;
}

//Valida que el numero de cedula sea real (calculado)
function validarCedula(ci) {
    if (ci != "") {
        var arrCoefs = [2, 9, 8, 7, 6, 3, 4, 1];
        var suma = 0;
        var difCoef = parseInt(arrCoefs.length - ci.length);
        for (var i = ci.length - 1; i > -1; i--) {
            var dig = ci.substring(i, i + 1);
            var digInt = parseInt(dig);
            var coef = arrCoefs[i + difCoef];
            suma = suma + digInt * coef;
        }
        if ((suma % 10) == 0) {
            return true;
        } else {
            return false;

        }
    }
    return false;

}

//Valida formato de mail con una regex
function isValidEmailAddress(emailAddress) {
    var pattern = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
    return pattern.test(emailAddress);
}

//valida que el email tenga el formato correcto
function validarEmail(obj) {
    
    expr = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    var email = $(obj).val()
    if (email != "") {
        if (expr.test(email)) {
            $(obj).removeClass('danger');
            $(obj).addClass('success');
            return true;
        }

        activarMensaje(ID_NOTIFICACIONMENSAJE, "El email tiene un formato invalido", "uk-notify-message-warning");
        $(obj).removeClass('success');
        $(obj).addClass('danger');
        return false;

    }

}
    //valida que el celular sea numerico, de largo 9 y sin espacios
    function validarCelular(celular){
        var expresionRegular1=/^([0-9]+){9}$/;//<--- con esto vamos a validar el numero
        var expresionRegular2=/\s/;//<--- con esto vamos a validar que no tenga espacios en blanco
 
        if (celular.value == '')
            return false;
        else if (expresionRegular2.test(celular))
            return false
        else if (!expresionRegular1.test(celular))
            return false
        else
            return true;
    }

    //Validacion de DNI
    function validarDNI(dni) {
        return true;
    }

    function validarLargoRut(objeto) {
        var texto = $(objeto).val();
        if (texto.length != 12) {
            $(objeto).removeClass('success');
            $(objeto).addClass('danger');
            return false;
        } else {
            $(objeto).removeClass('danger');
            $(objeto).addClass('success');
            return true;
        }
    }


    function quitaAcentosYMayusculas(str) {
        for (var i = 0; i < str.length; i++) {
            //Sustituye "á é í ó ú" 
            if (str.charAt(i) == "á") str = str.replace(/á/, "a");
            if (str.charAt(i) == "é") str = str.replace(/é/, "e");
            if (str.charAt(i) == "í") str = str.replace(/í/, "i");
            if (str.charAt(i) == "ó") str = str.replace(/ó/, "o");
            if (str.charAt(i) == "ú") str = str.replace(/ú/, "u");
        }
        str =str.toLowerCase();
        return str;
    }