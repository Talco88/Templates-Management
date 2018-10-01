var signIn = {};
var Global_singIn_BaseHTMLData = "";

signIn.setPage = function () {
    if (Global_singIn_BaseHTMLData === "") {
        login.LoadLobbyPageRes(true);
    }
    else{
        login.onPagedRecived();
    }
}

signIn.onPagedRecived = function () {
    $("#MainAppWindow").html(Global_singIn_BaseHTMLData);
    let loginBtn = document.querySelector(".login-btn");
    loginBtn.onclick = login.onLoginClicked;
}

signIn.onLoginClicked = function () {
    // do something when btn is clicked
    let email = document.querySelector('#loginEmail');
    let pass = document.querySelector('#loginPass');
    Platform.LogIn(email.value, pass.value, login.onLoginResponce);
}

signIn.onLoginResponce = function (iData) {
    login.nevigateToSignUpPage(iData);
}

signIn.nevigateToSignUpPage = function (iServerReturn) {
    if (iServerReturn.Status != "OK") {
        // Go to LogIn page
        $.ajax({
            url: "/WebApp/signIn.html",
            dataType: 'text',
            success: function (data) {
                Global_singIn_BaseHTMLData = data;
                login.onPagedRecived();
            },
            error: function () {
                alert("error Loading SignIn Page");
            }
        });
    }

    if (iServerReturn.StatusCode != 0) {
        // Go to LogIn page
        $.ajax({
            url: "/WebApp/signIn.html",
            dataType: 'text',
            success: function (data) {
                Global_index_BaseHTMLData = data;
                login.onPagedRecived();
            },
            error: function () {
                alert("error Loading SignIn Page");
            }
        });
    }

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