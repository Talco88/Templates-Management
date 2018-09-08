var login = {};
var Global_LoginPage_BaseHTMLData = "";

login.setPage = function () {
    if(Global_LoginPage_BaseHTMLData === ""){
        login.LoadLobbyPageRes(true);
    }
    else{
        login.onPagedRecived();
    }
}

login.onPagedRecived = function(){
    $("#MainAppWindow").html(Global_LoginPage_BaseHTMLData);
    let loginBtn = document.querySelector(".login-btn");
    loginBtn.onclick = login.onLoginClicked;
}

login.onLoginClicked = function(){
    // do something when btn is clicked
    Platform.login("username", "passworddddd", login.onLoginResponce);
}

login.onLoginResponce = function (iData){
    console.log(iData);
}

login.example = function(){
    Platform.SearchTemplate("some Keys", login.exampleReturnFunction);
}

login.exampleReturnFunction = function(iServerReturn){
    if (iServerReturn.Status != "OK"){
        // Error?
    }

    if (iServerReturn.StatusCode != 0){
        // Error?
    }

}

login.LoadLobbyPageRes = function (isSet) {
    $.ajax({
        url: "/WebApp/HTML/LoginPage.html",
        dataType: 'text',
        success: function (data) {
            Global_LoginPage_BaseHTMLData = data;
            login.onPagedRecived();
        },
        error: function () {
            alert("error Loading Lobby Page");
        }
    });
}