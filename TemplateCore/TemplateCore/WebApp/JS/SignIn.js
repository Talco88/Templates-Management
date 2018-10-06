var signIn = {};
var Global_singIn_BaseHTMLData = "";

signIn.onPagedRecived = function () {
    $("#MainAppWindow").html(Global_index_BaseHTMLData);

    // all the btn registered here

    let registerBtn = document.querySelector(".register-btn");
    registerBtn.onclick = signIn.onRegisterClicked;

}

signIn.onRegisterClicked = function () {
    // do this when btn is clicked
    let name = document.querySelector('#name');
    let email = document.querySelector('#email');
    let pass = document.querySelector('#password');
    Platform.RegisterUser(name.value, email.value, pass.value, signIn.onLoginResponce);
}
signIn.onLoginResponce = function (iData) {
    signIn.nevigateFromSignUpPage(iData);
}

signIn.nevigateFromSignUpPage = function (iServerReturn) {
    if (iServerReturn.Status != "OK") {
        mainPage.setPage(); // Go to Main page
    }
    if (iServerReturn.StatusCode != 0) {
        mainPage.setPage(); // Go to Main page
    }
    else {
        mainPage.setPage(); // Go again to Main page
    }
}

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
    loginBtn.onclick = signIn.onLoginClicked;
}

signIn.LoadLobbyPageRes = function (isSet) {
    $.ajax({
        url: "/WebApp/HTML/signIn.html",
        dataType: 'text',
        success: function (data) {
            Global_singIn_BaseHTMLData = data;
            signIn.onPagedRecived();
        },
        error: function () {
            alert("error Loading Lobby Page");
        }
    });
}