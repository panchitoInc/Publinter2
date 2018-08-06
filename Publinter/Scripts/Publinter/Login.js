function ValidarUseAndPass() {
    debugger;
    //function AjaxPost(action, object, callfunctionsuccess, index) {
    var model = $("#loginForm").serialize();
    AjaxPost("Account/Login", model, ValidarUseAndPassSuccess, 0);
}
function ValidarUseAndPassSuccess(data,Sindex) {
alert("Ok")
}