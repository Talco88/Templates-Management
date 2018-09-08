var login = {};
var Global_LoginPage_BaseHTMLData = "";

login.setPage = function () {
    login.LoadLobbyPageRes(true);
    //$("#MainAppWindow").html(Global_LoginPage_BaseHTMLData);
}



login.LoadLobbyPageRes = function (isSet) {
    $.ajax({
        url: "/WebApp/HTML/LoginPage.html",
        dataType: 'text',
        success: function (data) {
            Global_LoginPage_BaseHTMLData = data;
            if (isSet) {
                $("#MainAppWindow").html(data);
            }
        },
        error: function () {
            alert("error Loading Lobby Page");
        }
    });
}