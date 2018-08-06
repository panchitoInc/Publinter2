/******* css utils ********/

function ControlFlashColor(selector) {
    setTimeout(function () {
        $(selector).addClass("flashcolor");
    }, 1);
    $(selector).removeClass("flashcolor");
}

/************** AJAX  *****************/

//Funcion para hacer llamadas al server
//retorna el json generado por el controller
function AjaxPost(action, object, callfunctionsuccess, index) {
    if (action) {
        $.ajax({
            type: "POST",
            traditional: true,
            data: object,
            url: action,
            success: function (e) {
                callfunctionsuccess(e, index);
            }
        });
    }
}

function AjaxPostParam(action, object, callfunctionsuccess, index, param) {
    if (action) {
        $.ajax({
            type: "POST",
            traditional: true,
            data: object,
            url: action,
            success: function (e) {
                callfunctionsuccess(e, param);
            }
        });
    }
}

function AjaxGet(action, object, callfunctionsuccess, index) {
    if (action) {
        $.getJSON(action + "/" + object, function (data) {
            callfunctionsuccess(data, index);
        });
    }
}



/************** ARRAY  *****************/

//Obtengo el modelo desde la vista
function GetCurrentModel(name) {
    var model = $("#" + name).serialize();
    return model;
}

//Obtengo el nombre de la funcion
function GetFunctionName(object) {
    return object.toString().match(/^function\s*([^\s(]+)/)[1];
}

//obtengo un valor de un input formato importe javascript para hacer cuentas.
function GetValueFromInput(input) {
    if ($(input).val() != undefined) {        
        return parseFloat($(input).val().replace(".", "").replace(",", "."));
    }
    else {
        return parseFloat("0.00");
    }
}

//sumo los elementos de un array
function SumarArray(array) {
    return (array.length > 0) ? array.reduce(function (a, b) { return a + b; }).toString() : "0.00";
}

function SumarArrayFloat(array) {
    return (array.length > 0) ? array.reduce(function (a, b) { return a + b; },0) : 0;
}

//dado un importe string "1.000,00" a float java 1000.00
function ImporteStringTofloat(value) {
    return (value != undefined) ? parseFloat(value.replace(".", "").replace(",", ".")) : parseFloat("0.00"); //parseFloat(value); //
}

//evalua que solo se ingresen numeros 
function isNumber(evt) {
    evt = (evt) ? evt : window.event;
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    return true;
}

//paso de float java (1000.00) a importestring (1.000,00)
function floatToImporteString(value) {
    var val = parseFloat(value.toString().replace(",", "."));
    val = (val === undefined) ? 0 : val;
    return val.toFixed(2).toString().replace(".", ",");
    //return val.toFixed(2).toString().replace(".", ",").replace(/\B(?=(\d{3})+(?!\d))/g, ".");
}

//paso de float java (1000.00) a importestring (1.000,00)
function floatToStringDecimals(value, decimals) {
    
    var val = parseFloat(value.toString().replace(",", "."));
    val = ((val === undefined) || isNaN(val)) ? 0 : val;
    return val.toFixed(decimals).toString().replace(".", ",");
    //return val.toFixed(2).toString().replace(".", ",").replace(/\B(?=(\d{3})+(?!\d))/g, ".");
}

function numberThousandSeparator(value) {
    var number = value.split(",");
    number[0] = number[0].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");
    if (number[1] === undefined) {
        return number[0];
    }
    else {
        return number[0] + "," + number[1];
    }
}

//obtengo un array de valores a partir de los componentes de una clase
function GetArrayValues(classname) {
    var array = new Array();

    for (var i = 0; i < $("." + classname).length; i++) {
        array[i] = GetValueFromInput($("." + classname)[i]);        
    }

    return array;
}

//convierte una array a string y lo delimita por un nombre, 
//sirve para pasar una lista de int al controller.
function ArrayToParameter(ar, parameterName) {
    var stringarray = ar.toString();
    stringarray = stringarray.replace(/,/g, "&" + parameterName + "=");
    stringarray = "&" + parameterName + "=" + stringarray;
    return stringarray;
}

function SearchCodeInArray(myArray, code)
{
    var result = $.grep(myArray, function (e) { return e.Codigo == code; });

    if (result.length == 0) {
        // not found
    } else if (result.length == 1) {
        // access the foo property using result[0].foo
        return result[0];
    } else {
        // multiple items found
    }
}

function SearchIdInArray(myArray, Id)
{
    var result = $.grep(myArray, function (e) { return e.id == Id; });

    if (result.length == 0) {
        // not found
    } else if (result.length == 1) {
        // access the foo property using result[0].foo
        return result[0];
    } else {
        // multiple items found
    }
}

function PutFloatValueToInput(value, input, flash, decimals)
{
    
    $(input).attr('value', floatToStringDecimals(value, decimals));
    $(input).val(floatToStringDecimals(value, decimals));

    if (flash) {
        ControlFlashColor(input);
    }
}

function PutFloatValueToInputHidden(value, input, flash)
{
    $(input).attr('value', floatToStringDecimals(value, 8));
    $(input).val(floatToStringDecimals(value, 8));

    if (flash) {
        ControlFlashColor(input);
    }
}

/************** COOKIE *****************/

function getCookie(name) {
    var dc = document.cookie;
    var prefix = name + "=";
    var begin = dc.indexOf("; " + prefix);
    if (begin == -1) {
        begin = dc.indexOf(prefix);
        if (begin != 0) return null;
    }
    else {
        begin += 2;
        var end = document.cookie.indexOf(";", begin);
        if (end == -1) {
            end = dc.length;
        }
    }
    return unescape(dc.substring(begin + prefix.length, end));
}

function createCookie(name, value, days) {
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        var expires = "; expires=" + date.toGMTString();
    }
    else var expires = "";
    document.cookie = name + "=" + value + expires + "; path=/";
}

