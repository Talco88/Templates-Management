var login = {};
var Global_index_BaseHTMLData = "";

login.setPage = function () {
    if(Global_index_BaseHTMLData === ""){
        login.LoadLobbyPageRes(true);
    }
    else{
        login.onPagedRecived();
    }
}

login.onPagedRecived = function(){
    $("#MainAppWindow").html(Global_index_BaseHTMLData);
    let loginBtn = document.querySelector(".login-btn");
    loginBtn.onclick = login.onLoginClicked;
}

login.onLoginClicked = function(){
    // do something when btn is clicked
    let email = document.querySelector('#loginEmail');
    let pass = document.querySelector('#loginPass');
    Platform.LogIn(email.value, pass.value, login.onLoginResponce);
}

login.onLoginResponce = function (iData){
    login.nevigateToSignUpPage(iData);
}

login.nevigateToSignUpPage = function (iServerReturn) {
    if (iServerReturn.Status != "OK") {
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

login.example = function(){
    Platform.SearchTemplate("some Keys", login.exampleReturnFunction);
}

login.exampleReturnFunction = function (iServerReturn) {
    if (iServerReturn.Status != "OK"){
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

    if (iServerReturn.StatusCode != 0){
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

login.LoadLobbyPageRes = function (isSet) {
    $.ajax({
        url: "/WebApp/index.html",
        dataType: 'text',
        success: function (data) {
            Global_index_BaseHTMLData = data;
            login.onPagedRecived();
        },
        error: function () {
            alert("error Loading Lobby Page");
        }
    });
}