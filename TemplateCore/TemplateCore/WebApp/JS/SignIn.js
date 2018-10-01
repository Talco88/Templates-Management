var signIn = {};
var Global_singIn_BaseHTMLData = "";

signIn.setPage = function () {
    if (Global_singIn_BaseHTMLData === "") {
        signIn.LoadLobbyPageRes(true);
    }
    else{
        signIn.onPagedRecived();
    }
}

signIn.onPagedRecived = function () {
    $("#MainAppWindow").html(Global_singIn_BaseHTMLData);
    let loginBtn = document.querySelector(".login-btn");
    loginBtn.onclick = login.onLoginClicked;
}

signIn.LoadLobbyPageRes = function (isSet) {
    $.ajax({
        url: "/WebApp/HTML/signIn.html",
        dataType: 'text',
        success: function (data) {
            Global_singIn_BaseHTMLData = data;
            login.onPagedRecived();
        },
        error: function () {
            alert("error Loading Lobby Page");
        }
    });
}