function eraseCookie(name) {
    createCookie(name, "", -1);
}

/***************************************/

///Scrollea hasta una posición de la pantalla según un ID
function goToByScroll(id) {   
    // Scroll
    $('html,body').animate({
        scrollTop: $("#" + id).offset().top
    },
        'slow');
}

///Escucha si hay autocompletar en un input en chrome 
$.fn.allchange = function (callback) {
    var me = this;
    var last = "";
    var infunc = function () {
        var text = $(me).val();
        if (text != last) {
            last = text;
            callback();
        }
        setTimeout(infunc, 100);
    }
    setTimeout(infunc, 100);
};

function formattedDate(date) {
    var d = new Date(date || Date.now()),
        day = '' + d.getDate(),
        month = '' + (d.getMonth() + 1),
        year = d.getFullYear();

    if (month.length < 2) month = '0' + month;
    if (day.length < 2) day = '0' + day;

    return [day, month, year].join('/');
}

String.prototype.replaceAll = function (search, replacement) {
    var target = this;
    return target.split(search).join(replacement);
};

Date.prototype.subtractDays = function (days) {
    var dat = new Date(this.valueOf());
    dat.setDate(dat.getDate() - days);
    return dat;
}

Array.prototype.unique = function (a) {
    return function () { return this.filter(a) }
}(function (a, b, c) {
    return c.indexOf(a, b + 1) < 0
});

//Detecta si es un browser mobile

