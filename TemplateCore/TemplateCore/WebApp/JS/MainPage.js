var mainPage = {}
var Global_index_BaseHTMLData = "";

mainPage.setPage = function () {
    if (Global_index_BaseHTMLData === "") {
        mainPage.LoadLobbyPageRes(true);
    }
    else {
        mainPage.onPagedRecived();
    }
}

mainPage.onPagedRecived = function () {
    $("#MainAppWindow").html(Global_index_BaseHTMLData);

    // all the btn registered here


    //let loginBtn = document.querySelector(".login-btn");
    //loginBtn.onclick = mainPage.onLoginClicked;
}

mainPage.LoadLobbyPageRes = function (isSet) {
    $.ajax({
        url: "/WebApp/HTML/index.html",
        dataType: 'text',
        success: function (data) {
            Global_index_BaseHTMLData = data;
            mainPage.onPagedRecived();
        },
        error: function () {
            alert("error Loading Lobby Page");
        }
    });
}