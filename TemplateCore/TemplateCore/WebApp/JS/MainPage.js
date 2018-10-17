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

    let loginBtn = document.querySelector(".login-btn");
    loginBtn.onclick = mainPage.onLoginClicked;

    let signinBtn = document.querySelector(".signin-btn");
    signinBtn.onclick = mainPage.onSignUpClicked;

    let birthdayBtn = document.querySelector("#birthdayBtn");
    birthdayBtn.onclick = mainPage.onCategoryClicked;
}

mainPage.onLoginClicked = function () {
    // do this when btn is clicked
    let email = document.querySelector('#loginEmail');
    let pass = document.querySelector('#loginPass');
    Platform.LogIn(email.value, pass.value, mainPage.onLoginResponce);
}

mainPage.onSignUpClicked = function () {
    // do this when btn is clicked
    signIn.setPage();
}

mainPage.onLoginResponce = function (iData) {
    mainPage.nevigateToSignUpPage(iData);
    //let respoDiv = document.querySelector('#write the div name or class');
    //respoDiv.innerText = iData.RetObject;
}

mainPage.nevigateToSignUpPage = function (iServerReturn) {
    if (iServerReturn.Status != "OK") {
        signIn.setPage(); // Go to LogIn page
    }
    if (iServerReturn.StatusCode != 0) {
        signIn.setPage(); // Go to LogIn page
    }
    else {
        mainPage.setPage(); // Go again to Main page
    }
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

mainPage.onCategoryClicked = function () {
    mainPage.IsLoggedIn;
}

mainPage.IsLoggedIn = function () {
    if (mainPage.isLoggedinParam === undefined) {
        Platform.IsLogIn(mainPage.onIsloginCallback);
    }
    else {
        if (mainPage.isLoggedinParam) {
            birthdayCategory.setPage(); // Navigate to category
        }
        else {
            signIn.setPage(); // Go to LogIn page
        }
    }
}

mainPage.onLogOutClicked = function (isSet) {
    Platform.LogOut();
}

mainPage.onIsloginCallback = function (iResponse) {
    mainPage.isLoggedinParam = iResponse.RetObject;
    mainPage.IsLoggedIn();
}