function esMobile() {
    var esMobile = false; 
    
    if (/(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|ipad|iris|kindle|Android|Silk|lge |maemo|midp|mmp|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows (ce|phone)|xda|xiino/i.test(navigator.userAgent)
        || /1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-/i.test(navigator.userAgent.substr(0, 4))) isMobile = true;
    if (esMobile) {
        return true
    } else {
        return false
    }
}

//Detecta navegador y versión

function getBrowser() {
    var ua = navigator.userAgent, tem,
	M = ua.match(/(opera|chrome|safari|firefox|msie|trident(?=\/))\/?\s*(\d+)/i) || [];
    if (/trident/i.test(M[1])) {
        tem = /\brv[ :]+(\d+)/g.exec(ua) || [];
        return 'IE ' + (tem[1] || '');
    }
    if (M[1] === 'Chrome') {
        tem = ua.match(/\b(OPR|Edge)\/(\d+)/);
        if (tem != null) return tem.slice(1).join(' ').replace('OPR', 'Opera');
    }
    M = M[2] ? [M[1], M[2]] : [navigator.appName, navigator.appVersion, '-?'];
    if ((tem = ua.match(/version\/(\d+)/i)) != null)
        M.splice(1, 1, tem[1]);
    return M.join(' ');
}

//Scrollea según el container y el tamaño del bloque que se le pase

function ScrollNextBlock(container, nextBlockLine) {

    $(container).animate({ scrollTop: $(nextBlockLine).offset().top - $(container).offset().top + $(container).scrollTop() });
}


////////////////////////////////////////////
//NumeroALetras(numero, descripcionMoneda)//
////////////////////////////////////////////

function NumeroALetras(num, descripcionMoneda) {
    var data = {
        numero: num,
        enteros: Math.floor(num),
        centavos: (((Math.round(num * 100)) - (Math.floor(num) * 100))),
        letrasCentavos: '',
        letrasMonedaPlural: descripcionMoneda.toLowerCase(),//“PESOS”, 'Dólares', 'Bolívares', 'etcs'
        letrasMonedaSingular: descripcionMoneda.toLowerCase(), //“PESO”, 'Dólar', 'Bolivar', 'etc'

        letrasMonedaCentavoPlural: (descripcionMoneda.toLowerCase() == 'pesos') ? 'centésimos' : 'centavos',
        letrasMonedaCentavoSingular: 'centavo'
    };

    if (data.centavos > 0) {
        data.letrasCentavos = 'con ' + (function () {
            if (data.centavos == 1)
                return Millones(data.centavos) + ' ' + data.letrasMonedaCentavoSingular;
            else
                return Millones(data.centavos) + ' ' + data.letrasMonedaCentavoPlural;
        })();
    };

    if (data.enteros == 0)
        return 'cero ' + data.letrasMonedaPlural + ' ' + data.letrasCentavos;
    if (data.enteros == 1)
        return Millones(data.enteros) + ' ' + data.letrasMonedaSingular + ' ' + data.letrasCentavos;
    else
        return Millones(data.enteros) + ' ' + data.letrasMonedaPlural + ' ' + data.letrasCentavos;
}

function Unidades(num){

    switch(num)
    {
        case 1: return 'un';
        case 2: return 'dos';
        case 3: return 'tres';
        case 4: return 'cuatro';
        case 5: return 'cinco';
        case 6: return 'seis';
        case 7: return 'siete';
        case 8: return 'ocho';
        case 9: return 'nueve';
    }

    return '';
}//Unidades()

function Decenas(num){

    decena = Math.floor(num/10);
    unidad = num - (decena * 10);

    switch(decena)
    {
        case 1:
            switch(unidad)
            {
                case 0: return 'diez';
                case 1: return 'once';
                case 2: return 'doce';
                case 3: return 'trece';
                case 4: return 'catorce';
                case 5: return 'quince';
                default: return 'dieci' + Unidades(unidad);
            }
        case 2:
            switch(unidad)
            {
                case 0: return 'veinte';
                default: return 'veinti' + Unidades(unidad);
            }
        case 3: return DecenasY('treinta', unidad);
        case 4: return DecenasY('cuarenta', unidad);
        case 5: return DecenasY('cincuenta', unidad);
        case 6: return DecenasY('sesenta', unidad);
        case 7: return DecenasY('setenta', unidad);
        case 8: return DecenasY('ochenta', unidad);
        case 9: return DecenasY('noventa', unidad);
        case 0: return Unidades(unidad);
    }
}//Unidades()

function DecenasY(strSin, numUnidades) {
    if (numUnidades > 0)
        return strSin + ' y ' + Unidades(numUnidades)

    return strSin;
}//DecenasY()

function Centenas(num) {
    centenas = Math.floor(num / 100);
    decenas = num - (centenas * 100);

    switch(centenas)
    {
        case 1:
            if (decenas > 0)
                return 'ciento ' + Decenas(decenas);
            return 'cien';
        case 2: return 'doscientos ' + Decenas(decenas);
        case 3: return 'trescientos ' + Decenas(decenas);
        case 4: return 'cuatrocientos ' + Decenas(decenas);
        case 5: return 'quinientos ' + Decenas(decenas);
        case 6: return 'seiscientos ' + Decenas(decenas);
        case 7: return 'setecientos ' + Decenas(decenas);
        case 8: return 'ochocientos ' + Decenas(decenas);
        case 9: return 'novecientos ' + Decenas(decenas);
    }

    return Decenas(decenas);
}//Centenas()

function Seccion(num, divisor, strSingular, strPlural) {
    cientos = Math.floor(num / divisor)
    resto = num - (cientos * divisor)

    letras = '';

    if (cientos > 0)
        if (cientos > 1)
            letras = Centenas(cientos) + ' ' + strPlural;
        else
            letras = strSingular;

    if (resto > 0)
        letras += '';

    return letras;
}//Seccion()

function Miles(num) {
    divisor = 1000;
    cientos = Math.floor(num / divisor)
    resto = num - (cientos * divisor)

    strMiles = Seccion(num, divisor, 'un mil', 'mil');
    strCentenas = Centenas(resto);

    if(strMiles == '')
        return strCentenas;

    return strMiles + ' ' + strCentenas;
}//Miles()

function Millones(num) {
    divisor = 1000000;
    cientos = Math.floor(num / divisor)
    resto = num - (cientos * divisor)

    strMillones = Seccion(num, divisor, 'un millón', 'millones');
    strMiles = Miles(resto);

    if(strMillones == '')
        return strMiles;

    return strMillones + ' ' + strMiles;
}//Millones()
//Valida formato de mail con una regex
function isValidEmailAddress(emailAddress) {
    var pattern = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
    return pattern.test(emailAddress);
}
//objeto que devuelve formateado un numero con separacion de miles y decimales
var formatNumber = {
    
    separador: ".", // separador para los miles
    sepDecimal: ',', // separador para los decimales
    formatear: function (num) {
        num += '';
    
        //if (typeof (num) != "numeric")
        //{
        //    num = num.split('.')
        //    num = parseFloat(num);
        //}
        var splitStr = num.split('.');
        var splitLeft = splitStr[0];
        var splitRight = splitStr.length > 1 ? this.sepDecimal + splitStr[1] : '';
        var regx = /(\d+)(\d{3})/;
        while (regx.test(splitLeft)) {
            splitLeft = splitLeft.replace(regx, '$1' + this.separador + '$2');
        }
        
        if (splitRight == "") {
            splitRight = ",00"
        } else if (splitRight.indexOf(',')!= -1 && splitRight.length == 2)
            splitRight += "0";
        return this.simbol + splitLeft + splitRight;
    },
    new: function (num, simbol) {
        this.simbol = simbol || '';
        return this.formatear(num);
    }
}