var mainPage = {}
var Global_index_BaseHTMLData = "";

mainPage.setPage = function () {
    mainPage.loggedInUser = "";
    if (Global_index_BaseHTMLData === "") {
        mainPage.LoadLobbyPageRes(true);
    }
    else {
        mainPage.onPagedRecived();
    }
}

mainPage.SetUserInfo = function (iUserName) {
    if (iUserName != "") {
        mainPage.loggedInUser = iUserName;
    }

    if (mainPage.loggedInUser === "" && iUserName === "") {
        //show login button, hide logout button:
        document.getElementById("logout_button").style.display = "none";
        document.getElementById("login_button").style.display = "block";

        // define user as guest:
        document.getElementById("logged_in_username_text_field").textContent = "Guest";

    } else {
        //document.getElementById("logged_in_username_text_field").textContent = loggedInUser.name;
        document.getElementById("logged_in_username_text_field").textContent = iUserName;

        //show logout button, hide login button:
        document.getElementById("logout_button").style.display = "block";
        document.getElementById("login_button").style.display = "none";

        //var userRole = loggedInUser.role;
        //switch(userRole){
        //    case "mall_manager":
        //    case "store_manager":
        //        //hide shopping chart:
        //        document.getElementById("shopping_cart").style.display="none";

        //        //hide search gifts:
        //        document.getElementById("search_gifts").style.display="none";

        //        //hide about:
        //        document.getElementById("wishlists_label").style.display="none";
        //        break;

        //    default:
        //        break;
        //}
    }
}

mainPage.onPagedRecived = function () {
    $("#MainAppWindow").html(Global_index_BaseHTMLData);

    // all the btn registered here

    let loginBtn = document.querySelector(".login-btn");
    loginBtn.onclick = mainPage.onLoginClicked;

    let signinBtn = document.querySelector(".signin-btn");
    signinBtn.onclick = mainPage.onSignUpClicked;

    let logoutBtn = document.querySelector("#logout_button");
    logoutBtn.onclick = mainPage.onLogOutClicked;

    let categoryTiles = document.getElementsByClassName("category-click-event");
    for (var i = 0; i < categoryTiles.length; i++) {
        categoryTiles[i].onclick = mainPage.onCategoryClicked;
    }

    Platform.GetLoggedInUserData(mainPage.onUserLogedinResponce);

}

mainPage.onUserLogedinResponce = function (iServerResponce) {
    let userName = "";
    if (iServerResponce.StatusCode === 0) {
        userName = iServerResponce.RetObject.FirstName;
    }
    else {
        console.log(iServerResponce.RetObject);
    }

    mainPage.SetUserInfo(userName);
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

mainPage.onLogOutClicked = function () {
    // do this when btn is clicked
    let lgoout = document.querySelector('.logout-btn');
    Platform.LogoutCurrentUser(mainPage.onLogedoutResponce);
    mainPage.loggedInUser = "";
    mainPage.SetUserInfo("");
}

mainPage.onLogedoutResponce = function (iServerReturn) {
    if (iServerReturn.StatusCode === 0) {
        // fine
    }
    else {
        // log error
        console.log(iServerReturn.RetObject);
    }
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

mainPage.onCategoryClicked = function (iEvent) {
    mainPage.IsLoggedIn(iEvent.target.classList[1]);
}

mainPage.IsLoggedIn = function (iCategoryName) {
    if (mainPage.loggedInUser === "") {
        Platform.IsLogIn(mainPage.onIsloginCallback);
    }
    else {
        if (mainPage.loggedInUser) {
            templatesPage.setPage(iCategoryName); // Navigate to category's page
        }
        else {
            signIn.setPage(); // Go to LogIn page
        }
    }
}

mainPage.onIsloginCallback = function (iResponse) {
    if (iResponse.StatusCode === 0 && iResponse.RetObject){
        mainPage.loggedInUser = "user";
        Platform.GetLoggedInUserData(mainPage.onUserLogedinResponce);
    }
    else{
        mainPage.loggedInUser = "";
    }
}