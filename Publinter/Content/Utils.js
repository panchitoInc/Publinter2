
//sumo los elementos de un array
function SumarArray(array) {
    return (array.length > 0) ? array.reduce(function (a, b) { return a + b; }).toString() : "0.00";
}

//dado un importe string "1.000,00" a float java 1000.00
function ImporteStringTofloat(value) {
    return (value != undefined) ? parseFloat(value.replace(".", "").replace(",", ".")) : parseFloat("0.00"); //parseFloat(value); //
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

function formattedDate(date) {
    var d = new Date(date || Date.now()),
        day = '' + d.getDate(),
        month = '' + (d.getMonth() + 1),
        year = d.getFullYear();

    if (month.length < 2) month = '0' + month;
    if (day.length < 2) day = '0' + day;

    return [day, month, year].join('/');
}